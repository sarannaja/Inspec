// import { Component, OnInit } from '@angular/core';
import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TrainingService } from '../../services/training.service';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { async } from '@angular/core/testing';
import * as _ from 'lodash';

@Component({
  selector: 'app-condition-default-layout-train',
  templateUrl: './condition-default-layout-train.component.html',
  styleUrls: ['./condition-default-layout-train.component.css']
})
export class ConditionDefaultLayoutTrainComponent implements OnInit {

  trainingid: string;
  resulcondition: any[] = [];
  resulttraining: any[] = [];
  resulttraining2: any[] = [];
  resulttraining3: any[] = [];
  resulttrainingprogram: any[] = [];
  modalRef: BsModalRef;
  delid: any
  loading = false;
  dtOptions: any = {};
  dtOptions2: any = {};
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
  Form: FormGroup;
  programstartdate: Date;
  trainingphaseid: string;
  phaseno: string;
  chars: any[] = []

  // constructor() { }
  constructor(private modalService: BsModalService, 
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
    this.trainingservice.getTrainingCondition(this.trainingid)
    .subscribe(result => {
      this.resulcondition = result
      this.loading = true
      console.log(this.resulcondition);
    })

    //center training
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
    })


    
  }

  gotoBack() {
    window.history.back();
  }


  gotoMain(){
    this.router.navigate(['/train'])
  }

  gotoAdmin(){
    this.router.navigate(["/main"])
  }

  GotoDetail(trainingid2){
    //alert(trainingid2);
    this.router.navigate(['/train/detail/',trainingid2]);
  }

  GotoList(trainingid2){
    //alert(trainingid2);
    this.router.navigate(['/train/list/',trainingid2]);
  }

  GotoRegister(trainingid){
    //alert(trainingid);
    this.router.navigate(['/train/register/',trainingid])
  }

  openModal(template: TemplateRef<any>, id) {
    this.delid = id;
   // console.log(this.delid);
   this.modalRef = this.modalService.show(template);
 }


}
