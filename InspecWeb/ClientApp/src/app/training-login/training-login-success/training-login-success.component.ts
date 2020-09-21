import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { TrainingLoginService } from 'src/app/services/training-login.service';

@Component({
  selector: 'app-training-login-success',
  templateUrl: './training-login-success.component.html',
  styleUrls: ['./training-login-success.component.css']
})
export class TrainingLoginSuccessComponent implements OnInit {
  downloadUrl: any;
  url: any;
  Form: FormGroup;
  trainingName: any;
  trainingData: any = [];

  constructor(
    private fb: FormBuilder,
    private trainingLoginService: TrainingLoginService,
    private activatedRoute: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string,
  ) {
    this.downloadUrl = baseUrl + '/Uploads';
    this.url = baseUrl;
    this.trainingName = activatedRoute.snapshot.paramMap.get('trainingName')
  }

  ngOnInit() {
    this.Form = this.fb.group({
      username: new FormControl("", [Validators.required]),
    })
  }

  accept() {
    window.history.back();
  }
}
