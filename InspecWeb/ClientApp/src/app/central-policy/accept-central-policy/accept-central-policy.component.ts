import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, Validators, FormGroup, FormBuilder } from '@angular/forms';
import { IOption } from 'ng-select';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { UserService } from 'src/app/services/user.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';

@Component({
  selector: 'app-accept-central-policy',
  templateUrl: './accept-central-policy.component.html',
  styleUrls: ['./accept-central-policy.component.css']
})
export class AcceptCentralPolicyComponent implements OnInit {

  id
  resultdetailcentralpolicy: any = []
  resultcentralpolicyuser: any = []
  Form: FormGroup;
  userid: string
  answer
  constructor(private fb: FormBuilder,
    private modalService: BsModalService,
    private centralpolicyservice: CentralpolicyService,
    private userservice: UserService,
    private router: Router,
    private authorize: AuthorizeService,
    private activatedRoute: ActivatedRoute, ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
  }

  ngOnInit() {
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
        // alert(this.userid)
      })

    this.Form = this.fb.group({
      answer: new FormControl(null, [Validators.required]),
    })

    this.getDetailCentralPolicy()
    this.getCentralPolicyUser()
  }
  getDetailCentralPolicy() {
    this.centralpolicyservice.getdetailacceptcentralpolicydata(this.id)
      .subscribe(result => {
        this.resultdetailcentralpolicy = result
      })
  }
  getCentralPolicyUser() {
    this.centralpolicyservice.getdetailuseracceptcentralpolicydata(this.id)
      .subscribe(result => {
        this.resultcentralpolicyuser = result
        console.log("result" + result);
      })
  }

  storeAccept(value, answer) {
    this.centralpolicyservice.acceptCentralpolicy(value, answer, this.id)
      .subscribe(response => {
        console.log(response);
        this.Form.reset()
        this.router.navigate(['usercentralpolicy'])
      })
  }
}