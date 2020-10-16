// import { Component, OnInit } from '@angular/core';
import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TrainingService } from '../../services/training.service';
import { UserService } from 'src/app/services/user.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-success-default-layout-train',
  templateUrl: './success-default-layout-train.component.html',
  styleUrls: ['./success-default-layout-train.component.css']
})
export class SuccessDefaultLayoutTrainComponent implements OnInit {

  trainingid: string;
  modalRef: BsModalRef;
  downloadUrl: any;
  mainUrl: string;

  loading = false;
  resulttraining: any[] = [];
  resulttraining2: any[] = [];
  name: string;
  createdAt: string;
  detail: string;
  urlimg: string;
  startdate: string;
  enddate: string;
  regisstartdate: string;
  regisenddate: string;
  
  
  // constructor() { }
  constructor(private modalService: BsModalService, 
    private authorize: AuthorizeService,
    private fb: FormBuilder, 
    private trainingservice: TrainingService,
    private UserService: UserService,
    public share: TrainingService, 
    private router: Router,
    private activatedRoute: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string) {
      this.trainingid = activatedRoute.snapshot.paramMap.get('id')
      this.downloadUrl = baseUrl + 'Uploads'
      this.mainUrl = baseUrl
    }

  ngOnInit() {
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
      //console.log(this.resulttraining);
    })


  }

  gotoMain(){
    this.router.navigate(['/train'])
  }

  gotoAdmin(){
    this.router.navigate(["/main"])
  }

  GotoDetail(trainingid){
    //alert(trainingid2);
    this.router.navigate(['/train/detail/',trainingid]);
  }

  GotoList(trainingid){
    //alert(trainingid2);
    this.router.navigate(['/train/list/',trainingid]);
  }

}
