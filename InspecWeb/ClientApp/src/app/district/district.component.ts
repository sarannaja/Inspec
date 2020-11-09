import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { DistrictService } from '../services/district.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from "ngx-spinner";
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from '../services/user.service';
import { LogService } from '../services/log.service';
@Component({
  selector: 'app-district',
  templateUrl: './district.component.html',
  styleUrls: ['./district.component.css']
})
export class DistrictComponent implements OnInit {

  resultdistrict: any = []
  id
  name: any
  titleprovince:[]
  modalRef: BsModalRef;
  Form: FormGroup;
  loading = false;
  district_id : any;
  submitted = false;
  dtOptions: DataTables.Settings = {};
  userid :any;
  role_id :any;

  constructor(
    private modalService: BsModalService,
    private fb: FormBuilder,
    private districtservice: DistrictService,
    private activatedRoute : ActivatedRoute,
    private router:Router,
    private spinner: NgxSpinnerService,
    public share: DistrictService,
    private authorize: AuthorizeService,
    private userService: UserService,
    private logService: LogService,
    ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.name = activatedRoute.snapshot.paramMap.get('name')
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
    this.getdistrict();
    this.form();
  }

  getdistrict(){
    this.authorize.getUser()
    .subscribe(result => {
      this.userid = result.sub
      this.userService.getuserfirstdata(this.userid)
        .subscribe(result => {
        this.role_id = result[0].role_id
      })
    })
    this.spinner.show();
    this.districtservice.getdistrictdata(this.id).subscribe(result => {
      this.resultdistrict = result
      //this.titleprovince = result[0].province.name
      this.loading = true;
      this.spinner.hide();
      //console.log(this.resultdistrict);
    })
  }

  Subdistrict(id,provincename,districtname){
    this.router.navigate(['/subdistrict',id,provincename,districtname])
  }

  openModal(template: TemplateRef<any>=null, id=null,name=null) {
    this.Form.reset()
    this.submitted = false;
    this.district_id = id;//ID สำหรับลบ
    this.name = name;
    this.Form.patchValue({
      Name: name,
    })
    this.modalRef = this.modalService.show(template);
  }
  storeDistrict(value){
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }
    this.districtservice.adddistrict(value,this.id).subscribe(response => {
      this.logService.addLog(this.userid,'Districts','เพิ่ม',response.name,response.id).subscribe();
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.getdistrict()
    })
  }

  updateDistrict(value,id){
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }
    this.districtservice.editdistrict(value,id).subscribe(response => {
      this.logService.addLog(this.userid,'Districts','แก้ไข',response.name,response.id).subscribe();
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.getdistrict()
    })
  }

  delete(value) {
    this.districtservice.deletedistrict(value).subscribe(response => {
      this.logService.addLog(this.userid,'Districts','ลบ',this.name,this.district_id).subscribe();
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.getdistrict()
    })
  }

  form() {
    this.Form = this.fb.group({
      Name: new FormControl(null, [Validators.required]),
    })
  }
  get f() { return this.Form.controls; }
}
