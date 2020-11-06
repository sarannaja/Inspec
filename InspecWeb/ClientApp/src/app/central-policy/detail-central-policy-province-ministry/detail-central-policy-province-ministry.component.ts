import { Component, OnInit, TemplateRef, Inject, ɵɵtemplateRefExtractor } from '@angular/core';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { FormControl, Validators, FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { IMyOptions, IMyDateModel } from 'mydatepicker-th';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { SubjectService } from 'src/app/services/subject.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { async } from '@angular/core/testing';
import { ElectronicbookService } from 'src/app/services/electronicbook.service';
import { DepartmentService } from 'src/app/services/department.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { NotificationService } from 'src/app/services/notification.service';
import { ChartDataSets, ChartType, ChartOptions, Chart } from 'chart.js';
import { Label } from 'ng2-charts';
import * as _ from 'lodash'
import { InspectionplanService } from 'src/app/services/inspectionplan.service';
@Component({
  selector: 'app-detail-central-policy-province-ministry',
  templateUrl: './detail-central-policy-province-ministry.component.html',
  styleUrls: ['./detail-central-policy-province-ministry.component.css']
})
export class DetailCentralPolicyProvinceMinistryComponent implements OnInit {

  myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd/mm/yyyy',
    showClearDateBtn: false,
    editableDateField: false
  };
  resultuser: any = []
  resultpeople: any = []
  resultministrypeople: any[] = []
  resultdepartmentpeople: any[] = []
  resultprovincialdepartmentpeople: any[] = []
  resultdetailcentralpolicy: any = []
  resultcentralpolicyuser: any = []
  allMinistryPeople: any = [];
  alldepartmentPeople: any = [];
  allprovincialdepartmentPeople: any = [];
  allUserPeople: any = [];
  resultdetailcentralpolicyprovince: any = []
  UserPeopleId: any;
  // UserMinistryId: any;
  id
  Form2: FormGroup;
  Form3: FormGroup;
  Form: FormGroup;
  EditForm: FormGroup;
  EditForm2: FormGroup;
  EditForm3: FormGroup;
  EditForm4: FormGroup;
  AddForm: FormGroup;
  selectpeople: Array<any>
  selectministrypeople: Array<any>
  selectdepartmentpeople: Array<any>
  selectprovincialdepartmentpeople: Array<any>
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
  selectdataministrypeople: Array<any> = []
  ministryPeople: any = [];
  selectdatadepartmentpeople: Array<any>
  selectdataprovincialdepartmentpeople: Array<any>
  departmentPeople: any = [];
  provincialdepartmentPeople: any = [];
  selectdatapeople: Array<any>
  userPeople: any = [];
  fileStatus = false;
  form: FormGroup;
  FormQuestion: FormGroup;
  questionpeople: any = [];
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
  ministryId
  temp = []
  resultdsubjectid: any = []
  editAnswerForm: FormGroup;
  answer: any;
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
  role10Count: any = 0;
  role9Count: any = 0;
  timelineData: any = [];
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

    // [{ data: [[0, 6], [3, 6]], label: ["'ชื่อหน่วยงาน':'hello'", "'ชื่อหน่วยงาน':'hello2222'"] }]

    // { data: [6], label: 'นิก' },
    // { data: [10], label: 'ปาล์ม' }


    // {
    //   label: 'Dataset 1',
    //   backgroundColor: window.chartColors.red,
    //   data: [
    //     randomScalingFactor(),
    //     randomScalingFactor(),
    //     randomScalingFactor(),
    //     randomScalingFactor(),
    //     randomScalingFactor(),
    //     randomScalingFactor(),
    //     randomScalingFactor()
    //   ]
    // }
  }
  planId: any;
  userProvince: any[] = []
  listfiles: any = [];
  fileData: any = [{ ebookFile: '', fileDescription: '' }];
  watch

  get f() { return this.form.controls }
  get s() { return this.f.fileData as FormArray }

  constructor(private fb: FormBuilder,
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
    private inspectionplanservice: InspectionplanService,
    @Inject('BASE_URL') baseUrl: string) {
    this.id = activatedRoute.snapshot.paramMap.get('result')
    this.downloadUrl = baseUrl + '/Uploads';
    this.urllink = baseUrl + 'answersubject/outsider/';
    this.planId = activatedRoute.snapshot.paramMap.get('planId')
    this.watch = activatedRoute.snapshot.paramMap.get('watch')
  }

  async ngOnInit() {

    // this.graph();
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        // this.role_id = result.role_id
        // console.log(result);
        // alert(this.role_id)

        this.userService.getuserfirstdata(this.userid)
          .subscribe(result => {
            var result2 = result[0]
            this.userProvince = result2.userProvince
            // this.resultuser = result;
            //console.log("test" , this.resultuser);
            console.log("xxxxxxx", result2.userProvince);

            this.role_id = result[0].role_id
            this.ministryId = result[0].ministryId
            // alert(this.role_id)
          })
      })
    // alert(this.planId)
    console.log("ID: ", this.id);

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
      // questionPeople: new FormControl(null, [Validators.required]),
      signatureFiles: [null],
      description: new FormControl(null, [Validators.required]),
      fileType: new FormControl("เลือกประเภทเอกสารแนบ", [Validators.required]),
      fileData: new FormArray([]),
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

    this.FormQuestion = this.fb.group({
      notificationdate: new FormControl(null, [Validators.required]),
      deadlinedate: new FormControl(null, [Validators.required]),
      question: new FormControl(null, [Validators.required]),
    })


    // this.AddForm = this.fb.group({
    //   name: new FormControl(null, [Validators.required]),
    //   // centralpolicydateid: new FormControl(null, [Validators.required]),
    //   status: new FormControl("ใช้งานจริง่", [Validators.required]),
    //   inputsubjectdepartment: this.fb.array([
    //     // this.initdepartment()
    //   ]),
    // })

    this.inspectionplanservice.getTimeline(this.planId).subscribe(res => {
      this.timelineData = res.timelineData;
    })
    // this.getDetailCentralPolicy()
    this.getCentralPolicyProvinceUser()
    this.getDetailCentralPolicyProvince()
    // this.getquestion();

    // this.getMinistryPeople();
    // this.getUserPeople();
    this.getDepartmentPeople();
    this.getAnswer2();

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
      // scales: { // แสดง scales ของแผนภูมิเริมที่ 0
      //    yAxes: [{
      //       ticks:{
      //          beginAtZero:true
      //       }
      //    }]
      //  }
    })
  }
  initdepartment() {
    return this.fb.group({
      departmentId: [null, [Validators.required, Validators.pattern('[0-9]{3}')]],
      inputquestionopen: this.fb.array([
        this.initquestionopen()
      ]),
      inputquestionclose: this.fb.array([
        // this.initquestionclose()
      ])
    })
  }
  initquestionopen() {
    return this.fb.group({
      questionopen: [null, [Validators.required, Validators.pattern('[0-9]{3}')]]

      // initdepartment() {
      //   return this.fb.group({
      //     departmentId: [null, [Validators.required, Validators.pattern('[0-9]{3}')]],
      //     inputquestionopen: this.fb.array([
      //       this.initquestionopen()
      //     ]),
      //     inputquestionclose: this.fb.array([
      //       this.initquestionclose()
      //     ])
      //   })
      // }
      // initquestionopen() {
      //   return this.fb.group({
      //     questionopen: [null, [Validators.required, Validators.pattern('[0-9]{3}')]]

    })
  }
  // }
  // initquestionclose() {
  //   return this.fb.group({
  //     questionclose: [null, [Validators.required, Validators.pattern('[0-9]{3}')]],
  //     inputanswerclose: this.fb.array([
  //       this.initanswerclose()
  //     ])
  //   });
  // }
  // initanswerclose() {
  //   return this.fb.group({
  //     answerclose: [null, [Validators.required, Validators.pattern('[0-9]{3}')]],
  //   })
  // }

  openModaldelcenuser(template: TemplateRef<any>, id) {
    this.delid = id;
    this.modalRef = this.modalService.show(template);
  }
  openModalforwarddetail(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  async openModal(template: TemplateRef<any>) {

    this.Form.reset();

    this.modalRef = this.modalService.show(template);
    this.getDepartmentPeople();
    // this.getMinistryPeople();
    this.getProvincialDepartmentPeople();
    this.getUserPeople();
    this.getDepartmentdata();
  }
  openModal2(template: TemplateRef<any>, subjectid) {
    this.subjectid = subjectid
    this.departmentService.getalldepartdata().subscribe(res => {
      this.department = res.map((item, index) => {
        return {
          value: item.id,
          label: item.name
        }
      })
      this.modalRef = this.modalService.show(template);
    })
  }

  openModal3(template: TemplateRef<any>, subjectid) {
    this.subjectid = subjectid

    this.userservice.getuserdata(7).subscribe(result => {
      // alert(JSON.stringify(result))
      this.peopleanswer = result.map((item, index) => {
        return {
          value: item.id,
          label: item.name
        }
      })
      this.modalRef = this.modalService.show(template);
    })

  }

  editModal(template: TemplateRef<any>, id, name) {
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
    // barchartAllset: any = {
    //   label: ['คำตอบ', 'ตัวเลือก', '2008', '2009', '2010', '2011', '2012'],
    //   barChartData: [
    //     { data: [65, 59, 80, 81, 56, 55, 40], label: 'หน่วยงาน A', stack: 'a' },
    //     { data: [28, 48, 40, 19, 86, 27, 90], label: 'หน่วยงาน B', stack: 'b' },
    //     { data: [30, 40, 20, 12, 33, 23, 50], label: 'หน่วยงาน c', stack: 'c' },
    //   ]
    console.log('id', id);
    var barchartAllset: any
    // console.log("showGraph", item.subquestionChoiceCentralPolicyProvinces);
    var dataE: Array<any> = item.subquestionChoiceCentralPolicyProvinces
      .map(element => console.log('element', this.chartLabel(element))
      )
    // console.log("element", dataE);
    // function
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
  // getDetailCentralPolicy() {
  //   this.centralpolicyservice.getdetailcentralpolicydata(this.id)
  //     .subscribe(result => {
  //       // this.resultdetailcentralpolicy = result
  //     })
  // }

  getDetailCentralPolicyProvince() {
    this.centralpolicyservice.getdetailcentralpolicyprovincedata(this.id)
      .subscribe(result => {
        console.log("123", result);
        // alert(JSON.stringify(result))
        this.resultdetailcentralpolicy = result.centralpolicydata


        this.resultdate = result.centralPolicyEventdata.inspectionPlanEvent
        this.provincename = result.provincedata.name
        this.provinceid = result.provincedata.id
        this.resultuser = result.userdata
        this.electronicbookid = result.centralPolicyEventdata.electronicBookId

        // this.resultdetailcentralpolicyprovince = result.subjectcentralpolicyprovincedata
        this.centralpolicyprovincedata = result.centralpolicyprovince

        // this.form.patchValue({
        //   questionPeople: this.centralpolicyprovincedata.questionPeople,
        //   status: this.centralpolicyprovincedata.status
        // })
        // alert(this.centralpolicyprovincedata.step)
        // if (this.resultdetailcentralpolicyprovince.length > 0) {
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
        // } else {
        //   alert("else")
        // }
        this.resultdetailcentralpolicyprovince.forEach(element => {
          var subquestionCentralPolicyProvinces: any[] = element.subquestionCentralPolicyProvinces
          var subquestionChoicef: any[] = []

          subquestionCentralPolicyProvinces
            .filter(element2 => {
              return element2.type == "คำถามปลายปิด"
            })
            .forEach(result => {
              var data: any[] = result.subquestionChoiceCentralPolicyProvinces
              var data2: any[]
              var dataanswer: any[] = result.answerSubquestions
              var dataanswer2: any[]
              // data.push()
              data2 = data.map(result => {
                return result.name
              })
              console.log("result", result);

              dataanswer2 = dataanswer.map(result => {
                return result.answer
              })
              // barchartAllset: any = {
              //   label: ['แมวคือไร'],
              //   barChartData: [
              //     { data: [6], label: 'นิก' },
              //     { data: [10], label: 'ปาล์ม' }
              //   ]
              // }
              var reasult1 = {}
              dataanswer2.forEach(
                function (x) {
                  reasult1[x] = (reasult1[x] || 0) + 1;
                  // console.log(x);
                });
              // console.log("reasult1", reasult1);

              this.subquestionChoice.push(
                { question: data2, answer: dataanswer2.length == 0 ? 0 : dataanswer2, data: Object.values(reasult1) }
              )

              // this.subquestionChoice.
              // this.subquestionChoice.push(resultdata.name)
              // data.forEach(resultdata => {
              //   this.subquestionChoice.push(resultdata.name)
              // })
              //  result
            })
          // const count = this.subquestionChoice.reduce((acc, cur) => cur.id === id ? ++acc : acc, 0);
          // this.subquestionChoice.forEach(element => {
          //   console.log("in");
          //   element.data.forEach(element2 => {
          //     console.log("element2", element2);
          //     if (element.data = null) {
          //       console.log("in2");

          //     }
          //   })

          // })
          console.log(this.subquestionChoice);

          // subquestionCentralPolicyProvinces
          //   .filter(element2 => {
          //     return element2.type == "คำถามปลายปิด"
          //   })
          //   .forEach(result => {
          //     var dataanswer: any[] = result.answerSubquestions
          //     var dataanswer2: any[]
          //     // data.push()
          //     dataanswer2 = dataanswer.map(result => {
          //       return result.answer
          //     })
          //     this.answerSubquestions.push(
          //       dataanswer2
          //     )
          //     // this.subquestionChoice.push(resultdata.name)
          //     // data.forEach(resultdata => {
          //     //   this.subquestionChoice.push(resultdata.name)
          //     // })
          //     //  result
          //   })

          // var subquestionChoiceCentralPolicyProvinces = subquestionChoicef.subquestionChoiceCentralPolicyProvinces
          // subquestionChoicef.forEach(item => {
          //   this.subquestionChoice.push(item.name)
          // })
          // subquestionChoiceCentralPolicyProvinces
          // this.subquestionChoice.push(
          //   subquestionChoicef
          // )


          // this.subquestionChoice = this.subquestionChoice.push(subquestionChoicef)
          // this.subquestionChoice = this.subquestionChoice
          // element.subquestionCentralPolicyProvinces.forEach(element2 => {
          //   if (element2.type == "คำถามปลายปิด")
          //     this.subquestionChoice.push(element2.subquestionChoiceCentralPolicyProvinces)
          // })
          // this.subquestionCentralPolicyProvinces.push(element.subquestionCentralPolicyProvinces)
        })
        console.log('subquestionChoice', this.subquestionChoice);

        // console.log("subquestionChoice", subquestionChoice);

        this.getCalendarFile();
      })
  }

  getCentralPolicyProvinceUser() {
    this.centralpolicyservice.getcentralpolicyprovinceuserdata(this.id, this.planId)
      .subscribe(result => {
        console.log();

        this.resultcentralpolicyuser = result
        this.resultcentralpolicyuser.forEach(element => {
          if (element.user.role_id == 7) {
            this.role7Count = 1
          }
          if (element.user.role_id == 6) {
            // var checked = _.filter(element.user.userProvince, (v) => _.includes(this.userProvince.map(result => { return result.provinceId }), v.provinceId)).length
            // console.log("role6666",);
            // checked > 0 ? this.role6Count = 1 :null
            // alert(checked)
            // if (checked > 0) {
            this.role6Count = 1
            // }
          }
          if (element.user.role_id == 9) {
            this.role9Count = 1
          }
          if (element.user.role_id == 10) {
            this.role10Count = 1
          }
        });
        // console.log("result" + result);
      })

  }


  storeFiles(value) {
    // alert(JSON.stringify(value))
    this.electronicBookService.addElectronicBookFileFromCalendar(value, this.form.value.files, this.planId, this.id, this.form.value.signatureFiles).subscribe(response => {
      console.log(value);
      this.Form.reset()
      // this.router.navigate(['inspectionplanevent'])
      // console.log("get");
      // this.notificationService.addNotification(this.resultdetailcentralpolicy.id, this.provinceid, this.userid, 4, 1)
      //   .subscribe(response => {
      //     console.log(response);
      //   })

      window.history.back();

      // this.spinner.show();
      setTimeout(() => {
        this.getCalendarFile();
        this.form.reset();
        // this.spinner.hide();
      }, 300);


    })
  }

  storePeople(value: any) {
    let UserPeopleId: any[] = value.UserPeopleId
    // alert(JSON.stringify(value))
    this.centralpolicyservice.addCentralpolicyUser(value, this.id, this.userid, this.planId).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()

      if (this.timelineData.status == "ใช้งานจริง") {
        for (let i = 0; i < UserPeopleId.length; i++) {
          this.notificationService.addNotification(this.resultdetailcentralpolicy.id, this.provinceid, UserPeopleId[i], 1, this.planId, null)
            .subscribe(response => {
              console.log(response);
            })
        }
      }
      // for (let i = 0; i < UserPeopleId.length; i++) {
      //   this.notificationService.addNotification(this.resultdetailcentralpolicy.id, this.provinceid, UserPeopleId[i], 1, 1)
      //     .subscribe(response => {
      //       console.log(response);

      //     })
      // }

      this.getCentralPolicyProvinceUser();
    })
  }


  storeDepartment(value) {
    // alert(this.subjectid)
    this.centralpolicyservice.addDepartment(value, this.subjectid).subscribe(response => {
      console.log(value);
      this.Form2.reset()
      this.modalRef.hide()
      this.getDetailCentralPolicyProvince();
    })
  }

  storepeopleanswer(value) {
    let UserPeopleanswerId: any[] = value.peopleanswer
    // alert(this.subjectid)
    this.centralpolicyservice.addPeopleAnswer(value, this.subjectid).subscribe(response => {
      console.log(value);
      this.Form3.reset()
      this.modalRef.hide()


      // for (let i = 0; i < UserPeopleanswerId.length; i++) {
      //   this.notificationService.addNotification(this.resultdetailcentralpolicy.id, this.provinceid, UserPeopleanswerId[i], 5, 1)
      //     .subscribe(response => {
      //       console.log(response);

      //     })
      // }

      this.getDetailCentralPolicyProvince();
    })
  }

  storeMinistryPeople(value: any) {
    let UserPeopleId: any[] = value.UserPeopleId
    this.centralpolicyservice.addCentralpolicyUser(value, this.id, this.userid, this.planId).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      // for (let i = 0; i < UserPeopleId.length; i++) {
      //   this.notificationService.addNotification(this.resultdetailcentralpolicy.id, this.provinceid, UserPeopleId[i], 1, 1)
      //     .subscribe(response => {
      //       console.log(response);
      //     })
      // }
      this.getCentralPolicyProvinceUser();
    })
  }

  storeDepartmentPeople(value: any) {
    let UserPeopleId: any[] = value.UserPeopleId
    this.centralpolicyservice.addCentralpolicyUser(value, this.id, this.userid, this.planId).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()

      if (this.timelineData.status == "ใช้งานจริง") {
        for (let i = 0; i < UserPeopleId.length; i++) {
          this.notificationService.addNotification(this.resultdetailcentralpolicy.id, this.provinceid, UserPeopleId[i], 1, this.planId, null)
            .subscribe(response => {
              console.log(response);
            })
        }
      }
      // for (let i = 0; i < UserPeopleId.length; i++) {
      //   this.notificationService.addNotification(this.resultdetailcentralpolicy.id, this.provinceid, UserPeopleId[i], 1, 1)
      //     .subscribe(response => {
      //       console.log(response);
      //     })
      // }
      this.getCentralPolicyProvinceUser();
    })
  }

  editsubquestionclose(value, id) {
    this.subjectservice.editsubquestionprovince(value, id).subscribe(response => {
      console.log(value);
      this.EditForm.reset()
      this.modalRef.hide()
      this.getDetailCentralPolicyProvince();
    })
  }

  editsubquestionclosechoice(value, id) {
    this.subjectservice.editsubquestionchoiceprovince(value, id).subscribe(response => {
      console.log(value);
      this.EditForm2.reset()
      this.modalRef.hide()
      this.getDetailCentralPolicyProvince();
    })
  }

  editsubject(value, id) {
    this.subjectservice.editsubjectchoiceprovince(value, id).subscribe(response => {
      console.log(value);
      this.EditForm3.reset()
      this.modalRef.hide()
      this.getDetailCentralPolicyProvince();
    })
  }

  editsubjectquestionopen(value, id) {
    this.subjectservice.editsubjectquestionopenchoiceprovince(value, id).subscribe(response => {
      console.log(value);
      this.EditForm4.reset()
      this.modalRef.hide()
      this.getDetailCentralPolicyProvince();
    })
  }

  editAnswer(value, id) {
    this.subjectservice.editAnswer(value, id).subscribe(response => {
      console.log(value);
      this.editAnswerForm.reset()
      this.modalRef.hide()
      this.getDetailCentralPolicyProvince();
    })
  }

  // async getMinistryPeople() {
  //   // alert("123")
  //   await this.userservice.getuserdata(6).subscribe(result => {
  //     this.resultministrypeople = result // All
  //   })

  //   await this.centralpolicyservice.getcentralpolicyprovinceuserdata(this.id, this.planId).subscribe(async result => {
  //     await result.forEach(async element => {
  //       if (element.user.role_id == 6) {

  //         // var checked = _.filter(element.user.userProvince, (v) => _.includes(this.userProvince.map(result => { return result.provinceId }), v.provinceId)).length
  //         // alert(checked)
  //         // if (checked > 0) {
  //         // alert("123444")
  //         await this.allMinistryPeople.push(element.user)
  //         // }
  //       }
  //     }); // Selected
  //     // console.log("selectedMinistry: ", this.allMinistryPeople);
  //     this.getRecycledMinistryPeople();
  //   })
  // }

  // async getRecycledMinistryPeople() {
  //   this.selectdataministrypeople = []
  //   this.ministryPeople = this.allMinistryPeople
  //   console.log("MINISTRY: ", this.ministryPeople);
  //   console.log("allMinistry: ", this.resultministrypeople);
  //   console.log("this.userProvince", this.userProvince);

  //   if (this.ministryPeople.length == 0) {
  //     for (var i = 0; i < this.resultministrypeople.length; i++) {
  //       if (this.role_id == 3) {
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
  //         await this.selectdataministrypeople.push({ value: this.resultministrypeople[i].id, label: this.resultministrypeople[i].ministries.name + " - " + this.resultministrypeople[i].name + " เขต " + userregion  })
  //       } else {
  //         var checked = _.filter(this.resultministrypeople[i].userProvince, (v) => _.includes(this.userProvince.map(result => { return result.provinceId }), v.provinceId)).length
  //         // alert(checked)
  //         if (checked > 0) {
  //           var userregion = "";
  //           for (var j = 0; j < this.resultministrypeople[i].userRegion.length; j++) {

  //             if(this.resultministrypeople[i].userRegion[j].region.name == "เขตตรวจราชส่วนกลาง"){
  //               this.resultministrypeople[i].userRegion[j].region.name = "ส่วนกลาง"
  //             } else {
  //               this.resultministrypeople[i].userRegion[j].region.name = this.resultministrypeople[i].userRegion[j].region.name.replace('เขตตรวจราชการที่','');
  //             }

  //             if (j == (this.resultministrypeople[i].userRegion.length - 1)) {
  //               userregion += this.resultministrypeople[i].userRegion[j].region.name
  //             } else {
  //               userregion += this.resultministrypeople[i].userRegion[j].region.name + ", "
  //             }
  //           }
  //           await this.selectdataministrypeople.push({ value: this.resultministrypeople[i].id, label: this.resultministrypeople[i].ministries.name + " - " + this.resultministrypeople[i].name + " เขต " + userregion  })
  //         }
  //       }
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
  //         if (this.role_id == 3) {
  //           var userregion = "";
  //           for (var j = 0; j < this.resultministrypeople[i].userRegion.length; j++) {

  //             if(this.resultministrypeople[i].userRegion[j].region.name == "เขตตรวจราชส่วนกลาง"){
  //               this.resultministrypeople[i].userRegion[j].region.name = "ส่วนกลาง"
  //             } else {
  //               this.resultministrypeople[i].userRegion[j].region.name = this.resultministrypeople[i].userRegion[j].region.name.replace('เขตตรวจราชการที่','');
  //             }

  //             if (j == (this.resultministrypeople[i].userRegion.length - 1)) {
  //               userregion += this.resultministrypeople[i].userRegion[j].region.name
  //             } else {
  //               userregion += this.resultministrypeople[i].userRegion[j].region.name + ", "
  //             }
  //           }
  //           await this.selectdataministrypeople.push({ value: this.resultministrypeople[i].id, label: this.resultministrypeople[i].ministries.name + " - " + this.resultministrypeople[i].name + " เขต " + userregion  })
  //         } else {
  //           var checked = _.filter(this.resultministrypeople[i].userProvince, (v) => _.includes(this.userProvince.map(result => { return result.provinceId }), v.provinceId)).length
  //           // alert(checked)
  //           if (checked > 0) {
  //             var userregion = "";
  //             for (var j = 0; j < this.resultministrypeople[i].userRegion.length; j++) {

  //               if(this.resultministrypeople[i].userRegion[j].region.name == "เขตตรวจราชส่วนกลาง"){
  //                 this.resultministrypeople[i].userRegion[j].region.name = "ส่วนกลาง"
  //               } else {
  //                 this.resultministrypeople[i].userRegion[j].region.name = this.resultministrypeople[i].userRegion[j].region.name.replace('เขตตรวจราชการที่','');
  //               }

  //               if (j == (this.resultministrypeople[i].userRegion.length - 1)) {
  //                 userregion += this.resultministrypeople[i].userRegion[j].region.name
  //               } else {
  //                 userregion += this.resultministrypeople[i].userRegion[j].region.name + ", "
  //               }
  //             }
  //             await this.selectdataministrypeople.push({ value: this.resultministrypeople[i].id, label: this.resultministrypeople[i].ministries.name + " - " + this.resultministrypeople[i].name + " เขต " + userregion  })
  //           }
  //         }
  //       }
  //     }
  //   }
  //   // console.log("TEST: ", this.selectdataministrypeople);
  // }

  async getUserPeople() {
    await this.userservice.getuserdata(7).subscribe(result => {
      // alert(JSON.stringify(result))
      this.resultpeople = result
      // alert(JSON.stringify(this.resultpeople))
      console.log("tttt:", this.resultpeople);

    })
    await this.centralpolicyservice.getcentralpolicyprovinceuserdata(this.id, this.planId).subscribe(async result => {
      await result.forEach(async element => {
        if (element.user.role_id == 7) {
          this.allUserPeople.push(element.user)
        }
      }); // Selected
      console.log("selectedUser: ", this.allUserPeople);
      this.getRecycledUserPeople();
    })
  }

  async getRecycledUserPeople() {
    var test: any = [];
    test = this.resultpeople;
    this.selectdatapeople = []
    this.userPeople = this.allUserPeople
    console.log("user: ", this.userPeople);
    console.log("allUser: ", this.resultpeople);

    if (this.userPeople.length == 0) {
      for (var i = 0; i < this.resultpeople.length; i++) {

        if (this.role_id == 3) {
          await this.selectdatapeople.push({ value: this.resultpeople[i].id, label: "ด้าน" + this.resultpeople[i].sides.name + " - " + this.resultpeople[i].name })
        } else {

          var checked = _.filter(this.resultpeople[i].userProvince, (v) => _.includes(this.userProvince.map(result => { return result.provinceId }), v.provinceId)).length
          if (checked > 0) {
            await this.selectdatapeople.push({ value: this.resultpeople[i].id, label: "ด้าน" + this.resultpeople[i].sides.name + " - " + this.resultpeople[i].name })
          }
        }
      }
    }
    else {
      for (var i = 0; i < this.resultpeople.length; i++) {
        var n = 0;
        for (var ii = 0; ii < this.userPeople.length; ii++) {
          if (this.resultpeople[i].id == this.userPeople[ii].id) {
            await n++;
          }
        }
        if (n == 0) {
          if (this.role_id == 3) {
            await this.selectdatapeople.push({ value: this.resultpeople[i].id, label: "ด้าน" + this.resultpeople[i].sides.name + " - " + this.resultpeople[i].name })
          } else {
            var checked = _.filter(this.resultpeople[i].userProvince, (v) => _.includes(this.userProvince.map(result => { return result.provinceId }), v.provinceId)).length
            if (checked > 0) {
              await this.selectdatapeople.push({ value: this.resultpeople[i].id, label: "ด้าน" + this.resultpeople[i].sides.name + " - " + this.resultpeople[i].name })
            }
          }
        }
      }
    }
    console.log("TEST: ", this.selectdatapeople);
  }

  async getDepartmentPeople() {
    await this.userservice.getuserdata(10).subscribe(result => {
      this.resultdepartmentpeople = result // All
    })

    await this.centralpolicyservice.getcentralpolicyprovinceuserdata(this.id, this.planId)
      .subscribe(async result => {
        await result.forEach(async element => {
          if (element.user.role_id == 10) {
            await this.alldepartmentPeople.push(element.user)
          }
        }); // Selected
        // console.log("selecteddepartment: ", this.alldepartmentPeople);
        this.getRecycledDepartmentPeople();
      })
  }

  async getRecycledDepartmentPeople() {
    this.selectdatadepartmentpeople = []
    this.departmentPeople = this.alldepartmentPeople
    console.log("department: ", this.departmentPeople);
    console.log("alldepartment: ", this.resultdepartmentpeople);
    if (this.departmentPeople.length == 0) {
      // alert("if")
      for (var i = 0; i < this.resultdepartmentpeople.length; i++) {
        if (this.role_id == 3) {
          var userregion = "";
          for (var j = 0; j < this.resultdepartmentpeople[i].userRegion.length; j++) {

            if(this.resultdepartmentpeople[i].userRegion[j].region.name == "เขตตรวจราชส่วนกลาง"){
              this.resultdepartmentpeople[i].userRegion[j].region.name = "ส่วนกลาง"
            } else {
              this.resultdepartmentpeople[i].userRegion[j].region.name = this.resultdepartmentpeople[i].userRegion[j].region.name.replace('เขตตรวจราชการที่','');
            }

            if (j == (this.resultdepartmentpeople[i].userRegion.length - 1)) {
              userregion += this.resultdepartmentpeople[i].userRegion[j].region.name
            } else {
              userregion += this.resultdepartmentpeople[i].userRegion[j].region.name + ", "
            }
          }
          await this.selectdatadepartmentpeople.push({ value: this.resultdepartmentpeople[i].id, label: this.resultdepartmentpeople[i].departments.name + " - " + this.resultdepartmentpeople[i].name + " เขต " + userregion })
        } else {
          var checked = _.filter(this.resultdepartmentpeople[i].userProvince, (v) => _.includes(this.userProvince.map(result => { return result.provinceId }), v.provinceId)).length
          if (checked > 0) {
            if (this.ministryId == this.resultdepartmentpeople[i].ministryId) {
              var userregion = "";
              for (var j = 0; j < this.resultdepartmentpeople[i].userRegion.length; j++) {

                if(this.resultdepartmentpeople[i].userRegion[j].region.name == "เขตตรวจราชส่วนกลาง"){
                  this.resultdepartmentpeople[i].userRegion[j].region.name = "ส่วนกลาง"
                } else {
                  this.resultdepartmentpeople[i].userRegion[j].region.name = this.resultdepartmentpeople[i].userRegion[j].region.name.replace('เขตตรวจราชการที่','');
                }

                if (j == (this.resultdepartmentpeople[i].userRegion.length - 1)) {
                  userregion += this.resultdepartmentpeople[i].userRegion[j].region.name
                } else {
                  userregion += this.resultdepartmentpeople[i].userRegion[j].region.name + ", "
                }
              }
              await this.selectdatadepartmentpeople.push({ value: this.resultdepartmentpeople[i].id, label: this.resultdepartmentpeople[i].departments.name + " - " + this.resultdepartmentpeople[i].name + " เขต " + userregion })
            }
          }
        }
      }
    }
    else {
      // alert("else")
      for (var i = 0; i < this.resultdepartmentpeople.length; i++) {
        var n = 0;
        for (var ii = 0; ii < this.departmentPeople.length; ii++) {
          if (this.resultdepartmentpeople[i].id == this.departmentPeople[ii].id) {
            await n++;
          }
        }
        if (n == 0) {
          if (this.role_id == 3) {
            var userregion = "";
            for (var j = 0; j < this.resultdepartmentpeople[i].userRegion.length; j++) {

              if(this.resultdepartmentpeople[i].userRegion[j].region.name == "เขตตรวจราชส่วนกลาง"){
                this.resultdepartmentpeople[i].userRegion[j].region.name = "ส่วนกลาง"
              } else {
                this.resultdepartmentpeople[i].userRegion[j].region.name = this.resultdepartmentpeople[i].userRegion[j].region.name.replace('เขตตรวจราชการที่','');
              }

              if (j == (this.resultdepartmentpeople[i].userRegion.length - 1)) {
                userregion += this.resultdepartmentpeople[i].userRegion[j].region.name
              } else {
                userregion += this.resultdepartmentpeople[i].userRegion[j].region.name + ", "
              }
            }
            await this.selectdatadepartmentpeople.push({ value: this.resultdepartmentpeople[i].id, label: this.resultdepartmentpeople[i].departments.name + " - " + this.resultdepartmentpeople[i].name + " เขต " + userregion })
          } else {
            var checked = _.filter(this.resultdepartmentpeople[i].userProvince, (v) => _.includes(this.userProvince.map(result => { return result.provinceId }), v.provinceId)).length
            if (checked > 0) {
              if (this.ministryId == this.resultdepartmentpeople[i].ministryId) {
                var userregion = "";
                for (var j = 0; j < this.resultdepartmentpeople[i].userRegion.length; j++) {

                  if(this.resultdepartmentpeople[i].userRegion[j].region.name == "เขตตรวจราชส่วนกลาง"){
                    this.resultdepartmentpeople[i].userRegion[j].region.name = "ส่วนกลาง"
                  } else {
                    this.resultdepartmentpeople[i].userRegion[j].region.name = this.resultdepartmentpeople[i].userRegion[j].region.name.replace('เขตตรวจราชการที่','');
                  }

                  if (j == (this.resultdepartmentpeople[i].userRegion.length - 1)) {
                    userregion += this.resultdepartmentpeople[i].userRegion[j].region.name
                  } else {
                    userregion += this.resultdepartmentpeople[i].userRegion[j].region.name + ", "
                  }
                }
                await this.selectdatadepartmentpeople.push({ value: this.resultdepartmentpeople[i].id, label: this.resultdepartmentpeople[i].departments.name + " - " + this.resultdepartmentpeople[i].name + " เขต " + userregion })
              }
            }
          }
        }
      }
    }
    // console.log("TEST: ", this.selectdatadepartmentpeople);
  }

  async getProvincialDepartmentPeople() {
    await this.userservice.getuserdata(9).subscribe(result => {
      this.resultprovincialdepartmentpeople = result // All
    })

    await this.centralpolicyservice.getcentralpolicyprovinceuserdata(this.id, this.planId)
      .subscribe(async result => {
        await result.forEach(async element => {
          if (element.user.role_id == 9) {
            await this.allprovincialdepartmentPeople.push(element.user)
          }
        }); // Selected
        // console.log("selectedprovincialdepartment: ", this.allprovincialdepartmentPeople);
        this.getRecycledProvincialDepartmentPeople();
      })
  }

  async getRecycledProvincialDepartmentPeople() {
    this.selectdataprovincialdepartmentpeople = []
    this.provincialdepartmentPeople = this.allprovincialdepartmentPeople
    console.log("provincialdepartment: ", this.provincialdepartmentPeople);
    console.log("allprovincialdepartment: ", this.resultprovincialdepartmentpeople);
    if (this.provincialdepartmentPeople.length == 0) {
      // alert("if")
      for (var i = 0; i < this.resultprovincialdepartmentpeople.length; i++) {

        if (this.role_id == 3) {
          await this.selectdataprovincialdepartmentpeople.push({ value: this.resultprovincialdepartmentpeople[i].id, label: this.resultprovincialdepartmentpeople[i].provincialDepartments.name + " - " + this.resultprovincialdepartmentpeople[i].name })
        } else {

          var checked = _.filter(this.resultprovincialdepartmentpeople[i].userProvince, (v) => _.includes(this.userProvince.map(result => { return result.provinceId }), v.provinceId)).length
          if (checked > 0) {
            await this.selectdataprovincialdepartmentpeople.push({ value: this.resultprovincialdepartmentpeople[i].id, label: this.resultprovincialdepartmentpeople[i].provincialDepartments.name + " - " + this.resultprovincialdepartmentpeople[i].name })
          }
        }
      }
    }
    else {
      // alert("else")
      for (var i = 0; i < this.resultprovincialdepartmentpeople.length; i++) {
        var n = 0;
        for (var ii = 0; ii < this.provincialdepartmentPeople.length; ii++) {
          if (this.resultprovincialdepartmentpeople[i].id == this.provincialdepartmentPeople[ii].id) {
            await n++;
          }
        }
        if (n == 0) {
          if (this.role_id == 3) {
            await this.selectdataprovincialdepartmentpeople.push({ value: this.resultprovincialdepartmentpeople[i].id, label: this.resultprovincialdepartmentpeople[i].provincialDepartments.name + " - " + this.resultprovincialdepartmentpeople[i].name })
          } else {
            var checked = _.filter(this.resultprovincialdepartmentpeople[i].userProvince, (v) => _.includes(this.userProvince.map(result => { return result.provinceId }), v.provinceId)).length
            if (checked > 0) {
              await this.selectdataprovincialdepartmentpeople.push({ value: this.resultprovincialdepartmentpeople[i].id, label: this.resultprovincialdepartmentpeople[i].provincialDepartments.name + " - " + this.resultprovincialdepartmentpeople[i].name })
            }
          }
        }
      }
    }
    // console.log("TEST: ", this.selectdatadepartmentpeople);
  }

  uploadFile(event) {
    this.fileStatus = true;
    const file = (event.target as HTMLInputElement).files;

    this.form.patchValue({
      files: file
    });
    console.log("fff:", this.form.value.files)
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
    console.log("listfiles: ", this.form.value);
    console.log("eiei: ", this.s.controls);


    this.form.patchValue({
      files: this.listfiles
    });

    // console.log("listfiles", this.Formfile.get('files'));
    // this.Formfile.get('files').updateValueAndValidity()
  }

  getCalendarFile() {
    this.electronicBookService.getCalendarFile(this.planId, this.id).subscribe(res => {
      this.carlendarFile = res.carlendarFile;
      this.signatureFile = res.signatureFile;
      console.log("calendarFile: ", res);

    })
  }

  deleteProvinceial(value) {
    this.subjectservice.deleteProvincial(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.loading = false

      this.getDetailCentralPolicyProvince();

    })
  }

  deletepeopleanswer(value) {
    this.subjectservice.deletePeopleanswer(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.loading = false

      this.getDetailCentralPolicyProvince();

    })
  }
  // addV() {
  //   const control = <FormArray>this.AddForm.controls['inputsubjectdepartment'];
  //   control.push(this.initdepartment());
  // }
  // addW(iv) {
  //   const control = (<FormArray>this.AddForm.controls['inputsubjectdepartment']).at(iv).get('inputquestionopen') as FormArray;
  //   control.push(this.initquestionopen());
  // }
  // addX(iv) {
  //   const control = (<FormArray>this.AddForm.controls['inputsubjectdepartment']).at(iv).get('inputquestionclose') as FormArray;
  //   control.push(this.initquestionclose());
  // }
  // addY(iv, ix) {
  //   const control = ((<FormArray>this.AddForm.controls['inputsubjectdepartment']).at(iv).get('inputquestionclose') as FormArray).at(ix).get('inputanswerclose') as FormArray;
  //   control.push(this.initanswerclose());
  // }
  // remove(index: number) {
  //   this.d.removeAt(index);
  // }
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
      console.log(value);
      this.modalRef.hide()
      this.loading = false
      this.getDetailCentralPolicyProvince();
    })
  }
  deletequestion(value) {
    this.subjectservice.deletequestionrole3(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.loading = false
      this.getDetailCentralPolicyProvince();
    })
  }
  deleteoption(value) {
    this.subjectservice.deleteoptionrole3(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.loading = false
      this.getDetailCentralPolicyProvince();
    })
  }

  getDepartmentdata() {
    // this.resultprovince.forEach((element, index) => {
    //   console.log('element', element);

    this.departmentService.getalldepartdata()
      .subscribe(result => {
        this.temp = result.map((item, index) => {
          return {
            value: item.id,
            label: item.name,
          }
        })
        console.log(result);
      })
    // });
  }

  // storeSubject(value) {
  //   // this.spinner.show();
  //   console.log(value);
  //   this.subjectservice.addSubjectRole3(value, this.id).subscribe(response => {
  //     console.log("Response : ", response);
  //     this.resultdsubjectid.push(response.getSubjectID)
  //     response.termsList.forEach(element => {
  //       this.resultdsubjectid.push(element)
  //     });
  //     console.log("Response2 : ", this.resultdsubjectid);
  //     // this.storefiles();
  //     this.AddForm.reset();
  //     this.modalRef.hide();
  //     // this.spinner.hide();
  //     this.getDetailCentralPolicyProvince()
  //   })
  // }

  getAnswer2() {
    this.centralpolicyservice.getAnswer(this.id).subscribe(res => {
      this.answerData = res;
      console.log("answer: ", this.answerData);
    })
  }

  async openAnswerModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  checkType(type) {
    // alert(type)
    this.fileType = type;
  }
  storeQuestion(value) {
    console.log("storeQuestion", value);
    console.log("planID: ", this.planId);


    this.centralpolicyservice.addPeoplequestion(this.id, this.planId, value).subscribe(res => {
      this.FormQuestion.reset();
      this.modalRef.hide();
      // this.getquestion();
    })
  }
  getquestion() {
    this.centralpolicyservice.getquestionpeople(this.id, this.planId).subscribe(res => {
      this.questionpeople = res;
      console.log("question: ", this.questionpeople);
    })
  }

  delcentralpolicyuser(value) {
    // alert(id)
    this.centralpolicyservice.deletecentralpolicyuser(value).subscribe(response => {
      //console.log(value);
      // this.spinner.show();
      this.modalRef.hide()

      this.getCentralPolicyProvinceUser()

      // this.loading = false
      // this.getdata();
    })
  }
}
