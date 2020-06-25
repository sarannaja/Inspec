import { Component, OnInit, Inject } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { RequestorderrService } from 'src/app/services/requestorderr.service';

@Component({
  selector: 'app-request-order-export3',
  templateUrl: './request-order-export3.component.html',
  styleUrls: ['./request-order-export3.component.css']
})
export class RequestOrderExport3Component implements OnInit {
  userdata: any = [];
  id: any;
  userid: any;
  url = ""

  constructor(
    @Inject('BASE_URL') baseUrl: string,
    private userService: UserService,
    private requestderService: RequestorderrService
  ) { }
  ngOnInit() {
    this.getuserinfo();
  }
  getuserinfo() {
    this.userService.getuserdata(3)
      .subscribe(result => {
        this.userdata = result;
        console.log(result);
      })
  }
  exportrequest3(userid) {
    this.requestderService.getrequest3(userid.target.value)
      .subscribe(result => {
        console.log('result', result);
        window.open(this.url + "reportrequest/" + result.data);
      })
    this.getuserinfo();
  }
  changrequest(userid) {
    this.id = userid
  }
}
