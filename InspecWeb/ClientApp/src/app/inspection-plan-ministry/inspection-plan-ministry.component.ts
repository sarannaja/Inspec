import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CentralpolicyService } from '../services/centralpolicy.service';
import { InspectionplanService } from '../services/inspectionplan.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from '../services/user.service';
import { NotificationService } from '../services/notification.service';
import { IMyOptions, IMyDateModel } from 'mydatepicker-th';
import { FiscalyearService } from '../services/fiscalyear.service';
@Component({
  selector: 'app-inspection-plan-ministry',
  templateUrl: './inspection-plan-ministry.component.html',
  styleUrls: ['./inspection-plan-ministry.component.css']
})
export class InspectionPlanMinistryComponent implements OnInit {

  private myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd/mm/yyyy',
  };

  resultpeople: any = []
  resultinspectionplan: any = []
  resultcentralpolicy: any = []
  inspectionplan: Array<any> = []
  id
  provinceid
  name: any
  modalRef: BsModalRef;
  selectdatacentralpolicy: any[] = []
  Form: FormGroup
  Form2: FormGroup
  CentralpolicyId: any
  loading = false;
  data: any = [];
  userid: string
  dtOptions: DataTables.Settings = {};
  centralpolicyprovinceid: any
  role_id
  timelineData: any = [];
  ScheduleData: any = [];
  resultministrypeople: any = []
  selectdataministrypeople: any = [];
  startDate: any;
  endDate: any;
  currentyear
  url = "";
  rolecreatedby

  constructor(private modalService: BsModalService,
    private notificationService: NotificationService,
    private userservice: UserService,
    private fiscalyearservice: FiscalyearService,
    @Inject('BASE_URL') baseUrl: string,
    private router: Router, private fb: FormBuilder, private centralpolicyservice: CentralpolicyService, private inspectionplanservice: InspectionplanService, private activatedRoute: ActivatedRoute, private authorize: AuthorizeService, private userService: UserService,) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.provinceid = activatedRoute.snapshot.paramMap.get('provinceid')
    this.name = activatedRoute.snapshot.paramMap.get('name')
    this.url = baseUrl + 'inspectionplanevent';
  }

  async ngOnInit() {
    // alert(this.provinceid)

    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        // this.role_id = result.role_id
        // console.log(result);
        // alert(this.role_id)

        this.userService.getuserfirstdata(this.userid)
          .subscribe(result => {
            // this.resultuser = result;
            //console.log("test" , this.resultuser);
            this.role_id = result[0].role_id
            // alert(this.role_id)
          })
      })

    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [3],
          orderable: false
        }
      ]
    };

    this.getCurrentYear();
    this.getinspectionplanservice();
    this.getTimeline();
    this.getScheduleData();
    await this.getMinistryPeople();

    this.Form = this.fb.group({
      CentralpolicyId: new FormControl(null, [Validators.required]),

      startdate: new FormControl(null, [Validators.required]),
      enddate: new FormControl(null, [Validators.required]),

      notificationdate: new FormControl(null, [Validators.required]),
      deadlinedate: new FormControl(null, [Validators.required]),
    })

    // this.Form.patchValue({
    //   startdate: this.timelineData.startDate,
    //   enddate: this.timelineData.endDate
    // })

    this.Form2 = this.fb.group({
      UserPeopleId: new FormControl(null, [Validators.required]),
    })

  }

  getTimeline() {
    this.inspectionplanservice.getTimeline(this.id).subscribe(res => {
      console.log("Timeline: ", res);
      this.timelineData = res.timelineData;
      this.startDate = this.time(this.timelineData.startDate)
      this.endDate = this.time(this.timelineData.endDate)
    })
  }

  getCurrentYear() {
    this.fiscalyearservice.getcurrentyeardata().subscribe(result => {
      this.currentyear = result
    })
  }

  getScheduleData() {
    this.inspectionplanservice.getScheduleData(this.id, this.provinceid).subscribe(res => {
      console.log("ScheduleData: ", res);
      this.ScheduleData = res;
    })
  }

  async openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  Subject(id) {
    this.router.navigate(['/subject', id])
  }

  CrateInspectionPlan(id) {
    this.router.navigate(['/inspectionplan/createinspectionplan', id, { proid: this.provinceid }])
  }
  EditInspectionPlan(id: any) {
    this.router.navigate(['/inspectionplan/editinspectionplan', id])
  }
  DetailCentralPolicy(id: any) {
    this.inspectionplanservice.getcentralpolicyprovinceid(id, this.provinceid).subscribe(result => {
      console.log("result123", result);
      this.centralpolicyprovinceid = result
      // this.resultinspectionplan = result[0].centralPolicyEvents //Chose
      this.router.navigate(['/centralpolicy/detailcentralpolicyprovince/ministry', result, { planId: this.id }])
    })
    // var id = this.centralpolicyprovinceid
    // this.router.navigate(['/centralpolicy/detailcentralpolicyprovince', id])
    // this.router.navigate(['/centralpolicy/detailcentralpolicy', id])
  }
  AcceptCentralPolicy(id: any) {
    this.router.navigate(['/acceptcentralpolicy', id])
  }
  storeCentralPolicyEventRelation(value) {
    let CentralpolicyId: any[] = value.CentralpolicyId
    // alert(JSON.stringify(value))
    this.inspectionplanservice.addCentralPolicyEvent(value, this.id, this.userid, this.provinceid, this.startDate, this.endDate).subscribe(response => {


      this.Form.reset()
      this.modalRef.hide()

      for (let i = 0; i < CentralpolicyId.length; i++) {
        this.notificationService.addNotification(CentralpolicyId[i], this.provinceid, this.userid, 3, 1)
          .subscribe(response => {
            console.log(response);
          })
      }
      // alert("OK")
      // this.loading = false
      // this.inspectionplanservice.getinspectionplandata(this.id).subscribe(result => {
      //   this.resultinspectionplan = result[0].centralPolicyEvents //Chose
      //   console.log(this.resultinspectionplan[0].centralPolicyEvents.centralPolicy);
      // })
      this.loading = false;
      this.data = [];
      this.getinspectionplanservice()
    })
  }

  getinspectionplanservice() {
    this.inspectionplanservice.getinspectionplandata(this.id, this.provinceid).subscribe(result => {
      console.log("result", result);

      this.resultinspectionplan = result //Chose
      let test1 = result.test;
      let test2 = result.inspectionplandata;

      let data1: any = [];
      let data2: any = [];

      test1.forEach(element => {
        data1.push(element)
      });
      console.log("Data 1: ", data1);

      test2.forEach(element => {
        element.centralPolicyEvents.forEach(element2 => {
          data2.push(element2)
        });
      });
      console.log("Data 2: ", data2);

      data1.forEach(element => {
        data2.forEach(element2 => {
          if (element2.centralPolicyId == element.centralPolicyId) {
            this.data.push(element)
          }
        });
      });
      // this.loading = true;
      console.log("RESULTS: ", this.data);
      this.inspectionplanservice.getcentralpolicydata(this.provinceid)
        .subscribe(result => {
          this.resultcentralpolicy = result //All
          this.getRecycled()
          // alert(JSON.stringify(this.resultcentralpolicy))
        })
    })
  }

  getRecycled() {
    this.selectdatacentralpolicy = []
    this.inspectionplan = this.resultinspectionplan.test
    // && this.resultcentralpolicy[i].fiscalYear.year == this.currentyear

    if (this.inspectionplan.length == 0) {
      for (var i = 0; i < this.resultcentralpolicy.length; i++) {
        if (this.resultcentralpolicy[i].status == "ใช้งานจริง") {
          this.selectdatacentralpolicy.push({ value: this.resultcentralpolicy[i].id, label: this.resultcentralpolicy[i].title })
        }
      }
    }
    else {
      for (var i = 0; i < this.resultcentralpolicy.length; i++) {
        var n = 0;
        for (var ii = 0; ii < this.inspectionplan.length; ii++) {
          if (this.resultcentralpolicy[i].id == this.inspectionplan[ii].centralPolicyId) {
            n++;
          }
        }
        if (n == 0) {
          if (this.resultcentralpolicy[i].status == "ใช้งานจริง") {
            this.selectdatacentralpolicy.push({ value: this.resultcentralpolicy[i].id, label: this.resultcentralpolicy[i].title })
          }
        }
      }
    }
    this.loading = true;
  }

  async getMinistryPeople() {
    await this.userservice.getuserdata(6).subscribe(async result => {
      // alert(JSON.stringify(result))

      this.resultministrypeople = result // All
      console.log("Ministry: ", this.resultministrypeople);
      for (var i = 0; i < this.resultministrypeople.length; i++) {
        await this.selectdataministrypeople.push({ value: this.resultministrypeople[i].id, label: this.resultministrypeople[i].ministries.name + " - " + this.resultministrypeople[i].name })
      }
      // alert(JSON.stringify(this.selectdataministrypeople))
    })
  }

  storeMinistryPeople(value: any) {
    // console.log("storeMinistryPeople", this.data)
    // alert(JSON.stringify(this.data[0].centralPolicyId))
    for (let j = 0; j < this.data.length; j++) {
      let UserPeopleId: any[] = value.UserPeopleId
      this.centralpolicyservice.addCentralpolicyUser(value, this.data[j].centralPolicyId, this.userid, this.id).subscribe(response => {
        console.log(value);
        this.Form.reset()
        this.modalRef.hide()
        // for (let i = 0; i < UserPeopleId.length; i++) {
        //   this.notificationService.addNotification(this.data[j].centralPolicyId, this.provinceid, UserPeopleId[i], 1, 1)
        //     .subscribe(response => {
        //       console.log(response);
        //     })
        // }
        // this.getCentralPolicyProvinceUser();
      })
    }
  }

  changeplanstatus(value) {
    // alert(this.timelineData.id)

    this.inspectionplanservice.changeplanstatus(this.timelineData.id).subscribe(response => {
      // console.log(value);
      // this.Form.reset()
      this.modalRef.hide()
      // location.reload();
      this.getTimeline();

      this.notificationService.addNotification(1, this.provinceid, 1, 16, this.id)
        .subscribe(response => {
          console.log(response);
        })

    })
  }

  closemodal() {
    this.modalRef.hide()
    this.timelineData.status = "ร่างกำหนดการ"

    // alert( this.timelineData.status)
    // this.getTimeline();
  }

  time(date) {
    var ssss = new Date(date)
    var new_date = { year: ssss.getFullYear(), month: ssss.getMonth() + 1, day: ssss.getDate() }
    return new_date
  }

  onStartDateChanged(event: IMyDateModel) {
    // alert(JSON.stringify(event))
    this.startDate = event.date;
    console.log("SS: ", this.startDate);
  }

  onEndDateChanged(event: IMyDateModel) {
    // alert(JSON.stringify(event))
    this.endDate = event.date;
    console.log("EE: ", this.endDate);
  }
  EditPlanDate() {
    // alert(JSON.stringify(this.startDate))
    this.inspectionplanservice.editplandate(this.id, this.startDate, this.endDate).subscribe(response => {
      this.modalRef.hide()
      this.getTimeline();
    })
  }
  deleteDate() {
    this.inspectionplanservice.deleteplandate(this.id).subscribe(response => {
      this.modalRef.hide()
      this.router.navigate(['inspectionplanevent'])
    })
  }
}
