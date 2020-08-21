import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MinistryService } from '../services/ministry.service';
import { DepartmentService } from '../services/department.service';
import { NotofyService } from '../services/notofy.service';

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

  departmentId:any;
  ministryid:any;
  Form: FormGroup;
  EditForm: FormGroup;
  loading = false;
  dtOptions: DataTables.Settings = {};
  modalRef: BsModalRef;

  constructor(
    private fb: FormBuilder,
    private router:Router,
    private activatedRoute : ActivatedRoute,
    private modalService: BsModalService,
    private ministryservice: MinistryService,
    private departmentService: DepartmentService,
    private _NotofyService: NotofyService,
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
    this.ministryid = ministryid;
    // alert(this.ministryid);
    this.modalRef = this.modalService.show(template);
  }

  deleteModal(template: TemplateRef<any>, deleteID){
   this.departmentId =  deleteID
   this.modalRef = this.modalService.show(template);
  }

  editModal(template: TemplateRef<any>, id,Name,NameEN,ShortnameEN,ShortnameTH) {
   // alert(id);
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
    //alert(1);
    this.departmentService.addDepartment(value,this.ministryid).subscribe(response => {
      //alert(3);
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.getdata();
      this._NotofyService.onSuccess("เพื่มข้อมูล")
    })
  }

  updateDepartment(value){
   // alert(1 + ' : '+ this.departmentId);
    this.departmentService.editDepartment(value,this.departmentId).subscribe(response => {
     // alert(3);
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.getdata();
      this._NotofyService.onSuccess("แก้ไขข้อมูล")
    })
  }

  deleteDepartment(value) {
   // alert(1);
    this.departmentService.deleteDepartment(value).subscribe(response => {
     // alert(3);
      this.modalRef.hide()
      this.loading = false
      this.getdata();
      this._NotofyService.onSuccess("ลบข้อมูล")
    })
  }

  viewProvincialDepartment(id) {
    this.router.navigate(['/ministry/department/'+id+'/provincialdepartment'])
  }

}
export interface Ministryname{
  id?: number;
  name?: string;
};