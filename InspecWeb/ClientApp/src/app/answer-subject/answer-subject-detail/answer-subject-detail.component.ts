import { Component, OnInit } from '@angular/core';
import { AnswersubjectService } from 'src/app/services/answersubject.service';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-answer-subject-detail',
  templateUrl: './answer-subject-detail.component.html',
  styleUrls: ['./answer-subject-detail.component.css']
})
export class AnswerSubjectDetailComponent implements OnInit {

  id: any
  resultsubjectdetail: any = []
  resultsubquestion: any = []
  Form: FormGroup;
  province: any
  answar = [{test: "1212" , benz:"121212"}]

  constructor(
    private answersubjectservice: AnswersubjectService,
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
  ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
  }

  ngOnInit() {

    this.Form = this.fb.group({
      answer: [null, [Validators.required, Validators.pattern('[0-9]{3}')]],
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
      // this.spinner.hide();
    }
    )
  }
  store(value){
    alert(this.answar)
    console.log(value);
  }
  back() {
    window.history.back();
  }
}
