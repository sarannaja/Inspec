import { Component, OnInit, Input } from '@angular/core';
import { SubquestionCentralPolicyProvince } from './model';

@Component({
  selector: 'app-subject-detail-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.css']
})
export class ChartComponent implements OnInit {
  @Input() subjectCenProG: SubquestionCentralPolicyProvince;
  subjectCenProGMap

  width = 600;
  height = 400;
  type = "stackedcolumn2d";
  dataFormat = "json";
  dataSource: any = null

  constructor() { }

  ngOnInit() {
    this.subjectCenProGMap = this.subjectCenProG.subjectCentralPolicyProvinceGroups
      .map(result => { return { label: result.provincialDepartment.name, id: result.provincialDepartmentId } });
    console.log('this.subjectCenProG', this.subjectCenProG);

    this.dataSource = {
      // categories: [{ category: subjectCenProGMap.map(re => { return { labal: re.label } }) }],
      categories: [
        {
          category: this.subjectCenProGMap.map(res => ({ label: res.label }))
        }
      ],
      chart: {
        caption: this.subjectCenProG.name,
        subcaption: "จากหน่วยงานทั้งหมดที่ถูกเชิญ " + this.subjectCenProG.name,
        numbersuffix: " คน",
        // showsum: "0",
        "yaxismaxvalue": "10",
        "numdivlines": "4",
        // "decimals": "2",
        "showvalues": "1",
        // "forcedecimals": "1",
        // "yaxisvaluedecimals": "2",
        // "forceyaxisvaluedecimals": "1",
        "formatnumber": "1",
        plottooltext:
          "  <b> $label $dataValue</b> ที่ตอบว่า $seriesName",
        theme: "fusion",
        // drawcrossline: "1",

      },
      // categories:['category'] ,
      dataset: this.subjectCenProG.subquestionChoiceCentralPolicyProvinces
        .map(result => {
          console.log('result', result.subquestionCentralPolicyProvinceId);
          let data = this.subjectCenProG.answerSubquestions.filter(op => result.subquestionCentralPolicyProvinceId == op.subquestionCentralPolicyProvinceId)
          // console.log('data', data);
          let value = data
            .map(or => {
              console.log('or', or);

              return { value: this.subjectCenProGMap.filter(oi => or.user.provincialDepartmentId == oi.id && result.name == or.answer).length.toString() }
            })

          console.log('data', value);

          return {
            seriesname: result.name, data: value
          }
        }),


    }
    console.log('dataSource', this.dataSource);

  }

}
