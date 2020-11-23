import { Component, Inject, OnInit, TemplateRef } from '@angular/core';
import { TrainingService } from '../../services/training.service';
import { Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ExportReportService } from 'src/app/services/export-report.service';

@Component({
  selector: 'app-list-training-report',
  templateUrl: './list-training-report.component.html',
  styleUrls: ['./list-training-report.component.css']
})
export class ListTrainingReportComponent implements OnInit {

  resulttraining: any[] = []
  modalRef: BsModalRef;
  delid: any
  loading = false;
  dtOptions: any = {};
  url: any;

  constructor(private modalService: BsModalService,
    private fb: FormBuilder,
    private trainingservice: TrainingService,
    public share: TrainingService,
    private router: Router,
    private exportService: ExportReportService,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.url = baseUrl;
  }

  ngOnInit() {

    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [4, 5],
          orderable: false
        }
      ]

    };

    this.trainingservice.gettrainingregistercountdata()
      .subscribe(result => {
        this.resulttraining = result
        this.loading = true;
        console.log("list training: ", this.resulttraining);
      })
  }

  GotoRegisterTrainingList(trainingid) {
    this.router.navigate(['/training/report/list/', trainingid])
  }

  gotoBack() {
    window.history.back();
  }

  gotoMain(){
    this.router.navigate(['/main'])
  }

  gotoMainTraining(){
    this.router.navigate(['/training'])
  }

  // gotoTrainingManage(){
  //   this.router.navigate(['/training/manage/', this.trainingid])
  // }

  printReport() {
    this.exportService.reportTrainingList(this.resulttraining).subscribe(res => {
      window.open(this.url + "Uploads/" + res.data);
      console.log(res);
    })
  }

}
