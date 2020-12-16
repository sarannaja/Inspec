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
  dtOptions: any = {};
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
  region:any;
  province:any;
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
    this.imgprofileUrl = baseUrl + '/imgprofile';
  }
  ngOnInit() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      "language": {
        "lengthMenu": "แสดง  _MENU_  รายการ",
        "search": "ค้นหา:",
        "info": "แสดง _START_ ถึง _END_ จาก _TOTAL_ แถว",
        "infoEmpty": "แสดง 0 ของ 0 รายการ",
        "zeroRecords": "ไม่พบข้อมูล",
        "paginate": {
          "first": "หน้าแรก",
          "last": "หน้าสุดท้าย",
          "next": "ต่อไป",
          "previous": "ย้อนกลับ"
        },
      },
      dom: 'Bfrtip',
      buttons: [
        { extend: 'excel', text: 'Excel', className: 'btn btn-success glyphicon glyphicon-list-alt' },
        { extend: 'pdf', text: 'Pdf', className: 'btn btn-primary glyphicon glyphicon-file' },
        { extend: 'print', text: 'Print', className: 'btn btn-primary glyphicon glyphicon-print' }
      ]

    };
    this.getDataRegionsAndProvince()
  }
  getdata(regionid,provinceid) {
    this.spinner.show();
    this.userService.getuserpublicsectoradvisordata(regionid,provinceid)
      .subscribe(result => {
        this.resultuser = result;
        this.loading = true
        this.spinner.hide();
      })
  }

  getDataRegionsAndProvince() {
    this.regionService.getregiondataforuser().subscribe(res => {

      this.selectdataregion = res.importFiscalYearRelations.filter(
        (thing, i, arr) => arr.findIndex(t => t.regionId === thing.regionId) === i
      ).map((item, index) => {
        return {
          value: item.region.id,
          label: item.region.name
        }
      });
       this.region = 0;
    })


    this.provinceService.getprovincedata2()
     .subscribe(result => {
       this.selectdataprovince = result;
       this.province = 0;
      
     });
     this.getdata(this.region,this.province);
  }

  Changeregion(event){
    this.region = event.target.value;
    this.province = 0;
    this.loading = false;
    this.getdata(event.target.value,this.province);
  }

  Changeprovince(event){
    this.province = event.target.value;
    this.loading = false;
    this.getdata(this.region,event.target.value);
  }


  excel(){
    window.location.href = '/api/user/exceladvisercivilsector';
  }
}

