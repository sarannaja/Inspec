// import { Component, OnInit } from '@angular/core';
import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TrainingService } from '../../services/training.service';
import { Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';


@Component({
  selector: 'app-list-default-layout-train',
  templateUrl: './list-default-layout-train.component.html',
  styleUrls: ['./list-default-layout-train.component.css']
})
export class ListDefaultLayoutTrainComponent implements OnInit {

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
    this.dtOptions3 = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [3],
          orderable: false
        }
      ]

    };

    this.dtOptions2 = {
      //pagingType: 'full_numbers',
      // columnDefs: [
      //   {
      //     targets: [0,1],
      //     orderable: false
      //   }
      // ]

    };

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

    this.trainingservice.getregistertrainingdata(this.trainingid)
    .subscribe(result => {
      this.resulttraining3 = result
      this.loading = true
      console.log(this.resulttraining3);
    })

  }

  GotoDetail(trainingid2){
    //alert(trainingid2);
    this.router.navigate(['/train/detail/',trainingid2]);
  }

  gotoMain(){
    this.router.navigate(['/train'])
  }

  gotoAdmin(){
    this.router.navigate(["/main"])
  }

  gotoBack() {
    window.history.back();
  }
}
