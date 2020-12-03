

import { Router } from '@angular/router';
import { Component, OnInit, Inject, TemplateRef, HostListener } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { superAdmin, Centraladmin, Inspector, Provincialgovernor, Adminprovince, InspectorMinistry, publicsector, president, InspectorDepartment, InspectorExamination, External, allNav, NavBar } from './_nav';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from 'src/app/services/user.service';
import { FormGroup, FormBuilder, FormControl, Validators, AbstractControl } from '@angular/forms';
import { NotificationService } from 'src/app/services/notification.service';
import { Location } from "@angular/common";
import { ExecutiveorderService } from 'src/app/services/executiveorder.service';
import * as _ from 'lodash';
import { MenuService } from 'src/app/services/menu.service';
import { User } from 'oidc-client';
import { PasswordStrengthValidator } from 'src/api-authorization/new-login/password-strength.validators';
@Component({
  selector: 'app-default-layout',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.css']
  ,
  host: {
    "(window:resize)": "onWindowResize($event)"
  }
})

export class DefaultLayoutComponent implements OnInit {
  @HostListener('document:keydown', ['$event'])
  handleKeyboardEvent(event: KeyboardEvent): void {
    event.keyCode === 37 ? this.toggled = true : event.keyCode === 39 ? this.toggled = false : null

  }
  toggled = false;
  classIcon = "align-middle mr-2 fas fa-fw "
  classIcon2 = "align-middle mr-2 far "
  urlActive = ""
  classtap = 'sidebar-header'
  userid: any
  role_id: any
  nav: any
  imgprofileUrl: any;
  SignatureUrl: any;
  resultuser: any[];
  resultfirstuser: any[] = [];
  modalRef: BsModalRef;
  Form: FormGroup;
  Prefix: any;
  Name: any;
  FName: any;
  LName: any;
  Position: any;
  PhoneNumber: any;
  Email: any;
  files: any;
  Img: any;
  Signature: any;
  UserName: any;
  Formprofile: any;
  resultnotifications: any[] = [];
  resultnotificationscount: any[] = [];
  // childClassIcon = "align-middle mr-2 fas fa-fw
  bridge2: Array<Bridge>
  isMobile: boolean = false;
  width: number = window.innerWidth;
  height: number = window.innerHeight;
  mobileWidth: number = 900;
  lockNav: boolean = true
  submitted = false;
  arraynav: NavBar[] = []
  constructor(
    private authorize: AuthorizeService,
    private userService: UserService,
    private notificationService: NotificationService,
    private menuService: MenuService,
    private router: Router,
    private modalService: BsModalService,
    private fb: FormBuilder,
    private _ExecutiveorderService: ExecutiveorderService,
    private locationx: Location,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.urlActive = location.pathname;

    this.imgprofileUrl = baseUrl + '/imgprofile';
    this.SignatureUrl = baseUrl + '/Signature';


    allNav.map(result => {
      for (let i = 0; i < result.length; i++) {
        this.arraynav.push(result[i])
      }
    })

    this.arraynav = _.uniqBy(this.arraynav, function (e) {
      return e.url
    }).concat(this.arraynav.filter(result => result.url == null))
    // this.arraynav = arraynav
    //console.log('this.arraynav', this.arraynav);

  }
  // 0C-54-15-66-C2-D6

  moNav() {



  }
  onToggle(value: any = null) {
    this.toggled = !this.toggled;
  }
  onLockNav() {
    this.lockNav = !this.lockNav
  }

  ngOnInit() {
    this.nav = superAdmin;
    this.profileform();
    this.getuserinfo();
    this.getnotifications();
    this.moNav()

    // this.urlActive = this.router.url;
    // this.getplancount();
    // this.checkactive(this.nav[0].url);
    this.isMobile = this.width < this.mobileWidth;
    // this.urlActive = this.nav[0].url
  }
  onWindowResize(event) {
    this.width = event.target.innerWidth;
    this.height = event.target.innerHeight;
    this.isMobile = this.width < this.mobileWidth;
    //console.log(this.width);

  }
  checkactive(url) {
    this.urlActive = url
    this.router.navigate([url])
    //ถ้าใช่โทรศัพท์ให้เปิดปิด  toggled เมื่อเปิดลิ้งใหม่ ถ้าไม่ใช่เช็คว่า nav ถูก ล็อคมั้ยถ้าล็อคก็ปิดไม่ได้เมื่อมันเย้
    this.isMobile ? (this.toggled = !this.toggled) : (this.lockNav ? null : this.toggled = !this.toggled)
  }

