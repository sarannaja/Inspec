import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ElectronicbookService } from 'src/app/services/electronicbook.service';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { InspectionplanService } from 'src/app/services/inspectionplan.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserService } from 'src/app/services/user.service';
import { ExportReportService } from 'src/app/services/export-report.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-all-report-detail',
  templateUrl: './all-report-detail.component.html',
  styleUrls: ['./all-report-detail.component.css']
})
export class AllReportDetailComponent implements OnInit {
  url = "";
  downloadUrl: any;
  reportId: any;
  reportData: any = [];
  modalRef: BsModalRef;
  reportForm: FormGroup;
  centralPolicyEvent: any = [];
  fiscalYearData: any = [];
  regionData: any = [];
  provinceData: any = [];
  fiscalYearId: any;
  userid: string;
  role_id;
  commanderData: any = [];

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
    @Inject('BASE_URL') baseUrl: string,
    private fb: FormBuilder,
  ) {
    this.reportId = activatedRoute.snapshot.paramMap.get('id')
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
    this.getReportImportById();
    this.getCentralPolicyEvent();
    this.getImportFiscalYears();
    this.getCommander();
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
      commander: new FormControl(null, [Validators.required]),
    })
  }

  getReportImportById() {
    this.exportReportService.getImportedReportById(this.reportId).subscribe(res => {
      console.log("detail: ", res);
      this.reportData = res;
    })
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  openEditModal(template: TemplateRef<any>) {
    this.exportReportService.getImportedReportById(this.reportId).subscribe(res => {
      console.log("getImportById: ", res);

      this.fiscalYearId = res.importData.fiscalYearId;
      this.getImportFiscalYearRelations();

      this.reportForm.patchValue({
        centralPolicyEvent: res.importData.importReportGroups.map((item, index) => {
          return item.centralPolicyEvent.centralPolicyId
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

      setTimeout(() => {
        this.reportForm.patchValue({
          region: res.importData.regionId.toString(),
          province: res.importData.provinceId.toString(),
        })
      }, 300);
    })



    this.modalRef = this.modalService.show(template);
  }

  back() {
    window.history.back();
  }

  sendToCommander(value) {
    this.exportReportService.sendToCommander(this.reportId, value).subscribe(res => {
      console.log("sended: ", res);
      this.getReportImportById();
      this.modalRef.hide();
    })
  }

  editReport(value) {
    this.exportReportService.editImportReport(value, this.reportId).subscribe(res => {
      this.reportForm.reset();
      this.getReportImportById();
      this.modalRef.hide();
    })
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
          value: item.id.toString(),
          label: item.year
        }
      })
      console.log("fiscalYear: ", this.fiscalYearData);
    })
  }

  selectFiscalYear(value) {
    this.fiscalYearId = value.value;
    this.getImportFiscalYearRelations();
  }

  getImportFiscalYearRelations() {
    console.log("fiscalYEAR: ", this.fiscalYearId);

    this.exportReportService.getImportReportFiscalYearRelations().subscribe(res => {
      console.log("fiscalYearRelations: ", res);

      var uniqueRegion: any = [];
      uniqueRegion = res.importFiscalYearRelations.filter(
        (thing, i, arr) => arr.findIndex(t => t.regionId === thing.regionId) === i
      );
      console.log("uniqueRegions: ", uniqueRegion);

      this.regionData = uniqueRegion.map((item, index) => {
        return {
          value: item.region.id.toString(),
          label: item.region.name
        }
      })

      this.provinceData = res.importFiscalYearRelations.map((item, index) => {
        return {
          value: item.province.id.toString(),
          label: item.province.name
        }
      })
    })
  }

  getCommander() {
    this.exportReportService.getCommander().subscribe(res => {
      console.log("commander: ", res);
      this.commanderData = res.data.map((item, index) => {
        return {
          value: item.id.toString(),
          label: item.prefix + item.name + " ตำแหน่ง: " + item.position
        }
      })
    })
  }
}
