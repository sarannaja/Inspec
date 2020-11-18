import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { CentralpolicyService } from '../services/centralpolicy.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from 'src/app/services/user.service';
import { ExecutiveorderService } from '../services/executiveorder.service';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { IMyOptions } from 'mydatepicker-th';
import { NotificationService } from '../services/notification.service';
import { Executiveordercommanded, ExecutiveOrderAnswer } from '../models/excucommand';
import { NotofyService } from '../services/notofy.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { LogService } from '../services/log.service';

@Component({
  selector: 'app-executive-order',
  templateUrl: './executive-order.component.html',
  styleUrls: ['./executive-order.component.css']
})
export class ExecutiveOrderComponent implements OnInit {
  public myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd/mm/yyyy',
  };
  datas$
  resultexecutiveorder: Executiveordercommanded[] = []
  executiveOrderAnswers:ExecutiveOrderAnswer[] = []
  resultexecutiveorderdetail:any=[];
  modalRef: BsModalRef;
  dtOptions: DataTables.Settings = {};
  loading = false;
  userid: any;
  role_id: any;
  executive1data: any = [];
  Form: FormGroup;
  awnserForm: FormGroup;
  cancelForm:FormGroup;
  gotitForm:FormGroup;
  selectdatauser: Array<any>
  files: string[] = [];
  vid : any;
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
  vexecutiveOrderFiles: any;
  vanswerExecutiveOrderFiles: any;
  fileUrl: any;
  idexecutiveorder: any;
  idexecutiveorderanswer :any;
  // answerdetail: any;
  // answerProblem: any;
  // answerCounsel: any;
  testUser: any;
  url = "";
  Subject :any;
  Subjectdetail :any;
  Answer_by :any;
  Commanded_date :any;
  Draft :any;
  submitted = false;
  delete_id:any;
  Answerdetail: any;
  AnswerProblem: any;
  AnswerCounsel: any;
  executivefile :any[];
  date: any = { date: {year: (new Date()).getFullYear(), month: (new Date()).getMonth() + 1, day: (new Date()).getDate()} };

  constructor(
    private authorize: AuthorizeService,
    private userService: UserService,
    private router: Router,
    private fb: FormBuilder,
    private modalService: BsModalService,
    private executiveorderService: ExecutiveorderService,
    private notificationService: NotificationService,
    private _NotofyService: NotofyService,
    private spinner: NgxSpinnerService,
    private logService: LogService,
    @Inject('BASE_URL') baseUrl: string) {
    this.fileUrl = baseUrl + '/executivefile';
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

  //start getuser
  getuserinfo() {
    this.spinner.show();
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        this.userService.getuserfirstdata(this.userid)
          .subscribe(result => {
            this.role_id = result[0].role_id
            if (this.role_id == 8) {
              this.executiveorderService.getexecutiveordercommandeddata(this.userid)
                .subscribe(result => {
                  console.log("data",result);
                  this.getDatauser();
                  this.resultexecutiveorder = result;
                  this.loading = true;
                  this.spinner.hide();              
                })
            } else if (this.role_id == 1) {
              this.executiveorderService.getexecutiveorderdata()
                .subscribe(result => {
                //  console.log("getexecutiveorderdata",result);
                  this.getDatauser();
                  this.resultexecutiveorder = result;
                  this.loading = true;
                  this.spinner.hide();
                
                })
            } else {
              this.executiveorderService.getexecutiveorderanswereddata(this.userid)
                .subscribe(result => {
                 // console.log("data3",result);
                  this.getDatauser();
                  this.resultexecutiveorder = result;
                  this.loading = true;
                  this.spinner.hide();
               
                })
            }
          })
      })
  }
  //End getuser

  getDatauser() {
    this.userService.getuserdata(3)
      .subscribe(result => {
        console.log('data', result);
        this.selectdatauser = result.map((item, index) => {
          return { value: item.id, label: item.prefix+item.name + ' : '+ item.ministries.name }
        })

      })
  }

  // <!-- เปิดโมดอลยกเลิก -->
  cancelmodal(template: TemplateRef<any>, id){
    this.cancelForm.reset()
    this.idexecutiveorder = id;
    this.modalRef = this.modalService.show(template);
   }
   // <!-- เปิดโมดอลรับทราบ -->
  gotitmodal(template: TemplateRef<any>, id,idanswer,subject,subjectdetail){
  this.idexecutiveorder = id;
  this.idexecutiveorderanswer = idanswer;
  this.vsubject = subject;
  this.vsubjectdetail = subjectdetail;

  this.modalRef = this.modalService.show(template);
  }

  openModal(template: TemplateRef<any>,id,title) {
    this.Form.reset()
    this.submitted = false;
    this.delete_id = id;
    this.vsubject = title;
    this.modalRef = this.modalService.show(template);
    this.Form.patchValue({
      Draft:1
    })
  }

  // <!-- เปิดโมดอลแก้ไข -->
  editExecutiveOrder(template: TemplateRef<any>,id,commanded_date,subject,subjectdetail,draft,answer_by,executiveOrderFiles) {
    //this.Form.reset();
    this.submitted = false; 
    this.Form.patchValue({
      Subject : subject,
      Subjectdetail:subjectdetail,
      Answer_by:answer_by.map(result=>{
        return result.userID
      }),
      Draft:draft,
      Commanded_date:this.time(commanded_date)
    })
     this.idexecutiveorder = id; 
     this.executivefile = executiveOrderFiles;
    // this.vcommanded_date = commanded_date;
    // this.vsubject = subject;
    // this.vsubjectdetail = subjectdetail;
    // this.vanswer_by = answer_by;

    this.modalRef = this.modalService.show(template);
  }

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

  //เพิ่มข้อสั่งการ
  storeexecutiveorder(value) {
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }
    this.executiveorderService.addexecutiveorder(value, this.Form.value.files,this.userid)
      .subscribe(result => 
    {
      this.logService.addLog(this.userid,'ExecutiveOrders','เพิ่ม',result.title,result.id).subscribe();
      if(value.Draft == 1){
        this.Form.reset();
        this.loading = false;
        this.getuserinfo();
        this._NotofyService.onSuccess("เพิ่มข้อมูล")
        this.modalRef.hide();
      }else{
        this.notificationService.addNotification(1, 1, 1, 10, result.id,result.title,this.userid)
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

  //แก้ไขข้อสั่งการ
  updateexecutiveorder(value) {
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }
     this.executiveorderService.updateexecutiveorder(value, this.Form.value.files,this.idexecutiveorder).subscribe(result => {
      this.logService.addLog(this.userid,'ExecutiveOrders','แก้ไข',result.title,result.id).subscribe();
        if(value.Draft == 1){
          this.Form.reset();
          this.getuserinfo();
          this._NotofyService.onSuccess("แก้ไขข้อมูล")
          this.modalRef.hide();
        }else{
          this.notificationService.addNotification(1, 1, 1, 10, result.id,result.title,this.userid)
           .subscribe(result => {
            this.Form.reset();
            this.getuserinfo();
            this._NotofyService.onSuccess("แก้ไขข้อมูล")
            this.modalRef.hide();
         })
        }       
     })
   }

  time(date) {
    var ssss = new Date(date)
    var new_date = {date:{ year: ssss.getFullYear(), month: ssss.getMonth() + 1, day: ssss.getDate() }}
    return new_date
  }
  DetailExecutiveOrder(template: TemplateRef<any>,id) {

      this.executiveorderService.getexecutiveorderdetaildata(id)
        .subscribe(result => {
          console.log('detail',result);
          this.resultexecutiveorderdetail = result;
        })

   
    this.modalRef = this.modalService.show(template);
  }

  //<!-- modal รายงานผล -->
  answerModal(template: TemplateRef<any>, id) {
    this.awnserForm.reset()
    this.submitted = false; 
    this.idexecutiveorderanswer = id;
    this.modalRef = this.modalService.show(template);
  }
  //<!-- END modal รายงานผล -->


  //<!-- รายงานผล -->
  storeanswerexecutiveorder(value) {
    this.submitted = true;
    if (this.awnserForm.invalid) {
        return;
    }
    this.executiveorderService.answerexecutiveorder(value, this.awnserForm.value.files, this.idexecutiveorderanswer)
      .subscribe(result => {
        this.awnserForm.reset();
        this.loading = false;
        this.getuserinfo()
        this._NotofyService.onSuccess("รายงานผล")
        this.modalRef.hide();   
      })
  }
  //<!-- END รายงานผล -->


  exportexecutive2(id,userId) {
    this.executiveorderService.getexcutive2(id,userId)
      .subscribe(result => {
       // console.log('result', result);
        window.open(this.url + "reportexecutive/" + result.data);
      })
    this.getuserinfo();
  }

  //<!-- ยกเลิกข้อสั่งการ -->
  cancelexecutiveorder(value){
    this.executiveorderService.cancelexecutiveorder(value,this.idexecutiveorder).subscribe(result => {              
        this.cancelForm.reset()
        this.loading = false;
        this.getuserinfo();
        this.modalRef.hide();
    })
  }
  //<!-- END ยกเลิกข้อสั่งการ -->

  //<!-- รับทราบข้อสั่งการ -->
  gotitexecutiveorder(){  
    this.executiveorderService.gotitexecutiveorder(this.idexecutiveorder,this.idexecutiveorderanswer).subscribe(result => {
    
      this.notificationService.addNotification(1, 1, 1, 11, this.idexecutiveorder,this.vsubject,this.userid)
      .subscribe(result => {
       this.loading = false;
       this.getuserinfo();
       this.modalRef.hide();
      })
   })
  }
  //<!--END รับทราบข้อสั่งการ -->

  //<!-- ลบข้อสั่งการ -->
  deleteexecutiveorder(id){
    this.executiveorderService.deleteexecutiveorder(id).subscribe(result => {
      this.logService.addLog(this.userid,'ExecutiveOrders','ลบ',this.vsubject,id).subscribe();
      this.loading = false;
      this.getuserinfo();
      this._NotofyService.onSuccess("ลบข้อมูล")
      this.modalRef.hide();
   })
  }
   //<!-- END ลบข้อสั่งการ -->
  get f() { return this.Form.controls; }
  get g() { return this.awnserForm.controls; }
}
