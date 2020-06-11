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
import { NotificationService } from '../services/notification.service';

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
  importedReport: any = [];
  subjectData: any = [];
  fileFormExcel: FormGroup;
  downloadUrl: any
  commandData: any;
  commandForm: FormGroup;

  constructor(
    private router: Router,
    private electronicBookService: ElectronicbookService,
    private authorize: AuthorizeService,
    private modalService: BsModalService,
    private inspectionplanservice: InspectionplanService,
    private spinner: NgxSpinnerService,
    private userService: UserService,
    private exportReportService: ExportReportService,
    private notificationService: NotificationService,
    @Inject('BASE_URL') baseUrl: string,
    private fb: FormBuilder,
  ) {
    this.url = baseUrl,
      this.fileForm = this.fb.group({
        files: [null]
      })
    this.fileFormExcel = this.fb.group({
      fileExcel: [null]
    })
    this.downloadUrl = baseUrl + '/Uploads';
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
          targets: [4],
          orderable: false
        }
      ]
    };

    this.getImportedReport();
    this.getSubjectData();

    this.reportForm = this.fb.group({
      Subject: new FormControl(null, [Validators.required]),
      TypeExport: new FormControl(null, [Validators.required]),
      TypeReport: new FormControl(null, [Validators.required]),
    })

    this.commandForm = this.fb.group({
      command: new FormControl(null, [Validators.required]),
      commander: new FormControl(null, [Validators.required])
    })
  }

  getImportedReport() {
    this.exportReportService.getImportedReport(this.userid).subscribe(res => {
      console.log("importReport: ", res.data);
      this.importedReport = res.data;
      this.loading = true;
    })
  }

  getSubjectData() {
    this.exportReportService.getSubjectReport().subscribe(results => {
      console.log("resSubject: ", results);
      this.subjectData = results.data.map((item, index) => {
        return { value: item.id, label: item.name }
      })
    })
  }

  gotoDetail(id, elecId) {
    this.router.navigate(['/electronicbook/detail/' + id, { electronicBookId: elecId }])
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  uploadFileWord(event) {
    const file = (event.target as HTMLInputElement).files;
    this.fileForm.patchValue({
      files: file
    });
    this.fileForm.get('files').updateValueAndValidity()
  }

  uploadFileExcel(event) {
    const file = (event.target as HTMLInputElement).files;
    this.fileFormExcel.patchValue({
      fileExcel: file
    });
    this.fileForm.get('files').updateValueAndValidity()
  }

  storeReport(value) {
    console.log("Report Value: ", value);
    this.exportReportService.postImportedReport(value, this.fileForm.value.files, this.fileFormExcel.value.fileExcel, this.userid).subscribe(res => {
      this.modalRef.hide();
      this.fileForm.reset();
      this.reportForm.reset();
      this.fileFormExcel.reset();
      console.log("res ID: ", res);

      this.spinner.show();
      setTimeout(() => {
        this.loading = false;
        this.getImportedReport();
        this.loading = true;
        this.spinner.hide();
      }, 300);

      this.notificationService.addNotification(1, 1, this.userid, 14, res.importId)
      .subscribe(response => {
        console.log("Noti: ", response);
      });
    })
  }

  openDeleteModal(template: TemplateRef<any>, id) {
    this.delid = id;
    console.log("DELID: ", this.delid);

    this.modalRef = this.modalService.show(template);
  }

  deleteReport() {
    // alert(this.delid);
    this.exportReportService.deleteReport(this.delid).subscribe(res => {
      console.log("Delete Data: ", res);
      this.modalRef.hide();
      this.spinner.show();

      setTimeout(() => {
        this.getImportedReport();
        this.spinner.hide();
      }, 300);
    })
  }

  showCommandModal(template: TemplateRef<any>, id) {
    this.exportReportService.getCommanderReportById(id).subscribe(res => {
      // this.commandData = res.data.command;
      console.log("Commander: ", res.commanderName);
      this.commandForm.patchValue({
        command: res.data.command,
        commander: res.commanderName.name
      })
      this.modalRef = this.modalService.show(template);
    })
  }

  closeModal() {
    this.commandForm.reset();
    this.modalRef.hide();
  }
}
