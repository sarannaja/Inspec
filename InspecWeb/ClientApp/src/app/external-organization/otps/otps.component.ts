import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ExternalOrganizationService } from 'src/app/services/external-organization.service';
import { Regions, Ministers, Cabinets } from 'src/app/models/otps';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-otps',
  templateUrl: './otps.component.html',
  styleUrls: ['./otps.component.css']
})
export class OtpsComponent implements OnInit {
  region: Array<Regions>
  ministers: Array<Ministers>
  cabinets: Array<Cabinets>
  loading: boolean = false
  constructor(http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private externalOrganizationService: ExternalOrganizationService,
    private spinner: NgxSpinnerService) {

  }
  ngOnInit() {
    this.spinner.show();
    

  
    // this.external.getRegions()
    //   .subscribe(result => {
    //     this.region = result
    //     console.log('result',result);

    //   })
    console.log("/external-organization/otps");

  }
}

