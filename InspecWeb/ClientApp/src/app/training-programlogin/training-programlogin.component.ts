import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { TrainingService } from '../services/training.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormControl, Validators, FormGroup } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-training-programlogin',
  templateUrl: './training-programlogin.component.html',
  styleUrls: ['./training-programlogin.component.css']
})
export class TrainingProgramLoginComponent implements OnInit {
  trainingid: string;
  resulttraining: any[] = []
  modalRef: BsModalRef;
  delid: any
  loading = false;
  dtOptions: DataTables.Settings = {};
  mainUrl: string;
  Form: FormGroup;
  
  
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

    this.Form = this.fb.group({
      name: new FormControl(null, [Validators.required]),
      startyear: new FormControl(null, [Validators.required]),
      endyear: new FormControl(null, [Validators.required]),
      conditiontype: new FormControl(null, [Validators.required]),

    })
  }

  GotoSurveyTrainingList(trainingid){
    this.router.navigate(['/training/surveylist/',trainingid])
  }

  GotoPreviewTraining(trainingid){
    this.router.navigate(['/training/survey/preview/',trainingid])
  }

  openModal(template: TemplateRef<any>, id) {
   // this.delid = id;
   // console.log(this.delid);

   this.modalRef = this.modalService.show(template);
 }

 storeTraining(value) {
  //alert(JSON.stringify(value))
  this.trainingservice.addTrainingCondition(value, this.trainingid).subscribe(response => {
    console.log(value);
    this.Form.reset()
    this.modalRef.hide()
    this.loading = false;
    //this.router.navigate(['/training/surveylist/',trainingid])
    //this.router.navigate(['training'])
    this.trainingservice.getTrainingCondition(this.trainingid)
    .subscribe(result => {
      this.resulttraining = result
      this.loading = true
      //console.log(this.resulttraining);
    })

  })
}

  

}
