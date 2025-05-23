import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormArray, FormControl, Validators } from '@angular/forms';
import { AnswersubjectService } from 'src/app/services/answersubject.service';
import { SuggestionsubjectService } from 'src/app/services/suggestionsubject.service';
import { ActivatedRoute } from '@angular/router';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-answer-people-detail',
  templateUrl: './answer-people-detail.component.html',
  styleUrls: ['./answer-people-detail.component.css']
})
export class AnswerPeopleDetailComponent implements OnInit {

  id: any
  userid: string
  resultsubjectdetail: any = []
  resultsubquestion: any = []
  Form: FormGroup;
  Formfile: FormGroup;
  province: any
  answar = [{ test: "1212", benz: "121212" }]

  constructor(
    private answersubjectservice: AnswersubjectService,
    private suggestionservice: SuggestionsubjectService,
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private authorize: AuthorizeService,
    private spinner: NgxSpinnerService,
  ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
  }

  get f() { return this.Form.controls; }
  get t() { return this.f.result as FormArray; }

  Test(index, value) {
    this.t.at(index).patchValue({
      Answer: value
    })
    // ('answer').patchValue(event.target.value)
    console.log(this.t.value);
  }

  ngOnInit() {
    this.spinner.show();
    this.Form = this.fb.group({
      result: new FormArray([]),
      Suggestion: new FormControl(null, [Validators.required]),
    })
    this.Formfile = this.fb.group({
      files: [null],
      Type: ""
    })
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
      })
    this.getSubjectdetail()
  }
  getSubjectdetail() {
    this.answersubjectservice.getsubjectdetaildata(this.id).subscribe(result => {
      this.resultsubjectdetail = result
      this.province = this.resultsubjectdetail.centralPolicyProvince.province.name
      this.resultsubquestion = this.resultsubjectdetail.subquestionCentralPolicyProvinces
      // this.loading = true
      console.log(this.province);
      // this.loading = true;
      this.spinner.hide();
      this.addvalue();
    }
    )
  }
  addvalue() {
    this.Form.reset();
    this.t.clear();
    for (let i = 0; i < this.resultsubquestion.length; i++) {
      var test: any[] = this.resultsubquestion[i].subquestionChoiceCentralPolicyProvinces
      this.t.push(this.fb.group({
        UserId: "",
        SubquestionCentralPolicyProvinceId: [this.resultsubquestion[i].id],
        Question: [this.resultsubquestion[i].name],
        Answer: [""],
        Choice: [test],
        Type: [this.resultsubquestion[i].type]
      }))
    }
    console.log(this.t.value);

  }
  addsuggestion() {

  }
  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.Formfile.patchValue({
      files: file,
      Type: "รูปภาพ"
    });
    this.Formfile.get('files').updateValueAndValidity()

  }
  uploadSignatureFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.Formfile.patchValue({
      files: file,
      Type: "ลายมือชื่อ"
    });
    this.Formfile.get('files').updateValueAndValidity()

  }
  storeanswer(value) {
    this.spinner.show();
    this.storesuggestion(value)
    // this.storeansweruser(value)
  }
  storeansweruser(value) {
    console.log(this.userid);

    for (let i = 0; i < this.t.value.length; i++) {

      this.t.at(i).patchValue({
        UserId: this.userid,
      })
    }
    console.log(this.t.value);
    this.answersubjectservice.addAnswer(this.t.value).subscribe(result => {
      console.log("result", result);
      this.storesuggestion(value)
      // this.storefile()
      // this.Form.reset();
      // window.history.back();
    })
  }
  storesuggestion(value) {
    this.suggestionservice.addSuggestion(value, this.id, this.userid).subscribe(result => {
      this.storefile()
    })
  }
  storefile() {
    this.answersubjectservice.addFiles(this.id, this.Formfile.value, this.userid).subscribe(response => {
      this.Form.reset();
      this.Formfile.reset();
      this.spinner.hide();
      window.history.back();
    })
  }
  back() {
    window.history.back();
  }
}
