import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormControl, Validators, FormGroup, FormBuilder } from '@angular/forms';
import { IOption } from 'ng-select';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-accept-central-policy',
  templateUrl: './accept-central-policy.component.html',
  styleUrls: ['./accept-central-policy.component.css']
})
export class AcceptCentralPolicyComponent implements OnInit {

  id
  resultdetailcentralpolicy: any = []

  constructor(private fb: FormBuilder,
    private modalService: BsModalService,
    private centralpolicyservice: CentralpolicyService,
    private userservice: UserService,
    private activatedRoute: ActivatedRoute, ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
  }

  ngOnInit() {
    this.getDetailCentralPolicy()
  }
  getDetailCentralPolicy(){
    this.centralpolicyservice.getdetailcentralpolicydata(this.id)
    .subscribe(result => {
      this.resultdetailcentralpolicy = result
    })
  }
}
