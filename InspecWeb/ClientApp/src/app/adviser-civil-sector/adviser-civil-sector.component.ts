import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { NotificationService } from '../services/Pipe/alert.service';
import { ProvinceService } from '../services/province.service';
import { RegionService } from '../services/region.service';
import { MinistryService } from '../services/ministry.service';
import { UserService } from '../services/user.service'; // ผู้ใช้
import { NgxSpinnerService } from "ngx-spinner";
import { Subscription } from 'rxjs/internal/Subscription';
@Component({
  selector: 'app-adviser-civil-sector',
  templateUrl: './adviser-civil-sector.component.html',
  styleUrls: ['./adviser-civil-sector.component.css']
})
export class AdviserCivilSectorComponent implements OnInit {
  modalRef: BsModalRef;
  selectdatarole: any
  selectdataministry: any
  selectdatadeparment: any
  selectdataprovince: any
  selectdataregion: any
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
    private router: Router,
    private provinceService: ProvinceService,
    private regionService: RegionService,
    private ministryService: MinistryService,
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
    this.userService.getuserdata("7")
      .subscribe(result => {
        //alert(this.roleId);
        this.resultuser = result;
        this.loading = true
        this.spinner.hide();
        // console.log(this.resultuser);
      })
  }
}

