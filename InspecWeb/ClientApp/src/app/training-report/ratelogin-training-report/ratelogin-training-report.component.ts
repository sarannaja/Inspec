import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { TrainingService } from '../../services/training.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-ratelogin-training-report',
  templateUrl: './ratelogin-training-report.component.html',
  styleUrls: ['./ratelogin-training-report.component.css']
})
export class RateloginTrainingReportComponent implements OnInit {

  resulttraining: any[] = []
  resultsurveytraining: any[] = []
  trainingid: string;
  modalRef: BsModalRef;
  delid: any
  loading = false;
  dtOptions: DataTables.Settings = {};
  downloadUrl: any;
  mainUrl: string;
  Form: FormGroup;
  EditForm: FormGroup;
  selectdatalecturer: any[] = []
  selectdatasurvey: Array<any>
  lecturerid: any

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
      // columnDefs: [
      //   {
      //     targets: [4,5],
      //     orderable: false
      //   }
      // ]

    };
    
    this.trainingservice.getTrainingLoginRate(this.trainingid)
    .subscribe(result => {
      this.resulttraining = result;
      this.loading = true;
      console.log(this.resulttraining);
    })

  }

  gotoBack() {
    window.history.back();
  }
  
}
