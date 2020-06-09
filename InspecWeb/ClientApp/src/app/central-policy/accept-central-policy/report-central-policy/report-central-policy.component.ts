import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { ActivatedRoute } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ElectronicbookService } from 'src/app/services/electronicbook.service';

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
  userFiles: any[];
  report: any;
  delid: any;
  modalRef: BsModalRef;
  resultdetailcentralpolicyprovince: any = [];
  resultelectronicbookdetail: any = [];
  draftStatus: any;
  resultuser: any = []
  centralpolicyproviceid
  electronicbookid
  provincename
  provinceid
  resultdate: any = []
  carlendarFile: any = [];
  resultElecFile: any= [];
  fileType2: any;

  constructor(
    private fb: FormBuilder,
    private authorize: AuthorizeService,
    private centralpolicyservice: CentralpolicyService,
    private activatedRoute: ActivatedRoute,
    private modalService: BsModalService,
    private electronicBookService: ElectronicbookService,
  ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.centralpolicyproviceid = activatedRoute.snapshot.paramMap.get('centralpolicyproviceid')
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
      files: [null],
      Status: new FormControl("ร่างกำหนดการ", [Validators.required]),
      fileType: new FormControl("เลือกประเภทเอกสารแนบ", [Validators.required]),
      signatureFiles: [null],
      description: new FormControl(null, [Validators.required]),
    })

    this.getDetailCentralPolicy();
    this.getCentralPolicyUser();
    this.getUserFiles();
    this.getDetailCentralPolicyProvince();

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
        console.log("resultJA",  result);
      })
  }

  getUserFiles() {
    this.centralpolicyservice.getUserFiles(this.id).subscribe(results => {
      this.userFiles = results;
      console.log("UserFiles: ", this.userFiles);
      this.report = results.report
      this.draftStatus = results.status
      console.log("report:", this.report);
      this.form.patchValue({
        report: this.report,
        Status: this.draftStatus
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

  // getSubjectCentralPolicyProvince() {
  //   this.centralpolicyservice.getSubjectCentralPolicyProvince(this.id)
  //     .subscribe(result => {
  //       this.resultdetailcentralpolicyprovince = result
  //       console.log("resultdetailcentralpolicyprovince : ", result);
  //     })
  // }
  getDetailCentralPolicyProvince() {
    this.centralpolicyservice.getdetailcentralpolicyprovincedata(this.centralpolicyproviceid)
      .subscribe(result => {
        console.log("123", result);
        // alert(JSON.stringify(result))
        this.resultdetailcentralpolicy = result.centralpolicydata
        this.resultdetailcentralpolicyprovince = result.subjectcentralpolicyprovincedata
        this.resultuser = result.userdata
        this.electronicbookid = result.centralPolicyEventdata.electronicBookId

        this.resultdate = result.centralPolicyEventdata.inspectionPlanEvent
        this.provincename = result.provincedata.name
        this.provinceid = result.provincedata.id

        this.getCalendarFile();
        this.getElectronikbookFile();
      })
  }

  getCalendarFile() {
    this.electronicBookService.getCalendarFile(this.electronicbookid).subscribe(res => {
      this.carlendarFile = res.carlendarFile;
      console.log("calendarFile: ", res);

    })
  }

  getElectronikbookFile() {
    this.electronicBookService.getElectronicbookFile(this.electronicbookid).subscribe(res => {
      this.resultElecFile = res.electronicFile;
      console.log("resultElecFile: ", res);

    })
  }

  checkType2(type) {
    // alert(type)
    this.fileType2 = type;
  }

}
