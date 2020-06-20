import { Component, OnInit, Input } from '@angular/core';
import { ExternalOrganizationService } from 'src/app/services/external-organization.service';
import { Ministers, Cabinets, Regions, MinisterRegions } from '../../models/otps';
import { NgxSpinnerService } from 'ngx-spinner';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { MinisterModalComponent } from '../modals/minister-modal/minister-modal.component';
import * as _ from 'lodash';
import { RegionComponent } from '../modals/region/region.component';

@Component({
  selector: 'app-minister-table',
  templateUrl: './minister-table.component.html',
  styleUrls: ['./minister-table.component.css']
})
export class MinisterTableComponent implements OnInit {
  dtOptions: DataTables.Settings = {};
  ministers: Array<Ministers>
  cabinets: Array<Cabinets>
  cabinetDrop: Array<any>
  cabinet: any
  loading: boolean = false
  dataindata: Array<Ministers>
  fiscalyear: any[] = []
  year: any
  wara: any
  constructor(
    private externalOrganizationService: ExternalOrganizationService,
    private spinner: NgxSpinnerService,
    private modalService: BsModalService
  ) { }

  ngOnInit() {

    this.externalOrganizationService.getCabinets()
      .subscribe(results => {
        // console.log(results);

        this.cabinets = results

        // this.spinner.hide();
        // this.loading = true
        // let findDuplicates = arr => arr
        const findDuplicates = (arr, key) => {
          let sorted_arr = arr.slice().sort(); // You can define the comparing function here. 
          // JS by default uses a crappy string compare.
          // (we use slice to clone the array so the
          // original array won't be modified)
          let results = [];
          for (let i = 0; i < sorted_arr.length - 1; i++) {
            if (sorted_arr[i + 1][key] != sorted_arr[i][key]) {
              results.push(sorted_arr[i]);
            } else {
              console.log('not findDuplicates', sorted_arr[i]);
            }
          }
          return results;
        }
        let fiscalyear: Array<any> = []
        this.fiscalyear = _.uniqBy(this.cabinets.map(value => {
          console.log('value', value.fiscalYears[0].year);
          fiscalyear.push(value.fiscalYears[0])
          // let strArray =findDuplicates(value)

          return { ...value.fiscalYears[0], cabinet: value.name }
        })
          , 'year').sort(function (a, b) {
            // console.log(a);
            return b.year - a.year
          }); //removed if had duplicate id


        this.externalOrganizationService.getMinisters()
          .subscribe(results => {

            // console.log(results);

            this.dataindata = results
            this.ministers = results
            this.spinner.hide();
            // this.dataindata = this.ministers.filter(result => {
            //   // console.log(result.fiscalYears[0].year, this.fiscalyear[0].year);
            //   setTimeout(() => {
            //     this.loading = true

            //   }, 10)

            // this.year = this.fiscalyear[0].year;
            this.cabinetDrop = this.cabinets.map(result => { return result.name })
            this.cabinet = this.cabinetDrop[0]
            this.ChangeYear(this.fiscalyear[0].year)

            this.loading = true
          })
        // this.loading = true


      })
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      // processing: true
    };

  }
  ChangeYear(year) {
    // console.log(year);

    this.loading = false
    this.Changecabinet(year)

    // this.checkresult(this.year)
    //console.log(this.dataindata);

  }
  checkresult(year, cabinet: any = null) {
    this.dataindata = this.ministers.filter(result => {

      setTimeout(() => {
        this.loading = true

      }, 10)
      console.log(result.fiscalYears[0].year, result.cabinet.name, result.fiscalYears[0].year == year, result.cabinet.name == cabinet);


      return (result.fiscalYears[0].year == year && result.cabinet.name == cabinet)
    })
  }
  Changecabinet(year: any, cabinet: any = null) {
    var test = 'คำสั่งที่ 334/2560'
    this.cabinet = cabinet

    if (this.year != year) {
      this.year = year;
      this.cabinetDrop = this.cabinets.filter((item, index) => {
        return item.fiscalYears[0].year == year
      })
        .map(result => { return result.name })
      this.checkresult(year, this.cabinetDrop[0])

    } else {
      this.year = year;
      this.checkresult(this.year, cabinet)
    }
  }
  modalRef: BsModalRef;

  openModal(minister: Ministers) {
    this.modalRef = this.modalService.show(MinisterModalComponent, {
      initialState: {
        title: `${minister.name} ( ${minister.fiscalYears[0].name} ${minister.cabinet.name} )`,
        minister: minister,
        data: {},

      },
      class: 'modal-dialog-centered modal-md'
    });
  }

  openModalRegion(region: any) {
    console.log('region modal',region);
    
    this.modalRef = this.modalService.show(RegionComponent, {
      initialState: {
        title: `${region.name} ${region.id}`,
        region: region,
        Year: this.year,
        data: {},

      },
      class: 'modal-dialog-centered modal-lg'
    });
  }
  reportword(year) {
    // alert(year);
    this.dataindata = this.ministers.filter(result => {
      //  console.log(result.fiscalYears[0].year, year.target.value);
      setTimeout(() => {
        this.loading = true

      }, 10)

      return result.fiscalYears[0].year == year
    })

      console.log('data',this.dataindata)
  }
}