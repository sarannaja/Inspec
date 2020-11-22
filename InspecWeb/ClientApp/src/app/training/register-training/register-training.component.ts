import { Component, OnInit, TemplateRef } from '@angular/core';
import { TrainingService } from '../../services/training.service';
import { Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-register-training',
  templateUrl: './register-training.component.html',
  styleUrls: ['./register-training.component.css']
})
export class RegisterTrainingComponent implements OnInit {

  resulttraining: any[] = []
  modalRef: BsModalRef;
  delid: any
  loading = false;
  dtOptions: any = {};

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

    this.trainingservice.gettrainingdata()
    .subscribe(result => {
      this.resulttraining = result
      this.loading = true;
      console.log(this.resulttraining);
    })
  }

  GotoRegisterTrainingList(trainingid){
    this.router.navigate(['/training/registertraininglist',trainingid])
  }

}
