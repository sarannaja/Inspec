import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { MinistryService } from '../services/ministry.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NotofyService } from '../services/notofy.service';

@Component({
  selector: 'app-ministry',
  templateUrl: './ministry.component.html',
  styleUrls: ['./ministry.component.css']
})
export class MinistryComponent implements OnInit {
  resultministry: any[] = []
  delid:any
  name:any
  modalRef:BsModalRef;
  Form : FormGroup
  EditForm: FormGroup;
  loading = false;
  dtOptions: DataTables.Settings = {};

  constructor(private modalService: BsModalService, private fb: FormBuilder, private ministryservice: MinistryService,
    private router:Router,
    public share: MinistryService,
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
    this.ministryservice.getministry().subscribe(result=>{
      this.resultministry = result
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
  storeMinistry(value) {
    this.ministryservice.addMinistry(value).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.getdata()
      this._NotofyService.onSuccess("เพื่มข้อมูล")
    })
  }
  deleteMinistry(value) {
    this.ministryservice.deleteMinistry(value).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.getdata()
      this._NotofyService.onSuccess("ลบข้อมูล")
    })
  }

  editMinistry(value,delid) {
   
    this.ministryservice.editMinistry(value,delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.getdata()
      this._NotofyService.onSuccess("แก้ไขข้อมูล")
    })
  }

  viewDepartment(id) {
    this.router.navigate(['/ministry/'+id+'/department'])
  }

}
