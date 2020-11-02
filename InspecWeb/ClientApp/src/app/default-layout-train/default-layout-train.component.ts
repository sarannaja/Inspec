// import { Component, OnInit } from '@angular/core';
import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { TrainingService } from '../services/training.service';
import { Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-default-layout-train',
  templateUrl: './default-layout-train.component.html',
  // template:` <h3>แก้ในไฟล์ ts  </h3>`,
  styleUrls: ['./default-layout-train.component.css']
})
export class DefaultLayoutTrainComponent implements OnInit {

  resulttraining: any[] = []
  modalRef: BsModalRef;
  delid: any;
  loading = false;
  dtOptions: DataTables.Settings = {};
  downloadUrl: any;
  mainUrl: any;
  // constructor() { }
  constructor(private modalService: BsModalService, 
    private fb: FormBuilder, 
    private trainingservice: TrainingService,
    public share: TrainingService, 
    private router: Router,
    @Inject('BASE_URL') baseUrl: string) {
      this.downloadUrl = baseUrl + 'Uploads'
      this.mainUrl = baseUrl
    }

  ngOnInit() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [0,2,3,4],
          orderable: false
        }
      ],
      "language": {
        "lengthMenu": "แสดง  _MENU_  รายการ",
        "search": "ค้นหา:",
        "infoFiltered": "ไม่พบข้อมูล",
        "info": "แสดง _START_ ถึง _END_ จาก _TOTAL_ แถว",
        "infoEmpty": "แสดง 0 ของ 0 รายการ",
        "zeroRecords": "ไม่พบข้อมูล",
        "paginate": {
          "first": "<<",
          "last": ">>",
          "next": ">",
          "previous": "<"
        },
      }

    };

    this.trainingservice.gettrainingdata()
    .subscribe(result => {
      this.resulttraining = result
      this.loading = true;
      console.log(this.resulttraining);
    })
  }

  gotoDetail(trainingid){
    //alert(trainingid);
    this.router.navigate(['/train/detail/',trainingid])
  }

  gotoMain(){
    this.router.navigate(['/train'])
  }

  gotoAdmin(){
   
    this.router.navigate(["/main"])
  }


}
