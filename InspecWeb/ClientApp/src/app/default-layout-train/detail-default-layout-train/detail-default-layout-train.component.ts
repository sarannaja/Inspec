// import { Component, OnInit } from '@angular/core';
import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TrainingService } from '../../services/training.service';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { now } from 'moment';

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
  resulttrainingphase: any[] = [];
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
  userid: string;
  
  // constructor() { }
  constructor(private modalService: BsModalService,
    private authorize: AuthorizeService,
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


    
    this.trainingservice.getTrainingPhase(this.trainingid)
      .subscribe(result => {
        this.resulttrainingphase = result
        this.loading = true
        //console.log(this.resulttraining);
        // if (this.resulttrainingphase.length < 1){
        //   this.gotoMain();
        // }
        
      })

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
      

        // if(this.datex > this.regisstartdate){
        //   alert("เปิดรับสมัคร");
        // }
        // else{
        //   alert("ปิดรับสมัคร");
        // }
        
        

      }
      this.resulttraining = result
      this.loading = true;
      //console.log(this.resulttraining);
    })

    //right training
    this.trainingservice.gettrainingdataShowPage()
    .subscribe(result => {
      this.resulttraining2 = result
      this.loading = true;
      //console.log(this.resulttraining);
    })

    //document training
    this.trainingservice.getlisttrainingdocumentdata(this.trainingid)
    .subscribe(result => {
      if (result.length > 0){
        console.log("in");
        
        this.resulttraining3 = result
        this.loading = true
      }
      
      //console.log(this.resulttraining);
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

 gotoDetailRegisterTraining(phaseid) {
   //console.log(phaseid);
  this.router.navigate(['/train/detail/phase/',phaseid])
 }

 gotoApproveRegisterTraining() {
   //console.log(phaseid);
  this.router.navigate(['/train/list/',this.trainingid])
 }

 gotoConditionRegisterTraining() {
  //console.log(phaseid);
 this.router.navigate(['/train/detail/condition/',this.trainingid])
}
 
 gotoRegisterTraining() {
  this.router.navigate(['/train/register/',this.trainingid])

  //  if(value.type == 'in'){
  //   //alert('1:' + value.type)
  //   this.Form.reset()
  //   this.modalRef.hide()
  //   this.loading = false;
  //   this.router.navigate(['/train/register/',this.trainingid])
  //  }
  //  else{
  //   //alert('2:' + value.type)
  //   this.Form.reset()
  //   this.modalRef.hide()
  //   this.loading = false;
  //   this.router.navigate(['/train/register-external/',this.trainingid])
  //  }
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
