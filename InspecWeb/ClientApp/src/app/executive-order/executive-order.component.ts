import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CentralpolicyService } from '../services/centralpolicy.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from 'src/app/services/user.service';
import { DetailexecutiveorderService } from '../services/detailexecutiveorder.service';

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
  userid: any;
  role_id:any;
  executive1data: any = [];
  
  constructor(
    private authorize: AuthorizeService,
    private userService: UserService,
    private executiveOrderService: DetailexecutiveorderService,
    private router:Router, 
    private detailexecutiveorderService: CentralpolicyService, 
    private modalService: BsModalService) { }
    private exportExecutiveService: DetailexecutiveorderService
    private executive1: DetailexecutiveorderService
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
  DetailExecutiveOrder(id:any){
    this.router.navigate(['executiveorder/detailexecutiveorder', id])
    // console.log(id)
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

            if(this.role_id != 3){
              this.detailexecutiveorderService.getcentralpolicydata()
              .subscribe(result => {
                
                this.resultcentralpolicy = result
                this.loading = true
              })
            }else{
              this.executiveOrderService.getCentralpolicydata(this.userid)
              .subscribe(result => {
                // console.log("role3" , result); 
                this.resultcentralpolicy = result
                this.loading = true
              })
            }
        })
      })
    } 
    //End getuser

    getexcutive1(){
      this.executive1.getexcutive1(this.userid).subscribe(results => {
        this.executive1data = results;
      })
    }
    
    exportExcutive() {
      alert("Export");
      this.exportExecutiveService.exportExecutive(this.userid).subscribe(res => {
        console.log("export: ", res);
        this.executive1data();
      })
    }
}
