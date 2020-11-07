import { Component, OnInit, TemplateRef } from '@angular/core';
import { FiscalyearService } from 'src/app/services/fiscalyear.service';
import { ActivatedRoute } from '@angular/router';
import * as _ from "lodash";
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { RegionService } from 'src/app/services/region.service';
import { ProvinceService } from 'src/app/services/province.service';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

import { NgxSpinnerService } from "ngx-spinner";

@Component({
  selector: 'app-detail-governmentinspectionarea',
  templateUrl: './detail-governmentinspectionarea.component.html',
  styleUrls: ['./detail-governmentinspectionarea.component.css']
})
export class DetailGovernmentinspectionareaComponent implements OnInit {

  loading = false;
  dtOptions: DataTables.Settings = {};
  resultdetailfiscalyear: any[] = []
  resultregion: any[] = []
  resultprovince: any[] = []
  resultprovincerecycled: any[] = []
  modalRef: BsModalRef;
  config = {
    keyboard: false,
    ignoreBackdropClick: true
  };
  Form: FormGroup;
  id: any
  fiscalyearid2: any
  fiscalyearid: any
  regionId: Array<any> = []
  provincerecycledId: Array<any> = []
  FiscalYearId: any
  RegionId: any
  ProvinceId: any;
  regions: any[] = [];
  provinces: any[] = [];
  activeModal: boolean = false
  ascending: any = [];
  valueRegionId: any
  valueProvinceId: any
  valuedeleteRegionId: any
  checkedit: any;
  constructor(
    private modalService: BsModalService,
    private fiscalyearService: FiscalyearService,
    activateRoute: ActivatedRoute,
    private regionservice: RegionService,
    private provinceservice: ProvinceService,
    private fb: FormBuilder,
    private spinner: NgxSpinnerService) {
    this.fiscalyearid = activateRoute.snapshot.paramMap.get('id')
  }

  ngOnInit() {
    this.spinner.show();
    this.dtOptions = {
      pagingType: 'full_numbers',
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
    this.Form = this.fb.group({
      RegionId: new FormControl(null, [Validators.required]),
      ProvinceId: new FormControl(null, [Validators.required])
    })
    this.getProvinceRecycled()
    this.getData()

  }
  getProvinceRecycled() {
    this.fiscalyearService.getProvinceRecycled(this.fiscalyearid)
      .subscribe(result => {
        this.resultprovincerecycled = result
      })
  }
  getData() {
    this.checkedit = 0;
    // this.activeModal = false
    this.fiscalyearService.getDetailFiscalyear(this.fiscalyearid)
      .subscribe(result => {
        this.resultdetailfiscalyear = _.chain(result)
          .groupBy('regionId').map(function (v, i) {
            return {
              id: _.get(_.find(v, 'id'), 'id'),
              regionId: _.get(_.find(v, 'regionId'), 'regionId'),
              region: _.get(_.find(v, 'region'), 'region'),
              province: _.map(v, 'province')
            }
          }).value();
        this.loading = true;
        // alert("mo6 : "+JSON.stringify(this.resultdetailfiscalyear));
        // alert("mo7 : "+JSON.stringify(this.resultdetailfiscalyear.length));

        //<!-- เอาไว้เช็ค ว่ามี เขตตรวจกี่อันแล้ว 20200918-->
        this.regionId = this.resultdetailfiscalyear.map((item, index) => {
          return item.regionId
        })
        this.provincerecycledId = this.resultprovincerecycled.map((item, index) => {
          return item.provinceId
        })
        this.getProvinceRegion()

        this.spinner.hide();
      });
  }

  getdataRelation() {
    this.getData()
    this.getProvinceRecycled()
    this.getProvinceRegion()
  }

  getProvinceRegion() {
    this.resultprovince = [];

    this.provinceservice.getprovincedata2()
      .subscribe(result => {
        this.provinces = []
        this.resultprovince = result

        if (this.provincerecycledId.length == 0) {
          for (var i = 0; i < this.resultprovince.length; i++) {
            this.provinces.push({ value: this.resultprovince[i].id.toString(), label: this.resultprovince[i].name })
          }
        } else {
          for (var i = 0; i < this.resultprovince.length; i++) {
            var n = 0;
            for (var ii = 0; ii < this.provincerecycledId.length; ii++) {
              if (this.resultprovince[i].id == this.provincerecycledId[ii]) {
                n++;
              }
            }
            if (n == 0) {
              this.provinces.push({ value: this.resultprovince[i].id.toString(), label: this.resultprovince[i].name })
            }
          }
        }

      });

  }
}
