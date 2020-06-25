import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { CentralpolicyService } from '../services/centralpolicy.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from "ngx-spinner";
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from '../services/user.service';
import { FiscalyearService } from '../services/fiscalyear.service';

@Component({
  selector: 'app-central-policy',
  templateUrl: './central-policy.component.html',
  styleUrls: ['./central-policy.component.css']
})
export class CentralPolicyComponent implements OnInit {

  resultcentralpolicy: any = []
  resultcentralpolicyrold3: any = []
  resultfiscalyear: any = []
  delid: any
  modalRef: BsModalRef;
  dtOptions: DataTables.Settings = {};
  loading = false;
  userid
  role_id
  currentyear
  selectfiscalyearid

  constructor(
    private router: Router,
    private centralpolicyservice: CentralpolicyService,
    private fiscalyearservice: FiscalyearService,
    private modalService: BsModalService,
    private authorize: AuthorizeService,
    private userService: UserService,
    private spinner: NgxSpinnerService) { }

  ngOnInit() {
    this.spinner.show();

    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
        // alert(this.userid)
        this.userService.getuserfirstdata(this.userid)
          .subscribe(result => {
            // this.resultuser = result;
            //console.log("test" , this.resultuser);
            this.role_id = result[0].role_id
            // alert(this.role_id)
          })
      })

    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [6],
          orderable: false
        }
      ]

    };
    this.getFiscalyear()
    this.getCurrentYear()
  }

  openModal(template: TemplateRef<any>, id) {
    this.delid = id;
    this.modalRef = this.modalService.show(template);
  }
  getFiscalyear() {
    this.fiscalyearservice.getfiscalyeardata().subscribe(result => {
      this.resultfiscalyear = result
      // this.getCentralPolicy()
    })
  }
  getCurrentYear() {
    this.fiscalyearservice.getcurrentyeardata().subscribe(result => {
      this.currentyear = result
      this.getCurrentCentralPolicy()
    })
  }
  getCentralPolicy() {
    this.resultcentralpolicy = []
    this.centralpolicyservice.getcentralpolicydata()
      .subscribe(result => {
        this.resultcentralpolicy = result

        if (this.role_id == 3) {
          this.resultcentralpolicy = []
          result.forEach(element => {
            if (element.status == "ใช้งานจริง") {
              this.resultcentralpolicy.push(element);
            }
          });
          console.log("data", this.resultcentralpolicy);
        }

        this.loading = true;
        this.spinner.hide();
      })
  }
  getCurrentCentralPolicy() {
    this.resultcentralpolicy = []
    this.centralpolicyservice.getcentralpolicyfiscalyeardata(this.currentyear.id)
      .subscribe(result => {
        this.resultcentralpolicy = result
        if (this.role_id == 3) {
          this.resultcentralpolicy = []
          result.forEach(element => {
            if (element.status == "ใช้งานจริง") {
              this.resultcentralpolicy.push(element);
            }
          });
          console.log("data", this.resultcentralpolicy);
        }

        this.loading = true;
        this.spinner.hide();
      })
  }
  getSelectfiscalyear(){
    this.resultcentralpolicy = []
    this.centralpolicyservice.getcentralpolicyfiscalyeardata(this.selectfiscalyearid)
    .subscribe(result => {
      this.resultcentralpolicy = result
      if (this.role_id == 3) {
        this.resultcentralpolicy = []
        result.forEach(element => {
          if (element.status == "ใช้งานจริง") {
            this.resultcentralpolicy.push(element);
          }
        });
        console.log("data", this.resultcentralpolicy);
      }

      this.loading = true;
      this.spinner.hide();
    })
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
  Subject(id) {
    this.router.navigate(['/subject', id])
  }
  CreateCentralPolicy() {
    this.router.navigate(['/centralpolicy/createcentralpolicy'])
  }
  DetailCentralPolicy(id: any) {
    this.router.navigate(['/centralpolicy/detailcentralpolicy', id])
  }
  EditCentralPolicy(id: any) {
    this.router.navigate(['/centralpolicy/editcentralpolicy', id])
  }
  selectfiscalyear(value) {
    if (value == "currentfiscalyear") {
      this.getCurrentYear()
    }
    else if (value == "allfiscalyear") {
      this.getCentralPolicy()
      this.spinner.show();
    }
    else {
      this.selectfiscalyearid = value
      this.getSelectfiscalyear()
      this.spinner.show();
      // this.router.navigate(['/centralpolicyfiscalyear/' + id])
    }
  }
}
