import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { ReportService } from 'src/app/services/report.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { SubjectService } from 'src/app/services/subject.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-report-performance',
  templateUrl: './report-performance.component.html',
  styleUrls: ['./report-performance.component.css']
})
export class ReportPerformanceComponent implements OnInit {

  url = ""
  downloadUrl: any;
  modalRef: BsModalRef;
  provinceid: any
  centralpolicyid: any
  subjectid: any
  resultcentprovince: any = []
  selectprovince: any = []
  resultcentralpolicy: any = []
  selectcentralpolicy: any = []
  resultsubject: any = []
  selectsubject: any = []
  loading = false;
  Form: FormGroup;
  constructor(
    private spinner: NgxSpinnerService,
    private modalService: BsModalService,
    private reportservice: ReportService,
    private centralpolicyservice: CentralpolicyService,
    private subjectservice: SubjectService,
    private fb: FormBuilder,
    @Inject('BASE_URL') baseUrl: string,
  ) {
    this.url = baseUrl,
    this.downloadUrl = baseUrl + '/Uploads';
  }

  ngOnInit() {
    this.spinner.show();
    this.Form = this.fb.group({
      provinceid: new FormControl(null, [Validators.required]),
      centralpolicyid: new FormControl(null, [Validators.required]),
      subjectid: new FormControl(null, [Validators.required]),
      reporttype: new FormControl(null, [Validators.required]),
    })
    this.getprovince();
  }
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }
  getprovince() {
    this.reportservice.getprovince().subscribe(result => {
      this.resultcentprovince = result
      console.log(this.resultcentprovince);

      this.selectprovince = this.resultcentprovince.map((item, index) => {
        return { value: item.id, label: item.name }
      })
      this.spinner.hide();
    })
  }
  selectedprovince(value) {
    this.provinceid = value.value
    this.getcentralpolicy();
  }
  getcentralpolicy() {
    this.reportservice.getcentralpolicy(this.provinceid).subscribe(result => {
      this.resultcentralpolicy = result
      this.selectcentralpolicy = this.resultcentralpolicy.map((item, index) => {
        return { value: item.centralPolicy.id, label: item.centralPolicy.title }
      })
    })
  }
  selectedcentralpolicy(value) {
    this.centralpolicyid = value.value
    this.getsubject();
  }
  getsubject() {
    this.subjectservice.getsubjectdata(this.centralpolicyid).subscribe(result => {
      this.resultsubject = result
      console.log(this.resultsubject);
      this.selectsubject = this.resultsubject.map((item, index) => {
        return { value: item.id, label: item.name }
      })
    })
  }
  selectedsubject(value) {
    this.subjectid = value.value
    this.getsubjectdetail();
  }
  getsubjectdetail() {
    this.reportservice.getsubjectdetaildata(this.subjectid)
      .subscribe(result => {
        console.log(result);
      })
  }
  // storeReport(value) {
  //   console.log(value);
  //   this.reportservice.createReport(value).subscribe(res => {
  //     console.log("export: ", res);
  //     window.open(this.url + "Uploads/" + res.data);
  //     // this.modalRef.hide();
  //   })
  // }
}
