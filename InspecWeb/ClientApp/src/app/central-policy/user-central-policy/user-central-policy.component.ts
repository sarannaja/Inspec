import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { CentralpolicyService } from '../../services/centralpolicy.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from "ngx-spinner";
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { InspectionplanService } from 'src/app/services/inspectionplan.service';
import { NotificationService } from 'src/app/services/notification.service';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';
import * as _ from 'lodash'
@Component({
  selector: 'app-user-central-policy',
  templateUrl: './user-central-policy.component.html',
  styleUrls: ['./user-central-policy.component.css']
})
export class UserCentralPolicyComponent implements OnInit {

  resultcentralpolicy: any = []
  ScheduleData: any = [];
  delid: any
  modalRef: BsModalRef;
  dtOptions: DataTables.Settings = {};
  loading = false;
  userid: string
  centralpolicyprovinceid: any
  id
  provinceid
  timelineData: any = [];
  Form: FormGroup;
  assignForm: FormGroup;
  assigninternalForm: FormGroup;
  answer
  status
  ForwardName
  ForwardCount
  role_id
  checkType: any;
  peopletype: any;

  report: string = '';
  ministryId
  userProvince: any[] = []

  ministryuserdata: any = [];
  selectdataministrypeople: any = [];
  resultministrypeople: any = []
  MinistrySelect: any[] = []

