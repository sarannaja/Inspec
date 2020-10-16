import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { TrainingService } from '../services/training.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormControl, Validators, FormGroup } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import html2canvas from 'html2canvas';
import * as fs from 'file-saver';
// import { saveAs } from 'file-saver';
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
  qrdata = 'https://google.com';
  programloginid: any;
  programlogindb: any;
  programdate: any;
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

    this.trainingservice.getTrainingProgramLogin(this.trainingid)
      .subscribe(result => {
        this.resulttraining = result.filter(
          (thing, i, arr) => arr.findIndex(t => t.programDate === thing.programDate) === i
        );
        this.loading = true;
        console.log(this.resulttraining);
      })

    this.Form = this.fb.group({
      programtype: new FormControl(null, [Validators.required]),

    })
  }

  GotoSurveyTrainingList(trainingid) {
    this.router.navigate(['/training/surveylist/', trainingid])
  }

  GotoPreviewTraining(trainingid) {
    this.router.navigate(['/training/survey/preview/', trainingid])
  }

  openModal(template: TemplateRef<any>, id ,programdate) {
    // this.delid = id;
    
    this.programloginid = id;
    this.programdate = programdate
    this.modalRef = this.modalService.show(template);
    console.log(this.programloginid);
  }

  storeTraining(value) {
    this.trainingservice.getTrainingProgramLoginQR(this.programdate).subscribe(
      result => {
        this.programlogindb = result
        
        if (this.programlogindb.length <= 0){
          console.log("Insert");
          //insert
          //alert(JSON.stringify(value))
          this.trainingservice.addTrainingProgramLogin(value, this.trainingid, this.programdate).subscribe(response => {
            console.log(value);
            this.Form.reset()
            this.modalRef.hide()
            this.loading = false;
            //this.router.navigate(['/training/surveylist/',trainingid])
            //this.router.navigate(['training'])
            
            this.trainingservice.getTrainingProgramLogin(this.trainingid)
      .subscribe(result => {
        this.resulttraining = result.filter(
          (thing, i, arr) => arr.findIndex(t => t.programDate === thing.programDate) === i
        );
        this.loading = true;
        console.log(this.resulttraining);
      })
          })
  
        }
        else {
          console.log("Update");
          
          //update
          this.trainingservice.updateTrainingProgramLogin(value, this.programloginid).subscribe(response => {
            console.log(value);
            this.Form.reset()
            this.modalRef.hide()
            this.loading = false;
            //this.router.navigate(['/training/surveylist/',trainingid])
            //this.router.navigate(['training'])
            
            this.trainingservice.getTrainingProgramLogin(this.trainingid)
      .subscribe(result => {
        this.resulttraining = result.filter(
          (thing, i, arr) => arr.findIndex(t => t.programDate === thing.programDate) === i
        );
        this.loading = true;
        console.log(this.resulttraining);
      })
          })
  
  
  
        }


        


      }
    )

      

      
    
  }

  downloadQrCode(idQrcode,filename) {
    const image = html2canvas(document.querySelector('#' + idQrcode)).then(
      canvas => fs.saveAs(canvas.toDataURL(), filename)
    );
  }

  StringQRCode(phaseid, id, type){
    // console.log("id", id);
    // console.log("type", type);
    
    var textqrcode = this.mainUrl + 'training/login/' + phaseid + '/' + id + '/' + type ;
    //console.log("xxx:",textqrcode);
    return textqrcode;
  }

  gotoBack() {
    window.history.back();
  }



}
