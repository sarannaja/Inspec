import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotofyService } from '../services/notofy.service';
import { SectorService } from '../services/sector.service';


@Component({
  selector: 'app-sector',
  templateUrl: './sector.component.html',
  styleUrls: ['./sector.component.css']
})
export class SectorComponent implements OnInit {
    data: any[] = []
    delid:any
    name:any;
    modalRef:BsModalRef;
    Form : FormGroup
    loading = false;
    dtOptions: DataTables.Settings = {};

  constructor(
    private modalService: BsModalService, 
    private fb: FormBuilder, 
    private sectorservice: SectorService,
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
    })

  }

  getdata(){
    this.sectorservice.getdata().subscribe(result=>{
      this.data = result
      this.loading = true;

    })
  }
  openModal(template: TemplateRef<any>,id,name) {

    this.Form.reset()
    this.delid = id;
    this.Form.patchValue({
      Name: name,
    })
    this.modalRef = this.modalService.show(template);
  }

  store(value) {
    this.sectorservice.store(value).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.getdata()
      this._NotofyService.onSuccess("เพื่มข้อมูล")
    })
  }
  delete(delid) {
    this.sectorservice.delete(delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.getdata()
      this._NotofyService.onSuccess("ลบข้อมูล")
    })
  }

  edit(value,delid) {
    this.sectorservice.update(value,delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.getdata()
      this._NotofyService.onSuccess("แก้ไขข้อมูล")
    })
  }

}