import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CentralpolicyService } from '../services/centralpolicy.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from 'src/app/services/user.service';
//import { RequestorderService} from '../services/requestorder.service';
import { RequestorderrService } from '../services/requestorderr.service';

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
  userid: any;
  role_id:any;

  constructor(
    private authorize: AuthorizeService,
    private userService: UserService,
    private requestOrderService: RequestorderrService,
    private router:Router, 
    private detailrequestOrderService: CentralpolicyService,
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
    this.getuserinfo();
  }
  DetailRequestOrder(id:any){
    this.router.navigate(['requestorder/detailrequestorder', id])
  }

  //start getuser
  getuserinfo(){
    this.authorize.getUser()
    .subscribe(result => {
      this.userid = result.sub  
      this.userService.getuserfirstdata(this.userid)      
      .subscribe(result => {  
        //alert(result[0].role_id)   
        //console.log("test" , result);      
        this.role_id = result[0].role_id

          if(this.role_id != 5){
            this.detailrequestOrderService.getcentralpolicydata()
            .subscribe(result => {
               //console.log("role5" , result); 
              this.resultcentralpolicy = result
              this.loading = true
            })
          }else{
            //alert("hi")
            // this.detailrequestOrderService.getcentralpolicydata()
            // .subscribe(result => {
              
            //   this.resultcentralpolicy = result
            //   this.loading = true
            // })
            //this.requestOrderService.getcentralpolicydata()
           this.requestOrderService.getCentralpolicydata(this.userid)
           .subscribe(result => {
              //console.log("role5" , result); 
             this.resultcentralpolicy = result
             this.loading = true
           })
          }
      })
    })
  }
  //End getuser

}