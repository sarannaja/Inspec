import { Component, OnInit, TemplateRef, Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { InspectionplaneventService } from '../services/inspectionplanevent.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Calendar } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import listPlugin from '@fullcalendar/list';
import * as _ from 'lodash';
import * as moment from 'moment';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
declare var $: any;
@Injectable({
  providedIn: 'root'
})

@Component({
  selector: 'app-calendar-user',
  templateUrl: './calendar-user.component.html',
  styleUrls: ['./calendar-user.component.css']
})
export class CalendarUserComponent implements OnInit {
  url = "";
  resultinspectionplanevent: any = []
  inspectionplancalendar: any = []
  delid: any
  modalRef: BsModalRef;
  userid: string


  constructor(private router: Router, private inspectionplanservice: InspectionplaneventService,
    private authorize: AuthorizeService,
    private modalService: BsModalService, @Inject('BASE_URL') baseUrl: string) {
    // this.url = baseUrl + 'centralpolicy/detailcentralpolicyprovince/';
    this.url = baseUrl + 'usercentralpolicy/';
  }

  ngOnInit() {
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        //console.log(result);
        // alert(this.userid)
      })

    // this.inspectionplanservice.getinspectionplaneventuserdata(this.userid)
    //   .subscribe(result => {
    //     // //console.log("res: ", result);
    //     this.resultinspectionplanevent = result;

    //     var distinctThings: any[] = result.filter(
    //       (thing, i, arr) => arr.findIndex(t => t.id === thing.id) === i
    //     );

    //     //console.log("unique: ", distinctThings);
    //     // this.inspectionplancalendar = distinctThings;

    //     // var test: any = [];

    //     // distinctThings.forEach(element => {
    //     //   element.centralPolicyEvents.forEach(element2 => {
    //     //     element2.centralPolicy.centralPolicyUser.forEach(element3 => {
    //     //       if (element3.userId == this.userid) {
    //     //         // //console.log("ELEMENT3: ", element3);

    //     //       }
    //     //     });
    //     //   });
    //     // });

    //     var j = 0;
    //     this.inspectionplancalendar = distinctThings.map((item, indexo) => {
    //       // var parentArray = [{ item: 'one', children: [1, 2, 3] }, { item: 'two', children: [4, 5, 6] }];
    //       // //console.log("item: ", item.centralPolicyEvents);

    //       var dest;
    //       var test = 0;


    //       var element = item.centralPolicyEvents;
    //       // //console.log('element', element);

    //       var element2: any[] = [];
    //       for (let index2 = 0; index2 < element.length; index2++) {
    //         element2 = element[index2].centralPolicy.centralPolicyUser.filter(elementTest => {
    //           return elementTest.userId == this.userid
    //           // if () {
    //           //   element2.push(elementTest);
    //           // }
    //         });
    //       }
    //       var colorJa: any;
    //       console.log("T:" + ((((j + element2.length) - 1) - element.length) - element2.length), element2);
    //       // console.log("T:" + ((((j + element.length) - 1) - ((j + element2.length) - 1)) + j), element2);
    //       // // return  {data:element2,startDate:item.startDate,endDate:item.endDate}
    //       // test++
    //       // if (test == 0) {
    //       for (var i = 0; i <= j; i++) {
    //         // console.log(element2[((j + element.length) - 1)].status);
    //         //console.log("jjjj", j);
    //         if (((((j + element.length) - 1) - ((j + element2.length) - 1)) + j) < 0) {
    //           if (element2[((j + element.length) - 1)].status == "รอการตอบรับ") {
    //             // test = 1;
    //             colorJa = "#FFD700" //yellow
    //           } else if (element2[((j + element.length) - 1)].status == "ตอบรับ") {
    //             // test = 1;
    //             colorJa = "#008000" //green
    //           } else if (element2[((j + element.length) - 1)].status == "ปฏิเสธ") {
    //             // test = 1;
    //             colorJa = "#C70039" //red
    //           } else if (element2[((j + element.length) - 1)].status == "มอบหมาย") {
    //             // test = 1;
    //             colorJa = "#008000" //green
    //           }
    //         } else if (((((j + element2.length) - 1) - element.length) - element2.length) != element2.length) {
    //           if (element2[((((j + element2.length) - 1) - element.length) - element2.length)].status == "รอการตอบรับ") {
    //             // test = 1;
    //             colorJa = "#FFD700" //yellow
    //           } else if (element2[((((j + element2.length) - 1) - element.length) - element2.length)].status == "ตอบรับ") {
    //             // test = 1;
    //             colorJa = "#008000" //green
    //           } else if (element2[((((j + element2.length) - 1) - element.length) - element2.length)].status == "ปฏิเสธ") {
    //             // test = 1;
    //             colorJa = "#C70039" //red
    //           } else if (element2[((((j + element2.length) - 1) - element.length) - element2.length)].status == "มอบหมาย") {
    //             // test = 1;
    //             colorJa = "#008000" //green
    //           }
    //         } else {
    //           if (element2[(((((j + element2.length) - 1) - element.length) - element2.length) - 1)].status == "รอการตอบรับ") {
    //             // test = 1;
    //             colorJa = "#FFD700" //yellow
    //           } else if (element2[(((((j + element2.length) - 1) - element.length) - element2.length) - 1)].status == "ตอบรับ") {
    //             // test = 1;
    //             colorJa = "#008000" //green
    //           } else if (element2[(((((j + element2.length) - 1) - element.length) - element2.length) - 1)].status == "ปฏิเสธ") {
    //             // test = 1;
    //             colorJa = "#C70039" //red
    //           } else if (element2[(((((j + element2.length) - 1) - element.length) - element2.length) - 1)].status == "มอบหมาย") {
    //             // test = 1;
    //             colorJa = "#008000" //green
    //           }
    //         }

    //       }
    //       j++
    //       // j=element2.length - j;
    //       // }
    //       // test = 0
    //       // element2.filter((item2) => {
    //       //   // if (test == 0) {
    //       //   //   if (item2.status == "รอการตอบรับ") {
    //       //   //     test = 1;
    //       //   //     colorJa = "#C70039" //red
    //       //   //   } else if (item2.status == "ตอบรับ") {
    //       //   //     test = 0;
    //       //   //     colorJa = "#008000" //green
    //       //   //   } else if (item2.status == "ปฎิเสธ") {
    //       //   //     colorJa = "#008000" //green
    //       //   //   } else if (item2.status == "มอบหมาย") {
    //       //   //     colorJa = "#008000" //green
    //       //   //   }
    //       //   // }
    //       //   if (test == 0) {
    //       //     if (item2.status == "รอการตอบรับ") {
    //       //       test = 1;
    //       //       colorJa = "#FFD700" //yellow
    //       //     } else if (item2.status == "ตอบรับ") {
    //       //       test = 1;
    //       //       colorJa = "#008000" //green
    //       //     } else if (item2.status == "ปฎิเสธ") {
    //       //       test = 1;
    //       //       colorJa = "#C70039" //red
    //       //     } else if (item2.status == "มอบหมาย") {
    //       //       test = 1;
    //       //       colorJa = "#008000" //green
    //       //     }
    //       //   }
    //       // })

    //       // //console.log("element2: ", element2);
    //       return {
    //         id: item.id,
    //         title: item.province.name,
    //         provinceid: item.province.id,
    //         // id: item.centralPolicyEvents[0].centralPolicy.centralPolicyProvinces[0].id,
    //         // title: item.province.name + ", " + item.centralPolicyEvents[0].centralPolicy.title,
    //         start: moment(item.startDate), //.format("YYYY-MM-DD"),
    //         end: moment(item.endDate).add(1, 'days'), //.format("YYYY-MM-DD"),
    //         // status: test,
    //         color: colorJa
    //         // element2[1].status == "รอการตอบรับ" ?"#FFD700" :"#008000"
    //       }
    //     })
    //     console.log('res: ',
    //       this.inspectionplancalendar

    //     );

    //     // var testtest:any[] =  this.inspectionplancalendar[0].data
    //     // .map(result => {
    //     //   var colorJa: any;
    //     //   //console.log('result.startDate',result);

    //     //   if (result.status == "รอการตอบรับ") {
    //     //     // test = 1;
    //     //     colorJa = "#FFD700" //yellow
    //     //   } else if (result.status == "ตอบรับ") {
    //     //     // test = 1;
    //     //     colorJa = "#008000" //green
    //     //   } else if (result.status == "ปฎิเสธ") {
    //     //     // test = 1;
    //     //     colorJa = "#C70039" //red
    //     //   } else if (result.status == "มอบหมาย") {
    //     //     // test = 1;
    //     //     colorJa = "#008000" //green
    //     //   }
    //     //   return {
    //     //     id: result.id,
    //     //     title: result.province.name,
    //     //     provinceid: result.province.id,
    //     //     // id: result.centralPolicyEvents[0].centralPolicy.centralPolicyProvinces[0].id,
    //     //     // title: result.province.name + ", " + result.centralPolicyEvents[0].centralPolicy.title,
    //     //     start: moment( this.inspectionplancalendar[0].startDate), //.format("YYYY-MM-DD"),
    //     //     end: moment( this.inspectionplancalendar[1].endDate).add(1, 'days'), //.format("YYYY-MM-DD"),
    //     //     // status: test,
    //     //     color: colorJa
    //     //   }
    //     // })

    //     // this.inspectionplancalendar = testtest


    //     // this.inspectionplancalendar = this.inspectionplancalendar.map((item, index) => {
    //     //   return {
    //     //     id: item.id,
    //     //     title: item.province.name,
    //     //     provinceid: item.province.id,
    //     //     // id: item.centralPolicyEvents[0].centralPolicy.centralPolicyProvinces[0].id,
    //     //     // title: item.province.name + ", " + item.centralPolicyEvents[0].centralPolicy.title,
    //     //     start: moment(item.startDate), //.format("YYYY-MM-DD"),
    //     //     end: moment(item.endDate).add(1, 'days'), //.format("YYYY-MM-DD"),
    //     //     status:

    //     //         }
    //     //       })
    //     //     })
    //     //   }})

    //     // alert(JSON.stringify(this.inspectionplancalendar))
    //     this.getcalendar();
    //   })
    this.inspectionplanservice.getinspectionplaneventuserdata(this.userid)
      .subscribe(result => {
        // //console.log("res: ", result);
        this.resultinspectionplanevent = result;

        // var distinctThings: any[] =
        this.inspectionplancalendar = result.filter(
          (thing, i, arr) => arr.findIndex(t => t.inspectionPlanEventId === thing.inspectionPlanEventId) === i
        ).map(item2=>{
          var colorJa: any;
          if (item2.status == "รอการตอบรับ") {
            // test = 1;
            colorJa = "#FFD700" //yellow
          } else if (item2.status == "ตอบรับ") {
            // test = 1;
            colorJa = "#008000" //green
          } else if (item2.status == "ปฎิเสธ") {
            // test = 1;
            colorJa = "#C70039" //red
          } else if (item2.status == "มอบหมาย") {
            // test = 1;
            colorJa = "#008000" //green
          }
                 return {
            id: item2.inspectionPlanEventId,
            title: item2.province.name,
            provinceid: item2.provinceId,
            // id: result.centralPolicyEvents[0].centralPolicy.centralPolicyProvinces[0].id,
            // title: result.province.name + ", " + result.centralPolicyEvents[0].centralPolicy.title,
            start: moment(item2.inspectionPlanEvent.startDate), //.format("YYYY-MM-DD"),
            end: moment(item2.inspectionPlanEvent.endDate).add(1, 'days'), //.format("YYYY-MM-DD"),
            // status: test,
            color: colorJa
          }
        });


        this.getcalendar();
      })
  }

  getdata() {
    this.inspectionplanservice.getinspectionplaneventuserdata(this.userid)
      .subscribe(result => {
        this.resultinspectionplanevent = result

        this.inspectionplancalendar = result

        this.inspectionplancalendar = this.inspectionplancalendar.map((item, index) => {
          return {
            id: item.id,
            title: item.province.name,
            provinceid: item.province.id,
            // id: item.centralPolicyEvents[0].centralPolicy.centralPolicyProvinces[0].id,
            // title: item.province.name + "," + item.centralPolicyEvents[0].centralPolicy.title,
            start: moment(item.startDate), //.format("YYYY-MM-DD"),
            end: moment(item.endDate).add(1, 'days') //.format("YYYY-MM-DD"),
          }
        })
        this.getcalendar();
      })
  }

  getcalendar() {
    // var url_to_inspection = this.url
    // var calendarEl = document.getElementById('calendar');
    // var calendar = new Calendar(calendarEl, {
    //   events: this.inspectionplancalendar,
    //   plugins: [dayGridPlugin, timeGridPlugin, listPlugin],

    //   eventClick: function (info) {
    //     window.open(url_to_inspection + info.event.id);
    //   },

    //   eventMouseEnter: function (info) {
    //   },

    // });
    // calendar.render();
    setTimeout(() => {
      //console.log("INSPEC: ", this.inspectionplancalendar);
      var url_to_inspection = this.url
      $("#calendar").fullCalendar({
        events: this.inspectionplancalendar,
        header: {
          left: 'prev,next today',
          center: 'title',
          right: 'month,agendaWeek,agendaDay'
        },
        navLinks: true,
        editable: false,
 locale: 'th',
        viewRender: function (view, element) {
          setTimeout(function () {
            var strDate = $.trim($(".fc-center").find("h2").text());
            var arrDate = strDate.split(" ");
            var lengthArr = arrDate.length;
            var newstrDate = "";
            for (var i = 0; i < lengthArr; i++) {
              if (lengthArr - 1 == i || parseInt(arrDate[i]) > 1000) {
                var yearBuddha = parseInt(arrDate[i]) + 543;
                newstrDate += yearBuddha;
              } else {
                newstrDate += arrDate[i] + " ";
              }
            }
            $(".fc-center").find("h2").text(newstrDate);
          }, 5);
        },
        eventLimit: false,
        // eventBackgroundColor: "#DC143C",
        // eventBorderColor: "#DC143C",
        eventClick: function (event) {
          window.location.href = url_to_inspection + event.id + '/' + event.provinceid;;
          // window.location.replace(url_to_inspection + event.id);
          // window.open(url_to_inspection + event.id);
        },

        // dayClick: function(event) {
        //   alert("123")
        // },

        eventRender: function (event, element, view) {
          //console.log(element);
          element.find('span.fc-title').attr('data-toggle', 'tooltip');
          element.find('span.fc-title').attr('title', event.title);
        },
        // request to load current events
        // events: this.inspectionplancalendar  // request to load current events
      })
      // .on('click', '.fc-agendaWeek-button, .fc-month-button, .fc-agendaDay-button, .fc-prev-button, .fc-next-button', function () {
      //   $('[data-toggle="tooltip"]').tooltip();
      // });
      // $('[data-toggle="tooltip"]').tooltip();
    }, 100);
  }


  CraateInspectionPlanEvent() {
    this.router.navigate(['/inspectionplanevent/create'])
  }

  openModal(template: TemplateRef<any>, id) {
    this.delid = id;
    this.modalRef = this.modalService.show(template);
  }

  deleteInspectionPlanEvent(value) {
    this.inspectionplanservice.deleteInspectionplanevent(value).subscribe(response => {
      //console.log(value);
      this.modalRef.hide()
      this.getdata();
      // this.inspectionplanservice.getinspectionplaneventuserdata().subscribe(result => {
      //   this.resultinspectionplanevent = result
      //   //console.log(this.resultinspectionplanevent);
      // })
    })
  }

  InspectionPlan(id) {
    this.router.navigate(['/inspectionplan', id])
  }
  // CraateInspectionPlan() {
  //   this.router.navigate(['/inspectionplan/createinspectionplan'])
  // }
}
