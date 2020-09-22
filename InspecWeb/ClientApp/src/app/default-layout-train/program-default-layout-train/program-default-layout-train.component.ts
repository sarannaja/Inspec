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
  selector: 'app-program-default-layout-train',
  templateUrl: './program-default-layout-train.component.html',
  styleUrls: ['./program-default-layout-train.component.css']
})
export class ProgramDefaultLayoutTrainComponent implements OnInit {

  trainingid: string;
  resulttraining: any[] = [];
  resulttraining2: any[] = [];
  resulttraining3: any[] = [];
  resulttrainingprogram: any[] = [];
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
      this.trainingphaseid = activatedRoute.snapshot.paramMap.get('id')
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


    this.trainingservice.getprogramtraining(this.trainingphaseid)
      .subscribe(result => {
        this.trainingid = result[0].trainingPhase.trainingId
        this.programstartdate = result[0].programDate
        this.resulttrainingprogram = result
        this.loading = true
        this.phaseno = result[0].trainingPhase.phaseNo
        this.detail = result[0].trainingPhase.detail
        //console.log(this.resulttraining);

        //center training
        this.trainingservice.getdetailtraining(this.trainingid)
        .subscribe(result => {
          if (result.length != 0){
            
            this.name = result[0].name
            this.createdAt = result[0].createdAt
            
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
    })
    this.trainingservice.getTrainingPlan(this.trainingphaseid).subscribe(result => {

      this.chars = result.map(result2 => { return { ...result2, programDate: result2.trainingProgram.programDate } })
      this.chars = _.chain(this.chars)
        .groupBy("programDate")
        // `key` is group's name (color), `value` is the array of objects
        .map((value, key) => ({ programDate: key, value: value  }))
        .value()
      // chars = _.orderBy(chars, ['programDate'], ['asc']); // Use Lodash to sort array by 'name'

      // this.setState({ characters: chars })
      console.log('chars', this.chars);


      this.loading = true
      // this.dtOptions = {
      //   // pagingType: 'full_numbers',
      //   searching: false,
      //   pageLength: this.resulttrainingplan.length,
      //   lengthChange: false,
      //   info: false,
      //   paging: false,
      //   ordering: false,
      // };
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
