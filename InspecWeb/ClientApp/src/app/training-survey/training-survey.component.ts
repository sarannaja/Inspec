import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { TrainingService } from '../services/training.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-training-survey',
  templateUrl: './training-survey.component.html',
  styleUrls: ['./training-survey.component.css']
})
export class TrainingSurveyComponent implements OnInit {

  resulttraining: any[] = []
  modalRef: BsModalRef;
  delid: any
  loading = false;
  dtOptions: DataTables.Settings = {};
  mainUrl: string;
  trainingid: any;

  constructor(private modalService: BsModalService, 
    private fb: FormBuilder, 
    private trainingservice: TrainingService,
    public share: TrainingService, 
    private router: Router,
    private activatedRoute: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string) {
      this.mainUrl = baseUrl
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

  

}
