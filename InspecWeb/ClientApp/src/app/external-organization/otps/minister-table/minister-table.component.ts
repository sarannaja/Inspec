import { Component, OnInit, Input } from '@angular/core';
import { ExternalOrganizationService } from 'src/app/services/external-organization.service';
import { Ministers, Cabinets } from 'src/app/models/otps';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-minister-table',
  templateUrl: './minister-table.component.html',
  styleUrls: ['./minister-table.component.css']
})
export class MinisterTableComponent implements OnInit {
  dtOptions: DataTables.Settings = {};
  ministers: Array<Ministers>
  cabinets: Array<Cabinets>
  loading: boolean = false
  dataindata: Array<Ministers>
  fiscalyear: any = []
  constructor(private externalOrganizationService: ExternalOrganizationService, private spinner: NgxSpinnerService) { }

  ngOnInit() {

    this.externalOrganizationService.getCabinets()
      .subscribe(results => {
        console.log(results);

        this.cabinets = results
        // this.spinner.hide();
        // this.loading = true
        this.fiscalyear = this.cabinets.map(value => {
          return value.fiscalYears[0]
        }).sort(function (a, b) { return b.year - a.year });
        this.externalOrganizationService.getMinisters()
          .subscribe(results => {
            console.log(results);
            this.dataindata = results
            this.ministers = results
            // this.spinner.hide();
            // this.dataindata = this.ministers.filter(result => {
            //   // console.log( result.fiscalYears[0].year);

            //   return result.fiscalYears[0].year == this.fiscalyear[0].year
            // })
            this.loading = true
          })
        this.spinner.hide();
        // this.loading = true
        console.log(this.fiscalyear);

      })



    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      // processing: true
    };

  }
  ChangeYear(year) {
    console.log(year.target.value);

    this.dataindata = this.ministers.filter(result => {
      console.log( result.fiscalYears[0].year);

      return result.fiscalYears[0].year == year.target.value
    })
  }

}