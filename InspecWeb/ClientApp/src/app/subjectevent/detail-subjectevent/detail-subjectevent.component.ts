import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray } from '@angular/forms';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ChartOptions, ChartType } from 'chart.js';
import { Label } from 'ng2-charts';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { UserService } from 'src/app/services/user.service';
import { SubjectService } from 'src/app/services/subject.service';
import { ElectronicbookService } from 'src/app/services/electronicbook.service';
import { DepartmentService } from 'src/app/services/department.service';
import { NotificationService } from 'src/app/services/notification.service';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import * as Chart from 'chart.js';
import { SubquestionService } from 'src/app/services/subquestion.service';
import { IMyOptions, IMyDateModel } from 'mydatepicker-th';
import * as _ from 'lodash';
import { ReportService } from 'src/app/services/report.service';
import { AnyAaaaRecord } from 'dns';
import { NotofyService } from 'src/app/services/notofy.service';
import { ReturnStatement } from '@angular/compiler';
@Component({
  selector: 'app-detail-subjectevent',
  templateUrl: './detail-subjectevent.component.html',
  styleUrls: ['./detail-subjectevent.component.css']
})
export class DetailSubjecteventComponent implements OnInit {
  myDatePickerOptions: IMyOptions = {
    // other options...
    // dateFormat: 'dd/mm/yyyy',
    showClearDateBtn: false,
    editableDateField: false
  };

  submitted = false;
  notificationsubjectDate: any;
  deadlinesubjectDate: any;
  notificationpeoplequestionDate: any;
  deadlinepeoplequestionDate: any;

  departmentSelect: any[] = []
  subjectgroupstatussuggestion
  subjectgroupstatus
  subjectgroupland
  id
  subjectgroupid
  resultuser: any = []
  resultpeople: any = []
  resultministrypeople: any = []
  resultdetailcentralpolicy: any = []
  resultcentralpolicyuser: any = []
  allMinistryPeople: any = [];
  allUserPeople: any = [];
  resultdetailcentralpolicyprovince: any = []
  subjectgroup: any = []
  UserPeopleId: any;
  // UserMinistryId: any;
  FormAddQuestionsclose: FormGroup;
  Form2: FormGroup;
  Form3: FormGroup;
  Form: FormGroup;
  EditForm: FormGroup;
  EditForm2: FormGroup;
  EditForm3: FormGroup;
  EditForm4: FormGroup;
  FormSubject: FormGroup;
  FormReport: FormGroup;
  FormReportComment: FormGroup;
  FormReportSuggestionsResult: FormGroup;
  AddForm: FormGroup;
  selectpeople: Array<any>
  selectministrypeople: Array<any>
  modalRef: BsModalRef;
  editid: any
  subquestionclosename: any
  subquestionclosechoicename: any
  subject: any;
  subjectquestionopen: any;
  downloadUrl: any
  urllink
  loading = false;
  electronicbookid: any
  selectdataministrypeople: Array<any>
  ministryPeople: any = [];
  selectdatapeople: Array<any>
  userPeople: any = [];
  fileStatus = false;
  form: FormGroup;
  carlendarFile: any = [];
  provincename: any;
  provinceid
  resultdate: any = []
  department: any = []
  peopleanswer: any = []
  subjectid
  delid
  userid
  role_id
  temp = []
  resultdsubjectid: any = []
  editAnswerForm: FormGroup;
  answer: any;
  FormQuestion: FormGroup;
  answerData: any = [];
  centralpolicyprovincedata: any;
  answerSubquestions: any = []
  subquestionChoice: any[] = []
  fileStatus2 = false;
  signatureFile: any = [];
  fileType: any;
  lineChart: any = [];
  role7Count: any = 0;
  role6Count: any = 0;
  questionpeople: any = [];
  barChartOptions: ChartOptions = {
    responsive: true,
    scales: {
      xAxes: [
        {
          // stacked: true
        }
      ],
      yAxes: [
        {
          ticks: {
            suggestedMin: 0,
            // suggestedMax: 100
          },
          // stacked: true
        }
      ]
    },
  };
  // subquestionChoice[i].question
  barChartLabels: Label[] = ['2013', '2014', '2015', '2016', '2017', '2018'];
  barChartType: ChartType = 'bar';
  barChartLegend = true;
  barChartPlugins = [];
  // ssss: ChartDataSets[] = [{ data: [[5, 6], [3, 6]] }]
  barchartAllset: any = {
    label: ['เห็นด้วย', 'ไม่เห็นด้วย'],
    barChartData: [
      { data: [3, 1], label: 'หน่วยงาน A', stack: 'a' },
      { data: [2, 2], label: 'หน่วยงาน B', stack: 'a' },
    ]
  }
  showChart: number[] = []
  checkChart(id: number) { return (this.showChart.find(res => id == res) ? true : false) }
  closeChart(id: number) {

    const index = this.showChart.indexOf(id);
    delete this.showChart[index]
    if (index > -1) {

      // this.showChart.splice(index, 1);
    }
  }
  listfiles: any = [];
  fileData: any = [{ ebookFile: '', fileDescription: '' }];

  get f() { return this.form.controls }
  get s() { return this.f.fileData as FormArray }
  get faqc() { return this.FormAddQuestionsclose.controls; }
  get faed() { return this.Form2.controls; }
  get fadq() { return this.FormSubject.controls; }

  filterboxdepartments: any = []
  checkTypeReport: any;
  checkTypeReportPeople: any;
  checkTypeSuggestionsresult: any
  select = []
  answerpeople: any = []
  answerRecommenDationInspectors: any = []
  checkname: any[] = []
  constructor(
    private fb: FormBuilder,
    private modalService: BsModalService,
    private centralpolicyservice: CentralpolicyService,
    private userservice: UserService,
    private subjectservice: SubjectService,
    private activatedRoute: ActivatedRoute,
    // private spinner: NgxSpinnerService,
    private electronicBookService: ElectronicbookService,
    private departmentService: DepartmentService,
    private notificationService: NotificationService,
    private authorize: AuthorizeService,
    private userService: UserService,
    private subquestionservice: SubquestionService,
    private reportservice: ReportService,
    private _NotofyService: NotofyService,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.id = activatedRoute.snapshot.paramMap.get('result')
    this.subjectgroupid = activatedRoute.snapshot.paramMap.get('subjectgroupid')
    this.downloadUrl = baseUrl + '/Uploads';
    this.urllink = baseUrl + 'answersubject/outsider/';
  }

