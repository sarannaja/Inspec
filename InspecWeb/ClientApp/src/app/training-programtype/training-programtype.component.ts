import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { TrainingService } from '../services/training.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormControl, Validators, FormGroup } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-training-programtype',
  templateUrl: './training-programtype.component.html',
  styleUrls: ['./training-programtype.component.css']
})
export class TrainingProgramTypeComponent implements OnInit {

  resulttraining: any[] = []
  modalRef: BsModalRef;
  editid: any
  loading = false;
  dtOptions: DataTables.Settings = {};
  mainUrl: string;
  trainingid: any;
  Form: FormGroup;
  name: any;
  EditForm: FormGroup;

  constructor(private modalService: BsModalService,
    private fb: FormBuilder,
    private trainingservice: TrainingService,
    public share: TrainingService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string) {
      this.mainUrl = baseUrl
      // this.trainingid = activatedRoute.snapshot.paramMap.get('id')
    }

  ngOnInit() {

    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [2],
          orderable: false
        }
      ]

    };
    this.Form = this.fb.group({
      name: new FormControl(null, [Validators.required]),

    })

    this.trainingservice.getTrainingProgramType()
    .subscribe(result => {
      this.resulttraining = result
      this.loading = true;
    })
  }

  gotoBack() {
    window.history.back();
  }


  openModal(template: TemplateRef<any>, id:any = null) {
    this.editid = id;
    this.modalRef = this.modalService.show(template);
  }

  storeTraining(value) {
    this.trainingservice.addTrainingProgramType(value).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.trainingservice.getTrainingProgramType()
      .subscribe(result => {
        this.resulttraining = result
        this.loading = true
      })

    })
  }

  editModal(template: TemplateRef<any>, id, name) {
    this.editid = id;
    this.name = name;

    this.modalRef = this.modalService.show(template);
    this.EditForm = this.fb.group({
      "name": new FormControl(null, [Validators.required]),
    })
    this.EditForm.patchValue({
      "name": name,
    })
  }

  editTraining(value, editid) {
    this.trainingservice.editTrainingSurvey(value, editid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false

      this.trainingservice.getTrainingProgramType()
      .subscribe(result => {
        this.resulttraining = result
        this.loading = true
      })

    })
  }


  deleteTraining(value) {
    this.trainingservice.deleteTrainingProgramType(value).subscribe(response => {
      this.modalRef.hide()
      this.loading = false;

      this.trainingservice.getTrainingProgramType().subscribe(result => {
        this.resulttraining = result
        this.loading = true;

      })
    })
  }



}
