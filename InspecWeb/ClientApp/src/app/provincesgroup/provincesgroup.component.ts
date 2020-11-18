import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotofyService } from '../services/notofy.service';
import { ProvincesGroupService } from '../services/provincesGroup.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from '../services/user.service';
import { LogService } from '../services/log.service';



@Component({
  selector: 'app-provincesgroup',
  templateUrl: './provincesgroup.component.html',
  styleUrls: ['./provincesgroup.component.css']
})
export class ProvincesgroupComponent implements OnInit {
    data: any[] = []
    delid:any
    name:any;
    modalRef:BsModalRef;
    Form : FormGroup
    loading = false;
    userid :any;
    role_id :any;
    submitted = false;
    dtOptions: any = {};

  constructor(
    private modalService: BsModalService, 
    private fb: FormBuilder, 
    private provincesgroupservice: ProvincesGroupService,
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

    this.getdata();

    this.Form = this.fb.group({
      Name: new FormControl(null, [Validators.required]),
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
    this.provincesgroupservice.getdata().subscribe(result=>{
      this.data = result
      this.loading = true;

    })
  }
  openModal(template: TemplateRef<any>=null,id=null,name=null) {

    this.Form.reset()
    this.submitted = false; 
    this.delid = id;
    this.name = name;
    this.Form.patchValue({
      Name: name,
    })
    this.modalRef = this.modalService.show(template);
  }

  store(value) {
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }
    this.provincesgroupservice.store(value).subscribe(response => {
      this.logService.addLog(this.userid,'ProvincesGroups','เพิ่ม',response.name,response.id).subscribe();
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.getdata()
      this._NotofyService.onSuccess("เพื่มข้อมูล")
    })
  }
  delete(delid) {
    this.provincesgroupservice.delete(delid).subscribe(response => {
      this.logService.addLog(this.userid,'ProvincesGroups','ลบ',this.name,delid).subscribe();
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.getdata()
      this._NotofyService.onSuccess("ลบข้อมูล")
    })
  }

  edit(value,delid) {
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }
    this.provincesgroupservice.update(value,delid).subscribe(response => {
      this.logService.addLog(this.userid,'ProvincesGroups','แก้ไข',response.name,response.id).subscribe();
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.getdata()
      this._NotofyService.onSuccess("แก้ไขข้อมูล")
    })
  }
  get f() { return this.Form.controls; }
}
