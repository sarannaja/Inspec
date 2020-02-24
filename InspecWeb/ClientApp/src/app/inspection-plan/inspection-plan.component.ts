import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CentralpolicyService } from '../services/centralpolicy.service';

@Component({
  selector: 'app-inspection-plan',
  templateUrl: './inspection-plan.component.html',
  styleUrls: ['./inspection-plan.component.css']
})
export class InspectionPlanComponent implements OnInit {

  resultcentralpolicy: any = []

  constructor(private router:Router, private centralpolicyservice: CentralpolicyService) { }

  ngOnInit() {
    this.centralpolicyservice.getcentralpolicydata()
    .subscribe(result => {
      this.resultcentralpolicy = result
      console.log(this.resultcentralpolicy);
    })
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
