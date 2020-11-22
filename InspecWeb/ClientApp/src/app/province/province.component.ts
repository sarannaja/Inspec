import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ProvinceService } from '../services/province.service';
import { Router } from '@angular/router';
import { NotificationService } from '../services/Pipe/alert.service';
import { SnotifyService, SnotifyToastConfig, SnotifyPosition } from 'ng-snotify';
import { NgxSpinnerService } from "ngx-spinner";
import { NotofyService } from '../services/notofy.service';
import { LogService } from '../services/log.service';
import { UserService } from '../services/user.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';


@Component({
  selector: 'app-province',
  templateUrl: './province.component.html',
  styleUrls: ['./province.component.css']

})
export class ProvinceComponent implements OnInit {

  resultprovince: any[] = []
  delid: any
  name: any
  link: any
  modalRef: BsModalRef;
  Form: FormGroup;
  EditForm: FormGroup;
  loading = false;
  submitted = false;
  dtOptions: any = {};
  selectdatasector: Array<any>=[];
  selectdataprovincesgroup: Array<any>=[];
  userid :any;
  role_id :any;
  constructor(
    private modalService: BsModalService,
    private fb: FormBuilder,
    private provinceservice: ProvinceService,
    public share: ProvinceService,
    private router: Router,
    private snotifyService: NotificationService,
    private snotifyService2: SnotifyService,
    private spinner: NgxSpinnerService,
    private _NotofyService: NotofyService,
    private authorize: AuthorizeService,
    private userService: UserService,
    private logService: LogService,
  ) { }

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
    this.spinner.show();  
    this.getDataProvincesGroup();
    this.getDataSector();
    this.getdata();
    this.Form = this.fb.group({
      provincename: new FormControl(null, [Validators.required]),
      provincelink: new FormControl(null, [Validators.required]),
      Sector: new FormControl(null, [Validators.required]),
      Provincegroup: new FormControl(null, [Validators.required]),
      NameEN: new FormControl(null, [Validators.required]),
      ShortnameEN: new FormControl(null, [Validators.required]),
      ShortnameTH: new FormControl(null, [Validators.required]),
    })

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
    this.provinceservice.getprovincedata().subscribe(result => {
      //console.log('data',result);
      this.spinner.hide();
      this.resultprovince = result
      this.loading = true;
    })
  }

  getDataSector() {
    this.provinceservice.getsectordata()
      .subscribe(result => {
        this.selectdatasector = result.map((item, index) => {
          return { value: item.id, label: item.name }
        })
      })
  }

  getDataProvincesGroup() {
    this.provinceservice.getprovincegroupdata()
      .subscribe(result => {
        this.selectdataprovincesgroup = result.map((item, index) => {
          return { value: item.id, label: item.name }
        })
      })
  }

  District(id,name ){
    this.router.navigate(['/district', id,name])
  }

  openModal(template: TemplateRef<any>, id, name, link) {
    this.Form.reset()
    this.submitted = false; 
    this.delid = id;
    this.name = name;
    this.link = link
    this.modalRef = this.modalService.show(template);
  }
 

  storeProvince(value) {
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }
    this.provinceservice.addProvince(value).subscribe(response => {
      this.spinner.show();  
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.logService.addLog(this.userid,'Provinces','เพิ่ม',response.name,response.id).subscribe();
      this.getdata();
      this._NotofyService.onSuccess("เพื่มข้อมูล")
    })
  }

  deleteProvince(value) {
    this.provinceservice.deleteProvince(value).subscribe(response => {
      //console.log(value);
      this.spinner.show();  
      this.modalRef.hide()
      this.loading = false;
      this.logService.addLog(this.userid,'Provinces','ลบ',this.name,this.delid).subscribe();
      this.getdata();
      this._NotofyService.onSuccess("ลบข้อมูล")
    })
  }

  editModal(template: TemplateRef<any>, id, name, link,Sector,Provincegroup,NameEN,ShortnameEN,ShortnameTH) {
    this.Form.reset()
    this.submitted = false; 
    this.delid = id;
   // console.log('Sector : ' + JSON.stringify(Sector));
    // this.EditForm = this.fb.group({
    //   provincename: new FormControl(null, [Validators.required]),
    //   provincelink: new FormControl(null, [Validators.required]),
    //   Sector: new FormControl(null, [Validators.required]),
    //   Provincegroup: new FormControl(null, [Validators.required]),
    //   NameEN: new FormControl(null, [Validators.required]),
    //   ShortnameEN: new FormControl(null, [Validators.required]),
    //   ShortnameTH: new FormControl(null, [Validators.required]),
    // })

    this.Form.patchValue({
      provincename: name,
      provincelink: link,
      Provincegroup:Provincegroup,
      Sector:Sector,
      NameEN: NameEN,
      ShortnameEN : ShortnameEN,
      ShortnameTH : ShortnameTH,
    })

    this.modalRef = this.modalService.show(template);
  }
  editProvince(value, delid) {
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }
    this.provinceservice.editProvince(value, delid).subscribe(response => {
      this.spinner.show();  
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.logService.addLog(this.userid,'Provinces','แก้ไข',response.name,response.id).subscribe();
      this.getdata();
      this._NotofyService.onSuccess("แก้ไขข้อมูล")
    })
  }
  get f() { return this.Form.controls; }
}
