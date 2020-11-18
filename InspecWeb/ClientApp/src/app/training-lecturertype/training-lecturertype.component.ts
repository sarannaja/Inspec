import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { TrainingService } from '../services/training.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormControl, Validators, FormGroup } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

import { NotofyService } from '../services/notofy.service';
import { LogService } from '../services/log.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-training-lecturertype',
  templateUrl: './training-lecturertype.component.html',
  styleUrls: ['./training-lecturertype.component.css']
})
export class TrainingLecturerTypeComponent implements OnInit {

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
  userid: string;

  constructor(private modalService: BsModalService,
    private authorize: AuthorizeService,
    private _NotofyService: NotofyService,
    private spinner: NgxSpinnerService,
    private logService: LogService,
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
    this.getuserinfo();
    this.dtOptions = {
      columnDefs: [
        {
          targets: [2],
          orderable: false
        }
      ],
      pagingType: 'full_numbers',
      "language": {
        "lengthMenu": "แสดง  _MENU_  รายการ",
        "search": "ค้นหา:",
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
      name: new FormControl(null, [Validators.required]),

    })

    this.trainingservice.getTrainingLecturerType()
    .subscribe(result => {
      this.resulttraining = result
      this.loading = true;
      this.spinner.hide();
    })
  }

  gotoMain(){
    this.router.navigate(['/main'])
  }

  gotoBack() {
    window.history.back();
  }


  openModal(template: TemplateRef<any>, id:any = null, name:any = null) {
    this.editid = id;
    this.name = name;
    this.modalRef = this.modalService.show(template);
  }

  //start getuser
  getuserinfo() {
    this.spinner.show();
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
      })
  }

  storeTraining(value) {

    if (value.name != "" && value.name != null && value.name != "null"){
      this.trainingservice.addTrainingLecturerType(value).subscribe(response => {
        this.logService.addLog(this.userid,'TrainingLecturerType','เพิ่ม',response.name,response.id).subscribe();
        this.Form.reset()
        this.modalRef.hide()
        this.loading = false;
        this.trainingservice.getTrainingLecturerType()
        .subscribe(result => {
          this.resulttraining = result
          this.loading = true
          this._NotofyService.onSuccess("เพิ่มข้อมูล")
        })

      })

    }
    this.modalRef.hide()


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
    if (value.name != "" && value.name != null && value.name != "null"){
      this.trainingservice.editTrainingLecturerType(value, editid).subscribe(response => {
        this.Form.reset()
        this.modalRef.hide()
        this.loading = false
        this.logService.addLog(this.userid,'TrainingLecturerType','แก้ไข',response.name,response.id).subscribe();
        this.trainingservice.getTrainingLecturerType()
        .subscribe(result => {
          this.resulttraining = result
          this.loading = true
          this._NotofyService.onSuccess("แก้ไขข้อมูล")
        })

      })
    }
  }


  deleteTraining(value) {
    this.trainingservice.deleteTrainingLecturerType(value).subscribe(response => {
      this.modalRef.hide()
      this.loading = false;
      this.logService.addLog(this.userid,'TrainingLecturerType','ลบ',this.name,this.editid).subscribe();
      this.trainingservice.getTrainingLecturerType().subscribe(result => {
        this.resulttraining = result
        this.loading = true;
        this._NotofyService.onSuccess("ลบข้อมูล")
      })
    })
  }



}
