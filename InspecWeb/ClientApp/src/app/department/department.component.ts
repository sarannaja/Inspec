import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MinistryService } from '../services/ministry.service';
import { DepartmentService } from '../services/department.service';
import { NotofyService } from '../services/notofy.service';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { UserService } from '../services/user.service';
import { LogService } from '../services/log.service';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css']
})
export class DepartmentComponent implements OnInit {

  resultdepartment: any = []
  id :any;
  titleprovince: []
  titledistrict: []
  titlesubdistrict: []

  ministryname:Ministryname = {}
  name:any;
  departmentId:any;
  ministryid:any;
  Form: FormGroup;
  EditForm: FormGroup;
  loading = false;
  dtOptions: any = {};
  modalRef: BsModalRef;
  submitted = false;
  userid :any;
  role_id :any;

  constructor(
    private fb: FormBuilder,
    private router:Router,
    private activatedRoute : ActivatedRoute,
    private modalService: BsModalService,
    private ministryservice: MinistryService,
    private departmentService: DepartmentService,
    private _NotofyService: NotofyService,
    private authorize: AuthorizeService,
    private userService: UserService,
    private logService: LogService,
    ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
   }

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
    this.ministryservice.getministryfirst(this.id).subscribe(result=>{
      this.ministryname = result
    })

    this.departmentService.getdepartmentsdata(this.id)

    .subscribe(result=>{
      //console.log("data",result);
      this.resultdepartment = result

      this.loading = true;

    })

  }

  openModal(template: TemplateRef<any>, ministryid) {
    this.Form.reset()
    this.submitted = false;
    this.ministryid = ministryid;
    this.modalRef = this.modalService.show(template);
  }

  deleteModal(template: TemplateRef<any>, deleteID,name){
   this.departmentId =  deleteID
   this.name = name;
   this.modalRef = this.modalService.show(template);
  }

  editModal(template: TemplateRef<any>, id,Name,NameEN,ShortnameEN,ShortnameTH) {
    this.Form.reset()
    this.submitted = false;
    this.departmentId = id;
    this.Form.patchValue({
      Name: Name,
      NameEN : NameEN,
      ShortnameEN : ShortnameEN,
      ShortnameTH : ShortnameTH
      
    })
   this.modalRef = this.modalService.show(template);
 }


  storeDepartment(value) {
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }
    this.departmentService.addDepartment(value,this.ministryid).subscribe(response => {
      this.logService.addLog(this.userid,'Departments','เพิ่ม',response.name,response.id).subscribe();
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.getdata();
      this._NotofyService.onSuccess("เพื่มข้อมูล")
    })
  }

  updateDepartment(value){
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }
    this.departmentService.editDepartment(value,this.departmentId).subscribe(response => {
      this.logService.addLog(this.userid,'Departments','แก้ไข',response.name,response.id).subscribe();
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.getdata();
      this._NotofyService.onSuccess("แก้ไขข้อมูล")
    })
  }

  deleteDepartment(value) {
    this.departmentService.deleteDepartment(value).subscribe(response => {
      this.logService.addLog(this.userid,'Departments','ลบ',this.name,this.departmentId).subscribe();
      this.modalRef.hide()
      this.loading = false
      this.getdata();
      this._NotofyService.onSuccess("ลบข้อมูล")
    })
  }

  viewProvincialDepartment(id) {
    this.router.navigate(['/ministry/department/'+id+'/provincialdepartment'])
  }
  get f() { return this.Form.controls; }
}
export interface Ministryname{
  id?: number;
  name?: string;
};