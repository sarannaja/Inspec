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
  // results: Array<{
  //   id: number,
  //   fiscalYearId: number,
  //   regionId: number,
  //   region: Array<{
  //     id: number,
  //     name: string,
  //   }>
  //   province: Array<{
  //     id: number,
  //     name: string,
  //   }>
  //   provinceId: number,
  //   // province: null
  // }> = []
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
     
    }
    this.Form = this.fb.group({
      RegionId: new FormControl(null, [Validators.required]),
      ProvinceId: new FormControl(null, [Validators.required])
    })
    this.getProvinceRecycled()
    this.getData()

  }


  openModal(template: TemplateRef<any>) {
    if (this.regions.length == 0 && this.provinces.length == 0) {
      alert("ความสัมพันธ์ครบแล้ว")
    } else {
      this.modalRef = this.modalService.show(template, this.config);
    }

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
  openModalEdit(template: TemplateRef<any>) {

    this.modalRef = this.modalService.show(template);
  }
  getProvinceRecycled() {
    this.fiscalyearService.getProvinceRecycled(this.fiscalyearid)
      .subscribe(result => {
        this.resultprovincerecycled = result
      })
  }
  getData() {
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
        // console.log(this.resultdetailfiscalyear);

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
    this.regionservice.getregiondata()
      .subscribe(result => {
        console.log('region',this.regionId);
        this.regions = []
        this.valueRegionId
        this.resultregion = result

        if(this.regionId.length == 0){
          for(var i = 0; i < this.resultregion.length; i++){
            this.regions.push({ value: this.resultregion[i].id, label: this.resultregion[i].name })
          }
        } else {
          for (var i = 0; i < this.resultregion.length; i++){
            var n = 0;
            for(var ii = 0; ii < this.regionId.length; ii++){
              if(this.resultregion[i].id == this.regionId[ii]){
                n++;
              }
            }
            if(n == 0){
              this.regions.push({ value: this.resultregion[i].id, label: this.resultregion[i].name })
            }
          }
        }
      });
    this.provinceservice.getprovincedata2()
      .subscribe(result => {
        this.provinces = []
        this.resultprovince = result

        if(this.provincerecycledId.length == 0){
          for(var i = 0; i < this.resultprovince.length; i++){
            this.provinces.push({ value: this.resultprovince[i].id.toString(), label: this.resultprovince[i].name })
          }
        } else {
          for (var i = 0; i < this.resultprovince.length; i++){
            var n = 0;
            for(var ii = 0; ii < this.provincerecycledId.length; ii++){
              if(this.resultprovince[i].id == this.provincerecycledId[ii]){
                n++;
              }
            }
            if(n == 0){
              this.provinces.push({ value: this.resultprovince[i].id.toString(), label: this.resultprovince[i].name })
            }
          }
        }
        // // console.log("SSSS; ", this.resultprovince);
        // // console.log("CCCC: ", this.provincerecycledId);
        // for (var i = 0; i < this.resultprovince.length; i++) {
        //   // console.log("eeeee:", this.resultprovince[i].id);
        //   this.provincerecycledId.sort((a, b) => (a > b ? 1 : -1));
        //   // console.log("ASSSS:", this.ascending[i]);
        //   // console.log("DDDDDD:", this.ascending.length);
        //   if (this.resultprovince[i].id != this.provincerecycledId[i]) {
        //     this.provinces.push({ value: this.resultprovince[i].id.toString(), label: this.resultprovince[i].name })
        //   }
        // }
        // // console.log("eeee", this.regions);
      });

  }

  AddRelation(value) {
    this.fiscalyearService.addDetailFiscalyear(value, this.fiscalyearid).subscribe(response => {

      // console.clear();
      // console.log(this.fiscalyearid);
      // console.log("value",value.ProvinceId);
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
      console.log(this.id);
      console.log("value",value);
      this.valuedeleteRegionId = value
      this.modalRef.hide()
      this.loading = false
      this.getdataRelation()
      // location.reload()
    })
  }
}
