import { Component, OnInit, TemplateRef, Inject, SecurityContext } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

import { FormGroup, FormBuilder, FormControl, Validators, ValidatorFn, AbstractControl } from '@angular/forms';
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
import { NotofyService } from '../services/notofy.service';
import { SideService } from '../services/side.service';
import { DomSanitizer } from '@angular/platform-browser';
// import { SecurityContext } from '@angular/compiler/src/core';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
  host: {
    "(window:resize)": "onWindowResize($event)"
  }
})
export class UserComponent implements OnInit {

  public myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd/mm/yyyy',
  };
  re2 = /เขตตรวจราชการที่/g
  MinistryId
  DepartmentId
  modalRef: BsModalRef;
  selectdatarole: Array<any>
  selectdataministry: Array<any>
  selectdatadeparment: Array<any>
  selectdataprovince: Array<any>
  selectdataregion: Array<any>
  selectdatafiscalyear: Array<any>
  selectdataprovincialdepartment: any[] = []
  selectdataside: Array<any>
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
  // MinistryId: any;
  UserRegion: any;
  FiscalYear: any;
  files: string[] = [];
  imgprofileUrl: any;
  img: any;
  Startdate: any;
  Enddate: any;
  Commandnumberdate: any;
  Side: any;
  ed: any;
  cd: any;
  Autocreateuser: any = 1
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

  isMobile: boolean = false;
  width: number = window.innerWidth;
  height: number = window.innerHeight;
  mobileWidth: number = 900;
  user9proIndex: any = null

  fiscalYearId: any;
  date: any = { date: { year: (new Date()).getFullYear(), month: (new Date()).getMonth() + 1, day: (new Date()).getDate() } };
  title: string = 'รายชิ่อจังหวัด';
  content: string = 'Vivamus sagittis lacus vel augue laoreet rutrum faucibus.';
  html: string;
  submitted = false;
  constructor(
    private _NotofyService: NotofyService,
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
    private sideservice: SideService,
    private sanitizer: DomSanitizer,

    @Inject('BASE_URL') baseUrl: string
  ) {
    this.html = sanitizer.sanitize(SecurityContext.HTML, this.html);

    this.subscription = this.userService.getUserNav()
      .subscribe(
        result => {
          if (result.roleId != this.roleId) {
            this.loading = false;
            this.roleId = result.roleId
            setTimeout(() => { this.getData() }, 200)
          }
        });
    this.roleId = this.route.snapshot.paramMap.get('id') //เลขที่ส่งมาจาก url 
    this.imgprofileUrl = baseUrl + '/imgprofile';

  }
  ngOnDestroy() {
    // unsubscribe to ensure no memory leaks
    this.subscription.unsubscribe();
  }

  ngOnInit() {
    this.getData()
    this.userform()
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
    this.addForm.patchValue({
      Role_id: this.roleId
    })
  }
  onWindowResize(event) {
    this.width = event.target.innerWidth;
    this.height = event.target.innerHeight;
    this.isMobile = this.width < this.mobileWidth;
    //console.log(this.width);

  }
  get f() { return this.addForm.value }
  get k() { return this.addForm.controls; }
  // get t() { return this.addForm }
  getData() {
    this.spinner.show();
    this.getUser();
    this.getDataProvinces();
    this.getDataMinistries();
    this.getDatafiscalyear();
    //this.getDatafiscalyear();
    this.getDataSide();
    this.getRolename();



    //<!-- สิทธิ์การใช้งานจะแสดงในกรณีเปลี่ยนสิทธิ์ -->
    this.selectdatarole = this.datarole.map((item, index) => {
      return { value: item.id, label: item.name }
    })
    //<!-- END สิทธิ์การใช้งานจะแสดงในกรณีเปลี่ยนสิทธิ์ -->
  }

  openModal(template: TemplateRef<any>, IDdelete, UserName, Pw) {
    this.addForm.reset()
    this.submitted = false;
    this.id = IDdelete;//ID สำหรับลบ
    this.addForm.patchValue({
      Role_id: this.roleId,
      Autocreateuser: this.Autocreateuser,
      UserName: UserName,
      Pw: Pw
    })
    this.modalRef = this.modalService.show(template);
  }

  openeditModal(template: TemplateRef<any>, id, fiscalYearId, userRegion, UserProvince, ministryId: number, departmentId: number, provincialDepartmentId, SideId,
    commandnumber, commandnumberdate, email, prefix, fname, lname, position, phoneNumber, startdate, enddate, img, Autocreateuser, signature, userName) {
    // alert(commandnumber +"///"+commandnumberdate);
    // console.log("gg",item.userProvince,'userprovince',UserProvince);
    // alert(Autocreateuser);
    this.addForm.reset()
    this.submitted = false;
    this.Autocreateuser = Autocreateuser; // สร้าง UerName เองหรือป่าว
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
    if (enddate == null) {
      this.ed = enddate;
    } else {
      this.ed = this.time(enddate);
    }

    if (commandnumberdate == null) {
      this.cd = commandnumberdate;
    } else {
      this.cd = this.time(commandnumberdate);
    }

    this.addForm.patchValue({

      Role_id: this.roleId,
      Prefix: prefix,
      FName: fname,
      LName: lname,
      Position: position,
      PhoneNumber: phoneNumber,
      Email: email,
      MinistryId: ministryId,
      DepartmentId: departmentId,
      FiscalYear: fiscalYearId,
      ProvincialDepartmentId: provincialDepartmentId,
      UserRegion: userRegion.map(result => {
        return result.regionId
      }),
      UserProvince: this.roleId == 9 ? UserProvince.map(result => {
        // console.log("gg", result);

        return result.provinceId
      }) : UserProvince,

      // UserProvince: UserProvince,
      files: new FormControl(null, [Validators.required]),
      Startdate: this.time(startdate),
      Enddate: this.ed,
      Commandnumber: commandnumber,
      SideId: SideId,
      Commandnumberdate: this.cd,
      Formprofile: 0,
      Img: img,
      Signature: signature,
      Autocreateuser: Autocreateuser, //แพตข้อมูลว่าสร้าง UerName เองหรือป่าว
      UserName: userName
    })
    this.DepartmentId = departmentId
    // console.log(' value: departmentId', departmentId);

    this.getDataDepartments({ value: ministryId })
    this.getProvincialDepartments({ value: departmentId })
    this.MinistryId = ministryId

    this.modalRef = this.modalService.show(template);
  }

  getUser() {
    this.userService.getuserdata(this.roleId)
      .subscribe(result => {
        this.resultuser = result;
        //console.log('resultuser', result);

        this.loading = true
        this.spinner.hide();
        // console.log("userdata", this.resultuser);
      })
  }
  userpro(user: any[]) { return user.filter(((result, index) => index < 3)) }

  getRolename() {
    if (this.roleId == 1) {
      this.rolename = 'ผู้ดูแลระบบ'
    } else if (this.roleId == 2) {
      this.rolename = 'ผู้ดูแลแผนการตรวจราชการ'
    } else if (this.roleId == 3) {
      this.rolename = 'ผู้ตรวจราชการสำนักนายกรัฐมนตรี'
    } else if (this.roleId == 4) {
      this.rolename = 'ผู้ว่าราชการจังหวัด'
    } else if (this.roleId == 5) {
      this.rolename = 'หัวหน้าสำนักงานจังหวัด'
    } else if (this.roleId == 6) {
      this.rolename = 'ผู้ตรวจราชการกระทรวง'
    } else if (this.roleId == 7) {
      this.rolename = 'ที่ปรึกษาผู้ตรวจราชการภาคประชาชน'
    } else if (this.roleId == 8) {
      this.rolename = 'นายกรัฐมนตรี,รองนายกรัฐมนตรี'
    } else if (this.roleId == 9) {
      this.rolename = 'หน่วยงานตรวจราชการ'
    } else if (this.roleId == 10) {
      this.rolename = 'ผู้ตรวจราชการกรม'
    }
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
  getDataRegions($event) {
    this.regionService.getregiondataforuser($event.value).subscribe(res => {
      // let uniqueRegion: any = [];
      this.selectdataregion = res.importFiscalYearRelations.filter(
        (thing, i, arr) => arr.findIndex(t => t.regionId === thing.regionId) === i
      ).map((item, index) => {
        return {
          value: item.region.id,
          label: item.region.name
        }
      });
      // console.log(this.selectdataregion);
      //  = uniqueRegion
    })
  }


  getDataRegionsForTooltip(event) {
    this.regionService.getregiondataforuser(1).subscribe(res => {
      // this.html =
      //   `<span class="badge" >${res.importFiscalYearRelations.filter(
      //     // (thing, i, arr) => arr.findIndex(t => t.regionId == event.id) === i
      //     (resultf) => resultf.region.id == event.id
      //   ).map((item, index) => `${item.province.name}`)}<br></span>`
      // console.log(this.html, event.id);
      this.html =
        res.importFiscalYearRelations.filter(
          // (thing, i, arr) => arr.findIndex(t => t.regionId == event.id) === i
          (resultf) => resultf.region.id == event.id
        ).map((item, index) => `<span class="badge" >${item.province.name}</span>`)
      // console.log(this.html, event.id);
    })
  }

  getDataProvinces() {
    this.provinceService.getprovincedata2()
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

        // if (this.roleId != 1 && this.roleId != 2) {
        //   this.selectdataministry = result.filter((item, index) => {
        //     return item.id != 1
        //   }).map((item, index) => {
        //     return { value: item.id, label: item.name }
        //   })
        // } else {
        this.selectdataministry = result.map((item, index) => {
          return { value: item.id, label: item.name }
        })
        // }


      });
  }

  getDataDepartments(event) {
    this.departmentService.getdepartmentsforuserdata(event.value)
      .subscribe(result => {
        //   console.log('result', result);

        this.selectdatadeparment = result.map((item, index) => {
          return { value: item.id, label: item.name }
        })
      })
  }

  getProvincialDepartments(event) {
    this.userService.getprovincialdepartment(event.value)
      .subscribe(result => {
        console.log('provincaildepartmentId', result);

        this.selectdataprovincialdepartment = result.map((item, index) => {
          return { value: item.id, label: item.name }
        })
      })
  }

  getDataSide() {
    var test: any = [];
    this.sideservice.getdata()
      .subscribe(result => {
        this.selectdataside = result.map((item, index) => {
          return { value: item.id, label: item.name }
        })
      });
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
  store(value) {

    this.submitted = true;
    if (this.addForm.invalid) {
      console.log('mmm', Object.values(this.addForm.controls).map((result, index) => { return result.status == "INVALID" ? Object.keys(this.addForm.controls)[index] : false }), Object.keys(this.addForm.controls)[11])
      return;
    }

    this.addForm.patchValue({
      Autocreateuser: this.Autocreateuser,
    })
    this.userService.addUser(this.addForm.value, this.addForm.value.files, this.roleId)
      .subscribe(response => {
        this.addForm.reset()
        this.modalRef.hide()
        this.loading = false
        this._NotofyService.onSuccess("เพื่มข้อมูล")
        this.getUser()
      })
  }

  updateuser(value) {
    // alert(1);
    this.submitted = true;
    if (this.addForm.invalid) {
      // console.log('mmm', Object.values(this.addForm.controls).map((result, index) => { return result.status == "INVALID" ? this.addForm.controls[index] : false }), Object.keys(this.addForm.controls)[25])
      return;
    }
    this.addForm.patchValue({
      Autocreateuser: this.Autocreateuser,
    })
    this.userService.editprofile(this.addForm.value, this.addForm.value.files, null, this.id)
      .subscribe(response => {
        // this.userService.changepassword(this.id)
        // .subscribe(result=>{
        //   console.log('result changepassword' , result);

        // })
        // // alert(3);
        this.addForm.reset()
        this.modalRef.hide()
        this.loading = false
        this._NotofyService.onSuccess("แก้ไขข้อมูล")
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

  resetpassword(id) {
    this.userService.resetpassword(id).subscribe(response => {
      this.modalRef.hide()
      this.loading = false
      this._NotofyService.onSuccess("รีเซ็ต Password ")
      this.getUser()
    })
  }

  userform() {
    let roleId = this.route.snapshot.paramMap.get('id')
    //alert(this.Autocreateuser)
    this.addForm = this.fb.group({
      Prefix: new FormControl(null, [Validators.required]),
      FName: new FormControl(null, [Validators.required]),
      LName: new FormControl(null, [Validators.required]),
      Position: new FormControl(null, [Validators.required]),
      Role_id: new FormControl(null),
      PhoneNumber: new FormControl(null, [Validators.required]),
      Email: new FormControl(null, [Validators.required]),
      ProvinceId: new FormControl(null),
      MinistryId: [
        null,
        conditionalValidator(
          (() => (this.roleId == 1 || this.roleId == 2 || this.roleId == 6 || this.roleId == 8 || this.roleId == 9 || this.roleId == 10) === true),
          Validators.required
        )
      ],
      DepartmentId: [
        null,
        conditionalValidator(
          (() => (this.roleId == 1 || this.roleId == 2 || this.roleId == 6 || this.roleId == 8 || this.roleId == 9 || this.roleId == 10) === true),
          Validators.required
        )
      ], //20201027  yochigang
      FiscalYear: [
        1,
        conditionalValidator(
          (() => (this.roleId == 3 || this.roleId == 6 || this.roleId == 8) === true),
          Validators.required
        )
      ],

      ProvincialDepartmentId: [null,
        conditionalValidator(
          (() => (this.roleId == 9) === true),
          Validators.required
        )
      ],
      UserRegion: [
        null,
        conditionalValidator(
          (() => (this.roleId == 3 || this.roleId == 6 || this.roleId == 8) === true),
          Validators.required
        )
      ],
      UserProvince: [
        null,
        conditionalValidator(
          (() => (this.roleId == 4 || this.roleId == 5 || this.roleId == 7 || this.roleId == 9) === true),
          Validators.required
        )
      ],
      SubdistrictId: new FormControl(null),
      DistrictId: new FormControl(null),
      files: new FormControl(null),
      Startdate: new FormControl(null, [Validators.required]),
      Enddate: new FormControl(null),
      Commandnumber: new FormControl(null),
      SideId: [
        null,
        conditionalValidator(
          (() => (this.roleId == 7) === true),
          Validators.required
        )
      ],
      Commandnumberdate: new FormControl(null),
      Formprofile: 0,
      Img: new FormControl(null),
      Signature: new FormControl(null),
      UserName: [
        null,
        conditionalValidator(
          (() => (this.Autocreateuser == 0) === true),
          Validators.required
        )
      ],
      Autocreateuser: new FormControl(null, [Validators.required]),
      Pw: new FormControl(null),
      // con1: new FormControl(null, [Validators.required]),
      // con2: new FormControl(null, [Validators.required]),
    })

    function conditionalValidator(condition: (() => boolean), validator: ValidatorFn): ValidatorFn {
      return (control: AbstractControl): { [key: string]: any } => {
        if (!condition()) {
          return null;
        }
        return validator(control);
      }
    }
  }
  time(date) {
    var ssss = new Date(date)
    var new_date = { date: { year: ssss.getFullYear(), month: ssss.getMonth() + 1, day: ssss.getDate() } }
    return new_date
  }
}
