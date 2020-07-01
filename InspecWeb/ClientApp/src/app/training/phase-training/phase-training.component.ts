import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TrainingService } from '../../services/training.service';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from "ngx-spinner";
import { IOption } from 'ng-select';
import { IMyOptions } from 'mydatepicker-th';
import * as moment from 'moment';
import { Chart } from 'chart.js';

@Component({
  selector: 'app-phase-training',
  templateUrl: './phase-training.component.html',
  styleUrls: ['./phase-training.component.css']
})
export class PhaseTrainingComponent implements OnInit {

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
  selectdatalecturer: Array<IOption>
  resultlecturer: any = []
  startdate: any;
  enddate: any;
  dateOption: IMyOptions
  resulttrainingdetail: any[];
  lineChart: any;

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
      programtype: new FormControl(null, [Validators.required]),
      programdate: new FormControl(null, [Validators.required]),
      mStart: new FormControl(null, [Validators.required]),
      mEnd: new FormControl(null, [Validators.required]),
      programtopic: new FormControl(null, [Validators.required]),
      programdetail: new FormControl(null, [Validators.required]),
      lecturername: new FormControl(null, [Validators.required]),

    })

    this.trainingservice.getTrainingPhase(this.trainingid)
      .subscribe(result => {
        this.resulttraining = result
        this.loading = true
        //console.log(this.resulttraining);
      })


    this.trainingservice.getdetailtraining(this.trainingid)
      .subscribe(result => {
        if (result.length != 0) {
          this.startdate = result[0].startDate
          this.enddate = result[0].endDate
          this.dateOptionF(this.startdate, this.enddate)
          this.Form.patchValue({  
            'programdate':this.startdate
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



  dateOptionF(start, end) {
    var startDate = new Date(start);
    var endDate = new Date(end);
    var dateDayRemoveStart = new Date()
    dateDayRemoveStart.setDate(startDate.getDay() - 1)

    var dateDayRemoveEnd = new Date()
    dateDayRemoveEnd.setDate(endDate.getDay() + 1)

    // moment(startDate).calendar()
    
    this.dateOption = {
      showTodayBtn:false,
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



  openModal(template: TemplateRef<any>, id) {
    this.delid = id;
    //console.log(this.delid);
    this.modalRef = this.modalService.show(template);
  }


  storeTraining(value) {
    //alert(JSON.stringify(value))   
    //alert(this.form.value.files)
    this.trainingservice.addTrainingProgram(value, this.trainingid).subscribe(response => {
      console.log(value);
      this.Form.reset()
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
      this.trainingservice.getTrainingPhase(this.trainingid)
        .subscribe(result => {
          this.resulttraining = result
          this.loading = true
          //console.log(this.resulttraining);
        })
    })
  }

  gotoProgramTraining(id){
    this.router.navigate(['/training/phase/program/' + this.trainingid + '/' + id])
  }

}
