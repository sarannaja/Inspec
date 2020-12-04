import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { UserService } from '../services/user.service';
import { LogService } from '../services/log.service';
import { VillageService } from '../services/village.service';
import { NotofyService } from '../services/notofy.service';
import { DistrictService } from '../services/district.service';
import { SubdistrictService } from '../services/subdistrict.service';

@Component({
  selector: 'app-vilage',
  templateUrl: './vilage.component.html',
  styleUrls: ['./vilage.component.css']
})
export class VilageComponent implements OnInit {
  result: any = []
  id;
  titleprovince: [];
  titledistrict: [];
  Form: FormGroup;
  modalRef: BsModalRef;
  subdistrict_id:any;
  district_id:any;
  loading = false;
  dtOptions: any = {};
  province_id:any;
  provincename:any;
  districtname:any;
  subdistrictname:any;
  name:any;
  submitted = false;
  userid :any;
  role_id :any;
  constructor(
    private modalService: BsModalService,
    private fb: FormBuilder,
    private villageservice: VillageService,
    private activatedRoute: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private router:Router,
    private districtservice: DistrictService,
    private subdistrictservice: SubdistrictService,
    private _NotofyService: NotofyService,
    private authorize: AuthorizeService,
    private userService: UserService,
    private logService: LogService,
    ) {
    this.district_id = activatedRoute.snapshot.paramMap.get('iddistrict')
    this.id = activatedRoute.snapshot.paramMap.get('subdistrictid')
    this.provincename = activatedRoute.snapshot.paramMap.get('provincename')
    this.districtname = activatedRoute.snapshot.paramMap.get('districtname')
    this.subdistrictname = activatedRoute.snapshot.paramMap.get('subdistrictname')
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
      }
    };
    this.form();
    this.getdata();
  }
  getdata(){
    this.authorize.getUser()
    .subscribe(result => {
      this.userid = result.sub
      this.userService.getuserfirstdata(this.userid)
        .subscribe(result => {
        this.role_id = result[0].role_id
      })
    })
    this.spinner.show();

    this.subdistrictservice.getsubdistrictdata(this.district_id).subscribe(result => {
      this.province_id = result[0].district.provinceId 

      // console.log("momo",result);
      // console.log("momo2",this.subdistrict_id);
      //alert(this.subdistrict_id);
      // this.districtservice.getdistrictdata(this.subdistrict_id).subscribe(result => {
      //   this.province_id = result
      //   console.log("momo2",result);
      //  //alert(this.province_id);
      // })

    })

  
    this.villageservice.getvillagedata(this.id).subscribe(result => {
      this.result = result
      this.loading = true;
      this.spinner.hide();
    
    })
  }

  openModal(template: TemplateRef<any>=null, id=null,name=null) {
    this.Form.reset()
    this.submitted = false;
    this.name = name;
    this.subdistrict_id = id;//ID สำหรับลบ
    this.Form.patchValue({
      Name: name,
    })
    this.modalRef = this.modalService.show(template);
  }
  storeSubdistrict(value){
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }
    this.villageservice.add(value,this.id).subscribe(response => {
      this.logService.addLog(this.userid,'Villages','เพิ่ม',response.name,response.id).subscribe();
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this._NotofyService.onSuccess("เพิ่มข้อมูล")
      this.getdata()
    })
  }

  updateSubdistrict(value,id){
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }
    this.villageservice.edit(value,id).subscribe(response => {
      this.logService.addLog(this.userid,'Villages','แก้ไข',response.name,response.id).subscribe();
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this._NotofyService.onSuccess("แก้ไขข้อมูล")
      this.getdata()
    })
  }

  delete(value) {
    this.villageservice.delete(value).subscribe(response => {
      this.logService.addLog(this.userid,'Villages','ลบ',this.name,this.subdistrict_id).subscribe();
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this._NotofyService.onSuccess("ลบข้อมูล")
      this.getdata()
    })
  }

  form() {
    this.Form = this.fb.group({
      Name: new FormControl(null, [Validators.required]),
    })
  }
  get f() { return this.Form.controls; }
}
