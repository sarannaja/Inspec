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
  dtOptions: DataTables.Settings = {};
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
    this.trainingservice.getTrainingPlan(this.trainingphaseid).subscribe(result => {
      this.resulttrainingplan = result

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
      this.dtOptions = {
        // pagingType: 'full_numbers',
        searching: false,
        pageLength: this.resulttrainingplan.length,
        lengthChange: false,
        info: false,
        paging: false,
        ordering: false,
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
