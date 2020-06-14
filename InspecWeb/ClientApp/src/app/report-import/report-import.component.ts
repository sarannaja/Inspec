import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
import { ElectronicbookService } from '../services/electronicbook.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { InspectionplanService } from '../services/inspectionplan.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserService } from '../services/user.service';
import { ExportReportService } from '../services/export-report.service';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-report-import',
  templateUrl: './report-import.component.html',
  styleUrls: ['./report-import.component.css']
})
export class ReportImportComponent implements OnInit {
  electronicBookData: any = [];
  loading = false;
  dtOptions: DataTables.Settings = {};
  userid: string;
  delid: any;
  modalRef: BsModalRef;
  centralpolicyprovinceid: any;
  reportData: any = [];
  role_id;
  exportData: any = [];
  url = ""
  reportForm: FormGroup;
  fileForm: FormGroup;

  constructor(
    private router: Router,
    private electronicBookService: ElectronicbookService,
    private authorize: AuthorizeService,
    private modalService: BsModalService,
    private inspectionplanservice: InspectionplanService,
    private spinner: NgxSpinnerService,
    private userService: UserService,
    private exportReportService: ExportReportService,
    @Inject('BASE_URL') baseUrl: string,
    private fb: FormBuilder,
  ) {
    this.url = baseUrl,
    this.fileForm = this.fb.group({
      files: [null]
    })
  }

  ngOnInit() {
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
        this.userService.getuserfirstdata(this.userid)
          .subscribe(result => {
            this.role_id = result[0].role_id
          })
      })
    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [2],
          orderable: false
        }
      ]
    };
    this.getexportport();

    this.reportForm = this.fb.group({
      Subject: new FormControl(null, [Validators.required]),
      TypeExport:  new FormControl(null, [Validators.required]),
      TypeReport:  new FormControl(null, [Validators.required]),
    })
  }

  getexportport() {
    this.electronicBookService.getSubjectReport().subscribe(results => {
      console.log("res: ", results);

      this.loading = true;
    })
  }

  gotoDetail(id, elecId) {
    this.router.navigate(['/electronicbook/detail/' + id, { electronicBookId: elecId }])
  }

  exportReport(elecId, cenProId) {
    // alert("eiei");
    this.exportReportService.exportReport(this.userid, elecId, cenProId).subscribe(res => {
      console.log("export: ", res);
      this.createReport(res, cenProId);
    })
  }

  createReport(res, cenProId) {
    this.exportReportService.createReport(res, cenProId).subscribe(results => {
      console.log("aaa: ", res);
      // alert("Success");
      window.open(this.url + "Uploads/" + results.data);
    })
  }

  openModal(template: TemplateRef<any>) {
    console.log("DELID: ", this.delid);

    this.modalRef = this.modalService.show(template);
  }

  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.fileForm.patchValue({
      files: file
    });
    this.fileForm.get('files').updateValueAndValidity()
  }

  storeReport(value) {
    console.log("Report Value: ", value);

  }
}
