import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ElectronicbookService } from '../services/electronicbook.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { InspectionplanService } from '../services/inspectionplan.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserService } from '../services/user.service';
import { ExportReportService } from '../services/export-report.service';
import { NotificationService } from '../services/notification.service';

@Component({
  selector: 'app-commander-report',
  templateUrl: './commander-report.component.html',
  styleUrls: ['./commander-report.component.css']
})
export class CommanderReportComponent implements OnInit {
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
  commandForm: FormGroup;
  fileForm: FormGroup;
  importedReport: any = [];
  subjectData: any = [];
  fileFormExcel: FormGroup;
  downloadUrl: any;
  reportId: any;
  commandData: any;

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

    this.getCommanderReport();
    this.getSubjectData();

    this.commandForm = this.fb.group({
      command: new FormControl(null, [Validators.required])
    })
  }

  getCommanderReport() {
    this.exportReportService.getCommanderReport().subscribe(res => {
      console.log("commanderReport: ", res.data);
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

  storeCommand(value) {
    this.exportReportService.sendCommand(value, this.reportId, this.userid).subscribe(res => {
      this.commandForm.reset();
      this.getCommanderReport();
      this.modalRef.hide();

      console.log("Command RES: ", res);


      this.notificationService.addNotification(1, 1, res.createBy, 15, res.id)
      .subscribe(response => {
        console.log("Noti: ", response);
      });
    })
  }

  openCommandModal(template: TemplateRef<any>, id) {
    this.reportId = id;

    this.modalRef = this.modalService.show(template);
  }

  showCommandModal(template: TemplateRef<any>, id) {
    this.exportReportService.getCommanderReportById(id).subscribe(res => {
      this.commandData = res.data.command;
      console.log("Command: ", res.data);
      this.commandForm.patchValue({
        command: this.commandData
      })
      this.modalRef = this.modalService.show(template);
    })

  }

  closeModal() {
    this.commandForm.reset();
    this.modalRef.hide();
  }
}
