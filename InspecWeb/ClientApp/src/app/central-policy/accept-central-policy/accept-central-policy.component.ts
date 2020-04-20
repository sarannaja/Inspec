import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
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
  resultdetailcentralpolicyprovince: any = []
  Form: FormGroup;
  userid: string
  answer
  downloadUrl: any
  constructor(private fb: FormBuilder,
    private modalService: BsModalService,
    private centralpolicyservice: CentralpolicyService,
    private userservice: UserService,
    private router: Router,
    private authorize: AuthorizeService,
    private activatedRoute: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string  ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.downloadUrl = baseUrl + '/Uploads';
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
    this.getSubjectCentralPolicyProvince()
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

  getSubjectCentralPolicyProvince() {
    this.centralpolicyservice.getSubjectCentralPolicyProvince(this.id)
      .subscribe(result => {
        this.resultdetailcentralpolicyprovince = result
        console.log("resultdetailcentralpolicyprovince : ", result);
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
