import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TrainingService } from '../../services/training.service';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { IMyDateModel, IMyOptions } from 'mydatepicker-th';
import * as moment from 'moment';
import { Chart } from 'chart.js';
import { NgbTimeStruct } from '@ng-bootstrap/ng-bootstrap';
import * as _ from 'lodash';

import { NotofyService } from '../../services/notofy.service';
import { NgxSpinnerService } from "ngx-spinner";
import { LogService } from '../../services/log.service';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';

@Component({
  selector: 'app-program-training',
  templateUrl: './program-training.component.html',
  styleUrls: ['./program-training.component.css']
})
export class ProgramTrainingComponent implements OnInit {

  myDatePickerOptions: IMyOptions = {
    // other options...
    // dateFormat: 'dd/mm/yyyy',
    showClearDateBtn: false
  };
  trainingid: string
  resulttraining: any[] = []
  modalRef: BsModalRef;
  modalRef2: BsModalRef;
  delid: any
  programid: any
  name: any
  link: any
  loading = false;
  dtOptions: any = {};
  Form: FormGroup;
  EditForm: FormGroup;
  form: FormGroup;
  Formfile: FormGroup;
  // files: string[] = []
  downloadUrl: string;
  selectdatalecturer: Array<any>
  resultlecturer: any = []
  startdate: any;
  enddate: any;
  dateOption: IMyOptions
  resulttrainingdetail: any[];
  lineChart: any;
  submitted = false;

  lecturer: any = []
  oldlecturer: any = []
  removelecturer: any = []
  addlecturer: any = []
  files: any = []
  delfilesid: any
  resultprogramtype: any[];
  selectdataprogramtype: { value: any; label: any; }[];
  userid: any;
  programtopic: any;
  trainingidtrue: string;
  constructor(private modalService: BsModalService,
    private authorize: AuthorizeService,
    private _NotofyService: NotofyService,
    private spinner: NgxSpinnerService,
    private logService: LogService,
    private fb: FormBuilder,
    private trainingservice: TrainingService,
    public share: TrainingService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string) {
    this.trainingid = activatedRoute.snapshot.paramMap.get('id')
    this.trainingidtrue = activatedRoute.snapshot.paramMap.get('phaseid')
    this.downloadUrl = baseUrl + '/Uploads'
  }

