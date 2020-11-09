import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormControl, Validators, FormGroup, FormArray } from '@angular/forms';
import { TrainingService } from 'src/app/services/training.service';
import { ActivatedRoute, Router } from '@angular/router';
import { IMyDateModel, IMyOptions } from 'mydatepicker-th';

import { NotofyService } from '../../services/notofy.service';
import { NgxSpinnerService } from "ngx-spinner";
import { LogService } from '../../services/log.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';

@Component({
  selector: 'app-edit-training',
  templateUrl: './edit-training.component.html',
  styleUrls: ['./edit-training.component.css']
})
export class EditTrainingComponent implements OnInit {

  myDatePickerOptions: IMyOptions = {
    // other options...
    //dateFormat: 'dd/mm/yyyy',
    showClearDateBtn: false
  };
  
  name: any
  detail: any
  start_date: any
  end_date: any
  lecturer_name: any
  regis_start_date: any
  regis_end_date: any
  Form: FormGroup;
  EditForm: FormGroup;
  files: string[] = []
  inputdate: any = [{ start_date: '', end_date: '' }]
  form: FormGroup;
  formfile: FormGroup;
  downloadUrl: any;
  submitted = false;
  trainingid: any;
  resulttraining: any;
  ImgProfile: any;
  userid: string;

  constructor(private fb: FormBuilder,
    private authorize: AuthorizeService,
    private _NotofyService: NotofyService,
    private spinner: NgxSpinnerService,
    private logService: LogService,
    private trainingservice: TrainingService,
    public share: TrainingService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string) {
    this.downloadUrl = baseUrl + 'Uploads/'
    this.trainingid = activatedRoute.snapshot.paramMap.get('id')
  }

  ngOnInit() {
    this.getuserinfo();
    this.Form = this.fb.group({
      name: new FormControl(null, [Validators.required]),
      detail: new FormControl(null, [Validators.required]),
      generation: new FormControl(null, [Validators.required]),
      year: new FormControl(null, [Validators.required]),
      coursecode: new FormControl(null, [Validators.required]),
      start_date: new FormControl(null, [Validators.required]),
      end_date: new FormControl(null, [Validators.required]),
      regis_start_date: new FormControl(null, [Validators.required]),
      regis_end_date: new FormControl(null, [Validators.required]),
      //alertimg: "(รอการอัพเดท)"

    })
    this.form = this.fb.group({
      files: [null]
    })


    this.trainingservice.getdetailtraining(this.trainingid)
      .subscribe(result => {
        this.resulttraining = result
        console.log("resulttraining => ", this.resulttraining);
      
        this.ImgProfile = this.resulttraining[0].image;

        this.Form.patchValue({
          name: this.resulttraining[0].name,
          detail: this.resulttraining[0].detail,
          generation: this.resulttraining[0].generation,
          year: this.resulttraining[0].year,
          coursecode: this.resulttraining[0].courseCode,
          // regis_start_date: this.resulttraining[0].regisStartDate,
          // regis_end_date: this.resulttraining[0].regisEndDate,
          // start_date: this.resulttraining[0].startDate,
          // end_date: this.resulttraining[0].endDate,

          regis_start_date: {
            year: new Date(this.resulttraining[0].regisStartDate).getFullYear(),
            month: new Date(this.resulttraining[0].regisStartDate).getMonth() + 1,
            day: new Date(this.resulttraining[0].regisStartDate).getDate()
          },
          regis_end_date: {
            year: new Date(this.resulttraining[0].regisEndDate).getFullYear(),
            month: new Date(this.resulttraining[0].regisEndDate).getMonth() + 1,
            day: new Date(this.resulttraining[0].regisEndDate).getDate()
          },

          start_date: {
            year: new Date(this.resulttraining[0].startDate).getFullYear(),
            month: new Date(this.resulttraining[0].startDate).getMonth() + 1,
            day: new Date(this.resulttraining[0].startDate).getDate()
          },

          end_date: {
            year: new Date(this.resulttraining[0].endDate).getFullYear(),
            month: new Date(this.resulttraining[0].endDate).getMonth() + 1,
            day: new Date(this.resulttraining[0].endDate).getDate()
          },
          
        });

      })
      this.spinner.hide();
  }

  //start getuser
  getuserinfo() {
    this.spinner.show();
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
      })
  }
  
  editTraining(value) {
    console.log("editTraining =>", value);
    
    this.submitted = true;
    if (this.form.invalid) {
      console.log("in1");
      return;
    } else {

    this.spinner.show();
    this.trainingservice.editTraining(value, this.trainingid, this.form.value.files).subscribe(reuslt => {
      console.log(reuslt);
      this.Form.reset()
      this.spinner.hide();
      this.logService.addLog(this.userid,'Trainings','แก้ไข', reuslt.name, reuslt.id).subscribe();
      this.router.navigate(['training'])
      this._NotofyService.onSuccess("แก้ไขข้อมูล")
      // this.centralpolicyservice.getcentralpolicydata().subscribe(result => {
      //   this.centralpolicyservice = result
      //   console.log(this.resultcentralpolicy);
      // })
    })
    }
  }

  uploadFile(event) {

    const file = (event.target as HTMLInputElement).files;
    
    this.form.patchValue({
      alertimg: "(รอการอัพเดท)",
      files: file
    });
    //this.form.get('files').updateValueAndValidity()
    
  }



  addFile(event) {
    this.files = event.target.files
  }
  get f() { return this.Form.controls }
  get d() { return this.f.inputdate as FormArray }

  // appenddate() {
  //   this.d.push(this.fb.group({
  //     start_date: '',
  //     end_date: '',
  //   }))
  // }
  remove(index: number) {
    this.d.removeAt(index);
  }

  gotoBack() {
    window.history.back();
  }

  get fe() { return this.Form }

  onDateRegisChangedStart(event: IMyDateModel) {
    this.Form.patchValue({
      regis_start_date: event.date
    })
  }

  onDateRegisChangedEnd(event: IMyDateModel) {
    this.Form.patchValue({
      regis_end_date: event.date
    })
  }

  onDateChangedstart(event: IMyDateModel) {
    this.Form.patchValue({
      start_date: event.date
    })
  }

  onDateChangedend(event: IMyDateModel) {
    this.Form.patchValue({
      end_date: event.date
    })
  }

}
