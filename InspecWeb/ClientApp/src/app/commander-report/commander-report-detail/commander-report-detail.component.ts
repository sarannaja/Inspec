import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ElectronicbookService } from 'src/app/services/electronicbook.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { InspectionplanService } from 'src/app/services/inspectionplan.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserService } from 'src/app/services/user.service';
import { ExportReportService } from 'src/app/services/export-report.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-commander-report-detail',
  templateUrl: './commander-report-detail.component.html',
  styleUrls: ['./commander-report-detail.component.css']
})
export class CommanderReportDetailComponent implements OnInit {
  url = "";
  downloadUrl: any;
  reportId: any;
  reportData: any = [];
  modalRef: BsModalRef;
  commandForm: FormGroup;
  userid: any;
  role_id: any;
  checkCommanded = false;

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
    private activatedRoute: ActivatedRoute,
    private userservice: UserService,
    @Inject('BASE_URL') baseUrl: string,
    private fb: FormBuilder,
  ) {
    this.reportId = activatedRoute.snapshot.paramMap.get('id')
    this.url = baseUrl,
      this.downloadUrl = baseUrl + '/Uploads';
  }

  ngOnInit() {
    this.commandForm = this.fb.group({
      command: new FormControl(null, [Validators.required]),
    })

    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        // alert(this.userid)
        this.userservice.getuserfirstdata(this.userid)
          .subscribe(result => {
            this.role_id = result[0].role_id
            // alert(this.role_id)
          })
      })
    this.getReportImportById();
  }

  getReportImportById() {
    this.exportReportService.getImportedReportById(this.reportId).subscribe(res => {
      console.log("detail: ", res);
      this.reportData = res;

      res.importData.reportCommanders.forEach(element => {
        if (element.userCommanderId == this.userid) {
          this.checkCommanded = true;
          console.log("check: ", this.checkCommanded);

        }
      });
    })
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  back() {
    window.history.back();
  }

  sendToCommander() {
    this.exportReportService.sendToCommander(this.reportId).subscribe(res => {
      console.log("sended: ", res);
      this.getReportImportById();
      this.modalRef.hide();
    })
  }

  closeModal() {
    this.commandForm.reset();
    this.modalRef.hide();
  }

  sendCommand(value) {
    this.exportReportService.sendCommand(this.reportId, value, this.userid).subscribe(res => {
      console.log("commanded: ", res);
      this.getReportImportById();
      this.modalRef.hide();
    })
  }
}