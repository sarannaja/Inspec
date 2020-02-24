import { Component, OnInit } from '@angular/core';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-detail-central-policy',
  templateUrl: './detail-central-policy.component.html',
  styleUrls: ['./detail-central-policy.component.css']
})
export class DetailCentralPolicyComponent implements OnInit {

  resultcentralpolicy: any = []
  id

  constructor(private centralpolicyservice: CentralpolicyService,
    private activatedRoute : ActivatedRoute,) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
  }

  ngOnInit() {
    this.centralpolicyservice.detailcentralpolicydata(this.id).subscribe(result => {
      // alert(JSON.stringify(result))
      this.resultcentralpolicy = result
      console.log(this.resultcentralpolicy);
    })
  }

}
