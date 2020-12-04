import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { AnswersubjectService } from 'src/app/services/answersubject.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-answer-recommendationin-spector-edit',
  templateUrl: './answer-recommendationin-spector-edit.component.html',
  styleUrls: ['./answer-recommendationin-spector-edit.component.css']
})
export class AnswerRecommendationinSpectorEditComponent implements OnInit {

  id: any
  userid: any
  status: any
  resultrecommendationinspector: any = []
  resultanswerrecommendationinspector: any = []
  loading = false
  Form: FormGroup;
  constructor(
    private authorize: AuthorizeService,
    private answersubjectservice: AnswersubjectService,
    private spinner: NgxSpinnerService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
  ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
  }

  ngOnInit() {
    this.spinner.show();

    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
      })
    this.Form = this.fb.group({
      Answer: new FormControl(null, [Validators.required]),
      Status: new FormControl("ร่างกำหนดการ", [Validators.required]),
    })
    this.getRecommendationinspector()
  }
  getRecommendationinspector() {
    this.answersubjectservice.getRecommendationinspectordetail(this.id)
      .subscribe(result => {
        this.resultrecommendationinspector = result
        console.log("test", this.resultrecommendationinspector);
        this.getAnswerRecommendationinspector()
      })
  }
  getAnswerRecommendationinspector() {
    this.answersubjectservice.getAnswerRecommendationinspectoruser(this.userid, this.resultrecommendationinspector.id).subscribe(result => {
      this.resultanswerrecommendationinspector = result
      this.status = result.status
      console.log(result);
      this.Form.patchValue({
        Answer: this.resultanswerrecommendationinspector.answersuggestion,
      });
      this.spinner.hide();
      this.loading = true
    })
  }
  editcommendationinspector(value) {
    console.log(value);
    this.spinner.show();
    this.answersubjectservice.editAnswerRecommendationinspector(value, this.resultanswerrecommendationinspector.id).subscribe(result => {
      this.Form.reset();
      this.spinner.hide();
      window.history.back();
    })
  }
  back() {
    window.history.back();
  }
}
