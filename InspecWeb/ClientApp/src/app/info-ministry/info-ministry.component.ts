import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { MinistryService } from '../services/ministry.service';

@Component({
  selector: 'app-info-ministry',
  templateUrl: './info-ministry.component.html',
  styleUrls: ['./info-ministry.component.css']
})
export class InfoMinistryComponent implements OnInit {

  resultministry: any[] = []
  delid:any
  name:any
  modalRef:BsModalRef;
  Form : FormGroup
  EditForm: FormGroup;
  loading = false;
  dtOptions: DataTables.Settings = {};

  constructor(private modalService: BsModalService, private fb: FormBuilder, private ministryservice: MinistryService,
    public share: MinistryService) { }

  ngOnInit() {
    this.ministryservice.getministry().subscribe(result=>{
      this.resultministry = result
      this.loading = true;
    })
    this.Form = this.fb.group({
      "ministryname": new FormControl(null, [Validators.required]),
    })
  }

}
