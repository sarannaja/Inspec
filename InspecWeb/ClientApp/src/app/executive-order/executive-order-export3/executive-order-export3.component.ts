import { Component, OnInit,Inject} from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { DetailexecutiveorderService } from 'src/app/services/detailexecutiveorder.service';

@Component({
  selector: 'app-executive-order-export3',
  templateUrl: './executive-order-export3.component.html',
  styleUrls: ['./executive-order-export3.component.css']
})
export class ExecutiveOrderExport3Component implements OnInit {
  userdata: any = [];
  url = ""
  constructor(
    @Inject('BASE_URL') baseUrl: string,
    private userService: UserService,
    private detailexecutiveorderService: DetailexecutiveorderService
    
  ) { }

  ngOnInit() {
    this.getuserinfo();
  }
//start getuser
getuserinfo(){
   
  this.userService.getuserdata(3)      
  .subscribe(result => {  
    this.userdata = result;
  })
} 
//End getuser

exportexecutive3(userid) {

    this.detailexecutiveorderService.getexcutive3(userid.target.value)
      .subscribe(result => {
        //console.log('result',result);
        window.open(this.url + "reportexecutive/" + result.data);
      })
      this.getuserinfo();
      
}

changexcutive(userid){
 // this.id = userid
}
}
