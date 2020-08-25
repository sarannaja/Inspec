import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CentralpolicyService } from '../services/centralpolicy.service';
import { InspectionplanService } from '../services/inspectionplan.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

import { FormGroup, FormBuilder, FormControl, Validators, FormArray } from '@angular/forms';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from '../services/user.service';
import { NotificationService } from '../services/notification.service';
import { IMyOptions, IMyDateModel } from 'mydatepicker-th';
import { FiscalyearService } from '../services/fiscalyear.service';
import { Log } from 'oidc-client';
import * as _ from 'lodash'
@Component({
  selector: 'app-inspection-plan-department',
  templateUrl: './inspection-plan-department.component.html',
  styleUrls: ['./inspection-plan-department.component.css']
})
export class InspectionPlanDepartmentComponent implements OnInit {

  private myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd/mm/yyyy',
  };
  ProvincialDepartmentSelect: any[] = []
  DepartmentSelect: any[] = []
  MinistrySelect: any[] = []
  PeopleSelect: any[] = []
  resultdepartmentpeople: any = []
  resultpeople: any = []
  resultinspectionplan: any = []
  resultcentralpolicy: any = []
  inspectionplan: Array<any> = []
  id
  provinceid
  name: any
  modalRef: BsModalRef;
  selectdatacentralpolicy: any[] = []
  FormOther: FormGroup
  Form: FormGroup
  Form2: FormGroup
  EditForm: FormGroup
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
  resultprovincialdepartmentpeople: any = []
  resultdetailcentralpolicy: any = []
  resultfiscalyear: any = []
  selectdataministrypeople: any = [];
  selectdatapeople: any = [];
  selectdatadepartmentpeople: any = [];
  selectdataprovincialdepartmentpeople: any = [];
  startDate: any;
  endDate: any;
  startDate2: any;
  endDate2: any;
  currentyear
  url = "";
  rolecreatedby
  delid
  editid
  checkInspec: Boolean;
  year
  ministryuserdata: any = [];
  departmentuserdata: any = [];
  peopleuserdata: any = [];
  provincialdepartmentuserdata: any = [];
  userProvince: any[] = []
  ministryId
  watch

  constructor(private modalService: BsModalService,
    private notificationService: NotificationService,
    private userservice: UserService,
    private fiscalyearservice: FiscalyearService,
    @Inject('BASE_URL') baseUrl: string,
    private router: Router, private fb: FormBuilder, private centralpolicyservice: CentralpolicyService, private inspectionplanservice: InspectionplanService, private activatedRoute: ActivatedRoute, private authorize: AuthorizeService, private userService: UserService,) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.provinceid = activatedRoute.snapshot.paramMap.get('provinceid')
    this.name = activatedRoute.snapshot.paramMap.get('name')
    this.watch = activatedRoute.snapshot.paramMap.get('watch')
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
            this.userProvince = result[0].userProvince
            this.ministryId = result[0].ministryId
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
      ],
      "language": {
        "lengthMenu": "แสดง  _MENU_  รายการ",
        "search": "ค้นหา:",
        "info": "แสดง _START_ ถึง _END_ จาก _TOTAL_ แถว",
        "infoEmpty": "แสดง 0 ของ 0 รายการ",
        "zeroRecords": "ไม่พบข้อมูล",
        "paginate": {
          "first": "หน้าแรก",
          "last": "หน้าสุดท้าย",
          "next": "ต่อไป",
          "previous": "ย้อนกลับ"
        },
      }
    };

    this.getCurrentYear();
    this.getTimeline();
    this.getScheduleData();

    this.getministryuser();
    this.getdepartmentuser();
    this.getpeopleuser();
    this.getprovincialdepartmentuser();

    // await this.getMinistryPeople();
    // await this.getDepartmentPeople();
    // await this.getUserPeople();
    // await this.getProvincialDepartmentPeople();

    this.Form = this.fb.group({
      CentralpolicyId: new FormControl(null, [Validators.required]),

      startdate: new FormControl(null, [Validators.required]),
      enddate: new FormControl(null, [Validators.required]),

      notificationdate: new FormControl(null, [Validators.required]),
      deadlinedate: new FormControl(null, [Validators.required]),
    })

    this.FormOther = this.fb.group({
      title: new FormControl(null, [Validators.required]),
      start_date: new FormControl(null, [Validators.required]),
      end_date: new FormControl(null, [Validators.required]),
      year: new FormControl(1, [Validators.required]),
      type: new FormControl("อื่นๆ", [Validators.required]),
      // files: new FormControl(null, [Validators.required]),
      ProvinceId: new FormControl(1, [Validators.required]),
      // status: new FormControl("ร่างกำหนดการ", [Validators.required]),
      input: new FormArray([])
    })
    // this.Form.patchValue({
    //   startdate: this.timelineData.startDate,
    //   enddate: this.timelineData.endDate
    // })

    this.Form2 = this.fb.group({
      UserPeopleId: new FormControl(null),
      UserMinistryId: new FormControl(null),
      UserDepartmentId: new FormControl(null),
      UserProvincialDepartmentId: new FormControl(null),
    })
    this.EditForm = this.fb.group({
      title: new FormControl(null),
      year: new FormControl(null),
      type: new FormControl('อื่นๆ'),
    });
  }

  getTimeline() {
    this.inspectionplanservice.getTimeline(this.id).subscribe(res => {
      console.log("Timeline: ", res);
      this.timelineData = res.timelineData;
      this.startDate = this.time(this.timelineData.startDate)
      this.endDate = this.time(this.timelineData.endDate)
      this.year = this.getyear(this.timelineData.startDate)
      // alert(JSON.stringify(this.year))
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
      this.getinspectionplanservice();
    })
  }

  async openModal(template: TemplateRef<any>) {

    this.getMinistryPeople();
    this.getDepartmentPeople();
    this.getUserPeople();
    this.getProvincialDepartmentPeople();

    this.checkInspec = null;
    this.modalRef = this.modalService.show(template);
  }

  openModal2(template: TemplateRef<any>, delid) {
    this.delid = delid
    this.modalRef = this.modalService.show(template);
  }

  openModaledit(template: TemplateRef<any>, editid) {
    // alert(editid)
    console.log(editid);
    this.editid = editid



    this.getDetailCentralpolicy();
    this.getinspectionplanservice2();
    this.getFiscalyearservice();
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
  DetailCentralPolicy(id: any, watch) {
    // alert(watch)
    this.inspectionplanservice.getcentralpolicyprovinceid(id, this.provinceid).subscribe(result => {
      console.log("result123", result);
      this.centralpolicyprovinceid = result
      // this.resultinspectionplan = result[0].centralPolicyEvents //Chose
      this.router.navigate(['/centralpolicy/detailcentralpolicyprovince', result, { planId: this.id, watch: watch }])
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
      // this.modalService.show('modaldeleteProvince');

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

  async getinspectionplanservice() {

    await this.inspectionplanservice.getinspectionplandata(this.id, this.provinceid).subscribe(async result => {
      console.log("result", result);

      this.resultinspectionplan = result //Chose
      let test1 = result.test;
      let test2 = result.inspectionplandata;

      let data1: any = [];
      let data2: any = [];
      this.data = [];

      await test1.forEach(element => {
        data1.push(element)
      });
      console.log("Data 1: ", data1);

      await test2.forEach(element => {
        element.centralPolicyEvents.forEach(element2 => {
          data2.push(element2)
        });
      });
      console.log("Data 2: ", data2);

      await data1.forEach(async element => {
        await data2.forEach(async element2 => {
          if (element2.centralPolicyId == element.centralPolicyId) {
            await this.data.push(element)
          }
        });
      });
      // this.loading = true;
      console.log("RESULTS: ", this.data);
      await this.inspectionplanservice.getcentralpolicydata(this.provinceid, this.year)
        .subscribe(async result => {
          this.resultcentralpolicy = result //All
          await this.getRecycled()
          // alert(JSON.stringify(this.resultcentralpolicy))
        })
      this.loading = true;
    })

  }

  async getRecycled() {
    this.selectdatacentralpolicy = []
    this.inspectionplan = this.resultinspectionplan.test
    // && this.resultcentralpolicy[i].fiscalYear.year == this.currentyear

    if (this.inspectionplan.length == 0) {
      for (var i = 0; i < this.resultcentralpolicy.length; i++) {
        if (this.resultcentralpolicy[i].status == "ใช้งานจริง") {
          await this.selectdatacentralpolicy.push({ value: this.resultcentralpolicy[i].id, label: this.resultcentralpolicy[i].title })
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
            await this.selectdatacentralpolicy.push({ value: this.resultcentralpolicy[i].id, label: this.resultcentralpolicy[i].title })
          }
        }
      }
    }
    this.loading = true;
  }

  async getMinistryPeople() {

    await this.userservice.getuserdata(6).subscribe(async result => {
      // alert(JSON.stringify(result))
      this.selectdataministrypeople = []
      this.resultministrypeople = result // All
      console.log("Ministry: ", this.resultministrypeople);
      for (var i = 0; i < this.resultministrypeople.length; i++) {
        var checked = _.filter(this.resultministrypeople[i].userProvince, (v) => _.includes(this.userProvince.map(result => { return result.provinceId }), v.provinceId)).length
        if (checked > 0) {
          await this.selectdataministrypeople.push({ value: this.resultministrypeople[i].id, label: this.resultministrypeople[i].ministries.name + " - " + this.resultministrypeople[i].name })
        }
      }

      var data: any[] = this.ministryuserdata.map(result => {
        return result.user.id
      })
      this.MinistrySelect = _.filter(this.selectdataministrypeople, (v) => !_.includes(
        data, v.value
      ))
      // alert(JSON.stringify(this.selectdataministrypeople))
    })
  }
  async getUserPeople() {
    this.selectdatapeople = []
    await this.userservice.getuserdata(7).subscribe(async result => {
      this.resultpeople = result
      console.log("tttt:", this.resultpeople);
      for (var i = 0; i < this.resultpeople.length; i++) {
        await this.selectdatapeople.push({ value: this.resultpeople[i].id, label: "ด้าน" + this.resultpeople[i].side + " - " + this.resultpeople[i].name })
      }

      var data: any[] = this.peopleuserdata.map(result => {
        return result.user.id
      })
      this.PeopleSelect = _.filter(this.selectdatapeople, (v) => !_.includes(
        data, v.value
      ))

    })
  }
  async getDepartmentPeople() {
    this.selectdatadepartmentpeople = []
    await this.userservice.getuserdata(10).subscribe(async result => {
      this.resultdepartmentpeople = result // All
      for (var i = 0; i < this.resultdepartmentpeople.length; i++) {
        if (this.ministryId == this.resultdepartmentpeople[i].ministryId) {
          await this.selectdatadepartmentpeople.push({ value: this.resultdepartmentpeople[i].id, label: this.resultdepartmentpeople[i].ministries.name + " - " + this.resultdepartmentpeople[i].name })
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
  async getProvincialDepartmentPeople() {
    this.selectdataprovincialdepartmentpeople = []
    await this.userservice.getuserdata(9).subscribe(async result => {
      this.resultprovincialdepartmentpeople = result
      console.log("tttt:", this.resultprovincialdepartmentpeople);
      for (var i = 0; i < this.resultprovincialdepartmentpeople.length; i++) {
        await this.selectdataprovincialdepartmentpeople.push({ value: this.resultprovincialdepartmentpeople[i].id, label: this.resultprovincialdepartmentpeople[i].provincialDepartments.name + " - " + this.resultprovincialdepartmentpeople[i].name })
      }

      console.log("this.provincialdepartmentuserdata", this.provincialdepartmentuserdata);

      var data: any[] = this.provincialdepartmentuserdata.map(result => {
        return result.user.id
      })
      this.ProvincialDepartmentSelect = _.filter(this.selectdataprovincialdepartmentpeople, (v) => !_.includes(
        data, v.value
      ))

    })
  }
  getDetailCentralpolicy() {
    this.inspectionplanservice.getcentralpolicyeventdata(this.editid)
      .subscribe(result => {
        this.resultdetailcentralpolicy = result;
        console.log("RES: ", this.resultdetailcentralpolicy);
        this.startDate2 = this.time(this.resultdetailcentralpolicy.startDate);
        this.endDate2 = this.time(this.resultdetailcentralpolicy.endDate);
        // this.EditForm.patchValue({
        //   title: this.resultdetailcentralpolicy.title,
        //   year: this.resultdetailcentralpolicy.fiscalYearId.toString(),
        //   type: this.resultdetailcentralpolicy.type,
        //   status: this.resultdetailcentralpolicy.status
        // });
        this.EditForm.patchValue({
          year: this.resultdetailcentralpolicy.centralPolicy.fiscalYearId
        })
      });

  }
  getinspectionplanservice2() {
    this.inspectionplanservice.getinspectionplandata(this.id, this.provinceid).subscribe(result => {
      console.log("result", result);

      this.resultinspectionplan = result.inspectionplandata

      this.Form.patchValue({
        start_date: this.resultinspectionplan[0].startDate,
        end_date: this.resultinspectionplan[0].endDate,
        ProvinceId: this.resultinspectionplan[0].province.id,
      })

      // alert(JSON.stringify(this.resultinspectionplan))
    })
  }
  getFiscalyearservice() {
    this.fiscalyearservice.getfiscalyeardata().subscribe(result => {
      this.resultfiscalyear = result
    });
  }
  async storeMinistryPeople(value: any) {
    // alert(JSON.stringify(value))
    // console.log("storeMinistryPeople", this.data)
    console.log("data", this.data[0]);

    for (let j = 0; j < this.data.length; j++) {

      // alert(JSON.stringify(this.data[j].centralPolicyId))

      // let UserPeopleId: any[] = value.UserPeopleId
      await this.inspectionplanservice.getcentralpolicyprovinceid(this.data[j].centralPolicyId, this.data[j].inspectionPlanEvent.provinceId).subscribe(result => {

        this.centralpolicyservice.addCentralpolicyUser(value, result, this.userid, this.id).subscribe(response => {
          console.log(value);

          // for (let i = 0; i < UserPeopleId.length; i++) {
          //   this.notificationService.addNotification(this.data[j].centralPolicyId, this.provinceid, UserPeopleId[i], 1, 1)
          //     .subscribe(response => {
          //       console.log(response);
          //     })
          // }
          // this.getCentralPolicyProvinceUser();
          // alert(response);
          this.Form2.reset()
          this.modalRef.hide()

          this.getministryuser();
          this.getdepartmentuser();
          this.getpeopleuser();
          this.getprovincialdepartmentuser();
        })
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
    console.log("Date: ", date);

    let ssss = new Date(date)
    var new_date = {
      year: ssss.getFullYear(),
      month: ssss.getMonth() + 1,
      day: ssss.getDate()
    }
    console.log("newDate: ", new_date);

    return new_date
  }

  getyear(date) {
    console.log("Date: ", date);

    let ssss = new Date(date)
    var new_date = {
      year: ssss.getFullYear(),
    }
    console.log("newDate: ", new_date);

    return new_date
  }

  onStartDateChanged(event: IMyDateModel) {
    // alert(JSON.stringify(event))
    this.startDate = event.date;
    this.startDate2 = event.date;
    console.log("SS: ", this.startDate);
  }

  onEndDateChanged(event: IMyDateModel) {
    // alert(JSON.stringify(event))]
    this.endDate = event.date;
    this.endDate2 = event.date;
    console.log("EE: ", this.endDate);
  }
  EditPlanDate() {
    // alert(JSON.stringify(this.startDate))
    this.inspectionplanservice.editplandate(this.id, this.startDate, this.endDate).subscribe(response => {
      this.modalRef.hide()
      this.getTimeline();
    })
  }
  EditCentralPolicy(value) {
    console.log(value);
    this.inspectionplanservice.editcentralpolicy(this.editid, this.startDate2, this.endDate2, value).subscribe(response => {
      this.modalRef.hide()
      this.getinspectionplanservice();
    })
  }
  deleteDate() {
    this.inspectionplanservice.deleteplandate(this.id).subscribe(response => {
      this.modalRef.hide()
      this.router.navigate(['inspectionplanevent'])
    })
  }

  DeleteCentralPolicyEvent(value) {
    // alert(value)
    this.inspectionplanservice.deleteCentralPolicyEvent(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      // location.reload();
      this.loading = false

      this.getinspectionplanservice();

    })
  }
  DeleteCentralPolicy(value) {
    this.inspectionplanservice.deleteCentralPolicy(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.loading = false
      this.getinspectionplanservice()
    })
  }

  inspect(myradio) {
    // alert(myradio)
    this.checkInspec = true;
  }
  notInspec(value) {
    // alert(value)
    this.checkInspec = false;
  }

  storeInspectionPlan(value) {
    console.log("FORM: ", value);
    this.inspectionplanservice.addInspectionPlan(value, this.userid, this.id, this.provinceid, this.startDate, this.endDate, this.year).subscribe(response => {
      console.log("create Inspection plan: ", value);
      // this.Form.reset()
      // window.history.back();

      this.FormOther.reset()
      this.modalRef.hide()

      this.loading = false;
      this.data = [];
      this.getinspectionplanservice()
    })
  }

  getministryuser() {
    this.centralpolicyservice.getcentralpolicyministrydata(this.id).subscribe(result => {
      this.ministryuserdata = result.filter(
        (thing, i, arr) => arr.findIndex(t => t.user.id === thing.user.id) === i
      );
      console.log("this.ministryuserdata", this.ministryuserdata);
    })
  }
  getdepartmentuser() {
    this.centralpolicyservice.getcentralpolicydepartmentdata(this.id).subscribe(result => {
      this.departmentuserdata = result.filter(
        (thing, i, arr) => arr.findIndex(t => t.user.id === thing.user.id) === i
      );
      console.log("this.departmentuserdata", this.departmentuserdata);
    })
  }

  getprovincialdepartmentuser() {
    this.centralpolicyservice.getcentralpolicyprovincialdepartmentdata(this.id).subscribe(result => {
      this.provincialdepartmentuserdata = result.filter(
        (thing, i, arr) => arr.findIndex(t => t.user.id === thing.user.id) === i
      );
      console.log("this.departmentuserdata", this.departmentuserdata);
    })
  }

  getpeopleuser() {
    this.centralpolicyservice.getcentralpolicypeopledata(this.id).subscribe(result => {
      this.peopleuserdata = result.filter(
        (thing, i, arr) => arr.findIndex(t => t.user.id === thing.user.id) === i
      );
      console.log("this.peopleuserdata", this.peopleuserdata);
    })
  }
  // var distinctThings: any[] = result.filter(
  //       (thing, i, arr) => arr.findIndex(t => t.id === thing.id) === i
  //     );
}
