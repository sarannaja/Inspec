import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AnswersubjectService } from 'src/app/services/answersubject.service';
import { FormBuilder, FormGroup, Validators, FormControl, FormArray } from '@angular/forms';

@Component({
  selector: 'app-answer-outsider',
  templateUrl: './answer-outsider.component.html',
  styleUrls: ['./answer-outsider.component.css']
})
export class AnswerOutsiderComponent implements OnInit {

  id: any
  Username: any
  Userposition: any
  Userphonenumber: any
  resultsubjectdetail: any = []
  resultsubquestion: any[] = []
  resultChoice: any = []
  province: any
  Form: FormGroup;

  constructor(
    private answersubjectservice: AnswersubjectService,
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private router:Router
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
    this.Form = this.fb.group({
      result: new FormArray([])
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
      var test: any[] = this.resultsubquestion[i].subquestionChoiceCentralPolicyProvinces
      this.t.push(this.fb.group({
        SubquestionCentralPolicyProvinceId: [this.resultsubquestion[i].id],
        Name: "",
        Position: "",
        Phonenumber: "",
        Question: [this.resultsubquestion[i].name],
        Answer: [""],
        Choice: [test],
        Description: [""],
        Type: [this.resultsubquestion[i].type]
      }))
    }
    console.log(this.t.value);

  }

  storeanswer() {
    this.User()
    // console.log(this.t.value);
  }
  User(){    
    console.log(this.Username);
    
    for(let i = 0; i < this.t.value.length; i++){
      
      this.t.at(i).patchValue({
        Name: this.Username,
        Position: this.Userposition,
        Phonenumber: this.Userphonenumber
      })
    }
    console.log(this.t.value);
    this.answersubjectservice.addAnsweroutsider(this.t.value).subscribe(result => {
      console.log("result",result);
      this.Form.reset();
      this.router.navigate(['/ty'])
    })
  }
}
