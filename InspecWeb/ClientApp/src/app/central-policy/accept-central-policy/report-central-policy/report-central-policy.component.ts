import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { ActivatedRoute } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-report-central-policy',
  templateUrl: './report-central-policy.component.html',
  styleUrls: ['./report-central-policy.component.css']
})
export class ReportCentralPolicyComponent implements OnInit {

  id: any;
  fileStatus = false;
  form: FormGroup;
  userid: string
  resultdetailcentralpolicy: any = [];
  resultcentralpolicyuser: any = [];
  userFiles: any [];
  report: any;
  delid: any;
  modalRef: BsModalRef;
  resultdetailcentralpolicyprovince: any = [];
  resultelectronicbookdetail: any = [];
  resultuser: any = []
  constructor(
    private fb: FormBuilder,
    private authorize: AuthorizeService,
    private centralpolicyservice: CentralpolicyService,
    private activatedRoute: ActivatedRoute,
    private modalService: BsModalService,
  ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
  }

  ngOnInit() {
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
        // alert(this.userid)
      })

    this.form = this.fb.group({
      report: new FormControl(null, [Validators.required]),
      files: [null]
    })

    this.getDetailCentralPolicy();
    this.getCentralPolicyUser();
    this.getUserFiles();
    this.getSubjectCentralPolicyProvince();
  }

  getDetailCentralPolicy() {
    this.centralpolicyservice.getdetailacceptcentralpolicydata(this.id)
      .subscribe(result => {
        this.resultdetailcentralpolicy = result.centralpolicydata
        this.resultelectronicbookdetail = result.centralpolicydata.centralPolicyUser[0].electronicBook.detail
        this.resultuser = result.userdata
      })
  }

  getCentralPolicyUser() {
    this.centralpolicyservice.getdetailuseracceptcentralpolicydata(this.id)
      .subscribe(result => {
        this.resultcentralpolicyuser = result
        console.log("result" + result);
      })
  }

  getUserFiles() {
    this.centralpolicyservice.getUserFiles(this.id).subscribe(results => {
      this.userFiles = results;
      console.log("UserFiles: ", this.userFiles);
      this.report = results.report
      console.log("report:", this.report);
      this.form.patchValue({
        report: this.report
      });
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

  sendReport(value) {
    console.log("SUBMIT: ", value);
    console.log("files: ", this.form.value.files);

    this.centralpolicyservice.sendReport(value, this.form.value.files, this.id)
    .subscribe(response => {
      console.log("res: ", response);
      this.form.reset()
      window.history.back();
    })
  }

  deleteUserFile() {
    // alert(this.delid);
    this.centralpolicyservice.deleteUserFile(this.delid)
    .subscribe(response => {
      console.log("res: ", response);
      this.modalRef.hide();
      this.getDetailCentralPolicy();
    this.getCentralPolicyUser();
    this.getUserFiles();
    })
  }

  openModal(template: TemplateRef<any>, id) {
    this.delid = id;
    console.log("DELID: ", this.delid);

    this.modalRef = this.modalService.show(template);
  }

  getSubjectCentralPolicyProvince() {
    this.centralpolicyservice.getSubjectCentralPolicyProvince(this.id)
      .subscribe(result => {
        this.resultdetailcentralpolicyprovince = result
        console.log("resultdetailcentralpolicyprovince : ", result);
      })
  }

}
