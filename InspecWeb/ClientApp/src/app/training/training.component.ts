import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { TrainingService } from '../services/training.service';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { NotofyService } from '../services/notofy.service';
import { LogService } from '../services/log.service';

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
  Form: any;

  constructor(private modalService: BsModalService, 
    private authorize: AuthorizeService,
    private _NotofyService: NotofyService,
    private spinner: NgxSpinnerService,
    private logService: LogService,
    private fb: FormBuilder, 
    private trainingservice: TrainingService,
    public share: TrainingService, 
    private router: Router,
    @Inject('BASE_URL') baseUrl: string) {
      this.downloadUrl = baseUrl + '/Uploads'
      this.mainUrl = baseUrl
    }

  ngOnInit() {
    this.spinner.show();

    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [2,4,5],
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
          "first": "หน้าแรก",
          "last": "หน้าสุดท้าย",
          "next": "ต่อไป",
          "previous": "ย้อนกลับ"
        },
      }

    };
    
    this.Form = this.fb.group({
      "status": new FormControl(null, [Validators.required]),
    })

    this.trainingservice.gettrainingdata()
    .subscribe(result => {
      this.resulttraining = result
      this.loading = true;
      console.log(this.resulttraining);
      this.spinner.hide();
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

  SettingModal(template: TemplateRef<any>, id, status) {
    this.delid = id;
    //console.log(this.delid);

    this.modalRef = this.modalService.show(template);
    this.Form = this.fb.group({
      "status": new FormControl(null, [Validators.required]),

    })

    this.Form.patchValue({
      "status": status
    })
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


  SettingTraining(value) {
    console.log("SettingTraining =>", value);
    this.trainingservice.SettingTraining(value, this.delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false

      this.trainingservice.gettrainingdata().subscribe(result => {
        this.resulttraining = result
        this.loading = true;
        console.log(this.resulttraining);
      })
    })
  }

  gotoMain(){
    this.router.navigate(['/main'])
  }

  
  gotoPhaseTraining(trainingid){
    this.router.navigate(['/training/phase/', trainingid])
  }

  gotoManageTraining(trainingid){
    this.router.navigate(['/training/manage/', trainingid])
  }

  gotoEditTraining(trainingid){
    this.router.navigate(['/training/edittraining/', trainingid])
  }

  
}
