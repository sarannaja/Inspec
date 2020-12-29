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
  subjectCenProGMap2
  width = 600;
  height = 400;
  type = "stackedcolumn2d";
  dataFormat = "json";
  dataSource: any = null

  constructor() { }

  // ngOnInit() {
  //   // this.subjectCenProGMap2 = this.subjectCenProG.subquestionChoiceCentralPolicyProvinces
  //   //   .map(result => { return { label: result.name, id: result.id } });
  //   // console.log('this.subjectCenProG', this.subjectCenProG);

  //   // this.subjectCenProGMap = this.subjectCenProG.answerSubquestions
  //   //   .map(result => { return { label: result.answer, id: result.id } })

  //   this.subjectCenProGMap = this.subjectCenProG.subjectCentralPolicyProvinceGroups
  //     .map(result => { return { label: result.provincialDepartment.name, id: result.provincialDepartmentId } });
  //   console.log('this.subjectCenProG', this.subjectCenProG);
  //   this.dataSource = {
  //     // categories: [{ category: subjectCenProGMap.map(re => { return { labal: re.label } }) }],
  //     categories: [
  //       {
  //         category: this.subjectCenProGMap.map(res => ({ label: res.label }))
  //       }
  //     ],
  //     chart: {
  //       caption: this.subjectCenProG.name,
  //       subcaption: "จากหน่วยงานทั้งหมดที่ถูกเชิญ " + this.subjectCenProG.name,
  //       numbersuffix: " คน",
  //       // showsum: "0",
  //       "yaxismaxvalue": "5",
  //       "numdivlines": "2",
  //       // "decimals": "2",
  //       "showvalues": "1",
  //       // "forcedecimals": "1",
  //       // "yaxisvaluedecimals": "2",
  //       // "forceyaxisvaluedecimals": "1",
  //       "formatnumber": "1",
  //       plottooltext:
  //         "  <b> $label $seriesName</b> ที่ตอบว่า $dataValue",
  //       theme: "fusion",
  //       // drawcrossline: "1",

  //     },
  //     // categories:['category'] ,
  //     dataset: this.subjectCenProG.subquestionChoiceCentralPolicyProvinces
  //       .map(result => {
  //         console.log('result', result.subquestionCentralPolicyProvinceId);
  //         let data = this.subjectCenProG.answerSubquestions.filter(op => result.subquestionCentralPolicyProvinceId == op.subquestionCentralPolicyProvinceId)
  //         // console.log('data', data);
  //         let value = data
  //           .map(or => {
  //             console.log('or', or);

  //             return { value: this.subjectCenProGMap.filter(oi => or.user.provincialDepartmentId == oi.id && result.name == or.answer).length.toString() }
  //           })

  //         console.log('data', value);

  //         return {
  //           seriesname: result.name, data: value
  //         }
  //       }),

  //     // dataset: this.subjectCenProG.subjectCentralPolicyProvinceGroups
  //     //   .map(result => {
  //     //     console.log('result', result);
  //     //     let data = this.subjectCenProG.answerSubquestions.filter(op => result.subquestionCentralPolicyProvinceId == op.subquestionCentralPolicyProvinceId)
  //     //     // console.log('data', data);
  //     //     let value = data
  //     //       .map(or => {
  //     //         console.log('or', or);
  //     //         console.log("subjectCenProGMap", this.subjectCenProGMap);

  //     //         return { value: this.subjectCenProGMap.filter(oi => or.id == oi.id && result.provincialDepartment.name == or.user.provincialDepartments.name).length.toString() }
  //     //       })

  //     //     console.log("value: ", value);
  //     //     let value2: any = [];

  //     //     value.forEach(element => {
  //     //       if (element.value == "0") {
  //     //         value2.push({
  //     //           value: '"showValue":"0"'
  //     //         })
  //     //       } else {
  //     //         value2.push({
  //     //           value: element.value
  //     //         })
  //     //       }
  //     //     });

  //     //     console.log("value2 : ", value2);

  //     //     return {
  //     //       seriesname: result.provincialDepartment.name, data: value2
  //     //     }

  //     //   })

  //   }
  //   console.log('dataSource', this.dataSource);

  // }

  ngOnInit() {
    console.log("subjectCenProG", this.subjectCenProG);
    this.subjectCenProGMap = this.subjectCenProG.subquestionChoiceCentralPolicyProvinces
      .map(result => { return { label: result.name, id: result.id } });
    console.log("subjectCenProGMap", this.subjectCenProGMap);
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
        "yaxismaxvalue": "5",
        "numdivlines": "2",
        // "decimals": "2",
        "showvalues": "1",
        "formatnumber": "1",
        plottooltext:
          "  <b> $label $seriesName</b> ที่ตอบว่า $dataValue",
        theme: "fusion",
        // drawcrossline: "1",

      },
      // categories:['category'] ,
      dataset: this.subjectCenProG.subjectCentralPolicyProvinceGroups //ตัวเลือกคำตอบ
        .map(result => {
          // console.log('result', result.subquestionCentralPolicyProvinceId);
          let data = this.subjectCenProG.answerSubquestions.filter(op => result.subquestionCentralPolicyProvinceId == op.subquestionCentralPolicyProvinceId)
          console.log('data', data);
          let value = this.subjectCenProGMap
            .map(or => {
              // console.log('or', or);

              return { value: data.filter(oi => oi.answer == or.label && oi.user.provincialDepartments.name == result.provincialDepartment.name).length.toString() }
            })
          let value2: any = [];

          value.forEach(element => {
            if (element.value == "0") {
              value2.push({
                value: '"showValue":"0"'
              })
            } else {
              value2.push({
                value: element.value
              })
            }
          });

          console.log("value2 : ", value2);

          return {
            seriesname: result.provincialDepartment.name, data: value2
          }
        }),

    }
    console.log('dataSource', this.dataSource);

  }

}
