import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray } from '@angular/forms';
import { SubquestionService } from '../services/subquestion.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CentralpolicyService } from '../services/centralpolicy.service';

import { IMyDate } from 'mydatepicker-th';
import { SubjectService } from '../services/subject.service';
import { DepartmentService } from '../services/department.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { NotofyService } from '../services/notofy.service';

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
  resultdsubjectid: any = []
  subjectdepartmentId: Array<any> = []
  temp = []
  test = []
  id
  userid
  indexfiles
  name: any
  modalRef: BsModalRef;
  Form: FormGroup;
  Formfile: FormGroup;
  form: FormGroup;
  times: any[] = [];
  selectdatacentralpolicy: Array<any>
  listfiles: any = []
  fileData: any = [{ SubjectFile: '', fileDescription: '' }];
  submitted = false;

  constructor(
    private modalService: BsModalService,
    private fb: FormBuilder,
    private centralpolicyservice: CentralpolicyService,
    private subjectservice: SubjectService,
    private departmentservice: DepartmentService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private spinner: NgxSpinnerService,
    private authorize: AuthorizeService,
    private _NotofyService: NotofyService
  ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.name = activatedRoute.snapshot.paramMap.get('name')
    this.form = this.fb.group({
      name: [''],
      files: [null]
    })
  }

  ngOnInit() {
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
        // alert(this.userid)
      })

    this.Form = this.fb.group({
      name: new FormControl("", [Validators.required]),
      centralpolicydateid: new FormControl("", [Validators.required]),
      status: new FormControl("ใช้งานจริง", [Validators.required]),
      explanation: new FormControl(""),
      inputsubjectdepartment: this.fb.array([
        this.initdepartment()
      ]),
    })
    this.Formfile = this.fb.group({
      centralpolicydateid: new FormControl(null, [Validators.required]),
      // files: [null]
      fileData: this.fb.array([]),
      fileType: new FormControl("เลือกประเภทเอกสารแนบ", [Validators.required]),
    })
    this.getTimeCentralPolicy()
    // this.d.push(this.fb.group({
    //   questionopen: '',
    // }))

    console.log("55555", this.Form.value);
  }
  get f() { return this.Form.controls }
  get d() { return this.f.inputquestionopen as FormArray }
  get x() { return this.initanswerclose() }

  get ff() { return this.Formfile.controls }
  get s() { return this.ff.fileData as FormArray }

  get fv() { return this.f.inputsubjectdepartment }
  get tv() { return this.fv as FormArray }

  getinputquestionclose(index = 0, indextvff = 0) {
    let tvf = (this.tv.controls[index].get('inputquestionclose') as FormArray).controls
    let tvftvff = (tvf[indextvff].get('inputanswerclose') as FormArray).controls
    return { tvf, tvftvff }
  }

  openModalDelete(template: TemplateRef<any>, i) {
    console.log(i);
    this.indexfiles = i
    this.modalRef = this.modalService.show(template);
  }

  initdepartment() {
    return this.fb.group({
      departmentId: [null, [Validators.required]],
      // inputquestionopen: this.fb.array([
      //   this.initquestionopen()
      // ]),
      inputquestionclose: this.fb.array([
        this.initquestionclose()
      ])
    })
  }
  initquestionopen() {
    return this.fb.group({
      questionopen: [null, [Validators.required]]

    })
  }
  initquestionclose() {
    return this.fb.group({
      questionclose: [null, [Validators.required]],
      inputanswerclose: this.fb.array([
        this.initanswerclose()
      ])
    });
  }
  initanswerclose() {
    return this.fb.group({
      answerclose: [""],
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
    // this.resultprovince.forEach((element, index) => {
    //   console.log('element', element);

    this.departmentservice.getalldepartdata()
      .subscribe(result => {
        this.temp = result.map((item, index) => {
          return {
            value: item.id,
            label: item.name,
          }
        })
        console.log(result);
      })
    // });
  }
  storeSubject(value) {
    console.log(value);
    this.submitted = true;
    if (this.Form.invalid) {
      console.log("in1");
      return;
    } else {
      this.spinner.show();
      this.subjectservice.addSubject(value, this.id, this.userid).subscribe(response => {
        console.log("Response : ", response);
        this.resultdsubjectid.push(response.getSubjectID)
        response.termsList.forEach(element => {
          this.resultdsubjectid.push(element)
        });
        console.log("Response2 : ", this.resultdsubjectid);

        if (response.getSubjectID != 0) {
          this.storefiles();
        } else {
          this.Form.reset();
          this.Formfile.reset();
          this.spinner.hide();
          this._NotofyService.onSuccess("เพื่มข้อมูล")
          window.history.back();
        }
      })
    }

  }
  uploadFile(event) {
    var file = (event.target as HTMLInputElement).files;
    for (let i = 0, numFiles = file.length; i < numFiles; i++) {
      this.listfiles.push(file[i])
    }
    console.log("test", this.listfiles[0].name);

    this.Formfile.patchValue({
      files: this.listfiles
    });
    // console.log("listfiles", this.Formfile.get('files'));
    // this.Formfile.get('files').updateValueAndValidity()

  }
  uploadFile2(event) {
    var file = (event.target as HTMLInputElement).files;
    for (let i = 0, numFiles = file.length; i < numFiles; i++) {
      this.listfiles.push(file[i])
      this.s.push(this.fb.group({
        SubjectFile: file[i],
        fileDescription: '',
      }))
      // console.log(this.);

    }
    console.log("listfiles: ", this.Formfile.value);
    console.log("eiei: ", this.s.controls);


    this.form.patchValue({
      files: this.listfiles
    });
  }
  storefiles() {
    this.subjectservice.addFiles(this.resultdsubjectid, this.Formfile.value).subscribe(response => {
      this.Form.reset();
      this.Formfile.reset();
      this.spinner.hide();
      this._NotofyService.onSuccess("เพื่มข้อมูล")
      window.history.back();
    })
  }

  DeleteFile(value) {
    console.log(value);
    this.listfiles.splice(value, 1);
    console.log(this.listfiles);
    this.modalRef.hide()
  }
  // appendquestionopen() {
  //   this.d.push(this.fb.group({
  //     questionopen: ''
  //   }))
  // }
  addV() {
    const control = <FormArray>this.Form.controls['inputsubjectdepartment'];
    control.push(this.initdepartment());
  }
  addW(iv) {
    const control = (<FormArray>this.Form.controls['inputsubjectdepartment']).at(iv).get('inputquestionopen') as FormArray;
    control.push(this.initquestionopen());
  }
  addX(iv) {
    const control = (<FormArray>this.Form.controls['inputsubjectdepartment']).at(iv).get('inputquestionclose') as FormArray;
    control.push(this.initquestionclose());
  }
  addY(iv, ix) {
    const control = ((<FormArray>this.Form.controls['inputsubjectdepartment']).at(iv).get('inputquestionclose') as FormArray).at(ix).get('inputanswerclose') as FormArray;
    control.push(this.initanswerclose());
  }
  // remove(index: number) {
  //   this.d.removeAt(index);
  // }
  removeV(index: number) {
    const control = <FormArray>this.Form.controls['inputsubjectdepartment'];
    control.removeAt(index);
  }
  removeW(iv: number, iw: number) {
    const control = (<FormArray>this.Form.controls['inputsubjectdepartment']).at(iv).get('inputquestionopen') as FormArray;
    control.removeAt(iw);
  }
  removeX(iv: number, ix: number) {
    const control = (<FormArray>this.Form.controls['inputsubjectdepartment']).at(iv).get('inputquestionclose') as FormArray;
    control.removeAt(ix);
  }
  removeY(iv: number, ix: number, iy: number) {
    const control = ((<FormArray>this.Form.controls['inputsubjectdepartment']).at(iv).get('inputquestionclose') as FormArray).at(ix).get('inputanswerclose') as FormArray;
    control.removeAt(iy);
  }
  back() {
    window.history.back();
  }
}
