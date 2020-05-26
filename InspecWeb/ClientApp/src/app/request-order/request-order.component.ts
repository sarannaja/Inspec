import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CentralpolicyService } from '../services/centralpolicy.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-request-order',
  templateUrl: './request-order.component.html',
  styleUrls: ['./request-order.component.css']
})
export class RequestOrderComponent implements OnInit {

  resultcentralpolicy: any = []
  modalRef: BsModalRef;
  dtOptions: DataTables.Settings = {};
  loading = false;

  constructor(
    private router:Router, 
    private requestorderService: CentralpolicyService, 
    private modalService: BsModalService) { }

  ngOnInit() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [5],
          orderable: false
        }
      ]

    };

    this.requestorderService.getcentralpolicydata()
    .subscribe(result => {
      this.resultcentralpolicy = result
      this.loading = true
    })
  }
  DetailRequestOrder(id:any){
    this.router.navigate(['requestorder/detailrequestorder', id])
  }
}