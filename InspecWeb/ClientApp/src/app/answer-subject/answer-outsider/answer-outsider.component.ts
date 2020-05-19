import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AnswersubjectService } from 'src/app/services/answersubject.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-answer-outsider',
  templateUrl: './answer-outsider.component.html',
  styleUrls: ['./answer-outsider.component.css']
})
export class AnswerOutsiderComponent implements OnInit {

  id: any
  resultsubjectdetail: any = []
  resultsubquestion: any = []
  province: any
  Form: FormGroup;

  constructor( 
    private answersubjectservice: AnswersubjectService,
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
    ) { 
    this.id = activatedRoute.snapshot.paramMap.get('id')
  }

  ngOnInit() { 
    this.Form = this.fb.group({
      name:  [null, [Validators.required, Validators.pattern('[0-9]{3}')]],
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
}
