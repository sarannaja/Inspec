import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { TrainingService } from 'src/app/services/training.service';
import * as _ from 'lodash';
import * as Chart from 'chart.js';

@Component({
  selector: 'app-training-processing',
  templateUrl: './training-processing.component.html',
  styleUrls: ['./training-processing.component.css']
})
export class TrainingProcessingComponent implements OnInit {

  trainingLecturerJoinSurveysId
  downloadUrl
  resulttrainingplan: any[] = []
  loading = false;
  dtOptions: DataTables.Settings = {};
  chars: any[] = []
  lineChart: any;
  surveylikelineChart: any;
  horizontalBarChartData: any;
  surveyLikeBarChartData: any;
  resulttraining: any[];
  resulttrainingsurvey: any[];
  inputtrainingsurveyanswerYesOrNoTopic: any[];
  inputtrainingsurveyanswerYes: any[];
  inputtrainingsurveyanswerNo: any[];
  inputtrainingsurveyanswerName: any[];
  inputtrainingsurveyanswerScore: any[];
  constructor(private modalService: BsModalService,
    private fb: FormBuilder,
    private trainingservice: TrainingService,
    public share: TrainingService,
    private router: Router,
    private spinner: NgxSpinnerService,
    private activatedRoute: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string) {
    this.trainingLecturerJoinSurveysId = activatedRoute.snapshot.paramMap.get('id')
    this.downloadUrl = baseUrl + '/Uploads'
  }

