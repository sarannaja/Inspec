import { Component, Inject, OnInit, TemplateRef } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { SubjectService } from '../services/subject.service';
import { CentralpolicyService } from '../services/centralpolicy.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';

import { UserService } from '../services/user.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { InspectionplanService } from '../services/inspectionplan.service';
import { Router } from '@angular/router';
import { NotofyService } from '../services/notofy.service';
import { IMyOptions } from 'mydatepicker-th';
import { ReportService } from '../services/report.service';
import { ExportReportService } from '../services/export-report.service';
import { FiscalyearService } from '../services/fiscalyear.service';

@Component({
  selector: 'app-subjectevent',
  templateUrl: './subjectevent.component.html',
  styleUrls: ['./subjectevent.component.css']
})
export class SubjecteventComponent implements OnInit {
  myDatePickerOptions: IMyOptions = {
    // other options...

    dateFormat: 'dd/mm/yyyy',
    // dateFormat: 'dd/mmm/yyyy', เดือนเป็นไทย
  };
  dtOptions: DataTables.Settings = {};
  loading = false;
  modalRef: BsModalRef;
  selectcentralpolicy: any = [];
  resultcentralpolicy: any[] = [];
  resultcentralpolicy2: any = [];
  resultsubjectevent: any[] = [];
  Form: FormGroup;
  Form2: FormGroup;
  Form3: FormGroup;
  checkInspec: boolean;
  checkType: any = "ลงพื้นที่";
  selectdataprovince: Array<any>
  userid: string;
  resultprovince: any = [];
  province: any[] = []
  provincename: any[] = []
  provinceid: any[] = []
  selectdatacentralpolicy: Array<any>

  selectdataprovince2: Array<any>
  selectdatacentralpolicy2: Array<any>
  province2: any[] = []
  provincename2: any[] = []
  provinceid2: any[] = []

  CentralPolicyEvents: Array<any> = []
  subjectgroupsdatas: Array<any> = []
  checkOther: Boolean;
  submitted = false;
  // ceneventid
  submittedtime = false;
  submittedradio = false;
  downloadUrl: any
  checkTypeReport: any
  selectreporttype: Array<any>
  selectprovincialDepartment: any = [];
  //reporttype1
  subjectgroupidtype1: any
  centralPolicyIdtype1: any
  provinceIdtype1: any
  centralpolicyprovinceidtype1: any
  selectdatacentralpolicytype1: Array<any>
  //reporttype2
  FormReporttype2: FormGroup;
  checkTypeRepor2: any
  subjectgroupidtype2: any
  centralPolicyIdtype2: any
  provinceIdtype2: any
  centralpolicyprovinceidtype2: any
  selectdatacentralpolicytype2: Array<any>
  fiscalYearData: any = [];
  fiscalYearId: any
  regionId: any
  regionData: any = [];
  //reporttype3
  subjectgroupidtype3: any
  //reporttype4
  checkTypeRepor4: any
  FormReporttype4: FormGroup;
  subjectgroupidtype4: any
  centralPolicyIdtype4: any
  provinceIdtype4: any
  centralpolicyprovinceidtype4: any
  answerRecommenDationInspectors: any = []
  selectdatacentralpolicytype4: Array<any>
  //reporttype5
  subjectgroupidtype5: any
  centralPolicyIdtype5: any
  provinceIdtype5: any
  centralpolicyprovinceidtype5: any
  delid: any
  land
  class
  //reporttype6
  FormReporttype6: FormGroup;
  checkTypeRepor6: any
  subjectgroupidtype6: any
  centralPolicyIdtype6: any
  provinceIdtype6: any
  questionpeople: any = [];
  answerpeople: any = []
  centralpolicyprovinceidtype6: any
  selectpeopletype6 = []
  selectdatacentralpolicytype6: Array<any>

  constructor(
    private spinner: NgxSpinnerService,
    private modalService: BsModalService,
    private subjectservice: SubjectService,
    private centralpolicyservice: CentralpolicyService,
    private userservice: UserService,
    private authorize: AuthorizeService,
    private fb: FormBuilder,
    private inspectionplanservice: InspectionplanService,
    private router: Router,
    private _NotofyService: NotofyService,
    private reportservice: ReportService,
    private exportReportService: ExportReportService,
    private fiscalyearService: FiscalyearService,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.downloadUrl = baseUrl + '/Uploads';
  }

