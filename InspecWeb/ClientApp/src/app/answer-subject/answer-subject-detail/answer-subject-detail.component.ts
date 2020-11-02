import { Component, Inject, OnInit } from '@angular/core';
import { AnswersubjectService } from 'src/app/services/answersubject.service';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, FormControl, Validators, FormArray } from '@angular/forms';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { SuggestionsubjectService } from 'src/app/services/suggestionsubject.service';
import { NotofyService } from 'src/app/services/notofy.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-answer-subject-detail',
  templateUrl: './answer-subject-detail.component.html',
  styleUrls: ['./answer-subject-detail.component.css']
})
export class AnswerSubjectDetailComponent implements OnInit {

  id: any
  subjectGroupId: any
  userid: string
  resultsubjectdetail: any = []
  resultsubquestion: any = []
  resultansweroutsider: any = []
  resultsubquestioncount: any = []
  Form: FormGroup;
  Formfile: FormGroup;
  Formstatus: FormGroup;
  form: FormGroup;
  province: any
  provinceid: any
  centralPolicyId: any
  centralPolicyProvinceId: any
  answar = [{ test: "1212", benz: "121212" }]
  listfiles: any = []
  fileData: any = [{ AnswerSubjectFile: '', fileDescription: '' }];
  submitted = false;
  downloadUrl: any;

  constructor(
    private answersubjectservice: AnswersubjectService,
    private suggestionservice: SuggestionsubjectService,
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private authorize: AuthorizeService,
    private spinner: NgxSpinnerService,
    private _NotofyService: NotofyService,
    private notificationService: NotificationService,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.form = this.fb.group({
      name: [''],
      files: [null]
    })
    this.downloadUrl = baseUrl + '/Uploads';
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
    this.getSubjectdetail()
    this.getAnsweroutsider()
  }
  getSubjectdetail() {
    this.answersubjectservice.getsubjectdetaildata(this.id).subscribe(result => {
      this.resultsubjectdetail = result
      this.subjectGroupId = this.resultsubjectdetail.subjectGroupId
      this.province = this.resultsubjectdetail.centralPolicyProvince.province.name
      this.provinceid = this.resultsubjectdetail.centralPolicyProvince.provinceId
      this.centralPolicyId = this.resultsubjectdetail.centralPolicyProvince.centralPolicyId
      this.centralPolicyProvinceId = this.resultsubjectdetail.centralPolicyProvinceId
      this.resultsubquestion = this.resultsubjectdetail.subquestionCentralPolicyProvinces
      // this.loading = true
      console.log("123", this.subjectGroupId);
      // this.loading = true;
      this.spinner.hide();
      this.addvalue();
    }
    )
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
    for (let i = 0; i < this.resultsubquestion.length; i++) {
      var test: any[] = this.resultsubquestion[i].subquestionChoiceCentralPolicyProvinces
      this.t.push(this.fb.group({
        UserId: "",
        SubquestionCentralPolicyProvinceId: [this.resultsubquestion[i].id],
        AnswerSubquestionStatusId: [],
        Question: [this.resultsubquestion[i].name],
        Answer: ["", [Validators.required]],
        Choice: [test],
        Description: ["", Validators.required],
        Type: [this.resultsubquestion[i].type]
      }))
    }
    console.log(this.t.value);

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
  // uploadSignatureFile(event) {
  //   const file = (event.target as HTMLInputElement).files;
  //   this.Formfile.patchValue({
  //     files: file,
  //     Type: "ลายมือชื่อ"
  //   });
  //   this.Formfile.get('files').updateValueAndValidity()

  // }
  storeanswer(value, value2) {
    this.submitted = true;
    if (this.Form.invalid) {
      console.log("in1");
      return;
    } else {
      this.spinner.show();
      this.storestatus(value2)
    }
    // this.storeansweruser(value, value2)
  }
  storestatus(value2) {
    this.answersubjectservice.addStatus(value2, this.id, this.userid, this.subjectGroupId).subscribe(result => {
      console.log("result", result.id);
      var statusid = result.id
      if (value2.Status == "ใช้งานจริง") {
        this.notificationService.addNotification(this.centralPolicyId, this.provinceid, this.userid, 6, this.subjectGroupId,null)
          .subscribe(response => {
            console.log("innoti", response);
          })
      }
      this.storeansweruser(statusid)
    })
  }
  storeansweruser(statusid) {
    // console.log(this.userid);

    for (let i = 0; i < this.t.value.length; i++) {

      this.t.at(i).patchValue({
        UserId: this.userid,
        AnswerSubquestionStatusId: statusid
      })
    }
    console.log(this.t.value);
    this.answersubjectservice.addAnswer(this.t.value).subscribe(result => {
      console.log("result", result);
      // this.storestatus(value2)
      this.storefile()
      this._NotofyService.onSuccess("เพื่มข้อมูล")
    })
  }
  storefile() {
    this.answersubjectservice.addFiles(this.id, this.Formfile.value, this.userid).subscribe(response => {
      this.Form.reset();
      this.Formfile.reset();
      this.spinner.hide();
      window.history.back();
      // this.Formfile.value.files, this.Formfile.value
    })
  }
  back() {
    window.history.back();
  }
}
