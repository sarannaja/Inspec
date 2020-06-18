import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { CentralpolicyService } from '../services/centralpolicy.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { IOption } from 'ng-select';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from 'src/app/services/user.service';
import { ExecutiveorderService } from '../services/executiveorder.service';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { IMyOptions } from 'mydatepicker-th';
import { NotificationService } from '../services/notification.service';

@Component({
  selector: 'app-executive-order',
  templateUrl: './executive-order.component.html',
  styleUrls: ['./executive-order.component.css']
})
export class ExecutiveOrderComponent implements OnInit {
  private myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd/mm/yyyy',
  };
  resultexecutiveorder: any[] = []
  modalRef: BsModalRef;
  dtOptions: DataTables.Settings = {};
  loading = false;
  userid: any;
  role_id: any;
  executive1data: any = [];
  Form: FormGroup;
  awnserForm: FormGroup;
  selectdatauser: Array<IOption>
  files: string[] = [];
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
  imgprofileUrl: any;
  idexecutiveorder: any;
  answerdetail: any;
  answerProblem: any;
  answerCounsel: any;
  testUser: any;
  constructor(
    private authorize: AuthorizeService,
    private userService: UserService,
    private router: Router,
    private fb: FormBuilder,
    private modalService: BsModalService,
    private executiveorderService: ExecutiveorderService,
    private notificationService: NotificationService,
    @Inject('BASE_URL') baseUrl: string) {
    this.imgprofileUrl = baseUrl + '/executivefile';
  }

  ngOnInit() {

    this.dtOptions = {
      pagingType: 'full_numbers',
    };

    this.getuserinfo();


    this.Form = this.fb.group({
      "Commanded_by": this.userid,
      "Subject": new FormControl(null, [Validators.required]),
      "Subjectdetail": new FormControl(null, [Validators.required]),
      "Answer_by": new FormControl(null, [Validators.required]),
      "Commanded_date": new FormControl(null, [Validators.required]),
      files: new FormControl(null, [Validators.required]),
    })

    this.awnserForm = this.fb.group({
      "Answerdetail": new FormControl(null, [Validators.required]),
      "AnswerProblem": new FormControl(null, [Validators.required]),
      "AnswerCounsel": new FormControl(null, [Validators.required]),
      files: new FormControl(null, [Validators.required]),

    })
  }

  //start getuser
  getuserinfo() {
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        this.userService.getuserfirstdata(this.userid)
          .subscribe(result => {
            this.role_id = result[0].role_id
            if (this.role_id == 8) {
              this.executiveorderService.getexecutiveordercommandeddata(this.userid)
                .subscribe(result => {
                  this.getDatauser();
                  this.getUserServiceLoop(result)

                })
            } else if (this.role_id == 1) {
              this.executiveorderService.getexecutiveorderdata()
                .subscribe(result => {
                  this.getDatauser();
                  this.getUserServiceLoop(result)
                })
            } else {
              this.executiveorderService.getexecutiveorderanswereddata(this.userid)
                .subscribe(result => {
                  this.getDatauser();
                  this.getUserServiceLoop(result)
                })
            }
          })
      })
  }
  //End getuser

  getDatauser() {
    this.userService.getuserdata(3)
      .subscribe(result => {
        this.selectdatauser = result.map((item, index) => {
          return { value: item.id, label: item.prefix + ' ' + item.name }
        })

      })
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

  storedetailexecutiveorder(value) {
    this.executiveorderService.addexecutiveorder(value, this.Form.value.files).subscribe(result => {
      //alert(1);
      this.notificationService.addNotification(1, 1, result.answer_by, 10, result.id)
        .subscribe(result => {
         // alert(2);
        
          this.Form.reset();
          this.getuserinfo();
        })
        this.modalRef.hide();
    })
  }
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }
  DetailExecutiveOrder(template: TemplateRef<any>,
    commanded_date,
    commanded_by,
    subject,
    subjectdetail,
    status,
    createdAt,
    answer_by,
    answerdetail,
    answerProblem,
    answerCounsel,
    executiveOrderFiles,
    answerExecutiveOrderFiles) {


    this.vcommanded_date = commanded_date;
    this.vcommanded_by = commanded_by;
    this.vsubject = subject;
    this.vsubjectdetail = subjectdetail;
    this.vstatus = status;
    this.vcreatedAt = createdAt;
    this.vanswer_by = answer_by;
    this.vanswerdetail = answerdetail;
    this.vanswerProblem = answerProblem;
    this.vanswerCounsel = answerCounsel;
    this.vexecutiveOrderFiles = executiveOrderFiles;
    this.vanswerExecutiveOrderFiles = answerExecutiveOrderFiles;

    this.modalRef = this.modalService.show(template);
  }

  answerModal(template: TemplateRef<any>, id, answerdetail, answerProblem, answerCounsel) {
    this.idexecutiveorder = id;
    this.answerdetail = answerdetail;
    this.answerProblem = answerProblem;
    this.answerCounsel = answerCounsel;
    this.awnserForm.patchValue({
      "Answerdetail": answerdetail,
      "AnswerProblem": answerProblem,
      "AnswerCounsel": answerCounsel,
    });
    this.modalRef = this.modalService.show(template);
  }


  storeanswerexecutiveorder(value) {

    this.executiveorderService.answerexecutiveorder(value, this.awnserForm.value.files, this.idexecutiveorder)
      .subscribe(result => {

        // alert(result.id);
        // this.notificationService.addNotification(this.id, this.provincefornotirole3, 1, 11, result.id)
        //   .subscribe(result => {
        //   })
        this.modalRef.hide();
        this.getuserinfo()
        this.awnserForm.reset();

      })

  }
  getUserServiceLoop(array): void {
    var resultexecutiveorder: any[] = []
    array.forEach(element => {
      setTimeout(async () => {
        await this.userService.getuserfirstdata(element.answer_by)
          .subscribe(async result => {

            await this.userService.getuserfirstdata(element.commanded_by)
              .subscribe(resultCom => {
                resultexecutiveorder.push({ ...element, userans: result[0], usercom: resultCom[0] })
              })

          })
      }, 100)

    });
    setTimeout(async () => {
      this.resultexecutiveorder = await resultexecutiveorder
      // .sort(function (a, b) {
      //   // console.log(a);
      //   return a.id - b.id
      // });

      //console.log(this.resultexecutiveorder);


    }, 150)

    setTimeout(() => {


      //console.log(this.resultexecutiveorder);

      this.loading = true
    }, 600)
  }
}
