// import { Component, OnInit } from '@angular/core';
import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TrainingService } from '../../services/training.service';
import { UserService } from 'src/app/services/user.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { IOption } from 'ng-select';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

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
  resultuser :any [];
  resultpeople: any = []
  selectpeople: Array<IOption>
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
    this.Form = this.fb.group({
      name: new FormControl(null, [Validators.required]),
      cardid: new FormControl(null, [Validators.required]),
      position: new FormControl(null, [Validators.required]),
      phone: new FormControl(null, [Validators.required]),
      email: new FormControl(null, [Validators.required]),
      
    })

    // this.userservice.getuserfirstdata(0).subscribe(result => {
    //    alert(JSON.stringify(result))
    //   this.resultpeople = result
    //   console.log(this.resultpeople);
    //   this.selectpeople = this.resultpeople.map((item, index) => {
    //     return { value: item.id, label: item.name }
    //   })
    // })
    //alert(this.trainingid);

    this.trainingservice.getdetailtraining(this.trainingid)
    .subscribe(result => {
      if (result.length != 0){
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

    this.trainingservice.gettrainingdata()
    .subscribe(result => {
      this.resulttraining2 = result
      this.loading = true;
      //console.log(this.resulttraining);
    })
  }

//start getuser
getuserinfo(){
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
        cardid: this.CardId,
        position: this.Position,
        phone: this.PhoneNumber,
        email: this.Email,
        Formprofile:1,
        //files: this.files,
      });

    })
  })

}
//End getuser

  GotoDetail(trainingid2){
    //alert(trainingid2);
    this.router.navigate(['/train/detail/',trainingid2]);
  }

  GotoList(trainingid2){
    //alert(trainingid2);
    this.router.navigate(['/train/list/',trainingid2]);
  }

  storeTraining(value) {
    //alert(JSON.stringify(value))
    this.trainingservice.addTrainingRegister(value, this.trainingid).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.loading = false;
      this.router.navigate(['/train/register-success/', this.trainingid])
      //this.router.navigate(['/training/surveylist/',trainingid])
    })
  }

}
