import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { IOption } from 'ng-select';
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

@Component({
  selector: 'app-external-register',
  templateUrl: './external-register.component.html',
  styleUrls: ['./external-register.component.css']
})
export class ExternalRegisterComponent implements OnInit {

  private myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd/mm/yyyy',
  };

  modalRef: BsModalRef;
  selectdatarole: Array<IOption>
  selectdataministry: Array<IOption>
  selectdatadeparment: Array<IOption>
  selectdataprovince: Array<IOption>
  selectdataregion: Array<IOption>
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

  constructor(
    private modalService: BsModalService,
    private router: Router,
    private provinceService: ProvinceService,
    private regionService: RegionService,
    private ministryService: MinistryService,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private userService: UserService,
    private spinner: NgxSpinnerService,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.subscription = this.userService.getUserNav()
      .subscribe(
        result => {

        });


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
    })
  }

  getData() {
    this.spinner.show();
    this.dtOptions = {
      pagingType: 'full_numbers',

    };
    this.getDataProvinces()
    this.getDataRegions()
    this.getDataMinistries()
    this.selectdatarole = this.datarole.map((item, index) => {
      return { value: item.id, label: item.name }
    })
    this.selectdatadeparment = this.datadeparment.map((item, index) => {
      return { value: item.id, label: item.name }
    })

  }

  openModal(template: TemplateRef<any>, IDdelete) {
    this.id = IDdelete;//ID สำหรับลบ
    this.modalRef = this.modalService.show(template);
  }

  getDataProvinces() {
    this.provinceService.getprovincedata()
      .subscribe(result => {
        this.selectdataprovince = result.map((item, index) => {
          return { value: item.id, label: item.name }
        })

      })
  }

  getDataRegions() {
    this.regionService.getregiondata()
      .subscribe(result => {
        this.selectdataregion = result.map((item, index) => {
          return { value: item.id, label: item.name }
        })

      })
  }

  getDataMinistries() {
    this.ministryService.getministry()
      .subscribe(result => {
        this.selectdataministry = result.map((item, index) => {
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
    this.userService.addUser(value, this.addForm.value.files).subscribe(response => {
      //alert(3);
      this.addForm.reset()
      // this.modalRef.hide()
      // this.loading = false
      this.router.navigate(['/train/']);
    })
  }

  userform() {
    this.addForm = this.fb.group({
      Prefix: new FormControl(null, [Validators.required]),
      Name: new FormControl(null, [Validators.required]),
      Position: new FormControl(null, [Validators.required]),
      Role_id: new FormControl(11, [Validators.required]),
      PhoneNumber: new FormControl(null, [Validators.required]),
      Email: new FormControl(null, [Validators.required]),
      ProvinceId: new FormControl(null),
      MinistryId: new FormControl(null, [Validators.required]),
      DepartmentId: new FormControl(null),
      ProvincialDepartmentId: new FormControl(null),
      UserRegion: new FormControl(null, [Validators.required]),
      UserProvince: new FormControl(null, [Validators.required]),
      SubdistrictId: new FormControl(null),
      DistrictId: new FormControl(null),
      files: new FormControl(null, [Validators.required]),
      Startdate: new FormControl(null, [Validators.required]),
      Enddate: new FormControl(null, [Validators.required]),
    })
  }

  back() {
    window.history.back()
  }

}