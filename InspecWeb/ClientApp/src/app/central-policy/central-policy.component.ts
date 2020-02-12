import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-central-policy',
  templateUrl: './central-policy.component.html',
  styleUrls: ['./central-policy.component.css']
})
export class CentralPolicyComponent implements OnInit {

  constructor(private router:Router) { }

  ngOnInit() {
  }
  CreateCentralPolicy(){
    this.router.navigate(['/centralpolicy/createcentralpolicy'])
  }
  DetailCentralPolicy(id:any){
    this.router.navigate(['/centralpolicy/detailcentralpolicy',id])
  }
}
