import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { NgxSpinnerService } from 'ngx-spinner';

import { SubjectService } from '../services/subject.service';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { InspectionplanService } from '../services/inspectionplan.service';
import { RegionService } from '../services/region.service';
import { UserService } from 'src/app/services/user.service';

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

      this.loading = true;

      this.spinner.hide();

    });

}


  // ==========================================
  // Subject Event Detail
  // ==========================================

  Subjectevent(
    id,
    centralPolicyId,
    provinceId
  ) {

    this.inspectionplanservice
      .getcentralpolicyprovinceid(
        centralPolicyId,
        provinceId
      )
      .subscribe(result => {

        this.router.navigate([
          '/subjectevent/detail/' + result,
          {
            subjectgroupid: id
          }
        ]);

      });

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

        // เก็บ province ที่ filter แล้ว
        provinces: provincesInZone,

        // เอาชื่อจังหวัดมาต่อด้วย ,
        provinceName: provincesInZone
          .map(p => p.name)
          .join(', ')
      };

    })
    .filter(item => item !== null);

  console.log('filteredSubjectevent =>', this.filteredSubjectevent);
}

}