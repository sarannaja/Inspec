import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

  constructor(private router:Router) { }

  ngOnInit() {
  }
  CreateCentralPolicy(){
    this.router.navigate(['/main/createcentralpolicy'])
  }
  CraateInspectionPlan(){
    this.router.navigate(['/main/createinspectionplan'])
  }
  EditInspectionPlan(id:any){
    this.router.navigate(['/main/editinspectionplan',id])
  }
  DetailCentralPolicy(id:any){
    this.router.navigate(['/main/detailcentralpolicy',id])
  }
}
