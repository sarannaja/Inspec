import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { GorvermentinspectionplanService } from '../services/governmentinspectionplan.service';
import { UserService } from '../services/user.service';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { NotofyService } from '../services/notofy.service';

@Component({
  selector: 'app-governmentinspectionplan',
  templateUrl: './governmentinspectionplan.component.html',
  styleUrls: ['./governmentinspectionplan.component.css']
})
export class GovernmentinspectionplanComponent implements OnInit {
  resultgovernmentinspectionplan: any = []
  delid:any
  year:any
  title:any
  modalRef:BsModalRef;
  Form : FormGroup
  files: string[] = []
  loading = false;
  role_id:any;
  filesname: any;
  dtOptions: any = {};
  submitted = false;

  constructor(private modalService: BsModalService, 
    private fb: FormBuilder, 
    private governmentinspectionplanservice: GorvermentinspectionplanService,
    public share: GorvermentinspectionplanService,
    private _NotofyService: NotofyService,
    private userService: UserService,
    private authorize: AuthorizeService,
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
      },
      columnDefs: [
        {
          targets: [5],
          orderable: false
        }
      ],
      

    };
     //<!-- เช็ค role -->
    this.authorize.getUser()
    .subscribe(result => {
      this.userService.getuserfirstdata(result.sub)
          .subscribe(result => {
            this.role_id = result[0].role_id
          })
    })
    //<!-- END เช็ค role -->
    this.Form = this.fb.group({
      year: new FormControl(null, [Validators.required]),
      title: new FormControl(null, [Validators.required]),
      files : new FormControl(null, [Validators.required])
    })
  }

  getdata(){
    this.governmentinspectionplanservice.getgovernmentinspectionplan().subscribe(result=>{
      this.resultgovernmentinspectionplan = result
      this.loading = true
      })
  }
  openModal(template: TemplateRef<any>, id=null, year=null,title=null,filesname=null) {
    this.Form.reset()
    this.submitted = false;
    this.delid = id;
    this.filesname = filesname;

    this.Form.patchValue({
      year: year,
      title : title,    
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

  storeGovernmentinspectionplan(value) {
    // alert(JSON.stringify(value));
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }

    this.loading = false
    this.governmentinspectionplanservice.addGovernmentinspectionplan(value, this.Form.value.files).subscribe(response => {
     // console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.getdata()
      this._NotofyService.onSuccess("เพื่มข้อมูล")
    })
  }
  deleteGovernmentinspectionplan(value) {
    this.loading = false
    this.governmentinspectionplanservice.deleteGovernmentinspectionplan(value).subscribe(response => {
     // console.log(value);
      this.modalRef.hide()
      this.getdata()
      this._NotofyService.onSuccess("ลบข้อมูล")
    })
  }
  editGovernmentinspectionplan(value,delid) {
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }

    this.loading = false
    this.governmentinspectionplanservice.editGovernmentinspectionplan(value, this.Form.value.files,delid,this.filesname).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.getdata()
      this._NotofyService.onSuccess("แก้ไขข้อมูล")
    })
  }
  get f() { return this.Form.controls; }
}
