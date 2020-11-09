import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { TrainingService } from '../services/training.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormControl, Validators, FormGroup } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { NotofyService } from '../services/notofy.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { LogService } from '../services/log.service';

@Component({
  selector: 'app-training-survey',
  templateUrl: './training-survey.component.html',
  styleUrls: ['./training-survey.component.css']
})
export class TrainingSurveyComponent implements OnInit {

  resulttraining: any[] = []
  modalRef: BsModalRef;
  openid: any
  loading = false;
  dtOptions: DataTables.Settings = {};
  mainUrl: string;
  trainingid: any;
  Form: FormGroup;
  userid: string;
  surveytopic: any;

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
          targets: [3],
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
      name: new FormControl(null, [Validators.required]),

    })

    this.trainingservice.gettrainingsurveycountdata()
    .subscribe(result => {
      this.resulttraining = result
      this.loading = true;
      console.log(this.resulttraining);
      this.spinner.hide();
    })
  }
  
  //start getuser
  getuserinfo() {
    this.spinner.show();
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
      })
  }

  gotoBack() {
    window.history.back();
  }

  GotoSurveyTrainingList(trainingid){
    this.router.navigate(['/training/surveylist/',trainingid])
  }

  GotoPreviewTraining(trainingid){
    this.router.navigate(['/training/survey/preview/',trainingid])
  }

  GotoChartTraining(trainingid){
    this.router.navigate(['/training/survey/chart/',trainingid])
  }

  openModal(template: TemplateRef<any>, id) {

   this.modalRef = this.modalService.show(template);
  }

  openEditModal(template: TemplateRef<any>, id, name) {
    this.openid = id;
    this.surveytopic = name;
   // console.log(this.openid);

  this.Form.patchValue({
    "name": name,
  })

   this.modalRef = this.modalService.show(template);
  }

  

  storeTrainingsurveytopic(value) {
    //alert(JSON.stringify(value))
    this.trainingservice.addTrainingsurveytopic(value).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      //this.router.navigate(['/training/surveylist/',trainingid])
      //this.router.navigate(['training'])
      this.logService.addLog(this.userid,'TrainingSurveyTopics','เพิ่ม',response.name,response.id).subscribe();
      this.trainingservice.gettrainingsurveycountdata()
      .subscribe(result => {
        this.resulttraining = result
        this.loading = true
        this._NotofyService.onSuccess("เพิ่มข้อมูล")
        //console.log(this.resulttraining);
      })

    })
  }

  editTrainingsurveytopic(value) {
    if (value.name != "" && value.name != null && value.name != "null"){
      this.trainingservice.editTrainingsurveytopic(value, this.openid).subscribe(response => {
        this.Form.reset()
        this.modalRef.hide()
        this.loading = false
        this.logService.addLog(this.userid,'TrainingSurveyTopics','แก้ไข',response.name,response.id).subscribe();
        this.trainingservice.gettrainingsurveycountdata()
        .subscribe(result => {
          this.resulttraining = result
          this.loading = true
          this._NotofyService.onSuccess("แก้ไขข้อมูล")
        })

      })
    }
  }



}
