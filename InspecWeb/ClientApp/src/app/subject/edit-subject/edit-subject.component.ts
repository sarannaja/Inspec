import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { SubjectService } from 'src/app/services/subject.service';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { IOption } from 'ng-select';
import { IMyDate } from 'mydatepicker-th';

@Component({
  selector: 'app-edit-subject',
  templateUrl: './edit-subject.component.html',
  styleUrls: ['./edit-subject.component.css']
})
export class EditSubjectComponent implements OnInit {

  private startDate: IMyDate = { year: 0, month: 0, day: 0 };
  private endDate: IMyDate = { year: 0, month: 0, day: 0 };
  EditForm: FormGroup;
  FormAddQuestionsopen: FormGroup
  FormAddQuestionsclose: FormGroup
  FormEditQuestions: FormGroup
  FormEditChoices: FormGroup
  FormAddChoices: FormGroup
  id: any
  delid: any
  editid: any
  editname: any
  resultsubjectdetail: any = []
  resultcentralpolicy: any = []
  datetime: any = []
  questionsopen: any = []
  questionsclose: any = []
  times: IOption[] = [];
  modalRef: BsModalRef;

  constructor(
    private modalService: BsModalService,
    private fb: FormBuilder,
    private subjectservice: SubjectService,
    private centralpolicyservice: CentralpolicyService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private spinner: NgxSpinnerService) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
  }

  ngOnInit() {
    this.getSubjectDetail()
    this.EditForm = this.fb.group({
      name: new FormControl(null, [Validators.required]),
      centralPolicyDateId: new FormControl(null, [Validators.required]),
      // questionopen: new FormControl(null, [Validators.required]),
      // questionclose: new FormControl(null, [Validators.required]),
      // answerclose: new FormControl(null, [Validators.required]),
      // "provincelink": new FormControl(null, [Validators.required])
    })

  }
  getSubjectDetail() {
    this.subjectservice.getsubjectdetaildata(this.id)
      .subscribe(result => {
        this.resultsubjectdetail = result
        this.questionsopen = this.resultsubjectdetail.subquestions
        console.log("res: ", this.resultsubjectdetail);

        this.getTimeCentralPolicy()
        this.resultsubjectdetail.subjectDates.forEach(element => {
          // console.log(element.centralPolicyDate.id);
          this.datetime.push(element.centralPolicyDate.id)
        });
        this.EditForm.patchValue({
          name: this.resultsubjectdetail.name
        })
      })
  }
  getTimeCentralPolicy() {
    this.centralpolicyservice.getdetailcentralpolicydata(this.resultsubjectdetail.centralPolicyId).subscribe(result => {
      this.resultcentralpolicy = result
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
  openAddModalQuestionsopen(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
    this.FormAddQuestionsopen = this.fb.group({
      subjectId: this.id,
      name: new FormControl(null, [Validators.required]),
    })
  }
  openAddModalQuestionsclose(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
    this.FormAddQuestionsclose = this.fb.group({
      subjectId: this.id,
      name: new FormControl(null, [Validators.required]),
      inputanswerclose: this.fb.array([
        this.initanswerclose()
      ])

    })
  }
  openAddModalChoices(template: TemplateRef<any>, id ){
    this.modalRef = this.modalService.show(template);
    this.FormAddChoices = this.fb.group({
      subquestionid: id,
      name: new FormControl(null, [Validators.required]),
    })
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
  initanswerclose() {
    return this.fb.group({
      answerclose: [null, [Validators.required, Validators.pattern('[0-9]{3}')]],
    })
  }
  addX() {
    const control = <FormArray>this.FormAddQuestionsclose.controls['inputanswerclose'];
    control.push(this.initanswerclose());
  }
  removeX(index: number) {
    const control = <FormArray>this.FormAddQuestionsclose.controls['inputanswerclose'];
    control.removeAt(index);
  }
  AddQuestionsopen(value) {
    console.log(value);
    this.subjectservice.addSubquestionopen(value).subscribe(response => {
      console.log(value);
      this.FormAddQuestionsopen.reset()
      this.modalRef.hide()
      // this.loading = false
      this.getSubjectDetail()
    })
  }
  AddQuestionsclose(value) {
    console.log(value);
    this.subjectservice.addSubquestionclose(value).subscribe(response => {
      console.log(value);
      this.FormAddQuestionsclose.reset()
      this.modalRef.hide()
      // this.loading = false
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
  DeleteQuestionsopen(value) {
    console.log(value);
    this.subjectservice.deleteSubquestionopen(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.getSubjectDetail()
    })
  }
  DeleteChoices(value) {
    console.log(value);
    this.subjectservice.deleteChoices(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.getSubjectDetail()
    })
  }
  EditSubject(value, id) {
    console.log(id);
    console.log(value);
    this.subjectservice.editSubject(value, id).subscribe(response => {
      this.EditForm.reset()
      // alert("test")
      window.history.back()
    })
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
  back() {
    window.history.back();
  }
}
