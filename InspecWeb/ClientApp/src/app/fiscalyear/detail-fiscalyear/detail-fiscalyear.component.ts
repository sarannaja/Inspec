import { Component, OnInit, TemplateRef } from '@angular/core';
import { FiscalyearService } from 'src/app/services/fiscalyear.service';
import { ActivatedRoute } from '@angular/router';
import * as _ from "lodash";
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { RegionService } from 'src/app/services/region.service';
import { ProvinceService } from 'src/app/services/province.service';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { IOption } from 'ng-select';

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
  modalRef: BsModalRef;
  Form: FormGroup;
  id: any
  FiscalYearId: any
  RegionId: any
  ProvinceId: any;
  public regions: Array<IOption>;
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
    private fb: FormBuilder) {
    this.id = activateRoute.snapshot.paramMap.get('id')
  }

  ngOnInit() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [3],
          orderable: false
        }
      ]
    }
    this.Form = this.fb.group({
      RegionId: new FormControl(null, [Validators.required]),
      ProvinceId: new FormControl(null, [Validators.required])
    })



    this.fiscalyearService.getDetailFiscalyear(this.id)
      .subscribe(result => {

        this.resultdetailfiscalyear = _.chain(result).groupBy('regionId').map(function (v, i) {
          return {
            regionId: _.get(_.find(v, 'regionId'), 'regionId'),
            region: _.get(_.find(v, 'region'), 'region'),
            province: _.map(v, 'province')
          }
        }).value();
        this.loading = true;
        console.log(this.resultdetailfiscalyear);


        // this.results = 
        // result.forEach((item, index) => {
        //   console.log(index);
        //   if (this.results.length == 0) {
        //     this.results.push({
        //       id: item.id,
        //       fiscalYearId: item.fiscalYearId,
        //       regionId: item.regionId,
        //       region: [{ id: item.region.id, name: item.region.name }],
        //       provinceId: item.provinceId,
        //       province: [{ id: item.province.id, name: item.province.name }]
        //     })
        //     // return {...item}
        //     // console.log(this.results);

        //   } else if (item.regionId == this.results[index-1].regionId){
        //      if(item.provinceId != this.results[index-1].provinceId){
        //      this.results[index-1].province.push({id: item.province.id, name: item.province.name })
        //      }else{
        //       this.results.push({
        //         id: item.id,
        //         fiscalYearId: item.fiscalYearId,
        //         regionId: item.regionId,
        //         region: [{ id: item.region.id, name: item.region.name }],
        //         provinceId: item.provinceId,
        //         province: [{ id: item.province.id, name: item.province.name }]
        //       })
        //      }
        //   } else if(item.regionId != this.results[index-1].regionId) {

        //     this.results.push({
        //       id: item.id,
        //       fiscalYearId: item.fiscalYearId,
        //       regionId: item.regionId,
        //       region: [{ id: item.region.id, name: item.region.name }],
        //       provinceId: item.provinceId,
        //       province: [{ id: item.province.id, name: item.province.name }]
        //     })

        //   }else {

        //   }

        //     // this.results.forEach((element,index2) => {
        //     //   if(element)
        //     // });

        //     // for (let i = 0; i < this.results.length; i++) {


        //       // else{
        //       //   this.results.push({
        //       //     id: item.id,
        //       //     fiscalYearId: item.fiscalYearId,
        //       //     regionId: item.regionId,
        //       //     region: [{ id: item.region.id, name: item.region.name }],
        //       //     provinceId: item.provinceId,
        //       //     province: [{ id: item.province.id, name: item.province.name }]
        //       //   })
        //       // }


        //     // }

        // })
        // this.results = result

        // console.log(this.results);
      })

    this.regionservice.getregiondata().subscribe(result => {
      this.resultregion = result
      this.loading = true;
      console.log(this.resultregion);

      this.regions = this.resultregion.map((item, index) => {
        return { value: item.id, label: item.name }
      })
    })
    this.provinceservice.getprovincedata().subscribe(result => {
      this.resultprovince = result

      this.resultprovince = this.resultprovince.map((item, index) => {
        return { value: item.id, label: item.name }
      })

      console.log(this.resultprovince);
    })


  }
  openModal(template: TemplateRef<any>, id) {
    // this.id = id;
    // alert(this.id)

    this.modalRef = this.modalService.show(template);
  }

  AddRelation(value) {
    this.fiscalyearService.addDetailFiscalyear(value, this.id).subscribe(response => {

    // console.clear();
    // console.log(value);
    // console.log(this.Form.value);

    this.Form.reset()
    this.modalRef.hide()
    this.loading = false
      this.fiscalyearService.getDetailFiscalyear(this.id)
      .subscribe(result => {

        this.resultdetailfiscalyear = _.chain(result).groupBy('regionId').map(function (v, i) {
          return {
            regionId: _.get(_.find(v, 'regionId'), 'regionId'),
            region: _.get(_.find(v, 'region'), 'region'),
            province: _.map(v, 'province')
          }
        }).value();
        this.loading = true;
        console.log(this.resultdetailfiscalyear);
      })
      
    })
  }
}
