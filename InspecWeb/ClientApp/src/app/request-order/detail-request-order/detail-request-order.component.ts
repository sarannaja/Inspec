import { CentralpolicyService } from './../../services/centralpolicy.service';
import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { ProvinceService } from 'src/app/services/province.service';
import { IOption } from 'ng-select';
import { RequestorderService } from 'src/app/services/requestorder.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from 'src/app/services/user.service';
import { InspectionplanService } from 'src/app/services/inspectionplan.service';
import { NotificationService } from 'src/app/services/notification.service';
import { RequestorderrService } from 'src/app/services/requestorderr.service';

@Component({
  selector: 'app-detail-request-order',
  templateUrl: './detail-request-order.component.html',
  styleUrls: ['./detail-request-order.component.css']
})
export class DetailRequestOrderComponent implements OnInit {

  modalRef: BsModalRef;
  id: any
  text: any
  centralpolicy: any;
  answerdetail: any
  answerproblem: any
  answercounsel: any
  centralpolicyid: any
  provinceid: any
  resultrequestorder: any = []
  resultdetailrequestorder: any = []
  resultprovince: any = []
  file: string[] = []
  Form: FormGroup
  provinceId: any;
  request_id: any;
  userid: any;
  role_id: any;
  selectdataprovince: Array<IOption> = []
  EditForm: FormGroup;
  loading = false;
  dtOptions: DataTables.Settings = {};
  idAnswer: Number;
  provincefornotirole3: any;
  centralpolicyprovinceid: any;
  idview: any;
  UsercreateName: any;
  imgprofileUrl: any;
  createdat: any;
  requestfile: any;
  answerfile: any;
  provincename: any;

  constructor(
    private authorize: AuthorizeService,
    private userService: UserService,
    private notificationService: NotificationService,
    private modalService: BsModalService,
    private requestorderrService: RequestorderrService,
    private requestorderService: RequestorderService,
    private activatedRoute: ActivatedRoute,
    private provinceservice: ProvinceService,
    private fb: FormBuilder,
    private router: Router,
    private inspectionplanservice: InspectionplanService,
    private centralpolicyService: CentralpolicyService,
    @Inject('BASE_URL') baseUrl: string

  ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.Form = this.fb.group({
      name: [''],
      files: [null]
    })
    this.imgprofileUrl = baseUrl + '/requestfile';
  }

  ngOnInit() {
    this.getuserinfo();
    // this.getProvine();

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

    this.getDetailRequestOrder()

  }

