import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { TrainingService } from 'src/app/services/training.service';
import * as _ from 'lodash';

@Component({
  selector: 'app-plan-training',
  templateUrl: './plan-training.component.html',
  styleUrls: ['./plan-training.component.css']
})
export class PlanTrainingComponent implements OnInit {

  trainingphaseid
  downloadUrl
  resulttrainingplan: any[] = []
  loading = false;
  dtOptions: any = {};
  chars: any[] = []
  constructor(private modalService: BsModalService,
    private fb: FormBuilder,
    private trainingservice: TrainingService,
    public share: TrainingService,
    private router: Router,
    private spinner: NgxSpinnerService,
    private activatedRoute: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string) {
    this.trainingphaseid = activatedRoute.snapshot.paramMap.get('id')
    this.downloadUrl = baseUrl + '/Uploads'
  }

  ngOnInit() {
    this.spinner.show();
    this.getTrainingplan()
  }
  getTrainingplan() {
    this.trainingservice.getTrainingPlantable(this.trainingphaseid).subscribe(result => {
      this.resulttrainingplan = result

      console.log('this.resulttrainingplan =>', this.resulttrainingplan);



      this.chars = result.map(result2 => { return { ...result2, programDate: result2.programDate } })
      this.chars = _.chain(this.chars)
        .groupBy("programDate")
        // `key` is group's name (color), `value` is the array of objects
        .map((value, key) => ({ programDate: key, value: value }))
        .value()
      // chars = _.orderBy(chars, ['programDate'], ['asc']); // Use Lodash to sort array by 'name'

      // this.setState({ characters: chars })
      console.log('char', this.chars);

      this.loading = true
      // this.dtOptions = {
      //   // pagingType: 'full_numbers',
      //   searching: false,
      //   pageLength: this.resulttrainingplan.length,
      //   lengthChange: false,
      //   info: false,
      //   paging: false,
      //   ordering: false,
      //   // columnDefs: [
      //   //   {
      //   //     targets: "_all",
      //   //     orderable: false,
      //   //   }
      //   // ]
      // };
      this.spinner.hide();
    })
  }

  gotoMain() {
    this.router.navigate(['/main'])
  }

  gotoMainTraining() {
    this.router.navigate(['/training'])
  }

  gotoBack() {
    window.history.back();
  }
}
export interface Ngong {
  id: number;
  trainingProgramId: number;
  trainingProgram: TrainingProgram;
  trainingLecturerId: number;
  trainingLecturer: TrainingLecturer;
}

export interface TrainingLecturer {
  id: number;
  lecturerType: number;
  trainingLecturerTypes: null;
  lecturerName: string;
  phone: string;
  imageProfile: string;
  email: string;
  education: string;
  workHistory: string;
  experience: string;
  detailPlus: string;
  createdAt: string;
}

export interface TrainingProgram {
  id: number;
  trainingPhaseId: number;
  trainingPhase: TrainingPhase;
  trainingProgramLoginQRCodes: null;
  programDate: string;
  minuteStartDate: string;
  minuteEndDate: string;
  programType: number;
  trainingProgramTypes: null;
  programTopic: string;
  programDetail: string;
  programLocation: string;
  programToDress: string;
  createdAt: string;
  trainingProgramLecturers: any[];
  trainingProgramFiles: TrainingProgramFile[];
}

export interface TrainingPhase {
  id: number;
  trainingId: number;
  training: null;
  phaseNo: number;
  title: string;
  detail: string;
  startDate: string;
  endDate: string;
  location: string;
  group: number;
  createdAt: string;
}

export interface TrainingProgramFile {
  id: number;
  trainingProgramId: number;
  name: string;
}