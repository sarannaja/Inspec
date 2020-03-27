import { Component, OnInit, TemplateRef } from '@angular/core';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { FormControl, Validators, FormGroup, FormBuilder } from '@angular/forms';
import { IOption } from 'ng-select';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-detail-central-policy',
  templateUrl: './detail-central-policy.component.html',
  styleUrls: ['./detail-central-policy.component.css']
})
export class DetailCentralPolicyComponent implements OnInit {

  resultpeople: any = []
  resultministrypeople: any = []
  resultdetailcentralpolicy: any = []
  resultcentralpolicyuser: any = []
  UserPeopleId: any;
  // UserMinistryId: any;
  id
  Form: FormGroup;
  selectpeople: Array<IOption>
  selectministrypeople: Array<IOption>
  modalRef: BsModalRef;

  constructor(private fb: FormBuilder,
    private modalService: BsModalService,
    private centralpolicyservice: CentralpolicyService,
    private userservice: UserService,
    private activatedRoute: ActivatedRoute, ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
  }

  ngOnInit() {
    this.Form = this.fb.group({
      UserPeopleId: new FormControl(null, [Validators.required]),
      // UserMinistryId: new FormControl(null, [Validators.required]),
    })

    this.userservice.getuserdata(7).subscribe(result => {
      // alert(JSON.stringify(result))
      this.resultpeople = result
      console.log(this.resultpeople);
      this.selectpeople = this.resultpeople.map((item, index) => {
        return { value: item.id, label: item.name }
      })
    })
    this.userservice.getuserdata(6).subscribe(result => {
      // alert(JSON.stringify(result))
      this.resultministrypeople = result
      console.log(this.resultministrypeople);
      this.selectministrypeople = this.resultministrypeople.map((item, index) => {
        return { value: item.id, label: item.name }
      })
    })

    this.getDetailCentralPolicy()
    this.getCentralPolicyUser()
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  getDetailCentralPolicy() {
    this.centralpolicyservice.getdetailcentralpolicydata(this.id)
      .subscribe(result => {
        this.resultdetailcentralpolicy = result
      })
  }

  getCentralPolicyUser() {
    this.centralpolicyservice.getcentralpolicyuserdata(this.id)
      .subscribe(result => {
        this.resultcentralpolicyuser = result
        console.log("result" + result);
      })

  }


  storeFiles(value) {

  }

  storePeople(value) {
    // alert(JSON.stringify(value))
    this.centralpolicyservice.addCentralpolicyUser(value, this.id).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.getCentralPolicyUser();
    })
  }

  storeMinistryPeople(value) {
    alert(JSON.stringify(value))
  }
}
