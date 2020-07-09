import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormGroup, FormControl, Validators, FormBuilder, FormArray } from '@angular/forms';
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
  Formstatus: FormGroup
  resultQuestionPeople: any = []

  constructor(
    private answersubjectservice: AnswersubjectService,
    private activatedRoute: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private fb: FormBuilder,
    private authorize: AuthorizeService
  ) {
    this.id = activatedRoute.snapshot.paramMap.get('result')
  }
  get f() { return this.Form.controls; }
  get t() { return this.f.result as FormArray; }
  ngOnInit() {
    this.spinner.show();
    // this.Form = this.fb.group({
    //   AnswerPeople: new FormControl(null, [Validators.required]),
    // })
    this.Form = this.fb.group({
      result: new FormArray([])
    })
    this.Formstatus = this.fb.group({
      Status: new FormControl("ร่างกำหนดการ", [Validators.required]),
    })
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
      })
    this.getQuestionPeople()
  }
  getQuestionPeople() {
    this.answersubjectservice.getcentralpolicyprovince(this.id)
      .subscribe(result => {
        this.resultQuestionPeople = result
        this.spinner.hide();
        this.addvalue();
      })
  }
  addvalue() {
    this.Form.reset();
    this.t.clear();
    for (let i = 0; i < this.resultQuestionPeople.length; i++) {
      // var test: any[] = this.resultQuestionPeople[i]
      this.t.push(this.fb.group({
        CentralPolicyProvinceId: [parseInt(this.id)],
        CentralPolicyEventQuestionId: [this.resultQuestionPeople[i].id],
        UserId: [this.userid],
        Question: [this.resultQuestionPeople[i].questionPeople],
        Answer: [""]
      }))
    }
    // console.log("Form",this.t.value);

  }
  storeanswer(valuestatus) {
    this.spinner.show();
    console.log("Form", this.t.value);
    this.answersubjectservice.addAnswercentralpolicyprovince(this.t.value).subscribe(result => {
      console.log("result", result);
      this.storestatus(valuestatus)
    })
  }
  storestatus(valuestatus) {
    this.answersubjectservice.addStatusrole7(valuestatus, this.resultQuestionPeople[0].centralPolicyEventId, this.userid).subscribe(result => {
      this.spinner.hide();
      this.Form.reset();
      this.Formstatus.reset();
      window.history.back();
    })
  }
  back() {
    window.history.back();
  }
}
