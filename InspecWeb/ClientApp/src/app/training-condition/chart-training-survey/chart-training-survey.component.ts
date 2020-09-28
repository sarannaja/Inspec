import { Component, OnInit, TemplateRef, Inject} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TrainingService } from '../../services/training.service';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from "ngx-spinner";
import { Chart } from 'chart.js';

@Component({
  selector: 'app-chart-training-survey',
  templateUrl: './chart-training-survey.component.html',
  styleUrls: ['./chart-training-survey.component.css']
})
export class ChartTrainingSurveyComponent2 implements OnInit {

  trainingid: string
  resulttraining: any[] = []
  modalRef: BsModalRef;
  delid: any
  name: any
  link: any
  loading = false;
  dtOptions: DataTables.Settings = {};
  Form: FormGroup;
  EditForm: FormGroup;
  lineChart: any;

  constructor(private modalService: BsModalService,
    private fb: FormBuilder,
    private trainingservice: TrainingService,
    public share: TrainingService,
    private router: Router,
    private spinner: NgxSpinnerService,
    private activatedRoute: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string) {
      this.trainingid = activatedRoute.snapshot.paramMap.get('id')
    }



  ngOnInit() {
    this.spinner.show();
    this.dtOptions = {
      //pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [1,4],
          orderable: false
        }
      ]

    };
    this.Form = this.fb.group({
      name: new FormControl(null, [Validators.required]),

    })

    this.trainingservice.getlisttrainingsurveydata(this.trainingid)
    .subscribe(result => {
      this.resulttraining = result
      this.loading = true
      //console.log(this.resulttraining);
    })




    this.lineChart = new Chart('lineChart', { // สร้าง object และใช้ชื่อ id lineChart ในการอ้างอิงเพื่อนำมาเเสดงผล
      type: 'horizontalBar', // ใช้ชนิดแผนภูมิแบบเส้นสามารถเปลี่ยนชิดได้
      data: { // ข้อมูลภายในแผนภูมิแบบเส้น
          labels: ["หัวข้อที่ 1","หัวข้อที่ 2","หัวข้อที่ 3","หัวข้อที่ 4","หัวข้อที่ 5","หัวข้อที่ 6","หัวข้อที่ 7","หัวข้อที่ 8","หัวข้อที่ 9","หัวข้อที่ 10","หัวข้อที่ 11","หัวข้อที่ 12"], // ชื่อของข้อมูลในแนวแกน x
          datasets: [{ // กำหนดค่าข้อมูลภายในแผนภูมิแบบเส้น
             label: 'Number of items sold in months',
             data: [9,7,3,5,2,10,15,61,19,3,1,9],
             fill: false,
             lineTension: 0.2,
             borderColor: "red", // สีของเส้น
             borderWidth: 1
          }]
      },
      options: {
         title: { // ข้อความที่อยู่ด้านบนของแผนภูมิ
            text: "ประมวณผลทางสถิติ",
            display: true
         }
      },
      // scales: { // แสดง scales ของแผนภูมิเริมที่ 0
      //    yAxes: [{
      //       ticks:{
      //          beginAtZero:true
      //       }
      //    }]
      //  }
   })

  }





  openModal(template: TemplateRef<any>, id) {
     this.delid = id;
    // console.log(this.delid);

    this.modalRef = this.modalService.show(template);
  }

  storeTraining(value) {
    //alert(JSON.stringify(value))
    this.trainingservice.addTrainingsurvey(value, this.trainingid).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      //this.router.navigate(['/training/surveylist/',trainingid])
      //this.router.navigate(['training'])
      this.trainingservice.getlisttrainingsurveydata(this.trainingid)
      .subscribe(result => {
        this.resulttraining = result
        this.loading = true
        //console.log(this.resulttraining);
      })

    })
  }

  deleteTrainingSurvey(value) {
    this.trainingservice.deleteTrainingSurvey(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.loading = false;
      this.trainingservice.getlisttrainingsurveydata(this.trainingid).subscribe(result => {
        this.resulttraining = result
        this.loading = true;
        console.log(this.resulttraining);
      })
    })
  }

  editModal(template: TemplateRef<any>, id, name) {
    this.delid = id;
    this.name = name;
    //console.log(this.delid);
    //console.log(this.name);

    this.modalRef = this.modalService.show(template);
    this.EditForm = this.fb.group({
      "name": new FormControl(null, [Validators.required]),
      // "test" : new FormControl(null,[Validators.required,this.forbiddenNames.bind(this)])
    })
    this.EditForm.patchValue({
      "name": name,
    })
  }

  editTrainingSurvey(value, delid) {
    // alert(JSON.stringify(value));
    // console.clear();
    // console.log("kkkk" + JSON.stringify(value));
    this.trainingservice.editTrainingSurvey(value, delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.trainingservice.getlisttrainingsurveydata(this.trainingid)
      .subscribe(result => {
        this.resulttraining = result
        this.loading = true
        //console.log(this.resulttraining);
      })
    })
  }


}
