import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ActivatedRoute } from '@angular/router';
import { DetailexecutiveorderService } from 'src/app/services/detailexecutiveorder.service';

@Component({
  selector: 'app-detail-executive-order',
  templateUrl: './detail-executive-order.component.html',
  styleUrls: ['./detail-executive-order.component.css']
})
export class DetailExecutiveOrderComponent implements OnInit {

  modalRef: BsModalRef;
  id :any
  resultdetailexecutiveorder: any = []

  constructor(
    private modalService: BsModalService,
    private detailexecutiveorderService: DetailexecutiveorderService, 
    private activatedRoute : ActivatedRoute,
    ) 
    { 
      this.id = activatedRoute.snapshot.paramMap.get('id')
    }

  ngOnInit() {
    
    this.getDetailExecutiveOrder()
  }
  openModal(template: TemplateRef<any>) {
  
    this.modalRef = this.modalService.show(template);
  }
  getDetailExecutiveOrder(){
    this.detailexecutiveorderService.getdetailexecutiveorderdata(this.id)
    .subscribe(result => {
      this.resultdetailexecutiveorder = result
    })
  }
}