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
  resultuserregion: any = []
  delid: any
  modalRef: BsModalRef;
  userid: string
  role_id
  loading = false;
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
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        this.userService.getuserfirstdata(this.userid)
          .subscribe(result => {
            this.role_id = result[0].role_id
          })
        console.log(result);

        this.getprovince()
        this.getcalendaralldata()
      })
  }

  getprovince() {
    this.provinceservice.getprovincedata()
      .subscribe(result => {
        console.log("this.resultuserregion", result);
        this.resultuserregion = result
      })
  }

  getcalendaralldata() {
    this.inspectionplanservice.getinspectionplaneventalldata()
      .subscribe(result => {
        console.log(result);
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
            return {
              id: item.id,
              title: item.province.name,
              name: item.province.name + ", " + item.centralPolicyEvents[0].centralPolicy.title,
              provinceid: item.province.id,
              start: moment(item.startDate).format("YYYY-MM-DD"), //.format("YYYY-MM-DD"),
              end: moment(item.endDate).add(1, 'days').format("YYYY-MM-DD"), //.format("YYYY-MM-DD"),
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
              start: moment(item.startDate).format("YYYY-MM-DD"), //.format("YYYY-MM-DD"),
              end: moment(item.endDate).add(1, 'days').format("YYYY-MM-DD"), //.format("YYYY-MM-DD"),
              color: colorJa,
              roleCreatedBy: roleCreatedBy,
            }
          }
        })
        this.getcalendar();
        this.loading = true;
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
        eventClick: function (event) {
          console.log(event);
          console.log('this.role_id', self.role_id);

          if (event.roleCreatedBy == 3) {
            window.location.href = self.url + event.id + '/' + event.provinceid;
          } else if (event.roleCreatedBy == 6) {
            window.location.href = self.url + 'inspectorministry/' + event.id + '/' + event.provinceid;
          } else if (event.roleCreatedBy == 10) {
            window.location.href = self.url + 'inspectordepartment/' + event.id + '/' + event.provinceid;
          }
        },
        eventRender: function (event, element, view) {
          console.log(element);

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
    if (value == "allprovince") {
      this.loading = false;
      this.getcalendaralldata()
    } else {
      this.loading = false;
      this.getinspectionplaneventuserprovincedata(value)
    }
  }
}
