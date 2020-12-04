import { Component, OnInit } from '@angular/core';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';

@Component({
  selector: 'app-opm1111',
  templateUrl: './opm1111.component.html',
  styleUrls: ['./opm1111.component.css']
})

export class Opm1111Component implements OnInit {
  userid:any;
  
  constructor(
    private authorize: AuthorizeService,
  ) { }

  ngOnInit() {
    this.authorize.getUser()
    .subscribe(result => {
      this.userid = result.sub
    })
  }
  excel(){
    window.location.href = '/api/ExternalOrganization/excelopm-1111/'+ this.userid;
  }
}
