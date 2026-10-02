import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';

import { NgxSpinnerService } from 'ngx-spinner';

import { SubjectService } from '../services/subject.service';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { InspectionplanService } from '../services/inspectionplan.service';
import { RegionService } from '../services/region.service';
import { UserService } from 'src/app/services/user.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { after } from 'lodash';
import { NotofyService } from '../services/notofy.service';

@Component({
  selector: 'app-summary-central-policy',
  templateUrl: './summary-central-policy.html',
  styleUrls: ['./summary-central-policy.css']
})
export class SummaryCentralPolicyComponent implements OnInit {

  // ==========================================
  // Zone Tabs
  // ==========================================

  zones = [];

  selectedZone = 1;


  // ==========================================
  // DataTable
  // ==========================================

  dtOptions: any = {};


  // ==========================================
  // Loading
  // ==========================================

  loading = false;


  // ==========================================
  // Data
  // ==========================================

  resultsubjectevent: any[] = [];


  // ==========================================
  // User
  // ==========================================

  userid: string;
  resultuser: any[];

  summaryForm!: FormGroup;
  summaryEditForm!: FormGroup;
  modalRef: BsModalRef;
  savingSummary: Boolean = false;
  centralPolicyId: any;
  summaryId: any

  // ==========================================
  // Constructor
  // ==========================================


  constructor(
    private spinner: NgxSpinnerService,
    private subjectservice: SubjectService,
    private authorize: AuthorizeService,
    private inspectionplanservice: InspectionplanService,
    private router: Router,
    private regionService: RegionService,
    private userService: UserService,
    private modalService: BsModalService,
    private fb: FormBuilder,
    private _NotofyService: NotofyService,
  ) { }


  // ==========================================
  // On Init
  // ==========================================

  ngOnInit() {

    this.spinner.show();

    this.authorize.getUser()
      .subscribe(result => {


        this.userid = result.sub
        this.userService.getuserfirstdata(this.userid)
          .subscribe(result => {
            console.log('user data =>', result);
            // this.resultuser = result.userRegion
            this.zones = result[0].userRegion.map(region => ({
              id: region.regionId,
              name: region.region.name
            }));

            this.selectedZone = this.zones.length > 0 ? this.zones[0].id : null;
          })

        // this.userid = result.sub;

        // console.log('user data =>', result);

      });


    this.dtOptions = {

      pagingType: 'full_numbers',

      ordering: true,

      "language": {

        "lengthMenu": "แสดง  _MENU_  รายการ",

        "search": "ค้นหา:",

        "info": "แสดง _START_ ถึง _END_ จาก _TOTAL_ แถว",

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

    this.getRegionData();
    this.getSubjectevent();

    this.summaryForm = this.fb.group({
      detail: ['', Validators.required]
    });

    this.summaryEditForm = this.fb.group({
      detail: ['', Validators.required]
    });

    this.savingSummary = false;
  }
  region: any[] = [];

  getRegionData() {
    this.regionService.getregiondataforuser().subscribe(res => {

      console.log('region data =>', res);
      console.log('is array =>', Array.isArray(res));

      this.region = res.importFiscalYearRelations;

      this.filterByZone();

    });
  }
  // ==========================================
  // Get Subject Event
  // ==========================================
  getSubjectevent() {

    this.subjectservice
      .centralPolicySummary(this.userid)
      .subscribe(result => {

        console.log("SUBJECTEVENT ==> ", result);

        this.resultsubjectevent = result;

        this.filterByZone();

        // this.loading = true;

        this.spinner.hide();

      });

  }


  // ==========================================
  // Subject Event Detail
  // ==========================================

  Subjectevent(
    centralPolicyId
  ) {
        this.router.navigate([
          '/summarycentralpolicy/detail/' + centralPolicyId + '/' + this.selectedZone
        ]);
  }


  // ==========================================
  // Select Zone
  // ==========================================

  selectZone(zoneId: number): void {

    this.selectedZone = zoneId;

    this.filterByZone();

  }

  filteredSubjectevent: any[] = [];

  filterByZone(): void {
    this.loading = false;
    if (!Array.isArray(this.resultsubjectevent)) {
      return;
    }

    if (!Array.isArray(this.region)) {
      console.log('region ไม่ใช่ Array =>', this.region);
      return;
    }

    this.filteredSubjectevent = this.resultsubjectevent
      .map(item => {

        // จังหวัดทั้งหมดของ Policy นี้
        const provincesInZone = item.provinces.filter(province => {

          const regionData = this.region.find(
            r => r.provinceId === province.id
          );

          return regionData &&
            regionData.regionId === this.selectedZone;

        });

        // ถ้า Policy นี้ไม่มีจังหวัดใน Zone ที่เลือก
        // ไม่ต้องเอามาแสดง
        if (provincesInZone.length === 0) {
          return null;
        }

        return {
          id: item.id,
          centralPolicyId: item.centralPolicyId,
          title: item.title,
          detail: item.detail,
          summaryId: item.summaryId,
          // เก็บ province ที่ filter แล้ว
          provinces: provincesInZone,

          // เอาชื่อจังหวัดมาต่อด้วย ,
          provinceName: provincesInZone
            .map(p => p.name)
            .join(', ')
        };

      })
      .filter(item => item !== null);
    setTimeout(() => {
      this.loading = true;
    }, 1000);
    console.log('filteredSubjectevent =>', this.filteredSubjectevent);
  }

  closeSummaryModal() {
    this.modalRef.hide();
  }

  openModal(template: TemplateRef<any>, centralPolicyId) {
    this.centralPolicyId = centralPolicyId
    this.modalRef = this.modalService.show(template);
  }

  openEditModal(template: TemplateRef<any>, summaryId, detail) {

    // console.log('summaryId => ', summaryId);
    // console.log('summaryId => ', detail);

    this.summaryId = summaryId
    this.summaryEditForm.patchValue({
      detail: detail
    })
    this.modalRef = this.modalService.show(template);
  }

  saveSummary() {
    this.savingSummary = true
    this.loading = true;

    this.subjectservice
      .postSummary(this.summaryForm, this.userid, this.centralPolicyId)
      .subscribe(result => {
        console.log('res sum => ', result);
        this._NotofyService.onSuccess("เพิ่มสรุป",);
        this.summaryForm.reset();
        this.getSubjectevent();
        this.closeSummaryModal()
        this.savingSummary = false;
        this.loading = false;

      });
  }

  editSummary() {
    this.savingSummary = true
    this.loading = true;

    this.subjectservice
      .editSummary(this.summaryEditForm, this.userid, this.summaryId)
      .subscribe(result => {
        // console.log('res sum => ', result);
        this._NotofyService.onSuccess("แก้ไขสรุป",);
        this.summaryEditForm.reset();
        this.getSubjectevent();
        this.closeSummaryModal()
        this.savingSummary = false;
        this.loading = false;

      });
  }

}
