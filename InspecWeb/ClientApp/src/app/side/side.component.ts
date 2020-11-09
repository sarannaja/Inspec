import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { MinistryService } from '../services/ministry.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NotofyService } from '../services/notofy.service';
import { SideService } from '../services/side.service';

@Component({
  selector: 'app-side',
  templateUrl: './side.component.html',
  styleUrls: ['./side.component.css']
})
export class SideComponent implements OnInit {
  data: any[] = []
  delid:any
  name:any
  modalRef:BsModalRef;
  Form : FormGroup
  EditForm: FormGroup;
  loading = false;
  dtOptions: DataTables.Settings = {};

  constructor(private modalService: BsModalService, private fb: FormBuilder, 
    private sideservice: SideService,
    private router:Router,
    private _NotofyService: NotofyService,) { }
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
      NameEN: new FormControl(null, [Validators.required]),
      ShortnameEN: new FormControl(null, [Validators.required]),
      ShortnameTH: new FormControl(null, [Validators.required]),
    })
  }

  getdata(){
    this.sideservice.getdata().subscribe(result=>{
      this.data = result
      this.loading = true;
    })
  }
  openModal(template: TemplateRef<any>=null, id=null, Name=null,NameEN=null,ShortnameEN=null,ShortnameTH=null) {
    this.Form.reset()
    this.delid = id;
    this.Form.patchValue({
      Name: Name,
      NameEN : NameEN,
      ShortnameEN : ShortnameEN,
      ShortnameTH : ShortnameTH

    })
    this.modalRef = this.modalService.show(template);
  }
  store(value) {
    this.sideservice.store(value).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.getdata()
      this._NotofyService.onSuccess("เพื่มข้อมูล")
    })
  }
  delete(value) {
    this.sideservice.delete(value).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.getdata()
      this._NotofyService.onSuccess("ลบข้อมูล")
    })
  }

  edit(value,delid) {
   
    this.sideservice.update(value,delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.getdata()
      this._NotofyService.onSuccess("แก้ไขข้อมูล")
    })
  }

}