  departmentuserdata: any = [];
  selectdatadepartmentpeople: any = [];
  resultdepartmentpeople: any = []
  DepartmentSelect: any[] = []

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private centralpolicyservice: CentralpolicyService,
    private modalService: BsModalService,
    private authorize: AuthorizeService,
    private activatedRoute: ActivatedRoute,
    private inspectionplanservice: InspectionplanService,
    private notificationService: NotificationService,
    private userService: UserService,
    private spinner: NgxSpinnerService) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.provinceid = activatedRoute.snapshot.paramMap.get('provinceid')
  }

  ngOnInit() {
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
        // alert(this.userid)
        this.userService.getuserfirstdata(this.userid)
          .subscribe(result => {
            // this.resultuser = result;
            //console.log("test" , this.resultuser);
            this.role_id = result[0].role_id
            this.userProvince = result[0].userProvince
            this.ministryId = result[0].ministryId
            // alert(this.role_id)
          })
      })
    this.spinner.show();

    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [3],
          orderable: false
        }
      ]

    };

    this.Form = this.fb.group({
      answer: new FormControl(null, [Validators.required]),
    })
    this.assignForm = this.fb.group({
      assign: new FormControl(null, [Validators.required]),

      department: new FormControl(null, [Validators.required]),
      position: new FormControl(null, [Validators.required]),
      phone: new FormControl(null, [Validators.required]),
      email: new FormControl(null, [Validators.required]),

      // type: new FormControl(null),
    })

    this.assigninternalForm = this.fb.group({
      UserId: new FormControl(null, [Validators.required]),
    })

    this.getstatus();
    this.getTimeline();
    this.getScheduleData();

    this.getministryuser();
    this.getdepartmentuser();
  }

  getstatus() {
    this.centralpolicyservice.getcentralpolicyuserinviteddata(this.userid, this.id)
      .subscribe(result => {
        this.resultcentralpolicy = result
        this.status = result[0].status
        this.ForwardName = result[0].forwardName

        this.ForwardCount = result[0].forwardCount

        this.report = result[0].report

        this.loading = true;
        this.spinner.hide();
        console.log("resultcentralpolicyDATA: ", this.resultcentralpolicy);
      })
  }
  getTimeline() {
    this.inspectionplanservice.getTimeline(this.id).subscribe(res => {
      console.log("Timeline: ", res);
      this.timelineData = res.timelineData;
    })
  }

  openModal(template: TemplateRef<any>, id) {
    this.delid = id;
    this.modalRef = this.modalService.show(template);
  }

  openModal2(template: TemplateRef<any>, answer) {

    this.Form.patchValue({
      answer: this.report
    })

    this.getministryuser();
    this.getdepartmentuser();

    this.answer = answer
    this.modalRef = this.modalService.show(template);
  }

  Subject(id) {
    this.router.navigate(['/subject', id])
  }

  deleteCentralPolicy(value) {
    this.centralpolicyservice.deleteCentralPolicy(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.centralpolicyservice.getcentralpolicydata().subscribe(result => {
        this.resultcentralpolicy = result
        console.log(this.resultcentralpolicy);
      })
    })
  }

  CreateCentralPolicy() {
    this.router.navigate(['/centralpolicy/createcentralpolicy'])
  }
  DetailCentralPolicy(id: any) {
    this.router.navigate(['/centralpolicy/detailcentralpolicy', id])
  }

  AcceptCentralPolicy(id: any, cenid, proid) {
    this.inspectionplanservice.getcentralpolicyprovinceid(cenid, proid).subscribe(result => {
      console.log("result123", result);
      this.centralpolicyprovinceid = result
      this.router.navigate(['/acceptcentralpolicy', id, { centralpolicyproviceid: result, cenid: cenid, planid: this.id }])
    })
  }

  gotoReport(id: any, cenid, proid) {
    this.inspectionplanservice.getcentralpolicyprovinceid(cenid, proid).subscribe(result => {
      console.log("result123", result);
      this.centralpolicyprovinceid = result

      this.router.navigate(['/reportcentralpolicy', id, { centralpolicyproviceid: result }])

    })
  }

  storeAccept(answer) {
    console.log("this.resultcentralpolicy.centralPolicy.id", this.resultcentralpolicy);

    this.centralpolicyservice.acceptCentralpolicy(answer, this.id, this.userid)
      .subscribe(response => {
        console.log(response);

        let CentralpolicyId: any[] = this.resultcentralpolicy
        for (let i = 0; i < CentralpolicyId.length; i++) {
          this.notificationService.addNotification(CentralpolicyId[i].centralPolicyId, this.provinceid, this.userid, 2, this.id,null)
            .subscribe(response => {
              console.log(response);
            })
        }
        // location.reload();
        // this.notificationService.addNotification(this.resultdetailcentralpolicy.id, this.provinceid, this.userid, 2, 1)
        // .subscribe(response => {
        //   console.log(response);
        // })

        this.modalRef.hide()
        this.getstatus();


        // this.router.navigate(['calendaruser'])
      })
  }

  storeAccept2(value, answer) {
    // alert(JSON.stringify(value))
    this.centralpolicyservice.acceptCentralpolicy2(answer, this.id, this.userid, value)
      .subscribe(response => {
        console.log(response);

        let CentralpolicyId: any[] = this.resultcentralpolicy
        for (let i = 0; i < CentralpolicyId.length; i++) {
          this.notificationService.addNotification(CentralpolicyId[i].centralPolicyId, this.provinceid, this.userid, 2, this.id,null)
            .subscribe(response => {
              console.log(response);
            })
        }
        this.modalRef.hide()
        this.getstatus();
      })
  }

  getScheduleData() {
    this.inspectionplanservice.getScheduleData(this.id, this.provinceid).subscribe(res => {
      console.log("ScheduleData: ", res);
      this.ScheduleData = res;
    })
  }

  sendAssign(value) {
    this.centralpolicyservice.sendAssign(value, this.id, this.userid).subscribe(response => {
      console.log(response);

      let CentralpolicyId: any[] = this.resultcentralpolicy
      for (let i = 0; i < CentralpolicyId.length; i++) {
        this.notificationService.addNotification(CentralpolicyId[i].centralPolicyId, this.provinceid, this.userid, 2, this.id,null)
          .subscribe(response => {
            console.log(response);
          })
      }

      this.modalRef.hide();
      // this.router.navigate(['calendaruser'])
      this.getstatus();
    })
  }
  sendAssignInternal(value) {
    // alert(JSON.stringify(value))
    this.centralpolicyservice.sendAssignInternal(value, this.id, this.userid).subscribe(response => {
      console.log(response);

      let CentralpolicyId2: any[] = this.resultcentralpolicy
      for (let i = 0; i < CentralpolicyId2.length; i++) {
        this.notificationService.addNotification(CentralpolicyId2[i].centralPolicyId, this.provinceid, value.UserId, 19, this.id,null)
          .subscribe(response => {
            console.log(response);
          })
      }

      let CentralpolicyId: any[] = this.resultcentralpolicy
      for (let i = 0; i < CentralpolicyId.length; i++) {
        this.notificationService.addNotification(CentralpolicyId[i].centralPolicyId, this.provinceid, this.userid, 2, this.id,null)
          .subscribe(response => {
            console.log(response);
          })
      }
      this.assigninternalForm.reset()
      this.modalRef.hide();
      // this.router.navigate(['calendaruser'])
      this.getstatus();
    })
  }
  internal(myradio) {
    // alert(myradio)
    this.checkType = 1;
  }
  notinternal(value) {
    // alert(value)
    this.checkType = 2;
  }

  checktype(value) {
    this.assigninternalForm.reset()
    // alert(JSON.stringify(value))
    if (value == "ผู้ตรวจกระทรวง") {
      this.peopletype = 1;
    } else if (value == "ผู้ตรวจกรม") {
      this.peopletype = 2;
    } else {
      this.peopletype = 0;
    }
  }

  getministryuser() {
    this.centralpolicyservice.getcentralpolicyministrydata(this.id).subscribe(result => {
      this.ministryuserdata = result.filter(
        (thing, i, arr) => arr.findIndex(t => t.user.id === thing.user.id) === i
      );
      console.log("this.ministryuserdata", this.ministryuserdata);

      this.getMinistryPeople();
    })
  }
  async getMinistryPeople() {
    await this.userService.getuserdata(6).subscribe(async result => {
      this.selectdataministrypeople = []
      this.resultministrypeople = result // All
      console.log("Ministry: ", this.resultministrypeople);
      for (var i = 0; i < this.resultministrypeople.length; i++) {

        var checked = _.filter(this.resultministrypeople[i].userProvince, (v) => _.includes(this.userProvince.map(result => { return result.provinceId }), v.provinceId)).length
        if (checked > 0) {
          if (this.ministryId == this.resultministrypeople[i].ministryId) {
            await this.selectdataministrypeople.push({ value: this.resultministrypeople[i].id, label: this.resultministrypeople[i].ministries.name + " - " + this.resultministrypeople[i].name })
          }
        }

      }

      var data: any[] = this.ministryuserdata.map(result => {
        return result.user.id
      })
      this.MinistrySelect = _.filter(this.selectdataministrypeople, (v) => !_.includes(
        data, v.value
      ))
    })
  }

  getdepartmentuser() {
    this.centralpolicyservice.getcentralpolicydepartmentdata(this.id).subscribe(result => {
      this.departmentuserdata = result.filter(
        (thing, i, arr) => arr.findIndex(t => t.user.id === thing.user.id) === i
      );
      console.log("this.departmentuserdata", this.departmentuserdata);

      this.getDepartmentPeople();
    })
  }
  async getDepartmentPeople() {
    this.selectdatadepartmentpeople = []
    await this.userService.getuserdata(10).subscribe(async result => {
      this.resultdepartmentpeople = result // All
      for (var i = 0; i < this.resultdepartmentpeople.length; i++) {
        var checked = _.filter(this.resultdepartmentpeople[i].userProvince, (v) => _.includes(this.userProvince.map(result => { return result.provinceId }), v.provinceId)).length
        if (checked > 0) {
          if (this.ministryId == this.resultdepartmentpeople[i].ministryId) {
            await this.selectdatadepartmentpeople.push({ value: this.resultdepartmentpeople[i].id, label: this.resultdepartmentpeople[i].ministries.name + " - " + this.resultdepartmentpeople[i].name })
          }
        }
      }

      var data: any[] = this.departmentuserdata.map(result => {
        return result.user.id
      })
      this.DepartmentSelect = _.filter(this.selectdatadepartmentpeople, (v) => !_.includes(
        data, v.value
      ))

    })
  }
}
