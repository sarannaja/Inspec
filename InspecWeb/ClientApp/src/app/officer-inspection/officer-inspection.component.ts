import { Component, OnInit,Inject,TemplateRef } from '@angular/core';
import { UserService } from '../services/user.service'; // ผู้ใช้
import { Subscription } from 'rxjs/internal/Subscription';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { NgxSpinnerService } from "ngx-spinner";
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { IOption } from 'ng-select';

@Component({
  selector: 'app-officer-inspection',
  templateUrl: './officer-inspection.component.html',
  styleUrls: ['./officer-inspection.component.css']
})
export class OfficerInspectionComponent implements OnInit {

  modalRef: BsModalRef;
  loading = false;
  dtOptions: DataTables.Settings = {};
  roleId: any;
  rolename: any;
  resultuser: any[] = [];
  resultfirstuser:any[] = [];
  subscription: Subscription;
  addForm: FormGroup;
  id: any;
  //name input
  Prefix: any;
  Name: any;
  Position: any;
  Role_id: any;
  PhoneNumber: any;
  Email: any;
  ProvinceId: any;
  DistrictId:any;
  SubdistrictId: any;
  MinistryId: any;
  UserRegion:any;
  files: string[] = [];
  imgprofileUrl: any;
  //END name input
  

  constructor(
    private modalService: BsModalService,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private userService: UserService,
    private spinner: NgxSpinnerService,
    @Inject('BASE_URL') baseUrl: string
  ) {
  
  }
 
  ngOnInit() {
    this.getUser()
  }

  getUser() {
    this.userService.getuserdata(3)
      .subscribe(result => {
        //alert(this.roleId);
        this.resultuser = result;
        this.loading = true
        this.spinner.hide();
        //console.log(this.resultuser);
      })
  }

}
