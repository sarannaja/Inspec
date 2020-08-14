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
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import * as Chart from 'chart.js';
import { SubquestionService } from 'src/app/services/subquestion.service';
import * as _ from 'lodash';
import { ReportService } from 'src/app/services/report.service';
@Component({
  selector: 'app-detail-subjectevent',
  templateUrl: './detail-subjectevent.component.html',
  styleUrls: ['./detail-subjectevent.component.css']
})
export class DetailSubjecteventComponent implements OnInit {

  departmentSelect: any[] = []
  subjectgroupstatus
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

  listfiles: any = [];
  fileData: any = [{ ebookFile: '', fileDescription: '' }];

  get f() { return this.form.controls }
  get s() { return this.f.fileData as FormArray }

  filterboxdepartments: any = []
  checkTypeReport: any;
  select = []
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
      questionPeople: new FormControl(null, [Validators.required]),
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

    this.AddForm = this.fb.group({
      name: new FormControl(null, [Validators.required]),
      // centralpolicydateid: new FormControl(null, [Validators.required]),
      status: new FormControl("ใช้งานจริง", [Validators.required]),
      inputsubjectdepartment: this.fb.array([
        // this.initdepartment()
      ]),
    })

    // this.FormReport = this.fb.group({
    //   type: new FormControl(null, [Validators.required]),
    //   title: new FormControl(null, [Validators.required]),
    //   name: new FormControl(null, [Validators.required]),
    //   explanation: new FormControl(null, [Validators.required]),

    // })
    await this.getDetailCentralPolicyProvince()
    await this.getsubjecteventDetail();
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


