import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CentralpolicyService } from '../services/centralpolicy.service';
import { InspectionplanService } from '../services/inspectionplan.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

import { FormGroup, FormBuilder, FormControl, Validators, FormArray } from '@angular/forms';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { UserService } from '../services/user.service';
import { NotificationService } from '../services/notification.service';
import { IMyOptions, IMyDateModel } from 'mydatepicker-th';
import { FiscalyearService } from '../services/fiscalyear.service';
import { Log } from 'oidc-client';
import * as _ from 'lodash'
import { NotofyService } from '../services/notofy.service';
import { NgxSpinnerService } from "ngx-spinner";
@Component({
  selector: 'app-inspection-plan-department',
  templateUrl: './inspection-plan-department.component.html',
  styleUrls: ['./inspection-plan-department.component.css']
})
export class InspectionPlanDepartmentComponent implements OnInit {

  myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd/mm/yyyy',
    showClearDateBtn: false,
    editableDateField: false
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
  dtOptions: any = {};
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
  checkInspec: boolean;
  year
  ministryuserdata: any = [];
  departmentuserdata: any = [];
  peopleuserdata: any = [];
  provincialdepartmentuserdata: any = [];
  userProvince: any[] = []
  ministryId
  watch
  submitted = false;
  lastpath
  startDateTime: any;
  endDateTime: any;
  constructor(private modalService: BsModalService,
    private notificationService: NotificationService,
    private userservice: UserService,
    private _NotofyService: NotofyService,
    private fiscalyearservice: FiscalyearService,
    private spinner: NgxSpinnerService,
    @Inject('BASE_URL') baseUrl: string,
    private router: Router, private fb: FormBuilder, private centralpolicyservice: CentralpolicyService, private inspectionplanservice: InspectionplanService, private activatedRoute: ActivatedRoute, private authorize: AuthorizeService, private userService: UserService,) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.provinceid = activatedRoute.snapshot.paramMap.get('provinceid')
    this.name = activatedRoute.snapshot.paramMap.get('name')
    const getLastItem = thePath => thePath.substring(thePath.lastIndexOf('/') + 1)
    this.watch = getLastItem(this.router.url)
    this.url = baseUrl + 'inspectionplanevent';

    this.lastpath = window.location.pathname.split('/')[1];
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

    // this.getCurrentYear();
    this.getTimeline();
    // this.getScheduleData();

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

      // startdate: new FormControl(null, [Validators.required]),
      // enddate: new FormControl(null, [Validators.required]),