  ngOnInit() {

    this.getuserinfo();
    this.spinner.show();
    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [0, 1, 2, 3, 4],
          orderable: false
        }
      ],
      "language": {
        "lengthMenu": "แสดง  _MENU_  รายการ",
        "search": "ค้นหา:",
        "infoFiltered": "ไม่พบข้อมูล",
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

    this.getprogramtraining()

    this.trainingservice.getTrainingProgramType()
    .subscribe(result => {
      this.resultprogramtype = result

      this.resultprogramtype = result
      if (this.resultprogramtype.length > 0){
        this.selectdataprogramtype = this.resultprogramtype.map((item, index) => {
          return { value: item.id, label: item.name }
        })
      }


    })

    this.Form = this.fb.group({
      TrainingPhaseId: this.trainingid,
      programtype: new FormControl(null, [Validators.required]),
      programdate: new FormControl(null, [Validators.required]),
      mStart: new FormControl(null, [Validators.required]),
      mEnd: new FormControl(null, [Validators.required]),
      programtopic: new FormControl(null, [Validators.required]),
      programdetail: new FormControl(null, [Validators.required]),
      programlocation: new FormControl(null, [Validators.required]),
      programtodress: new FormControl(null, [Validators.required]),
      lecturername: new FormControl(null, [Validators.required]),
    })
    this.Formfile = this.fb.group({
      files: [null],
    })
    this.EditForm = this.fb.group({
      programtype: new FormControl(null, [Validators.required]),
      programdate: new FormControl(null, [Validators.required]),
      mStart: new FormControl(null, [Validators.required]),
      mEnd: new FormControl(null, [Validators.required]),
      programtopic: new FormControl("", [Validators.required]),
      programdetail: new FormControl("", [Validators.required]),
      programlocation: new FormControl("", [Validators.required]),
      programtodress: new FormControl("", [Validators.required]),
      lecturername: new FormControl(null, [Validators.required]),
    })



    //dropdown lecturer
    this.trainingservice.gettraininglecturer()
      .subscribe(result => {
        this.resultlecturer = result
        console.log(this.resultlecturer);
        this.selectdatalecturer = this.resultlecturer.map((item, index) => {
          return { value: item.id, label: item.lecturerName }
        })
        console.log("123", this.selectdatalecturer);

      })


    this.trainingservice.getdetailtraining(this.trainingid)
      .subscribe(result => {

        if (result.length != 0) {
          this.startdate = result[0].startDate
          this.enddate = result[0].endDate
          this.dateOptionF(this.startdate, this.enddate)
          this.Form.patchValue({
            'programdate': this.startdate
          })
          console.log(this.startdate)
          console.log(this.enddate);
          console.log("test", this.dateOption);
          // this.dateOption(this.startdate , this.enddate)
        }
        this.resulttrainingdetail = result
        //this.loading = true;
        //console.log(JSON.stringify(this.dateOption(this.startdate , this.enddate)));

        //console.log(this.resulttraining);
      })





    // this.lineChart = new Chart('lineChart', { // สร้าง object และใช้ชื่อ id lineChart ในการอ้างอิงเพื่อนำมาเเสดงผล
    //   type: 'pie', // ใช้ชนิดแผนภูมิแบบเส้นสามารถเปลี่ยนชิดได้
    //   data: { // ข้อมูลภายในแผนภูมิแบบเส้น
    //     labels: ["Jan", "Feb", "March", "April", "May", "June", "July", "August", "Sep", "Oct", "Nov", "Dec"], // ชื่อของข้อมูลในแนวแกน x
    //     datasets: [{ // กำหนดค่าข้อมูลภายในแผนภูมิแบบเส้น
    //       label: 'Number of items sold in months',
    //       data: [9, 7, 3, 5, 2, 10, 15, 61, 19, 3, 1, 9],
    //       fill: false,
    //       lineTension: 0.2,
    //       borderColor: "red", // สีของเส้น
    //       borderWidth: 1
    //     }]
    //   },
    //   options: {
    //     title: { // ข้อความที่อยู่ด้านบนของแผนภูมิ
    //       text: "Bar Chart",
    //       display: true
    //     }
    //   },
    //   // scales: { // แสดง scales ของแผนภูมิเริมที่ 0
    //   //    yAxes: [{
    //   //       ticks:{
    //   //          beginAtZero:true
    //   //       }
    //   //    }]
    //   //  }
    // })

  }

  //start getuser
  getuserinfo() {
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
      })
  }

  getprogramtraining() {
    //table program
    this.trainingservice.getprogramtraining(this.trainingid)
      .subscribe(result => {
        this.resulttraining = result

        console.log(result);
        
        if (result.length > 0){
          console.log("startDate =>", result[0].trainingPhase.startDate);
          console.log("endDate =>", result[0].trainingPhase.endDate);

          this.dateOptionF(this.startdate, this.enddate)
        }
        
        this.loading = true;
        this.spinner.hide();
        //console.log(this.resulttraining);
      })
  }


  dateOptionF(start, end) {
    var startDate = new Date(start);
    var endDate = new Date(end);
    var dateDayRemoveStart = new Date()
    dateDayRemoveStart.setDate(startDate.getDay() - 1)
    

    var dateDayRemoveEnd = new Date()
    dateDayRemoveEnd.setDate(endDate.getDay() + 1)

    //console.log("dateDayRemoveStart => ", dateDayRemoveStart.getDay());
    
    // moment(startDate).calendar()

    this.dateOption = {
      showTodayBtn: false,
      //เกือบใช้ได้ ติดลบวันเหลือ 0
      // disableDateRanges: [
      // {
      //     begin:{
      //     year: new Date(start).getFullYear() - 100,
      //     month: new Date(start).getMonth() + 1,
      //     day: new Date(start).getDate()
      //   },
      //   end:{
      //     year: new Date(start).getFullYear(),
      //     month: new Date(start).getMonth() + 1,
      //     day: new Date(start).getDate() - 15
      //   }
      // }
      //   ,
      // {
      //   begin:{
      //     year: new Date(end).getFullYear(),
      //     month: new Date(end).getMonth() + 1,
      //     day: new Date(end).getDate() + 1
      //   },
      //   end:{
      //     year: new Date(end).getFullYear() + 100,
      //     month: new Date(end).getMonth() + 1,
      //     day: new Date(end).getDate()
      //   }
      // }]


      // disableUntil: {year: 2020, month: 5, day: 10},
      // disableDateRanges: [
      //   {
      //     end: {
      //       year: dateDayRemoveStart.getFullYear(),
      //       month: dateDayRemoveStart.getMonth(),
      //       day: (dateDayRemoveStart.getDay())
      //     },
      //     begin: {
      //       year: (startDate.getFullYear()) - 50,
      //       month: startDate.getMonth(),
      //       day: (startDate.getDay()),
      //     }
      //   },
      //   {
      //     end: {
      //       year: (endDate.getFullYear()) + 50,
      //       month: endDate.getMonth(),
      //       day: (endDate.getDay())
      //     },
      //     begin: {
      //       year: (dateDayRemoveEnd.getFullYear()),
      //       month: dateDayRemoveEnd.getMonth(),
      //       day: (dateDayRemoveEnd.getDay()),
      //     }
      //   },
      //   // {
      //   //   end: {
      //   //     year: new Date(endDate).getFullYear()+1000,
      //   //     month: new Date(endDate).getMonth(),
      //   //     day: new Date(endDate).getDay(),
      //   //   },
      //   //   begin:  {
      //   //     year: new Date(startDate).getFullYear(),
      //   //     month: new Date(startDate).getMonth(),
      //   //     day: new Date(startDate).getDay(),
      //   //   }
      //   // }
      // ]
    }

    // console.log("Start:" + start);
    // console.log("End:" + end);


    // var sss = (endDate - startDate) / 100000000
    // var mod = (endDate - startDate) % 100000000
    // var ssaa = parseInt(mod.toString().substr(1, 1))
    // var aass = ssaa <= 5 ? sss : sss + 1
    // return dateOption
  }



  openModal(template: TemplateRef<any>, id: any = null, programTopic: any = null) {
    this.submitted = false;
    this.delid = id;
    this.programtopic = programTopic;
    console.log("programtopic =>", this.programtopic);
    this.Form.patchValue({
      TrainingPhaseId: this.trainingid,
    })
    
    this.modalRef = this.modalService.show(template);
  }
  editModal(template: TemplateRef<any>, id, programtype, programdate, mStart, mEnd, programtopic, programdetail, programlocation, programtodress, lecturername: any[] = [], files) {
    console.log("123", lecturername);
    this.programid = id
    this.getprogramtrainingdetail()
    this.modalRef = this.modalService.show(template);
    // this.lecturer = lecturername.map(result => result.trainingLecturerId)
    // this.oldlecturer = lecturername.map(result => result.trainingLecturerId)
    // this.EditForm.patchValue({
    //   programtype: programtype,
    //   // programdate: programdate,
    //   programdate: {
    //     year: new Date(programdate).getFullYear(),
    //     month: new Date(programdate).getMonth() + 1,
    //     day: new Date(programdate).getDate()
    //   },
    //   mStart: mStart,
    //   mEnd: mEnd,
    //   programtopic: programtopic,
    //   programdetail: programdetail,
    //   programlocation: programlocation,
    //   programtodress: programtodress,
    //   lecturername: this.lecturer
    // })

    // this.files = files
  }
  getprogramtrainingdetail(){
    var lecturername: any[] = []
    this.trainingservice.getprogramtrainingdetail(this.programid).subscribe(result => {
      lecturername = result.trainingProgramLecturers
      this.lecturer = lecturername.map(result => result.trainingLecturerId)
      this.oldlecturer = lecturername.map(result => result.trainingLecturerId)
      this.EditForm.patchValue({
        programtype: result.programType,
        // programdate: programdate,
        programdate: {
          year: new Date(result.programDate).getFullYear(),
          month: new Date(result.programDate).getMonth() + 1,
          day: new Date(result.programDate).getDate()
        },
        mStart: result.minuteStartDate,
        mEnd: result.minuteEndDate,
        programtopic: result.programTopic,
        programdetail: result.programDetail,
        programlocation: result.programLocation,
        programtodress: result.programToDress,
        lecturername: this.lecturer
      })
      this.files = result.trainingProgramFiles
    })
  }
  get fe() { return this.EditForm }

  onDateChanged(event: IMyDateModel) {

    this.EditForm.patchValue({
      programdate: event.date
    })
  }

  openModaldelfiles(template: TemplateRef<any>, id) {
    this.delfilesid = id;
    this.modalRef2 = this.modalService.show(template);
  }

  storeTraining(value) {
    //alert(JSON.stringify(value))
    //alert(this.form.value.files)
    console.log("viewdata:", value);
    console.log(this.Formfile.value.files);
    
    // if (this.Form.invalid) {
    //   this.submitted = true;
    //   console.log("in1");
    //   return;
    // } else {
      console.log("viewadddata:", value);
      this.trainingservice.addTrainingProgram(value, this.Formfile.value.files).subscribe(response => {
        console.log("addTrainingProgram =>:", response);
        this.Form.reset()
        this.submitted = false;
        this.modalRef.hide();
        this.loading = false;
        this.logService.addLog(this.userid,'TrainingPrograms','เพิ่ม', response.programTopic,response.id).subscribe();
        this.getprogramtraining();
        this._NotofyService.onSuccess("เพิ่มข้อมูล")
        // this.trainingservice.getprogramtraining(this.trainingid)
        //   .subscribe(result => {
        //     this.resulttraining = result
        //     this.loading = true
        //     //console.log(this.resulttraining);
        //   })
      })
    // }
  }

  editTraining(value) {
    console.log(value);
    this.programtopic = value.programtopic
    console.log("Old: ", this.oldlecturer);
    console.log("New: ", this.lecturer);

    this.removelecturer = _.differenceBy(this.oldlecturer, this.lecturer);
    console.log("Remove Value => ", this.removelecturer);

    this.addlecturer = _.differenceBy(this.lecturer, this.oldlecturer);
    console.log("Add Value => ", this.addlecturer);
    this.trainingservice.editTrainingProgram(value, this.programid, this.removelecturer, this.addlecturer, this.Formfile.value.files).subscribe(result => {
      console.log(result);
      this.EditForm.reset()
      this.modalRef.hide()
      this.loading = false;
      this.logService.addLog(this.userid,'TrainingPrograms','แก้ไข', result.programTopic, result.id).subscribe();
      this.getprogramtraining();
      this._NotofyService.onSuccess("แก้ไขข้อมูล")
    })
  }
  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.Formfile.patchValue({
      files: file,
    });
    this.Formfile.get('files').updateValueAndValidity()
  }

  // addFile(event) {
  //   this.files = event.target.files
  // }
  get f() { return this.Form.controls }
  get fedit() { return this.EditForm.controls }
  get d() { return this.f.inputdate as FormArray }

  deleteTraining(value) {
    this.trainingservice.deleteTrainingProgram(value).subscribe(response => {
      //console.log(value);
      this.modalRef.hide()
      this.loading = false;
      this.logService.addLog(this.userid,'TrainingPrograms','ลบ', this.programtopic, this.delid).subscribe();
      this.trainingservice.getprogramtraining(this.trainingid)
        .subscribe(result => {
          this.resulttraining = result
          this.loading = true
          this._NotofyService.onSuccess("ลบข้อมูล")
          //console.log(this.resulttraining);
        })
    })
  }
  deletefiles(){
    this.trainingservice.deleteTrainingProgramFiles(this.delfilesid).subscribe(result => {
      this.modalRef2.hide();
      this.getprogramtrainingdetail()
    })
  }

  gotoMain(){
    this.router.navigate(['/main'])
  }

  gotoMainTraining(){
    this.router.navigate(['/training'])
  }

  gotoMainTrainingPhase(){
    this.router.navigate(['/training/phase/', this.trainingidtrue])
  }

  gotoBack() {
    window.history.back();
  }
}
