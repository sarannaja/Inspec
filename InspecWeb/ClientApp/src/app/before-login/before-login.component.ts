import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-before-login',
  templateUrl: './before-login.component.html',
  styleUrls: ['./before-login.component.css']
})
export class BeforeLoginComponent implements OnInit {

  constructor(
    private router: Router
  ) { }

  ngOnInit() {
  }

  GotoLogin() {
    console.log('go login ');
    location.href = '/Identity/Account/Login'
  }

  goToCalendar() {
    location.href = '/inspectionplanevent/all/noauth'
  }

  goToInspectionOrder() {
    location.href = '/inspectionordermain'
  }

  goToGovermentInspectionArea() {
    location.href = '/supportgovernment/governmentinspectionareamain'
  }

  goToLaw() {
    location.href = '/supportgovernment/circularlettermain'
  }

  goToNationalStrategy() {
    location.href = 'http://nscr.nesdb.go.th/%E0%B8%A2%E0%B8%B8%E0%B8%97%E0%B8%98%E0%B8%A8%E0%B8%B2%E0%B8%AA%E0%B8%95%E0%B8%A3%E0%B9%8C%E0%B8%8A%E0%B8%B2%E0%B8%95%E0%B8%B4/'
  }

  goToReformNation() {
    location.href = 'http://nscr.nesdb.go.th/%E0%B9%81%E0%B8%9C%E0%B8%99%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%9B%E0%B8%8F%E0%B8%B4%E0%B8%A3%E0%B8%B9%E0%B8%9B%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B9%80%E0%B8%97%E0%B8%A8/'
  }

  goToCentralpolicy() {
    location.href = '/maincentralpolicy'
  }

  goToTrain() {
    location.href = '/train'
  }

  goToGoverment() {
    location.href = '/infoministrymain'
  }

  goToProvince() {
    location.href = '/informationprovincemain'
  }

  goToPeopleContact() {
    location.href = '/inspectormain'
  }

}