  async openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
    // await this.getMinistryPeople();
    // await this.getUserPeople();
    this.getDepartmentdata();
  }
  openModal2(template: TemplateRef<any>, subjectid, departmentSelected: any[] = []) {
    this.subjectid = subjectid
    this.departmentService.getalldepartdata().subscribe(res => {
      this.department = res.map((item, index) => {
        return {
          value: item.id,
          label: item.name
        }
      })

      console.log(this.department);
      var data: any[] = departmentSelected.map(result => {
        return result.provincialDepartment.id
      })

      this.departmentSelect = _.filter(this.department, (v) => !_.includes(
        data, v.value
      ))


      this.modalRef = this.modalService.show(template);
    })
  }

  openModal3(template: TemplateRef<any>, subjectid) {
    this.subjectid = subjectid
  }

  openAlertModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
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
    console.log('id', id);
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
    console.log("select",this.select);
    this.checkTypeReport = 0;
    this.modalRef = this.modalService.show(template);
  }
  report1(value) {
    // alert(myradio)
    this.checkTypeReport = 1;
  }
  report2(value) {
    // alert(myradio)
    this.checkTypeReport = 2;
  }
  report3(value) {
    // alert(myradio)
    this.checkTypeReport = 3;
  }
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
              data2 = data.map(result => {
                return result.name
              })
              console.log("result", result);

              dataanswer2 = dataanswer.map(result => {
                return result.answer
              })
              var reasult1 = {}
              dataanswer2.forEach(
                function (x) {
                  reasult1[x] = (reasult1[x] || 0) + 1;
                });

              this.subquestionChoice.push(
                { question: data2, answer: dataanswer2.length == 0 ? 0 : dataanswer2, data: Object.values(reasult1) }
              )

            })
          console.log(this.subquestionChoice);


        })
        console.log('subquestionChoice', this.subquestionChoice);
        this.getCalendarFile();
      })
  }
  getsubjecteventDetail() {
    this.centralpolicyservice.getSubjecteventdetaildata(this.id, this.subjectgroupid)
      .subscribe(result => {
        this.resultdetailcentralpolicyprovince = result.subjectcentralpolicyprovincedata
        this.subjectgroup = result.subjectgroup

        this.subjectgroupstatus = this.subjectgroup.status
        console.log("result", result);
        // alert(JSON.stringify(this.subjectgroup.status))
        this.form.patchValue({
          // questionPeople: this.centralpolicyprovincedata.questionPeople,
          status: this.subjectgroup.status
        })

      })
  }
  storeFiles(value) {
    // alert("123")
    // if(value.status == "ใช้งานจริง"){
    //   this.notificationService.addNotification(this.resultdetailcentralpolicy.id, this.provinceid, this.userid, 4, 1)
    //   .subscribe(response => {
    //     console.log(response);
    //   })
    // }
    this.electronicBookService.addSubjectEventFile(value, this.form.value.files, this.subjectgroupid, this.id, this.form.value.signatureFiles).subscribe(response => {
      console.log(value);
      this.Form.reset()

      if (value.status == "ใช้งานจริง") {
        this.notificationService.addNotification(this.resultdetailcentralpolicy.id, this.provinceid, this.userid, 4, 1)
          .subscribe(response => {
            console.log(response);
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
    this.centralpolicyservice.addDepartment(value, this.subjectid).subscribe(response => {
      console.log(value);
      this.Form2.reset()
      this.modalRef.hide()
      this.getsubjecteventDetail();
    })
  }

  storepeopleanswer(value) {
    let UserPeopleanswerId: any[] = value.peopleanswer
    this.centralpolicyservice.addPeopleAnswer(value, this.subjectid).subscribe(response => {
      console.log(value);
      this.Form3.reset()
      this.modalRef.hide()


      for (let i = 0; i < UserPeopleanswerId.length; i++) {
        this.notificationService.addNotification(this.resultdetailcentralpolicy.id, this.provinceid, UserPeopleanswerId[i], 5, 1)
          .subscribe(response => {
            console.log(response);

          })
      }

      this.getDetailCentralPolicyProvince();
    })
  }

  editsubquestionclose(value, id) {
    this.subjectservice.editsubquestionprovince(value, id).subscribe(response => {
      console.log(value);
      this.EditForm.reset()
      this.modalRef.hide()
      this.getDetailCentralPolicyProvince();

      this.getsubjecteventDetail();
      this.getAnswer2();
    })
  }

  editsubquestionclosechoice(value, id) {
    this.subjectservice.editsubquestionchoiceprovince(value, id).subscribe(response => {
      console.log(value);
      this.EditForm2.reset()
      this.modalRef.hide()
      this.getDetailCentralPolicyProvince();

      this.getsubjecteventDetail();
      this.getAnswer2();
    })
  }

  editsubject(value, id) {
    this.subjectservice.editsubjectchoiceprovince(value, id).subscribe(response => {
      console.log(value);
      this.EditForm3.reset()
      this.modalRef.hide()
      this.getDetailCentralPolicyProvince();

      this.getsubjecteventDetail();
      this.getAnswer2();
    })
  }

  editsubjectquestionopen(value, id) {
    this.subjectservice.editsubjectquestionopenchoiceprovince(value, id).subscribe(response => {
      console.log(value);
      this.EditForm4.reset()
      this.modalRef.hide()
      this.getDetailCentralPolicyProvince();

      this.getsubjecteventDetail();
      this.getAnswer2();
    })
  }

  editAnswer(value, id) {
    this.subjectservice.editAnswer(value, id).subscribe(response => {
      console.log(value);
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
  //   console.log("MINISTRY: ", this.ministryPeople);
  //   console.log("allMinistry: ", this.resultministrypeople);
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
  //   // console.log("TEST: ", this.selectdataministrypeople);
  // }

  // async getUserPeople() {
  //   await this.userservice.getuserdata(7).subscribe(result => {
  //     // alert(JSON.stringify(result))
  //     this.resultpeople = result
  //     // alert(JSON.stringify(this.resultpeople))
  //     console.log("tttt:", this.resultpeople);

  //   })
  // }

  // async getRecycledUserPeople() {
  //   var test: any = [];
  //   test = this.resultpeople;
  //   this.selectdatapeople = []
  //   this.userPeople = this.allUserPeople
  //   console.log("user: ", this.userPeople);
  //   console.log("allUser: ", this.resultpeople);

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
  //   console.log("TEST: ", this.selectdatapeople);
  // }

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
    this.electronicBookService.getSubjectEventFile(this.subjectgroupid, this.id).subscribe(res => {
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

      this.getsubjecteventDetail();

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
      this.getsubjecteventDetail();
    })
  }
  deletequestion(value) {
    this.subjectservice.deletequestionrole3(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.loading = false
      this.getsubjecteventDetail();
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
  }

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
  openAddModalQuestionsclose(template: TemplateRef<any>, subjectid) {
    console.log("subjectid:", subjectid);
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

  openModalSubject(template: TemplateRef<any>, subjectid) {
    this.departmentService.getalldepartdata().subscribe(res => {
      this.department = res.map((item, index) => {
        return {
          value: item.id,
          label: item.name
        }
      })
    })
    console.log("subjectid:", subjectid);
    this.modalRef = this.modalService.show(template);
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
    console.log(value);
    this.subquestionservice.addSubquestioncloseevent(value).subscribe(result => {
      console.log(result);
      this.FormAddQuestionsclose.reset()
      this.modalRef.hide()
      this.getsubjecteventDetail()
    })
  }

  storeSubject(value) {
    // this.spinner.show();
    console.log(value);
    this.subjectservice.addSubjectRole3(value).subscribe(response => {

      this.AddForm.reset();
      this.modalRef.hide();
      this.getDetailCentralPolicyProvince();
      this.getsubjecteventDetail();
    })
  }
  storeReport() {
    this.reportservice.createReportSubject(this.resultdetailcentralpolicy, this.resultdetailcentralpolicyprovince).subscribe(result => {
      console.log("export: ", result);
      window.open(this.downloadUrl + "/" + result.data);
      // this.modalRef.hide();
    })
  }
}
