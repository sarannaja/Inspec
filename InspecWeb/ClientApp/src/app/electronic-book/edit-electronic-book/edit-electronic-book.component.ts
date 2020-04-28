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

@Component({
  selector: 'app-edit-electronic-book',
  templateUrl: './edit-electronic-book.component.html',
  styleUrls: ['./edit-electronic-book.component.css']
})
export class EditElectronicBookComponent implements OnInit {

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
  delid: any;

  constructor(private fb: FormBuilder,
    private modalService: BsModalService,
    private centralpolicyservice: CentralpolicyService,
    private userservice: UserService,
    private subjectservice: SubjectService,
    private activatedRoute: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private electronicBookService: ElectronicbookService,
    @Inject('BASE_URL') baseUrl: string ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.elecId = activatedRoute.snapshot.paramMap.get('electronicBookId')
    this.centralPolicyUserId = activatedRoute.snapshot.paramMap.get('centralPolicyUserId')
    this.downloadUrl = baseUrl + '/Uploads';

    this.form = this.fb.group({
      files: [null]
    })
  }

  ngOnInit() {
    console.log("ELECTID: ", this.id);

    this.spinner.show();
    this.Form = this.fb.group({
      UserPeopleId: new FormControl(null, [Validators.required]),
      // UserMinistryId: new FormControl(null, [Validators.required]),
    })

    this.detailForm = this.fb.group({
      eBookDetail: new FormControl(null, [Validators.required]),
      Status: new FormControl(null, [Validators.required]),
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

    // this.getDetailCentralPolicy()
    this.getCentralPolicyProvinceUser();
    this.getDetailCentralPolicyProvince();
    this.getElectronicBookDetail();
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
        console.log(result);
        // alert(JSON.stringify(result))
        this.resultdetailcentralpolicy = result.centralpolicydata
        this.resultdetailcentralpolicyprovince = result.subjectcentralpolicyprovincedata
        this.resultuser = result.userdata
      })
  }

  getCentralPolicyProvinceUser() {
    this.centralpolicyservice.getcentralpolicyprovinceuserdata(this.id)
      .subscribe(result => {
        this.resultcentralpolicyuser = result
        console.log("result" + result);
      })

  }


  storeFiles(value) {

  }

  storePeople(value) {
    // alert(JSON.stringify(value))
    this.centralpolicyservice.addCentralpolicyUser(value, this.id).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.getCentralPolicyProvinceUser();
    })
  }

  storeMinistryPeople(value) {
    alert(JSON.stringify(value))
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
    this.electronicBookService.getElectronicBookDetail(this.centralPolicyUserId).subscribe(result => {
      console.log("EDIT ElectronicBookDetal: ", result);
      // alert("EDIT: " + result);
      this.resultelectronicbookdetail = result.centralPolicyUser[0].electronicBook.detail
      this.resultStatus = result.centralPolicyUser[0].electronicBook.status;
      this.resultElecFile = result.centralPolicyUser[0].electronicBook.electronicBookFiles
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
}
