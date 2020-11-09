import { Component, OnInit, Inject } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { RequestorderrService } from 'src/app/services/requestorderr.service';

@Component({
  selector: 'app-request-order-export1',
  templateUrl: './request-order-export1.component.html',
  styleUrls: ['./request-order-export1.component.css']
})
export class RequestOrderExport1Component implements OnInit {
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
    this.userService.getuserdataforrequestorder()
      .subscribe(result => {
        this.userdata = result;
        console.log("ddd", result);
      })
  }
  exportrequest(userid) {
    this.requestderService.getrequest1(userid.target.value)
      .subscribe(result => {
        console.log('result', result);
       // alert(userid.target.value);
        window.open(this.url + "reportrequest/" + result.data);
      })
    this.getuserinfo();
  }
  changrequest(userid) {
    this.id = userid
  }
}
