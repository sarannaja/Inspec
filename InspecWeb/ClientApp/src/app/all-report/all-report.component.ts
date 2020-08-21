import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray } from '@angular/forms';
import { Router } from '@angular/router';
import { ElectronicbookService } from '../services/electronicbook.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { InspectionplanService } from '../services/inspectionplan.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserService } from '../services/user.service';
import { ExportReportService } from '../services/export-report.service';
import { NotificationService } from '../services/notification.service';

@Component({
  selector: 'app-all-report',
  templateUrl: './all-report.component.html',
  styleUrls: ['./all-report.component.css']
})
export class AllReportComponent implements OnInit {
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
  checkType: any;
  departmentData: any = [];
  inputdate: any = [{ start_date: '', end_date: '' }];
  startDate: any;
  zoneData: any = [];

  get f() { return this.reportForm.controls }
  get d() { return this.f.inputdate as FormArray }

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
    this.getDepartment();
    this.getRegion();
    this.getZone();
    this.getProvince();

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
      department: new FormControl(null, [Validators.required]),
      inputdate: new FormArray([]),
      zone: new FormControl(null, [Validators.required]),
    })

    this.form = this.fb.group({
      files: [null]
    })

    this.d.push(this.fb.group({
      start_date: '',
      end_date: '',
    }))
  }

  getImportedReport() {
    this.exportReportService.getAllImportedReport().subscribe(res => {
      console.log("AllimportReport: ", res);
      this.importedReport = res;
      this.loading = true;
    })
  }

  gotoDetail(id) {
    this.router.navigate(['/allreport/detail/' + id])
  }

  openModal(template: TemplateRef<any>) {
    this.checkType = 0;
    this.modalRef = this.modalService.show(template);
  }

  openExportModal(template: TemplateRef<any>, reportId) {
    this.modalRef = this.modalService.show(template);
    this.reportId = reportId;
  }

  // storeReport(value) {
  //   console.log("Report Value: ", value);
  //   this.exportReportService.postImportReport(value, this.userid, this.form.value.files).subscribe(res => {
  //     this.reportForm.reset();
  //     this.loading = false;
  //     this.getImportedReport();
  //     this.modalRef.hide();

  //     // this.notificationService.addNotification(1, 1, this.userid, 14, res.importId)
  //     //   .subscribe(response => {
  //     //     console.log("Noti: ", response);
  //     //   });
  //   })
  // }

  closeModal() {
    this.modalRef.hide();
  }

  exportReport() {
    this.exportReportService.getImportedReportById(this.reportId).subscribe(res => {
      console.log("reportById: ", res);

      this.exportReportService.createReport2(res, this.reportId).subscribe(res => {
        console.log("export: ", res);
        window.open(this.url + "Uploads/" + res.data);
        this.modalRef.hide();
      })
    })
  }

  getDepartment() {
    this.exportReportService.getDepartments().subscribe(res => {
      console.log("getDepartments: ", res);
      this.departmentData = res.departments.map((item, index) => {
        return {
          value: item.id,
          label: item.name
        }
      })
      console.log("Departments: ", this.departmentData);
    })
  }

  getRegion() {
    this.exportReportService.getRegions().subscribe(res => {
      console.log("getRegions: ", res);
      this.regionData = res.regions.map((item, index) => {
        return {
          value: item.id,
          label: item.name
        }
      })
      console.log("Regions: ", this.regionData);
    })
  }

  typeReport(value) {
    console.log("value: ", value);
    this.checkType = value;
  }

  exportDepartment(value) {
    console.log("Department Value: ", value);
    this.exportReportService.getAllReportByDepartment(value).subscribe(res => {
      // console.log("RESSS: ", res);

      this.exportReportService.reportDepartment(res, this.checkType).subscribe(res => {
        console.log("export: ", res);
        window.open(this.url + "Uploads/" + res.data);
        this.modalRef.hide();
      })
    })
  }

  exportRegion(value) {
    console.log("Department Value: ", value);
    this.exportReportService.getAllReportByRegionId(value).subscribe(res => {
      // console.log("RESSS: ", res);

      this.exportReportService.reportRegion(res, this.checkType).subscribe(res => {
        console.log("export: ", res);
        window.open(this.url + "Uploads/" + res.data);
        this.modalRef.hide();
      })
    })
  }

  exportZone(value) {
    console.log("Department Value: ", value);
    this.exportReportService.getAllReportByZoneId(value).subscribe(res => {
      // console.log("RESSS: ", res);

      this.exportReportService.reportZone(res, this.checkType).subscribe(res => {
        console.log("export: ", res);
        window.open(this.url + "Uploads/" + res.data);
        this.modalRef.hide();
      })

      // this.reportForm.reset();
      // this.loading = false;
      // this.getImportedReport();
      // this.modalRef.hide();
    })
  }

  getProvince() {
    this.exportReportService.getProvinces().subscribe(res => {
      console.log("getProvinces: ", res);
      this.provinceData = res.provinces.map((item, index) => {
        return {
          value: item.id,
          label: item.name
        }
      })
      console.log("Provinces: ", this.provinceData);
    })
  }

  exportProvince(value) {
    console.log("Province Value: ", value);
    this.exportReportService.getAllReportProvinceId(value).subscribe(res => {
      // console.log("RESSS: ", res);

      this.exportReportService.reportProvince(res, this.checkType).subscribe(res => {
        console.log("export: ", res);
        window.open(this.url + "Uploads/" + res.data);
        this.modalRef.hide();
      })

      // this.reportForm.reset();
      // this.loading = false;
      // this.getImportedReport();
      // this.modalRef.hide();
    })
  }

  exportDay(value) {
    // console.log("Day Value: ", value);
    this.exportReportService.getAllReportDay(value).subscribe(res => {
      console.log("RESSS: ", res);

      this.exportReportService.reportDay(res, this.checkType).subscribe(res => {
        console.log("export: ", res);
        window.open(this.url + "Uploads/" + res.data);
        this.modalRef.hide();
      })

      // this.reportForm.reset();
      // this.loading = false;
      // this.getImportedReport();
      // this.modalRef.hide();
    })
  }

  onStartDateChanged(value) {
    console.log("startDateChange: ", value);
    this.startDate = value.date.year + '-' + value.date.month + '-' + value.date.day;
    console.log("Date ja: ", this.startDate);
  }

  getZone() {
    this.exportReportService.getZones().subscribe(res => {
      console.log("getZones: ", res);
      this.zoneData = res.sectors.map((item, index) => {
        return {
          value: item.id,
          label: item.name
        }
      })
      console.log("Zones: ", this.regionData);
    })
  }
}
