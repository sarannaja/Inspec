import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { SupportsubjectService } from 'src/app/services/supportsubject.service';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthorizeService } from 'src/api-authorization/authorize.service';

@Component({
  selector: 'app-support-subject-centralpolicy',
  templateUrl: './support-subject-centralpolicy.component.html',
  styleUrls: ['./support-subject-centralpolicy.component.css']
})
export class SupportSubjectCentralpolicyComponent implements OnInit {

  loading = false;
  dtOptions: DataTables.Settings = {};
  resultsubject: any;
  id:any
  name:any

  constructor(
    private supportsubject: SupportsubjectService,
    private spinner: NgxSpinnerService,
    private centralpolicyservice: CentralpolicyService,
    private router: Router,
    private authorize: AuthorizeService,
    activateRoute: ActivatedRoute,
  
  ) { this.id = activateRoute.snapshot.paramMap.get('id')
  this.name = activateRoute.snapshot.paramMap.get('name')}

  ngOnInit() {
    this.spinner.show();

    this.dtOptions = {
      pagingType: 'full_numbers',
    
    };
  this.getSubject()
  
  
  }

  getSubject() {
    this.supportsubject.getsubjectdata(this.id).subscribe(result => {
      this.resultsubject = result
      this.loading = true;
      this.spinner.hide();
      //console.log('lll',result);
      
    }
    )}
    subjectdetail(id){
      this.router.navigate(['/supportsubjectdetail',id])
     // alert(id)
    }
  
}
