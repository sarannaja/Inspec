import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ActivatedRoute } from '@angular/router';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';

@Component({
  selector: 'app-detail-executive-order',
  templateUrl: './detail-executive-order.component.html',
  styleUrls: ['./detail-executive-order.component.css']
})
export class DetailExecutiveOrderComponent implements OnInit {

  modalRef: BsModalRef;
  id :any
  resultdetailcentralpolicy: any = []

  constructor(
    private modalService: BsModalService,
    private centralpolicyservice: CentralpolicyService, 
    private activatedRoute : ActivatedRoute,
    ) 
    { 
      this.id = activatedRoute.snapshot.paramMap.get('id')
    }

  ngOnInit() {
    
    this.getDetailCentralPolicy()
  }
  openModal(template: TemplateRef<any>) {
  
    this.modalRef = this.modalService.show(template);
  }
  getDetailCentralPolicy(){
    this.centralpolicyservice.getdetailcentralpolicydata(this.id)
    .subscribe(result => {
      this.resultdetailcentralpolicy = result
    })
  }
}
