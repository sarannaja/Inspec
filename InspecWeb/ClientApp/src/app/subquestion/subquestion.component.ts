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
  resultprovince: any[] = []
  resultdepartment = []
  subjectdepartmentId: Array<any> = []
  temp = []
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
      subjectdepartment: this.subjectdepartmentId,
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
  // addsubjectdepartment(value) {
  //   console.log(value.value);
  //   if (this.subjectdepartmentId.length == 0) {
  //     this.subjectdepartmentId.push(value.value)
  //     console.log("in");
  //   } else {
  //     this.subjectdepartmentId.forEach((item, index) => {
  //       if (item != value.value) {
  //         this.subjectdepartmentId.push(value.value)
  //         console.log("inpush" + index);

  //       } else {
  //         this.subjectdepartmentId.splice(index, 1)
  //         console.log("insplice" + index);
  //       }
  //     })
  //   }
  //   // this.subjectdepartmentId.push(value.value)
  //   console.log("value", this.subjectdepartmentId);
  // }
  addsubjectdepartment2(value, input) {
    // var subject = value.vaule

    if (input == 'add') {
      this.subjectdepartmentId = this.addSubjectdepartments(this.subjectdepartmentId, value)
      console.log(this.subjectdepartmentId);
    } else {
      this.subjectdepartmentId = this.removeSubjectdepartments(this.subjectdepartmentId, value)
      console.log(this.subjectdepartmentId);

    }


  }

  removeSubjectdepartments(array: any, value) {
    var s = new Set(array);
    s.delete(value);
    return Array.from(s);
  }

  addSubjectdepartments(array: any, value) {
    var s = new Set(array);
    s.add(value);
    return Array.from(s);
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
    this.resultprovince.forEach((element, index) => {
      console.log('element', element);

      this.departmentservice.getdepartmentdata(element)
        .subscribe(result => {
          console.log("Result : " + index, result);
          this.temp[index] = result.map((item, index) => {
            return {
              value: item.id,
              label: item.provincialDepartment.name,
              provincialDepartmentID: item.provincialDepartmentID,
              provinceId: item.provinceId
            }
          })
          console.log(this.temp[index]);
        })
    });
  }
  storeSubject(value) {
    console.log(value);
    this.subjectservice.addSubject(value, this.id).subscribe(response => {
      //this.subjectdepartmentId
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
