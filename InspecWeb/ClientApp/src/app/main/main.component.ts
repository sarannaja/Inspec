import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthorizeService } from 'src/api-authorization/authorize.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {
  email:string =''
  role_id:number = 0;
  constructor(private router:Router , private authorize:AuthorizeService) { }

  ngOnInit() {
    this.authorize.getUser()
    .subscribe(result=>{
      this.email = result.name
      this.role_id = result.Role_id
      console.log(result);
      
    })
  }
  // CreateCentralPolicy(){
  //   this.router.navigate(['/main/createcentralpolicy'])
  // }
  // CraateInspectionPlan(){
  //   this.router.navigate(['/main/createinspectionplan'])
  // }
  // EditInspectionPlan(id:any){
  //   this.router.navigate(['/main/editinspectionplan',id])
  // }
  // DetailCentralPolicy(id:any){
  //   this.router.navigate(['/main/detailcentralpolicy',id])
  // }
}
