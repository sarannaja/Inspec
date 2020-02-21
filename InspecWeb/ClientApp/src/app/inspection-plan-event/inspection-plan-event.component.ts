import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { InspectionplaneventService } from '../services/inspectionplanevent.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-inspection-plan-event',
  templateUrl: './inspection-plan-event.component.html',
  styleUrls: ['./inspection-plan-event.component.css']
})
export class InspectionPlanEventComponent implements OnInit {

  resultinspectionplanevent: any = []
  delid: any
  modalRef: BsModalRef;

  constructor(private router:Router, private inspectionplanservice: InspectionplaneventService, private modalService: BsModalService) { }

  ngOnInit() {
    this.inspectionplanservice.getinspectionplaneventdata()
    .subscribe(result => {
      this.resultinspectionplanevent = result
      console.log(this.resultinspectionplanevent);
    })
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
      this.inspectionplanservice.getinspectionplaneventdata().subscribe(result => {
        this.resultinspectionplanevent = result
        console.log(this.resultinspectionplanevent);
      })
    })
  }

  InspectionPlan(){
    this.router.navigate(['/inspectionplan'])
  }

}
