import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { TrainingService } from '../services/training.service';
import { Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-training',
  templateUrl: './training.component.html',
  styleUrls: ['./training.component.css']
})
export class TrainingComponent implements OnInit {

  resulttraining: any[] = []
  modalRef: BsModalRef;
  delid: any
  loading = false;
  dtOptions: DataTables.Settings = {};
  downloadUrl: any;
  mainUrl: string;

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
  CreateTraining(){
    this.router.navigate(['/training/createtraining'])
  }
  openModal(template: TemplateRef<any>, id) {
    this.delid = id;
    console.log(this.delid);

    this.modalRef = this.modalService.show(template);
  }
  deleteTraining(value) {
    this.trainingservice.deleteTraining(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.loading = false;
      this.trainingservice.gettrainingdata().subscribe(result => {
        this.resulttraining = result
        this.loading = true;
        console.log(this.resulttraining);
      })
    })
  }

  
  gotoPhaseTraining(trainingid){
    this.router.navigate(['/training/phase/', trainingid])
  }

  gotoManageTraining(trainingid){
    this.router.navigate(['/training/manage/', trainingid])
  }

}
