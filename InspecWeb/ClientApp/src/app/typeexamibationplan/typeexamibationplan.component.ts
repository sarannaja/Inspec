import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotofyService } from '../services/notofy.service';
import { TypeexamibationplanService } from '../services/typeexamibationplan.service';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { LogService } from '../services/log.service';
import { UserService } from '../services/user.service';


@Component({
  selector: 'app-typeexamibationplan',
  templateUrl: './typeexamibationplan.component.html',
  styleUrls: ['./typeexamibationplan.component.css']
})
export class TypeexamibationplanComponent implements OnInit {
    data: any[] = []
    delid:any
    name:any;
    modalRef:BsModalRef;
    Form : FormGroup
    loading = false;
    userid :any;
    dtOptions: any = {}; 
    role_id :any;
    submitted = false;

  constructor(
    private modalService: BsModalService, 
    private fb: FormBuilder, 
    private typeexamibationplanservice: TypeexamibationplanService,
    private _NotofyService: NotofyService,
    private authorize: AuthorizeService,
    private logService: LogService,
    private userService: UserService,
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
    this.typeexamibationplanservice.getdata().subscribe(result=>{
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

    this.typeexamibationplanservice.store(value).subscribe(response => {
      this.logService.addLog(this.userid,'Typeexaminationplans','เพิ่ม',response.name,response.id).subscribe();
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.getdata()
      this._NotofyService.onSuccess("เพื่มข้อมูล")
    })
  }
  delete(delid) {
    this.typeexamibationplanservice.delete(delid).subscribe(response => {
      this.logService.addLog(this.userid,'Typeexaminationplans','ลบ',this.name,delid).subscribe();
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
    this.typeexamibationplanservice.update(value,delid).subscribe(response => {
      this.logService.addLog(this.userid,'Typeexaminationplans','แก้ไข',response.name,response.id).subscribe();
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.getdata()
      this._NotofyService.onSuccess("แก้ไขข้อมูล")
    })
  }
  get f() { return this.Form.controls; }
}
