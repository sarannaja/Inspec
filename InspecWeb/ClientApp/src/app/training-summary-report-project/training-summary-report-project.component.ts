import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TrainingService } from '../services/training.service';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from "ngx-spinner";
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { NotofyService } from '../services/notofy.service';
import { LogService } from '../services/log.service';

@Component({
  selector: 'app-training-summary-report-project',
  templateUrl: './training-summary-report-project.component.html',
  styleUrls: ['./training-summary-report-project.component.css']
})
export class TrainingSummaryReportProjectComponent implements OnInit {

  trainingid: string
  resulttraining: any[] = []
  modalRef: BsModalRef;
  delid: any
  name: any
  link: any
  loading = false;
  dtOptions: any = {};
  Form: FormGroup;
  EditForm: FormGroup;
  form: FormGroup;
  files: string[] = []
  downloadUrl: string;
  phaseid: string;
  group: string;
  userid: any;
  delname: any;

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
    this.trainingid = activatedRoute.snapshot.paramMap.get('trainingid')
    this.downloadUrl = baseUrl + '/Uploads'
  }

  ngOnInit() {
    this.spinner.show();
    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [4],
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
      detail: new FormControl(null, [Validators.required]),

    })
    this.form = this.fb.group({
      files: [null]
    })

    this.trainingservice.getTrainingSummaryReportProject(this.trainingid)
      .subscribe(result => {
        this.resulttraining = result
        this.loading = true
        console.log("result =>", this.resulttraining);
      })
  }

  openModal(template: TemplateRef<any>, id: any = null) {
    this.delid = id;
    //console.log(this.delid);
    this.modalRef = this.modalService.show(template);
  }

  opendeleteModal(template: TemplateRef<any>, id: any = null, name) {
    this.delid = id;
    this.delname = name;
    //console.log(this.delid);
    this.modalRef = this.modalService.show(template);
  }


  storeTraining(value) {
    console.log("storeTraining =>", value);
    console.log("phaseid =>", this.phaseid);
    console.log("group =>", this.group);
    
    //alert(JSON.stringify(value))
    //alert(this.form.value.files)
    this.trainingservice.addTrainingSummaryReportProject(value, this.form.value.files, this.trainingid).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.logService.addLog(this.userid,'TrainingSummaryReports','เพิ่ม', response.detail, response.id).subscribe();
      this.trainingservice.getTrainingSummaryReportProject(this.trainingid).subscribe(result => {
        this.resulttraining = result
        this.loading = true
        this._NotofyService.onSuccess("เพิ่มข้อมูล");
        //console.log(this.resulttraining);
      })
    })
  }

  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.form.patchValue({
      files: file
    });
    this.form.get('files').updateValueAndValidity()
  }

  addFile(event) {
    this.files = event.target.files
  }
  get f() { return this.Form.controls }
  get d() { return this.f.inputdate as FormArray }

  deleteTraining(id) {
    this.trainingservice.deleteTrainingSummaryReportProject(id).subscribe(response => {
      //console.log(value);
      this.modalRef.hide()
      this.loading = false;
      this.logService.addLog(this.userid,'TrainingSummaryReports','ลบ', this.delname, this.delid).subscribe();
      this.trainingservice.getTrainingSummaryReportProject(this.trainingid).subscribe(result => {
        this.resulttraining = result
        this.loading = true;
        this._NotofyService.onSuccess("ลบข้อมูล");
        //console.log(this.resulttraining);
      })
    })
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

  gotoTrainingManage(){
    this.router.navigate(['/training/manage/', this.trainingid])
  }

}