  mapUserProvince() {

  }
  openModal(template: TemplateRef<any>, userid) {
    //console.log(this.userid);

    this.userService.getuserfirstdata(userid)
      .subscribe(result1 => {
        var userProvince: any = result1[0].userProvince
        //console.log('getuserprovince', result1[0].userProvince);
        this.selectdataprovince = [];

        this.requestorderService.getcentralpolicyprovinceiddata(this.id)
          .subscribe(result => {
            // console.log('userProvince', userProvince);

            // var province: Array<any>
            for (let i = 0; i < userProvince.length; i++) {

              var selectdataprovince = result.filter((item, index) => {
                //console.log(userProvince[i].provinceId, item.provinceId);
                return userProvince[i].provinceId == item.provinceId
              })
                .map((item, index) => {
                  return { value: item.province.id, label: item.province.name }
                })
              this.selectdataprovince = this.selectdataprovince.concat(selectdataprovince)
              //console.log( this.selectdataprovince);

            }

          })

        // alert(this.id);
        //   this.selectdataprovince = result[0].userProvince.map((item, index) => {
        //     return { value: item.province.id, label: item.province.name }
        //   })

      })

    this.modalRef = this.modalService.show(template);
    this.modalRef.hide();
    this.Form.reset();
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
    this.createdat = date;
    this.centralpolicy = text
    this.text = text2;
    this.requestfile = file
    this.answerdetail = name;
    this.answerproblem = name2;
    this.answercounsel = name3;
    this.answerfile = file2;

    this.modalRef = this.modalService.show(template);
  }
  getDetailRequestOrder() {
    this.requestorderrService.getdetailrequestorderdata(this.id)
      .subscribe(result => {
        this.resultdetailrequestorder = result
        //console.log('c',this.resultdetailrequestorder);
        this.loading = true
      })

  }
  getProvine() {
    this.requestorderrService.getprovince(this.id)
      .subscribe(result => {
        this.resultprovince = result
        // console.log("in1", this.resultprovince);

        this.selectdataprovince = this.resultprovince.map((item, index) => {
          return { value: item.province.id, label: item.province.name }
        })
        // console.log("in2", this.selectdataprovince);
      })
  }
  addModal(template: TemplateRef<any>, id, name, userid) {
    //alert(3)
    this.userService.getuserfirstdata(userid)
      .subscribe(result => {

        this.provincename = result[0].province.id
        //console.log(this.provincename);

      })
    this.id = id;
    this.text = name;
    this.centralpolicyid = id;
    this.provinceId = id;
    this.modalRef = this.modalService.show(template);
    this.EditForm = this.fb.group({
      "detailrequestordername": new FormControl(null, [Validators.required]),
      // "test" : new FormControl(null,[Validators.required,this.forbiddenNames.bind(this)])
    })
    this.EditForm.patchValue({
      "detailrequestordername": name
    })
  }
  uploadFile(event) {
    // console.log("event", event);

    var file = (event.target as HTMLInputElement).files;
    this.Form.patchValue({
      files: file
    });
    // console.log(this.Form.get('files').value);


  }
  selectedEditFile(event) {
    // console.log("event", event);

    var files: FileList = (event.target as HTMLInputElement).files;
    this.EditForm.get('files').patchValue(files)
    // this.EditForm.patchValue({

    // });
    // console.log(this.EditForm.get('files').value);

  }
  storedetailrequestorder(value) {
    //console.log(this.provinceId);

    //console.log("value", this.Form.value.files)
    this.requestorderrService.adddetailrequestorder(value, this.Form.value.files, this.id, this.userid)
      .subscribe(result => {
        this.request_id = result.id;

        this.notificationService.addNotification(this.id, value.provinceId, this.userid, 12, this.request_id)
          .subscribe(result => {
          })

        this.modalRef.hide();
        this.Form.reset();
        this.getuserinfo()
        this.getProvine()
        this.getDetailRequestOrder()
      })
  }
  answerModal(template: TemplateRef<any>, item) {
     //alert(JSON.stringify(item))
    // console.log(item);

    this.idAnswer = item.id;
    this.modalRef = this.modalService.show(template);
    this.EditForm = this.fb.group({
      "AnswerDetail": new FormControl(null, [Validators.required]),
      "AnswerProblem": new FormControl(null, [Validators.required]),
      "AnswerCounsel": new FormControl(null, [Validators.required]),
      "AnswerUserId": new FormControl(null, [Validators.required]),//
      "files": new FormControl(null, [Validators.required]),
      // "test" : new FormControl(null,[Validators.required,this.forbiddenNames.bind(this)])

    })
    this.EditForm.patchValue({
      "AnswerDetail": item.answerDetail,
      "AnswerProblem": item.answerProblem,
      "AnswerCounsel": item.answerCounsel,
     
    })
    this.getuserinfo()
    this.modalRef.hide();
    this.EditForm.reset();

  }
  uploadFile2(event) {
    // console.log("event", event);

    const file = (event.target as HTMLInputElement).files;
    this.Form.patchValue({
      files: file
    });
    this.Form.get('files2').updateValueAndValidity()
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
            // console.log("roleID: ", this.role_id);

            if (this.role_id != 3) {
              this.requestorderrService.getdetailrequestorderdata(this.id)
                .subscribe(result => {
                  this.resultdetailrequestorder = result
                  // console.log("test: ", this.resultdetailrequestorder);
                  this.loading = true
                })
            } else {

              this.requestorderrService.getdetailrequestorderdatarole3(this.id, this.userid)
                .subscribe(result => {
                  this.resultdetailrequestorder = result
                  this.provincefornotirole3 = result[0].province
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

  editAnswerrequestorder(value) {
    //alert(this.idAnswer)
    // alert(JSON.stringify(value))
    // console.log(this.requestorderrService.editanswerrequest());
    // console.log("id: ", this.idAnswer)
    this.requestorderService.editAnswerrequestorder(value, this.idAnswer)
      .subscribe(result => {
        // console.log(result);

        var body = {
        }
        this.notificationService.addNotification(this.id, this.provincefornotirole3, 1, 13, this.idAnswer)
          .subscribe(result => {
            // console.log(result);

          })
         //alert(this.userid)
         //console.log('user: ', this.userid);

      })

  }

}