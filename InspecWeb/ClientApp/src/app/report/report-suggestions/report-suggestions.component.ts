import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ReportService } from 'src/app/services/report.service';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { SubjectService } from 'src/app/services/subject.service';

@Component({
  selector: 'app-report-suggestions',
  templateUrl: './report-suggestions.component.html',
  styleUrls: ['./report-suggestions.component.css']
})
export class ReportSuggestionsComponent implements OnInit {

  url = ""
  downloadUrl: any;
  modalRef: BsModalRef;
  // provinceid: any
  // centralpolicyid: any
  // subjectid: any
  // resultcentprovince: any = []
  // selectprovince: any = []
  // resultcentralpolicy: any = []
  // selectcentralpolicy: any = []
  // resultsubject: any = []
  // selectsubject: any = []
  // loading = false;
  // Form: FormGroup;

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
    // this.storeReport();
  }
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }
  storeReport() {
    this.reportservice.createReport2().subscribe(res => {
      console.log("export: ", res);
      window.open(this.url + "Uploads/" + res.data);
      // this.modalRef.hide();
    })
  }
}
