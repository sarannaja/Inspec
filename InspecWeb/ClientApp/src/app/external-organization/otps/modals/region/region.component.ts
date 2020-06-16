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
  FiscalYears: FiscalYear[] = []
  constructor(private external: ExternalOrganizationService) { }

  ngOnInit() {
    this.external.getRegionById(this.region.Id)
      .subscribe(result => {
        console.log(result,this.region.Id);
        
        this.newRegion = result
        this.FiscalYears = result.FiscalYears
      })
    console.log(this.region);

  }

}
