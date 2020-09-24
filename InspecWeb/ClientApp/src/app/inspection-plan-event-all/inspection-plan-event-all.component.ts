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
import { ProvinceService } from '../services/province.service';
declare var $: any;
@Injectable({
  providedIn: 'root'
})

@Component({
  selector: 'app-inspection-plan-event-all',
  templateUrl: './inspection-plan-event-all.component.html',
  styleUrls: ['./inspection-plan-event-all.component.css']
})
export class InspectionPlanEventAllComponent implements OnInit {
  url = "";
  resultinspectionplanevent: any = []
  inspectionplancalendar: any = []
  resultuserregion: any[] = []
  delid: any
  modalRef: BsModalRef;
  userid: string
  role_id
  loading = false;

  selectedProvince: any = "allprovince"

  constructor(private router: Router, private inspectionplanservice: InspectionplaneventService,
    private provinceservice: ProvinceService,
    private authorize: AuthorizeService,
    private modalService: BsModalService,
    private userService: UserService,
    @Inject('BASE_URL') baseUrl: string) {
    // this.url = baseUrl + 'centralpolicy/detailcentralpolicyprovince/';
    this.url = baseUrl + 'inspectionplan/';
  }

  ngOnInit() {
    // this.authorize.getUser()
    //   .subscribe(result => {
    //     this.userid = result.sub
    //     this.userService.getuserfirstdata(this.userid)
    //       .subscribe(result => {
    //         this.role_id = result[0].role_id
    //       })
    //     //console.log(result);

    //   })
      this.getprovince()
      this.getcalendaralldata()
  }

  getprovince() {
    this.provinceservice.getonlyprovince()
      .subscribe(result => {
        //console.log("this.resultuserregion", result);
        this.resultuserregion = result
      })
  }

  getcalendaralldata() {
    this.inspectionplanservice.getinspectionplaneventalldata()
      .subscribe(result => {
        //console.log(result);
        this.resultinspectionplanevent = result
        this.inspectionplancalendar = result
        this.inspectionplancalendar = this.inspectionplancalendar.map((item, index) => {

          var roleCreatedBy: any = "";

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

          if (item.centralPolicyEvents.length != 0) {
            var name = ""
            for (var i = 0; i < item.centralPolicyEvents.length; i++) {
              if (i == (item.centralPolicyEvents.length - 1)) {
                name = name + item.centralPolicyEvents[i].centralPolicy.title
              } else {
                name = name + item.centralPolicyEvents[i].centralPolicy.title + ", "
              }
            }
            return {
              id: item.id,
              title: item.province.name,
              name: item.province.name + " : " + name,
              provinceid: item.province.id,
              // id: item.centralPolicyEvents[0].centralPolicy.centralPolicyProvinces[0].id,
              // title: item.province.name + ", " + item.centralPolicyEvents[0].centralPolicy.title,
              start: moment(item.startDate), //.format("YYYY-MM-DD"),
              end: moment(item.endDate), //.format("YYYY-MM-DD"),
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
              end: moment(item.endDate), //.format("YYYY-MM-DD"),
              color: colorJa,
              roleCreatedBy: roleCreatedBy,
            }
          }
        })
        this.getcalendar();
        // setTimeout(() => {

        this.loading = true;
        // }, 300)
      })
  }

  getcalendar() {
    setTimeout(() => {
      const self = this;
      $("#calendar").fullCalendar({
        header: {

          left: 'prev,next today',
          center: 'title',
          right: 'month,agendaWeek,agendaDay',

        },
        navLinks: true,
        editable: false,
        eventLimit: false,
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
        eventClick: function (event) {
          //console.log(event);
          //console.log('this.role_id', self.role_id);
          var watch = 1;
          if (event.roleCreatedBy == 3) {
            window.location.href = self.url + event.id + '/' + event.provinceid + '/' + watch;
          } else if (event.roleCreatedBy == 6) {
            window.location.href = self.url + 'inspectorministry/' + event.id + '/' + event.provinceid + '/' + watch;
          } else if (event.roleCreatedBy == 10) {
            window.location.href = self.url + 'inspectordepartment/' + event.id + '/' + event.provinceid + '/' + watch;
          }
        },
        eventRender: function (event, element, view) {
          //console.log(element);
          console.log(element, element.find('span.fc-time'), event);
          var date = moment(event.start).format('HH:mm')
          element.find('span.fc-time').text(date)
          element.find('span.fc-title').attr('data-toggle', 'tooltip');
          element.find('span.fc-title').attr('title', event.name);
        },
        events: self.inspectionplancalendar,  // request to load current events
      })
    }, 100);
  }

  getinspectionplaneventuserprovincedata(value) {
    // alert(value)
    this.inspectionplanservice.getinspectionplaneventuserprovincedata(value)
      .subscribe(result => {
        console.log(result);
        this.resultinspectionplanevent = result
        this.inspectionplancalendar = result
        this.inspectionplancalendar = this.inspectionplancalendar.map((item, index) => {

          return {
            id: item.id,
            title: item.province.name,
            provinceid: item.province.id,
            // id: item.centralPolicyEvents[0].centralPolicy.centralPolicyProvinces[0].id,
            // title: item.province.name + ", " + item.centralPolicyEvents[0].centralPolicy.title,
            start: moment(item.startDate), //.format("YYYY-MM-DD"),
            end: moment(item.endDate).add(1, 'days') //.format("YYYY-MM-DD"),
          }
        })
        // alert(JSON.stringify(this.inspectionplancalendar))
        this.getcalendar();
        this.loading = true;
      })
  }


  selectprovince(value) {
    //console.log(value);

    if (value == "allprovince") {
      this.loading = false;
      this.getcalendaralldata()
    } else {
      this.loading = false;
      this.getinspectionplaneventuserprovincedata(value)
    }
  }
}
