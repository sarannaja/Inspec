import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { MinistryService } from '../services/ministry.service';
import { Router } from '@angular/router';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';

@Component({
  selector: 'app-info-ministry',
  templateUrl: './info-ministry.component.html',
  styleUrls: ['./info-ministry.component.css']
})
export class InfoMinistryComponent implements OnInit {
  [x: string]: any;

  resultministry: any[] = []
  delid: any
  name: any
  modalRef: BsModalRef;
  Form: FormGroup
  EditForm: FormGroup;
  loading = false;
  dtOptions: any = {};
  userid: any;

  constructor(
    private modalService: BsModalService,
    private fb: FormBuilder,
    private ministryservice: MinistryService,
    public share: MinistryService,
    private router: Router,
    private authorize: AuthorizeService,
  ) { }

  ngOnInit() {
    this.ministryservice.getministry().subscribe(result => {
      this.resultministry = result
      this.loading = true;
    })
    this.Form = this.fb.group({
      "ministryname": new FormControl(null, [Validators.required]),
    })

    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
      })
  }

  infodepartment(id) {
    if (this.userid == null) {
      this.router.navigate(['infoministrymain/' + id + '/infodepartment'])
    } else {
      this.router.navigate(['infoministry/' + id + '/infodepartment'])
    }

  }

  //<!-- excel -->
  excelministry() {
    window.location.href = '/api/ministry/excelministry';
  }
  //<!-- END excel -->
}
