import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { FiscalyearService } from '../services/fiscalyear.service';
import { Router } from '@angular/router';
import { IMyOptions } from 'mydatepicker-th';
import { NgxSpinnerService } from "ngx-spinner";

@Component({
  selector: 'app-support-fiscalyear',
  templateUrl: './support-fiscalyear.component.html',
  styleUrls: ['./support-fiscalyear.component.css']
})
export class SupportFiscalyearComponent implements OnInit {
  loading = false;
  dtOptions: DataTables.Settings = {};
  resultfiscalyear: any[] = []

  constructor( 
    private modalService: BsModalService, 
    private fb: FormBuilder, 
    private fiscalyearservice: FiscalyearService,
    public share: FiscalyearService, 
    private router: Router,
    private spinner: NgxSpinnerService) { }

  ngOnInit() {
     this.fiscalyearservice.getfiscalyeardata().subscribe(result => {
      this.resultfiscalyear = result
      this.loading = true;
      this.spinner.hide();
      //console.log(this.resultfiscalyear);
     });
  }
  DetailFiscalYear(id) {

    this.router.navigate(['/supportdetailfiscalyear', id])
  }

}
