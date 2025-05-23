import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ElectronicbookService } from 'src/app/services/electronicbook.service';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { InspectionplanService } from 'src/app/services/inspectionplan.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserService } from 'src/app/services/user.service';
import { ExportReportService } from 'src/app/services/export-report.service';
import { NotificationService } from 'src/app/services/notification.service';
import { FormBuilder, FormGroup, FormControl, Validators, FormArray } from '@angular/forms';
import { NotofyService } from 'src/app/services/notofy.service';
import { TypeexamibationplanService } from 'src/app/services/typeexamibationplan.service';

@Component({
  selector: 'app-report-import-deatail',
  templateUrl: './report-import-deatail.component.html',
  styleUrls: ['./report-import-deatail.component.css']
})
export class ReportImportDeatailComponent implements OnInit {
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
  resulttypeexamibationplan: any[];
  listfiles: any = [];
  form: FormGroup;
  delId: any;

  get f() { return this.reportForm.controls }
  get s() { return this.f.fileData as FormArray }

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
    private _NotofyService: NotofyService,
    private fb: FormBuilder,
    private typeexamibationplanservice: TypeexamibationplanService,
  ) {
    this.reportId = activatedRoute.snapshot.paramMap.get('id')
    this.url = baseUrl,
      this.downloadUrl = baseUrl + '/Uploads';
  }

  ngOnInit() {
    this.spinner.show();
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
    this.getTypeexamibationplan();
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

      fileData: new FormArray([]),
    })

    this.form = this.fb.group({
      files: [null]
    })
  }

  getReportImportById() {
    this.exportReportService.getImportedReportById2(this.reportId).subscribe(res => {
      console.log("detail: ", res);
      this.reportData = res;
      this.spinner.hide();
    })
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  openEditModal(template: TemplateRef<any>) {
    this.exportReportService.getImportedReportById2(this.reportId).subscribe(res => {
      console.log("getImportById: ", res);

      this.fiscalYearId = res.importData.fiscalYearId;
      this.getImportFiscalYearRelations();

      this.reportForm.patchValue({
        centralPolicyEvent: res.importData.importReportGroups.map((item, index) => {
          return item.centralPolicyId
        }),
        centralPolicyType: res.importData.centralPolicyTypeId,
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
      res.data.forEach(element => {
        this.notificationService.addNotification(this.reportData.importData.importReportGroups[0].centralPolicyEvent.centralPolicyId, 1, element.commanderId, 20, element.commanderReportId, null,this.userid)
          .subscribe(response => {
            console.log("Noti res: ", response);
          });
      });

      this.getReportImportById();
      this.modalRef.hide();
      this._NotofyService.onSuccess("ส่งรายงานผลการตรวจ",)
    })
  }

  editReport(value) {
    this.spinner.show();
    this.exportReportService.editImportReport2(value, this.reportId, this.listfiles).subscribe(res => {
      this.reportForm.reset();
      this.listfiles = [];
      this.getReportImportById();
      this.modalRef.hide();
      this._NotofyService.onSuccess("แก้ไขข้อมูล",)
    })
  }

  getCentralPolicyEvent() {
    this.electronicBookService.getCentralPolicyEbook(this.userid).subscribe(res => {
      console.log("cenData: ", res);
      this.centralPolicyEvent = res.map((item, index) => {
        return {
          value: item.id,
          label: item.centralPolicyTitle + "  -  " + "จังหวัด: " + item.inspectionPlanEventProvinceName
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

  getTypeexamibationplan() {
    this.typeexamibationplanservice.getdata().subscribe(result => {
      this.resulttypeexamibationplan = result
    })
  }

  uploadFile(event) {
    var file = (event.target as HTMLInputElement).files;
    for (let i = 0, numFiles = file.length; i < numFiles; i++) {
      this.listfiles.push(file[i])
      this.s.push(this.fb.group({
        reportFile: file[i],
        fileDescription: '',
        type: file[i].type,
      }))
    }
    console.log("listfiles: ", this.reportForm.value);
    console.log("eiei: ", this.s.controls);


    this.form.patchValue({
      files: this.listfiles
    });
  }

  openDeleteFileModal(template: TemplateRef<any>, id) {
    this.delId = id;
    this.modalRef = this.modalService.show(template);
  }

  deleteFile() {
    this.exportReportService.deleteFile(this.delId)
      .subscribe(response => {
        console.log("res: ", response);
        this.modalRef.hide();
        this.getReportImportById();
      })
  }

}
