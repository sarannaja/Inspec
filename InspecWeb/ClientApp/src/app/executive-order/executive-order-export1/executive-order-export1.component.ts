import { ExecutiveorderService } from 'src/app/services/executiveorder.service';
import { Component, OnInit, Inject } from '@angular/core';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-executive-order-export1',
  templateUrl: './executive-order-export1.component.html',
  styleUrls: ['./executive-order-export1.component.css']
})
export class ExecutiveOrderExport1Component implements OnInit {
  userdata: any = [];
  id: any;
  userid: any;
  url = ""
  constructor(
    @Inject('BASE_URL') baseUrl: string,
    private userService: UserService,
    private executiveorderService: ExecutiveorderService
  ) { }

  ngOnInit() {
    this.getuserinfo();
  }

  //start getuser
  getuserinfo() {

    this.userService.getuserdata(8)
      .subscribe(result => {
        this.userdata = result;
        //console.log(result);

      })

  }
  //End getuser
  exportexecutive(userid) {

    this.executiveorderService.getexcutive1(userid.target.value)
      .subscribe(result => {
      //  console.log('result', result);
        window.open(this.url + "reportexecutive/" + result.data);
      })
    this.getuserinfo();

  }

  changexcutive(userid) {
    this.id = userid
  }
}
