import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SubdistrictService } from '../services/subdistrict.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from '../services/user.service';
import { LogService } from '../services/log.service';

@Component({
  selector: 'app-subdistrict',
  templateUrl: './subdistrict.component.html',
  styleUrls: ['./subdistrict.component.css']
})
export class SubdistrictComponent implements OnInit {

  resultsubdistrict: any = []
  id;
  titleprovince: [];
  titledistrict: [];
  Form: FormGroup;
  modalRef: BsModalRef;
  subdistrict_id:any;
  loading = false;
  dtOptions: DataTables.Settings = {};
  province_id:any;
  provincename:any;
  districtname:any;
  name:any;
  submitted = false;
  userid :any;
  role_id :any;
  constructor(
    private modalService: BsModalService,
    private fb: FormBuilder,
    private subdistrictservice: SubdistrictService,
    private activatedRoute: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private router:Router,
    // public share: SubdistrictService,
    private authorize: AuthorizeService,
    private userService: UserService,
    private logService: LogService,
    ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.provincename = activatedRoute.snapshot.paramMap.get('provincename')
    this.districtname = activatedRoute.snapshot.paramMap.get('districtname')
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
    this.form();
    this.getsubdistrict();
  }
  getsubdistrict(){
    this.authorize.getUser()
    .subscribe(result => {
      this.userid = result.sub
      this.userService.getuserfirstdata(this.userid)
        .subscribe(result => {
        this.role_id = result[0].role_id
      })
    })
    this.spinner.show();
    this.subdistrictservice.getsubdistrictdata(this.id).subscribe(result => {
      this.resultsubdistrict = result
      // this.titleprovince = result[0].district.province.name
      // this.province_id = result[0].district.province.id
      // this.titledistrict = result[0].district.name
      this.loading = true;
      this.spinner.hide();
    
    })
  }

  openModal(template: TemplateRef<any>=null, id=null,name=null) {
    this.Form.reset()
    this.submitted = false;
    this.name = name;
    this.subdistrict_id = id;//ID สำหรับลบ
    this.Form.patchValue({
      Name: name,
    })
    this.modalRef = this.modalService.show(template);
  }
  storeSubdistrict(value){
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }
    this.subdistrictservice.addsubdistrict(value,this.id).subscribe(response => {
      this.logService.addLog(this.userid,'Subdistricts','เพิ่ม',response.name,response.id).subscribe();
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.getsubdistrict()
    })
  }

  updateSubdistrict(value,id){
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }
    this.subdistrictservice.editsupdistrict(value,id).subscribe(response => {
      this.logService.addLog(this.userid,'Subdistricts','แก้ไข',response.name,response.id).subscribe();
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.getsubdistrict()
    })
  }

  delete(value) {
    this.subdistrictservice.deletesupdistrict(value).subscribe(response => {
      this.logService.addLog(this.userid,'Subdistricts','ลบ',this.name,this.subdistrict_id).subscribe();
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.getsubdistrict()
    })
  }

  form() {
    this.Form = this.fb.group({
      Name: new FormControl(null, [Validators.required]),
    })
  }
  vilage(iddistrict,idsubdistrict,provincename){
    alert(provincename);
    this.router.navigate(['/vilage',iddistrict,idsubdistrict,provincename])
  }
  get f() { return this.Form.controls; }
}