  ngOnInit() {
    this.spinner.show();
    this.getTrainingProcessingAnswerOpen();
    this.getChartAnswerScore();


    //this.trainingservice.getlisttrainingsurveydata(1)
    this.trainingservice.getprocessingYestrainingsurvey(this.trainingLecturerJoinSurveysId)
    .subscribe(result => {
      this.resulttraining = result
      this.loading = true
      console.log("resulttraining =>", this.resulttraining);
      
      this.inputtrainingsurveyanswerYesOrNoTopic = this.resulttraining.map(
        (item, index) => {
          return item.surveyName
        }
      )

      this.inputtrainingsurveyanswerYes = this.resulttraining.map(
        (item, index) => {
          return item.ansYesSum
        }
      )

      this.inputtrainingsurveyanswerNo = this.resulttraining.map(
        (item, index) => {
          return item.ansNoSum
        }
      )
      console.log("inputtrainingsurveyanswerYesOrNoTopic =>", this.inputtrainingsurveyanswerYesOrNoTopic);
      console.log("inputtrainingsurveyanswerYes =>", this.inputtrainingsurveyanswerYes);
      console.log("inputtrainingsurveyanswerNo =>", this.inputtrainingsurveyanswerNo);
      console.log("inputtrainingsurveyanswer =>", this.inputtrainingsurveyanswerYesOrNoTopic);

      var MONTHS = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
      var color = Chart.helpers.color;
      this.horizontalBarChartData = {
      //labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
      //หัวข้อประเมิน
      labels: this.inputtrainingsurveyanswerYesOrNoTopic,
			datasets: [{
				label: 'ใช่',
        //backgroundColor: color(window.chartColors.red).alpha(0.5).rgbString(),
        backgroundColor: "green",
				borderColor: "green",
				borderWidth: 1,
        //data: [5,17,3,50,12],
        data: this.inputtrainingsurveyanswerYes,
			}, {
				label: 'ไม่ใช่',
        //backgroundColor: color(window.chartColors.blue).alpha(0.5).rgbString(),
        backgroundColor: "red",
				borderColor: "red",
        //data: [20,7,3,5,2],
        data: this.inputtrainingsurveyanswerNo,
			}]

    };

      console.log("horizontalBarChartData =>", this.horizontalBarChartData);

      this.lineChart = new Chart('lineChart', { // สร้าง object และใช้ชื่อ id lineChart ในการอ้างอิงเพื่อนำมาเเสดงผล
        type: 'horizontalBar', // ใช้ชนิดแผนภูมิแบบเส้นสามารถเปลี่ยนชิดได้
        data: this.horizontalBarChartData,
        //{ // ข้อมูลภายในแผนภูมิแบบเส้น
            //labels: ["หัวข้อที่ 1","หัวข้อที่ 2","หัวข้อที่ 3","หัวข้อที่ 4","หัวข้อที่ 5","หัวข้อที่ 6","หัวข้อที่ 7","หัวข้อที่ 8","หัวข้อที่ 9","หัวข้อที่ 10","หัวข้อที่ 11","หัวข้อที่ 12"], // ชื่อของข้อมูลในแนวแกน x
            //labels: [this.inputtrainingsurveyanswer], // ชื่อของข้อมูลในแนวแกน x
            // datasets: [{ // กำหนดค่าข้อมูลภายในแผนภูมิแบบเส้น
            //   label: 'Number of items sold in months',
            //   data: [9,7,3,5,2,10,15,61,19,3,1,9],
            //   fill: false,
            //   lineTension: 0.2,
            //   borderColor: "red", // สีของเส้น
            //   borderWidth: 1
            // }]
        // },
        // options: {
        //   title: { // ข้อความที่อยู่ด้านบนของแผนภูมิ
        //       text: "ประมวณผลทางสถิติ",
        //       display: true
        //   }
        // }
      })

    })



    this.trainingservice.getprocessingLiketrainingsurvey(this.trainingLecturerJoinSurveysId)
      .subscribe(result => {
        this.resulttrainingsurvey = result
        this.loading = true
        console.log("resulttrainingsurvey =>", this.resulttrainingsurvey);

        this.inputtrainingsurveyanswerName = this.resulttrainingsurvey.map(
          (item, index) => {
            return item.surveyName
          }
        )

        this.inputtrainingsurveyanswerScore = this.resulttrainingsurvey.map(
          (item, index) => {
            return item.scoreSum
          }
        )

        this.surveyLikeBarChartData = {
          //labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
          //หัวข้อประเมิน
          labels: this.inputtrainingsurveyanswerName,
          datasets: [{
            label: this.inputtrainingsurveyanswerName,
            //backgroundColor: color(window.chartColors.red).alpha(0.5).rgbString(),
            backgroundColor: "green",
            borderColor: "green",
            borderWidth: 1,
            //data: [5,17,3,50,12],
            data: this.inputtrainingsurveyanswerScore,
          }]
    
        };

        
      this.surveylikelineChart = new Chart('lineChart2', { // สร้าง object และใช้ชื่อ id lineChart ในการอ้างอิงเพื่อนำมาเเสดงผล
        type: 'horizontalBar', // ใช้ชนิดแผนภูมิแบบเส้นสามารถเปลี่ยนชิดได้
        data: this.surveyLikeBarChartData,
        // { // ข้อมูลภายในแผนภูมิแบบเส้น
        //     labels: ["หัวข้อที่ 1","หัวข้อที่ 2","หัวข้อที่ 3","หัวข้อที่ 4","หัวข้อที่ 5","หัวข้อที่ 6","หัวข้อที่ 7","หัวข้อที่ 8","หัวข้อที่ 9","หัวข้อที่ 10","หัวข้อที่ 11","หัวข้อที่ 12"], // ชื่อของข้อมูลในแนวแกน x
        //     //labels: [this.inputtrainingsurveyanswer], // ชื่อของข้อมูลในแนวแกน x
        //     datasets: [{ // กำหนดค่าข้อมูลภายในแผนภูมิแบบเส้น
        //       label: 'Number of items sold in months',
        //       data: [9,7,3,5,2,10,15,61,19,3,1,9],
        //       fill: false,
        //       lineTension: 0.2,
        //       borderColor: "red", // สีของเส้น
        //       borderWidth: 1
        //     }]
        // },
        // options: {
        //   title: { // ข้อความที่อยู่ด้านบนของแผนภูมิ
        //       text: "ประมวณผลทางสถิติ",
        //       display: true
        //   }
        // }
      })
    })
    
  


  }

  getTrainingProcessingAnswerOpen() {
    this.trainingservice.getprocessingOpentrainingsurvey(this.trainingLecturerJoinSurveysId).subscribe(result => {
      this.resulttrainingplan = result

      this.chars = result.map(result2 => { return { ...result2, SurveyTopic: result2.trainingSurvey.name } })
      this.chars = _.chain(this.chars)
        .groupBy("SurveyTopic")
        // `key` is group's name (color), `value` is the array of objects
        .map((value, key) => ({ SurveyTopic: key, value: value  }))
        .value()
      // chars = _.orderBy(chars, ['programDate'], ['asc']); // Use Lodash to sort array by 'name'

      // this.setState({ characters: chars })
      console.log('chars', this.chars);


      this.loading = true
      this.dtOptions = {
        // pagingType: 'full_numbers',
        searching: false,
        pageLength: this.resulttrainingplan.length,
        lengthChange: false,
        info: false,
        paging: false,
        ordering: false,
        // columnDefs: [
        //   {
        //     targets: "_all",
        //     orderable: false,
        //   }
        // ]
      };
      this.spinner.hide();
    })
  }

  getChartAnswerScore() {
    
  }

  gotoBack() {
    window.history.back();
  }
}