import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormGroup, FormControl, Validators, FormBuilder, FormArray } from '@angular/forms';
import { AnswersubjectService } from 'src/app/services/answersubject.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { Answerrole7List } from 'src/app/services/nikmodel/answerrole7list';
import { GetQuestionPeople } from 'src/app/services/nikmodel/answarrole7';

@Component({
  selector: 'app-answer-central-policy-province-edit',
  templateUrl: './answer-central-policy-province-edit.component.html',
  styleUrls: ['./answer-central-policy-province-edit.component.css']
})
export class AnswerCentralPolicyProvinceEditComponent implements OnInit {

  id
  userid
  centralPolicyEventId
  status
  resultQuestionPeople: GetQuestionPeople[] = []
  resultanswer: Answerrole7List[] = []
  Form: FormGroup
  Formstatus: FormGroup
  loading = false;

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
    this.getAnsweruser()
  }
  getQuestionPeople() {
    this.answersubjectservice.getcentralpolicyprovince(this.id)
      .subscribe(result => {
        // console.log(result);
        this.loading = true
        this.resultQuestionPeople = result
      })
  }
  getAnsweruser() {
    this.answersubjectservice.getAnsweruserlistrole7(this.id, this.userid).subscribe(result => {
      console.log(result);
      this.resultanswer = result
      result[0].centralPolicyEventQuestion.centralPolicyEventId
      this.centralPolicyEventId = this.resultanswer[0].centralPolicyEventQuestion.centralPolicyEventId
      this.getAnswerstatus()
      this.addvalue();
    })
  }
  getAnswerstatus() {
    this.answersubjectservice.getAnswerstatusrole7(this.centralPolicyEventId, this.userid).subscribe(result => {
      this.status = result.status
      this.spinner.hide();
    })
  }
  addvalue() {
    this.Form.reset();
    this.t.clear();
    for (let i = 0; i < this.resultanswer.length; i++) {
      // var test: any[] = this.resultQuestionPeople[i]
      this.t.push(this.fb.group({
        Id: [this.resultanswer[i].id],
        CentralPolicyProvinceId: [this.resultanswer[i].centralPolicyProvinceId],
        CentralPolicyEventQuestionId: [this.resultanswer[i].centralPolicyEventQuestionId],
        UserId: [this.userid],
        Question: [this.resultanswer[i].centralPolicyEventQuestion.questionPeople],
        Answer: [this.resultanswer[i].answer]
      }))
    }
    console.log(this.t.value);

  }
  editanswer(value) {
    console.log(this.t.value);
    console.log(value);
    this.spinner.show();
    this.answersubjectservice.editAnswerrole7(this.t.value).subscribe(result => {
      this.editStatus(value)
    })
  }
  editStatus(value) {
    this.answersubjectservice.editStatusrole7(value, this.centralPolicyEventId).subscribe(result => {
      this.Form.reset();
      this.Formstatus.reset();
      this.spinner.hide();
      window.history.back();
    })
  }
  back() {
    window.history.back();
  }
}