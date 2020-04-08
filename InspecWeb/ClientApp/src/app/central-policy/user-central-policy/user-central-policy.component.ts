import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { CentralpolicyService } from '../../services/centralpolicy.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from "ngx-spinner";
import { AuthorizeService } from 'src/api-authorization/authorize.service';

@Component({
  selector: 'app-user-central-policy',
  templateUrl: './user-central-policy.component.html',
  styleUrls: ['./user-central-policy.component.css']
})
export class UserCentralPolicyComponent implements OnInit {

  resultcentralpolicy: any = []
  delid: any
  modalRef: BsModalRef;
  dtOptions: DataTables.Settings = {};
  loading = false;
  userid: string

  constructor(
    private router:Router,
    private centralpolicyservice: CentralpolicyService,
    private modalService: BsModalService,
    private authorize: AuthorizeService,
    private spinner: NgxSpinnerService) { }

  ngOnInit() {
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
        // alert(this.userid)
      })
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

    this.centralpolicyservice.getcentralpolicyuserinviteddata(this.userid)
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

  AcceptCentralPolicy(id: any){
    this.router.navigate(['/acceptcentralpolicy',id])
  }

}
