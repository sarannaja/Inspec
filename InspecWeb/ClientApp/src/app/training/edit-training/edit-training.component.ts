import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormControl, Validators, FormGroup, FormArray } from '@angular/forms';
import { TrainingService } from 'src/app/services/training.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { IMyOptions } from 'mydatepicker-th';
@Component({
  selector: 'app-edit-training',
  templateUrl: './edit-training.component.html',
  styleUrls: ['./edit-training.component.css']
})
export class EditTrainingComponent implements OnInit {
  myDatePickerOptions: IMyOptions = {
    // other options...

    dateFormat: 'dd/mm/yyyy',
    showClearDateBtn: false,
    editableDateField: false
    // dateFormat: 'dd/mmm/yyyy', เดือนเป็นไทย
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

  constructor(private fb: FormBuilder,
    private trainingservice: TrainingService,
    public share: TrainingService,
    private router: Router,
    private spinner: NgxSpinnerService,
    private activatedRoute: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string) {
    this.downloadUrl = baseUrl + '/Uploads'
    this.trainingid = activatedRoute.snapshot.paramMap.get('id')
  }

  ngOnInit() {
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

    })
    this.form = this.fb.group({
      files: [null]
    })


    this.trainingservice.getdetailtraining(this.trainingid)
      .subscribe(result => {
        this.resulttraining = result
        console.log("resulttraining => ", this.resulttraining);

        this.Form.patchValue({
          name: this.resulttraining[0].name,
          detail: this.resulttraining[0].detail,
          generation: this.resulttraining[0].generation,
          year: this.resulttraining[0].year,
          coursecode: this.resulttraining[0].courseCode,
          start_date: this.resulttraining[0].startDate,
          end_date: this.resulttraining[0].endDate,
          regis_start_date: this.resulttraining[0].regisStartDate,
          regis_end_date: this.resulttraining[0].regisEndDate,
        });

      })
  }
  storeTraining(value) {

    this.submitted = true;
    if (this.form.invalid) {
      console.log("in1");
      return;
    } else {

      this.spinner.show();
      this.trainingservice.addTraining2(value, this.form.value.files).subscribe(reuslt => {
        console.log(reuslt);
        this.Form.reset()
        this.spinner.hide();
        this.router.navigate(['training'])
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
      files: file
    });
    this.form.get('files').updateValueAndValidity()
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

}
