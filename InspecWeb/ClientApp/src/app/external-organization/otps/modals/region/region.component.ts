import { Component, OnInit } from '@angular/core';
import { Regions } from '../../../models/otps';
import { HttpClient } from '@angular/common/http';
import { ExternalOrganizationService } from 'src/app/services/external-organization.service';
import { NewRegion, FiscalYear, Projects } from 'src/app/external-organization/models/Region';
import { FiscalYear as FiscalYear2 } from 'src/app/models/otpsprovince';
import { ChartOptions, ChartType } from 'chart.js';
import { Label, SingleDataSet, monkeyPatchChartJsTooltip, monkeyPatchChartJsLegend, Color } from 'ng2-charts';

@Component({
  selector: 'app-region',
  templateUrl: './region.component.html',
  styleUrls: ['./region.component.css']
})
export class RegionComponent implements OnInit {
  title;
  region: any
  newRegion: NewRegion = null
  FiscalYear: FiscalYear
  provinces: any[] = []
  disableprovince: boolean = false
  Year: any
  loading: boolean = false
  public pieChartLabels: Label[] = [];
  public pieChartData: SingleDataSet = [];
  public pieChartType: ChartType = 'pie';
  public pieChartLegend = true;
  public pieChartPlugins = [];
  public pieChartcolors: Color[] = [
    {
      backgroundColor: []
    }
  ];
  public pieChartOptions: ChartOptions = {
    responsive: true,
  };


  // constructor() {

  // }
  constructor(private external: ExternalOrganizationService) {
    monkeyPatchChartJsTooltip();
    monkeyPatchChartJsLegend();
  }

  ngOnInit() {
    this.external.getRegionById(this.region.id)
      .subscribe(async result => {

        this.newRegion = result
        this.FiscalYear = result.FiscalYears.find(result => result.Year == this.Year)

        // console.log(this.setProvince(this.FiscalYear.Provinces));
        this.provinces = this.setProvince(this.FiscalYear.Provinces)
        console.log(' this.this.setProvince(this.FiscalYear.Provinces)', this.setProvince(this.FiscalYear.Provinces));
        // this.provinces
        this.disableprovince = true

        setTimeout(() => {
          this.pieChartsetUp(this.provinces)

          console.log(this.pieChartLabels, this.pieChartData, this.pieChartcolors);
        }, 1500)



      })

  }

  setProvince(array: any[]) {
    var provinces: any[] = []

    for (let i = 0; i < array.length; i++) {
      this.external.getOtpsProviceById(array[i].Id)
        .subscribe(async result2 => {
          // this.provinces.push()
          // console.log(
          provinces.push(
            result2.fiscalYears
              .map((result3 => {
                return { ...result3, province: array[i].Name }
              })).find(itemPro => this.Year == itemPro.year && this.region.id == itemPro.region.id)
          )

        })
    }
    return provinces
    // this.provinces = test
  }


  pieChartsetUp(data: any[]) {
    console.log('in setup', data[0].province);

    for (let i = 0; i < data.length; i++) {
      console.log(data[i].province, data[i].projects.count);

      this.pieChartLabels.push(data[i].province)
      this.pieChartData.push(data[i].projects.count)
      this.pieChartcolors[0].backgroundColor = this.pieChartcolors[0].backgroundColor.concat(this.getRandomColor())
    }
    this.loading = true
  }

  getRandomColor() {
    var color = Math.floor(0x1000000 * Math.random()).toString(16);
    return '#' + ('000000' + color).slice(-6);
  }



}

export interface mapProvince {

}