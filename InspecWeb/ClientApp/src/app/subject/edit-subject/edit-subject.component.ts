import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { SubjectService } from 'src/app/services/subject.service';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { IOption } from 'ng-select';
import { IMyDate } from 'mydatepicker-th';
import { DepartmentService } from 'src/app/services/department.service';
import { SubquestionService } from 'src/app/services/subquestion.service';

@Component({
  selector: 'app-edit-subject',
  templateUrl: './edit-subject.component.html',
  styleUrls: ['./edit-subject.component.css']
})
export class EditSubjectComponent implements OnInit {

  private startDate: IMyDate = { year: 0, month: 0, day: 0 };
  private endDate: IMyDate = { year: 0, month: 0, day: 0 };
  EditForm: FormGroup;
  FormAddDate: FormGroup
  FormAddQuestionsopen: FormGroup
  FormAddQuestionsclose: FormGroup
  FormEditQuestions: FormGroup
  FormEditChoices: FormGroup
  FormAddChoices: FormGroup
  FormAddEditDepartment: FormGroup;
  FormAddDepartmentQuestion: FormGroup;
  Formfile: FormGroup;
  id: any
  delid: any
  editid: any
  editname: any
  downloadUrl: any
  resultsubjectdetail: any = []
  resultcentralpolicy: any = []
  resultprovince: any = []
  resultdsubjectid: any = []
  datetime: any = []
  question: any = []
  departments: any = []
  department: any = []
  filterboxdepartments: any = []
  centralpolicyid: any
  boxcount = 0
  subquestionCentralPolicyProvincesId: any
  // questionsopen: any = []
  // questionsclose: any = []
  times: IOption[] = [];
  timesselect: IOption[] = [];
  modalRef: BsModalRef;

  get f() { return this.FormAddQuestionsopen.controls; }
  get t() { return this.f.ProvincialDepartmentId as FormArray; }
  get ff() { return this.FormAddQuestionsclose.controls; }
  get tt() { return this.ff.ProvincialDepartmentId as FormArray; }

  constructor(
    private modalService: BsModalService,
    private fb: FormBuilder,
    private subjectservice: SubjectService,
    private subquestionservice: SubquestionService,
    private centralpolicyservice: CentralpolicyService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private spinner: NgxSpinnerService,
    private departmentservice: DepartmentService,
    @Inject('BASE_URL') baseUrl: string) {
    this.downloadUrl = baseUrl + '/Uploads';
    this.id = activatedRoute.snapshot.paramMap.get('id')
  }

