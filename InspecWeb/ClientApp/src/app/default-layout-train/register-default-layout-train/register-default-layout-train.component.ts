// import { Component, OnInit } from '@angular/core';
import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TrainingService } from '../../services/training.service';
import { UserService } from 'src/app/services/user.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NotofyService } from 'src/app/services/notofy.service';
@Component({
  selector: 'app-register-default-layout-train',
  templateUrl: './register-default-layout-train.component.html',
  styleUrls: ['./register-default-layout-train.component.css']
})
export class RegisterDefaultLayoutTrainComponent implements OnInit {

  trainingid: string;
  resulttraining: any[] = [];
  resulttraining2: any[] = [];
  modalRef: BsModalRef;
  delid: any
  loading = false;
  dtOptions: DataTables.Settings = {};
  dtOptions2: DataTables.Settings = {};
  name: string;
  createdAt: Date;
  detail: string;
  urlimg: string;
  startdate: Date;
  enddate: Date;
  regisstartdate: Date;
  regisenddate: Date;
  downloadUrl: any;

  userid: any
  resultuser: any[];
  resultpeople: any = []
  selectpeople: Array<any>
  role_id: any
  Prefix: any;
  Name: any;
  CardId: any;
  Position: any;
  PhoneNumber: any;
  Email: any;
  Img: any;
  Form: FormGroup;
  mainUrl: string;
  department
  username
  departmentid
  check: any
  // constructor() { }
  constructor(private modalService: BsModalService,
    private authorize: AuthorizeService,
    private UserService: UserService,
    private fb: FormBuilder,
    private trainingservice: TrainingService,
    public share: TrainingService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private _NotofyService: NotofyService,
    @Inject('BASE_URL') baseUrl: string) {
    this.trainingid = activatedRoute.snapshot.paramMap.get('id')
    this.downloadUrl = baseUrl + 'Uploads'
    this.mainUrl = baseUrl
  }

  ngOnInit() {
    // this.dtOptions = {
    //   pagingType: 'full_numbers',
    //   columnDefs: [
    //     {
    //       targets: [4,5],
    //       orderable: false
    //     }
    //   ]

    // };

    // this.dtOptions2 = {
    //   pagingType: 'full_numbers',
    //   columnDefs: [
    //     {
    //       targets: [4,5],
    //       orderable: false
    //     }
    //   ]

    // };
    this.getuserinfo();

    // alert(this.userid)
    // alert(this.trainingid)
    this.trainingservice.getchecktrainingregister(this.trainingid, this.userid)
      .subscribe(result => {
        this.check = result

        if (this.check == true) {
          this.router.navigate(['/train/']);
          this._NotofyService.onError("ท่านได้ทำการสมัครอบรมเรียบร้อยแล้ว")
        }
      })


    this.Form = this.fb.group({
      name: new FormControl(null, [Validators.required]),
      cardid: new FormControl(null, [Validators.required]),
      position: new FormControl(null, [Validators.required]),
      department: new FormControl(null, [Validators.required]),
      phone: new FormControl(null, [Validators.required]),
      email: new FormControl(null, [Validators.required]),

      type: new FormControl(null, [Validators.required]),
      nickname: new FormControl(null, [Validators.required]),
      retireddate: new FormControl(null, [Validators.required]),
      birthdate: new FormControl(null, [Validators.required]),
      officeaddress: new FormControl(null, [Validators.required]),
      fax: new FormControl(null, [Validators.required]),
      collaboratorname: new FormControl(null, [Validators.required]),
      collaboratorphone: new FormControl(null, [Validators.required]),
      collaboratorphoneoffice: new FormControl(null, [Validators.required]),
      collaboratoremail: new FormControl(null, [Validators.required]),

      files: [null],
      CertificationFiles: [null],
      idcardFiles: [null],
      GovernmentpassportFiles: [null],
    })


    this.trainingservice.getdetailtraining(this.trainingid)
      .subscribe(result => {
        if (result.length != 0) {
          this.name = result[0].name
          this.createdAt = result[0].createdAt
          this.detail = result[0].detail
          this.urlimg = result[0].image
          this.startdate = result[0].startDate
          this.enddate = result[0].endDate
          this.regisstartdate = result[0].regisStartDate
          this.regisenddate = result[0].regisEndDate
        }
        this.resulttraining = result
        this.loading = true;
        //console.log(this.resulttraining);
      })

  }

  //start getuser
  getuserinfo() {
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        //alert(this.userid)
        this.UserService.getuserfirstdata(this.userid)
          .subscribe(result => {


            this.resultuser = result;
            console.log("resultuser => " , this.resultuser);
            this.role_id = result[0].role_id

            this.Prefix = result[0].prefix
            this.Name = result[0].name
            this.Position = result[0].position
            this.PhoneNumber = result[0].phoneNumber
            this.Email = result[0].email
            this.Img = result[0].img

            this.username = result[0].userName
            this.department = result[0].ministries.name
            this.departmentid = result[0].ministries.id
            // alert(this.username)
            this.Form.patchValue({
              Prefix: this.Prefix,
              name: this.Name,
              cardid: this.CardId,
              position: this.Position,
              phone: this.PhoneNumber,
              email: this.Email,
              Formprofile: 1,
              department: this.department
              // departmentid: this.departmentid
              //files: this.files,
            });

          })
      })

  }
  //End getuser

  gotoMain(){
    this.router.navigate(['/train'])
  }

  gotoAdmin(){
    this.router.navigate(["/main"])
  }

  gotoBack() {
    window.history.back();
  }

  GotoDetail(trainingid2) {
    //alert(trainingid2);
    this.router.navigate(['/train/detail/', trainingid2]);
  }

  GotoList(trainingid2) {
    //alert(trainingid2);
    this.router.navigate(['/train/list/', trainingid2]);
  }

  storeTraining(value) {
    // alert(JSON.stringify(value.retireddate))
    this.trainingservice.addTrainingRegister(value, this.trainingid, this.Form.value.files, this.Form.value.CertificationFiles, this.Form.value.idcardFiles, this.Form.value.GovernmentpassportFiles, this.userid, this.username, this.departmentid).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.loading = false;
      this.router.navigate(['/train/register-success/', this.trainingid])
      //this.router.navigate(['/training/surveylist/',trainingid])
    })
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
      CertificationFiles: file
    });
    this.Form.get('files').updateValueAndValidity()
  }
  uploadFile3(event) {
    const file = (event.target as HTMLInputElement).files;
    this.Form.patchValue({
      idcardFiles: file
    });
    this.Form.get('files').updateValueAndValidity()
  }
  uploadFile4(event) {
    const file = (event.target as HTMLInputElement).files;
    this.Form.patchValue({
      GovernmentpassportFiles: file
    });
    this.Form.get('files').updateValueAndValidity()
  }
}
