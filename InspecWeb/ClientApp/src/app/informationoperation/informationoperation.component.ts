import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { InformationoperationService } from '../services/informationoperation.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from '../services/user.service';
import { LogService } from '../services/log.service';
import { NotofyService } from '../services/notofy.service';

@Component({
  selector: 'app-informationoperation',
  templateUrl: './informationoperation.component.html',
  styleUrls: ['./informationoperation.component.css']
})
export class InformationoperationComponent implements OnInit {
  resultinformationoperation: any = []
  delid:any
  location:any
  name: any
  detail: any
  tel: any
  province: any
  district: any
  modalRef:BsModalRef;
  Form : FormGroup
  files: string[] = []
  loading = false;
  submitted = false;
  dtOptions: DataTables.Settings = {};
  userid :any;
  role_id :any;
  file:any;

  constructor(private modalService: BsModalService, 
    private fb: FormBuilder, 
    private informationoperationservice: InformationoperationService,
    public share: InformationoperationService,
    private authorize: AuthorizeService,
    private userService: UserService,
    private logService: LogService,
    private _NotofyService: NotofyService,
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
      },
    };
    this.getdata();
    this.Form = this.fb.group({
      location: new FormControl(null, [Validators.required]),
      name: new FormControl(null, [Validators.required]),
      detail: new FormControl(null, [Validators.required]),
      tel: new FormControl(null, [Validators.required]),
      province: new FormControl(null, [Validators.required]),
      district: new FormControl(null, [Validators.required]),
      files : new FormControl(null)
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
    this.informationoperationservice.getinformationoperation().subscribe(result=>{
      this.resultinformationoperation = result
      this.loading = true
      })
  }
  openModal(template: TemplateRef<any>=null, id=null, location=null, name=null, detail=null, tel=null, province=null, district=null,file=null) {
    this.delid = id;
    this.location = location;
    this.submitted =false;
    this.name = name;
    this.detail = detail;
    this.tel = tel;
    this.province = province;
    this.district = district;
    this.file = file;
    this.Form.patchValue({
      location : location,
      name : name,
      detail : detail,
      tel : tel,
      province : province,
      district : district,
    }) 

    this.modalRef = this.modalService.show(template);
  }
  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.Form.patchValue({
      files: file
    });
    this.Form.get('files').updateValueAndValidity()
  }

  storeInformationoperation(value) {
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }
    this.informationoperationservice.addInformationoperation(value, this.Form.value.files).subscribe(response => {
      this.logService.addLog(this.userid,'Informationoperations','เพิ่ม',response.name,response.id).subscribe();
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this._NotofyService.onSuccess("เพิ่มข้อมูล")
      this.getdata();
    })
  }
  deleteInformationoperation(value) {
    this.informationoperationservice.deleteInformationoperation(value).subscribe(response => {
      this.logService.addLog(this.userid,'Informationoperations','ลบ',this.name,this.delid).subscribe();
      this.modalRef.hide()
      this.loading = false
      this._NotofyService.onSuccess("ลบข้อมูล")
      this.getdata();
    })
  }
  editInformationoperation(value,delid) {
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }
    this.informationoperationservice.editInformationoperation(value,this.Form.value.files,delid).subscribe(response => {
      this.logService.addLog(this.userid,'Informationoperations','แก้ไข',response.title,response.id).subscribe();
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this._NotofyService.onSuccess("แก้ไขข้อมูล")
      this.getdata();
    })
  }
  get f() { return this.Form.controls; }
}
