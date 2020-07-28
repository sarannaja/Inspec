import { Component, OnInit, TemplateRef, Injectable, Inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { InspectionplaneventService } from '../../services/inspectionplanevent.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Calendar } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import listPlugin from '@fullcalendar/list';
import * as moment from 'moment';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from '../../services/user.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { Subscription } from 'rxjs/internal/Subscription';
declare var $: any;
@Injectable({
  providedIn: 'root'
})

@Component({
  selector: 'app-inspection-plan-event-province',
  templateUrl: './inspection-plan-event-province.component.html',
  styleUrls: ['./inspection-plan-event-province.component.css']
})
export class InspectionPlanEventProvinceComponent implements OnInit {
  url = "";
  resultinspectionplanevent: any = []
  inspectionplancalendar: any = []
  resultuserregion: any = []
  delid: any
  modalRef: BsModalRef;
  userid: string
  role_id
  id
  Form: FormGroup;
  subscription: Subscription;
  loading: boolean = false

  constructor(private router: Router, private inspectionplanservice: InspectionplaneventService,
    private authorize: AuthorizeService,
    private modalService: BsModalService,
    private userService: UserService,
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private spinner: NgxSpinnerService,
    @Inject('BASE_URL') baseUrl: string) {

    this.subscription = this.userService.getUserNav()
      .subscribe(
        result => {
          if (result.roleId != this.id) {
            // this.loading = false;
            console.log('result.roleId', result.roleId);
            this.id = result.roleId
            setTimeout(() => { this.getinspectionplaneventuserprovincedata() }, 200)
          }
        });
    // this.url = baseUrl + 'centralpolicy/detailcentralpolicyprovince/';
    this.url = baseUrl + 'inspectionplan/';
    this.id = activatedRoute.snapshot.paramMap.get('id')

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

    this.Form = this.fb.group({
      province: new FormControl(null, [Validators.required]),
    })

    this.Form.patchValue({
      province: this.id
    })

    this.inspectionplanservice.getuserregion(this.userid)
      .subscribe(result => {
        console.log("this.resultuserregion", result);
        this.resultuserregion = result
        this.getinspectionplaneventuserprovincedata()
      })


  }

  getinspectionplaneventuserprovincedata() {
    this.inspectionplanservice.getinspectionplaneventuserprovincedata(this.id)
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

  async getcalendar() {
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
    await setTimeout(() => {
      var url_to_inspection = this.url
      $("#calendar").fullCalendar({
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
        eventClick: function (event) {
          window.location.href = url_to_inspection + event.id + '/' + event.provinceid;
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

    this.loading = true
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
      this.router.navigate(['/inspectionplanevent'])
    } else {
      this.loading = false
      var id = value
      this.userService.sendNav(value);
      this.router.navigate(['/inspectionplaneventprovince/' + id])
    }
  }
}
