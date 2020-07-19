import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { NotificationService } from '../services/Pipe/alert.service';
import { ProvinceService } from '../services/province.service';
import { RegionService } from '../services/region.service';
import { MinistryService } from '../services/ministry.service';
import { UserService } from '../services/user.service'; // ผู้ใช้
import { NgxSpinnerService } from "ngx-spinner";
import { Subscription } from 'rxjs/internal/Subscription';
import { IMyOptions } from 'mydatepicker-th';
import { DepartmentService } from '../services/department.service';
import { FiscalyearService } from '../services/fiscalyear.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  public myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd/mm/yyyy',
  };

  modalRef: BsModalRef;
  selectdatarole: Array<any>
  selectdataministry: Array<any>
  selectdatadeparment: Array<any>
  selectdataprovince: Array<any>
  selectdataregion: Array<any>
  selectdatafiscalyear: Array<any>
  selectdataprovincialdepartment: Array<any>
  loading = false;
  dtOptions: DataTables.Settings = {};
  roleId: any;
  rolename: any;
  resultuser: any[] = [];
  resultfirstuser: any[] = [];
  subscription: Subscription;
  addForm: FormGroup;
  id: any;
  //name input
  Prefix: any;
  Name: any;
  Position: any;
  Role_id: any;
  PhoneNumber: any;
  Email: any;
  ProvinceId: any;
  DistrictId: any;
  SubdistrictId: any;
  MinistryId: any;
  UserRegion: any;
  files: string[] = [];
  imgprofileUrl: any;
  Startdate: any;
  Enddate: any;
  //END name input
  datarole: any = [
    {
      id: 1,
      name: 'Super Admin'
    },
    {
      id: 2,
      name: 'Admin แผนการตรวจราชการประจำปี'
    },
    {
      id: 3,
      name: 'ผู้ตรวจราชการ'
    },
    {
      id: 4,
      name: 'ผู้ว่าราชการจังหวัด'
    },
    {
      id: 5,
      name: 'ผู้ตรวจจังหวัด'
    },
    {
      id: 6,
      name: 'ผู้ตรวจกระทรวง'
    },
    {
      id: 7,
      name: 'ผู้ตรวจภาคประชาชน'
    },
    {
      id: 8,
      name: 'นายก/รองนายก'
    },
    {
      id: 9,
      name: 'ผู้ตรวจกรม/หน่วยงาน'
    },

  ]

  datadeparment: any = [
    {
      id: 1,
      name: 'กองทัพไทย'
    },
    {
      id: 2,
      name: 'สำนักงานรัฐมนตรีกระทรวงการคลัง'
    },
  ]
  fiscalYearId: any;
  date: any = { date: { year: (new Date()).getFullYear(), month: (new Date()).getMonth() + 1, day: (new Date()).getDate() } };

  constructor(
    private modalService: BsModalService,
    private router: Router,
    private provinceService: ProvinceService,
    private regionService: RegionService,
    private ministryService: MinistryService,
    private departmentService: DepartmentService,
    private fiscalyearService: FiscalyearService,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private userService: UserService,
    private spinner: NgxSpinnerService,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.subscription = this.userService.getUserNav()
      .subscribe(
        result => {
          if (result.roleId != this.roleId) {
            this.loading = false;
            this.roleId = result.roleId
            setTimeout(() => { this.getData() }, 200)
          }
        });
    this.roleId = this.route.snapshot.paramMap.get('id')
    this.imgprofileUrl = baseUrl + '/imgprofile';

    //เลขที่ส่งมาจาก url 
  }
  ngOnDestroy() {
    // unsubscribe to ensure no memory leaks
    this.subscription.unsubscribe();
  }
  ngOnInit() {
    this.getData()
    this.userform()
    this.addForm.patchValue({
      Role_id: this.roleId
    })
  }

  getData() {
    this.spinner.show();
    this.dtOptions = {
      pagingType: 'full_numbers',

    };
    this.getUser()
    this.getDataProvinces()
    //this.getDataRegions()
    this.getDataMinistries()
    this.getDatafiscalyear()

    this.selectdatarole = this.datarole.map((item, index) => {
      return { value: item.id, label: item.name }
    })

    // this.selectdatadeparment = this.datadeparment.map((item, index) => {
    //   return { value: item.id, label: item.name }
    // })

    if (this.roleId == 1) {
      this.rolename = 'ผู้ดูแลระบบ'
    } else if (this.roleId == 2) {
      this.rolename = 'ผู้ดูแลแผนการตรวจราชการประจำปี'
    } else if (this.roleId == 3) {
      this.rolename = 'ผู้ตรวจราชการ'
    } else if (this.roleId == 4) {
      this.rolename = 'ผู้ว่าราชการจังหวัด'
    } else if (this.roleId == 5) {
      this.rolename = 'หัวหน้าสำนักงานจังหวัด'
    } else if (this.roleId == 6) {
      this.rolename = 'ผู้ตรวจราชการกระทรวง'
    } else if (this.roleId == 7) {
      this.rolename = 'ผู้ตรวจภาคประชาชน'
    } else if (this.roleId == 8) {
      this.rolename = 'ผู้บริหาร/นายก/รองนายก'
    } else if (this.roleId == 9) {
      this.rolename = 'หน่วยงานตรวจราชการ'
    } else if (this.roleId == 10) {
      this.rolename = 'ผู้ตรวจราชการกรม'
    }
  }

  openModal(template: TemplateRef<any>, IDdelete) {
    this.addForm.reset()
    this.id = IDdelete;//ID สำหรับลบ
    this.addForm.patchValue({
      Role_id: this.roleId
    })
    this.modalRef = this.modalService.show(template);
  }

  getUser() {
    this.userService.getuserdata(this.roleId)
      .subscribe(result => {
        //alert(this.roleId);
        this.resultuser = result;
        this.loading = true
        this.spinner.hide();
        console.log("userdata", this.resultuser);
      })
  }

  getDatafiscalyear() {
    this.fiscalyearService.getfiscalyeardata()
      .subscribe(result => {
        console.log('mo', result)
        this.selectdatafiscalyear = result.map((item, index) => {
          return { value: item.id, label: item.year }
        })

      })
  }

  getDataRegions(event) {

    this.regionService.getregiondataforuser(event.value).subscribe(res => {
      console.log("fiscalYearRelations: ", res);

      var uniqueRegion: any = [];
      uniqueRegion = res.importFiscalYearRelations.filter(
        (thing, i, arr) => arr.findIndex(t => t.regionId === thing.regionId) === i
      );
      console.log("uniqueRegions: ", uniqueRegion);

      this.selectdataregion = uniqueRegion.map((item, index) => {
        return {
          value: item.region.id,
          label: item.region.name
        }
      })
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

  getDataMinistries() {
    var test: any = [];
    this.ministryService.getministry()
      .subscribe(result => {
        this.selectdataministry = result.filter((item, index) => {
          return item.id != 1
        }).map((item, index) => {
          return { value: item.id, label: item.name }
        })
      });
  }

  getDataDepartments(event) {
    this.departmentService.getdepartmentsforuserdata(event.value)
      .subscribe(result => {
        this.selectdatadeparment = result.map((item, index) => {
          return { value: item.id, label: item.name }
        })
      })
  }

  getProvincialDepartments(event) {
    this.userService.getprovincialdepartment(event.value)
      .subscribe(result => {
        this.selectdataprovincialdepartment = result.map((item, index) => {
          return { value: item.id, label: item.name }
        })
      })
  }

  uploadFile(event) {
    //alert('uploadfile');
    const file = (event.target as HTMLInputElement).files;
    this.addForm.patchValue({
      files: file
    });
    this.addForm.get('files').updateValueAndValidity()

  }
  //เพิ่ม user
  adduser(value) {
    //alert(1);
    this.userService.addUser(value, this.addForm.value.files, this.roleId).subscribe(response => {
      //alert(3);
      this.addForm.reset()
      this.modalRef.hide()
      this.loading = false
      this.getUser()
    })
  }

  //ลบ user
  deleteuser(value) {
    this.userService.deleteUser(value).subscribe(response => {
      this.modalRef.hide()
      this.loading = false
      this.getUser()
    })
  }

  userform() {
    this.addForm = this.fb.group({
      Prefix: new FormControl(null, [Validators.required]),
      Name: new FormControl(null, [Validators.required]),
      Position: new FormControl(null, [Validators.required]),
      Role_id: new FormControl(null, [Validators.required]),
      PhoneNumber: new FormControl(null, [Validators.required]),
      Email: new FormControl(null, [Validators.required]),
      ProvinceId: new FormControl(null),
      MinistryId: new FormControl(null, [Validators.required]),
      DepartmentId: new FormControl(null),
      FiscalYear: new FormControl(null),
      ProvincialDepartmentId: new FormControl(null),
      UserRegion: new FormControl(null, [Validators.required]),
      UserProvince: new FormControl(null, [Validators.required]),
      SubdistrictId: new FormControl(null),
      DistrictId: new FormControl(null),
      files: new FormControl(null, [Validators.required]),
      Startdate: new FormControl(null, [Validators.required]),
      Enddate: new FormControl(null, [Validators.required]),
      Commandnumber: new FormControl(null),


    })
  }

}
