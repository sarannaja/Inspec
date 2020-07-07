import { Component, OnInit } from '@angular/core';
import { ExternalOrganizationService } from 'src/app/services/external-organization.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserService } from 'src/app/services/user.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserManager } from 'oidc-client';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { OpmCaseDetailComponent } from '../modals/detail-modal/detail-modal.component';

@Component({
  selector: 'app-gcc1111-table',
  templateUrl: './gcc1111-table.component.html',
  styleUrls: ['./gcc1111-table.component.css']
})
export class Gcc1111TableComponent implements OnInit {
  results: Array<any>
  dtOptions: DataTables.Settings = {};
  loading: boolean = false
  userId
  modalRef: BsModalRef;

  constructor(
    private spinner: NgxSpinnerService,
    private extranalService: ExternalOrganizationService,
    private authorizeService: AuthorizeService,
    private modalService: BsModalService
      ) { }
  getData() {
    // this.userManager.getUser().then(result=>{
    //   console.log('result',result);
      
    // })
    this.extranalService.getGcc1111UserProvince( this.userId)
    .subscribe(result => {
      this.loading = false
      this.results = result
      this.loading = true
      this.spinner.hide();

    })
    // console.log("user", );
     
  }
  openModal(data) {
    this.modalRef = this.modalService.show(OpmCaseDetailComponent, {
      initialState: {
        title: data.objective_text,
        id:data.case_id,
        data: data,

      },
      class: 'modal-dialog-centered modal-lg'
    });
  }
  ngOnInit() {
    this.authorizeService.getUser()
    .subscribe(result => {
      this.userId = result.sub
     
    })
    setTimeout(()=>{this.getData()},100)
    
   
  }

}
