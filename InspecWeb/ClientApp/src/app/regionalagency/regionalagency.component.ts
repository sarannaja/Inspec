import { Component, OnInit,Inject,TemplateRef } from '@angular/core';
import { UserService } from '../services/user.service'; // ผู้ใช้
import { Subscription } from 'rxjs/internal/Subscription';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { NgxSpinnerService } from "ngx-spinner";
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { MinistryService } from '../services/ministry.service';


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
  //END name input
  

  constructor(
    private modalService: BsModalService,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private userService: UserService,
    private spinner: NgxSpinnerService,
    private ministryService: MinistryService,
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
    this.getDataMinistriesfirst()
  }

  getdata(id) {
    this.spinner.show();
    this.userService.getuserregionalagencydata(id)
      .subscribe(result => {
        //alert(this.roleId);
        this.resultuser = result;
        this.loading = true
        this.spinner.hide();
        // console.log(this.resultuser);
      })
  }
  getDataMinistriesfirst() {
    
    this.ministryService.getministry()
     .subscribe(result => {
       this.selectministry = result;
       this.ministry = 0;
       //console.log("momox",result[0].id)
       this.getdata(this.ministry);
       //alert(this.ministry)
     });
 }

 Changeministry(event){
   // alert(JSON.stringify(event.target));
   // console.log("momox",event.target)
   // alert(event.target.value);
   this.ministry = event.target.value;
   this.loading = false;
   this.getdata(event.target.value);
 }
  excel(){
    window.location.href = '/api/user/excelregionalagency';
  }
}
