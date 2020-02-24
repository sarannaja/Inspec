import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { MinistryService } from '../services/ministry.service';


@Component({
  selector: 'app-ministry',
  templateUrl: './ministry.component.html',
  styleUrls: ['./ministry.component.css']
})
export class MinistryComponent implements OnInit {
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

    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [3],
          orderable: false
        }
      ]

    };

    this.ministryservice.getministry().subscribe(result=>{
      this.resultministry = result
      this.loading = true;
    })
    this.Form = this.fb.group({
      "ministryname": new FormControl(null, [Validators.required]),
    })
  }
  openModal(template: TemplateRef<any>, id, name) {
    this.delid = id;
    this.name = name;
    console.log(this.delid);
    console.log(this.name);
    
    this.modalRef = this.modalService.show(template);
  }
  storeMinistry(value) {
    this.ministryservice.addMinistry(value).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.ministryservice.getministry().subscribe(result => {
        this.resultministry = result
        this.loading = true;
        console.log(this.resultministry);
      })
    })
  }
  deleteMinistry(value) {
    this.ministryservice.deleteMinistry(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.loading = false;
      this.ministryservice.getministry().subscribe(result => {
        this.resultministry = result
        this.loading = true;
        console.log(this.resultministry);
      })
    })
  }
  editModal(template: TemplateRef<any>, id, name) {
    this.delid = id;
    this.name = name
    console.log(this.delid);
    console.log(this.name);

    this.modalRef = this.modalService.show(template);
    this.EditForm = this.fb.group({
      "ministryname": new FormControl(null, [Validators.required]),
      // "test" : new FormControl(null,[Validators.required,this.forbiddenNames.bind(this)])
    })
    this.EditForm.patchValue({
      "ministryname": name
    })
  }
  editMinistry(value,delid) {
    console.clear();
    console.log(value);
    this.ministryservice.editMinistry(value,delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.ministryservice.getministry().subscribe(result => {
        this.resultministry = result
        this.loading = true;
       
      })
    })
  }

}
