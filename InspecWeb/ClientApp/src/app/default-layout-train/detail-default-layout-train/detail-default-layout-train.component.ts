// import { Component, OnInit } from '@angular/core';
import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TrainingService } from '../../services/training.service';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-detail-default-layout-train',
  templateUrl: './detail-default-layout-train.component.html',
  styleUrls: ['./detail-default-layout-train.component.css']
})
export class DetailDefaultLayoutTrainComponent implements OnInit {

  trainingid: string;
  resulttraining: any[] = [];
  resulttraining2: any[] = [];
  resulttraining3: any[] = [];
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
  mainUrl: any;
  Form: FormGroup;

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
    this.dtOptions = {
      // pagingType: 'full_numbers',
      // columnDefs: [
      //   {
      //     targets: [4,5],
      //     orderable: false
      //   }
      // ]

    };

    this.dtOptions2 = {
      // pagingType: 'full_numbers',
      // columnDefs: [
      //   {
      //     targets: [4,5],
      //     orderable: false
      //   }
      // ]

    };

    this.Form = this.fb.group({
      type: new FormControl(null, [Validators.required]),
      
    })

    //alert(this.trainingid);

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

    //right training
    this.trainingservice.gettrainingdata()
    .subscribe(result => {
      this.resulttraining2 = result
      this.loading = true;
      //console.log(this.resulttraining);
    })

    //document training 
    this.trainingservice.getlisttrainingdocumentdata(this.trainingid)
    .subscribe(result => {
      this.resulttraining3 = result
      this.loading = true
      //console.log(this.resulttraining);
    })
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

 storeTraining(value) {
   

   if(value.type == 'in'){
    //alert('1:' + value.type)
    this.Form.reset()
    this.modalRef.hide()
    this.loading = false;
    this.router.navigate(['/train/register/',this.trainingid])
   }
   else{
    //alert('2:' + value.type)
    this.Form.reset()
    this.modalRef.hide()
    this.loading = false;
    this.router.navigate(['/train/register-external/',this.trainingid])
   }
  // alert(JSON.stringify(value))
  // this.trainingservice.addTrainingsurvey(value, this.trainingid).subscribe(response => {
  //   console.log(value);
  //   this.Form.reset()
  //   this.modalRef.hide()
  //   this.loading = false;
  //   //this.router.navigate(['/training/surveylist/',trainingid])
  //   //this.router.navigate(['training'])
  //   this.trainingservice.getlisttrainingsurveydata(this.trainingid)
  //   .subscribe(result => {
  //     this.resulttraining = result
  //     this.loading = true
      //console.log(this.resulttraining);
    // })

  // })
}

}
