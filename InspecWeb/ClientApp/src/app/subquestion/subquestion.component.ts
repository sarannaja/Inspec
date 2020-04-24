import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray } from '@angular/forms';
import { SubquestionService } from '../services/subquestion.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CentralpolicyService } from '../services/centralpolicy.service';
import { IOption } from 'ng-select';
import { IMyDate } from 'mydatepicker-th';
import { SubjectService } from '../services/subject.service';
import { DepartmentService } from '../services/department.service';

@Component({
  selector: 'app-subquestion',
  templateUrl: './subquestion.component.html',
  styleUrls: ['./subquestion.component.css']
})
export class SubquestionComponent implements OnInit {

  private startDate: IMyDate = { year: 0, month: 0, day: 0 };
  private endDate: IMyDate = { year: 0, month: 0, day: 0 };
  inputquestionopen: any = [{ questionopen: '' }]
  // inputquestionclose: any = [{ questionclose: '', answercloselist: [] }]
  // inputanswerclose: any = [{ answerclose: '' }]
  resultcentralpolicy: any = []
  resultprovince: any = []
  resultdepartment = []
  test = []
  id
  name: any
  modalRef: BsModalRef;
  Form: FormGroup;
  times: IOption[] = [];
  selectdatacentralpolicy: Array<IOption>
  benz = [{ value: '1212', label: 'benz' }, { value: '1212', label: 'Songnew' }];

  constructor(
    private modalService: BsModalService,
    private fb: FormBuilder,
    private centralpolicyservice: CentralpolicyService,
    private subjectservice: SubjectService,
    private departmentservice: DepartmentService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    public share: SubquestionService) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.name = activatedRoute.snapshot.paramMap.get('name')
  }

  ngOnInit() {

    this.Form = this.fb.group({
      name: new FormControl(null, [Validators.required]),
      centralpolicydateid: new FormControl(null, [Validators.required]),
      inputquestionopen: new FormArray([]),
      inputquestionclose: this.fb.array([
        this.initquestionclose()
      ])
    })
    this.getTimeCentralPolicy()
    this.d.push(this.fb.group({
      questionopen: '',
    }))

    console.log("55555", this.Form.value);
  }
  get f() { return this.Form.controls }
  get d() { return this.f.inputquestionopen as FormArray }
  get x() { return this.initanswerclose() }

  initquestionclose() {
    return this.fb.group({
      questionclose: [null, [Validators.required, Validators.pattern('[0-9]{3}')]],
      inputanswerclose: this.fb.array([
        this.initanswerclose()
      ])
    });
  }
  initanswerclose() {
    return this.fb.group({
      answerclose: [null, [Validators.required, Validators.pattern('[0-9]{3}')]],
    })
  }
  addX() {
    const control = <FormArray>this.Form.controls['inputquestionclose'];
    control.push(this.initquestionclose());
  }
  addY(ix) {
    const control = (<FormArray>this.Form.controls['inputquestionclose']).at(ix).get('inputanswerclose') as FormArray;
    control.push(this.initanswerclose());
  }
  back() {
    window.history.back();
  }

  getTimeCentralPolicy() {
    this.centralpolicyservice.getdetailcentralpolicydata(this.id).subscribe(result => {
      this.resultcentralpolicy = result
      // this.resultprovince = this.resultcentralpolicy.centralPolicyProvinces
      this.resultcentralpolicy.centralPolicyProvinces.forEach(element => {
        //  console.log("element: ", element.provinceId);
        this.resultprovince.push(element.provinceId)
      });
      this.getDepartmentdata()
      // console.log("111", this.resultprovince);

      this.times = []
      // let StartDate = ImyDate = {year:  0}
      for (var i = 0; i < this.resultcentralpolicy.centralPolicyDates.length; i++) {
        let d: Date = new Date(this.resultcentralpolicy.centralPolicyDates[i].startDate)
        // alert(this.resultsubject[0].centralPolicy.centralPolicyDates[i].startDate)
        let e: Date = new Date(this.resultcentralpolicy.centralPolicyDates[i].endDate)
        this.startDate = {
          year: d.getFullYear() + 543,
          month: d.getMonth() + 1,
          day: d.getDate()
        }
        this.endDate = {
          year: e.getFullYear() + 543,
          month: e.getMonth() + 1,
          day: e.getDate()
        }
        let test = this.startDate.day + '/' + this.startDate.month + '/' + this.startDate.year +
          "  ถึง  " + this.endDate.day + '/' + this.endDate.month + '/' + this.endDate.year


        this.times.push({
          value: this.resultcentralpolicy.centralPolicyDates[i].id,
          label: test,
        })
        // console.log(this.resultcentralpolicy.centralPolicyDates[i].startDate);
      }

    })
  }

  getDepartmentdata() {
    this.departmentservice.getdepartmentdata(this.id).subscribe(result => {

      // for (var i = 0; i < result.length; i++) {
      //   this.resultdepartment.push({ value: result[i].provinceDepartmentId, label: result[i].provinceName })
      // }
      // this.resultdepartment = result
      // alert(this.resultprovince)


      for (var i = 0; i < this.resultprovince.length; i++) {
      
        this.resultdepartment[this.resultprovince[i]] = result.map((item, index) => {

          if (item.provinceId == this.resultprovince[i]) {
            return {
              value: item.provinceDepartmentId.toString(),
              label: item.provinceName,
              //id : item.provinceId
            }
          } else {
            return {
              
            }
  
          }
        }) 

      }


      console.log(this.resultdepartment);
    })

    // this.resultprovince.forEach(element => {
    //   this.departmentservice.getdepartmentdata(element).subscribe(result => {
    //     // console.log('doo' , result)
    //     // for(var i = 0 ; i<result.length ; i++){
    //     //   // console.log(result[i].provincialDepartment.name)
    //     //   this.resultdepartment.push({value : result[i].provincialDepartment.id , label : result[i].provincialDepartment.name})
    //     // }
    //     // console.log('resultdepartment'  , this.resultdepartment)

    //    this.resultdepartment.push(result)
    //    for (var i = 0 ; i< this.resultcentralpolicy.centralPolicyProvinces.length ; i ++){
    //     //  console.log('nick' , 'nick')
    //     this.test.push([{value : this.resultdepartment[i][0].id , label:this.resultdepartment[i][1].id}])
    //     // for(var j = 0 ; j < this.resultdepartment[i].length ; j++){
    //     //           this.test.push([{value : this.resultdepartment[i][j].id , label : this.resultdepartment[i][j].provincialDepartment.name}])
    //     // }
    //    }
    //    console.log("test",this.test);    
    //   })

    // })
    // alert("FF: " + this.resultdepartment[0])



    // for(var i = 0 ; i< this.resultdepartment[0].length  ;i++){
    //           console.log('test')
    // }
    // console.log(this.resultdepartment)
    // console.log("benz" , this.benz )
  }
  storeSubject(value) {
    console.log(value);
    this.subjectservice.addSubject(value, this.id).subscribe(response => {
      // console.log("Response : ", response);

      this.Form.reset()
      window.history.back();
    })
  }

  appendquestionopen() {
    this.d.push(this.fb.group({
      questionopen: ''
    }))
  }
  remove(index: number) {
    this.d.removeAt(index);
  }
  removeX(index: number) {
    const control = <FormArray>this.Form.controls['inputquestionclose'];
    control.removeAt(index);
  }
  removeY(ix: number, iy: number) {
    const control = (<FormArray>this.Form.controls['inputquestionclose']).at(ix).get('inputanswerclose') as FormArray;
    control.removeAt(iy);
  }
}
