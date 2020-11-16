import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TrainingService } from '../services/training.service';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

import { IMyDateModel, IMyOptions } from 'mydatepicker-th';
import * as moment from 'moment';
import { Chart } from 'chart.js';

import { NotofyService } from '../services/notofy.service';
import { NgxSpinnerService } from "ngx-spinner";
import { LogService } from '../services/log.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';

@Component({
  selector: 'app-training-summary-report',
  templateUrl: './training-summary-report.component.html',
  styleUrls: ['./training-summary-report.component.css']
})
export class TrainingSummaryReportComponent implements OnInit {
  myDatePickerOptions: IMyOptions = {
    // other options...
    //dateFormat: 'dd/mm/yyyy',
    showClearDateBtn: false
  };

  trainingid: string
  resulttraining: any[] = []
  modalRef: BsModalRef;
  delid: any
  name: any
  link: any
  loading = false;
  dtOptions: DataTables.Settings = {};
  Form: FormGroup;
  EditForm: FormGroup;
  form: FormGroup;
  files: string[] = []
  downloadUrl: string;
  selectdatalecturer: Array<any>
  resultlecturer: any = []
  startdate: any;
  enddate: any;
  dateOption: IMyOptions
  resulttrainingdetail: any[];
  lineChart: any;
  // test: any = [];
  submitted = false;
  editid: any;
  userid: string;
  groupnum = 6;

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
    this.trainingid = activatedRoute.snapshot.paramMap.get('trainingid')
    this.downloadUrl = baseUrl + '/Uploads'
  }

  ngOnInit() {
    this.getuserinfo();
    this.spinner.show();
    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [0, 1, 2, 3],
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
    this.EditForm = this.fb.group({
      "phaseno": new FormControl(null, [Validators.required]),
      "startdate": new FormControl(null, [Validators.required]),
      "enddate": new FormControl(null, [Validators.required]),
      "title": new FormControl(null, [Validators.required]),
      "detail": new FormControl(null, [Validators.required]),
      "location": new FormControl(null, [Validators.required]),
      "group": new FormControl(null, [Validators.required]),
    })

    this.Form = this.fb.group({
      phaseno: new FormControl(null, [Validators.required]),
      title: new FormControl(null, [Validators.required]),
      detail: new FormControl(null, [Validators.required]),
      startdate: new FormControl(null, [Validators.required]),
      enddate: new FormControl(null, [Validators.required]),
      location: new FormControl(null, [Validators.required]),
      group: new FormControl(null, [Validators.required]),
    })

    this.getTrainingPhase()


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
          console.log(this.dateOption);
          // this.dateOption(this.startdate , this.enddate)
        }
        this.resulttrainingdetail = result
        this.loading = true;
        //console.log(JSON.stringify(this.dateOption(this.startdate , this.enddate)));

        //console.log(this.resulttraining);
      })


  }
  getTrainingPhase() {
    this.trainingservice.getTrainingPhase(this.trainingid)
      .subscribe(result => {
        this.resulttraining = result;
        this.loading = true;
        this.spinner.hide();
        console.log(this.resulttraining);
      })
  }

  


  dateOptionF(start, end) {
    //console.log("dateOptionF =>", start, end);
    
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
      disableDateRanges: [
        {
          end: {
            year: dateDayRemoveStart.getFullYear(),
            month: dateDayRemoveStart.getMonth(),
            day: (dateDayRemoveStart.getDay())
          },
          begin: {
            year: (startDate.getFullYear()) - 50,
            month: startDate.getMonth(),
            day: (startDate.getDay()),
          }
        },
        {
          end: {
            year: (endDate.getFullYear()) + 50,
            month: endDate.getMonth(),
            day: (endDate.getDay())
          },
          begin: {
            year: (dateDayRemoveEnd.getFullYear()),
            month: dateDayRemoveEnd.getMonth(),
            day: (dateDayRemoveEnd.getDay()),
          }
        },
        // {
        //   end: {
        //     year: new Date(endDate).getFullYear()+1000,
        //     month: new Date(endDate).getMonth(),
        //     day: new Date(endDate).getDay(),
        //   },
        //   begin:  {
        //     year: new Date(startDate).getFullYear(),
        //     month: new Date(startDate).getMonth(),
        //     day: new Date(startDate).getDay(),
        //   }
        // }
      ]
    }

    // console.log("Start:" + start);
    // console.log("End:" + end);


    // var sss = (endDate - startDate) / 100000000
    // var mod = (endDate - startDate) % 100000000
    // var ssaa = parseInt(mod.toString().substr(1, 1))
    // var aass = ssaa <= 5 ? sss : sss + 1
    // return dateOption
  }

  //start getuser
  getuserinfo() {
    this.spinner.show();
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
      })
  }



  openModal(template: TemplateRef<any>, id: any = null) {
    this.submitted = false;
    this.delid = id;
    this.Form.reset();
    //console.log(this.delid);
    this.modalRef = this.modalService.show(template);
  }

  editModal(template: TemplateRef<any>, id, phaseno, startdate, enddate, title, detail, location, group) {
    this.submitted = false;
    this.editid = id;
    //this.EditForm.reset();
    //console.log(this.delid);

    console.log(this.EditForm.value);
    this.modalRef = this.modalService.show(template);
    this.EditForm.patchValue({
      "phaseno": phaseno,
      startdate: {
        year: new Date(startdate).getFullYear(),
        month: new Date(startdate).getMonth() + 1,
        day: new Date(startdate).getDate()
      },
      enddate:  {
        year: new Date(enddate).getFullYear(),
        month: new Date(enddate).getMonth() + 1,
        day: new Date(enddate).getDate()
      },
      "title": title,
      "detail": detail,
      "location": location,
      "group": group,
    })
    console.log("Form =>", this.EditForm.value);

    //console.log("element: ", element.startDate)
    //const checkTimeStart = <FormArray>this.EditForm.get('inputdate') as FormArray;
    // let sDate: Date = new Date(startdate);
    // let eDate: Date = new Date(enddate)
    // console.log("EEE", sDate);

    // this.d.push(this.fb.group({
    //   startdate: {
    //     year: sDate.getFullYear(),
    //     month: sDate.getMonth() + 1,
    //     day: sDate.getDate()
    //   },
    //   enddate: {
    //     year: eDate.getFullYear(),
    //     month: eDate.getMonth() + 1,
    //     day: eDate.getDate()
    //   }
    // }))




  }


  get fe() { return this.EditForm }

  onDateChangedStart(event: IMyDateModel) {
    this.EditForm.patchValue({
      startdate: event.date
    })
  }

  onDateChangedEnd(event: IMyDateModel) {
    this.EditForm.patchValue({
      enddate: event.date
    })
  }


  storeTraining(value) {
    console.log("storeTraining => ", value);
    this.submitted = true;
    if (this.Form.invalid) {
      console.log("in1");
      return;
    } else {
      this.spinner.show();
      // this.test = []
      // for (let i = 0; i < value.group; i++) {
      //   this.test.push({
      //     id: i + 1
      //   })
      // }
      // console.log(this.test);

      this.trainingservice.addTrainingPhase(value, this.trainingid).subscribe(response => {
        this.submitted = false;
        this.modalRef.hide();
        console.log("viewdata:", value);
        console.log("result:", response);
        this.Form.reset();
        this.loading = false;
        this.logService.addLog(this.userid,'TrainingPhases','เพิ่ม', response.title,response.id).subscribe();
        this.getTrainingPhase();
        this._NotofyService.onSuccess("เพิ่มข้อมูล");
      })
    }
  }


  editTraining(value, id) {
    console.log("editTraining => ", value);
    this.submitted = true;
    if (this.EditForm.invalid) {
      console.log("in1");
      return;
    } else {
      this.trainingservice.editTrainingPhase(value, id).subscribe(response => {
        this.submitted = false;
        this.modalRef.hide();
        //this.EditForm.reset();
        this.loading = false;
        this.logService.addLog(this.userid,'TrainingPhases','แก้ไข', value.name,"").subscribe();
        this.getTrainingPhase();
        this._NotofyService.onSuccess("แก้ไขข้อมูล");

      })
    }
  }

  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.form.patchValue({
      files: file
    });
    this.form.get('files').updateValueAndValidity()
  }

  addFile(event) {
    this.files = event.target.files
  }
  get f() { return this.Form.controls }
  get d() { return this.f.inputdate as FormArray }

  deleteTraining(value) {
    this.trainingservice.deleteTrainingPhase(value).subscribe(response => {
      //console.log(value);
      this.modalRef.hide()
      this.loading = false;
      this.logService.addLog(this.userid,'ตารางกำหนดการหลักสูตรการอบรม(ช่วง)(TrainingPhases)','ลบ', value.name,"").subscribe();
      this.getTrainingPhase()
      this._NotofyService.onSuccess("ลบข้อมูล");
    })
  }
  gotoBack() {
    window.history.back();
  }

  gotoProgramTraining(id) {
    this.router.navigate(['/training/phase/program/' + this.trainingid + '/' + id])
  }
  gotoPlanTraining(id) {
    this.router.navigate(['/training/phase/plan/' + id])
  }

  gotoSummaryReportGroupTraining(phaseid) {
    this.router.navigate(['/training/report/summary/phase/group/' + this.trainingid + '/' + phaseid])
  }

}
