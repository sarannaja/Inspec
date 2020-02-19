import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-inspection-plan',
  templateUrl: './inspection-plan.component.html',
  styleUrls: ['./inspection-plan.component.css']
})
export class InspectionPlanComponent implements OnInit {

  constructor(private router:Router) { }

  ngOnInit() {
  }
  CraateInspectionPlan(){
    this.router.navigate(['/inspectionplan/createinspectionplan'])
  }
  EditInspectionPlan(id:any){
    this.router.navigate(['/inspectionplan/editinspectionplan',id])
  }
  DetailCentralPolicy(id:any){
    this.router.navigate(['/centralpolicy/detailcentralpolicy',id])
  }
}
