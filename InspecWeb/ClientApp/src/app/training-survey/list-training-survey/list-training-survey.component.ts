import { Component, OnInit, TemplateRef, Inject} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TrainingService } from '../../services/training.service';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

import { NotofyService } from '../../services/notofy.service';
import { NgxSpinnerService } from "ngx-spinner";
import { LogService } from '../../services/log.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';


@Component({
  selector: 'app-list-training-survey',
  templateUrl: './list-training-survey.component.html',
  styleUrls: ['./list-training-survey.component.css']
})
export class ListTrainingSurveyComponent implements OnInit {

  surveytopicid: string
  resulttraining: any[] = []
  modalRef: BsModalRef;
  openid: any
  name: any
  surveytype: any
  link: any
  loading = false;
  dtOptions: DataTables.Settings = {};
  Form: FormGroup;
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
      this.surveytopicid = activatedRoute.snapshot.paramMap.get('surveytopicid')
    }
    
    

  ngOnInit() {
    this.getuserinfo();
    this.dtOptions = {
      //pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [1,4],
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
      surveytype: new FormControl(null, [Validators.required]),
      name: new FormControl(null, [Validators.required]),
      
    })

    this.EditForm = this.fb.group({
      surveytype: new FormControl(null, [Validators.required]),
      name: new FormControl(null, [Validators.required]),
    })
    console.log("surveytopicid =>", this.surveytopicid);
    
    this.trainingservice.getlisttrainingsurveydata(this.surveytopicid)
    .subscribe(result => {
      this.resulttraining = result
      this.loading = true
      console.log("resulttraining =>",this.resulttraining);
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

  openModal(template: TemplateRef<any>, id) {
     this.openid = id;
    // console.log(this.delid);

    this.modalRef = this.modalService.show(template);
  }

  storeTraining(value) {
    //alert(JSON.stringify(value))
    console.log(value);
    this.trainingservice.addTrainingsurvey(value, this.surveytopicid).subscribe(response => {
      
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      //this.router.navigate(['/training/surveylist/',trainingid])
      //this.router.navigate(['training'])
      console.log("response =>", response.name, response.id);
      
      this.logService.addLog(this.userid,'TrainingSurveys','เพิ่ม',response.name,response.id).subscribe();
      this.trainingservice.getlisttrainingsurveydata(this.surveytopicid)
      .subscribe(result => {
        this.resulttraining = result
        this.loading = true
        //console.log(this.resulttraining);
        this._NotofyService.onSuccess("เพิ่มข้อมูล")
      })

    })
  }

  deleteTrainingSurvey() {
    console.log(this.openid);
    this.trainingservice.deleteTrainingSurvey(this.openid).subscribe(response => {
      this.modalRef.hide()
      this.loading = false;
      this.logService.addLog(this.userid,'TrainingSurveys','เพิ่ม', this.name, this.openid).subscribe();
      this.trainingservice.getlisttrainingsurveydata(this.surveytopicid).subscribe(result => {
        this.resulttraining = result
        this.loading = true;
        console.log(this.resulttraining);
        this._NotofyService.onSuccess("ลบข้อมูล")
      })
    })
  }

  editModal(template: TemplateRef<any>, id, name, surveytype) {
    this.openid = id;
    this.name = name;
    this.surveytype = surveytype;
   
    //console.log(this.delid);
    //console.log(this.name);
    this.EditForm.patchValue({
      "surveytype": surveytype,
      "name": name,
    })

    this.modalRef = this.modalService.show(template);
    
    
  }

  editTrainingSurvey(value) {
    // alert(JSON.stringify(value));
    // console.clear();
    // console.log("kkkk" + JSON.stringify(value));
    this.trainingservice.editTrainingSurvey(value, this.openid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.logService.addLog(this.userid,'TrainingSurveys','แก้ไข', response.name, response.id).subscribe();
      this.trainingservice.getlisttrainingsurveydata(this.surveytopicid)
      .subscribe(result => {
        this.resulttraining = result
        this.loading = true
        this._NotofyService.onSuccess("แก้ไขข้อมูล")
        //console.log(this.resulttraining);
      })
    })
  }

  gotoBack() {
    window.history.back();
  }


}
