import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AnswersubjectService } from 'src/app/services/answersubject.service';
import { FormBuilder, FormGroup, Validators, FormControl, FormArray } from '@angular/forms';

@Component({
  selector: 'app-answer-outsider',
  templateUrl: './answer-outsider.component.html',
  styleUrls: ['./answer-outsider.component.css']
})
export class AnswerOutsiderComponent implements OnInit {

  id: any
  resultsubjectdetail: any = []
  resultsubquestion: any[] = []
  resultChoice: any =[]
  province: any
  Form: FormGroup;

  constructor(
    private answersubjectservice: AnswersubjectService,
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
  ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
  }

  get f() { return this.Form.controls; }
  get t() { return this.f.result as FormArray; }

  ngOnInit() {
    this.Form = this.fb.group({
      result: new FormArray([])
      // subquestionid : new FormControl(null, [Validators.required]),
      // name: "nik",
      // position: "position",
      // phonenumber: "1234567890",
      // answer: [null, [Validators.required, Validators.pattern('[0-9]{3}')]],
    })
    this.getSubjectdetail()
  }
  getSubjectdetail() {
    this.answersubjectservice.getsubjectdetaildata(this.id).subscribe(result => {
      this.resultsubjectdetail = result
      this.province = this.resultsubjectdetail.centralPolicyProvince.province.name
      this.resultsubquestion = this.resultsubjectdetail.subquestionCentralPolicyProvinces
      // this.loading = true

      console.log(this.resultsubquestion);
      // this.loading = true;
      // this.spinner.hide();
      this.addvalue();
    }
    )
  }
  addvalue() {
    this.Form.reset();
    this.t.clear();
    for (let i = 0; i < this.resultsubquestion.length; i++) {
      var test:any[] = this.resultsubquestion[i].subquestionChoiceCentralPolicyProvinces
      this.t.push(this.fb.group({
        subquestionid: [this.resultsubquestion[i].id],
        name: "nik",
        position: "position",
        phonenumber: "1234567890",
        question: [this.resultsubquestion[i].name],
        answer: [ test.length == 0 ? "" : test[0].name],
        choice: [test],
        type: [this.resultsubquestion[i].type]
      }))
    }
    console.log(this.t.value);
    
  }

  storeanswer() {
    console.log(this.t.value);
  }
}
