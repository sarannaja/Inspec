import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { AnswersubjectService } from 'src/app/services/answersubject.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';

@Component({
  selector: 'app-answer-central-policy-province',
  templateUrl: './answer-central-policy-province.component.html',
  styleUrls: ['./answer-central-policy-province.component.css']
})
export class AnswerCentralPolicyProvinceComponent implements OnInit {

  id: any
  userid: any
  Form: FormGroup
  resultQuestionPeople: any = null

  constructor(
    private answersubjectservice: AnswersubjectService,
    private activatedRoute: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private fb: FormBuilder,
    private authorize: AuthorizeService
  ) {
    this.id = activatedRoute.snapshot.paramMap.get('result')
  }

  ngOnInit() {
    this.spinner.show();
    this.Form = this.fb.group({
      AnswerPeople: new FormControl(null, [Validators.required]),
    })
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
      })
    this.getQuestionPeople()
  }
  getQuestionPeople() {
    this.answersubjectservice.getcentralpolicyprovinc(this.id)
      .subscribe(result => {
        this.resultQuestionPeople = result
        console.log("eqwwqewqewqewqdfskfdnsfnsdjkfds", this.resultQuestionPeople);
        this.spinner.hide();
      })
  }
  storeanswer(value) {
    this.spinner.show();
    console.log(value);
    this.answersubjectservice.addAnswercentralpolicyprovince(value, this.id, this.userid).subscribe(result => {
      console.log("result", result);
      this.spinner.show();
      this.Form.reset();
      window.history.back();
    })
  }
  back() {
    window.history.back();
  }
}
