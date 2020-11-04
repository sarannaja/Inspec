import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { InspectionorderService } from '../services/inspectionorder.service';
import { NotofyService } from '../services/notofy.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { LogService } from '../services/log.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-inspectionorder',
  templateUrl: './inspectionorder.component.html',
  styleUrls: ['./inspectionorder.component.css']
})
export class InspectionorderComponent implements OnInit {
  resultInspectionorder: any = []
  delid: any
  year: any
  order: any
  name: any
  createBy: any
  modalRef:BsModalRef;
  Form : FormGroup
  files: string[] = []
  loading = false;
  dtOptions: DataTables.Settings = {};
  submitted = false;
  filename :any;
  userid : any;
  role_id:any;
  title:any;

  constructor(private modalService: BsModalService, 
    private fb: FormBuilder,
    private inspectionorderservice: InspectionorderService,
    public share: InspectionorderService,
    private _NotofyService: NotofyService,
    private spinner: NgxSpinnerService,
    private logService: LogService,
    private authorize: AuthorizeService,
    private userService: UserService,
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
      year: new FormControl(null, [Validators.required]),
      order: new FormControl(null, [Validators.required]),
      createBy: new FormControl(null, [Validators.required]),
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
          });
          
      });
    this.inspectionorderservice.getinspectionorder().subscribe(result=>{
      this.resultInspectionorder = result
      this.loading = true
      })
  }
  openModal(template: TemplateRef<any>, id, year,order,name,createBy,filename) {
    this.Form.reset()
    this.submitted = false;
    this.delid = id;
    this.title = name;
    this.filename = filename;
    this.Form.patchValue({
    name : name,
    year : year,
    order : order,
    createBy : createBy
    });
    this.modalRef = this.modalService.show(template);
  }
  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.Form.patchValue({
      files: file
    });
    this.Form.get('files').updateValueAndValidity()

  }

  storeInspectionorder(value) {
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }

    this.inspectionorderservice.addInspectionorder(value, this.Form.value.files)
    .subscribe(response => {
      this.logService.addLog(this.userid,'InspectionOrder','เพิ่ม',response.title,response.id).subscribe();
      this.Form.reset()
      this.loading = false;
      this.getdata();
      this.modalRef.hide()
      this._NotofyService.onSuccess("เพิ่มข้อมูล")
    })
  }
  deleteInspectionorder(value) {
    this.inspectionorderservice.deleteInspectionorder(value).subscribe(response => {
      this.logService.addLog(this.userid,'InspectionOrder','ลบ',this.title,this.delid).subscribe();
      this.loading = false;
      this.getdata();
      this.modalRef.hide()
      this._NotofyService.onSuccess("ลบข้อมูล")
    })
  }
  editInspectionorder(value,delid) {  
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }
    this.inspectionorderservice.editInspectionorder(value,this.Form.value.files,delid).subscribe(response => {
      this.logService.addLog(this.userid,'InspectionOrder','แก้ไข',response.title,response.id).subscribe();
      this.Form.reset()
      this.loading = false;
      this.getdata();
      this.modalRef.hide()
      this._NotofyService.onSuccess("แก้ไขข้อมูล")
    })
  }
  get f() { return this.Form.controls; }
}

