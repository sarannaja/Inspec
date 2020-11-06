import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormControl, Validators, FormGroup, FormArray } from '@angular/forms';
import { TrainingService } from 'src/app/services/training.service';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { IMyOptions } from 'mydatepicker-th';

@Component({
  selector: 'app-create-training',
  templateUrl: './create-training.component.html',
  styleUrls: ['./create-training.component.css']
})
export class CreateTrainingComponent implements OnInit {

  name: any
  detail: any
  start_date: any
  end_date: any
  lecturer_name: any
  regis_start_date: any
  regis_end_date: any
  Form: FormGroup;
  files: string[] = []
  inputdate: any = [{ start_date: '', end_date: '' }]
  form: FormGroup;
  formfile: FormGroup;
  downloadUrl: any;
  submitted = false;
  public myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd/mm/yyyy',
    showTodayBtn: true
  };

  constructor(private fb: FormBuilder,
    private trainingservice: TrainingService,
    public share: TrainingService,
    private router: Router,
    private spinner: NgxSpinnerService,
    @Inject('BASE_URL') baseUrl: string) {
    this.downloadUrl = baseUrl + '/Uploads'
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
      // files: new FormControl(null, [Validators.required]),
      // inputdate: new FormArray([]),
      // lecturer_name: new FormControl(null, [Validators.required]),
      // "test" : new FormControl(null,[Validators.required,this.forbiddenNames.bind(this)])
    })
    this.form = this.fb.group({
      files: [null]
    })
    // this.d.push(this.fb.group({
    //   start_date: '',
    //   end_date: '',
    // }))
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
