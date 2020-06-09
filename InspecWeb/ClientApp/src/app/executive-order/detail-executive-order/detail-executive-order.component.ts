import { CentralpolicyService } from './../../services/centralpolicy.service';
import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { DetailexecutiveorderService } from 'src/app/services/detailexecutiveorder.service';
import { ProvinceService } from 'src/app/services/province.service';
import { IOption } from 'ng-select';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from 'src/app/services/user.service';
import { InspectionplanService } from 'src/app/services/inspectionplan.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-detail-executive-order',
  templateUrl: './detail-executive-order.component.html',
  styleUrls: ['./detail-executive-order.component.css']
})
export class DetailExecutiveOrderComponent implements OnInit {

  modalRef: BsModalRef;
  id: any
  text: any
  centralpolicy: any
  detailexecutiveorder: any
  answerdetail: any
  answerproblem: any
  answercounsel: any
  centralpolicyid: any
  provinceid: any
  resultexecutiveorder: any = []
  resultdetailexecutiveorder: any = []
  resultprovince: any = []
  file: string[] = []
  answerfile: string[] = []
  Form: FormGroup
  provinceId: any;
  selectdataprovince: Array<IOption>
  EditForm: FormGroup;
  loading = false;
  dtOptions: DataTables.Settings = {};
  idAnswer: Number;
  userid: any;
  role_id: any;
  executive_id: any;
  idview: any;
  centralpolicyprovinceid: any;
  provincefornotirole3: any;
  createdAt: any;
  UsercreateName: any;
  executivefile: any;
  imgprofileUrl: any;

  constructor(
    private authorize: AuthorizeService,
    private userService: UserService,
    private notificationService: NotificationService,
    private modalService: BsModalService,
    private detailexecutiveorderService: DetailexecutiveorderService,
    private activatedRoute: ActivatedRoute,
    private provinceservice: ProvinceService,
    private fb: FormBuilder,
    private router: Router,
    private inspectionplanservice: InspectionplanService,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.Form = this.fb.group({
      name: [''],
      files: [null]
    })
    this.imgprofileUrl = baseUrl + '/executivefile';

  }

  ngOnInit() {
    this.getuserinfo();
    this.getProvine();

    this.Form = this.fb.group({
      "byuserid": new FormControl(null, [Validators.required]),
      "name": new FormControl(null, [Validators.required]),
      "AnswerDetail": new FormControl(null, [Validators.required]),
      "AnswerProblem": new FormControl(null, [Validators.required]),
      "AnswerCounsel": new FormControl(null, [Validators.required]),
      provinceId: new FormControl(null, [Validators.required]),
      files: new FormControl(null, [Validators.required]),
      files2: new FormControl(null, [Validators.required]),
    })

  }
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }
  editModal(template: TemplateRef<any>, id) {
    this.idAnswer = id;
    this.modalRef = this.modalService.show(template);
  }
  viewModal(template: TemplateRef<any>, id, date, userid, text, text2, file, name, name2, name3, file2) {

    this.userService.getuserfirstdata(userid)
      .subscribe(result => {

        this.UsercreateName = result[0].name

      })
    this.idview = id;
    this.createdAt = date;
    this.centralpolicy = text
    this.text = text2;
    this.executivefile = file
    this.answerdetail = name;
    this.answerproblem = name2;
    this.answercounsel = name3;
    this.answerfile = file2

    this.modalRef = this.modalService.show(template);
    // this.getviewExecutiveOrder(id);

  }
  getDetailExecutiveOrder() {
    this.detailexecutiveorderService.getdetailexecutiveorderdata(this.id)
      .subscribe(result => {
        this.resultdetailexecutiveorder = result
        // console.log(this.resultdetailexecutiveorder);
        this.loading = true
      })
    this.getDetailExecutiveOrder()
  }

  getProvine() {
    this.detailexecutiveorderService.getprovince(this.id)
      .subscribe(result => {
        this.resultprovince = result
        //console.log("in1", this.resultprovince);

        this.selectdataprovince = this.resultprovince.map((item, index) => {
          return { value: item.province.id, label: item.province.name }
        })
        //console.log("in2", this.selectdataprovince);
      })
  }
  addModal(template: TemplateRef<any>, id, name) {
    this.id = id;
    this.text = name;
    this.centralpolicyid = id;
    this.provinceid = id;
    this.modalRef = this.modalService.show(template);
    this.EditForm = this.fb.group({
      "detailexecutiveordername": new FormControl(null, [Validators.required]),
    })
    this.EditForm.patchValue({
      "detailexecutiveordername": name
    })
  }

  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.Form.patchValue({
      files: file
    });
    this.Form.get('files').updateValueAndValidity()
  }
  storedetailexecutiveorder(value) {

    this.detailexecutiveorderService.adddetailexecutiveorder(value, this.Form.value.files, this.id)
      .subscribe(result => {

        this.executive_id = result.id;

        this.notificationService.addNotification(this.id, value.provinceId, this.userid, 10, this.executive_id)
          .subscribe(result => {
          })

        this.modalRef.hide();
        this.Form.reset();
        this.getuserinfo()
        this.getProvine()
        
      })

  }
  answerModal(template: TemplateRef<any>, name, id) {
    this.id = id;
    this.answerdetail = name;
    this.answerproblem = name;
    this.answercounsel = name;
    this.modalRef = this.modalService.show(template);
    this.EditForm = this.fb.group({
      "detailexecutiveordername": new FormControl(null, [Validators.required]),
    })
    this.EditForm.patchValue({
      "detailexecutiveordername": name
    })
  }
  uploadFile2(event) {
    //console.log("event", event);

    const answerfile = (event.target as HTMLInputElement).files;
    this.Form.patchValue({
      files: answerfile
    });
    this.Form.get('files2').updateValueAndValidity()
  }
  storeanswerexecutiveorder(value) {

    this.detailexecutiveorderService.answerexecutiveorder(value, this.Form.value.files, this.idAnswer)
      .subscribe(result => {

        // alert(result.id);
        this.notificationService.addNotification(this.id, this.provincefornotirole3, 1, 11, result.id)
          .subscribe(result => {
          })
        this.modalRef.hide();
        this.getuserinfo()
        this.Form.reset();
        //console.log("user",this.userid);
        
      })

  }

  getuserinfo() {
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        this.Form.patchValue({
          byuserid: this.userid
        })
        this.userService.getuserfirstdata(this.userid)
          .subscribe(result => {
            this.role_id = result[0].role_id

            if (this.role_id != 3) {
              this.detailexecutiveorderService.getdetailexecutiveorderdata(this.id)
                .subscribe(result => {
                  this.resultdetailexecutiveorder = result
                  // console.log("test: ", this.resultdetailexecutiveorder);
                  this.loading = true
                })
            } else {
              this.detailexecutiveorderService.getdetailexecutiveorderdatarole3(this.id, this.userid)
                .subscribe(result => {
                  this.resultdetailexecutiveorder = result
                  this.provincefornotirole3 = result[0].provinceId
                  // console.log('momo : ',this.resultdetailexecutiveorder);
                  this.loading = true
                })
            }
          })
      })
  }
  //End getuser

  DetailCentralPolicy(id: any, provinceid) {
    this.inspectionplanservice.getcentralpolicyprovinceid(id, provinceid).subscribe(result => {
      this.centralpolicyprovinceid = result
      this.router.navigate(['/centralpolicy/detailcentralpolicyprovince', result])
    })

  }

}