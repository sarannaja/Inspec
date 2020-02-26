import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { InspectionplaneventService } from '../services/inspectionplanevent.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Calendar } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import listPlugin from '@fullcalendar/list';

@Component({
  selector: 'app-inspection-plan-event',
  templateUrl: './inspection-plan-event.component.html',
  styleUrls: ['./inspection-plan-event.component.css']
})
export class InspectionPlanEventComponent implements OnInit {

  resultinspectionplanevent: any = []
  inspectionplancalendar: any = []
  delid: any
  modalRef: BsModalRef;

  constructor(private router:Router, private inspectionplanservice: InspectionplaneventService, private modalService: BsModalService) { }

  ngOnInit() {
    this.inspectionplanservice.getinspectionplaneventdata()
    .subscribe(result => {
      this.resultinspectionplanevent = result

      this.inspectionplancalendar = result
      this.inspectionplancalendar = this.inspectionplancalendar.map((item , index) =>{
        return {
          title : item.name,
          start : item.startDate,
          end :  item.endDate,
        }
      })
     this.getcalendar();
    })
  }

  getdata() {
    this.inspectionplanservice.getinspectionplaneventdata()
    .subscribe(result => {
      this.resultinspectionplanevent = result

      this.inspectionplancalendar = result
      this.inspectionplancalendar = this.inspectionplancalendar.map((item , index) =>{
        return {
          title : item.name,
          start : item.startDate,
          end :  item.endDate,
        }
      })
     this.getcalendar();
    })
  }

  getcalendar(){
    var calendarEl = document.getElementById('calendar');
    var calendar = new Calendar(calendarEl, {
      events:  this.inspectionplancalendar,
    plugins: [ dayGridPlugin, timeGridPlugin, listPlugin ]
     });
     calendar.render();
  }

  CraateInspectionPlanEvent(){
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

  InspectionPlan(id){
    this.router.navigate(['/inspectionplan', id])
  }
}