  userNav(url, id): void {
    this.router.navigate([url])
    // send message to subscribers via observable subject
    this.userService.sendNav(id);
  }

  Logout() {
    
    this.authorize.signOut({ local: true })
  }

  openModal(template: TemplateRef<any>) {
    // alert(1);
    this.Form.patchValue({
      Prefix: this.Prefix,
      FName: this.FName,
      LName: this.LName,
      Position: this.Position,
      PhoneNumber: this.PhoneNumber,
      Email: this.Email,
      Formprofile: 1,
      files: this.files,
      Img: this.Img,
      Signature: this.Signature,
    });
    this.modalRef = this.modalService.show(template);
  }

  passwordModal(template: TemplateRef<any>) {
    this.submitted = false;
    this.Form.patchValue({
      UserName: this.UserName,

    });
    this.modalRef = this.modalService.show(template);
  }

  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.Form.patchValue({
      files: file
    });
    this.Form.get('files').updateValueAndValidity()
  }

  uploadFile2(event) {
    const file = (event.target as HTMLInputElement).files;
    this.Form.patchValue({
      files2: file
    });
    this.Form.get('files2').updateValueAndValidity()
  }

  profileform() {
    this.Form = this.fb.group({
      Prefix: new FormControl(null, [Validators.required]),
      FName: new FormControl(null, [Validators.required]),
      LName: new FormControl(null, [Validators.required]),
      Position: new FormControl(null, [Validators.required]),
      PhoneNumber: new FormControl(null, [Validators.required]),
      Email: new FormControl(null, [Validators.required]),
      files: new FormControl(null, [Validators.required]),
      files2: new FormControl(null, [Validators.required]),
      Img: this.Img,
      Signature: this.Signature,
      Formprofile: 1,
      UserName: new FormControl(null, [Validators.required]),
      Password: ['', [Validators.required, PasswordStrengthValidator]],
      PassWordconfirm: new FormControl(null, [Validators.required])
    }, {
      validator: this.passwordConfirming
    })

  }
  passwordConfirming(c: AbstractControl): { invalid: boolean } {
    if (c.get('Password').value !== c.get('PassWordconfirm').value) {
      return { invalid: true };
    }
  }
  getnotifications() {
    // this._ExecutiveorderService.getexecutiveorderanswereddatafirst(this.userid)
    //   .subscribe(resultsub => {

    //    //console.log('resultsub', resultsub);

    this.notificationService.getnotificationsdata(this.userid)
      .subscribe(result => {
        this.resultnotifications = result
       // console.log('noti',result);
        //     .map(resultxe => {
        //       //console.log('this.getTest(result.xe)', resultsub.find(res => resultxe.xe == res.executiveOrder.id).executiveOrder.subject);

        //       // this._ExecutiveorderService.getexecutiveorderanswereddatafirst(result.xe)
        //       return { ...resultxe, subject: resultsub.find(res => resultxe.xe == res.executiveOrder.id).executiveOrder.subject }
        //     });

        // })
      })


    this.notificationService.getnotificationscountdata(this.userid)
      .subscribe(result => {
        this.resultnotificationscount = result;
      })
  }

  detailnotifications(id, statusid, xe, provinceId, userId, centralPolicyId) {
    this.notificationService.updateNotification(id)
      .subscribe(result => {
        if (statusid == 20) { //song
          location.href = '/commanderreport/detail/' + xe;
        }
        else if (statusid == 9) { //song
          location.href = '/reportimport/detail/' + xe;
        }
        else if (statusid == 16) { //aof
          location.href = '/usercentralpolicy/' + xe + '/' + provinceId;
          // this.router.navigate(['/usercentralpolicy/' + xe + '/' + provinceId])
        }
        else if (statusid == 1) { //aof
          location.href = '/usercentralpolicy/' + xe + '/' + provinceId;
          // this.router.navigate(['/usercentralpolicy/' + xe + '/' + provinceId])
        }
        else if (statusid == 2) { //aof role6 and 10 มาทำต่อด้วย
          if (this.role_id == 3) {
            location.href = '/inspectionplan/' + xe + '/' + provinceId + '/0';
          } else if (this.role_id == 6) {
            location.href = '/inspectionplan/ministry/' + xe + '/' + provinceId + '/0';
          } else if (this.role_id == 10) {
            location.href = '/inspectionplan/department/' + xe + '/' + provinceId + '/0';
          }
        }
        else if (statusid == 3) { //aof role6 and 10 มาทำต่อด้วย
          this.notificationService.getinspactionsplaneven(xe)
            .subscribe(result => {
              if (result.roleCreatedBy == "3") {
                location.href = '/inspectionplan/' + xe + '/' + provinceId + '/1';
              } else if (result.roleCreatedBy == "6") {
                location.href = '/inspectionplan/ministry/' + xe + '/' + provinceId + '/1';
              } else if (result.roleCreatedBy == "10") {
                location.href = '/inspectionplan/department/' + xe + '/' + provinceId + '/1';
              }
            })
          // location.href = '/inspectionplan/' + xe + '/' + provinceId + '/1';
        }
        else if (statusid == 4) { //ask nik
          location.href = '/answersubject/list/' + xe;
        }
        else if (statusid == 5) { //ask nik
          location.href = '/answerpeople';
        }
        else if (statusid == 6) { //ask nik
          this.notificationService.getCentralPolicyProvince(centralPolicyId, provinceId).subscribe(result => {

            //  location.href = '/subjectevent/detail/' + result, { subjectgroupid: xe, };
            location.href = '/subjectevent/detail/' + result + ';subjectgroupid=' + xe
          })
        }
        else if (statusid == 7) { //song
          this.notificationService.getElectronicBookUserInvite(userId, xe).subscribe(res => {
            //console.log("inviteId: ", res);
            //  location.href = 'electronicbook/invitedetail/' + xe, { ebookInviteId: res };
            location.href = 'electronicbook/invitedetail/' + xe + ';ebookInviteId=' + res;

          })
        }

        else if (statusid == 19) { //aof
          location.href = '/usercentralpolicy/' + xe + '/' + provinceId;
        }

        else if (statusid == 17) { //song
          location.href = '/electronicbook/provincedetail/' + xe;
        }
        else if (statusid == 18) { //song
          this.notificationService.getElectronicBookProvincialDepartment(provinceId, xe).subscribe(res => {
            //console.log("inviteId: ", res);
            //  location.href = '/electronicbook/departmentdetail/' + xe, { electronicBookProvincialDepartmentId: res };
            location.href = '/electronicbook/departmentdetail/' + xe + ';electronicBookProvincialDepartmentId=' + res;
          })
        }
        else if (statusid == 8) { //song
          location.href = '/electronicbook/detail/' + xe;
        }

        else if (statusid == 21) { //song
          location.href = '/answerpeople';
        }
        else if (statusid == 22) { //song
          location.href = '/answerpeople';
        }
        else if (statusid == 23) { //song
          location.href = '/answersubject/list/' + xe;
        }
        else if (statusid == 24) { //song
          location.href = '/answersubject/list/' + xe;
        }
        else if (statusid == 25) { //song
          location.href = '/answerrecommendationinspector/';
        }
        else if (statusid == 10 || statusid == 11) { //yochigang
          location.href = '/executiveorder';
        }
        else if (statusid == 12 || statusid == 13) { //yochigang
          location.href = '/requestorder';
        }

        // this.nav = superAdmin;
        // this.profileform();
        // this.getuserinfo();
        // this.getnotifications();
        // this.checkactive(this.nav[0].url);
      })
  }
  getUser: boolean = true
  //start getuser
  async getuserinfo() {
    // this.authorize.getUser()
    //   .subscribe(result => {
    this.authorize.getUser()
      .subscribe(result => {
        if (this.getUser) {
          this.getUser = false
          this.userid = result.sub
          this.userService.getuserfirstdata(this.userid)
            .subscribe(result => {

              this.resultuser = result;
              //console.log('dataxx', result);

              this.role_id = result[0].role_id
              this.Prefix = result[0].prefix
              this.Name = result[0].name
              this.FName = result[0].firstnameth
              this.LName = result[0].lastnameth
              this.Position = result[0].position
              this.PhoneNumber = result[0].phoneNumber
              this.Email = result[0].email
              this.Img = result[0].img
              this.Signature = result[0].signature
              this.UserName = result[0].userName

              this.Form.patchValue({
                Prefix: this.Prefix,
                FName: this.Name,
                LName: this.Name,
                Position: this.Position,
                PhoneNumber: this.PhoneNumber,
                Email: this.Email,
                Formprofile: 1,
                files: this.files,
                Img: this.Img,
                Signature: this.Signature,
                UserName: this.UserName
              });
              // this.nav
              this.menuService.getmenudata(this.role_id)
                .subscribe(result => {
                  let mock_menu_disable: any[] = []


                  // mock_menu_disable คือตัวแปรสำหรับแทนที่ดึงมาจาก service และดึง key ของแตค่ล่ะตัวที่ไม่ใช่ 0
                  for (const [key, value] of Object.entries(result)) {
                    let eiei = key
                    if (key != 'createdAt' && key != 'role_id' && key != 'id' && value != 0) {
                      //console.log('key: ' + key, value);
                      // mock_menu_disable.push({ [eiei]: value })
                      mock_menu_disable.push(eiei)

                    }
                  }
                 // console.log(mock_menu_disable);

                  //สำหรับฟิลเตอร์ nav
                  this.nav = this.arraynav.filter(function (item) {
                    return mock_menu_disable.indexOf(item.menuname) == -1;
                  })

                  this.nav = mock_menu_disable.map((item) => {
                    let obj = this.arraynav.find(x => x.menuname == item)
                    return obj

                  });
                  this.nav = _.orderBy(this.nav, ['orderby'], ['asc']);
                  // chars =
                  //console.log('  this.nav', this.nav);

                });

              // if (this.role_id == 1) {
              //   this.nav = superAdmin //ซุปเปอร์แอดมิน
              // } else if (this.role_id == 2) {
              //   this.nav = Centraladmin //แอดมินส่วนกลาง
              // } else if (this.role_id == 3) {
              //   this.nav = Inspector //ผู้ตรวจราชการ
              // } else if (this.role_id == 4) {
              //   this.nav = Provincialgovernor //ผู้ว่าราชการจังหวัด
              // } else if (this.role_id == 5) {
              //   this.nav = Adminprovince //หัวหน้าสำนักงานจังหวัด
              // } else if (this.role_id == 6) {
              //   this.nav = InspectorMinistry // ผู้ตรวจกระทรวง
              // } else if (this.role_id == 7) {
              //   this.nav = publicsector // ผู้ตรวจภาคประชาชน
              // } else if (this.role_id == 8) {
              //   this.nav = president // ผู้บริหาร หรือ นายก รองนายก
              // } else if (this.role_id == 9) {
              //   this.nav = InspectorExamination //หน่วยงานตรวจ
              // } else if (this.role_id == 10) {
              //   this.nav = InspectorDepartment // ผู้ตรวจกรม
              // } else if (this.role_id == 11) {
              //   this.nav = External // ภายนอก
              // }
              // this.bridge2.push(bridge)
            })
        }
      })
    // .toPromise()
    //   .then((res: any) => {
    //     console.log(res);
    //   })
    // const results = new Promise(function (resolve, reject) {
    //   setTimeout(function () {
    //     let data: any



    //     resolve(data);

    //   }, 1500);
    // });
    // results.then((result: any) => {

    // })
    //   })
    // //console.log('await', 'in1');
    // //console.log('await', result);




    // .subscribe(result => {


    // })
  }
  //End getuser
  //for
  editprofile(value) {
    this.userService.editprofile(value, this.Form.value.files, this.Form.value.files2, this.userid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      location.reload();
      this.getuserinfo();
    })
  }
  password(value) {

    this.submitted = true;
    if (this.Form.get('Password').invalid && this.Form.get('PassWordconfirm').invalid) {
      return;
    }

    if (value.Password == value.PassWordconfirm) {
      this.userService.changepassword(value, this.userid).subscribe(response => {
        this.Form.reset()
        this.modalRef.hide()
        location.reload();
        this.getuserinfo();
      })
    } else {
      alert("ยืนยันรหัสผ่านไม่ถูกต้อง");
    }
  }

  bridge(name) {
    // this.bridge2.filter(result => {
    //   return result.name == name
    // })[0]
    // console.log(this.bridge2.filter(result => {
    //   return result.name == name
    // })[0]);

    return this.bridge2.filter(result => {
      return result.name == name
    })[0]
  }
  get f() { return this.Form.controls; }
}


export interface Bridge {
  name: string, test: any
}
