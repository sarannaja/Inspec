import { Component, OnInit, TemplateRef, Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { InspectionplaneventService } from '../services/inspectionplanevent.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Calendar } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import listPlugin from '@fullcalendar/list';
import * as moment from 'moment';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from '../services/user.service';
declare var $: any;
@Injectable({
  providedIn: 'root'
})

@Component({
  selector: 'app-inspection-plan-event',
  templateUrl: './inspection-plan-event.component.html',
  styleUrls: ['./inspection-plan-event.component.css']
})
export class InspectionPlanEventComponent implements OnInit {
  url = "";
  resultinspectionplanevent: any = []
  inspectionplancalendar: any = []
  resultuserregion: any = []
  delid: any
  modalRef: BsModalRef;
  userid: string
  role_id
  constructor(private router: Router, private inspectionplanservice: InspectionplaneventService,
    private authorize: AuthorizeService,
    private modalService: BsModalService,
    private userService: UserService,
    @Inject('BASE_URL') baseUrl: string) {
    // this.url = baseUrl + 'centralpolicy/detailcentralpolicyprovince/';
    this.url = baseUrl + 'inspectionplan/';
  }

  ngOnInit() {
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        this.userService.getuserfirstdata(this.userid)
          .subscribe(result => {
            // this.resultuser = result;
            //console.log("test" , this.resultuser);
            this.role_id = result[0].role_id
            // alert(this.role_id)
          })
        console.log(result);
        // alert(this.userid)
      })

    this.inspectionplanservice.getuserregion(this.userid)
      .subscribe(result => {
        console.log("this.resultuserregion", result);
        this.resultuserregion = result
      })

    this.inspectionplanservice.getinspectionplaneventdata(this.userid)
      .subscribe(result => {
        console.log(result);
        this.resultinspectionplanevent = result
        this.inspectionplancalendar = result
        this.inspectionplancalendar = this.inspectionplancalendar.map((item, index) => {

          var roleCreatedBy: any = "";

          if (this.role_id == 3) {
            if (item.roleCreatedBy == "3") {
              roleCreatedBy = 3
            } else if (item.roleCreatedBy == "6") {
              roleCreatedBy = 6
            } else if (item.roleCreatedBy == "10") {
              roleCreatedBy = 10
            }
          }

          else if (this.role_id == 6) {
            var colorJa: any;
            if (item.roleCreatedBy == "3") {
              roleCreatedBy = 3
            } else if (item.roleCreatedBy == "6") {
              roleCreatedBy = 6
              colorJa = "#3B7DDD" //blue
            } else if (item.roleCreatedBy == "10") {
              roleCreatedBy = 10
            }
          }

          else if (this.role_id == 10) {
            var colorJa: any;
            if (item.roleCreatedBy == "3") {
              roleCreatedBy = 3
            } else if (item.roleCreatedBy == "6") {
              roleCreatedBy = 6
            }
            else if (item.roleCreatedBy == "10") {
              roleCreatedBy = 10
              colorJa = "#C70039" //blue
            }
          }


          else {
            var colorJa: any;
            if (item.roleCreatedBy == "3") {
              roleCreatedBy = 3
            } else if (item.roleCreatedBy == "6") {
              roleCreatedBy = 6
              colorJa = "#3B7DDD" //blue
            }
            else if (item.roleCreatedBy == "10") {
              roleCreatedBy = 10
              colorJa = "#C70039" //blue
            }
          }
          // alert(item.centralPolicyEvents[0].centralPolicy.title)
          if (item.centralPolicyEvents.length != 0) {
            return {
              id: item.id,
              title: item.province.name,
              name: item.province.name + ", " + item.centralPolicyEvents[0].centralPolicy.title,
              provinceid: item.province.id,
              // id: item.centralPolicyEvents[0].centralPolicy.centralPolicyProvinces[0].id,
              // title: item.province.name + ", " + item.centralPolicyEvents[0].centralPolicy.title,
              start: moment(item.startDate), //.format("YYYY-MM-DD"),
              end: moment(item.endDate).add(1, 'days'), //.format("YYYY-MM-DD"),
              color: colorJa,
              roleCreatedBy: roleCreatedBy,
            }
          }
          else {
            return {
              id: item.id,
              title: item.province.name,
              provinceid: item.province.id,
              name: '',
              // id: item.centralPolicyEvents[0].centralPolicy.centralPolicyProvinces[0].id,
              // title: item.province.name + ", " + item.centralPolicyEvents[0].centralPolicy.title,
              start: moment(item.startDate), //.format("YYYY-MM-DD"),
              end: moment(item.endDate).add(1, 'days'), //.format("YYYY-MM-DD"),
              color: colorJa,
              roleCreatedBy: roleCreatedBy,
            }
          }
        })
        // alert(JSON.stringify(this.inspectionplancalendar))
        this.getcalendar();
      })
  }

  getdata() {
    this.inspectionplanservice.getinspectionplaneventdata(this.userid)
      .subscribe(result => {
        this.resultinspectionplanevent = result

        this.inspectionplancalendar = result
        this.inspectionplancalendar = this.inspectionplancalendar.map((item, index) => {
          return {
            id: item.id,
            title: item.province.name + ", " + item.centralPolicyEvents[0].centralPolicy.title,
            provinceid: item.province.id,
            // id: item.centralPolicyEvents[0].centralPolicy.centralPolicyProvinces[0].id,
            // title: item.province.name + "," + item.centralPolicyEvents[0].centralPolicy.title,
            start: moment(item.startDate), //.format("YYYY-MM-DD"),
            end: moment(item.endDate).add(1, 'days'), //.format("YYYY-MM-DD"),
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
      const self = this;
      // var self.url = this.url

      $("#calendar2").fullCalendar({
        header: {

          left: 'prev,next today',
          center: 'title',
          right: 'month,agendaWeek,agendaDay',

        },
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
        navLinks: true,
        editable: false,
        eventLimit: false,
        eventClick: function (event) {
          // alert(JSON.stringify(event))
          console.log(event);
          console.log('this.role_id', self.role_id);
          // if (self.role_id == 3) {

          //   window.location.href = self.url + event.id + '/' + event.provinceid;
          // }
          // else {
          // alert(event.roleCreatedBy)
          var watch = 0;
          if (event.roleCreatedBy == 3) {
            window.location.href = self.url + event.id + '/' + event.provinceid + '/' + watch;
          } else if (event.roleCreatedBy == 6) {
            window.location.href = self.url + 'inspectorministry/' + event.id + '/' + event.provinceid + '/' + watch;
          } else if (event.roleCreatedBy == 10) {
            window.location.href = self.url + 'inspectordepartment/' + event.id + '/' + event.provinceid + '/' + watch;
          }
          // }
          // else
          // window.location.replace(url_to_inspection + event.id);
          // window.open(url_to_inspection + event.id);
        },

        // dayClick: function(event) {
        //   alert("123")
        // },

        eventRender: function (event, element, view) {
          console.log(element);

          element.find('span.fc-title').attr('data-toggle', 'tooltip');
          element.find('span.fc-title').attr('title', event.name);
        },
        events: self.inspectionplancalendar,  // request to load current events
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
      console.log(value);
      this.modalRef.hide()
      this.getdata();
      // this.inspectionplanservice.getinspectionplaneventdata().subscribe(result => {
      //   this.resultinspectionplanevent = result
      //   console.log(this.resultinspectionplanevent);
      // })
    })
  }

  InspectionPlan(id) {
    this.router.navigate(['/inspectionplan', id])
  }
  // CraateInspectionPlan() {
  //   this.router.navigate(['/inspectionplan/createinspectionplan'])
  // }
  selectprovince(value) {
    if (value == "allprovince") {
      window.location.reload();
    } else {
      var id = value
      this.router.navigate(['/inspectionplaneventprovince/' + id])
    }
  }
}
