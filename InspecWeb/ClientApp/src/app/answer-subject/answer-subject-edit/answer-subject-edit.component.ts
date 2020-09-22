import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { AnswersubjectService } from 'src/app/services/answersubject.service';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, FormControl, Validators, FormArray } from '@angular/forms';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { SuggestionsubjectService } from 'src/app/services/suggestionsubject.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { NotofyService } from 'src/app/services/notofy.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-answer-subject-edit',
  templateUrl: './answer-subject-edit.component.html',
  styleUrls: ['./answer-subject-edit.component.css']
})
export class AnswerSubjectEditComponent implements OnInit {

  id: any
  subjectGroupId: any
  editid: any
  delid: any
  userid: string
  resultsubjectdetail: any = []
  resultanswer: any = []
  resultansweruserdetail: any = []
  resultsubquestion: any = []
  resultanswerstatus: any = []
  resultanswerfiles: any = []
  resultansweroutsider: any = []
  resultsubquestioncount: any = []
  province: any
  provinceid: any
  centralPolicyId: any
  centralPolicyProvinceId: any
  Form: FormGroup
  Formstatus: FormGroup
  Formfile: FormGroup
  form: FormGroup
  modalRef: BsModalRef;
  downloadUrl: any
  status: any
  listfiles: any = []
  fileData: any = [{ AnswerSubjectFile: '', fileDescription: '' }];
  printstatus: any

  constructor(
    private modalService: BsModalService,
    private answersubjectservice: AnswersubjectService,
    private suggestionservice: SuggestionsubjectService,
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private authorize: AuthorizeService,
    private spinner: NgxSpinnerService,
    @Inject('BASE_URL') baseUrl: string,
    private _NotofyService: NotofyService,
    private notificationService: NotificationService,
  ) {
    this.downloadUrl = baseUrl + '/Uploads';
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.form = this.fb.group({
      name: [''],
      files: [null]
    })
  }
  get f() { return this.Form.controls; }
  get t() { return this.f.result as FormArray; }

  get ff() { return this.Formfile.controls }
  get s() { return this.ff.fileData as FormArray }

  Test(index, value) {
    this.t.at(index).patchValue({
      Answer: value
    })
    // ('answer').patchValue(event.target.value)
    console.log(this.t.value);
  }

