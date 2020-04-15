import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DetailexecutiveorderService } from 'src/app/services/detailexecutiveorder.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-executive-order',
  templateUrl: './executive-order.component.html',
  styleUrls: ['./executive-order.component.css']
})
export class ExecutiveOrderComponent implements OnInit {

  resultexecutiveorder: any = []
  modalRef: BsModalRef;
  dtOptions: DataTables.Settings = {};
  loading = false;

  constructor(
    private router:Router, 
    private detailexecutiveorderService: DetailexecutiveorderService,  
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

    this.detailexecutiveorderService.getExecutiveorderdata()
    .subscribe(result => {
      this.resultexecutiveorder = result
      this.loading = true
    })
  }
  DetailExecutiveOrder(id:any){
    this.router.navigate(['executiveorder/detailexecutiveorder', id])
  }
}
