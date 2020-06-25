import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from 'src/app/services/user.service';
import { RequestorderrService } from '../services/requestorderr.service';
import { IOption } from 'ng-select';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { IMyOptions } from 'mydatepicker-th';

@Component({
  selector: 'app-request-order',
  templateUrl: './request-order.component.html',
  styleUrls: ['./request-order.component.css']
})
export class RequestOrderComponent implements OnInit {
  
  private myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd/mm/yyyy',
  };
  resultrequestorder: any[] = []
  modalRef: BsModalRef;
  dtOptions: DataTables.Settings = {};
  loading = false;
  userid: any;
  role_id:any;
  selectdatauser: Array<IOption>
  Form: FormGroup;
  awnserForm:FormGroup;
  requestorderid :any ;
  answerdetail : any;
  answerProblem : any;
  answerCounsel : any;
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
  constructor(
    private authorize: AuthorizeService,
    private userService: UserService,
    private requestOrderService: RequestorderrService,
    private router:Router,
    private fb: FormBuilder, 
    private modalService: BsModalService,
    @Inject('BASE_URL') baseUrl: string) { }

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

  getuserinfo(){
    this.authorize.getUser()
    .subscribe(result => {
      this.userid = result.sub  
      this.userService.getuserfirstdata(this.userid)      
      .subscribe(result => {  
      
        this.role_id = result[0].role_id

        if (this.role_id == 5) { //แจ้งคำร้องขอ
          this.requestOrderService.getrequestordercommandeddata(this.userid)
            .subscribe(result => {
              console.log('momomo',result);
              this.getDatauser();
              this.getUserServiceLoop(result)

            })
        } else if (this.role_id == 1) { //แอดมินใหญ่
          this.requestOrderService.getrequestorderdata()
            .subscribe(result => {
              this.getDatauser();
              this.getUserServiceLoop(result)
            })
        } else { // คนรับคำร้องขอ
          this.requestOrderService.getrequestorderanswereddata(this.userid)
            .subscribe(result => {
              this.getDatauser();
              this.getUserServiceLoop(result)
            })
        }
        
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

  storerequestorde(value) {
    this.requestOrderService.addrequestorder(value, this.Form.value.files).subscribe(result => {
      //alert(1);
      // this.notificationService.addNotification(1, 1, result.answer_by, 10, result.id)
      //   .subscribe(result => {
         // alert(2);
          this.modalRef.hide();
          this.Form.reset();
          this.getuserinfo();
        //})
    })
  }
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }


  getDatauser() {
    this.userService.getuserdata(3)
      .subscribe(result => {
        this.selectdatauser = result.map((item, index) => {
          return { value: item.id, label: item.prefix + ' ' + item.name }
        })

      })
  }

  answerModal(template: TemplateRef<any>, id, answerdetail, answerProblem, answerCounsel) {
     this.requestorderid = id;
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

  
  storeanswere(value) {
   // alert(1);
    this.requestOrderService.answerrequestorder(value, this.awnserForm.value.files, this.requestorderid)
      .subscribe(result => {
      //  alert(3);
        // alert(result.id);
        // this.notificationService.addNotification(this.id, this.provincefornotirole3, 1, 11, result.id)
        //   .subscribe(result => {
        //   })
        this.modalRef.hide();
        this.getuserinfo()
        this.awnserForm.reset();

      })

  }

  Detailrequestorderforclick(template: TemplateRef<any>,
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
    requestorderFiles,
    answerrequestorderFiles) {


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
    this.vrequestorderFiles = requestorderFiles;
    this.vanswerrequestorderFiles = answerrequestorderFiles;

    this.modalRef = this.modalService.show(template);
  }

  getUserServiceLoop(array): void {
    var resultrequestorder: any[] = []
    array.forEach(element => {
      setTimeout(async () => {
        await this.userService.getuserfirstdata(element.answer_by)
          .subscribe(async result => {
            await this.userService.getuserfirstdata(element.commanded_by)
              .subscribe(resultCom => {
                resultrequestorder.push({ ...element, userans: result[0], usercom: resultCom[0] })
              })

          })
      }, 100)

    });
    setTimeout(async () => {
      this.resultrequestorder = await resultrequestorder
    }, 150)

    setTimeout(() => {
      this.loading = true
    }, 600)
  }
}