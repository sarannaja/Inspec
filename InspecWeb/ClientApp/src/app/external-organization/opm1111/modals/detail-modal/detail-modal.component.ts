import { Component, OnInit } from '@angular/core';
import { Ministers } from '../../../models/otps';
import { OpmCase } from 'src/app/external-organization/models/opmcase';
import { ExternalOrganizationService } from 'src/app/services/external-organization.service';

@Component({
  selector: 'app-detail-modal',
  templateUrl: './detail-modal.component.html',
  styleUrls: ['./detail-modal.component.css']
})
export class OpmCaseDetailComponent implements OnInit {
  title;
  id
  result:OpmCase
  loading:boolean= false
  constructor(private external:ExternalOrganizationService) { }

  ngOnInit() {
    this.getData()
    console.log();
  }

  getData(){
    this.external.getGcc1111CaseDetail(this.id)
    .subscribe(result=>{
      this.loading = true
      this.result = result
    })
  }

}
