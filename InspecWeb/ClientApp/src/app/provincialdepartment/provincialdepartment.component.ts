import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MinistryService } from '../services/ministry.service';
import { DepartmentService } from '../services/department.service';
import { ProvincialDepartmentService } from '../services/provincialdepartment.service';
import { ProvinceService } from '../services/province.service';
import { NotofyService } from '../services/notofy.service';
import { ExternalOrganizationService } from '../services/external-organization.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from '../services/user.service';
import { LogService } from '../services/log.service';

@Component({
  selector: 'app-provincialdepartment',
  templateUrl: './provincialdepartment.component.html',
  styleUrls: ['./provincialdepartment.component.css']
})
export class ProvincialDepartmentComponent implements OnInit {

  resultprovincialdepartment: any = []
  resultdetail: any = []
  selectedProvince: any[] = []
  id: any;
  selectdataprovince: any[] = []
  ministryname: data = {}
  departmentname: data = {}
  name:any;
  provincialdepartmentId: any;
  departmentId: any;
  ministryid: any;
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
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private modalService: BsModalService,
    private ministryservice: MinistryService,
    private departmentService: DepartmentService,
    private provinceService: ProvinceService,
    private provincialDepartmentService: ProvincialDepartmentService,
    private external: ExternalOrganizationService,
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
    this.getDataProvinces();

    this.Form = this.fb.group({
      Name: new FormControl(null, [Validators.required]),
      Province: new FormControl(null, [Validators.required]),
    })
  }

  getdata() {
    this.authorize.getUser()
    .subscribe(result => {
      this.userid = result.sub
      this.userService.getuserfirstdata(this.userid)
        .subscribe(result => {
        this.role_id = result[0].role_id
      })
    })
    this.ministryservice.getministryfirst2(this.id).subscribe(result => {
      this.ministryname = result
    })

    this.departmentService.getdepartmentsfirst(this.id).subscribe(result => {
      this.departmentname = result
    })

    this.provincialDepartmentService.getprovincialdepartmentdata(this.id).subscribe(result => {
     // console.log(result);
      this.resultprovincialdepartment = result
      this.loading = true;
    })

  }

  getDataProvinces() {

    this.provinceService.getprovincedata2()
      .subscribe(result => {
        this.external.getProvinceRegion()
          .subscribe(result2 => {
            this.selectdataprovince = result.map(result => {
              // console.log(
              //   result.name
              // );
              var region = result2.filter(
                (thing, i, arr) => arr.findIndex(t => t.name === result.name) === i
              )[0].region
              // console.log(
              //   region
              // );


              return { ...result, region: region, label: result.name, value: result.id }
            })
            //console.log(this.selectdataprovince);
          })
        // this.spinner.hide();
      })

  }
  public onSelectAll() {
    var selected = this.selectdataprovince.map(item => item.id);
    this.Form.get('Province').patchValue(selected);
    this.selectedProvince = selected
  }

  public onClearAll() {
    this.Form.get('Province').patchValue([]);
  }
  openModal(template: TemplateRef<any>, departmentId) {
    this.Form.reset()
    this.submitted = false;
    this.departmentId = departmentId;
    // alert(this.ministryid);
    this.modalRef = this.modalService.show(template);
  }

  deleteModal(template: TemplateRef<any>, deleteID,name) {
    this.departmentId = deleteID
    this.name = name;
    this.modalRef = this.modalService.show(template);
  }

  editModal(template: TemplateRef<any>, id, Name) {
    this.Form.reset()
    this.submitted = false;
    this.provincialDepartmentService.getdetaildata(id).subscribe(response => {
      //console.log('datadetail',response)
      this.resultdetail = response;
      this.provincialdepartmentId = id;
      this.Form.patchValue({
        Name: Name,
        Province: response.map(result => { return result.provinceId }),

      })
      this.selectedProvince = response.map(result => { return result.provinceId })
    })

    this.modalRef = this.modalService.show(template);
  }

  openDetailModal(template: TemplateRef<any>, id, name) {

    this.provincialDepartmentService.getdetaildata(id).subscribe(response => {
      //console.log('datadetail', response)
      this.resultdetail = response;
    })
    this.modalRef = this.modalService.show(template);
  }


  storeprovincialDepartment(value) {
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }
    this.provincialDepartmentService.addProvincialDepartment(value, this.departmentId).subscribe(response => {
      this.logService.addLog(this.userid,'ProvincialDepartment','เพิ่ม',response.name,response.id).subscribe();

      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.getdata();
      this._NotofyService.onSuccess("เพื่มข้อมูล")
    })
  }

  updateprovincialDepartment(value) {
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }
    this.provincialDepartmentService.updateProvincialDepartment(value, this.provincialdepartmentId).subscribe(response => {
      this.logService.addLog(this.userid,'ProvincialDepartment','แก้ไข',response.name,response.id).subscribe();
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.getdata();
      this._NotofyService.onSuccess("แก้ไขข้อมูล")
    })
  }

  deleteprovincialDepartment(value) {

    this.provincialDepartmentService.deleteProvincialDepartment(value).subscribe(response => {
      this.logService.addLog(this.userid,'ProvincialDepartment','ลบ',this.name,this.departmentId).subscribe();
      this.modalRef.hide()
      this.loading = false
      this.getdata();
      this._NotofyService.onSuccess("ลบข้อมูล")
    })
  }
  get f() { return this.Form.controls; }
}

export interface data {
  id?: number;
  name?: string;
};