  ngOnInit() {
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
        // alert(this.userid)
      })
    // this.spinner.show();
    this.dtOptions = {
      pagingType: 'full_numbers',
      ordering: true,
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

    this.Form = this.fb.group({
      "CentralpolicyId": new FormControl(null, [Validators.required]),
      "startdate": new FormControl(null),
      "enddate": new FormControl(null),
      "province": new FormControl(null, [Validators.required]),
      "land": new FormControl(null),
      // "centralPolicyOther": new FormControl(null, [Validators.required])
    })

    this.Form2 = this.fb.group({
      "CentralpolicyId2": new FormControl(null, [Validators.required]),
      "province2": new FormControl(null, [Validators.required]),
    })

    this.Form3 = this.fb.group({
      "centralPolicyOther": new FormControl(null, [Validators.required]),
      "province": new FormControl(null, [Validators.required]),
      "land": new FormControl(null),
      "startdate": new FormControl(null),
      "enddate": new FormControl(null),
    })

    this.FormReporttype2 = this.fb.group({
      type: new FormControl(null, [Validators.required]),
      provincialDepartmentId: new FormControl(null, [Validators.required]),
      SubjectGroupId: new FormControl(null, [Validators.required])
    })
    this.FormReporttype4 = this.fb.group({
      type: new FormControl(null, [Validators.required]),
      provincialDepartmentId: new FormControl(null, [Validators.required]),
      SubjectGroupId: new FormControl(null, [Validators.required]),
    })
    this.FormReporttype6 = this.fb.group({
      type: new FormControl(null, [Validators.required]),
      userid: new FormControl(null, [Validators.required])
    })
    // this.getCentralPolicy();
    this.getSubjectevent();


    this.userservice.getprovincedata(this.userid).subscribe(result => {
      this.resultprovince = result

      this.selectdataprovince = this.resultprovince.map((item, index) => {
        return { value: item.province.id, label: item.province.name }
      })

      console.log(this.resultprovince);
    })
    this.selectreporttype = [
      { label: "รายงานแบบประเด็นการตรวจติดตาม", value: "1" },
      { label: "รายงานผลการดำเนินการของหน่วยรับตรวจ", value: "2" },
      { label: "รายงานแบบข้อเสนอแนะของผู้ตรวจราชการ", value: "3" },
      { label: "รายงานผลการดำเนินการตามข้อเสนอแนะของผู้ตรวจราชการ", value: "4" },
      { label: "รายงานแบบสอบถามความคิดเห็นของที่ปรึกษาผู้ตรวจราชการภาคประชาชน", value: "5" },
      { label: "รายงานความคิดเห็นของที่ปรึกษาผู้ตรวจราชการภาคประชาชน", value: "6" }
    ]
  }
  openModal(template: TemplateRef<any>, id, land, classcen) {
    this.class = classcen;
    this.land = land;
    this.delid = id;

    this.submitted = false;
    this.submittedtime = false;
    this.submittedradio = false;

    this.checkInspec = null;
    this.checkType = 0;
    this.modalRef = this.modalService.show(template);
  }
  reportModalReport(template: TemplateRef<any>) {
    this.checkTypeReport = 0;
    this.modalRef = this.modalService.show(template);
  }
  getCentralPolicy() {
    this.centralpolicyservice.getcentralpolicydata()
      .subscribe(result => {
        this.resultcentralpolicy = result
        this.selectcentralpolicy = this.resultcentralpolicy.map((item, index) => {
          return { value: item.id, label: item.title }
        })
      })
  }

  getSubjectevent() {
    this.subjectservice.getsubjectevent(this.userid).subscribe(result => {
      this.resultsubjectevent = result
      this.selectdatacentralpolicytype1 = this.resultsubjectevent.map((item, index) => {
        return { value: item.id, label: item.centralPolicy.title }
      })
      this.loading = true;
    })
  }

  selectedprovince(event, i, item) {
    this.province[i] = event;
    this.provincename[i] = event.label;
    this.provinceid[i] = event.value;

    // alert(JSON.stringify(event));
    // console.log("item", item);
    // this.t.at(i).get('resultcentralpolicy').patchValue([1, 2])

    this.centralpolicyservice.getcentralpolicyfromprovince(event.value)
      .subscribe(result => {
        this.resultcentralpolicy = result
        // alert(JSON.stringify(this.resultcentralpolicy))

        this.selectdatacentralpolicy = this.resultcentralpolicy.filter((item, index) => {
          return item.centralPolicy.status == "ใช้งานจริง"
        }).map((item, index) => {
          return { value: item.centralPolicy.id, label: item.centralPolicy.title }
        })
        // this.t.at(i).get('resultcentralpolicy').patchValue(this.selectdatacentralpolicy)
        // console.log("t", this.t.value);
      })
  }


  selectedprovince2(event, i, item) {
    // alert("123")
    // this.ceneventid = 0
    this.province2[i] = event;
    this.provincename2[i] = event.label;
    this.provinceid2[i] = event.value;

    this.subjectservice.geteventfromcalendar(event.value)
      .subscribe(result => {

        // alert(JSON.stringify(result.centralPolicyEvents))
        this.CentralPolicyEvents = result.centralPolicyEvents
        this.subjectgroupsdatas = result.subjectgroupsdatas

        this.selectdatacentralpolicy2 = this.CentralPolicyEvents.filter((item, index) => {
          return item.haveSubject == 0
        }).map((item, index) => {
          var start_date = (this.time(item.startDate).day) + "/" + (this.time(item.startDate).month) + "/" + (this.time(item.startDate).year + 543)
          var end_date = (this.time(item.endDate).day) + "/" + (this.time(item.endDate).month) + "/" + (this.time(item.endDate).year + 543)
          // alert(start_date)
          return { value: { centralPolicyId: item.centralPolicy.id, centralPolicyeventId: item.id }, label: item.centralPolicy.title + " จังหวัด" + item.inspectionPlanEvent.province.name + " (" + start_date + " - " + end_date + ")" }
        })
        // alert(JSON.stringify(this.selectdatacentralpolicy2))
        // this.getRecycled()
      })
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
    // alert(JSON.stringify(new_date))
    return new_date
  }

  getRecycled() {
    // alert(JSON.stringify(this.CentralPolicyEvents))
    this.selectdatacentralpolicy2 = []

    // if (this.subjectgroupsdatas.length == 0) {

    //   for (var i = 0; i < this.CentralPolicyEvents.length; i++) {
    //     // alert(JSON.stringify(this.CentralPolicyEvents[i].centralPolicy.title))
    //     this.selectdatacentralpolicy2.push({ value: this.CentralPolicyEvents[i].centralPolicy.id, label: this.CentralPolicyEvents[i].centralPolicy.title })
    //   }
    // }

    // else {
    for (var i = 0; i < this.CentralPolicyEvents.length; i++) {
      var n = 0;
      for (var ii = 0; ii < this.subjectgroupsdatas.length; ii++) {
        if (this.CentralPolicyEvents[i].haveSubject == 1) {
          // alert("1")
          n++;
        }
      }
      if (n == 0) {
        // alert("push")
        // alert(JSON.stringify(this.CentralPolicyEvents[i].centralPolicy.title))
        this.selectdatacentralpolicy2.push({ value: this.CentralPolicyEvents[i].centralPolicy.id, label: this.CentralPolicyEvents[i].centralPolicy.title })
      }
    }
    // }
  }
  storeCentralPolicy(value) {

    // alert(JSON.stringify(this.Form.value.startdate))
    // alert(JSON.stringify(this.Form.value.enddate))

    // alert(JSON.stringify(this.Form.invalid))
    // alert(this.Form.value.startdate)
    console.log('storeCentralPolicy', value, this.userid);

    this.submitted = true;

    if (this.Form.value.province == null || this.Form.value.CentralpolicyId == null) {
      if (value.land == null) {
        this.submittedradio = true;
      }
      // return;
    } else {
      // alert("else1")
      if (value.land == null) {
        this.submittedradio = true;
      }
      this.submitted = false;
      if (value.land == "ลงพื้นที่") {
        // alert("else2")
        // alert(value.land)
        if (this.Form.value.startdate != null || this.Form.value.enddate != null) {
          // alert(this.Form.value.startdate)
          // alert("if")
          this.subjectservice.subjectevent(value, this.userid)
            .subscribe(result => {
              this._NotofyService.onSuccess("เพื่มข้อมูล",)
              this.loading = false;
              this.modalRef.hide();
              this.Form.reset()
              // this.resultcentralpolicy = result
              this.getSubjectevent();
            })
        } else {
          // alert("else3")
          // alert("else")
          this.submittedtime = true;
          // return;
        }
      } else if (value.land == "ไม่ลงพื้นที่") {
        this.subjectservice.subjecteventnoland(value, this.userid)
          .subscribe(result => {
            this._NotofyService.onSuccess("เพื่มข้อมูล",)
            this.loading = false;
            this.modalRef.hide();
            this.Form.reset()
            // this.resultcentralpolicy = result
            this.getSubjectevent();
          })
      }
    }
  }
  storeCentralPolicy3(value) {

    console.log('storeCentralPolicy', value, this.userid);

    this.submitted = true;

    if (this.Form3.value.province == null || this.Form3.value.centralPolicyOther == null) {
      if (value.land == null) {
        this.submittedradio = true;
      }
    } else {
      if (value.land == null) {
        this.submittedradio = true;
      }
      this.submitted = false;
      if (value.land == "ลงพื้นที่") {
        if (this.Form3.value.startdate != null || this.Form3.value.enddate != null) {
          console.log('this.Form3.value.startdate', this.Form3.value.startdate, this.Form3.value.enddate);

          this.subjectservice.subjecteventnolandOtherland(value, this.userid)
            .subscribe(result => {
              this._NotofyService.onSuccess("เพื่มข้อมูล",)
              this.loading = false;
              this.modalRef.hide();
              this.Form3.reset();
              this.getSubjectevent();
            })
        } else {
          this.submittedtime = true;
        }
      } else if (value.land == "ไม่ลงพื้นที่") {
        this.subjectservice.subjecteventnolandOther(value, this.userid)
          .subscribe(result => {
            this._NotofyService.onSuccess("เพื่มข้อมูล",)
            this.loading = false;
            this.modalRef.hide();
            this.Form3.reset();
            this.getSubjectevent();
          })
      }

    }
  }
  storeCentralPolicy2(value) {
    this.submitted = true;
    if (this.Form2.invalid) {
      console.log("in1");
      return;
    } else {
      this.submitted = false;
      // alert(JSON.stringify(value))
      this.subjectservice.postsubjecteventfromcalendar(value, this.userid)
        .subscribe(result => {
          this._NotofyService.onSuccess("เพื่มข้อมูล",)
          this.loading = false;
          this.modalRef.hide();
          this.Form2.reset()
          // this.resultcentralpolicy = result
          this.getSubjectevent();
        })
    }
  }



  inspect(myradio) {
    // alert(myradio)
    this.checkInspec = true;
  }
  notInspec(value) {
    // alert(value)
    this.checkInspec = false;
  }

  type(myradio) {
    // alert(myradio)
    this.checkType = 1;
  }
  notType(value) {
    // alert(value)
    this.checkType = 2;
  }
  other() {
    this.checkType = 3;
  }
  startdate_enddate(event) {

    // alert(JSON.stringify(event))

    this.Form.patchValue({
      startdate: event.start_date,
      enddate: event.end_date,
    })
    console.log(this.Form.value);

  }

  startdate_enddate3(event) {
    this.Form3.patchValue({
      startdate: event.start_date,
      enddate: event.end_date,
    })
    console.log(this.Form3.value);
  }

  Subjectevent(id, centralPolicyId, provinceId) {
    this.inspectionplanservice.getcentralpolicyprovinceid(centralPolicyId, provinceId).subscribe(result => {
      // this.centralpolicyprovinceid = result
      this.router.navigate(['/subjectevent/detail/' + result, { subjectgroupid: id, }])
    })
  }
  ////reportsrart////
  reporttype(event) {
    console.log(event.value);
    this.checkTypeReport = event.value;
    this.checkTypeRepor2 = 0;
    this.checkTypeRepor6 = 0;
  }
  ////reporttype1start////
  rssj(id): any {
    return this.resultsubjectevent.find(result => result.id == id)
  }
  select1(event) {
    this.subjectgroupidtype1 = this.rssj(event.value).id,
      this.centralPolicyIdtype1 = this.rssj(event.value).centralPolicyId,
      this.provinceIdtype1 = this.rssj(event.value).provinceId
    this.inspectionplanservice.getcentralpolicyprovinceid(this.centralPolicyIdtype1, this.provinceIdtype1).subscribe(result => {
      this.centralpolicyprovinceidtype1 = result
    })
  }
  storeReport() {
    var resultdetailcentralpolicy: any = []
    var resultdetailcentralpolicyprovince: any = []
    this.centralpolicyservice.getdetailcentralpolicyprovincedata(this.centralpolicyprovinceidtype1)
      .subscribe(result => {
        console.log("123", result);
        resultdetailcentralpolicy = result.centralpolicydata
        this.centralpolicyservice.getSubjecteventdetaildata(this.centralpolicyprovinceidtype1, this.subjectgroupidtype1)
          .subscribe(result => {
            resultdetailcentralpolicyprovince = result.subjectcentralpolicyprovincedata
            this.reportservice.createReportSubject(resultdetailcentralpolicy, resultdetailcentralpolicyprovince).subscribe(result => {
              console.log("export: ", result);
              window.open(this.downloadUrl + "/" + result.data);
              // this.modalRef.hide();
            })

          })

      })
  }
  ////reporttype1end/////
  ////reporttype2start/////
  reporttyp21(value) {
    // alert(myradio)
    this.checkTypeRepor2 = 1;
    this.FormReporttype2.patchValue({
      type: this.checkTypeRepor2,
    })
  }
  reporttyp22(value) {
    // alert(myradio)
    this.checkTypeRepor2 = 2;
    this.FormReporttype2.patchValue({
      type: this.checkTypeRepor2,
    })
  }
  reporttyp23(value) {
    // alert(myradio)
    this.checkTypeRepor2 = 3;
    this.FormReporttype2.patchValue({
      type: this.checkTypeRepor2,
    })
    this.exportReportService.getImportReportFiscalYears().subscribe(res => {
      console.log("fiscalYear1: ", res);
      this.fiscalYearData = res.importFiscalYear.map((item, index) => {
        return {
          value: item.id,
          label: item.year
        }
      })
      console.log("fiscalYear: ", this.fiscalYearData);
    })
  }
  select21(event) {
    var resultdetailcentralpolicyprovince: any = []
    var provincialDepartments: any[] = [];
    this.subjectgroupidtype2 = this.rssj(event.value).id,
      this.centralPolicyIdtype2 = this.rssj(event.value).centralPolicyId,
      this.provinceIdtype2 = this.rssj(event.value).provinceId
    this.FormReporttype2.patchValue({
      SubjectGroupId: this.subjectgroupidtype2,
    })
    this.inspectionplanservice.getcentralpolicyprovinceid(this.centralPolicyIdtype2, this.provinceIdtype2).subscribe(result => {
      this.centralpolicyprovinceidtype2 = result
      this.centralpolicyservice.getSubjecteventdetaildata(this.centralpolicyprovinceidtype2, this.subjectgroupidtype2)
        .subscribe(result => {
          resultdetailcentralpolicyprovince = result.subjectcentralpolicyprovincedata
          console.log(resultdetailcentralpolicyprovince);
          resultdetailcentralpolicyprovince.forEach(element => {
            element.subquestionCentralPolicyProvinces.forEach(element2 => {
              element2.subjectCentralPolicyProvinceGroups.forEach(element3 => {
                provincialDepartments.push(element3)
              })
            });
          });
          this.selectprovincialDepartment = provincialDepartments.map((item, index) => {
            return {
              value: item.provincialDepartment.id,
              label: item.provincialDepartment.name,
            }
          })
          console.log("selectprovincialDepartment: ", this.selectprovincialDepartment);
          this.selectprovincialDepartment = this.selectprovincialDepartment.filter(
            (thing, i, arr) => arr.findIndex(t => t.value === thing.value) === i
          );
          console.log("uniqueProvincialDepartment: ", this.selectprovincialDepartment);
        })
    })
  }
  select22province(event) {
    console.log(event);
    this.subjectservice.getsubjecteventprovince(this.userid, event.value).subscribe(result => {
      console.log(result);
      this.selectdatacentralpolicytype2 = result.map((item, index) => {
        return { value: item.id, label: item.centralPolicy.title }
      })
    })
  }
  select22(event) {
    this.subjectgroupidtype2 = this.rssj(event.value).id,
      this.centralPolicyIdtype2 = this.rssj(event.value).centralPolicyId,
      this.provinceIdtype2 = this.rssj(event.value).provinceId
    this.FormReporttype2.patchValue({
      SubjectGroupId: this.subjectgroupidtype2,
    })
    this.inspectionplanservice.getcentralpolicyprovinceid(this.centralPolicyIdtype2, this.provinceIdtype2).subscribe(result => {
      this.centralpolicyprovinceidtype2 = result
    })
  }
  select23fiscalYear(event) {
    this.fiscalYearId = event.value;
    this.fiscalyearService.getreportfiscalyearrelations(this.fiscalYearId, this.userid).subscribe(res => {
      console.log("fiscalYearRelations: ", res);

      var uniqueRegion: any = [];
      uniqueRegion = res.termsList.filter(
        (thing, i, arr) => arr.findIndex(t => t.regionId === thing.regionId) === i
      );
      console.log("uniqueRegions: ", uniqueRegion);

      this.regionData = uniqueRegion.map((item, index) => {
        return {
          value: item.region.id,
          label: item.region.name
        }
      })
    })
  }
  select23region(event) {
    this.regionId = event.value;
  }
  storeReportPerformance(value) {
    console.log(value);
    this.reportservice.createReporttype1(value).subscribe(result => {
      this.FormReporttype2.reset();
      this.modalRef.hide();
      window.open(this.downloadUrl + "/" + result.data);
    })
  }
  storeReportPerformance2() {
    this.reportservice.createReporttype2(this.FormReporttype2.value, this.provinceIdtype2, this.centralpolicyprovinceidtype2).subscribe(result => {
      this.FormReporttype2.reset();
      this.modalRef.hide();
      window.open(this.downloadUrl + "/" + result.data);
    })
  }
  storeReportPerformance3() {
    this.reportservice.createReporttype3(this.FormReporttype2.value, this.regionId, this.userid).subscribe(result => {
      this.FormReporttype2.reset();
      this.modalRef.hide();
      window.open(this.downloadUrl + "/" + result.data);
    })
  }
  ////reporttype2end/////
  ////reporttype3start/////
  select3(event) {
    this.subjectgroupidtype3 = this.rssj(event.value).id
  }
  storeReportsuggestions() {
    this.reportservice.createReportsuggestions(this.subjectgroupidtype3).subscribe(result => {
      window.open(this.downloadUrl + "/" + result.data);
    })
  }
  ////reporttype3end/////
  ////reporttype4start/////
  reporttyp41(value) {
    // alert(myradio)
    this.checkTypeRepor4 = 1;
    this.FormReporttype4.patchValue({
      type: this.checkTypeRepor4,
    })
  }
  reporttyp42(value) {
    // alert(myradio)
    this.checkTypeRepor4 = 2;
    this.FormReporttype4.patchValue({
      type: this.checkTypeRepor4,
    })
  }
  reporttyp43(value) {
    // alert(myradio)
    this.checkTypeRepor4 = 3;
    this.FormReporttype4.patchValue({
      type: this.checkTypeRepor4,
    })
    this.exportReportService.getImportReportFiscalYears().subscribe(res => {
      console.log("fiscalYear1: ", res);
      this.fiscalYearData = res.importFiscalYear.map((item, index) => {
        return {
          value: item.id,
          label: item.year
        }
      })
      console.log("fiscalYear: ", this.fiscalYearData);
    })
  }
  select41(event) {
    var subjectgroup: any = []
    this.subjectgroupidtype4 = this.rssj(event.value).id,
      this.centralPolicyIdtype4 = this.rssj(event.value).centralPolicyId,
      this.provinceIdtype4 = this.rssj(event.value).provinceId
    this.FormReporttype4.patchValue({
      SubjectGroupId: this.subjectgroupidtype4
    })
    this.inspectionplanservice.getcentralpolicyprovinceid(this.centralPolicyIdtype4, this.provinceIdtype4).subscribe(result => {
      this.centralpolicyprovinceidtype4 = result
      this.centralpolicyservice.getSubjecteventdetaildata(this.centralpolicyprovinceidtype4, this.subjectgroupidtype4)
        .subscribe(result => {
          subjectgroup = result.subjectgroup
          var test: any = [];
          subjectgroup.answerRecommenDationInspectors.forEach(element => {
            test.push(element.user.provincialDepartments)
          });
          this.answerRecommenDationInspectors = test.filter(
            (thing, i, arr) => arr.findIndex(t => t === thing) === i
          );
          console.log("uniqueanswerRecommenDationInspectors: ", this.answerRecommenDationInspectors);
          this.selectprovincialDepartment = this.answerRecommenDationInspectors.map((item, index) => {
            return {
              value: item.id,
              label: item.name,
            }
          })
          console.log("1111", this.selectprovincialDepartment);
        })
    })
  }
  select42province(event) {
    console.log(event);
    this.subjectservice.getsubjecteventprovince(this.userid, event.value).subscribe(result => {
      console.log(result);
      this.selectdatacentralpolicytype4 = result.map((item, index) => {
        return { value: item.id, label: item.centralPolicy.title }
      })
    })
  }
  select42(event) {
    this.subjectgroupidtype4 = this.rssj(event.value).id,
      this.centralPolicyIdtype4 = this.rssj(event.value).centralPolicyId,
      this.provinceIdtype4 = this.rssj(event.value).provinceId
    this.FormReporttype4.patchValue({
      SubjectGroupId: this.subjectgroupidtype4
    })

  }
  select43fiscalYear(event) {
    this.fiscalYearId = event.value;
    this.fiscalyearService.getreportfiscalyearrelations(this.fiscalYearId, this.userid).subscribe(res => {
      console.log("fiscalYearRelations: ", res);

      var uniqueRegion: any = [];
      uniqueRegion = res.termsList.filter(
        (thing, i, arr) => arr.findIndex(t => t.regionId === thing.regionId) === i
      );
      console.log("uniqueRegions: ", uniqueRegion);

      this.regionData = uniqueRegion.map((item, index) => {
        return {
          value: item.region.id,
          label: item.region.name
        }
      })
    })
  }
  select43region(event) {
    this.regionId = event.value;
  }
  storeReportSuggestionsResulttype1(value) {
    this.reportservice.createReportSuggestionsResulttype1(value, this.provinceIdtype4).subscribe(result => {
      window.open(this.downloadUrl + "/" + result.data);
    })
  }
  storeReportSuggestionsResulttype2(value) {
    console.log(value);

    this.reportservice.createReportSuggestionsResulttype2(value, this.provinceIdtype4).subscribe(result => {
      window.open(this.downloadUrl + "/" + result.data);
    })
  }
  storeReportSuggestionsResulttype3(value) {
    this.reportservice.createReportSuggestionsResulttype3(value, this.regionId, this.userid).subscribe(result => {
      window.open(this.downloadUrl + "/" + result.data);
    })
  }
  ////reporttype4end/////
  ////reporttype5start/////
  select5(event) {
    this.subjectgroupidtype5 = this.rssj(event.value).id,
      this.centralPolicyIdtype5 = this.rssj(event.value).centralPolicyId,
      this.provinceIdtype5 = this.rssj(event.value).provinceId
    this.inspectionplanservice.getcentralpolicyprovinceid(this.centralPolicyIdtype5, this.provinceIdtype5).subscribe(result => {
      this.centralpolicyprovinceidtype5 = result
    })
  }
  storeReportQuestionnaire() {
    var subjectgroup: any = []
    this.centralpolicyservice.getSubjecteventdetaildata(this.centralpolicyprovinceidtype5, this.subjectgroupidtype5)
      .subscribe(result => {
        subjectgroup = result.subjectgroup
        if (subjectgroup.subjectGroupPeopleQuestions[0] == null) {
          this._NotofyService.onFalsenik("ไม่พบแบบสอบถามในเรื่องตรวจ");
        } else {
          this.reportservice.createReportQuestionnaire(subjectgroup.subjectGroupPeopleQuestions[0].centralPolicyEventId).subscribe(result => {
            window.open(this.downloadUrl + "/" + result.data);
          })
        }
      })
  }
  ////reporttype5end/////

  deleteProvince(value) {
    // alert(this.class)
    if (this.land == "ไม่ลงพื้นที่") {

      if (this.class == "แผนการตรวจประจำปี") {

        this.subjectservice.delsubjecteventnoland(value)
          .subscribe(result => {
            this._NotofyService.onSuccess("ลบข้อมูล",)
            // this.spinner.show();
            this.modalRef.hide()
            this.loading = false
            this.getSubjectevent();
          })

      } else {
        this.inspectionplanservice.deleteCentralPolicy(value).subscribe(response => {
          this._NotofyService.onSuccess("ลบข้อมูล",)
          // this.spinner.show();
          this.modalRef.hide()
          this.loading = false
          this.getSubjectevent();
        })
      }

    } else if (this.land == "ลงพื้นที่") {
      if (this.class == "แผนการตรวจประจำปี") {
        this.inspectionplanservice.getCentralPolicyEvent(value).subscribe(response => {

          this.inspectionplanservice.deleteCentralPolicyEvent(response).subscribe(response => {
            this._NotofyService.onSuccess("ลบข้อมูล",)
            // this.spinner.show();
            this.modalRef.hide()
            this.loading = false
            this.getSubjectevent();

          })
        })
      } else {
        this.inspectionplanservice.deleteCentralPolicy(value).subscribe(response => {
          this._NotofyService.onSuccess("ลบข้อมูล",)
          // this.spinner.show();
          this.modalRef.hide()
          this.loading = false
          this.getSubjectevent();
        })
      }
    }
  }
  ////reporttype6start/////
  reporttyp61(value) {
    // alert(myradio)
    this.checkTypeRepor6 = 1;
    this.FormReporttype6.patchValue({
      type: this.checkTypeRepor6,
    })
  }
  reporttyp62(value) {
    // alert(myradio)
    this.checkTypeRepor6 = 2;
    this.FormReporttype6.patchValue({
      type: this.checkTypeRepor6,
    })
  }
  select62province(event) {
    console.log(event);
    this.subjectservice.getsubjecteventprovince(this.userid, event.value).subscribe(result => {
      console.log(result);
      this.selectdatacentralpolicytype6 = result.map((item, index) => {
        return { value: item.id, label: item.centralPolicy.title }
      })
    })
  }

  select61(event) {
    var subjectgroup: any = []
    this.subjectgroupidtype6 = this.rssj(event.value).id,
      this.centralPolicyIdtype6 = this.rssj(event.value).centralPolicyId,
      this.provinceIdtype6 = this.rssj(event.value).provinceId
    // alert(this.subjectgroup.subjectGroupPeopleQuestions[0].centralPolicyEventId)
    this.inspectionplanservice.getcentralpolicyprovinceid(this.centralPolicyIdtype6, this.provinceIdtype6).subscribe(result => {
      this.centralpolicyprovinceidtype6 = result
      this.centralpolicyservice.getSubjecteventdetaildata(this.centralpolicyprovinceidtype6, this.subjectgroupidtype6)
        .subscribe(result => {
          subjectgroup = result.subjectgroup
          this.centralpolicyservice.getquestionpeople(this.centralpolicyprovinceidtype6, subjectgroup.subjectGroupPeopleQuestions[0].centralPolicyEventId).subscribe(res => {
            // alert(JSON.stringify(res))
            this.questionpeople = res;
            this.questionpeople.forEach(element => {
              this.answerpeople.push(element.answerCentralPolicyProvinces)
            });
            this.selectpeopletype6 = this.questionpeople[0].answerCentralPolicyProvinces.map((item, index) => {
              return {
                value: item.user.id,
                label: item.user.name,
              }
            })
            console.log("question: ", this.questionpeople);
            console.log("test", this.answerpeople);
          })
        })
    })
  }
  select62(event) {
    this.subjectgroupidtype6 = this.rssj(event.value).id,
      this.centralPolicyIdtype6 = this.rssj(event.value).centralPolicyId,
      this.provinceIdtype6 = this.rssj(event.value).provinceId
    this.inspectionplanservice.getcentralpolicyprovinceid(this.centralPolicyIdtype6, this.provinceIdtype6).subscribe(result => {
      this.centralpolicyprovinceidtype6 = result
    })
  }
  storeReportComment(value) {
    console.log(value);
    this.reportservice.createReportCommenttype1(value, this.centralpolicyprovinceidtype6).subscribe(result => {
      window.open(this.downloadUrl + "/" + result.data);
    })
  }
  storeReportComment2(value) {
    this.reportservice.createReportCommenttype2(value, this.provinceIdtype6, this.centralpolicyprovinceidtype6).subscribe(result => {
      window.open(this.downloadUrl + "/" + result.data);
    })
  }
  ////reporttype6end/////
}
