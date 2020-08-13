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
  downloadUrl: any;
  centralPolicyEvent: any = [];
  fiscalYearId: any;
  fiscalYearData: any = [];
  regionData: any = [];
  provinceData: any = [];
  reportId: any;
  form: FormGroup;

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

    this.getImportedReport();
    this.getCentralPolicyEvent();
    this.getImportFiscalYears();

    this.reportForm = this.fb.group({
      centralPolicyEvent: new FormControl(null, [Validators.required]),
      centralPolicyType: new FormControl(null, [Validators.required]),
      reportType: new FormControl(null, [Validators.required]),
      inspectionRound: new FormControl(null, [Validators.required]),
      fiscalYear: new FormControl(null, [Validators.required]),
      region: new FormControl(null, [Validators.required]),
      province: new FormControl(null, [Validators.required]),
      monitoringTopics: new FormControl(null, [Validators.required]),
      detailReport: new FormControl(null, [Validators.required]),
      suggestion: new FormControl(null, [Validators.required]),
      command: new FormControl(null, [Validators.required]),
    })

    this.form = this.fb.group({
      files: [null]
    })
  }

  getImportedReport() {
    this.exportReportService.getImportedReport(this.userid).subscribe(res => {
      console.log("importReport: ", res);
      this.importedReport = res;
      this.loading = true;
    })
  }

  gotoDetail(id) {
    this.router.navigate(['/reportimport/detail/' + id])
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  openExportModal(template: TemplateRef<any>, reportId) {
    this.modalRef = this.modalService.show(template);
    this.reportId = reportId;
  }

  openEditModal(template: TemplateRef<any>, reportId) {
    this.exportReportService.getImportedReportById(reportId).subscribe(res => {
      console.log("getImportById: ", res);

      this.reportForm.patchValue({
        centralPolicyEvent: res.importData.importReportGroups.map((item, index) => {
          return (item.centralPolicyEvent.centralPolicyId.toString())
        }),
        centralPolicyType: res.importData.centralPolicyType,
        reportType: res.importData.reportType,
        inspectionRound: res.importData.inspectionRound,
        fiscalYear: res.importData.fiscalYearId.toString(),
        monitoringTopics: res.importData.monitoringTopics,
        detailReport: res.importData.detailReport,
        suggestion: res.importData.suggestion,
        command: res.importData.command,
      })
    })

    this.modalRef = this.modalService.show(template);
  }

  storeReport(value) {
    console.log("Report Value: ", value);
    this.exportReportService.postImportReport(value, this.userid, this.form.value.files).subscribe(res => {
      this.reportForm.reset();
      this.loading = false;
      this.getImportedReport();
      this.modalRef.hide();

      // this.notificationService.addNotification(1, 1, this.userid, 14, res.importId)
      //   .subscribe(response => {
      //     console.log("Noti: ", response);
      //   });
    })
  }

  editReport(value) {
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
        this.loading = false;
        this.getImportedReport();
        this.spinner.hide();
      }, 300);
    })
  }

  closeModal() {
    this.modalRef.hide();
  }

  getCentralPolicyEvent() {
    this.electronicBookService.getCentralPolicyEbook(this.userid).subscribe(res => {
      console.log("cenData: ", res);
      this.centralPolicyEvent = res.map((item, index) => {
        return {
          value: item.id,
          label: item.centralPolicy.title + "  -  " + "จังหวัด: " + item.inspectionPlanEvent.province.name
        }
      })
    })
  }

  getImportFiscalYears() {
    this.exportReportService.getImportReportFiscalYears().subscribe(res => {
      console.log("fiscalYear1: ", res);
      this.fiscalYearData = res.importFiscalYear.map((item, index) => {
        return {
          value: item.id,
          label: item.year
        }
      })
      console.log("fiscalYear: ", this.fiscalYearData);
    })
  }

  getImportFiscalYearRelations() {
    this.exportReportService.getImportReportFiscalYearRelations(this.fiscalYearId).subscribe(res => {
      console.log("fiscalYearRelations: ", res);

      var uniqueRegion: any = [];
      uniqueRegion = res.importFiscalYearRelations.filter(
        (thing, i, arr) => arr.findIndex(t => t.regionId === thing.regionId) === i
      );
      console.log("uniqueRegions: ", uniqueRegion);

      this.regionData = uniqueRegion.map((item, index) => {
        return {
          value: item.region.id,
          label: item.region.name
        }
      })

      this.provinceData = res.importFiscalYearRelations.map((item, index) => {
        return {
          value: item.province.id,
          label: item.province.name
        }
      })
    })
  }

  selectFiscalYear(value) {
    this.fiscalYearId = value.value;
    this.getImportFiscalYearRelations();
  }

  exportReport() {
    this.exportReportService.getImportedReportById(this.reportId).subscribe(res => {
      console.log("reportById: ", res);
      // var data: any = [];
      // this.exportData = res.importData.importReportGroups.map((item, index) => {
      //   return {
      //     title: item.centralPolicyEvent.centralPolicy.title,
      //     subject: item.centralPolicyEvent.centralPolicy.centralPolicyProvinces.map((item2, index2) => {
      //       return item2.subjectCentralPolicyProvinces
      //     }),
      //     centralPolicyType: res.importData.centralPolicyType,
      //     command: res.importData.command,
      //     detailReport: res.importData.detailReport,
      //     fiscalYear: res.importData.fiscalYear.year,
      //     inspectionRound: res.importData.inspectionRound,
      //     monitoringTopics: res.importData.monitoringTopics,
      //     province: res.importData.province.name,
      //     region: res.importData.region.name,
      //     reportType: res.importData.reportType,
      //     suggestion: res.importData.suggestion
      //   }
      // })
      // console.log("Data: ", this.exportData);

      this.exportReportService.createReport2(res, this.reportId).subscribe(res => {
        console.log("export: ", res);
        window.open(this.url + "Uploads/" + res.data);
        this.modalRef.hide();
      })
    })
  }

  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.form.patchValue({
      files: file
    });
    console.log("fff:", this.form.value.files)
    this.form.get('files').updateValueAndValidity()
  }

}
