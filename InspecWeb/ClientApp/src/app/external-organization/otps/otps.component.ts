import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ExternalOrganizationService } from 'src/app/services/external-organization.service';
import { Regions } from 'src/app/models/otps';

@Component({
  selector: 'app-otps',
  templateUrl: './otps.component.html',
  styleUrls: ['./otps.component.css']
})
export class OtpsComponent implements OnInit {
  region: Array<Regions>

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private external: ExternalOrganizationService) {

  }
  ngOnInit() {
    this.external.getRegions()
      .subscribe(result => {
        this.region = result
        console.log('result',result);
        
      })
  }
}

