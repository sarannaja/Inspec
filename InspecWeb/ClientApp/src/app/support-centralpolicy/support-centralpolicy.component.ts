import { Component, OnInit } from '@angular/core';
import { SupportcentralpolicyService } from '../services/supportcentralpolicy.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';

@Component({
  selector: 'app-support-centralpolicy',
  templateUrl: './support-centralpolicy.component.html',
  styleUrls: ['./support-centralpolicy.component.css']
})
export class SupportCentralPolicyComponent implements OnInit {

  resultcentralpolicy: any = []
  loading = false;
  dtOptions: DataTables.Settings = {};

  constructor(
    private supportcentralpolicy: SupportcentralpolicyService,
    private spinner: NgxSpinnerService,
    private router: Router,
  ) { }

  ngOnInit() {
    this.spinner.show();

    this.getcentralpolicy();
    this.loading = true;
    this.spinner.hide();
    this.dtOptions = {
      pagingType: 'full_numbers',
   

    };

  }
  getcentralpolicy() {
    this.supportcentralpolicy.getcentralpolicydata()
      .subscribe(result => {
        this.resultcentralpolicy = result
      }
      )
  }
  subjectcentralpolicy(id){
    this.router.navigate(['/supportsubjectcentralpolicy',id])
  }


}



