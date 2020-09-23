import { Component, OnInit, TemplateRef, Inject} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TrainingService } from '../../services/training.service';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';


@Component({
  selector: 'app-preview-training-survey',
  templateUrl: './preview-training-survey.component.html',
  styleUrls: ['./preview-training-survey.component.css']
})
export class PreviewTrainingSurveyComponent implements OnInit {

  trainingid: string;
  resulttraining: any[] = [];
  modalRef: BsModalRef;
  delid: any;
  loading = false;
  dtOptions: DataTables.Settings = {};
  
  trainingname: string;
  
  constructor(private modalService: BsModalService, 
    private fb: FormBuilder, 
    private trainingservice: TrainingService,
    public share: TrainingService, 
    private router: Router,
    private activatedRoute: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string) {
      this.trainingid = activatedRoute.snapshot.paramMap.get('id')
    }
    
    

  ngOnInit() {

    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [4,5],
          orderable: false
        }
      ]

    };

    

    this.trainingservice.getlisttrainingsurveydata(this.trainingid)
    .subscribe(result => {
      if (result.length != 0){
        this.trainingname = result[0].trainingSurveyTopic.name
      }
      this.resulttraining = result
      this.loading = true
      //console.log(this.resulttraining);
    })
    

    // this.trainingservice.gettrainingdata2(id)
    // .subscribe(result => {
    //   this.resulttraining = result
    //   this.loading = true;
    //   console.log(this.resulttraining);
    // })

    //this.gettrainingdata2()
  }

  // gettrainingdata2() {
  //   this.trainingservice.gettrainingdata2(1)
  //   .subscribe(result => {
  //     this.resulttraining = result
  //     this.loading = true
  //     //console.log(this.resulttraining);
  //   })
  // }


}
