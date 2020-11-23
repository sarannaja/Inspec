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
  dtOptions: any = {};
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
  provinceId: any;

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

        var data: any = [];
        data = result;
        this.provinceId = data.user.provinceId;

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
      ],
      "language": {
        "lengthMenu": "แสดง  _MENU_  รายการ",
        "search": "ค้นหา:",
        // "info": "แสดง _PAGE_ ของ _PAGES_ รายการ",
        // "info": "แสดง _PAGE_ ของ _PAGES_ รายการ จาก _TOTAL_ แถว",
        "info": "แสดง _START_ ถึง _END_ จาก _TOTAL_ แถว",
        // "info": "แสดง _START_ ถึง _END_ จาก _TOTAL_ แถว",
        "infoEmpty": "แสดง 0 ของ 0 รายการ",
        "zeroRecords": "ไม่พบข้อมูล",
        "paginate": {
          "first": "หน้าแรก",
          "last": "หน้าสุดท้าย",
          "next": "ต่อไป",
          "previous": "ย้อนกลับ"
        },
      }
    };

    this.getCommanderReport();

    this.commandForm = this.fb.group({
      command: new FormControl(null, [Validators.required])
    })
  }

  getCommanderReport() {
    this.exportReportService.getCommanderReportById(this.provinceId, this.userid).subscribe(res => {
      console.log("commanderReport: ", res);
      this.importedReport = res;
      this.loading = true;
    })
  }

  gotoDetail(id) {
    this.router.navigate(['/commanderreport/detail/' + id])
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  // storeCommand(value) {
  //   this.exportReportService.sendCommand(value, this.reportId, this.userid).subscribe(res => {
  //     this.commandForm.reset();
  //     this.getCommanderReport();
  //     this.modalRef.hide();

  //     console.log("Command RES: ", res);


  //     this.notificationService.addNotification(1, 1, res.createBy, 15, res.id)
  //     .subscribe(response => {
  //       console.log("Noti: ", response);
  //     });
  //   })
  // }

  closeModal() {
    this.commandForm.reset();
    this.modalRef.hide();
  }
}
