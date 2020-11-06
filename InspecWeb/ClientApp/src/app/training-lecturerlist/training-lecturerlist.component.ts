import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { TrainingService } from '../services/training.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-training-lecturerlist',
  templateUrl: './training-lecturerlist.component.html',
  styleUrls: ['./training-lecturerlist.component.css']
})
export class TrainingLecturerListComponent implements OnInit {

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
      this.downloadUrl = baseUrl + '/Uploads'
      this.mainUrl = baseUrl
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
    

    this.Form = this.fb.group({
      name: new FormControl(null, [Validators.required]),
     
    })

    this.trainingservice.gettraininglecturerlist(this.trainingid)
    .subscribe(result => {
      this.resulttraining = result.filter(
        (thing, i, arr) => arr.findIndex(t => t.trainingLecturerId === thing.trainingLecturerId) === i
      );
      this.loading = true;
      console.log(this.resulttraining);
    })

    this.trainingservice.gettrainingsurveycountdata()
    .subscribe(result => {
      this.resultsurveytraining = result
      //this.loading = true;
      //console.log("12345:",this.resultsurveytraining);

      if (this.resultsurveytraining.length > 0){
        this.selectdatasurvey = this.resultsurveytraining.map((item, index) => {
          return { value: item.id, label: item.name }
        })
      }
        

    })
  }
  CreateTraining(){
    this.router.navigate(['/training/createtraining'])
  }
  openModal(template: TemplateRef<any>, id) {
    this.delid = id;
    console.log(this.delid);

    this.modalRef = this.modalService.show(template);
  }

  surveyModal(template: TemplateRef<any>, id) {
    this.lecturerid = id;
    console.log(this.lecturerid);

    this.modalRef = this.modalService.show(template);
  }

  storeTraining(value) {
    //alert(JSON.stringify(value))
    console.log("data:",value);
    console.log("lecturerid:",this.lecturerid);

    this.trainingservice.addTraininglecturerjoinsurvey(value, this.lecturerid, this.trainingid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.trainingservice.gettraininglecturerlist(this.trainingid)
      .subscribe(result => {
        this.resulttraining = result.filter(
          (thing, i, arr) => arr.findIndex(t => t.trainingLecturerId === thing.trainingLecturerId) === i
        );
        this.loading = true;
        console.log(this.resulttraining);
      })
    })
  }

  editModal(template: TemplateRef<any>, id, lecturerName, phone, email, education, workHistory, experience) {
    this.delid = id;
    //console.log(this.delid);

    this.modalRef = this.modalService.show(template);
    this.EditForm = this.fb.group({
      "lecturername": new FormControl(null, [Validators.required]),
      "lecturerphone": new FormControl(null, [Validators.required]),
      "lectureremail": new FormControl(null, [Validators.required]),
      "education": new FormControl(null, [Validators.required]),
      "workhistory": new FormControl(null, [Validators.required]),
      "experience": new FormControl(null, [Validators.required]),
    })


    this.EditForm.patchValue({
      "lecturername": lecturerName,
      "lecturerphone": phone,
      "lectureremail": email,
      "education": education,
      "workhistory": workHistory,
      "experience": experience,
    })
  }

  editTraining(value, delid) {
    // alert(JSON.stringify(value));
    // console.clear();
    // console.log("kkkk" + JSON.stringify(value));
    this.trainingservice.editTraininglecturer(value, delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false

      this.trainingservice.gettraininglecturer()
      .subscribe(result => {
        this.resulttraining = result
        this.loading = true;
        console.log(this.resulttraining);
      })
    })
  }

  deleteTraining(value) {
    this.trainingservice.deleteTrainingLecturer(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.loading = false;
      this.trainingservice.gettraininglecturer()
      .subscribe(result => {
        this.resulttraining = result
        this.loading = true;
        console.log(this.resulttraining);
      })
    })
  }

  gotoProgramTraining(trainingid){
    this.router.navigate(['/training/program/', trainingid])
  }

  // gotoSurverTraining(){
  //   this.router.navigate(['/train/survey/', this.trainingid])
  // }

  gotoBack() {
    window.history.back();
  }

  gotoProcessingTraining(trainingLecturerId){
    this.router.navigate(['/training/survey/processing/', trainingLecturerId])
  }
  reportTrainingLecturer(trainingLecturerId){
    // alert(trainingLecturerId)
    this.trainingservice.reportTrainingLecturer(trainingLecturerId).subscribe(result => {
      window.open(this.downloadUrl + "/" + result.data);
    })
  }
  
}
