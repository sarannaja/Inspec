import { Component, OnInit, TemplateRef } from '@angular/core';
import { TrainingService } from '../services/training.service';
import { Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-training-register',
  templateUrl: './training-register.component.html',
  styleUrls: ['./training-register.component.css']
})
export class TrainingRegisterComponent implements OnInit {

  resulttraining: any[] = []
  modalRef: BsModalRef;
  delid: any
  loading = false;
  dtOptions: DataTables.Settings = {};

  constructor(private modalService: BsModalService, private fb: FormBuilder, private trainingservice: TrainingService,
    public share: TrainingService, private router: Router) { }

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

    this.trainingservice.gettrainingregistercountdata()
    .subscribe(result => {
      this.resulttraining = result
      this.loading = true;
      console.log(this.resulttraining);
    })
  }

  GotoRegisterTrainingList(trainingid){
    this.router.navigate(['/training/registerlist/',trainingid])
  }

  GotoRegisterTrainingProgram(trainingid){
    this.router.navigate(['training/register/program/',trainingid])
  }
}
