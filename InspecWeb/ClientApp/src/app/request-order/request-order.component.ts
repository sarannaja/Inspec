import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { UserService } from 'src/app/services/user.service';
import { RequestorderrService } from '../services/requestorderr.service';

import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { IMyOptions } from 'mydatepicker-th';
import { NotificationService } from '../services/notification.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { LogService } from '../services/log.service';
import { NotofyService } from '../services/notofy.service';

@Component({
  selector: 'app-request-order',
  templateUrl: './request-order.component.html',
  styleUrls: ['./request-order.component.css']
})
export class RequestOrderComponent implements OnInit {

  public myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd/mm/yyyy',
  };
  resultrequestorder: any[] = []
  userRegion: any[] = []
  modalRef: BsModalRef;
  dtOptions: any = {};
  loading = false;
  userid: any;
  role_id: any;
  selectdatauser: Array<any>
  Form: FormGroup;
  cancelForm: FormGroup;
  awnserForm: FormGroup;
  gotitForm: FormGroup;
  requestorderid: any;
  answerdetail: any;
  answerProblem: any;
  answerCounsel: any;
  vcommanded_date: any;
  vcommanded_by: any;
  vsubject: any;
  vsubjectdetail: any;
  vstatus: any;
  vcreatedAt: any;
  vanswer_by: any;
  vanswerdetail: any;
  vanswerProblem: any;
  vanswerCounsel: any;
  vrequestorderFiles: any;
  vanswerrequestorderFiles: any;
  idrequestorder: any;
  url = ""
  resultrequestorderdetail: any[] = [];
  idrequestorderanswer: any;
  fileUrl: any;

  submitted = false;
  Subject: any;
  Subjectdetail: any;
  Answer_by: any;
  Commanded_date: any;
  Draft: any;

  Answerdetail: any;
  AnswerProblem: any;
  AnswerCounsel: any;

  delete_id: any;
  requestfile: any[];

  date: any = { date: { year: (new Date()).getFullYear(), month: (new Date()).getMonth() + 1, day: (new Date()).getDate() } };

  constructor(
    private authorize: AuthorizeService,
    private userService: UserService,
    private requestOrderService: RequestorderrService,
    private notificationService: NotificationService,
    private router: Router,
    private fb: FormBuilder,
    private modalService: BsModalService,
    private spinner: NgxSpinnerService,
    private logService: LogService,
    private _NotofyService: NotofyService,
    @Inject('BASE_URL') baseUrl: string) {
    this.fileUrl = baseUrl + '/requestfile';
  }

  ngOnInit() {
    this.getuserinfo();
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
    this.Form = this.fb.group({
      Commanded_by: this.userid,
      Subject: new FormControl(null, [Validators.required]),
      Subjectdetail: new FormControl(null, [Validators.required]),
      Answer_by: new FormControl(null, [Validators.required]),
      Commanded_date: new FormControl(null, [Validators.required]),
      Draft: new FormControl(null, [Validators.required]),
      files: new FormControl(null),
    })

    this.awnserForm = this.fb.group({
      Answerdetail: new FormControl(null, [Validators.required]),
      AnswerProblem: new FormControl(null, [Validators.required]),
      AnswerCounsel: new FormControl(null, [Validators.required]),
      files: new FormControl(null),

    })

    this.cancelForm = this.fb.group({
      "canceldetail": new FormControl(null),
    })

    this.gotitForm = this.fb.group({
    })


  }

  getuserinfo() {
    this.authorize.getUser()
      .subscribe(result => {
        // if(this.userid)
        this.userid = result.sub
        this.userService.getuserfirstdata(this.userid)
          .subscribe(result => {

            this.role_id = result[0].role_id
            this.userRegion = result[0].userRegion

            if (this.role_id == 4 || this.role_id == 5 || this.role_id == 9) { //แจ้งคำร้องขอ
              this.requestOrderService.getrequestordercommandeddata(this.userid)
                .subscribe(result => {
                  // console.log('data',result);            
                  this.getDatauser();
                  this.resultrequestorder = result;
                  this.loading = true;
                  this.spinner.hide();
                })
            } else if (this.role_id == 1) { //แอดมินใหญ่
              this.requestOrderService.getrequestorderdata()
                .subscribe(result => {
                  this.getDatauser();
                  this.resultrequestorder = result;
                  this.loading = true;
                  this.spinner.hide();
                })
            } else { // คนรับคำร้องขอ
              this.requestOrderService.getrequestorderanswereddata(this.userid)
                .subscribe(result => {
                  this.getDatauser();
                  this.resultrequestorder = result;
                  this.loading = true;
                  this.spinner.hide();
                })
            }

          })
      })
  }
  // <!-- select ข้อมูลคนรับคำร้องขอ -->
  getDatauser() {
    this.userService.getuserselectforexecutiveorderandrequestorder()
      .subscribe(result => {

        this.selectdatauser = result
          .filter((item, index) => {
            let data: any[] = []
            item.userRegion.map(res => data.push(res))
            //  console.log( )

            return data.filter(x => this.userRegion.every(y => x.regionId != y.regionId)).length == 0

          }).map(item => {
            return {
              value: item.id,
              label: item.prefix + ' ' + item.name + ' : ' + item.ministries.name + ' (' + item.position2 + ')'
            }
          })

      })
  }
  // <!-- END select ข้อมูลคนรับคำร้องขอ -->

  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.Form.patchValue({
      files: file
    });
    this.Form.get('files').updateValueAndValidity()

  }

  uploadFileanswer(event) {
    const file = (event.target as HTMLInputElement).files;
    this.awnserForm.patchValue({
      files: file
    });
    this.awnserForm.get('files').updateValueAndValidity()

  }

  openModal(template: TemplateRef<any>, id, title) {
    this.Form.reset()
    this.submitted = false;
    this.delete_id = id;
    this.vsubject = title;
    this.modalRef = this.modalService.show(template);
    this.Form.patchValue({
      Draft: 1
    })
  }

  // <!-- แก้ไขคำร้องขอ -->
  editmodalRequestOrder(template: TemplateRef<any>, id, commanded_date, subject, subjectdetail, draft, answer_by, filename) {
    this.Form.reset();
    this.submitted = false;
    this.Form.patchValue({
      Subject: subject,
      Subjectdetail: subjectdetail,
      Answer_by: answer_by.map(result => {
        return result.userID
      }),
      Draft: draft,
      Commanded_date: this.time(commanded_date)
    })
    this.idrequestorder = id;
    this.requestfile = filename;
    // this.vcommanded_date = commanded_date;
    // this.vsubject = subject;
    // this.vsubjectdetail = subjectdetail;
    // this.vanswer_by = answer_by;

    this.modalRef = this.modalService.show(template);
  }

  // <!-- เปิดโมดอลยกเลิก -->
  cancelmodal(template: TemplateRef<any>, id) {
    this.cancelForm.reset()
    this.idrequestorder = id;
    this.modalRef = this.modalService.show(template);
  }
  // <!-- เปิดโมดอลรับทราบ -->

  // <!-- เปิดโมดอลรับทราบ -->
  gotitmodal(template: TemplateRef<any>, id, idanswer, subject, subjectdetail) {
    this.requestorderid = id;
    this.idrequestorderanswer = idanswer;
    this.vsubject = subject;
    this.vsubjectdetail = subjectdetail;

    this.modalRef = this.modalService.show(template);
  }

  //<!-- setเวลา -->
  time(date) {
    var ssss = new Date(date)
    var new_date = { date: { year: ssss.getFullYear(), month: ssss.getMonth() + 1, day: ssss.getDate() } }
    return new_date
  }
  //<!-- END setเวลา -->


  //<!--เพิ่มข้อคำร้องขอ -->
  storerequestorder(value) {

    this.submitted = true;
    if (this.Form.invalid) {
      return;
    }

    this.requestOrderService.addrequestorder(value, this.Form.value.files, this.userid)
      .subscribe(result => {
        this.logService.addLog(this.userid, 'RequestOrders', 'เพิ่ม', result.title, result.id).subscribe();
        if (value.Draft == 1) {
          this.Form.reset();
          this.loading = false;
          this.getuserinfo();
          this._NotofyService.onSuccess("เพิ่มข้อมูล")
          this.modalRef.hide();
        } else {
          this.notificationService.addNotification(1, 1, 1, 12, result.id, result.title, this.userid)
            .subscribe(result => {
              this.Form.reset();
              this.loading = false;
              this.getuserinfo();
              this._NotofyService.onSuccess("เพิ่มข้อมูล")
              this.modalRef.hide();
            })
        }
      })
  }
  //<!-- END เพิ่มข้อคำร้องขอ -->

  //แก้ไข
  updaterequestorder(value) {

    this.submitted = true;
    if (this.Form.invalid) {
      return;
    }
    this.requestOrderService.updaterequestorder(value, this.Form.value.files, this.idrequestorder).subscribe(result => {
      this.logService.addLog(this.userid, 'RequestOrders', 'แก้ไข', result.title, result.id).subscribe();
      if (value.Draft == 1) {
        this.Form.reset();
        this.getuserinfo();
        this._NotofyService.onSuccess("แก้ไขข้อมูล")
        this.modalRef.hide();
      } else {
        this.notificationService.addNotification(1, 1, 1, 12, result.id, result.title, this.userid)
          .subscribe(result => {
            this.Form.reset();
            this.getuserinfo();
            this._NotofyService.onSuccess("แก้ไขข้อมูล")
            this.modalRef.hide();
          })
      }
    })
  }

  //<!-- ยกเลิกข้อสั่งการ -->
  cancelexecutiveorder(value) {
    this.requestOrderService.cancelrequestorder(value, this.idrequestorder).subscribe(result => {
      this.cancelForm.reset()
      this.loading = false;
      this.getuserinfo();
      this.modalRef.hide();
    })
  }
  //<!-- END ยกเลิกข้อสั่งการ -->


  //<!-- รายระเอียดข้อสั่งการ -->
  DetailRequestOrdermodal(template: TemplateRef<any>, id) {

    this.requestOrderService.getrequestorderdetaildata(id)
      .subscribe(result => {
        console.log('detail', result);
        this.resultrequestorderdetail = result;
      })
    this.modalRef = this.modalService.show(template);
  }
  //<!-- END รายระเอียดข้อสั่งการ -->

  //<!-- รับทราบข้อสั่งการ -->
  gotitrequestorder() {
    this.requestOrderService.gotitrequestorder(this.requestorderid, this.idrequestorderanswer).subscribe(result => {
      this.notificationService.addNotification(1, 1, 1, 13, this.requestorderid, this.vsubject, this.userid)
        .subscribe(result => {
          this.loading = false;
          this.getuserinfo();
          this.modalRef.hide();
        })
    })
  }
  //<!--END รับทราบข้อสั่งการ -->

  //<!-- modal รายงานผล -->
  answerModal(template: TemplateRef<any>, id) {
    this.awnserForm.reset()
    this.submitted = false;
    this.idrequestorderanswer = id;
    this.modalRef = this.modalService.show(template);
  }
  //<!-- END modal รายงานผล -->


  //<!-- รายงานผล -->
  storeanswerrequestorder(value) {
    this.submitted = true;
    if (this.awnserForm.invalid) {
      return;
    }
    this.requestOrderService.answerrequestorder(value, this.awnserForm.value.files, this.idrequestorderanswer)
      .subscribe(result => {
        this.awnserForm.reset();
        this.loading = false;
        this.getuserinfo()
        this._NotofyService.onSuccess("รายงานผล")
        this.modalRef.hide();
      })
  }
  //<!-- END รายงานผล -->

  deleterequestorder(id) {
    this.requestOrderService.deleterequestorder(id).subscribe(result => {
      this.logService.addLog(this.userid, 'reportrequestorder', 'ลบ', this.vsubject, id).subscribe();
      this.loading = false;
      this.getuserinfo();
      this._NotofyService.onSuccess("ลบข้อมูล")
      this.modalRef.hide();
    })
  }

  exportexecutive2(id, userId) {
    this.requestOrderService.getrequest2(id, userId)
      .subscribe(result => {
        window.open(this.url + "reportrequestorder/" + result.data);
      })
    this.getuserinfo();
  }
  get f() { return this.Form.controls; }
  get g() { return this.awnserForm.controls; }
}