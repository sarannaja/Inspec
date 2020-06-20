import { Component, OnInit } from '@angular/core';
import { Regions } from '../../../models/otps';
import { HttpClient } from '@angular/common/http';
import { ExternalOrganizationService } from 'src/app/services/external-organization.service';
import { NewRegion, FiscalYear } from 'src/app/external-organization/models/Region';

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
  Year:any
  constructor(private external: ExternalOrganizationService) { }

  ngOnInit() {
    this.external.getRegionById(this.region.id)
      .subscribe(result => {
        console.log(result, this.region);

        this.newRegion = result
        this.FiscalYear = result.FiscalYears.filter(result => {
          return  result.Year == this.Year
        })[0]
      })
    console.log(this.region);

  }

}
