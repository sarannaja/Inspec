import { Component, OnInit } from '@angular/core';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { AnswersubjectService } from 'src/app/services/answersubject.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-answer-recommendationin-spector-detail',
  templateUrl: './answer-recommendationin-spector-detail.component.html',
  styleUrls: ['./answer-recommendationin-spector-detail.component.css']
})
export class AnswerRecommendationinSpectorDetailComponent implements OnInit {

  id: any
  userid: any
  resultrecommendationinspector: any = []
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

    this.Form = this.fb.group({
      SubjectGroupId: this.id,
      Answer: new FormControl(null, [Validators.required]),
      Status: new FormControl("ร่างกำหนดการ", [Validators.required]),
    })
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
      })

    this.getRecommendationinspector()
  }
  getRecommendationinspector() {
    this.answersubjectservice.getRecommendationinspectordetail(this.id)
      .subscribe(result => {
        this.resultrecommendationinspector = result
        this.spinner.hide();
        this.loading = true
        console.log("test", this.resultrecommendationinspector);
      })
  }
  storerecommendationinspector(value) {
    this.spinner.show();
    console.log(value);
    this.answersubjectservice.addRecommendationinspector(value, this.userid).subscribe(result => {
      this.Form.reset();
      this.spinner.hide();
      window.history.back();
    })
  }
  back() {
    window.history.back();
  }
}
