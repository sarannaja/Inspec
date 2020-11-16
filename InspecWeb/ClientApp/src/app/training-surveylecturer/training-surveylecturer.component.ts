import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { TrainingService } from '../services/training.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormControl, Validators, FormGroup } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-training-surveylecturer',
  templateUrl: './training-surveylecturer.component.html',
  styleUrls: ['./training-surveylecturer.component.css']
})
export class TrainingSurveyLecturerComponent implements OnInit {

  resulttraining: any[] = []
  modalRef: BsModalRef;
  delid: any
  loading = false;
  dtOptions: DataTables.Settings = {};
  mainUrl: string;
  trainingid: any;
  Form: FormGroup;
  userid: string;
  resultuser: any[];
  role_id: any;
  username: any;

  constructor(private modalService: BsModalService,
    private authorize: AuthorizeService,
    private UserService: UserService,
    private fb: FormBuilder,
    private trainingservice: TrainingService,
    public share: TrainingService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string) {
      this.mainUrl = baseUrl
      // this.trainingid = activatedRoute.snapshot.paramMap.get('id')
    }

  ngOnInit() {

    // this.dtOptions = {
    //   pagingType: 'full_numbers',
    //   columnDefs: [
    //     {
    //       targets: [3],
    //       orderable: false
    //     }
    //   ]

    // };
    
    this.getuserinfo()

    
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

            // this.Prefix = result[0].prefix
            // this.Name = result[0].name
            // this.Position = result[0].position
            // this.PhoneNumber = result[0].phoneNumber
            // this.Email = result[0].email
            // this.Img = result[0].img

            this.username = result[0].userName
            console.log("username =>",this.username);
            // this.department = result[0].provincialDepartments.name
            // this.departmentid = result[0].provincialDepartments.id
            // alert(this.username)
            
            this.trainingservice.getTrainingSurveyLecturer(this.username)
            .subscribe(result => {
              this.resulttraining = result
              this.loading = true;
              
              console.log("resultTrainingSurveyLecturer =>",this.resulttraining);
            })

          })
      })

  }
  //End getuser

  gotoBack() {
    window.history.back();
  }
 
  GotoSurveyLecturer(trainingid, surveyjoinlecturerid, surveytopicid){
    this.router.navigate(['/train/survey/'+ trainingid + '/' + surveyjoinlecturerid + '/' + surveytopicid])
  }

  GotoSurveyTrainingList(trainingid){
    this.router.navigate(['/training/surveylist/',trainingid])
  }

  GotoPreviewTraining(trainingid){
    this.router.navigate(['/training/survey/preview/',trainingid])
  }

  GotoChartTraining(trainingid){
    this.router.navigate(['/training/survey/chart/',trainingid])
  }

  openModal(template: TemplateRef<any>, id) {
    this.delid = id;
   // console.log(this.delid);

   this.modalRef = this.modalService.show(template);
  }

  storeTraining(value) {
    //alert(JSON.stringify(value))
    this.trainingservice.addTrainingsurveytopic(value).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      //this.router.navigate(['/training/surveylist/',trainingid])
      //this.router.navigate(['training'])
      this.trainingservice.gettrainingsurveycountdata()
      .subscribe(result => {
        this.resulttraining = result
        this.loading = true
        //console.log(this.resulttraining);
      })

    })
  }



}
