// import { Component, OnInit } from '@angular/core';
import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TrainingService } from '../../services/training.service';
import { UserService } from 'src/app/services/user.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, FormArray } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { IOption } from 'ng-select';


@Component({
  selector: 'app-survey-default-layout-train',
  templateUrl: './survey-default-layout-train.component.html',
  styleUrls: ['./survey-default-layout-train.component.css']
})
export class SurveyDefaultLayoutTrainComponent implements OnInit {
  addScore: any[] = [
    4,
    3,
    2,
    1,
  ]
  trainingid: string;
  resulttraining: any[] = [];
  resulttraining2: any[] = [];
  resulttraining3: any[] = [];
  modalRef: BsModalRef;
  delid: any
  loading = false;
  dtOptions: DataTables.Settings = {}; //data training value
  dtOptions2: DataTables.Settings = {}; //data trainging all
  dtOptions3: DataTables.Settings = {}; //data register training
  name: string;
  createdAt: Date;
  detail: string;
  urlimg: string;
  startdate: Date;
  enddate: Date;
  regisstartdate: Date;
  regisenddate: Date;
  downloadUrl: any;
  mainUrl: any;

  check: boolean = false
  userid: any
  resultuser: any[];
  resultpeople: any = []
  selectpeople: Array<IOption>
  role_id: any
  Prefix: any;
  Name: any;
  Position: any;
  PhoneNumber: any;
  Email: any;
  Img: any;
  Form: FormGroup;
  FormAnswer: FormGroup;


  // constructor() { }
  constructor(private modalService: BsModalService,
    private authorize: AuthorizeService,
    private UserService: UserService,
    private fb: FormBuilder,
    private trainingservice: TrainingService,
    public share: TrainingService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string) {
    this.trainingid = activatedRoute.snapshot.paramMap.get('id')
    this.downloadUrl = baseUrl + 'Uploads'
    this.mainUrl = baseUrl
  }



  ngOnInit() {
    this.getuserinfo();
    console.log(this.addScore);

    this.FormAnswer = this.fb.group({
      result: new FormArray([])
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

    this.trainingservice.getlisttrainingsurveydata(this.trainingid)
      .subscribe(result => {
        this.Addvalue(result)
        console.log('result', result);

        // this.resulttraining = result
        this.loading = true
        //console.log(this.resulttraining);
      })



  }
  get f() { return this.FormAnswer.controls }
  get t() { return this.f.result as FormArray; }

  onSubmit(value: any) {

    // console.log(this.t.value);

    for (let i = 0; i < this.t.value.length; i++) {
      if (this.t.value[i].score == 0) {
        console.log(this.t.value[i].name);

        this.check = true
      }
    }

    if (this.check) {
      alert("เลือกให้ครบ")
    } else {
      var inputtrainingsurveyanswer: Array<any> = this.t.value.map(
        (item, index) => {
          return {
            trainingsurveyId: item.trainingsurveyId,
            score: item.score
          }
        })
      var body = {
        Name: this.Name,
        Position: this.Position,
        inputtrainingsurveyanswer
      }
      this.trainingservice.addTrainingsurveyanswer(body)
        .subscribe(result => {
          console.log(result);
          
        })
      alert('เข้าหลังบ้านตรงนี้')
    }

    console.log(value, this.check);

  }
  Addvalue(result: Array<any>) {
    this.FormAnswer.reset()
    this.t.clear()
    for (let i = 0; i < result.length; i++) {
      this.t.push(this.fb.group({
        trainingsurveyId: [result[i].id],
        score: [0],
        AnsName: [result[i].name],

      }))
    }
  }

  formSelected(index, value) {
    this.t.at(index).patchValue({
      score: value
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
            //console.log("test" , this.resultuser);
            this.role_id = result[0].role_id

            this.Prefix = result[0].prefix
            this.Name = result[0].name
            this.Position = result[0].position
            this.PhoneNumber = result[0].phoneNumber
            this.Email = result[0].email
            this.Img = result[0].img

            this.Form.patchValue({
              Prefix: this.Prefix,
              name: this.Name,
              Position: this.Position,
              PhoneNumber: this.PhoneNumber,
              Email: this.Email,
              Formprofile: 1,
              //files: this.files,
            });

          })
      })

  }
  //End getuser

}
