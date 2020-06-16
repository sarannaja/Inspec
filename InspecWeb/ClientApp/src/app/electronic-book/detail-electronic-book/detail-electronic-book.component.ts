import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { IOption } from 'ng-select';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { UserService } from 'src/app/services/user.service';
import { SubjectService } from 'src/app/services/subject.service';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ElectronicbookService } from 'src/app/services/electronicbook.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DepartmentService } from 'src/app/services/department.service';

@Component({
  selector: 'app-detail-electronic-book',
  templateUrl: './detail-electronic-book.component.html',
  styleUrls: ['./detail-electronic-book.component.css']
})
export class DetailElectronicBookComponent implements OnInit {

  resultuser: any = []
  resultpeople: any = []
  resultministrypeople: any = []
  resultdetailcentralpolicy: any = []
  resultcentralpolicyuser: any = []
  resultdetailcentralpolicyprovince: any = []
  UserPeopleId: any;
  // UserMinistryId: any;
  id: any;
  elecId: any;
  Form: FormGroup;
  EditForm: FormGroup;
  EditForm2: FormGroup;
  Form4: FormGroup;
  selectpeople: Array<IOption>
  selectministrypeople: Array<IOption>
  modalRef: BsModalRef;
  editid: any
  subquestionclosename: any
  subquestionclosechoicename: any
  downloadUrl: any
  loading = false;
  resultelectronicbookdetail: any = [];
  centralPolicyUserId: any;
  detailForm: FormGroup;
  resultStatus: any = [];
  form: FormGroup;
  fileStatus = false;
  resultElecFile: any = [];
  resultElecFile2: any = [];
  delid: any;
  resultreport: any = [];
  provincename
  provinceid
  resultdate: any = []
  electronicbookid
  carlendarFile: any = [];
  signatureFile: any = [];
  electronikbookFile: any = [];
  policyDropdown: Array<IOption>
  urllink: any;
  subjectCentralPolicyID: any;
  editSuggestionForm: FormGroup;
  userid
  role_id
  reportBody: any = [];
  show = false;
  showIndex: any;
  resultreportnum = 0;
  answerPeople: any = [];
  commentData: any = [];
  ProceedData:any = [];
  answerData: any = [];
  electOwnData: any = [];

  signatureFile2: any = [];
  adminProvinceForm: FormGroup;

  subjectid
  department: any = []
  Form2: FormGroup;
  exportData: any = [];

  constructor(
    private fb: FormBuilder,
    private modalService: BsModalService,
    private centralpolicyservice: CentralpolicyService,
    private userservice: UserService,
    private subjectservice: SubjectService,
    private activatedRoute: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private electronicBookService: ElectronicbookService,
    private authorize: AuthorizeService,
    private notificationService: NotificationService,
    private departmentService: DepartmentService,
    @Inject('BASE_URL') baseUrl: string) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.elecId = activatedRoute.snapshot.paramMap.get('electronicBookId')
    // this.centralPolicyUserId = activatedRoute.snapshot.paramMap.get('centralPolicyUserId')
    this.downloadUrl = baseUrl + '/Uploads';
    this.urllink = baseUrl + 'answersubject/outsider/';

