import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { FiscalyearService } from 'src/app/services/fiscalyear.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from 'src/app/services/user.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { Subscription } from 'rxjs/internal/Subscription';
import { FormControl, Validators, FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-central-policy-fiscalyear',
  templateUrl: './central-policy-fiscalyear.component.html',
  styleUrls: ['./central-policy-fiscalyear.component.css']
})
export class CentralPolicyFiscalyearComponent implements OnInit {

  resultcentralpolicy: any = []
  resultcentralpolicyrold3: any = []
  resultfiscalyear: any = []
  id: any
  delid: any
  modalRef: BsModalRef;
  dtOptions: DataTables.Settings = {};
  loading = false;
  userid
  role_id
  subscription: Subscription;
  Form: FormGroup;
  constructor(
    private router: Router,
    private centralpolicyservice: CentralpolicyService,
    private fiscalyearservice: FiscalyearService,
    private modalService: BsModalService,
    private authorize: AuthorizeService,
    private userService: UserService,
    private spinner: NgxSpinnerService,
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
  ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.subscription = this.userService.getUserNav()
      .subscribe(
        result => {
          if (result.roleId != this.id) {
            // this.loading = false;
            console.log('result.roleId', result.roleId);
            this.id = result.roleId
            setTimeout(() => { this.getCentralPolicy() }, 200)
          }
        });
  }

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
          targets: [5],
          orderable: false
        }
      ]

    };
    this.Form = this.fb.group({
      province: new FormControl(null, [Validators.required]),
    })

    this.Form.patchValue({
      province: this.id
    })
    this.getFiscalyear()
  }
  openModal(template: TemplateRef<any>, id) {
    this.delid = id;
    this.modalRef = this.modalService.show(template);
  }
  getFiscalyear() {
    this.fiscalyearservice.getfiscalyeardata().subscribe(result => {
      this.resultfiscalyear = result
      this.getCentralPolicy()
    })
  }
  getCentralPolicy() {
    this.centralpolicyservice.getcentralpolicyfiscalyeardata(this.id)
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
    if (value == "allfiscalyear") {
      this.router.navigate(['/centralpolicy'])
    } else {
      var id = value
      this.userService.sendNav(value);
      this.router.navigate(['/centralpolicyfiscalyear/' + id])
    }
  }
}
