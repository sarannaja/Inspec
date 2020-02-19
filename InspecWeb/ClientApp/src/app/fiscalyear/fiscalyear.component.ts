import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { FiscalyearService } from '../services/fiscalyear.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-fiscalyear',
  templateUrl: './fiscalyear.component.html',
  styleUrls: ['./fiscalyear.component.css']
})
export class FiscalyearComponent implements OnInit {

  resultfiscalyear: any = []
  delid: any
  year: any
  modalRef: BsModalRef;
  Form: FormGroup
  forbiddenUsernames = ['admin', 'test', 'xxxx'];

  constructor(private modalService: BsModalService, private fb: FormBuilder, private fiscalyearservice: FiscalyearService,
    public share: FiscalyearService, private router:Router) { }

  ngOnInit() {
    this.Form = this.fb.group({
      "fiscalyear": new FormControl(null, [Validators.required]),
      // "test" : new FormControl(null,[Validators.required,this.forbiddenNames.bind(this)])
    })

    //แก้ไข
    this.Form.patchValue({
      // test: "testest"
    })

    this.fiscalyearservice.getfiscalyeardata().subscribe(result => {
      this.resultfiscalyear = result
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
      this.fiscalyearservice.getfiscalyeardata().subscribe(result => {
        this.resultfiscalyear = result
        console.log(this.resultfiscalyear);
      })
    })
  }
  deleteFiscalyear(value) {
    this.fiscalyearservice.deleteFiscalyear(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.fiscalyearservice.getfiscalyeardata().subscribe(result => {
        this.resultfiscalyear = result
        console.log(this.resultfiscalyear);
      })
    })
  }
  editFiscalyear(value,delid) {
    console.clear();
    console.log(value);
    this.fiscalyearservice.editFiscalyear(value,delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.fiscalyearservice.getfiscalyeardata().subscribe(result => {
        this.resultfiscalyear = result
      })
    })
  }

  DetailFiscalYear(id:any){
    this.router.navigate(['/fiscalyear/detailfiscalyear',id])
  }
}
