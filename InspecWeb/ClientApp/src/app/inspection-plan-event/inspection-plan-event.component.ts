import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-inspection-plan-event',
  templateUrl: './inspection-plan-event.component.html',
  styleUrls: ['./inspection-plan-event.component.css']
})
export class InspectionPlanEventComponent implements OnInit {

  constructor(private router:Router) { }

  ngOnInit() {
  }
  CraateInspectionPlanEvent(){
    this.router.navigate(['/inspectionplanevent/create'])
  }
}
