import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TrainingLoginService } from '../services/training-login.service';

@Component({
  selector: 'app-training-login',
  templateUrl: './training-login.component.html',
  styleUrls: ['./training-login.component.css']
})
export class TrainingLoginComponent implements OnInit {
  downloadUrl: any;
  url: any;
  Form: FormGroup;
  trainingPhaseId: any;
  trainingData: any = [];
  submitted = false;
  fail = false;

  constructor(
    private fb: FormBuilder,
    private trainingLoginService: TrainingLoginService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    @Inject('BASE_URL') baseUrl: string,
  ) {
    this.downloadUrl = baseUrl + '/Uploads';
    this.url = baseUrl;
    this.trainingPhaseId = activatedRoute.snapshot.paramMap.get('phaseid')
  }

  ngOnInit() {
    this.Form = this.fb.group({
      username: new FormControl("", [Validators.required]),
    })

    this.getTrainingData();
  }

  getTrainingData() {
    this.trainingLoginService.getTrainingData(this.trainingPhaseId).subscribe(res => {
      console.log("trainingData => ", res);
      this.trainingData = res;
    })
  }

  TrainingSignin(value) {
    this.submitted = true;
    this.fail = false;
    if (this.Form.invalid) {
      console.log("in1");
      return;
    } else {
      this.trainingLoginService.signInTraining(value, this.trainingPhaseId).subscribe(res => {
        console.log('RES => ', res);
        if (res == true) {
          this.router.navigate(['/training/login-success', { trainingName: this.trainingData.name }])
        } else if (res == false) {
          this.fail = true;
          console.log('fail', this.fail);
        }
      })
    }
  }

}
