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
  selectdataside:Array<any>
  loading = false;
  dtOptions: DataTables.Settings = {};
  roleId: any;
  rolename: any;
  resultuser: any[] = [];
  resultfirstuser: any[] = [];
  subscription: Subscription;
  addForm: FormGroup;
  id: any;
  //<!-- input -->
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
  FiscalYear:any;
  files: string[] = [];
  imgprofileUrl: any;
  img:any;
  Startdate: any;
  Enddate: any;
  Commandnumberdate:any;
  Side:any;
  ed:any;
  cd:any;
  //<!-- END input -->
  datarole: any = [
    {
      id: 1,
      name: 'ผู้ดูแลระบบ'
    },
    {
      id: 2,
      name: 'ผู้ดูแลแผนการตรวจราชการประจำปี'
    },
    {
      id: 3,
      name: 'ผู้ตรวจราชการสำนักนายกรัฐมนตร'
    },
    {
      id: 4,
      name: 'ผู้ว่าราชการจังหวัด'
    },
    {
      id: 5,
      name: 'หัวหน้าสำนักงานจังหวัด'
    },
    {
      id: 6,
      name: 'ผู้ตรวจราชการกระทรวง'
    },
    {
      id: 7,
      name: 'ที่ปรึกษาผู้ตรวจราชการภาคประชาชน'
    },
    {
      id: 8,
      name: 'นายกรัฐมนตรี,รองนายกรัฐมนตร'
    },
    {
      id: 9,
      name: 'หน่วยงานตรวจราชการ'
    },
    {
      id: 10,
      name: 'ผู้ตรวจราชการกรม'
    },

  ]

  dataside: any = [
    {
      id: 'วิชาการ',
      name: 'วิชาการ'
    },
    {
      id: 'สังคม',
      name: 'สังคม'
    },
    {
      id: 'สิ่งแวดล้อม',
      name: 'สิ่งแวดล้อม'
    },
    {
      id: 'เศรษฐกิจ',
      name: 'เศรษฐกิจ'
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
    this.getUser()
    this.getDataProvinces()
    this.getDataMinistries()
    this.getDatafiscalyear()

    //<!-- ด้านสำหรับ role 7 -->
    this.selectdataside = this.dataside.map((item, index) => {
      return { value: item.id, label: item.name }
    })
    //<!-- END ด้านสำหรับ role 7 -->

    //<!-- สิทธิ์การใช้งานจะแสดงในกรณีเปลี่ยนสิทธิ์ -->
    this.selectdatarole = this.datarole.map((item, index) => {
      return { value: item.id, label: item.name }
    })
     //<!-- END สิทธิ์การใช้งานจะแสดงในกรณีเปลี่ยนสิทธิ์ -->

    

    if (this.roleId == 1) {
      this.rolename = 'ผู้ดูแลระบบ'
    } else if (this.roleId == 2) {
      this.rolename = 'ผู้ดูแลแผนการตรวจราชการประจำปี'
    } else if (this.roleId == 3) {
      this.rolename = 'ผู้ตรวจราชการสำนักนายกรัฐมนตร'
    } else if (this.roleId == 4) {
      this.rolename = 'ผู้ว่าราชการจังหวัด'
    } else if (this.roleId == 5) {
      this.rolename = 'หัวหน้าสำนักงานจังหวัด'
    } else if (this.roleId == 6) {
      this.rolename = 'ผู้ตรวจราชการกระทรวง'
    } else if (this.roleId == 7) {
      this.rolename = 'ที่ปรึกษาผู้ตรวจราชการภาคประชาชน'
    } else if (this.roleId == 8) {
      this.rolename = 'นายกรัฐมนตรี,รองนายกรัฐมนตร'
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
      Role_id: this.roleId,
    })
    this.modalRef = this.modalService.show(template);
  }

  openeditModal(template: TemplateRef<any>, id,fiscalYearId,userRegion,UserProvince,ministryId,departmentId,provincialDepartmentId,side,
    commandnumber,commandnumberdate,email,prefix,name,position,phoneNumber,startdate,enddate,img) 
  {
    this.addForm.reset()
    this.id = id;
    this.img = img;
    this.regionService.getregiondataforuser(fiscalYearId).subscribe(res => {    
      var uniqueRegion: any = [];
      uniqueRegion = res.importFiscalYearRelations.filter(
        (thing, i, arr) => arr.findIndex(t => t.regionId === thing.regionId) === i
      );
      this.selectdataregion = uniqueRegion.map((item, index) => {
        return {
          value: item.region.id,
          label: item.region.name
        }
      })
    })

    if(enddate == null){
      this.ed = enddate;
    }else{
      this.ed = this.time(enddate);
    }

    if(commandnumberdate == null){
      this.cd = commandnumberdate;
    }else{
      this.cd = this.time(commandnumberdate);
    }
    
    this.addForm.patchValue({
      
      Role_id: this.roleId,
      Prefix: prefix,
      Name: name,
      Position: position,
      PhoneNumber: phoneNumber,
      Email: email,
      MinistryId: ministryId,
      DepartmentId: departmentId,
      FiscalYear: fiscalYearId,
      ProvincialDepartmentId: provincialDepartmentId,
      UserRegion: userRegion.map(result=>{
        return result.regionId
      }),
      UserProvince: UserProvince,
      files: new FormControl(null, [Validators.required]),
      Startdate: this.time(startdate),
      Enddate: this.ed,
      Commandnumber: commandnumber,
      Side: side,
      Commandnumberdate: this.cd,
      Formprofile: 0,
      Img:img,
    })
    this.modalRef = this.modalService.show(template);
  }

  openchangeModal(template: TemplateRef<any>, id,fiscalYearId,userRegion,UserProvince,ministryId,departmentId,provincialDepartmentId,side,
    commandnumber,commandnumberdate,email,prefix,name,position,phoneNumber,startdate,enddate,img) 
  {
    //alert(this.roleId);
    this.addForm.reset()
    this.id = id;
    this.img = img;
    this.regionService.getregiondataforuser(fiscalYearId).subscribe(res => {    
      var uniqueRegion: any = [];
      uniqueRegion = res.importFiscalYearRelations.filter(
        (thing, i, arr) => arr.findIndex(t => t.regionId === thing.regionId) === i
      );
      this.selectdataregion = uniqueRegion.map((item, index) => {
        return {
          value: item.region.id,
          label: item.region.name
        }
      })
    })

    if(enddate == null){
      this.ed = enddate;
    }else{
      this.ed = this.time(enddate);
    }

    if(commandnumberdate == null){
      this.cd = commandnumberdate;
    }else{
      this.cd = this.time(commandnumberdate);
    }

    this.addForm.patchValue({
      Role_id: this.roleId,
      Prefix: prefix,
      Name: name,
      Position: position,
      PhoneNumber: phoneNumber,
      Email: email,
      MinistryId: ministryId,
      DepartmentId: departmentId,
      FiscalYear: fiscalYearId,
      ProvincialDepartmentId: provincialDepartmentId,
      UserRegion: userRegion.map(result=>{
        return result.regionId
      }),
      UserProvince: UserProvince,
      files: new FormControl(null, [Validators.required]),
      Startdate: this.time(startdate),
      Enddate: this.ed,
      Commandnumber: commandnumber,
      Side: side,
      Commandnumberdate: this.cd,
      Formprofile: 0,
      Img:img,
    })
    this.modalRef = this.modalService.show(template);
  }


  getUser() {
    this.userService.getuserdata(this.roleId)
      .subscribe(result => {
        this.resultuser = result;
        this.loading = true
        this.spinner.hide();
        //console.log("userdata", this.resultuser);
      })
  }

  getDatafiscalyear() {
    this.fiscalyearService.getfiscalyeardata()
      .subscribe(result => {
      //  console.log('mo', result)
        this.selectdatafiscalyear = result.map((item, index) => {
          return { value: item.id, label: item.year }
        })

      })
  }

  getDataRegions(event) {
    this.regionService.getregiondataforuser(event.value).subscribe(res => {    
      var uniqueRegion: any = [];
      uniqueRegion = res.importFiscalYearRelations.filter(
        (thing, i, arr) => arr.findIndex(t => t.regionId === thing.regionId) === i
      );
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

  updateuser(value) {
   // alert(1);
    this.userService.editprofile(value, this.addForm.value.files, this.id).subscribe(response => {
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
      Enddate: new FormControl(null),
      Commandnumber: new FormControl(null),
      Side: new FormControl(null),
      Commandnumberdate: new FormControl(null),
      Formprofile: 0,
      Img: new FormControl(null),
    })
  }
  time(date) {
    var ssss = new Date(date)
    var new_date = {date:{ year: ssss.getFullYear(), month: ssss.getMonth() + 1, day: ssss.getDate() }}
    return new_date
  }
}
