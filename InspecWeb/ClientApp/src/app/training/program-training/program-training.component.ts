import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TrainingService } from '../../services/training.service';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from "ngx-spinner";

import { IMyDateModel, IMyOptions } from 'mydatepicker-th';
import * as moment from 'moment';
import { Chart } from 'chart.js';
import { NgbTimeStruct } from '@ng-bootstrap/ng-bootstrap';
import * as _ from 'lodash';

@Component({
  selector: 'app-program-training',
  templateUrl: './program-training.component.html',
  styleUrls: ['./program-training.component.css']
})
export class ProgramTrainingComponent implements OnInit {

  myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd/mm/yyyy',
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
  dtOptions: DataTables.Settings = {};
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
  constructor(private modalService: BsModalService,
    private fb: FormBuilder,
    private trainingservice: TrainingService,
    public share: TrainingService,
    private router: Router,
    private spinner: NgxSpinnerService,
    private activatedRoute: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string) {
    this.trainingid = activatedRoute.snapshot.paramMap.get('id')
    this.downloadUrl = baseUrl + '/Uploads'
  }

  ngOnInit() {


    this.spinner.show();
    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [0, 1, 2, 3, 4],
          orderable: false
        }
      ]

    };
    this.Form = this.fb.group({
      TrainingPhaseId: this.trainingid,
      programtype: new FormControl("", [Validators.required]),
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
      programtype: new FormControl("", [Validators.required]),
      programdate: new FormControl(null, [Validators.required]),
      mStart: new FormControl(null, [Validators.required]),
      mEnd: new FormControl(null, [Validators.required]),
      programtopic: new FormControl(null, [Validators.required]),
      programdetail: new FormControl(null, [Validators.required]),
      programlocation: new FormControl(null, [Validators.required]),
      programtodress: new FormControl(null, [Validators.required]),
      lecturername: new FormControl(null, [Validators.required]),
    })

    this.getprogramtraining()

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
        this.loading = true;
        //console.log(JSON.stringify(this.dateOption(this.startdate , this.enddate)));

        //console.log(this.resulttraining);
      })





    this.lineChart = new Chart('lineChart', { // สร้าง object และใช้ชื่อ id lineChart ในการอ้างอิงเพื่อนำมาเเสดงผล
      type: 'pie', // ใช้ชนิดแผนภูมิแบบเส้นสามารถเปลี่ยนชิดได้
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

  getprogramtraining() {
    //table program
    this.trainingservice.getprogramtraining(this.trainingid)
      .subscribe(result => {
        this.resulttraining = result
        this.loading = true
        console.log(result[0].trainingPhase.startDate);
        console.log(result[0].trainingPhase.endDate);

        this.dateOptionF(this.startdate, this.enddate)

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

    // moment(startDate).calendar()

    this.dateOption = {
      showTodayBtn: false,
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



  openModal(template: TemplateRef<any>, id) {
    this.delid = id;
    //console.log(this.delid);
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
    this.submitted = true;
    if (this.Form.invalid) {
      console.log("in1");
      return;
    } else {
      this.trainingservice.addTrainingProgram(value, this.Formfile.value.files).subscribe(response => {
        console.log("viewadddata:", value);
        this.Form.reset()
        this.modalRef.hide()
        this.loading = false;
        this.getprogramtraining();
        // this.trainingservice.getprogramtraining(this.trainingid)
        //   .subscribe(result => {
        //     this.resulttraining = result
        //     this.loading = true
        //     //console.log(this.resulttraining);
        //   })
      })
    }
  }

  editTraining(value) {
    console.log(value);
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
      this.getprogramtraining();
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
  get d() { return this.f.inputdate as FormArray }

  deleteTraining(value) {
    this.trainingservice.deleteTrainingProgram(value).subscribe(response => {
      //console.log(value);
      this.modalRef.hide()
      this.loading = false;
      this.trainingservice.getprogramtraining(this.trainingid)
        .subscribe(result => {
          this.resulttraining = result
          this.loading = true
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
  gotoBack() {
    window.history.back();
  }
}
