import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ReportService } from 'src/app/services/report.service';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { SubjectService } from 'src/app/services/subject.service';

@Component({
  selector: 'app-report-comment',
  templateUrl: './report-comment.component.html',
  styleUrls: ['./report-comment.component.css']
})
export class ReportCommentComponent implements OnInit {

  url = ""
  downloadUrl: any;
  modalRef: BsModalRef;

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
  }
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }
  storeReport() {
    this.reportservice.createReport5().subscribe(res => {
      console.log("export: ", res);
      window.open(this.url + "Uploads/" + res.data);
      // this.modalRef.hide();
    })
  }
}