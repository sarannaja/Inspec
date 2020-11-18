import { Component, OnInit } from '@angular/core';
import { ExternalOrganizationService } from 'src/app/services/external-organization.service';
import { ProvinceOtps } from '../../models/province-otps';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';

@Component({
  selector: 'app-province-otps-table',
  templateUrl: './province-otps-table.component.html',
  styleUrls: ['./province-otps-table.component.css']
})
export class ProvinceOtpsTableComponent implements OnInit {
  dtOptions: any = {};
  loading: boolean = false
  results: Array<ProvinceOtps>
  constructor(private external: ExternalOrganizationService,
    private sanitizer: DomSanitizer
  ) { }

  ngOnInit() {
    this.getData()
  }
  transform(base64Image) {
    return this.sanitizer.bypassSecurityTrustResourceUrl(base64Image);
  }
  // public get htmlProperty(base64Image) {
  //   return this.sanitizer.bypassSecurityTrustResourceUrl(base64Image);
  // }
  goToWebSite(url:string){
    var newUrl = `http://${url.replace('http://'||'https://','')}` 
    console.log(newUrl);
    // "" + url.replace('http://'||'https://','')
    window.open(newUrl)
    // window.
  }
  goToMap(url:string){
    
    var newUrl = `https://www.google.co.th/maps/place/${url}` 
    console.log(newUrl);
    // "" + url.replace('http://'||'https://','')
    window.open(newUrl)
    // window.
  }

  getData() {

    this.external.getOtpsProviceOtps()
      .subscribe(result => {
        this.results = result.map(resultMap => {
          return { ...resultMap, Seal: `data:image/jpeg;base64, ${resultMap.Seal}` }
        })
        console.log(result);
        this.dtOptions = {
          pagingType: 'full_numbers',
          pageLength: 10,
          // processing: true
        };
        this.loading = true
      })

  }

}
