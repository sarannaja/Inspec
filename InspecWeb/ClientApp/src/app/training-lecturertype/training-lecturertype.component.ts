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
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [2],
          orderable: false
        }
      ]

    };
    this.Form = this.fb.group({
      name: new FormControl(null, [Validators.required]),
      
    })

    this.trainingservice.getTrainingLecturerType()
    .subscribe(result => {
      this.resulttraining = result
      this.loading = true;
    })
  }

  gotoBack() {
    window.history.back();
  }


  openModal(template: TemplateRef<any>, id) {
    this.editid = id;
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
        this.Form.reset()
        this.modalRef.hide()
        this.loading = false;
        this.logService.addLog(this.userid,'ประเภทวิทยากร(TrainingLecturerType)','เพิ่ม',value.name,"").subscribe();
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
        this.logService.addLog(this.userid,'ประเภทวิทยากร(TrainingLecturerType)','แก้ไข',value.name,"").subscribe();
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
      this.logService.addLog(this.userid,'ประเภทวิทยากร(TrainingLecturerType)','ลบ',value.name,"").subscribe();
      this.trainingservice.getTrainingLecturerType().subscribe(result => {
        this.resulttraining = result
        this.loading = true;
        this._NotofyService.onSuccess("ลบข้อมูล")
      })
    })
  }



}