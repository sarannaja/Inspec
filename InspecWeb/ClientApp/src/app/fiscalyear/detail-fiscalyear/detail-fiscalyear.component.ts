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
  selector: 'app-detail-fiscalyear',
  templateUrl: './detail-fiscalyear.component.html',
  styleUrls: ['./detail-fiscalyear.component.css']
})
export class DetailFiscalyearComponent implements OnInit {

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



  openModal(template: TemplateRef<any>) {
    this.Form.reset()
    this.checkedit = 0;
    this.regionservice.getregiondata()
      .subscribe(result => {
        //  console.log('region',this.regionId);
        this.regions = []
        this.valueRegionId
        this.resultregion = result
        // alert("mo4 : "+this.regionId.length);
        // alert("mo5 : "+JSON.stringify(this.resultregion));
        if (this.regionId.length == 0) {
          for (var i = 0; i < this.resultregion.length; i++) {
            this.regions.push({ value: this.resultregion[i].id, label: this.resultregion[i].name })
            //alert(" mo1: "+this.resultregion[i].id);
          }
        } else {
          for (var i = 0; i < this.resultregion.length; i++) {
            var n = 0;

            for (var ii = 0; ii < this.regionId.length; ii++) {
              if (this.resultregion[i].id == this.regionId[ii]) {
                n++;
              }
              // alert(" mo2: "+this.resultregion[i].id + this.resultregion[i].name);
            }

            if (n == 0) {
              this.regions.push({ value: this.resultregion[i].id, label: this.resultregion[i].name })
              //alert(" mo3: "+this.resultregion[i].id);
            }
          }
        }
      });
    // alert(this.regions.length +" & " + this.provinces.length)
    if (this.regions.length == 0 && this.provinces.length == 0) {
      alert("ความสัมพันธ์ครบแล้ว")
    } else {
      this.modalRef = this.modalService.show(template, this.config);
    }

  }

  openModalEdit(template: TemplateRef<any>, id, region, province) {
    this.Form.reset()
    this.id = id
   // this.checkedit = 1;
  //  alert(JSON.stringify(this.regions))

    this.regionservice.getregiondata()
      .subscribe(result => {



        this.regions = []
        this.valueRegionId
        this.resultregion = result
        let regioin_fill_o: any[] = this.resultdetailfiscalyear.filter(result => result.regionId != region)
        this.regions = result
          .filter(x => regioin_fill_o.every(y => y.regionId != x.id))
          .map(resultregion => { return { value: resultregion.id, label: resultregion.name } })



        let province_o: any[] = []
        this.resultdetailfiscalyear
          .forEach(result => result.province
            .forEach(pro => province_o.push(pro)
            ))
        this.provinces = this.resultprovince
          .filter(x => province_o.filter(x => province.every(y => y.id != x.id)).every(y => y.id != x.id))
          .map(xxx => { return { value: xxx.id, label: xxx.name } })
       // console.log(this.provinces, province_o);

      
        this.Form.patchValue({
          RegionId: region,
          ProvinceId: province.map(result => {
            return result.id
          }),
        })


      });
  

    this.modalRef = this.modalService.show(template);
  }

  closeModel() {
    this.Form.reset()
    this.modalRef.hide()
  }

  openModalDelete(template: TemplateRef<any>, id) {
    // this.activeModal = true
    this.id = id
    console.log(this.id);

    this.modalRef = this.modalService.show(template);
  }
  AddRelation(value) {
    this.fiscalyearService.addDetailFiscalyear(value, this.fiscalyearid).subscribe(response => {
      this.valueRegionId = value.RegionId
      this.valueProvinceId = value.ProvinceId
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.getdataRelation()

    })
  }
  EditRelation(value, id) {
    this.fiscalyearService.editDetailFiscalyear(value, this.fiscalyearid,id).subscribe(response => {
      this.valueRegionId = value.RegionId
      this.valueProvinceId = value.ProvinceId
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.getdataRelation()

    })
  }
  deleteRelation(value) {
    this.fiscalyearService.deleteDetailFiscalyear(this.id, this.fiscalyearid).subscribe(response => {
      this.valuedeleteRegionId = value
      this.modalRef.hide()
      this.loading = false
      this.getdataRelation()
    })
  }
}
