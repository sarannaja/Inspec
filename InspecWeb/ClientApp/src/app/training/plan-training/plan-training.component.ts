import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { TrainingService } from 'src/app/services/training.service';

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
  dtOptions: DataTables.Settings = {};

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
    this.trainingservice.getTrainingPlan(this.trainingphaseid).subscribe(result => {
      this.resulttrainingplan = result
      this.loading = true
      this.dtOptions = {
        // pagingType: 'full_numbers',
        searching: false,
        pageLength: this.resulttrainingplan.length,
        lengthChange: false,
        info: false,
        paging: false,
        ordering:false,
        // columnDefs: [
        //   {
        //     targets: "_all",
        //     orderable: false,
        //   }
        // ]
      };
      this.spinner.hide();
    })
  }
}