  async ngOnInit() {
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        this.userService.getuserfirstdata(this.userid)
          .subscribe(result => {
            this.role_id = result[0].role_id
          })
      })

    //console.log("ID: ", this.id);

    this.FormQuestion = this.fb.group({
      // notificationdate: new FormControl(null, [Validators.required]),
      // deadlinedate: new FormControl(null, [Validators.required]),
      question: new FormControl(null, [Validators.required]),
    })


    // this.spinner.show();
    this.Form = this.fb.group({
      UserPeopleId: new FormControl(null, [Validators.required]),
    })


    this.Form2 = this.fb.group({
      DepartmentId: new FormControl(null, [Validators.required]),
      // UserMinistryId: new FormControl(null, [Validators.required]),
    })
    this.Form3 = this.fb.group({
      peopleanswer: new FormControl(null, [Validators.required]),
      // UserMinistryId: new FormControl(null, [Validators.required]),
    })
    this.form = this.fb.group({
      files: [null],
      // step: new FormControl(null, [Validators.required]),
      status: new FormControl(null, [Validators.required]),
      questionPeople: new FormControl(null, [Validators.required]),
      signatureFiles: [null],
      description: new FormControl(null, [Validators.required]),
      fileType: new FormControl("เลือกประเภทเอกสารแนบ", [Validators.required]),
      fileData: new FormArray([]),

      notificationsubjectdate: new FormControl(null),
      deadlinesubjectdate: new FormControl(null),

      notificationpeoplequestiondate: new FormControl(null),
      deadlinepeoplequestiondate: new FormControl(null),

      suggestion: new FormControl(null),
      statussuggestion: new FormControl(null),
    })


    this.EditForm = this.fb.group({
      subquestionclose: new FormControl(),
    })

    this.EditForm2 = this.fb.group({
      subquestionclosechoice: new FormControl(),
    })

    this.EditForm3 = this.fb.group({
      subject: new FormControl(),
    })

    this.EditForm4 = this.fb.group({
      subjectquestionopen: new FormControl(),
    })

    this.AddForm = this.fb.group({
      name: new FormControl(null, [Validators.required]),
      // centralpolicydateid: new FormControl(null, [Validators.required]),
      status: new FormControl("ใช้งานจริง", [Validators.required]),
      inputsubjectdepartment: this.fb.array([
        // this.initdepartment()
      ]),
    })
    this.FormReport = this.fb.group({
      type: new FormControl(null, [Validators.required]),
      provincialDepartmentId: new FormControl(null, [Validators.required])
    })
    this.FormReportComment = this.fb.group({
      type: new FormControl(null, [Validators.required]),
      userid: new FormControl(null, [Validators.required])
    })
    this.FormReportSuggestionsResult = this.fb.group({
      type: new FormControl(null, [Validators.required]),
      provincialDepartmentId: new FormControl(null, [Validators.required]),
      SubjectGroupId: this.subjectgroupid
    })
    await this.getsubjecteventDetail();
    await this.getDetailCentralPolicyProvince()
    // await this.getMinistryPeople();
    // await this.getUserPeople();
    await this.getAnswer2();
    // await this.getDepartment()


    setTimeout(() => {
      // this.spinner.hide();
    }, 800);
  }
  graph() {
    this.lineChart = new Chart('lineChart', { // สร้าง object และใช้ชื่อ id lineChart ในการอ้างอิงเพื่อนำมาเเสดงผล
      type: 'bar', // ใช้ชนิดแผนภูมิแบบเส้นสามารถเปลี่ยนชิดได้
      data: { // ข้อมูลภายในแผนภูมิแบบเส้น
        labels: ["Jan", "Feb", "March", "April", "May", "June", "July", "August", "Sep", "Oct", "Nov", "Dec"], // ชื่อของข้อมูลในแนวแกน x
        datasets: [{ // กำหนดค่าข้อมูลภายในแผนภูมิแบบเส้น
          label: 'Number of items sold in months',
          data: [9, 7, 3, 5, 2, 10, 15, 61, 19, 3, 1, 9],
          fill: false,
          lineTension: 0.2,
          borderColor: "red", // สีของเส้น
          borderWidth: 1
        }]
      },
      options: {
        title: { // ข้อความที่อยู่ด้านบนของแผนภูมิ
          text: "Bar Chart",
          display: true
        }
      },
    })
  }

  async openModalQuestionPeople(template: TemplateRef<any>) {
    this.submitted = false;
    this.modalRef = this.modalService.show(template);
  }

  async openModal(template: TemplateRef<any>) {
    this.submitted = false;
    this.modalRef = this.modalService.show(template);
    // await this.getMinistryPeople();
    // await this.getUserPeople();
    this.getDepartmentdata();
  }


  openModal3(template: TemplateRef<any>, subjectid) {
    this.submitted = false;
    this.subjectid = subjectid
  }

  openAlertModal(template: TemplateRef<any>) {
    this.submitted = false;
    this.modalRef = this.modalService.show(template);
  }

  editModal(template: TemplateRef<any>, id, name) {
    this.submitted = false;
    this.editid = id;
    this.subquestionclosename = name;

    this.modalRef = this.modalService.show(template);
    this.EditForm = this.fb.group({
      subquestionclose: new FormControl(),

    })
    this.EditForm.patchValue({
      subquestionclose: name,
    })
  }
  showGraph(item, id) {
    //console.log('id', id);
    var barchartAllset: any
    var dataE: Array<any> = item.subquestionChoiceCentralPolicyProvinces
      .map(element => console.log('element', this.chartLabel(element))
      )
  }
  chartLabel(element) {
    return element
  }

  editModal2(template: TemplateRef<any>, id, name) {
    this.editid = id;
    this.subquestionclosechoicename = name;

    this.modalRef = this.modalService.show(template);
    this.EditForm2 = this.fb.group({
      subquestionclosechoice: new FormControl(),

    })
    this.EditForm2.patchValue({
      subquestionclosechoice: name,
    })
  }

  editModal3(template: TemplateRef<any>, id, name) {
    this.editid = id;
    this.subject = name;

    this.modalRef = this.modalService.show(template);
    this.EditForm3 = this.fb.group({
      subject: new FormControl(),

    })
    this.EditForm3.patchValue({
      subject: name,
    })
  }

  editModal4(template: TemplateRef<any>, id, name) {
    this.editid = id;
    this.subjectquestionopen = name;

    this.modalRef = this.modalService.show(template);
    this.EditForm4 = this.fb.group({
      subjectquestionopen: new FormControl(),

    })
    this.EditForm4.patchValue({
      subjectquestionopen: name,
    })
  }

  editModal5(template: TemplateRef<any>, id, name) {
    this.editid = id;
    this.answer = name;

    this.modalRef = this.modalService.show(template);
    this.editAnswerForm = this.fb.group({
      answer: new FormControl(),
      answerId: new FormControl(),

    })
    this.editAnswerForm.patchValue({
      answer: name,
      answerId: id
    })
  }

  DelModal(template: TemplateRef<any>, id) {
    this.delid = id;
    this.modalRef = this.modalService.show(template);
  }

  DelModal2(template: TemplateRef<any>, id) {
    this.delid = id;
    this.modalRef = this.modalService.show(template);
  }

  delsubjectModal(template: TemplateRef<any>, id) {
    this.delid = id;
    this.modalRef = this.modalService.show(template);
  }

  delquestionModal(template: TemplateRef<any>, id) {
    this.delid = id;
    this.modalRef = this.modalService.show(template);
  }
  deloptionModal(template: TemplateRef<any>, id) {
    this.delid = id;
    this.modalRef = this.modalService.show(template);
  }
  reportModal(template: TemplateRef<any>, provincialDepartment) {
    this.select = provincialDepartment.map((item, index) => {
      return {
        value: item.provincialDepartment.id,
        label: item.provincialDepartment.name,
      }
    })
    //console.log("select", this.select);
    this.checkTypeReport = 0;
    this.modalRef = this.modalService.show(template);
  }
  reportModalSuggestionsresult(template: TemplateRef<any>) {
    this.checkTypeSuggestionsresult = 0;
    this.select = this.answerRecommenDationInspectors.map((item, index) => {
      return {
        value: item.id,
        label: item.name,
      }
    })
    this.modalRef = this.modalService.show(template);
  }
  reportModalComment(template: TemplateRef<any>) {
    this.checkTypeReportPeople = 0;
    this.modalRef = this.modalService.show(template);
  }
  report1(value) {
    // alert(myradio)
    this.checkTypeReport = 1;
    this.FormReport.patchValue({
      type: this.checkTypeReport,
    })
  }
  report2(value) {
    // alert(myradio)
    this.checkTypeReport = 2;
    this.FormReport.patchValue({
      type: this.checkTypeReport,
    })
  }
  report3(value) {
    // alert(myradio)
    this.checkTypeReport = 3;
  }
  reportsuggestionsresult1(value) {
    // alert(myradio)
    this.checkTypeSuggestionsresult = 1;
    this.FormReportSuggestionsResult.patchValue({
      type: this.checkTypeSuggestionsresult,
    })
  }
  reportsuggestionsresult2(value) {
    // alert(myradio)
    this.checkTypeSuggestionsresult = 2;
    this.FormReportSuggestionsResult.patchValue({
      type: this.checkTypeSuggestionsresult,
    })
  }
  reportcomment1(value) {
    // alert(myradio)
    this.checkTypeReportPeople = 1;
    this.FormReportComment.patchValue({
      type: this.checkTypeReportPeople,
    })
  }
  reportcomment2(value) {
    // alert(myradio)
    this.checkTypeReportPeople = 2;
    this.FormReportComment.patchValue({
      type: this.checkTypeReportPeople,
    })
  }
  getDetailCentralPolicyProvince() {
    this.centralpolicyservice.getdetailcentralpolicyprovincedata(this.id)
      .subscribe(result => {
        console.log("getDetailCentralPolicyProvince 1=>", result);
        // alert(JSON.stringify(result))
        this.resultdetailcentralpolicy = result.centralpolicydata
        // this.resultdate = result.centralPolicyEventdata.inspectionPlanEvent
        this.provincename = result.provincedata.name
        this.provinceid = result.provincedata.id
        // this.resultuser = result.userdata
        // this.electronicbookid = result.centralPolicyEventdata.electronicBookId
        // this.resultdetailcentralpolicyprovince = result.subjectcentralpolicyprovincedata
        this.centralpolicyprovincedata = result.centralpolicyprovince

        // alert(result.centralPolicyEventdata.inspectionPlanEvent.id);

        // if (result.centralPolicyEventdata.inspectionPlanEvent.id != null) {
        //   // alert("IN")
        //   this.centralpolicyservice.getdetailcentralpolicyprovincedata3(result.centralPolicyEventdata.inspectionPlanEvent.id)
        //     .subscribe(result2 => {
        //       alert(JSON.stringify(result2))
        //       this.resultuser = result.userdata
        //     })
        // }
        // this.form.patchValue({
        //   questionPeople: this.centralpolicyprovincedata.questionPeople,
        //   status: this.centralpolicyprovincedata.status
        // })

        // if (this.role_id == 3) {
        //   if (this.centralpolicyprovincedata.step == 'มอบหมายเขต') {
        //     this.centralpolicyprovincedata.step = "มอบหมายจังหวัด"
        //   }
        //   this.form.patchValue({
        //     step: this.centralpolicyprovincedata.step
        //   })
        // } else if (this.role_id == 5) {
        //   if (this.centralpolicyprovincedata.step == 'มอบหมายเขต') {
        //     this.centralpolicyprovincedata.step = "มอบหมายเขต"
        //   }
        //   this.form.patchValue({
        //     step: this.centralpolicyprovincedata.step
        //   })
        // }

        // this.resultdetailcentralpolicyprovince.forEach(element => {
        //   var subquestionCentralPolicyProvinces: any[] = element.subquestionCentralPolicyProvinces
        //   var subquestionChoicef: any[] = []

        //   subquestionCentralPolicyProvinces
        //     .filter(element2 => {
        //       return element2.type == "คำถามปลายปิด"
        //     })
        //     .forEach(result => {
        //       var data: any[] = result.subquestionChoiceCentralPolicyProvinces
        //       var data2: any[]
        //       var dataanswer: any[] = result.answerSubquestions
        //       var dataanswer2: any[]
        //       data2 = data.map(result => {
        //         return result.name
        //       })
        //       //console.log("result", result);

        //       dataanswer2 = dataanswer.map(result => {
        //         return result.answer
        //       })
        //       var reasult1 = {}
        //       dataanswer2.forEach(
        //         function (x) {
        //           reasult1[x] = (reasult1[x] || 0) + 1;
        //         });

        //       this.subquestionChoice.push(
        //         { question: data2, answer: dataanswer2.length == 0 ? 0 : dataanswer2, data: Object.values(reasult1) }
        //       )

        //     })
        //   //console.log(this.subquestionChoice);


        // })
        // //console.log('subquestionChoice', this.subquestionChoice);
        this.getCalendarFile();
      })
  }
  getsubjecteventDetail() {
    // alert("123")
    this.centralpolicyservice.getSubjecteventdetaildata(this.id, this.subjectgroupid)
      .subscribe(result => {
        console.log("getsubjecteventDetail 1=>", result);

        this.resultdetailcentralpolicyprovince = result.subjectcentralpolicyprovincedata

        for (let i = 0; i < result.subjectcentralpolicyprovincedata.length; i++) {
          this.checkname[i] = result.subjectcentralpolicyprovincedata[i].name;
        }

        console.log("this.checkname", this.checkname)
        // for(){

        // }
        // this.checkname =

        this.subjectgroup = result.subjectgroup

        //console.log("this.subjectgroup", this.subjectgroup);
        // alert(JSON.stringify(this.subjectgroup.peopleQuestionNotificationDate))


        this.notificationsubjectDate = this.time(this.subjectgroup.subjectNotificationDate)


        this.deadlinesubjectDate = this.time(this.subjectgroup.subjectDeadlineDate)



        this.notificationpeoplequestionDate = this.time(this.subjectgroup.peopleQuestionNotificationDate)


        this.deadlinepeoplequestionDate = this.time(this.subjectgroup.peopleQuestionDeadlineDate)


        this.resultdate = result.centralPolicyEventdata.inspectionPlanEvent
        // alert(JSON.stringify(this.resultdate))
        this.subjectgroupstatus = this.subjectgroup.status

        this.subjectgroupstatussuggestion = this.subjectgroup.statusSuggestion

        this.subjectgroupland = this.subjectgroup.land
        this.resultuser = result.userdata
        // this.subjectgroups = this.subjectgroup.created
        // alert("123")
        // alert(this.subjectgroupland)

        //console.log("result", result);
        // alert(JSON.stringify(this.subjectgroup.status))
        if (this.subjectgroup.suggestion != null && this.subjectgroup.suggestion != "null") {
          this.form.patchValue({
            // questionPeople: this.centralpolicyprovincedata.questionPeople,
            status: this.subjectgroup.status,
            suggestion: this.subjectgroup.suggestion,
            statussuggestion: this.subjectgroup.statusSuggestion
          })
        }

        this.form.patchValue({
          status: this.subjectgroup.status,
          statussuggestion: this.subjectgroup.statusSuggestion
        })

        var test: any = [];
        this.subjectgroup.answerRecommenDationInspectors.forEach(element => {
          test.push(element.user.provincialDepartments)
        });
        // //console.log("test: ", test);
        this.answerRecommenDationInspectors = test.filter(
          (thing, i, arr) => arr.findIndex(t => t === thing) === i
        );
        //console.log("uniqueanswerRecommenDationInspectors: ", this.answerRecommenDationInspectors);

        // this.getquestion();
        this.getquestion();
      })
  }
  storeFiles(value) {
    // alert(this.notificationsubjectDate.year)
    // return;
///////////////////////
    // if (this.subjectgroupland == 'ลงพื้นที่') {
    //   if (this.notificationpeoplequestionDate.year == 1970 || this.deadlinepeoplequestionDate.year == 1970) {
    //     if (this.form.value.notificationpeoplequestiondate == null || this.form.value.deadlinepeoplequestiondate == null) {
    //       this.submitted = true;
    //       return;
    //     }
    //   }
    // }


    // if (this.notificationsubjectDate.year == 1970 || this.deadlinesubjectDate.year == 1970) {
    //   if (this.form.value.notificationsubjectdate == null || this.form.value.deadlinesubjectdate == null) {
    //     // alert("2")
    //     this.submitted = true;
    //     return;
    //   }
    // }
    //////////////////
    // if (this.form.value.notificationsubjectdate == null) {
    //   alert("if")
    //   this.form.value.notificationsubjectdate = this.notificationsubjectDate
    //   alert(JSON.stringify(this.form.value.notificationsubjectdate))
    // } else {
    //   alert(JSON.stringify(this.form.value.notificationsubjectdate))
    // }
    // alert(JSON.stringify(this.form.value.notificationsubjectdate))

    if (this.form.value.notificationsubjectdate == null) {
      this.form.value.notificationsubjectdate = this.notificationsubjectDate.year + "-" + this.notificationsubjectDate.month + "-" + this.notificationsubjectDate.day
    } else {
      this.form.value.notificationsubjectdate = this.form.value.notificationsubjectdate.date.year + "-" + this.form.value.notificationsubjectdate.date.month + "-" + this.form.value.notificationsubjectdate.date.day
    }
    if (this.form.value.deadlinesubjectdate == null) {
      this.form.value.deadlinesubjectdate = this.deadlinesubjectDate.year + "-" + this.deadlinesubjectDate.month + "-" + this.deadlinesubjectDate.day
    } else {
      this.form.value.deadlinesubjectdate = this.form.value.deadlinesubjectdate.date.year + "-" + this.form.value.deadlinesubjectdate.date.month + "-" + this.form.value.deadlinesubjectdate.date.day
    }
    if (this.form.value.notificationpeoplequestiondate == null) {
      this.form.value.notificationpeoplequestiondate = this.notificationpeoplequestionDate.year + "-" + this.notificationpeoplequestionDate.month + "-" + this.notificationpeoplequestionDate.day
    } else {
      this.form.value.notificationpeoplequestiondate = this.form.value.notificationpeoplequestiondate.date.year + "-" + this.form.value.notificationpeoplequestiondate.date.month + "-" + this.form.value.notificationpeoplequestiondate.date.day
    }
    if (this.form.value.deadlinepeoplequestiondate == null) {
      this.form.value.deadlinepeoplequestiondate = this.deadlinepeoplequestionDate.year + "-" + this.deadlinepeoplequestionDate.month + "-" + this.deadlinepeoplequestionDate.day
    } else {
      this.form.value.deadlinepeoplequestiondate = this.form.value.deadlinepeoplequestiondate.date.year + "-" + this.form.value.deadlinepeoplequestiondate.date.month + "-" + this.form.value.deadlinepeoplequestiondate.date.day
    }
    // alert(JSON.stringify(this.form.value.notificationsubjectdate))
    // return;
    // if(this.form.value.notificationsubjectdate == '1970-1-1' || this.form.value.notificationsubjectdate == null){
    //   this.form.value.notificationsubjectdate = null
    // } else {
    //   this.form.value.notificationsubjectdate = this.form.value.notificationsubjectdate.date.year + "-" + this.form.value.notificationsubjectdate.date.month + "-" + this.form.value.notificationsubjectdate.date.day
    // }
    // if(this.form.value.deadlinesubjectdate == '1970-1-1' || this.form.value.deadlinesubjectdate == null){
    //   this.form.value.deadlinesubjectdate = null
    // }else {
    //      this.form.value.deadlinesubjectdate = this.form.value.deadlinesubjectdate.date.year + "-" + this.form.value.deadlinesubjectdate.date.month + "-" + this.form.value.deadlinesubjectdate.date.day
    //    }

    // if(this.form.value.notificationpeoplequestiondate == '1970-1-1' || this.form.value.notificationpeoplequestiondate == null){
    //   this.form.value.notificationpeoplequestiondate = null
    // }else {
    //  this.form.value.notificationpeoplequestiondate = this.form.value.notificationpeoplequestiondate.date.year + "-" + this.form.value.notificationpeoplequestiondate.date.month + "-" + this.form.value.notificationpeoplequestiondate.date.day
    //    }
    // if(this.form.value.deadlinepeoplequestiondate == '1970-1-1' || this.form.value.deadlinepeoplequestiondate == null){
    //   this.form.value.deadlinepeoplequestiondate = null
    // }else {
    //     this.form.value.deadlinepeoplequestiondate = this.form.value.deadlinepeoplequestiondate.date.year + "-" + this.form.value.deadlinepeoplequestiondate.date.month + "-" + this.form.value.deadlinepeoplequestiondate.date.day
    //    }


    // alert(this.form.value.notificationsubjectdate)
    // alert("123")
    // if(value.status == "ใช้งานจริง"){
    //   this.notificationService.addNotification(this.resultdetailcentralpolicy.id, this.provinceid, this.userid, 4, 1)
    //   .subscribe(response => {
    //     //console.log(response);
    //   })
    // }
    this.electronicBookService.addSubjectEventFile(value, this.form.value.files, this.subjectgroupid, this.id, this.form.value.signatureFiles).subscribe(response => {
      //console.log(value);
      this.Form.reset()

      if (value.status == "ใช้งานจริง") {
        this.notificationService.addNotification(this.resultdetailcentralpolicy.id, this.provinceid, this.userid, 4, this.subjectgroupid, null, this.userid)
          .subscribe(response => {
            //console.log(response);
          })
        this.notificationService.addNotification(this.resultdetailcentralpolicy.id, this.provinceid, this.userid, 5, this.subjectgroupid, null, this.userid)
          .subscribe(response => {
            //console.log(response);
          })
      }

      if (value.statussuggestion == "ใช้งานจริง") {
        this.notificationService.addNotification(this.resultdetailcentralpolicy.id, this.provinceid, this.userid, 25, this.subjectgroupid, null, this.userid)
          .subscribe(response => {
            //console.log(response);
          })
      }

      window.history.back();
      // setTimeout(() => {
      //   // this.getCalendarFile();
      //   this.form.reset();
      // }, 300);

    })
  }


  storeDepartment(value) {
    // alert(this.subjectid)
    if (this.Form2.invalid) {
      this.submitted = true;
      console.log("in1");
      return;
    } else {
      this.centralpolicyservice.addDepartment(value, this.subjectid).subscribe(response => {
        this._NotofyService.onSuccess("เพื่มข้อมูล")
        //console.log(value);
        this.Form2.reset()
        this.modalRef.hide()
        this.getsubjecteventDetail();
      })
    }

  }

  storepeopleanswer(value) {
    let UserPeopleanswerId: any[] = value.peopleanswer
    this.centralpolicyservice.addPeopleAnswer(value, this.subjectid).subscribe(response => {
      //console.log(value);
      this.Form3.reset()
      this.modalRef.hide()


      for (let i = 0; i < UserPeopleanswerId.length; i++) {
        this.notificationService.addNotification(this.resultdetailcentralpolicy.id, this.provinceid, UserPeopleanswerId[i], 5, 1, null, this.userid)
          .subscribe(response => {
            //console.log(response);

          })
      }

      this.getDetailCentralPolicyProvince();
    })
  }

  editsubquestionclose(value, id) {
    this.subjectservice.editsubquestionprovince(value, id).subscribe(response => {
      this._NotofyService.onSuccess("แก้ไขข้อมูล")
      //console.log(value);
      this.EditForm.reset()
      this.modalRef.hide()
      this.getDetailCentralPolicyProvince();

      this.getsubjecteventDetail();
      this.getAnswer2();
    })
  }

  editsubquestionclosechoice(value, id) {
    this.subjectservice.editsubquestionchoiceprovince(value, id).subscribe(response => {
      this._NotofyService.onSuccess("แก้ไขข้อมูล")
      //console.log(value);
      this.EditForm2.reset()
      this.modalRef.hide()
      this.getDetailCentralPolicyProvince();

      this.getsubjecteventDetail();
      this.getAnswer2();
    })
  }

  editsubject(value, id) {
    this.subjectservice.editsubjectchoiceprovince(value, id).subscribe(response => {
      //console.log(value);
      this.EditForm3.reset()
      this.modalRef.hide()
      this.getDetailCentralPolicyProvince();

      this.getsubjecteventDetail();
      this.getAnswer2();
    })
  }

  editsubjectquestionopen(value, id) {
    this.subjectservice.editsubjectquestionopenchoiceprovince(value, id).subscribe(response => {
      //console.log(value);
      this.EditForm4.reset()
      this.modalRef.hide()
      this.getDetailCentralPolicyProvince();

      this.getsubjecteventDetail();
      this.getAnswer2();
    })
  }

  editAnswer(value, id) {
    this.subjectservice.editAnswer(value, id).subscribe(response => {
      //console.log(value);
      this.editAnswerForm.reset()
      this.modalRef.hide()
      this.getDetailCentralPolicyProvince();

      this.getsubjecteventDetail();
      this.getAnswer2();
    })
  }

  // async getMinistryPeople() {
  //   await this.userservice.getuserdata(6).subscribe(result => {
  //     this.resultministrypeople = result // All
  //   })
  // }

  // async getRecycledMinistryPeople() {
  //   this.selectdataministrypeople = []
  //   this.ministryPeople = this.allMinistryPeople
  //   //console.log("MINISTRY: ", this.ministryPeople);
  //   //console.log("allMinistry: ", this.resultministrypeople);
  //   if (this.ministryPeople.length == 0) {
  //     for (var i = 0; i < this.resultministrypeople.length; i++) {
  //       await this.selectdataministrypeople.push({ value: this.resultministrypeople[i].id, label: this.resultministrypeople[i].name })
  //     }
  //   }
  //   else {
  //     for (var i = 0; i < this.resultministrypeople.length; i++) {
  //       var n = 0;
  //       for (var ii = 0; ii < this.ministryPeople.length; ii++) {
  //         if (this.resultministrypeople[i].id == this.ministryPeople[ii].id) {
  //           await n++;
  //         }
  //       }
  //       if (n == 0) {
  //         await this.selectdataministrypeople.push({ value: this.resultministrypeople[i].id, label: this.resultministrypeople[i].name })
  //       }
  //     }
  //   }
  //   // //console.log("TEST: ", this.selectdataministrypeople);
  // }

  // async getUserPeople() {
  //   await this.userservice.getuserdata(7).subscribe(result => {
  //     // alert(JSON.stringify(result))
  //     this.resultpeople = result
  //     // alert(JSON.stringify(this.resultpeople))
  //     //console.log("tttt:", this.resultpeople);

  //   })
  // }

  // async getRecycledUserPeople() {
  //   var test: any = [];
  //   test = this.resultpeople;
  //   this.selectdatapeople = []
  //   this.userPeople = this.allUserPeople
  //   //console.log("user: ", this.userPeople);
  //   //console.log("allUser: ", this.resultpeople);

  //   if (this.userPeople.length == 0) {
  //     for (var i = 0; i < this.resultpeople.length; i++) {
  //       await this.selectdatapeople.push({ value: this.resultpeople[i].id, label: this.resultpeople[i].name })
  //     }
  //   }
  //   else {
  //     for (var i = 0; i < this.resultpeople.length; i++) {
  //       var n = 0;
  //       for (var ii = 0; ii < this.userPeople.length; ii++) {
  //         if (this.resultpeople[i].id == this.userPeople[ii].id) {
  //           await n++;
  //         }
  //       }
  //       if (n == 0) {
  //         await this.selectdatapeople.push({ value: this.resultpeople[i].id, label: this.resultpeople[i].name })
  //       }
  //     }
  //   }
  //   //console.log("TEST: ", this.selectdatapeople);
  // }

  uploadFile(event) {
    this.fileStatus = true;
    const file = (event.target as HTMLInputElement).files;

    this.form.patchValue({
      files: file
    });
    //console.log("fff:", this.form.value.files)
    this.form.get('files').updateValueAndValidity()
  }

  uploadFile2(event) {
    var file = (event.target as HTMLInputElement).files;
    for (let i = 0, numFiles = file.length; i < numFiles; i++) {
      this.listfiles.push(file[i])
      this.s.push(this.fb.group({
        ebookFile: file[i],
        fileDescription: '',
      }))
    }
    //console.log("listfiles: ", this.form.value);
    //console.log("eiei: ", this.s.controls);


    this.form.patchValue({
      files: this.listfiles
    });

    // //console.log("listfiles", this.Formfile.get('files'));
    // this.Formfile.get('files').updateValueAndValidity()
  }

  getCalendarFile() {
    this.electronicBookService.getSubjectEventFile(this.subjectgroupid, this.id).subscribe(res => {
      this.carlendarFile = res.carlendarFile;
      this.signatureFile = res.signatureFile;
      //console.log("calendarFile: ", res);

    })
  }

  deleteProvinceial(value) {
    this.subjectservice.deleteProvincial(value).subscribe(response => {
      this._NotofyService.onSuccess("ลบข้อมูล")
      //console.log(value);
      this.modalRef.hide()
      this.loading = false

      this.getsubjecteventDetail();

    })
  }

  deletepeopleanswer(value) {
    this.subjectservice.deletePeopleanswer(value).subscribe(response => {
      //console.log(value);
      this.modalRef.hide()
      this.loading = false

      this.getDetailCentralPolicyProvince();

    })
  }

  removeV(index: number) {
    const control = <FormArray>this.AddForm.controls['inputsubjectdepartment'];
    control.removeAt(index);
  }
  removeW(iv: number, iw: number) {
    const control = (<FormArray>this.AddForm.controls['inputsubjectdepartment']).at(iv).get('inputquestionopen') as FormArray;
    control.removeAt(iw);
  }
  removeX(iv: number, ix: number) {
    const control = (<FormArray>this.AddForm.controls['inputsubjectdepartment']).at(iv).get('inputquestionclose') as FormArray;
    control.removeAt(ix);
  }
  removeY(iv: number, ix: number, iy: number) {
    const control = ((<FormArray>this.AddForm.controls['inputsubjectdepartment']).at(iv).get('inputquestionclose') as FormArray).at(ix).get('inputanswerclose') as FormArray;
    control.removeAt(iy);
  }
  back() {
    window.history.back();
  }

  deletesubject(value) {
    this.subjectservice.deletesubjectrole3(value).subscribe(response => {
      //console.log(value);
      this.modalRef.hide()
      this.loading = false
      this.getDetailCentralPolicyProvince();
      this.getsubjecteventDetail();
    })
  }
  deletequestion(value) {
    this.subjectservice.deletequestionrole3(value).subscribe(response => {
      this._NotofyService.onSuccess("ลบข้อมูล")
      //console.log(value);
      this.modalRef.hide()
      this.loading = false
      this.getsubjecteventDetail();
      this.getDetailCentralPolicyProvince();
    })
  }
  deleteoption(value) {
    this.subjectservice.deleteoptionrole3(value).subscribe(response => {
      //console.log(value);
      this.modalRef.hide()
      this.loading = false
      this.getDetailCentralPolicyProvince();
    })
  }

  getDepartmentdata() {
    this.departmentService.getalldepartdata()
      .subscribe(result => {
        this.temp = result.map((item, index) => {
          return {
            value: item.id,
            label: item.name,
          }
        })
        //console.log(result);
      })
  }

  getAnswer2() {
    this.centralpolicyservice.getAnswer(this.id).subscribe(res => {
      this.answerData = res;
      console.log("getAnswer2 1=> ", this.answerData);
    })
  }

  async openAnswerModal(template: TemplateRef<any>) {
    this.submitted = false;
    this.modalRef = this.modalService.show(template);
  }

  checkType(type) {
    // alert(type)
    this.fileType = type;
  }
  openAddModalQuestionsclose(template: TemplateRef<any>, subjectid) {
    this.submitted = false;
    //console.log("subjectid:", subjectid);
    this.modalRef = this.modalService.show(template);
    this.FormAddQuestionsclose = this.fb.group({
      subjectId: subjectid,
      box: 0,
      type: "คำถามปลายปิด",
      name: new FormControl(null, [Validators.required]),
      ProvincialDepartmentId: new FormArray([]),
      inputanswerclose: this.fb.array([
        this.initanswerclose()
      ])
    })
  }
  openModalSubject2(template: TemplateRef<any>, subjectid) {
    this.submitted = false;
    this.departmentService.getdepartmentdata(this.provinceid).subscribe(res => {
      this.department = res.map((item, index) => {
        return {
          value: item.provincialDepartmentID,
          label: item.provincialDepartment.name
        }
      })

      this.departmentSelect = this.department;

      //console.log("subjectid:", subjectid);

      this.FormSubject = this.fb.group({
        DepartmentId: new FormControl(null, [Validators.required]),
        subjectId: subjectid,
        box: 0,
        type: "คำถามปลายปิด",
        name: new FormControl(null, [Validators.required]),
        ProvincialDepartmentId: new FormArray([]),
        inputanswerclose: this.fb.array([
          this.initanswerclose()
        ])
      })

      this.modalRef = this.modalService.show(template);
    })

  }

  openModalSubject(template: TemplateRef<any>, subjectid, departmentSelected: any[] = []) {
    this.submitted = false;
    this.departmentService.getdepartmentdata(this.provinceid).subscribe(res => {
      this.department = res.map((item, index) => {
        return {
          value: item.provincialDepartmentID,
          label: item.provincialDepartment.name
        }
      })

      //console.log(this.department);
      var data: any[] = departmentSelected.map(result => {
        return result.provincialDepartment.id
      })

      this.departmentSelect = _.filter(this.department, (v) => !_.includes(
        data, v.value
      ))

      //console.log("subjectid:", subjectid);

      this.FormSubject = this.fb.group({
        DepartmentId: new FormControl(null, [Validators.required]),
        subjectId: subjectid,
        box: 0,
        type: "คำถามปลายปิด",
        name: new FormControl(null, [Validators.required]),
        ProvincialDepartmentId: new FormArray([]),
        inputanswerclose: this.fb.array([
          this.initanswerclose()
        ])
      })

    })

    this.modalRef = this.modalService.show(template);
  }
  openModal2(template: TemplateRef<any>, subjectid, departmentSelected: any[] = []) {
    this.submitted = false;
    this.subjectid = subjectid
    this.departmentService.getdepartmentdata(this.provinceid).subscribe(res => {
      this.department = res.map((item, index) => {
        return {
          value: item.provincialDepartmentID,
          label: item.provincialDepartment.name
        }
      })


      var data: any[] = departmentSelected.map(result => {
        return result.provincialDepartment.id
      })

      this.departmentSelect = _.filter(this.department, (v) => !_.includes(
        data, v.value
      ))
      //console.log("this.department", this.department);
      //console.log("departmentSelected", departmentSelected);
      //console.log("departmentSelect", this.departmentSelect);

      this.modalRef = this.modalService.show(template);
    })
  }

  initanswerclose() {
    return this.fb.group({
      answerclose: [null, [Validators.required, Validators.pattern('[0-9]{3}')]],
    })
  }
  addXX() {
    const control = <FormArray>this.FormAddQuestionsclose.controls['inputanswerclose'];
    control.push(this.initanswerclose());
  }
  addXXX() {
    const control = <FormArray>this.FormSubject.controls['inputanswerclose'];
    control.push(this.initanswerclose());
  }
  removeXX(index: number) {
    const control = <FormArray>this.FormAddQuestionsclose.controls['inputanswerclose'];
    control.removeAt(index);
  }
  removeXXX(index: number) {
    const control = <FormArray>this.FormSubject.controls['inputanswerclose'];
    control.removeAt(index);
  }
  AddQuestionsclose(value) {
    //console.log(value);
    if (this.FormAddQuestionsclose.get('name').invalid) {
      this.submitted = true;
      console.log("in1");
      return;
    } else {
      this.subquestionservice.addSubquestioncloseevent(value).subscribe(result => {
        this._NotofyService.onSuccess("เพื่มข้อมูล")
        //console.log(result);
        this.FormAddQuestionsclose.reset()
        this.modalRef.hide()
        this.getsubjecteventDetail()
      })
    }
  }

  storeSubject(value) {
    // alert("123")
    // this.spinner.show();
    //console.log("valuevaluevaluevaluevaluevaluevaluevalue", value);
    if (this.FormSubject.get('name').invalid && this.FormSubject.get('DepartmentId').invalid) {
      this.submitted = true;
      console.log("in1");
      return;
    } else {
      this.subjectservice.addSubjectRole3(value).subscribe(response => {
        this._NotofyService.onSuccess("เพื่มข้อมูล")
        this.AddForm.reset();
        this.modalRef.hide();
        this.getDetailCentralPolicyProvince();
        this.getsubjecteventDetail();
      })
    }
  }
  storeReport() {
    this.reportservice.createReportSubject(this.resultdetailcentralpolicy, this.resultdetailcentralpolicyprovince).subscribe(result => {
      //console.log("export: ", result);
      window.open(this.downloadUrl + "/" + result.data);
      // this.modalRef.hide();
    })
  }
  // storeReportPerformance(value) {
  //   //console.log(value);
  //   this.reportservice.createReporttype1(value).subscribe(result => {
  //     this.FormQuestion.reset();
  //     this.modalRef.hide();
  //     window.open(this.downloadUrl + "/" + result.data);
  //   })
  // }
  // storeReportPerformance2() {
  //   console.log(this.provinceid);
  //   this.reportservice.createReporttype2(this.FormReport.value, this.provinceid, this.id, this.subjectgroupid).subscribe(result => {
  //     this.FormQuestion.reset();
  //     this.modalRef.hide();
  //     window.open(this.downloadUrl + "/" + result.data);
  //   })
  // }
  storeReportQuestionnaire() {
    this.reportservice.createReportQuestionnaire(this.subjectgroup.subjectGroupPeopleQuestions[0].centralPolicyEventId).subscribe(result => {
      window.open(this.downloadUrl + "/" + result.data);
    })
  }
  storeReportsuggestions() {
    this.reportservice.createReportsuggestions(this.subjectgroupid).subscribe(result => {
      window.open(this.downloadUrl + "/" + result.data);
    })
  }
  storeReportSuggestionsResulttype1(value) {
    this.reportservice.createReportSuggestionsResulttype1(value, this.provinceid).subscribe(result => {
      window.open(this.downloadUrl + "/" + result.data);
    })
  }
  storeReportSuggestionsResulttype2(value) {
    this.reportservice.createReportSuggestionsResulttype2(value, this.provinceid).subscribe(result => {
      window.open(this.downloadUrl + "/" + result.data);
    })
  }
  // storeReportComment(value) {
  //   console.log(value);
  //   this.reportservice.createReportCommenttype1(value).subscribe(result => {
  //     window.open(this.downloadUrl + "/" + result.data);
  //   })
  // }
  // storeReportComment2() {
  //   this.reportservice.createReportCommenttype2(this.FormReportComment.value, this.provinceid).subscribe(result => {
  //     window.open(this.downloadUrl + "/" + result.data);
  //   })
  // }
  storeQuestion(value) {
    this.centralpolicyservice.addPeoplequestion(this.id, this.subjectgroup.subjectGroupPeopleQuestions[0].centralPolicyEventId, value).subscribe(res => {
      this.FormQuestion.reset();
      this.modalRef.hide();
      this.getquestion();
    })
  }
  getquestion() {
    // alert("!23")
    // alert(this.subjectgroup.subjectGroupPeopleQuestions[0].centralPolicyEventId)
    if (this.resultdate != null) {
      this.centralpolicyservice.getquestionpeople(this.id, this.subjectgroup.subjectGroupPeopleQuestions[0].centralPolicyEventId).subscribe(res => {
        // alert(JSON.stringify(res))
        this.questionpeople = res;
        this.questionpeople.forEach(element => {
          this.answerpeople.push(element.answerCentralPolicyProvinces)
        });
        this.select = this.questionpeople[0].answerCentralPolicyProvinces.map((item, index) => {
          return {
            value: item.user.id,
            label: item.user.name,
          }
        })
        //console.log("question: ", this.questionpeople);
        //console.log("test", this.answerpeople);
      })
    }
  }

  time(date) {
    //console.log("Date: ", date);

    let ssss = new Date(date)
    var new_date = {
      year: ssss.getFullYear(),
      month: ssss.getMonth() + 1,
      day: ssss.getDate()
    }
    //console.log("newDate: ", new_date);

    return new_date
  }

  onStartDateChanged(event: IMyDateModel) {
    // alert(JSON.stringify(event))
    this.notificationsubjectDate = event.date;
    // this.notificationpeoplequestionDate = event.date;
    // //console.log("SS: ", this.startDate);
  }
  onStartDateChanged2(event: IMyDateModel) {
    // alert(JSON.stringify(event))
    // this.notificationsubjectDate = event.date;
    this.notificationpeoplequestionDate = event.date;
    // //console.log("SS: ", this.startDate);
  }

  onEndDateChanged(event: IMyDateModel) {
    // alert(JSON.stringify(event))]
    this.deadlinesubjectDate = event.date;
    // this.deadlinepeoplequestionDate = event.date;
    // //console.log("EE: ", this.endDate);
  }
  onEndDateChanged2(event: IMyDateModel) {
    // alert(JSON.stringify(event))]
    // this.deadlinesubjectDate = event.date;
    this.deadlinepeoplequestionDate = event.date;
    // //console.log("EE: ", this.endDate);
  }
}
