import { Router } from '@angular/router';
import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { superAdmin, Centraladmin, Inspector, Provincialgovernor, Adminprovince, InspectorMinistry, publicsector, president, InspectorDepartment, InspectorExamination } from './_nav';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from 'src/app/services/user.service';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { NotificationService } from 'src/app/services/notification.service';

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
  toggled = false;
  classIcon = "align-middle mr-2 fas fa-fw "
  urlActive = ""
  classtap = 'sidebar-header'
  userid: any
  role_id: any
  nav: any
  imgprofileUrl: any;
  resultuser: any[];
  resultfirstuser: any[] = [];
  modalRef: BsModalRef;
  Form: FormGroup;
  Prefix: any;
  Name: any;
  Position: any;
  PhoneNumber: any;
  Email: any;
  files: any;
  Img: any;
  Formprofile: any;
  resultnotifications: any[] = [];
  resultnotificationscount: any[] = [];
  // childClassIcon = "align-middle mr-2 fas fa-fw
  bridge2: Array<Bridge>
  isMobile: boolean = false;
  width: number = window.innerWidth;
  height: number = window.innerHeight;
  mobileWidth: number = 900;
  constructor(
    private authorize: AuthorizeService,
    private userService: UserService,
    private notificationService: NotificationService,
    private router: Router,
    private modalService: BsModalService,
    private fb: FormBuilder,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.imgprofileUrl = baseUrl + '/imgprofile';
  }
  // 0C-54-15-66-C2-D6


  onToggle() {
    this.toggled = !this.toggled;
  }
  ngOnInit() {
    this.nav = superAdmin;
    this.profileform();
    this.getuserinfo();
    this.getnotifications();
    // this.getplancount();
    this.checkactive(this.nav[0].url);
    this.isMobile = this.width < this.mobileWidth;
    // this.urlActive = this.nav[0].url
  }
  onWindowResize(event) {
    this.width = event.target.innerWidth;
    this.height = event.target.innerHeight;
    this.isMobile = this.width < this.mobileWidth;
    console.log(this.width);

  }
  checkactive(url) {
    this.urlActive = url
    if(this.isMobile){
      this.toggled = !this.toggled;

    }
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
    this.modalRef = this.modalService.show(template);
  }

  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.Form.patchValue({
      files: file
    });
    this.Form.get('files').updateValueAndValidity()
  }

  profileform() {
    this.Form = this.fb.group({
      Prefix: new FormControl(null, [Validators.required]),
      Name: new FormControl(null, [Validators.required]),
      Position: new FormControl(null, [Validators.required]),
      PhoneNumber: new FormControl(null, [Validators.required]),
      Email: new FormControl(null, [Validators.required]),
      files: new FormControl(null, [Validators.required]),
      Formprofile: 1,
    })
  }

  getnotifications() {
    this.notificationService.getnotificationsdata(this.userid)
      .subscribe(result => {
        this.resultnotifications = result;
      })

    this.notificationService.getnotificationscountdata(this.userid)
      .subscribe(result => {
        this.resultnotificationscount = result;
      })
  }

  detailnotifications(id) {
    this.notificationService.updateNotification(id)
      .subscribe(result => {
        this.nav = superAdmin;
        this.profileform();
        this.getuserinfo();
        this.getnotifications();
        this.checkactive(this.nav[0].url);
      })
  }

  //start getuser
  getuserinfo() {
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        this.userService.getuserfirstdata(this.userid)
          .subscribe(result => {
            this.resultuser = result;

            this.role_id = result[0].role_id
            this.Prefix = result[0].prefix
            this.Name = result[0].name
            this.Position = result[0].position
            this.PhoneNumber = result[0].phoneNumber
            this.Email = result[0].email
            this.Img = result[0].img

            this.Form.patchValue({
              Prefix: this.Prefix,
              Name: this.Name,
              Position: this.Position,
              PhoneNumber: this.PhoneNumber,
              Email: this.Email,
              Formprofile: 1,
              files: this.files,
            });

            if (this.role_id == 1) {
              this.nav = superAdmin //ซุปเปอร์แอดมิน
            } else if (this.role_id == 2) {
              this.nav = Centraladmin //แอดมินส่วนกลาง
            } else if (this.role_id == 3) {
              this.nav = Inspector //ผู้ตรวจราชการ
            } else if (this.role_id == 4) {
              this.nav = Provincialgovernor //ผู้ว่าราชการจังหวัด
            } else if (this.role_id == 5) {
              this.nav = Adminprovince //หัวหน้าสำนักงานจังหวัด
            } else if (this.role_id == 6) {
              this.nav = InspectorMinistry // ผู้ตรวจกระทรวง
            } else if (this.role_id == 7) {
              this.nav = publicsector // ผู้ตรวจภาคประชาชน
            } else if (this.role_id == 8) {
              this.nav = president // ผู้บริหาร หรือ นายก รองนายก
            } else if (this.role_id == 9) {
              this.nav = InspectorExamination //หน่วยงานตรวจ
            } else if (this.role_id == 10) {
              this.nav = InspectorDepartment // ผู้ตรวจกรม
            }
            // this.bridge2.push(bridge)

            console.log(


            );
          })
      })
  }
  //End getuser
  //for
  editprofile(value) {
    this.userService.editprofile(value, this.Form.value.files, this.userid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.getuserinfo();
    })
  }

  bridge(name) {
    // this.bridge2.filter(result => {
    //   return result.name == name
    // })[0]
    console.log(this.bridge2.filter(result => {
      return result.name == name
    })[0]);

    return this.bridge2.filter(result => {
      return result.name == name
    })[0]
  }
}


export interface Bridge {
  name: string, test: any
}
