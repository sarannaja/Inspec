import { Component, OnInit, Inject } from '@angular/core';
import { UserService } from '../services/user.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-province-departmentuser',
  templateUrl: './province-departmentuser.component.html',
  styleUrls: ['./province-departmentuser.component.css']
})
export class ProvinceDepartmentuserComponent implements OnInit {
  resultuser: any[];
  loading= false;
  dtOptions: DataTables.Settings = {};

  constructor(private userService: UserService,
    private spinner: NgxSpinnerService,
    @Inject('BASE_URL') baseUrl: string) { }

  ngOnInit() {
    this.getUser()
  }
  getUser() {
    this.userService.getuserdata("9")
      .subscribe(result => {
        //alert(this.roleId);
        this.resultuser = result;
        this.loading = true
        this.spinner.hide();
        // console.log(this.resultuser);
      })
  }
}
