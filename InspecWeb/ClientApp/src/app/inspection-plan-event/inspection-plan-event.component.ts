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
  delid: any
  modalRef: BsModalRef;
  userid: string

  constructor(private router: Router, private inspectionplanservice: InspectionplaneventService,
    private authorize: AuthorizeService,
    private modalService: BsModalService, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'centralpolicy/detailcentralpolicyprovince/';
  }

  ngOnInit() {
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
        // alert(this.userid)
      })

    this.inspectionplanservice.getinspectionplaneventdata(this.userid)
      .subscribe(result => {
        console.log(result);
        this.resultinspectionplanevent = result
        this.inspectionplancalendar = result
        this.inspectionplancalendar = this.inspectionplancalendar.map((item, index) => {

          return {
            id: item.centralPolicyEvents[0].centralPolicy.centralPolicyProvinces[0].id,
            title: item.province.name + ", " + item.centralPolicyEvents[0].centralPolicy.title,
            start: moment(item.startDate), //.format("YYYY-MM-DD"),
            end: moment(item.endDate).add(1, 'days') //.format("YYYY-MM-DD"),
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
            id: item.centralPolicyEvents[0].centralPolicy.centralPolicyProvinces[0].id,
            title: item.province.name + "," + item.centralPolicyEvents[0].centralPolicy.title,
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
      var url_to_inspection = this.url
      $("#calendar").fullCalendar({
        header: {
          left: 'prev,next today',
          center: 'title',
          right: 'month,agendaWeek,agendaDay'
        },
        navLinks: true,
        editable: false,
        eventLimit: false,
        eventClick: function (event) {
          window.location.href = url_to_inspection + event.id;
          // window.location.replace(url_to_inspection + event.id);
          // window.open(url_to_inspection + event.id);
        },

        // dayClick: function(event) {
        //   alert("123")
        // },

        eventRender: function (event, element, view) {
          console.log(element);

          element.find('span.fc-title').attr('data-toggle', 'tooltip');
          element.find('span.fc-title').attr('title', event.title);
        },
        events: this.inspectionplancalendar,  // request to load current events
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
  CraateInspectionPlan() {
    this.router.navigate(['/inspectionplan/createinspectionplan'])
  }
}
