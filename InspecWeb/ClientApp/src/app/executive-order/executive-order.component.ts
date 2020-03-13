import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CentralpolicyService } from '../services/centralpolicy.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-executive-order',
  templateUrl: './executive-order.component.html',
  styleUrls: ['./executive-order.component.css']
})
export class ExecutiveOrderComponent implements OnInit {

  resultcentralpolicy: any = []
  modalRef: BsModalRef;
  dtOptions: DataTables.Settings = {};
  loading = false;

  constructor(
    private router:Router, 
    private centralpolicyservice: CentralpolicyService, 
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

    this.centralpolicyservice.getcentralpolicydata()
    .subscribe(result => {
      this.resultcentralpolicy = result
      this.loading = true
    })
  }
  DetailExecutiveOrder(id:any){
    this.router.navigate(['executiveorder/detailexecutiveorder', id])
  }
}
