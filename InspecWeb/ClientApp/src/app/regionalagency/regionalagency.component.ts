import { Component, OnInit,Inject,TemplateRef } from '@angular/core';
import { UserService } from '../services/user.service'; // ผู้ใช้
import { Subscription } from 'rxjs/internal/Subscription';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { NgxSpinnerService } from "ngx-spinner";
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { MinistryService } from '../services/ministry.service';
import { RegionService } from '../services/region.service';
import { ProvinceService } from '../services/province.service';


@Component({
  selector: 'app-regionalagency',
  templateUrl: './regionalagency.component.html',
  styleUrls: ['./regionalagency.component.css']
})
export class RegionalagencyComponent implements OnInit {

  modalRef: BsModalRef;
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
  selectministry:any=[];
  ministry :any;
  selectdataregion:any=[];
  selectdataprovince:any=[];
  region:any;
  province:any;
  //END name input
  

  constructor(
    private modalService: BsModalService,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private userService: UserService,
    private spinner: NgxSpinnerService,
    private ministryService: MinistryService,
    private regionService: RegionService,
    private provinceService: ProvinceService,
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

  getdata(id,provinceid) {
    this.spinner.show();
    this.userService.getuserregionalagencydata(id,provinceid)
      .subscribe(result => {
        //alert(this.roleId);
        this.resultuser = result;
        this.loading = true
        this.spinner.hide();
        // console.log(this.resultuser);
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
    this.loading = false;
    this.getdata(event.target.value,this.province);
  }

  Changeprovince(event){
    this.province = event.target.value;
    this.loading = false;
    this.getdata( this.region,event.target.value);
  }
  excel(){
    window.location.href = '/api/user/excelregionalagency';
  }
}
