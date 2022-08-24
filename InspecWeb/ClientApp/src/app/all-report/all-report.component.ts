import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray } from '@angular/forms';
import { Router } from '@angular/router';
import { ElectronicbookService } from '../services/electronicbook.service';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { InspectionplanService } from '../services/inspectionplan.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserService } from '../services/user.service';
import { ExportReportService } from '../services/export-report.service';
import { NotificationService } from '../services/notification.service';
import { NotofyService } from '../services/notofy.service';
import { IMyOptions } from 'mydatepicker-th';
declare var jQuery: any;

@Component({
  selector: 'app-all-report',
  templateUrl: './all-report.component.html',
  styleUrls: ['./all-report.component.css']
})
export class AllReportComponent implements OnInit {
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
  presidentData: any = [];
  provinceData: any = [];
  reportId: any;
  form: FormGroup;
  checkType: any;
  departmentData: any = [];
  inputdate: any = [{ start_date: '', end_date: '' }];
  startDate: any;
  zoneData: any = [];
  provinceId
  regionId
  filter: any = new Object;
  checkSort = 0;
  startdateforexport
  myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd/mm/yyyy',
    showClearDateBtn: false,
    editableDateField: false
  };

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
    private _NotofyService: NotofyService,
    @Inject('BASE_URL') baseUrl: string,
    private fb: FormBuilder,
  ) {
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
    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [5],
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

    this.getImportFiscalYears();

    this.getImportedReport();
    this.getRegion();
    this.getZone();
    this.getPresident();

    this.getProvince();
    this.getDepartment();

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

      president: new FormControl(null, [Validators.required]),
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
      this.spinner.hide();
    })
  }

  gotoDetail(id) {
    this.router.navigate(['/allreport/detail/' + id])
  }

  openModal(template: TemplateRef<any>) {
    this.reportForm.reset();
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
    this.spinner.show();
    this.exportReportService.getImportedReportById(this.reportId).subscribe(res => {
      console.log("reportById: ", res);

      this.exportReportService.createReport2(res, this.reportId).subscribe(res => {
        console.log("export: ", res);
        window.open(this.url + "Uploads/" + res.data);
        this.spinner.hide();
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
      console.log("RESSS55555kkk: ", res.reports);
      if (res.reports.length == 0) {
        console.log("IN NO DATA");

        this._NotofyService.onFalse("ขออภัย", "ไม่มีข้อมูล")
      } else {
        this.filter = res;
        if (this.startDate != undefined) {
          this.filter.reports = res.reports.filter(result => {
            let createAt = new Date(result.createAt)
            console.log("result.createAt123", createAt.toLocaleDateString());
            return (createAt.toLocaleDateString() == this.startdateforexport)
          })
        }
        console.log("filterssss ", this.filter);

        if (this.filter.reports.length == 0) {
          console.log("IN NO DATA");

          this._NotofyService.onFalse("ขออภัย", "ไม่มีข้อมูล")
        } else {
          this.exportReportService.reportDepartment(this.filter, this.checkType).subscribe(res => {
            console.log("export: ", res);
            window.open(this.url + "Uploads/" + res.data);
            this.modalRef.hide();
          })
        }
      }
    })
  }


  getPresident() {
    this.exportReportService.getPresident().subscribe(res => {
      console.log("getZones: ", res);
      this.presidentData = res.presidents.map((item, index) => {
        return {
          value: item.userRegion,
          label: item.prefix + " " + item.name,
          // region: item.userRegion.id
        }
      })
      console.log("Zones: ", this.regionData);
    })
  }

  exportPresident(value) {
    console.log("President Value: ", value);
    for (let i = 0; i < value.president.length; i++) {
      // alert(JSON.stringify(value.president[i].regionId))
      // alert(value.president[i].regionId)
      this.exportReportService.getAllReportByPresidentId(value.president[i].regionId).subscribe(res => {
        console.log("RES President => ", res);

        this.filter = res;
        if (this.startDate != undefined) {
          this.filter.reports = res.reports.filter(result => {
            let createAt = new Date(result.createAt)
            console.log("result.createAt123", createAt.toLocaleDateString());
            return (createAt.toLocaleDateString() == this.startdateforexport)
          })
        }
        console.log("Filter President => ", this.filter);
        this.exportReportService.reportRegion(this.filter, "รายเขต").subscribe(res => {
          console.log("export: ", res);
          window.open(this.url + "Uploads/" + res.data);
          this.modalRef.hide();
        })
      })
    }

    // this.exportReportService.getAllReportByRegionId(value).subscribe(res => {
    //   // console.log("RESSS: ", res);

    //   // this.exportReportService.reportRegion(res, this.checkType).subscribe(res => {
    //   //   console.log("export: ", res);
    //   //   window.open(this.url + "Uploads/" + res.data);
    //   //   this.modalRef.hide();
    //   // })

    // })

  }

  exportRegion(value) {
    console.log("Department Value: ", value);
    this.exportReportService.getAllReportByRegionId(value).subscribe(res => {
      // console.log("RESSS: ", res);

      if (res.reports.length == 0) {
        console.log("IN NO DATA");
        this._NotofyService.onFalse("ขออภัย", "ไม่มีข้อมูล");
      } else {
        this.filter = res;
        if (this.startDate != undefined) {
          this.filter.reports = res.reports.filter(result => {
            let createAt = new Date(result.createAt)
            console.log("result.createAt123", createAt.toLocaleDateString());
            return (createAt.toLocaleDateString() == this.startdateforexport)
          })
        }
        // console.log("this.filter", this.filter)
        if (this.filter.reports.length == 0) {
          console.log("IN NO DATA");
          this._NotofyService.onFalse("ขออภัย", "ไม่มีข้อมูล");
        } else {
          this.exportReportService.reportRegion(this.filter, this.checkType).subscribe(res => {
            console.log("export: ", res);
            window.open(this.url + "Uploads/" + res.data);
            this.modalRef.hide();
          })
        }
      }
    })
  }

  exportZone(value) {
    console.log("Department Value: ", value);
    this.exportReportService.getAllReportByZoneId(value).subscribe(res => {
      if (res.reports.length == 0) {
        console.log("IN NO DATA");
        this._NotofyService.onFalse("ขออภัย", "ไม่มีข้อมูล");
      } else {
        this.filter = res;
        if (this.startDate != undefined) {
          this.filter.reports = res.reports.filter(result => {
            let createAt = new Date(result.createAt)
            console.log("result.createAt123", createAt.toLocaleDateString());
            return (createAt.toLocaleDateString() == this.startdateforexport)
          })
        }
        // console.log("this.filter", this.filter)
        if (this.filter.reports.length == 0) {
          console.log("IN NO DATA");
          this._NotofyService.onFalse("ขออภัย", "ไม่มีข้อมูล");
        } else {
          this.exportReportService.reportZone(this.filter, this.checkType).subscribe(res => {
            console.log("export: ", res);
            window.open(this.url + "Uploads/" + res.data);
            this.modalRef.hide();
          })
        }
      }

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
      if (res.reports.length == 0) {
        console.log("IN NO DATA");
        this._NotofyService.onFalse("ขออภัย", "ไม่มีข้อมูล");
      } else {
        this.filter = res;
        if (this.startDate != undefined) {
          this.filter.reports = res.reports.filter(result => {
            let createAt = new Date(result.createAt)
            console.log("result.createAt123", createAt.toLocaleDateString());
            return (createAt.toLocaleDateString() == this.startdateforexport)
          })
        }

        if (this.filter.reports.length == 0) {
          console.log("IN NO DATA");
          this._NotofyService.onFalse("ขออภัย", "ไม่มีข้อมูล");
        } else {
          this.exportReportService.reportProvince(this.filter, this.checkType).subscribe(res => {
            console.log("export: ", res);
            window.open(this.url + "Uploads/" + res.data);
            this.modalRef.hide();
          })
        }
      }
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
      if (res.reports.length == 0) {
        console.log("IN NO DATA");
        this._NotofyService.onFalse("ขออภัย", "ไม่มีข้อมูล");
      } else {
        this.exportReportService.reportDay(res, this.checkType).subscribe(res => {
          console.log("export: ", res);
          window.open(this.url + "Uploads/" + res.data);
          this.modalRef.hide();
        })

        // this.reportForm.reset();
        // this.loading = false;
        // this.getImportedReport();
        // this.modalRef.hide();
      }
    })
  }

  onStartDateChanged(value) {
    console.log("startDateChange: ", value);
    this.startDate = value.date.year + '-' + value.date.month + '-' + value.date.day;
    this.startdateforexport = value.date.month + '/' + value.date.day + '/' + value.date.year;
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

  changeActive(reportId) {
    this.exportReportService.changeActive(reportId).subscribe(res => {
      console.log("active: ", res.active);
      this.loading = false;
      this._NotofyService.onSuccess()
      this.getImportedReport();
    })
  }

  ExportAll(value) {

    // alert(JSON.stringify(value.zone))
    if (value.zone == null) {
      this.checkType = "รายวัน";
      this.exportDay(value);
      return;
    }

    if (value.president == null) {
      this.checkType = "รายภาค";
      this.exportZone(value);
    } else if (value.region == null) {
      this.checkType = "รายเขตนายก";
      this.exportPresident(value);
    } else if (value.province == null) {
      this.checkType = "รายเขต";
      this.exportRegion(value);
    } else if (value.department == null) {
      this.checkType = "รายจังหวัด";
      this.exportProvince(value);
    } else {
      this.checkType = "รายหน่วยงาน";
      this.exportDepartment(value);
    }

    console.log("valuevaluevalue", value)
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

  selectFiscalYear(value) {
    this.fiscalYearId = value.value;
    this.getZone();
    // this.getImportFiscalYearRelations();
  }

  selectZone() {
    // alert(123)
    // this.fiscalYearId = value.value;
    this.getPresident();
    // this.getImportFiscalYearRelations();
  }
  selectPresident() {
    this.getRegion();
    // this.fiscalYearId = value.value;
    // this.getZone();
    // this.getImportFiscalYearRelations();
  }
  selectRegion(value) {
    this.regionId = value.value;
    this.exportReportService.getImportReportprovinceFiscalYearRelations(this.fiscalYearId, this.regionId).subscribe(res => {
      console.log("fiscalYearRelations: ", res);

      var uniqueRegion: any = [];
      uniqueRegion = res.importFiscalYearRelations.filter(
        (thing, i, arr) => arr.findIndex(t => t.provinceId === thing.provinceId) === i
      );
      console.log("uniqueProvinces: ", uniqueRegion);

      this.provinceData = uniqueRegion.map((item, index) => {
        return {
          value: item.province.id,
          label: item.province.name
        }
      })
    })
  }

  selectProvince(value) {
    this.provinceId = value.value;
    this.exportReportService.getImportReportdepartmentFiscalYearRelations(this.provinceId).subscribe(res => {
      console.log("fiscalYearRelations: ", res);

      var uniqueRegion: any = [];
      uniqueRegion = res.importFiscalYearRelations.filter(
        (thing, i, arr) => arr.findIndex(t => t.provincialDepartmentId === thing.provincialDepartmentId) === i
      );
      console.log("uniqueDepartments: ", uniqueRegion);

      this.departmentData = uniqueRegion.map((item, index) => {
        return {
          value: item.provincialDepartment.id,
          label: item.provincialDepartment.name
        }
      })
    })
  }

  sortDate() {
    this.loading = false;
    if (this.checkSort == 0) {
      this.exportReportService.sortDate(this.userid).subscribe(res => {
        console.log("importReport: ", res);
        this.importedReport = res;
        this.loading = true;
        this.checkSort = 1;
      })
    } else {
      this.exportReportService.sortDateDESC(this.userid).subscribe(res => {
        this.importedReport = res;
        this.loading = true;
        this.checkSort = 0;
      })
    }
  }

}
