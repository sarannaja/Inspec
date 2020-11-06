import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { RegionService } from '../services/region.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { LogService } from '../services/log.service';
import { NotofyService } from '../services/notofy.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-region',
  templateUrl: './region.component.html',
  styleUrls: ['./region.component.css']
})
export class RegionComponent implements OnInit {

  resultregion: any[] = []
  delid: any
  name: any
  modalRef: BsModalRef;
  Form: FormGroup
  EditForm: FormGroup;
  loading = false;
  submitted = false;
  dtOptions: DataTables.Settings = {};
  forbiddenUsernames = ['admin', 'test', 'xxxx'];
  userid :any;
  role_id:any;
  constructor(
    private authorize: AuthorizeService,
    private userService: UserService,
    private modalService: BsModalService, 
    private fb: FormBuilder,
    private regionservice: RegionService,
    public share: RegionService,
    private spinner: NgxSpinnerService,
    private logService: LogService,
    private _NotofyService: NotofyService,
    ) { }

  ngOnInit() {
    this.getdata();
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
    this.Form = this.fb.group({
      name: new FormControl(null, [Validators.required]),
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
    this.regionservice.getregiondata().subscribe(result => {
      this.resultregion = result
      this.loading = true;
    })
  }

  openModal(template: TemplateRef<any>=null, id=null, name=null) {
    this.Form.reset()
    this.submitted = false;
    this.delid = id;
    this.name = name;
    // alert(name)
    this.Form.patchValue({
      name: name
    })

    this.modalRef = this.modalService.show(template);
  }


  storeRegion(value) {

    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }

    this.regionservice.addRegion(value).subscribe(response => {
      this.logService.addLog(this.userid,'Regions','เพิ่ม',response.name,response.id).subscribe();
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this._NotofyService.onSuccess("เพิ่มข้อมูล");
      this.getdata();
    })
  }
  deleteRegion(value) {
    this.logService.addLog(this.userid,'Regions','ลบ',this.name,this.delid).subscribe();
    this.regionservice.deleteRegion(value).subscribe(response => {
      this.modalRef.hide()
      this.loading = false;
      this._NotofyService.onSuccess("ลบข้อมูล")
      this.getdata();
    })
  }

  editRegion(value,delid) {

    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }

    this.regionservice.editRegion(value,delid).subscribe(response => {
      this.logService.addLog(this.userid,'Regions','แก้ไข',response.name,response.id).subscribe();
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this._NotofyService.onSuccess("แก้ไขข้อมูล");
      this.getdata();
    })
  }
  get f() { return this.Form.controls; }
}
