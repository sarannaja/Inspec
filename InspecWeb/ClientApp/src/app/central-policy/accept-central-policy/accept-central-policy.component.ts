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
  resultuser: any = []
  id
  resultdetailcentralpolicy: any = []
  resultcentralpolicyuser: any = []
  resultdetailcentralpolicyprovince: any = []
  Form: FormGroup;
  assignForm: FormGroup;
  userid: string
  answer
  resultelectronicbookdetail: any = []
  downloadUrl: any;
  modalRef: BsModalRef;
  assignDetail: any;

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
    this.assignForm = this.fb.group({
      assign: new FormControl(null, [Validators.required]),
    })

    this.getDetailCentralPolicy()
    this.getCentralPolicyUser()
    this.getSubjectCentralPolicyProvince()
    this.getAssign();
  }
  getDetailCentralPolicy() {
    this.centralpolicyservice.getdetailacceptcentralpolicydata(this.id)
      .subscribe(result => {
        console.log("elec",result);

        this.resultdetailcentralpolicy = result.centralpolicydata
        this.resultuser = result.userdata
        this.resultelectronicbookdetail = result.centralpolicydata.centralPolicyUser[0].electronicBook.detail
        // alert(JSON.stringify(this.resultelectronicbookdetail))
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
        // this.router.navigate(['usercentralpolicy'])
        this.router.navigate(['calendaruser'])
      })
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  sendAssign(value) {
    this.centralpolicyservice.sendAssign(value, this.id).subscribe(response => {
      console.log(response);
      this.modalRef.hide();
    })
  }

  getAssign() {
    this.centralpolicyservice.getAssign(this.id).subscribe(res => {
      this.assignDetail = res.forward;
      console.log("ASSIGN: ", this.assignDetail);
      this.assignForm.patchValue({
        assign: this.assignDetail
      })
    })
  }
}
