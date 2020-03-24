import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { FiscalyearService } from '../services/fiscalyear.service';
import { Router } from '@angular/router';
import { IMyOptions } from 'mydatepicker-th';

@Component({
  selector: 'app-fiscalyear',
  templateUrl: './fiscalyear.component.html',
  styleUrls: ['./fiscalyear.component.css']
})
export class FiscalyearComponent implements OnInit {

  resultfiscalyear: any[] = []
  delid: any
  year: any
  modalRef: BsModalRef;
  Form: FormGroup
  EditForm: FormGroup;
  loading = false;
  dtOptions: DataTables.Settings = {};
  forbiddenUsernames = ['admin', 'test', 'xxxx'];
  private myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd/mm/yyyy',
    showTodayBtn: true
  };
  private myDatePickerOptionsyear: IMyOptions = {
    // other options...
    dateFormat: 'YYYY',
    
  };

  constructor(
    private modalService: BsModalService, 
    private fb: FormBuilder, 
    private fiscalyearservice: FiscalyearService,
    public share: FiscalyearService, 
    private router: Router) { }

  ngOnInit() {

    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [3],
          orderable: false
        }
      ]
    }


    this.Form = this.fb.group({
      "fiscalyear": new FormControl(null, [Validators.required]),
      "startdate": new FormControl(null, [Validators.required]),
      "enddate": new FormControl(null, [Validators.required]),
      // "test" : new FormControl(null,[Validators.required,this.forbiddenNames.bind(this)])
    })

    //แก้ไข
    this.Form.patchValue({
      // test: "testest"
    })

    this.fiscalyearservice.getfiscalyeardata().subscribe(result => {
      this.resultfiscalyear = result
      this.loading = true;
      console.log(this.resultfiscalyear);

    })
  }
  openModal(template: TemplateRef<any>, id, year) {
    this.delid = id;
    this.year = year;
    console.log(this.delid);
    console.log(this.year);

    this.modalRef = this.modalService.show(template);
  }
  storeFiscalyear(value) {
    this.fiscalyearservice.addFiscalyear(value).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.fiscalyearservice.getfiscalyeardata().subscribe(result => {
        this.resultfiscalyear = result
        this.loading = true;
        console.log(this.resultfiscalyear);
      })
    })
  }
  deleteFiscalyear(value) {
    this.fiscalyearservice.deleteFiscalyear(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.loading = false;
      this.fiscalyearservice.getfiscalyeardata().subscribe(result => {
        this.resultfiscalyear = result
        this.loading = true;
        console.log(this.resultfiscalyear);
      })
    })
  }
  editModal(template: TemplateRef<any>, id, year) {
    this.delid = id;
    this.year = year
    console.log(this.delid);
    console.log(this.year);

    this.modalRef = this.modalService.show(template);
    this.EditForm = this.fb.group({
      "fiscalyear": new FormControl(null, [Validators.required]),
      // "test" : new FormControl(null,[Validators.required,this.forbiddenNames.bind(this)])
    })
    this.EditForm.patchValue({
      "fiscalyear": year
    })
  }
  editFiscalyear(value, delid) {
    console.clear();
    console.log(value);
    this.fiscalyearservice.editFiscalyear(value, delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.fiscalyearservice.getfiscalyeardata().subscribe(result => {
        this.resultfiscalyear = result
        this.loading = true;
      })
    })
  }

  DetailFiscalYear(id: any) {
    this.router.navigate(['/fiscalyear/detailfiscalyear', id])
  }
}
