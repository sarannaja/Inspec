import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { TrainingService } from '../../services/training.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-lecturer-training',
  templateUrl: './lecturer-training.component.html',
  styleUrls: ['./lecturer-training.component.css']
})
export class LecturerTrainingComponent implements OnInit {

  resulttraining: any[] = []
  modalRef: BsModalRef;
  delid: any
  loading = false;
  dtOptions: DataTables.Settings = {};
  downloadUrl: any;
  mainUrl: string;
  Form: FormGroup;
  EditForm: FormGroup;
  submitted = false;
  ViewForm: FormGroup;

  constructor(private modalService: BsModalService, 
    private fb: FormBuilder, 
    private trainingservice: TrainingService,
    public share: TrainingService, 
    private router: Router,
    @Inject('BASE_URL') baseUrl: string) {
      this.downloadUrl = baseUrl + '/Uploads'
      this.mainUrl = baseUrl
    }

  ngOnInit() {

    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [2,3],
          orderable: false
        }
      ]

    };

    this.Form = this.fb.group({
      "LecturerType": new FormControl("", [Validators.required]),
      lecturername: new FormControl(null, [Validators.required]),
      lecturerphone: new FormControl(null, [Validators.required]),
      lectureremail: new FormControl(null, [Validators.required]),
      education: new FormControl(null, [Validators.required]),
      workhistory: new FormControl(null, [Validators.required]),
      experience: new FormControl(null, [Validators.required]),
      detailplus: new FormControl(null, [Validators.required]),
      
    })

    this.trainingservice.gettraininglecturer()
    .subscribe(result => {
      this.resulttraining = result
      this.loading = true;
      console.log(this.resulttraining);
    })
  }
  get f() { return this.Form.controls }

  CreateTraining(){
    this.router.navigate(['/training/createtraining'])
  }
  openModal(template: TemplateRef<any>, id) {
    this.delid = id;
    console.log(this.delid);

    this.modalRef = this.modalService.show(template);
  }

  storeTraining(value) {
    console.log(value);
    this.submitted = true;
    if (this.Form.invalid) {
      console.log("in1");
      return;
    } else {


      //alert(JSON.stringify(value))
      this.trainingservice.addTraininglecturer(value).subscribe(response => {
        console.log(value);
        this.modalRef.hide()
        this.Form.reset()
        this.loading = false;
        this.trainingservice.gettraininglecturer()
        .subscribe(result => {
          this.resulttraining = result
          this.loading = true;
          console.log(this.resulttraining);
        })

      })
    }
  }

  ViewModal(template: TemplateRef<any>, id, lecturerName, phone, email, education, workHistory, experience, detailplus) {
    this.delid = id;
    //console.log(this.delid);

    this.modalRef = this.modalService.show(template);
    this.ViewForm = this.fb.group({
      "vlecturertype": new FormControl(null, [Validators.required]),
      "lecturername": new FormControl(null, [Validators.required]),
      "lecturerphone": new FormControl(null, [Validators.required]),
      "lectureremail": new FormControl(null, [Validators.required]),
      "education": new FormControl(null, [Validators.required]),
      "workhistory": new FormControl(null, [Validators.required]),
      "experience": new FormControl(null, [Validators.required]),
      "detailplus": new FormControl(null, [Validators.required]),
    })
    this.ViewForm.patchValue({
      "vlecturertype": 'sdfdsf',
      "lecturername": lecturerName,
      "lecturerphone": phone,
      "lectureremail": email,
      "education": education,
      "workhistory": workHistory,
      "experience": experience,
      "detailplus": detailplus,
    })
  }

  editModal(template: TemplateRef<any>, id, lecturerName, phone, email, education, workHistory, experience, detailplus) {
    this.delid = id;
    //console.log(this.delid);

    this.modalRef = this.modalService.show(template);
    this.EditForm = this.fb.group({
      "LecturerType": new FormControl(null, [Validators.required]),
      "lecturername": new FormControl(null, [Validators.required]),
      "lecturerphone": new FormControl(null, [Validators.required]),
      "lectureremail": new FormControl(null, [Validators.required]),
      "education": new FormControl(null, [Validators.required]),
      "workhistory": new FormControl(null, [Validators.required]),
      "experience": new FormControl(null, [Validators.required]),
      "detailplus": new FormControl(null, [Validators.required]),
    })


    this.EditForm.patchValue({
      "LecturerType": new FormControl(null, [Validators.required]),
      "lecturername": lecturerName,
      "lecturerphone": phone,
      "lectureremail": email,
      "education": education,
      "workhistory": workHistory,
      "experience": experience,
      "detailplus": detailplus,
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
}