  ngOnInit() {
    this.printstatus = 0
    this.spinner.show();
    this.Form = this.fb.group({
      result: new FormArray([]),
    })
    this.Formfile = this.fb.group({
      // files: [null],
      // Type: ""
      fileData: new FormArray([]),
      fileType: new FormControl("เลือกประเภทเอกสารแนบ", [Validators.required]),
    })
    this.Formstatus = this.fb.group({
      Status: new FormControl("ร่างกำหนดการ", [Validators.required]),
    })
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
      })
    this.getSubjectdetail();
    this.getAnsweruser();
    this.getAnswerstatus();
    this.getAnswerfiles();
    this.getAnsweroutsider();
  }
  openModalEdit(template: TemplateRef<any>, id) {
    console.log(id);
    this.editid = id
    this.modalRef = this.modalService.show(template);
    this.getAnsweruserdetail(id);
  }
  openAddFile(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }
  openModalDelete(template: TemplateRef<any>, id) {
    console.log(id);
    this.delid = id
    this.modalRef = this.modalService.show(template);
  }
  getSubjectdetail() {
    this.answersubjectservice.getsubjectdetaildata(this.id).subscribe(result => {
      this.resultsubjectdetail = result
      this.subjectGroupId = this.resultsubjectdetail.subjectGroupId
      this.province = this.resultsubjectdetail.centralPolicyProvince.province.name
      this.provinceid = this.resultsubjectdetail.centralPolicyProvince.provinceId
      this.centralPolicyId = this.resultsubjectdetail.centralPolicyProvince.centralPolicyId
      this.centralPolicyProvinceId = this.resultsubjectdetail.centralPolicyProvinceId
      // this.resultsubquestion = this.resultsubjectdetail.subquestionCentralPolicyProvinces
      // this.loading = true
      console.log("123", this.subjectGroupId);
      // this.loading = true;
      this.spinner.hide();
      // this.addvalue();
    }
    )
  }
  getAnsweruser() {
    this.answersubjectservice.getAnsweruserlist(this.id, this.userid).subscribe(result => {
      this.resultanswer = result
    })
  }
  getAnsweruserdetail(id) {
    this.answersubjectservice.getAnsweruserdetail(id, this.userid).subscribe(result => {
      this.resultansweruserdetail = result
      this.resultsubquestion = this.resultansweruserdetail.subquestionCentralPolicyProvince
      console.log("resultsubquestion", this.resultsubquestion);
      this.addvalue();
    })
  }
  getAnswerstatus() {
    this.answersubjectservice.getAnswerstatus(this.id, this.userid).subscribe(result => {
      this.resultanswerstatus = result
      this.status = result.status
      console.log("status", this.status);
      // console.log("resultanswerstatus", this.resultanswerstatus);

    })
  }
  getAnswerfiles() {
    this.answersubjectservice.getAnswerfile(this.id, this.userid).subscribe(result => {
      this.resultanswerfiles = result
    })
  }
  getAnsweroutsider() {
    this.answersubjectservice.getAnsweroutsider(this.id, this.userid).subscribe(result => {
      this.resultansweroutsider = result
      this.resultsubquestioncount = this.resultansweroutsider.filter(
        (thing, i, arr) => arr.findIndex(t => t.subquestionCentralPolicyProvinceId === thing.subquestionCentralPolicyProvinceId) === i
      );
      console.log("uniqueresultsubquestioncount: ", this.resultsubquestioncount);
    })
  }
  addvalue() {
    this.Form.reset();
    this.t.clear();
    // for (let i = 0; i < this.resultsubquestion.length; i++) {
    var test: any[] = this.resultsubquestion.subquestionChoiceCentralPolicyProvinces
    this.t.push(this.fb.group({
      UserId: "",
      SubquestionCentralPolicyProvinceId: [this.resultsubquestion.id],
      Question: [this.resultsubquestion.name],
      Answer: [""],
      Choice: [test],
      Description: [""],
      Type: [this.resultsubquestion.type]
    }))
    // }
  }
  editAnswer(value) {
    this.answersubjectservice.editAnswer(this.t.value, this.editid).subscribe(result => {
      console.log("result", result);
      this.modalRef.hide()
      this.Form.reset()
      this.getSubjectdetail();
      this.getAnsweruser();
    })
  }
  editstatus(value) {
    // this.spinner.show();
    console.log("123", value.Status);
    this.answersubjectservice.editStatus(value, this.resultanswerstatus.id, this.subjectGroupId).subscribe(result => {
      if (value.Status == "ใช้งานจริง") {
        this.notificationService.addNotification(this.centralPolicyId, this.provinceid, this.userid, 6, this.subjectGroupId)
          .subscribe(response => {
            console.log("innoti", response);
          })
      }
      this.storefile()
      this._NotofyService.onSuccess("แก้ไขข้อมูล")
      this.Form.reset();
      this.Formstatus.reset();
      // this.spinner.hide();
      window.history.back();
    })
  }
  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.Formfile.patchValue({
      files: file,
      Type: "รูปภาพ"
    });
    this.Formfile.get('files').updateValueAndValidity()
  }
  uploadFile2(event) {
    var file = (event.target as HTMLInputElement).files;
    for (let i = 0, numFiles = file.length; i < numFiles; i++) {
      this.listfiles.push(file[i])
      this.s.push(this.fb.group({
        AnswerSubjectFile: file[i],
        fileDescription: '',
      }))
    }
    console.log("listfiles: ", this.Formfile.value);
    console.log("eiei: ", this.s.controls);


    this.form.patchValue({
      files: this.listfiles
    });
  }
  storefile() {
    this.answersubjectservice.addFiles(this.id, this.Formfile.value, this.userid).subscribe(response => {
      this.modalRef.hide();
      this.Formfile.reset();
      this.spinner.hide();
      window.history.back();
      // this.getAnswerfiles();
      // this.Formfile.value.files, this.Formfile.value
    })
  }
  DeleteFile(value) {
    console.log(value);
    this.answersubjectservice.deleteFile(value).subscribe(result => {
      this.modalRef.hide()
      this.getAnswerfiles();
    })
  }
  printPage() {
    this.printstatus = 1
    setTimeout(() => {
      this.printPage2()
    }, 1000);
  }
  printPage2() {
    window.print();
    setTimeout(() => {
      this.printstatus = 0
    }, 800);
  }
  back() {
    window.history.back();
  }
}
