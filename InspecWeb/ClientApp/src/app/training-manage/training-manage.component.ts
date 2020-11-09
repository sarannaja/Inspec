import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { TrainingService } from '../services/training.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-training-manage',
  templateUrl: './training-manage.component.html',
  styleUrls: ['./training-manage.component.css']
})
export class TrainingManageComponent implements OnInit {
  trainingid: string;
  resulttraining: any[] = []
  modalRef: BsModalRef;
  delid: any
  loading = false;
  dtOptions: DataTables.Settings = {};
  mainUrl: string;
  datacondition: any[];
  
  
  constructor(private modalService: BsModalService, 
    private fb: FormBuilder, 
    private trainingservice: TrainingService,
    public share: TrainingService, 
    private router: Router,
    private activatedRoute: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string) {
      this.mainUrl = baseUrl,
      this.trainingid = activatedRoute.snapshot.paramMap.get('id')
    }

  ngOnInit() {
    
    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [3],
          orderable: false
        }
      ]

    };

    this.trainingservice.gettrainingsurveycountdata()
    .subscribe(result => {
      this.resulttraining = result
      this.loading = true;
      console.log(this.resulttraining);
    })

    this.getcheck_condition();
  }

  //ข้อมูลกำหนดคุณสมบัติ
  getcheck_condition() {
    this.trainingservice.getTrainingCondition(this.trainingid)
    .subscribe(result => {
      this.datacondition = result
      this.loading = true
      console.log(this.datacondition);
    })
  }




  GotoUploadDocument(){
    this.router.navigate(['/training/documentlist/',this.trainingid])
  }

  // GotoSurveyTrainingList(trainingid){
  //   this.router.navigate(['/training/surveylist/',trainingid])
  // }

  // GotoPreviewTraining(trainingid){
  //   this.router.navigate(['/training/survey/preview/',trainingid])
  // }

  gotoIDCodeTraining(){
    this.router.navigate(['/training/idcode/', this.trainingid])
  }

  gotoNamePlateTraining(){
    this.router.navigate(['/nameplate/', this.trainingid])
  }

  gotoConditionTraining(){
    this.router.navigate(['/training/condition/', this.trainingid])
  }

  gotoRegisterListTraining(){
    this.router.navigate(['/training/registerlist/', this.trainingid])
  }

  gotoLecturerListTraining(){
    this.router.navigate(['/training/lecturerlist/', this.trainingid])
  }

  gotoProgramLoginTraining(){
    this.router.navigate(['/training/programlogin/', this.trainingid])
  }
  
  gotoGroupTraining(){
    this.router.navigate(['/training/register/program/group/', this.trainingid])
  }

  gotogLoginListTraining(){
    this.router.navigate(['/training/login/list/', this.trainingid])
  }

  gotogReportTraining(){
    this.router.navigate(['/training/reportmemu/', this.trainingid])
  }

  gotoBack() {
    window.history.back();
  }

}