  ngOnInit() {
    this.getSubjectDetail()
    this.EditForm = this.fb.group({
      name: new FormControl(null, [Validators.required]),
      centralPolicyDateId: new FormControl(null, [Validators.required]),
      status: new FormControl("ร่างกำหนดการ", [Validators.required]),
      // questionopen: new FormControl(null, [Validators.required]),
      // questionclose: new FormControl(null, [Validators.required]),
      // answerclose: new FormControl(null, [Validators.required]),
      // "provincelink": new FormControl(null, [Validators.required])
    })
    this.FormAddDepartmentQuestion = this.fb.group({
      inputsubjectdepartment: this.fb.array([
        this.initdepartment()
      ]),
    })
    this.Formfile = this.fb.group({
      centralpolicydateid: new FormControl(null, [Validators.required]),
      files: [null]
    })
  }
  initdepartment() {
    return this.fb.group({
      departmentId: [null, [Validators.required, Validators.pattern('[0-9]{3}')]],
      inputquestionopen: this.fb.array([
        this.initquestionopen()
      ]),
      inputquestionclose: this.fb.array([
        this.initquestionclose()
      ])
    })
  }
  initquestionopen() {
    return this.fb.group({
      questionopen: [null, [Validators.required, Validators.pattern('[0-9]{3}')]]

    })
  }
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
  getSubjectDetail() {
    this.subjectservice.getsubjectdetaildata(this.id)
      .subscribe(result => {
        this.resultsubjectdetail = result
        this.question = this.resultsubjectdetail.subquestionCentralPolicyProvinces
        this.departments = this.resultsubjectdetail.subquestionCentralPolicyProvinces
        this.centralpolicyid = this.resultsubjectdetail.centralPolicyProvince.centralPolicyId
        console.log("res: ", this.resultsubjectdetail);
        this.getCentralPolicyProvincesl()
        this.getboxsubquestion()
        this.getTimeCentralPolicy()
        this.resultsubjectdetail.subjectDateCentralPolicyProvinces.forEach(element => {
          // console.log("niklog", element.centralPolicyDateProvince);
          this.datetime.push(element.centralPolicyDateProvince.id)
        });
        this.EditForm.patchValue({
          name: this.resultsubjectdetail.name,
          // centralPolicyDateId: this.datetime
        })
        this.times = []
        for (var i = 0; i < this.resultsubjectdetail.subjectDateCentralPolicyProvinces.length; i++) {
          let d: Date = new Date(this.resultsubjectdetail.subjectDateCentralPolicyProvinces[i].centralPolicyDateProvince.startDate)
          // alert(this.resultsubject[0].centralPolicy.centralPolicyDates[i].startDate)
          let e: Date = new Date(this.resultsubjectdetail.subjectDateCentralPolicyProvinces[i].centralPolicyDateProvince.endDate)
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
            value: this.resultsubjectdetail.subjectDateCentralPolicyProvinces[i].centralPolicyDateProvince.id,
            label: test,
          })
        }
      })
  }
  getCentralPolicyProvincesl() {
    this.centralpolicyservice.getdetailcentralpolicydata(this.centralpolicyid)
      .subscribe(result => {
        this.resultprovince = result
      })
  }
  getboxsubquestion() {
    var departments = this.departments
    console.log("aaaaa: ", this.departments);
    var question = this.question
    this.filterboxdepartments = departments.filter(
      (thing, i, arr) => arr.findIndex(t => t.box === thing.box) === i
    ); //song
    console.log("CCCCCCC: ", this.filterboxdepartments);

    var test: any = [];

    this.departments.forEach(element => {
      element.subjectCentralPolicyProvinceGroups.forEach(element2 => {

        if (element.id == element2.subquestionCentralPolicyProvinceId) {
          test.push({
            departmentID: element2.provincialDepartment.id,
            departmentName: element2.provincialDepartment.name,
            question: element.name,
            type: element.type,
            subquestionChoice: element.subquestionChoiceCentralPolicyProvinces,
            box: element.box
          })
        }
      });
    });
    console.log("TEST: ", test);
  }

  getTimeCentralPolicy() {
    this.centralpolicyservice.getdetailcentralpolicydata(this.resultsubjectdetail.centralPolicyProvince.centralPolicyId).subscribe(result => {
      this.resultcentralpolicy = result
      this.timesselect = []


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


        this.timesselect.push({
          value: this.resultcentralpolicy.centralPolicyDates[i].id,
          label: test,
        })
        // console.log(this.resultcentralpolicy.centralPolicyDates[i].startDate);
      }

    })
  }
  openAddModalDate(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
    this.FormAddDate = this.fb.group({
      centralPolicyDateId: new FormControl(null, [Validators.required])
    })
  }
  openAddDepartmentModal(template: TemplateRef<any>, subquestionCentralPolicyProvincesId, inputbox) {
    this.subquestionCentralPolicyProvincesId = subquestionCentralPolicyProvincesId
    console.log(this.subquestionCentralPolicyProvincesId);
    console.log((inputbox));
    this.FormAddEditDepartment = this.fb.group({
      DepartmentId: new FormControl(null, [Validators.required]),
      Box: inputbox
    })
    this.departmentservice.getalldepartdata().subscribe(res => {
      this.department = res.map((item, index) => {
        return {
          value: item.id,
          label: item.name
        }
      })
      this.modalRef = this.modalService.show(template);
    })
  }
  openAddModalQuestionsopen(template: TemplateRef<any>, inputbox) {
    console.log("box:", inputbox);
    console.log("filterboxdepartments:", this.filterboxdepartments);
    this.modalRef = this.modalService.show(template);
    this.FormAddQuestionsopen = this.fb.group({
      subjectId: this.id,
      box: inputbox,
      type: "คำถามปลายเปิด",
      name: new FormControl(null, [Validators.required]),
      ProvincialDepartmentId: new FormArray([])
    })
    this.filterboxdepartments.forEach(element => {
      element.subjectCentralPolicyProvinceGroups.forEach(element2 => {
        if (element.box == inputbox) {
          console.log("testbenz", element2.provincialDepartmentId)
          this.t.push(this.fb.group({ ProvincialDepartmentId: element2.provincialDepartmentId }))
        }
        console.log("element2", element2);
      })
      console.log("element", element);
    })
    console.log(this.FormAddQuestionsopen.value);
  }

  openAddModalQuestionsclose(template: TemplateRef<any>, inputbox) {
    console.log("box:", inputbox);
    this.modalRef = this.modalService.show(template);
    this.FormAddQuestionsclose = this.fb.group({
      subjectId: this.id,
      box: inputbox,
      type: "คำถามปลายปิด",
      name: new FormControl(null, [Validators.required]),
      ProvincialDepartmentId: new FormArray([]),
      inputanswerclose: this.fb.array([
        this.initanswerclose()
      ])
    })
    this.filterboxdepartments.forEach(element => {
      element.subjectCentralPolicyProvinceGroups.forEach(element2 => {
        if (element.box == inputbox) {
          console.log("testbenz", element2.provincialDepartmentId)
          this.tt.push(this.fb.group({ ProvincialDepartmentId: element2.provincialDepartmentId }))
        }
        console.log("element2", element2);
      })
      console.log("element", element);
    })
    console.log(this.FormAddQuestionsclose.value);
  }
  openAddModalChoices(template: TemplateRef<any>, id) {
    this.modalRef = this.modalService.show(template);
    this.FormAddChoices = this.fb.group({
      subquestionid: id,
      name: new FormControl(null, [Validators.required]),
    })
  }
  openAddFile(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
    // this.FormAddDate = this.fb.group({
    //   centralPolicyDateId: new FormControl(null, [Validators.required])
    // })
  }
  openEditmodalQuestions(template: TemplateRef<any>, id, name) {
    console.log(id);
    console.log(name);
    this.editid = id
    this.editname = name
    this.modalRef = this.modalService.show(template);
    this.FormEditQuestions = this.fb.group({
      name: new FormControl(null, [Validators.required]),
    })
    this.FormEditQuestions.patchValue({
      name: name
    })
  }
  openEditmodalChoices(template: TemplateRef<any>, id, name) {
    console.log(id);
    console.log(name);
    this.editid = id
    this.editname = name
    this.modalRef = this.modalService.show(template);
    this.FormEditChoices = this.fb.group({
      name: new FormControl(null, [Validators.required]),
    })
    this.FormEditChoices.patchValue({
      name: name
    })
  }
  openModalDelete(template: TemplateRef<any>, id) {
    console.log(id);
    this.delid = id
    this.modalRef = this.modalService.show(template);
  }
  openModal(template: TemplateRef<any>) {
    // this.modalRef = this.modalService.show(template);
    // await this.getMinistryPeople();
    // await this.getUserPeople();
    // this.getDepartmentdata();
    this.departmentservice.getalldepartdata().subscribe(res => {
      this.department = res.map((item, index) => {
        return {
          value: item.id,
          label: item.name
        }
      })
      this.modalRef = this.modalService.show(template);
    })
  }

  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.Formfile.patchValue({
      files: file
    });
    this.Formfile.get('files').updateValueAndValidity()

  }

  AddDate(value) {
    console.log(value);
    console.log(this.resultsubjectdetail.id);
    console.log("del", this.times);
    this.subjectservice.adddeleteDate(this.times, value, this.resultsubjectdetail.id).subscribe(result => {
      this.FormAddDate.reset()
      this.modalRef.hide()
      this.getSubjectDetail()
    })
  }
  AddDepartment(value) {
    console.log(value);
    this.centralpolicyservice.addeditDepartment(value, this.id).subscribe(response => {
      console.log(value);
      this.FormAddEditDepartment.reset()
      this.modalRef.hide()
      this.getSubjectDetail();
    })
  }
  AddQuestionsopen(value) {
    console.log(value);
    this.subquestionservice.addSubquestionopen(value).subscribe(result => {
      console.log(result);
      this.FormAddQuestionsopen.reset()
      this.modalRef.hide()
      this.getSubjectDetail()
    })
  }
  AddQuestionsclose(value) {
    console.log(value);
    this.subquestionservice.addSubquestionclose(value).subscribe(result => {
      console.log(result);
      this.FormAddQuestionsclose.reset()
      this.modalRef.hide()
      this.getSubjectDetail()
    })
  }
  AddChoices(value) {
    console.log(value);
    this.subjectservice.addChoices(value).subscribe(response => {
      this.FormAddChoices.reset()
      this.modalRef.hide()
      this.getSubjectDetail()
    })
  }
  AddDepartmentQuestion(value) {
    var Boxcount
    for (var i = 0; i < this.filterboxdepartments.length; i++) {
      Boxcount = this.filterboxdepartments[i].box + 1
    }
    console.log("Boxcount", Boxcount);
    console.log(value);
    this.subjectservice.AddDepartmentQuestion(value, Boxcount, this.id).subscribe(result => {
      console.log(result);
      this.FormAddDepartmentQuestion.reset()
      this.modalRef.hide()
      this.getSubjectDetail()
    })
  }
  AddFile() {
    this.resultdsubjectid = []
    this.resultdsubjectid.push(this.id)
    //  alert(this.resultdsubjectid)
    this.subjectservice.addFiles(this.resultdsubjectid, this.Formfile.value.files).subscribe(result => {
      console.log(result);
      this.Formfile.reset();
      this.modalRef.hide()
      this.getSubjectDetail()
    })
  }

  DeleteQuestionsopen(value) {
    console.log(value);
    this.subjectservice.deleteSubquestionopen(value).subscribe(response => {
      this.modalRef.hide()
      this.getSubjectDetail()
    })
  }
  DeleteChoices(value) {
    console.log(value);
    this.subjectservice.deleteChoices(value).subscribe(response => {
      this.modalRef.hide()
      this.getSubjectDetail()
    })
  }
  DeleteFile(value) {
    console.log(value);
    this.subjectservice.deleteFile(value).subscribe(response => {
      this.modalRef.hide()
      this.getSubjectDetail()
    })
  }

  EditSubject(value, id) {
    // this.EditForm.reset();
    console.log(id);
    console.log(value);
    console.log("map: ", this.resultsubjectdetail);
    var CentralPolicyDateId: any = []
    var departmentId: any = []
    CentralPolicyDateId = this.resultsubjectdetail.subjectDateCentralPolicyProvinces.map((item, index) => {
      return {
        centralpolicydateid: item.id
      }
    })
    departmentId = this.filterboxdepartments.subjectCentralPolicyProvinceGroups.map((item, index) => {
      return {
        departmentId: item.provincialDepartment.id
      }
    })
    console.log(CentralPolicyDateId);
    console.log(departmentId);
    

    // this.subjectservice.editSubject(value, id).subscribe(response => {
    //   this.EditForm.reset()
    //   // alert("test")
    //   window.history.back()
    // })
  }
  EditQuestions(value, editid) {
    console.log(editid);
    console.log(value);
    this.subjectservice.editSubquestionopen(value, editid).subscribe(response => {
      this.FormEditQuestions.reset()
      this.modalRef.hide()
      this.getSubjectDetail()
    })
  }
  EditChoices(value, editid) {
    console.log(editid);
    console.log(value);
    this.subjectservice.editChoices(value, editid).subscribe(response => {
      this.FormEditChoices.reset()
      this.modalRef.hide()
      this.getSubjectDetail()
    })
  }

  addX() {
    const control = <FormArray>this.FormAddQuestionsclose.controls['inputanswerclose'];
    control.push(this.initanswerclose());
  }
  addWW(iv) {
    const control = (<FormArray>this.FormAddDepartmentQuestion.controls['inputsubjectdepartment']).at(iv).get('inputquestionopen') as FormArray;
    control.push(this.initquestionopen());
  }
  addXX(iv) {
    const control = (<FormArray>this.FormAddDepartmentQuestion.controls['inputsubjectdepartment']).at(iv).get('inputquestionclose') as FormArray;
    control.push(this.initquestionclose());
  }
  addYY(iv, ix) {
    const control = ((<FormArray>this.FormAddDepartmentQuestion.controls['inputsubjectdepartment']).at(iv).get('inputquestionclose') as FormArray).at(ix).get('inputanswerclose') as FormArray;
    control.push(this.initanswerclose());
  }
  removeX(index: number) {
    const control = <FormArray>this.FormAddQuestionsclose.controls['inputanswerclose'];
    control.removeAt(index);
  }
  removeWW(iv: number, iw: number) {
    const control = (<FormArray>this.FormAddDepartmentQuestion.controls['inputsubjectdepartment']).at(iv).get('inputquestionopen') as FormArray;
    control.removeAt(iw);
  }
  removeXX(iv: number, ix: number) {
    const control = (<FormArray>this.FormAddDepartmentQuestion.controls['inputsubjectdepartment']).at(iv).get('inputquestionclose') as FormArray;
    control.removeAt(ix);
  }
  removeYY(iv: number, ix: number, iy: number) {
    const control = ((<FormArray>this.FormAddDepartmentQuestion.controls['inputsubjectdepartment']).at(iv).get('inputquestionclose') as FormArray).at(ix).get('inputanswerclose') as FormArray;
    control.removeAt(iy);
  }
  back() {
    window.history.back();
  }
}
