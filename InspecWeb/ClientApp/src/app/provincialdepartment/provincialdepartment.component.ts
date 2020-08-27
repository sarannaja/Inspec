import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MinistryService } from '../services/ministry.service';
import { DepartmentService } from '../services/department.service';
import { ProvincialDepartmentService } from '../services/provincialdepartment.service';
import { ProvinceService } from '../services/province.service';
import { NotofyService } from '../services/notofy.service';

@Component({
  selector: 'app-provincialdepartment',
  templateUrl: './provincialdepartment.component.html',
  styleUrls: ['./provincialdepartment.component.css']
})
export class ProvincialDepartmentComponent implements OnInit {

  resultprovincialdepartment: any = []
  resultdetail: any = []
  selectedProvince: any[] = []
  id :any;
  selectdataprovince: Array<any>=[]
  ministryname:data = {}
  departmentname:data = {}

  provincialdepartmentId :any;
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
    private provinceService: ProvinceService,
    private provincialDepartmentService: ProvincialDepartmentService,
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
    this.getDataProvinces();
   
    this.Form = this.fb.group({
      Name: new FormControl(null, [Validators.required]),
      Province: new FormControl(null, [Validators.required]),
    })
  }

  getdata(){

    this.ministryservice.getministryfirst2(this.id).subscribe(result=>{
      this.ministryname = result
    })

    this.departmentService.getdepartmentsfirst(this.id).subscribe(result=>{
      this.departmentname = result
    })

    this.provincialDepartmentService.getprovincialdepartmentdata(this.id).subscribe(result=>{
      //console.log(result);
      this.resultprovincialdepartment = result
      this.loading = true;
    })

  }

  getDataProvinces() {
    this.provinceService.getprovincedata()
      .subscribe(result => {
        this.selectdataprovince = result.map((item, index) => {
          return { value: item.id, label: item.name }
        })

      })
  }

  openModal(template: TemplateRef<any>, departmentId) {
    this.Form.reset()
    this.departmentId = departmentId;
    // alert(this.ministryid);
    this.modalRef = this.modalService.show(template);
  }

  deleteModal(template: TemplateRef<any>, deleteID){
   this.departmentId =  deleteID
   this.modalRef = this.modalService.show(template);
  }

  editModal(template: TemplateRef<any>, id,Name) {
   // alert(id);
   this.provincialDepartmentService.getdetaildata(id).subscribe(response => {
    //console.log('datadetail',response)
    this.resultdetail = response;
    this.provincialdepartmentId = id;
    this.Form.patchValue({
      Name : Name,
      Province: response.map(result=> {return result.provinceId}),
      
    })
    this.selectedProvince =  response.map(result=> {return result.provinceId})
  })
   
   this.modalRef = this.modalService.show(template);
 }

openDetailModal(template: TemplateRef<any>, id,name) {

  this.provincialDepartmentService.getdetaildata(id).subscribe(response => {
    console.log('datadetail',response)
    this.resultdetail = response;
  })
  this.modalRef = this.modalService.show(template);
}


storeprovincialDepartment(value) {
 
    this.provincialDepartmentService.addProvincialDepartment(value,this.departmentId).subscribe(response => {
     
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.getdata();
      this._NotofyService.onSuccess("เพื่มข้อมูล")
    })
  }

  updateprovincialDepartment(value){
   //alert(1)
    this.provincialDepartmentService.updateProvincialDepartment(value,this.provincialdepartmentId).subscribe(response => {
      //alert(3)
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.getdata();
      this._NotofyService.onSuccess("แก้ไขข้อมูล")
    })
  }

  deleteprovincialDepartment(value) {
   
    this.provincialDepartmentService.deleteProvincialDepartment(value).subscribe(response => {
     
      this.modalRef.hide()
      this.loading = false
      this.getdata();
      this._NotofyService.onSuccess("ลบข้อมูล")
    })
  }





}

export interface data{
  id?: number;
  name?: string;
};
