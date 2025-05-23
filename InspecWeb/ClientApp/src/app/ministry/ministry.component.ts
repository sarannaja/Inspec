import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { MinistryService } from '../services/ministry.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NotofyService } from '../services/notofy.service';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { UserService } from '../services/user.service';
import { LogService } from '../services/log.service';

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
  submitted = false;
  userid :any;
  role_id :any;
  dtOptions: any = {};

  constructor(private modalService: BsModalService,
    private fb: FormBuilder, 
    private ministryservice: MinistryService,
    private router:Router,
    public share: MinistryService,
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
    this.ministryservice.getministry().subscribe(result=>{
      this.resultministry = result
      this.loading = true;
    })
  }
  openModal(template: TemplateRef<any>=null, id=null, Name=null,NameEN=null,ShortnameEN=null,ShortnameTH=null) {
    this.Form.reset()
    this.submitted = false;
    this.delid = id;
    this.name = Name;
    this.Form.patchValue({
      Name: Name,
      NameEN : NameEN,
      ShortnameEN : ShortnameEN,
      ShortnameTH : ShortnameTH

    })
    this.modalRef = this.modalService.show(template);
  }
  storeMinistry(value) {
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }
    this.ministryservice.addMinistry(value).subscribe(response => {
      this.logService.addLog(this.userid,'Ministries','เพิ่ม',response.name,response.id).subscribe();
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.getdata()
      this._NotofyService.onSuccess("เพื่มข้อมูล")
    })
  }
  deleteMinistry(value) {
    this.ministryservice.deleteMinistry(value).subscribe(response => {
      this.logService.addLog(this.userid,'Ministries','ลบ',this.name,this.delid).subscribe();
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.getdata()
      this._NotofyService.onSuccess("ลบข้อมูล")
    })
  }

  editMinistry(value,delid) {
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }
    this.ministryservice.editMinistry(value,delid).subscribe(response => {
      this.logService.addLog(this.userid,'Ministries','แก้ไข',response.name,response.id).subscribe();
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
  get f() { return this.Form.controls; }
}
