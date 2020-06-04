import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { StatepolicyService } from '../services/statepolicy.service';

@Component({
  selector: 'app-statepolicy',
  templateUrl: './statepolicy.component.html',
  styleUrls: ['./statepolicy.component.css']
})
export class StatepolicyComponent implements OnInit {
  resultstatepolicy: any = []
  id:any
  modalRef:BsModalRef;
  Form : FormGroup
  files: string[] = []
  loading = false;
  dtOptions: DataTables.Settings = {};

  constructor(
    private modalService: BsModalService, 
    private fb: FormBuilder, 
    private statepolicyservice: StatepolicyService,
    public share: StatepolicyService
  ) { }

  ngOnInit() {
    this.statepolicyservice.getstatepolicydata(this.id).subscribe(result => {
      console.log('res',result)
      this.resultstatepolicy = result
      this.loading = true
    })
  }

}
