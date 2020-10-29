import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormGroup, FormControl, Validators, FormBuilder, FormArray } from '@angular/forms';
import { AnswersubjectService } from 'src/app/services/answersubject.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { Answerrole7List } from 'src/app/services/nikmodel/answerrole7list';
import { GetQuestionPeople } from 'src/app/services/nikmodel/answarrole7';
import { NotofyService } from 'src/app/services/notofy.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-answer-central-policy-province-edit',
  templateUrl: './answer-central-policy-province-edit.component.html',
  styleUrls: ['./answer-central-policy-province-edit.component.css']
})
export class AnswerCentralPolicyProvinceEditComponent implements OnInit {

  id
  inspectionPlanEventId
  userid
  centralPolicyEventId
  status
  resultQuestionPeople: any[] = []
  resultsubjecteventfiles: any[] = []
  resultanswer: Answerrole7List[] = []
  Form: FormGroup
  Formstatus: FormGroup
  loading = false;
  submitted = false;
  provinceid: any
  centralPolicyProvinceId: any
  subjectGroupId: any
  centralPolicyId: any
  downloadUrl: any;

  constructor(
    private answersubjectservice: AnswersubjectService,
    private activatedRoute: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private fb: FormBuilder,
    private authorize: AuthorizeService,
    private _NotofyService: NotofyService,
    private notificationService: NotificationService,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.id = activatedRoute.snapshot.paramMap.get('result')
    this.inspectionPlanEventId = activatedRoute.snapshot.paramMap.get('inspectionplaneventid')
    this.downloadUrl = baseUrl + '/Uploads';
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
    this.answersubjectservice.getcentralpolicyprovince(this.id, this.inspectionPlanEventId)
      .subscribe(result => {
        // console.log(result);
        this.loading = true
        this.resultQuestionPeople = result
        this.provinceid = this.resultQuestionPeople[0].centralPolicyEvent.inspectionPlanEvent.provinceId
        this.centralPolicyProvinceId = this.resultQuestionPeople[0].centralPolicyEvent.centralPolicyId
        this.subjectGroupId = this.resultQuestionPeople[0].centralPolicyEvent.subjectGroupPeopleQuestions[0].subjectGroupId
        this.centralPolicyId = this.resultQuestionPeople[0].centralPolicyEvent.centralPolicyId
        this.getSubjectEventFiles()
      })
  }
  getSubjectEventFiles(){
    this.answersubjectservice.getSubjectEventFiles(this.subjectGroupId).subscribe(result => {
      this.resultsubjecteventfiles = result
    })
  }
  getAnsweruser() {
    this.answersubjectservice.getAnsweruserlistrole7(this.id, this.inspectionPlanEventId, this.userid).subscribe(result => {
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
        Answer: [this.resultanswer[i].answer, Validators.required]
      }))
    }
    console.log(this.t.value);

  }
  editanswer(value) {
    this.submitted = true;
    if (this.Form.invalid) {
      console.log("in1");
      return;
    } else {
      console.log(this.t.value);
      console.log(value);
      this.spinner.show();
      this.answersubjectservice.editAnswerrole7(this.t.value).subscribe(result => {
        this.editStatus(value)
      })
    }
  }
  editStatus(value) {
    this.answersubjectservice.editStatusrole7(value, this.centralPolicyEventId).subscribe(result => {
      if (value.Status == "ใช้งานจริง") {
        this.notificationService.addNotification(this.centralPolicyId, this.provinceid, this.userid, 6, this.subjectGroupId,null)
          .subscribe(response => {
            console.log("innoti", response);
          })
      }
      this.Form.reset();
      this.Formstatus.reset();
      this.spinner.hide();
      this._NotofyService.onSuccess("แก้ไขข้อมูล")
      window.history.back();
    })
  }
  back() {
    window.history.back();
  }
}