      // notificationdate: new FormControl(null, [Validators.required]),
      // deadlinedate: new FormControl(null, [Validators.required]),
    })

    this.FormOther = this.fb.group({
      title: new FormControl(null, [Validators.required]),
      // start_date: new FormControl(null, [Validators.required]),
      // end_date: new FormControl(null, [Validators.required]),
      // year: new FormControl(1, [Validators.required]),
      // type: new FormControl("อื่นๆ", [Validators.required]),
      // files: new FormControl(null, [Validators.required]),
      ProvinceId: new FormControl(this.provinceid, [Validators.required]),
      // status: new FormControl("ร่างกำหนดการ", [Validators.required]),
      // input: new FormArray([])
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
      // year: new FormControl(null),
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
      this.startDateTime = this.gettimetime(this.timelineData.startDate);
      this.endDateTime = this.gettimetime(this.timelineData.endDate);
      this.getCurrentYear();
    })
  }

  gettimetime(date) {
    console.log("Date: ", date);

    let ssss = new Date(date)
    var new_date = {
      year: ssss.getFullYear(),
      month: ssss.getMonth() + 1,
      day: ssss.getDate()
    }
    console.log("newDate: ", new_date);

    return ssss
  }
  
  getCurrentYear() {
    this.fiscalyearservice.getcurrentyeardata(this.year.year).subscribe(result => {
      var current_year = new Date().getFullYear() + 543;
      var current_date = new Date();
      let d3: any
      // alert(JSON.stringify(this.year.year))
      let start_date = new Date(result.startDate)
      let end_date = new Date(result.endDate)

      // alert(JSON.stringify(result))

      this.currentyear = ((current_date.toISOString() > end_date.toISOString())) ? result.year + 1 : result.year
      this.getScheduleData();
      // alert(JSON.stringify(this.currentyear))
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
    this.submitted = false;
    this.loading = false;
    // this.getMinistryPeople();
    // this.getDepartmentPeople();
    this.getUserPeople();
    // this.getProvincialDepartmentPeople();

    this.checkInspec = null;
    this.modalRef = this.modalService.show(template);
  }

  async openModalAddCentralPolicy(template: TemplateRef<any>) {
    this.submitted = false;
    // this.loading = false;
    // this.getMinistryPeople();
    // this.getDepartmentPeople();
    // this.getUserPeople();
    // this.getProvincialDepartmentPeople();

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
      if (this.lastpath == "noauth") {
        this.router.navigate(['/centralpolicy/detailcentralpolicyprovince/department/noauth', result, { planId: this.id, watch: watch }])
      } else {
        this.router.navigate(['/centralpolicy/detailcentralpolicyprovince/department/', result, { planId: this.id, watch: watch }])
      }
    })
    // var id = this.centralpolicyprovinceid
    // this.router.navigate(['/centralpolicy/detailcentralpolicyprovince', id])
    // this.router.navigate(['/centralpolicy/detailcentralpolicy', id])
  }
  AcceptCentralPolicy(id: any) {
    this.router.navigate(['/acceptcentralpolicy', id])
  }
  storeCentralPolicyEventRelation(value) {
    if (this.Form.invalid) {
      this.submitted = true;
      console.log("in1");
      return;
    } else {
      let CentralpolicyId: any[] = value.CentralpolicyId
      // alert(JSON.stringify(value))
      this.inspectionplanservice.addCentralPolicyEvent(value, this.id, this.userid, this.provinceid, this.startDate, this.endDate).subscribe(response => {
        this._NotofyService.onSuccess("เพื่มข้อมูล",)

        this.Form.reset()
        this.modalRef.hide()
        // this.modalService.show('modaldeleteProvince');

        for (let i = 0; i < CentralpolicyId.length; i++) {
          this.notificationService.addNotification(CentralpolicyId[i], this.provinceid, this.userid, 3, 1, null,this.userid)
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
      await this.inspectionplanservice.getcentralpolicydata(this.provinceid, this.currentyear)
      .subscribe(async (result:any) => {
        this.resultcentralpolicy = result.data //All
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

  // async getMinistryPeople() {

  //   await this.userservice.getuserdata(6).subscribe(async result => {
  //     // alert(JSON.stringify(result))
  //     this.selectdataministrypeople = []
  //     this.resultministrypeople = result // All
  //     console.log("Ministry: ", this.resultministrypeople);
  //     if (this.role_id == 3) {
  //       for (var i = 0; i < this.resultministrypeople.length; i++) {

  //         // alert(JSON.stringify(this.resultministrypeople[i].userRegion))

  //         var userregion = "";
  //         for (var j = 0; j < this.resultministrypeople[i].userRegion.length; j++) {

  //           if(this.resultministrypeople[i].userRegion[j].region.name == "เขตตรวจราชส่วนกลาง"){
  //             this.resultministrypeople[i].userRegion[j].region.name = "ส่วนกลาง"
  //           } else {
  //             this.resultministrypeople[i].userRegion[j].region.name = this.resultministrypeople[i].userRegion[j].region.name.replace('เขตตรวจราชการที่','');
  //           }

  //           if (j == (this.resultministrypeople[i].userRegion.length - 1)) {
  //             userregion += this.resultministrypeople[i].userRegion[j].region.name
  //           } else {
  //             userregion += this.resultministrypeople[i].userRegion[j].region.name + ", "
  //           }
  //         }

  //         await this.selectdataministrypeople.push({ value: this.resultministrypeople[i].id, label: this.resultministrypeople[i].ministries.name + " - " + this.resultministrypeople[i].name + " เขต " + userregion })
  //       }
  //     } else {
  //       for (var i = 0; i < this.resultministrypeople.length; i++) {
  //         var checked = _.filter(this.resultministrypeople[i].userProvince, (v) => _.includes(this.userProvince.map(result => { return result.provinceId }), v.provinceId)).length
  //         if (checked > 0) {

  //           var userregion = "";
  //           for (var j = 0; j < this.resultministrypeople[i].userRegion.length; j++) {
  //             if (j == (this.resultministrypeople[i].userRegion.length - 1)) {
  //               userregion += this.resultministrypeople[i].userRegion[j].region.name
  //             } else {
  //               userregion += this.resultministrypeople[i].userRegion[j].region.name + ", "
  //             }
  //           }

  //           await this.selectdataministrypeople.push({ value: this.resultministrypeople[i].id, label: this.resultministrypeople[i].ministries.name + " - " + this.resultministrypeople[i].name + " เขต " + userregion })
  //         }
  //       }
  //     }
  //     var data: any[] = this.ministryuserdata.map(result => {
  //       return result.user.id
  //     })
  //     this.MinistrySelect = _.filter(this.selectdataministrypeople, (v) => !_.includes(
  //       data, v.value
  //     ))
  //     // alert(JSON.stringify(this.selectdataministrypeople))
  //     this.getDepartmentPeople();
  //   })


  // }
  async getUserPeople() {
    this.selectdatapeople = []
    await this.userservice.getuserdata(7).subscribe(async result => {
      this.resultpeople = result
      console.log("tttt:", this.resultpeople);
      // if (this.role_id == 3) {
      //   for (var i = 0; i < this.resultpeople.length; i++) {
      //     await this.selectdatapeople.push({ value: this.resultpeople[i].id, label: "ด้าน" + this.resultpeople[i].sides.name + " - " + this.resultpeople[i].name })
      //   }
      // } else {
        for (var i = 0; i < this.resultpeople.length; i++) {
          var checked = _.filter(this.resultpeople[i].userProvince, (v) => _.includes(this.userProvince.map(result => { return result.provinceId }), v.provinceId)).length
          if (checked > 0) {
            await this.selectdatapeople.push({ value: this.resultpeople[i].id, label: "ด้าน" + this.resultpeople[i].sides.name + " - " + this.resultpeople[i].name + " จังหวัด" +  this.resultpeople[i].userProvince[0].province.name })
          }
        }
      // }
      var data: any[] = this.peopleuserdata.map(result => {
        return result.user.id
      })
      this.PeopleSelect = _.filter(this.selectdatapeople, (v) => !_.includes(
        data, v.value
      ))
      this.getProvincialDepartmentPeople();
    })

  }
  // async getDepartmentPeople() {
  //   this.selectdatadepartmentpeople = []
  //   await this.userservice.getuserdata(10).subscribe(async result => {
  //     this.resultdepartmentpeople = result // All
  //     if (this.role_id == 3) {
  //       for (var i = 0; i < this.resultdepartmentpeople.length; i++) {

  //         var userregion = "";
  //         for (var j = 0; j < this.resultdepartmentpeople[i].userRegion.length; j++) {

  //           if(this.resultdepartmentpeople[i].userRegion[j].region.name == "เขตตรวจราชส่วนกลาง"){
  //             this.resultdepartmentpeople[i].userRegion[j].region.name = "ส่วนกลาง"
  //           } else {
  //             this.resultdepartmentpeople[i].userRegion[j].region.name = this.resultdepartmentpeople[i].userRegion[j].region.name.replace('เขตตรวจราชการที่','');
  //           }

  //           if (j == (this.resultdepartmentpeople[i].userRegion.length - 1)) {
  //             userregion += this.resultdepartmentpeople[i].userRegion[j].region.name
  //           } else {
  //             userregion += this.resultdepartmentpeople[i].userRegion[j].region.name + ", "
  //           }
  //         }

  //         await this.selectdatadepartmentpeople.push({ value: this.resultdepartmentpeople[i].id, label: this.resultdepartmentpeople[i].ministries.name + " - " + this.resultdepartmentpeople[i].name + " เขต " + userregion})
  //       }
  //     } else {
  //       for (var i = 0; i < this.resultdepartmentpeople.length; i++) {
  //         var checked = _.filter(this.resultdepartmentpeople[i].userProvince, (v) => _.includes(this.userProvince.map(result => { return result.provinceId }), v.provinceId)).length
  //         if (checked > 0) {
  //           if (this.ministryId == this.resultdepartmentpeople[i].ministryId) {

  //             var userregion = "";
  //             for (var j = 0; j < this.resultdepartmentpeople[i].userRegion.length; j++) {
  //               if (j == (this.resultdepartmentpeople[i].userRegion.length - 1)) {
  //                 userregion += this.resultdepartmentpeople[i].userRegion[j].region.name
  //               } else {
  //                 userregion += this.resultdepartmentpeople[i].userRegion[j].region.name + ", "
  //               }
  //             }

  //             await this.selectdatadepartmentpeople.push({ value: this.resultdepartmentpeople[i].id, label: this.resultdepartmentpeople[i].ministries.name + " - " + this.resultdepartmentpeople[i].name + " เขต " + userregion})
  //           }
  //         }
  //       }
  //     }
  //     var data: any[] = this.departmentuserdata.map(result => {
  //       return result.user.id
  //     })
  //     this.DepartmentSelect = _.filter(this.selectdatadepartmentpeople, (v) => !_.includes(
  //       data, v.value
  //     ))
  //     this.getUserPeople();
  //   })


  // }
  async getProvincialDepartmentPeople() {
    this.selectdataprovincialdepartmentpeople = []
    await this.userservice.getuserdata(9).subscribe(async result => {
      this.resultprovincialdepartmentpeople = result
      console.log("tttt:", this.resultprovincialdepartmentpeople);
      // if (this.role_id == 3) {
      //   for (var i = 0; i < this.resultprovincialdepartmentpeople.length; i++) {
      //     await this.selectdataprovincialdepartmentpeople.push({ value: this.resultprovincialdepartmentpeople[i].id, label: this.resultprovincialdepartmentpeople[i].provincialDepartments.name + " - " + this.resultprovincialdepartmentpeople[i].name })
      //   }
      // } else {
        for (var i = 0; i < this.resultprovincialdepartmentpeople.length; i++) {
          var checked = _.filter(this.resultprovincialdepartmentpeople[i].userProvince, (v) => _.includes(this.userProvince.map(result => { return result.provinceId }), v.provinceId)).length
          if (checked > 0) {
            await this.selectdataprovincialdepartmentpeople.push({ value: this.resultprovincialdepartmentpeople[i].id, label: this.resultprovincialdepartmentpeople[i].provincialDepartments.name + " - " + this.resultprovincialdepartmentpeople[i].name })
          }
        }
      // }
      console.log("this.provincialdepartmentuserdata", this.provincialdepartmentuserdata);

      var data: any[] = this.provincialdepartmentuserdata.map(result => {
        return result.user.id
      })
      this.ProvincialDepartmentSelect = _.filter(this.selectdataprovincialdepartmentpeople, (v) => !_.includes(
        data, v.value
      ))
      this.loading = true;
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

      let UserMinistryId: any[] = value.UserMinistryId
      let UserDepartmentId: any[] = value.UserDepartmentId
      let UserProvincialDepartmentId: any[] = value.UserProvincialDepartmentId
      let UserPeopleId: any[] = value.UserPeopleId

      await this.inspectionplanservice.getcentralpolicyprovinceid(this.data[j].centralPolicyId, this.data[j].inspectionPlanEvent.provinceId).subscribe(result => {

        this.centralpolicyservice.addCentralpolicyUser(value, result, this.userid, this.id).subscribe(response => {
          console.log(value);

          if (UserMinistryId != null) {
            if (this.timelineData.status == "ใช้งานจริง") {
              for (let i = 0; i < UserMinistryId.length; i++) {
                this.notificationService.addNotification(this.data[j].centralPolicyId, this.provinceid, UserMinistryId[i], 1, this.id, null,this.userid)
                  .subscribe(response => {
                    console.log(response);
                  })
              }
            }
          }
          if (UserDepartmentId != null) {
            if (this.timelineData.status == "ใช้งานจริง") {
              for (let i = 0; i < UserDepartmentId.length; i++) {
                this.notificationService.addNotification(this.data[j].centralPolicyId, this.provinceid, UserDepartmentId[i], 1, this.id, null,this.userid)
                  .subscribe(response => {
                    console.log(response);
                  })
              }
            }
          }
          if (UserProvincialDepartmentId != null) {
            if (this.timelineData.status == "ใช้งานจริง") {
              for (let i = 0; i < UserProvincialDepartmentId.length; i++) {
                this.notificationService.addNotification(this.data[j].centralPolicyId, this.provinceid, UserProvincialDepartmentId[i], 1, this.id, null,this.userid)
                  .subscribe(response => {
                    console.log(response);
                  })
              }
            }
          }

          if (UserPeopleId != null) {
            if (this.timelineData.status == "ใช้งานจริง") {
              for (let i = 0; i < UserPeopleId.length; i++) {
                this.notificationService.addNotification(this.data[j].centralPolicyId, this.provinceid, UserPeopleId[i], 1, this.id, null,this.userid)
                  .subscribe(response => {
                    console.log(response);
                  })
              }
            }
          }
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
    this._NotofyService.onSuccess("เพื่มข้อมูล",)
  }

  changeplanstatus(value) {
    // alert(this.timelineData.id)

    this.inspectionplanservice.changeplanstatus(this.timelineData.id).subscribe(response => {
      this._NotofyService.onSuccess("เปลี่ยนสถานะ",)
      // console.log(value);
      // this.Form.reset()
      this.modalRef.hide()
      // location.reload();
      this.getTimeline();

      this.notificationService.addNotification(1, this.provinceid, 1, 16, this.id, null,this.userid)
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
    if (this.endDateTime.getHours() < 10 && this.endDateTime.getMinutes() < 10) {
      this.endDateTime = "0" + this.endDateTime.getHours() + ":0" + this.endDateTime.getMinutes() + ":00"
    } else if (this.endDateTime.getHours() < 10 && this.endDateTime.getMinutes() >= 10) {
      this.endDateTime = "0" + this.endDateTime.getHours() + ":" + this.endDateTime.getMinutes() + ":00"
    } else if (this.endDateTime.getHours() >= 10 && this.endDateTime.getMinutes() < 10) {
      this.endDateTime = this.endDateTime.getHours() + ":0" + this.endDateTime.getMinutes() + ":00" 
    } else if (this.endDateTime.getHours() >= 10 && this.endDateTime.getMinutes() >= 10) {
      this.endDateTime = this.endDateTime.getHours() + ":" + this.endDateTime.getMinutes() + ":00"
    }

    if (this.startDateTime.getHours() < 10 && this.startDateTime.getMinutes() < 10) {
      this.startDateTime = "0" + this.startDateTime.getHours() + ":0" + this.startDateTime.getMinutes() + ":00"
    } else if (this.startDateTime.getHours() < 10 && this.startDateTime.getMinutes() >= 10) {
      this.startDateTime = "0" + this.startDateTime.getHours() + ":" + this.startDateTime.getMinutes() + ":00"
    } else if (this.startDateTime.getHours() >= 10 && this.startDateTime.getMinutes() < 10) {
      this.startDateTime = this.startDateTime.getHours() + ":0" + this.startDateTime.getMinutes() + ":00"
    } else if (this.startDateTime.getHours() >= 10 && this.startDateTime.getMinutes() >= 10) {
      this.startDateTime = this.startDateTime.getHours() + ":" + this.startDateTime.getMinutes() + ":00"
    }
    // alert(JSON.stringify(this.startDate))
    this.inspectionplanservice.editplandate(this.id, this.startDate, this.endDate,this.userid,1,1).subscribe(response => {
      this.modalRef.hide()
      this.getTimeline();
    })
  }
  EditCentralPolicy(value) {
    console.log(value);
    this.inspectionplanservice.editcentralpolicy(this.editid, this.startDate2, this.endDate2, value, this.userid).subscribe(response => {
      this._NotofyService.onSuccess("แก้ไขข้อมูล",)
      this.modalRef.hide()
      this.getinspectionplanservice();
    })
  }
  deleteDate() {
    this.inspectionplanservice.deleteplandate(this.id,this.userid).subscribe(response => {
      this._NotofyService.onSuccess("ลบข้อมูล",)
      this.modalRef.hide()
      this.router.navigate(['inspectionplanevent'])
    })
  }

  DeleteCentralPolicyEvent(value) {
    // alert(value)
    this.inspectionplanservice.deleteCentralPolicyEvent(value, this.userid,this.id).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      // location.reload();
      this.loading = false

      this.getinspectionplanservice();

    })
  }
  DeleteCentralPolicy(value) {
    this.inspectionplanservice.deleteCentralPolicy(value, this.userid).subscribe(response => {
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

    // this.FormOther.patchValue({
    //   ProvinceId: this.provinceid,
    // })
    this.submitted = true;
    if (this.FormOther.invalid) {
      console.log("this.FormOther", this.FormOther);
      return;
    } else {
      console.log("FORM: ", value);
      this.inspectionplanservice.addInspectionPlan(value, this.userid, this.id, this.provinceid, this.startDate, this.endDate, this.year).subscribe(response => {
        this._NotofyService.onSuccess("เพื่มข้อมูล",)
        console.log("create Inspection plan: ", value);
        // this.Form.reset()
        // window.history.back();

        this.FormOther.reset()
        this.modalRef.hide()

        this.FormOther.patchValue({
          ProvinceId: this.provinceid,
        })

        this.loading = false;
        this.data = [];
        this.getinspectionplanservice()
      })
    }
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
  starttime(event) {
    console.log('Time changed', event);
    this.startDateTime = event
  }
  endtime(event) {
    console.log('Time changed', event);
    this.endDateTime = event
  }
}