    this.form = this.fb.group({
      files: [null],
    })
  }

  ngOnInit() {
    console.log("ELECTID: ", this.id);

    this.spinner.show();

    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
        // alert(this.userid)
        this.userservice.getuserfirstdata(this.userid)
          .subscribe(result => {
            // this.resultuser = result;
            //console.log("test" , this.resultuser);
            this.role_id = result[0].role_id
            // alert(this.role_id)
          })
      })

    this.Form = this.fb.group({
      UserPeopleId: new FormControl(null, [Validators.required]),
      questionPeople: new FormControl(null, [Validators.required]),
      // UserMinistryId: new FormControl(null, [Validators.required]),
    })

    this.Form2 = this.fb.group({
      DepartmentId: new FormControl(null, [Validators.required]),
      // UserMinistryId: new FormControl(null, [Validators.required]),
    })
    this.Form4 = this.fb.group({
      proceed: new FormControl(null, [Validators.required]),
    })
    this.adminProvinceForm = this.fb.group({
      provinceSuggestion: new FormControl(null, [Validators.required]),
    });

    this.detailForm = this.fb.group({
      eBookDetail: new FormControl(null, [Validators.required]),
      Status: new FormControl(null, [Validators.required]),
      checkDetail: new FormControl(null, [Validators.required]),
      Problem: new FormControl(null, [Validators.required]),
      Suggestion: new FormControl(null, [Validators.required]),
      PolicyIssue: new FormControl(null, [Validators.required]),
    })

    this.editSuggestionForm = this.fb.group({
      checkDetail: new FormControl(null, [Validators.required]),
      Problem: new FormControl(null, [Validators.required]),
      Suggestion: new FormControl(null, [Validators.required]),
    })

    this.EditForm = this.fb.group({
      subquestionclose: new FormControl(),
    })

    this.EditForm2 = this.fb.group({
      subquestionclosechoice: new FormControl(),
    })

    this.userservice.getuserdata(7).subscribe(result => {
      // alert(JSON.stringify(result))
      this.resultpeople = result
      console.log(this.resultpeople);
      this.selectpeople = this.resultpeople.map((item, index) => {
        return { value: item.id, label: item.name }
      })
    })
    this.userservice.getuserdata(6).subscribe(result => {
      // alert(JSON.stringify(result))
      this.resultministrypeople = result
      console.log(this.resultministrypeople);
      this.selectministrypeople = this.resultministrypeople.map((item, index) => {
        return { value: item.id, label: item.name }
      })
    })

    this.getAnswer();

    // this.getDetailCentralPolicy()
    this.getCentralPolicyProvinceUser();
    this.getDetailCentralPolicyProvince();
    this.getElectronicBookDetail();
    this.getElectOwnCreate();
    this.getCommentData();
    this.getProceedData();
    this.getElectronicBookOwnDetail();
    setTimeout(() => {
      this.spinner.hide();
    }, 300);

  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  openDeleteModal(template: TemplateRef<any>, id) {
    this.delid = id;
    this.modalRef = this.modalService.show(template);
  }

  openEbookDetailModal(template: TemplateRef<any>, subjectCenID) {
    this.modalRef = this.modalService.show(template);
    this.subjectCentralPolicyID = subjectCenID;
    // alert(subjectID);
  }

  openEditSuggestionModal(template: TemplateRef<any>, subjectID) {
    this.modalRef = this.modalService.show(template);
    // alert(subjectID);
    this.subjectCentralPolicyID = subjectID;
    this.electronicBookService.getSuggestionDetailById(subjectID, this.elecId).subscribe(result => {
      console.log("SUGGEST: ", result);
      this.editSuggestionForm.patchValue({
        checkDetail: result.detail,
        Problem: result.problem,
        Suggestion: result.suggestion,
      })
    })


  }

  deleteFile() {
    // alert(this.delid);
    this.electronicBookService.deleteFile(this.delid)
      .subscribe(response => {
        console.log("res: ", response);
        this.modalRef.hide();
        this.getElectronicBookDetail();
      })
  }

  editModal(template: TemplateRef<any>, id, name) {
    this.editid = id;
    this.subquestionclosename = name;

    this.modalRef = this.modalService.show(template);
    this.EditForm = this.fb.group({
      subquestionclose: new FormControl(),

    })
    this.EditForm.patchValue({
      subquestionclose: name,
    })
  }

  editModal2(template: TemplateRef<any>, id, name) {
    this.editid = id;
    this.subquestionclosechoicename = name;

    this.modalRef = this.modalService.show(template);
    this.EditForm2 = this.fb.group({
      subquestionclosechoice: new FormControl(),

    })
    this.EditForm2.patchValue({
      subquestionclosechoice: name,
    })
  }

  getDetailCentralPolicy() {
    this.centralpolicyservice.getdetailcentralpolicydata(this.id)
      .subscribe(result => {
        // this.resultdetailcentralpolicy = result
      })
  }

  getDetailCentralPolicyProvince() {
    this.centralpolicyservice.getdetailcentralpolicyprovincedata(this.id)
      .subscribe(result => {
        console.log("EiEi: ", result);
        // alert(JSON.stringify(result))
        this.resultdetailcentralpolicy = result.centralpolicydata
        console.log("res ja", this.resultdetailcentralpolicy);

        this.answerPeople = result.answerPeople;

        this.resultdetailcentralpolicyprovince = result.subjectcentralpolicyprovincedata
        this.resultuser = result.userdata

        this.electronicbookid = result.centralPolicyEventdata.electronicBookId
        this.resultdate = result.centralPolicyEventdata.inspectionPlanEvent
        console.log("RES DATE: ", this.resultdate);


        this.provincename = result.provincedata.name
        this.provinceid = result.provincedata.id

        this.policyDropdown = result.subjectcentralpolicyprovincedata.map((item, index) => {
          return { value: item.id, label: item.name }
        })

        // alert(this.resultdetailcentralpolicyprovince[0].centralPolicyProvince.questionPeople)
        this.Form.patchValue({
          questionPeople: this.resultdetailcentralpolicyprovince[0].centralPolicyProvince.questionPeople,
        })

        this.getCalendarFile();
        this.getElectronikbookFile();
        this.getSignatureProvince();
      })
  }

  getCentralPolicyProvinceUser() {
    this.centralpolicyservice.getcentralpolicyprovinceuserdata(this.id)
      .subscribe(result => {
        this.resultcentralpolicyuser = result
        console.log("resultCenUser", result);
      })

  }


  storeFiles(value) {

  }

  storePeople(value) {
    // alert(JSON.stringify(value))
    // this.centralpolicyservice.addCentralpolicyUser(value, this.id).subscribe(response => {
    //   console.log(value);
    //   this.Form.reset()
    //   this.modalRef.hide()
    //   this.getCentralPolicyProvinceUser();
    // })
  }

  storeMinistryPeople(value) {
    // alert(JSON.stringify(value))
  }

  editsubquestionclose(value, id) {
    this.subjectservice.editsubquestionprovince(value, id).subscribe(response => {
      console.log(value);
      this.EditForm.reset()
      this.modalRef.hide()
      this.getDetailCentralPolicyProvince();
    })
  }

  editsubquestionclosechoice(value, id) {
    this.subjectservice.editsubquestionchoiceprovince(value, id).subscribe(response => {
      console.log(value);
      this.EditForm2.reset()
      this.modalRef.hide()
      this.getDetailCentralPolicyProvince();
    })
  }

  getElectronicBookDetail() {
    this.electronicBookService.getElectronicBookDetail(this.elecId).subscribe(result => {
      console.log("EDIT ElectronicBookDetal: ", result);
      // alert("EDIT: " + result);
      // this.resultelectronicbookdetail = result.electData.detail;
      this.resultStatus = result.electData.status;
      this.resultElecFile2 = result.electData.electronicBookFiles;
      console.log("res file: ", this.resultElecFile2);


      // this.resultreport = result.centralPolicyUser
      this.resultreport = result.report

      this.resultreport.forEach(element => {
        // alert(JSON.stringify(element))
        if (element.report != null) {
          this.resultreportnum = 1;
        }
      });

      console.log("results report: ", this.resultreport);

      this.detailForm.patchValue({
        eBookDetail: this.resultelectronicbookdetail,
        Status: result.status
      })
    })
  }

  editDetail(value) {
    this.electronicBookService.editElectronicBookDetail(value, this.elecId, this.form.value.files).subscribe(result => {
      console.log("res: ", result);

      this.spinner.show();

      setTimeout(() => {
        this.getElectronicBookDetail();
        this.spinner.hide();
      }, 300);
    })
  }

  addSugestionDetail(value) {
    console.log("Detail Form: ", value);

    this.electronicBookService.addSuggestion(value, this.elecId, this.subjectCentralPolicyID).subscribe(result => {
      console.log("res: ", result);

      this.modalRef.hide();
      this.getDetailCentralPolicyProvince();

    })
  }

  addProceed(value) {
    this.electronicBookService.addProceed(value, this.elecId, this.userid).subscribe(result => {
      console.log("res: ", result);
      this.modalRef.hide();
      this.getProceedData();
    })
  }

  editSugestionDetail(value) {
    console.log("Detail Form: ", value);

    this.electronicBookService.editSuggestion(value, this.elecId, this.subjectCentralPolicyID).subscribe(result => {
      console.log("res Edit Suggestion: ", result);

      this.modalRef.hide();
      this.getDetailCentralPolicyProvince();

    })
  }

  uploadFile(event) {
    this.fileStatus = true;
    const file = (event.target as HTMLInputElement).files;

    this.form.patchValue({
      files: file
    });
    console.log("fff:", this.form.value.files)
    this.form.get('files').updateValueAndValidity()
  }

  back() {
    window.history.back();
  }

  getCalendarFile() {
    // alert(this.electronicbookid)
    this.electronicBookService.getCalendarFile(this.electronicbookid).subscribe(res => {
      this.carlendarFile = res.carlendarFile;
      // this.signatureFile2 = res.signatureFile;
      console.log("calendarFile: ", res);

    })
  }
  getElectronikbookFile() {
    this.electronicBookService.getElectronicbookFile(this.electronicbookid).subscribe(res => {
      this.resultElecFile = res.electronicFile;
      console.log("ElecFile: ", res);

    })
  }
  getElectOwnCreate() {
    this.electronicBookService.getElectOwnCreate(this.elecId).subscribe(res => {
      console.log("Own Elect: ", res);
      // alert(res);
      this.editSuggestionForm.patchValue({
        checkDetail: res.detail,
        Problem: res.problem,
        Suggestion: res.suggestion,
      })
    })
  }

  getSignatureProvince() {
    // alert(this.electronicbookid)
    this.electronicBookService.getSignatureFile(this.elecId).subscribe(res => {
      this.signatureFile = res;
      this.adminProvinceForm.patchValue({
        provinceSuggestion: this.signatureFile[0].description
      })
      console.log("signatureFile: ", this.signatureFile);

    })
  }

  addSignatureFile() {
    // alert(this.adminProvinceForm.value.provinceSuggestion)
    this.addReportTable();
    this.electronicBookService.addSignatureFile(this.elecId, this.form.value.files, this.adminProvinceForm.value).subscribe(res => {
      console.log("signatureFile: ", res);

      this.spinner.show();

      this.notificationService.addNotification(this.resultdetailcentralpolicy.id, this.provinceid, this.userid, 8, 1)
        .subscribe(response => {
          console.log(response);
        })


      setTimeout(() => {
        this.getSignatureProvince();
        this.spinner.hide();
      }, 300);
    })

  }

  addReportTable() {
    this.electronicBookService.addReportTable(this.resultdetailcentralpolicy, this.editSuggestionForm.value, this.resultdetailcentralpolicy.id).subscribe(res => {
      console.log("resReportTable: ", res);

    })
  }

  getCommentData() {
    this.centralpolicyservice.getComment(this.id).subscribe(res => {
      this.commentData = res;
      console.log("comment: ", this.commentData);

    })
  }

  getProceedData() {
    this.electronicBookService.getProceed(this.elecId).subscribe(res => {
      this.ProceedData = res;
      console.log("Proceed: ", this.ProceedData);
    })
  }

  getAnswer() {
    this.centralpolicyservice.getAnswer(this.id).subscribe(res => {
      this.answerData = res;
      console.log("answer: ", this.answerData);
    })
  }

  async openAnswerModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  // checkBtn(index) {
  //   let value = document.getElementById('collapseAsk'+index).getAttribute('aria-expanded')

  //   console.log(value);

  //   if(value == "false"){

  //     $('#collapseAsk'+index).addClass('btn-askhide');
  //     $('.far fa-fw fa-plus-square').addClass('none');
  //     $('.far fa-fw fa-minus-square').addClass('block');
  //   }else{
  //     $('#collapseAsk'+index).removeClass('btn-askhide');
  //     $('.far fa-fw fa-plus-square').addClass('block');
  //     $('.far fa-fw fa-minus-square').addClass('none');
  //   }
  // }

  getElectronicBookOwnDetail() {
    this.electronicBookService.getElectOwnDetail(this.id).subscribe(res => {
      this.electOwnData = res;
      console.log("own data:", this.electOwnData);
    })
  }

  openModal2(template: TemplateRef<any>, subjectid) {
    this.subjectid = subjectid
    this.departmentService.getalldepartdata().subscribe(res => {
      this.department = res.map((item, index) => {
        return {
          value: item.id,
          label: item.name
        }
      })
      this.modalRef = this.modalService.show(template);
    })
  }

  storeDepartment(value) {
    // alert(this.subjectid)
    this.centralpolicyservice.addDepartment(value, this.subjectid).subscribe(response => {
      console.log(value);
      this.Form2.reset()
      this.modalRef.hide()
      this.getDetailCentralPolicyProvince();
    })
  }

}
