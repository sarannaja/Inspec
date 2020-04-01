import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { CentralpolicyService } from '../services/centralpolicy.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from "ngx-spinner";

@Component({
  selector: 'app-central-policy',
  templateUrl: './central-policy.component.html',
  styleUrls: ['./central-policy.component.css']
})
export class CentralPolicyComponent implements OnInit {

  resultcentralpolicy: any = []
  delid: any
  modalRef: BsModalRef;
  dtOptions: DataTables.Settings = {};
  loading = false;

  constructor(
    private router:Router, 
    private centralpolicyservice: CentralpolicyService, 
    private modalService: BsModalService,
    private spinner: NgxSpinnerService) { }

  ngOnInit() {
    this.spinner.show();

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
      this.loading = true;
      this.spinner.hide();
      console.log(this.resultcentralpolicy);
    })

  }

  openModal(template: TemplateRef<any>, id) {
    this.delid = id;
    this.modalRef = this.modalService.show(template);
  }

  Subject(id) {
    this.router.navigate(['/subject', id])
  }

  deleteCentralPolicy(value) {
    this.centralpolicyservice.deleteCentralPolicy(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.centralpolicyservice.getcentralpolicydata().subscribe(result => {
        this.resultcentralpolicy = result
        console.log(this.resultcentralpolicy);
      })
    })
  }

  CreateCentralPolicy(){
    this.router.navigate(['/centralpolicy/createcentralpolicy'])
  }
  DetailCentralPolicy(id:any){
    this.router.navigate(['/centralpolicy/detailcentralpolicy',id])
  }
}
