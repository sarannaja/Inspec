import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DepartmentService } from '../services/department.service';

@Component({
  selector: 'app-info-department',
  templateUrl: './info-department.component.html',
  styleUrls: ['./info-department.component.css']
})
export class InfoDepartmentComponent implements OnInit {
  resultdepartment: any = []
  id
  name: any
  ministryName:any;
  Form: FormGroup;
  EditForm: FormGroup;
  loading = false;
  dtOptions: DataTables.Settings = {};
 

  constructor(
    private modalService: BsModalService,
    private fb: FormBuilder,
    private departmentService: DepartmentService,
    private activatedRoute : ActivatedRoute,
    private router:Router,
  ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
  }

  ngOnInit() {
    this.departmentService.getdepartmentsforsupportdata(this.id).subscribe(result => {
      // console.log('data',result);
      this.resultdepartment = result;
      this.ministryName = result[0].ministries.name;
      this.loading = true
   })
  }
}
