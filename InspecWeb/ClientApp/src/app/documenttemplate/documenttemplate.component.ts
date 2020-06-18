import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { DocumenttemplateService } from '../services/documenttemplate.service';

@Component({
  selector: 'app-documenttemplate',
  templateUrl: './documenttemplate.component.html',
  styleUrls: ['./documenttemplate.component.css']
})
export class DocumenttemplateComponent implements OnInit {
  resultDocumenttemplate: any = []
  delid: any
  year: any
  title: any
  modalRef:BsModalRef;
  Form : FormGroup
  files: string[] = []
  loading = false;
  dtOptions: DataTables.Settings = {};

  constructor(private modalService: BsModalService, private fb: FormBuilder, private documenttemplateservice: DocumenttemplateService,
    public share: DocumenttemplateService) { }

  ngOnInit() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [5],
          orderable: false
        }
      ]

    };
    this.documenttemplateservice.getdocumenttemplate().subscribe(result=>{
    this.resultDocumenttemplate = result
    this.loading = true
    })
    this.Form = this.fb.group({
      title: new FormControl(null, [Validators.required]),
      year: new FormControl(null, [Validators.required]),
      files : new FormControl(null, [Validators.required])
    })
  }
  openModal(template: TemplateRef<any>, id, year,title) {
    this.delid = id;
    this.title = title;
    this.year = year;
    console.log(this.delid);
    console.log(this.title);
    console.log(this.year);
    
    this.modalRef = this.modalService.show(template);
  }
  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.Form.patchValue({
      files: file
    });
    this.Form.get('files').updateValueAndValidity()

  }

  storeDocumenttemplate(value) {
    // alert(JSON.stringify(value));
    this.documenttemplateservice.addDocumenttemplate(value, this.Form.value.files).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.documenttemplateservice.getdocumenttemplate().subscribe(result => {
        this.resultDocumenttemplate = result
        console.log(this.resultDocumenttemplate);
      })
    })
  }
  deleteDocumenttemplate(value) {
    this.documenttemplateservice.deleteDocumenttemplate(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.documenttemplateservice.getdocumenttemplate().subscribe(result => {
        this.resultDocumenttemplate = result
        console.log(this.resultDocumenttemplate);
      })
    })
  }
  
  editDocumenttemplate(value,delid) {
    console.log(value);
    this.documenttemplateservice.editDocumenttemplate(value,delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.documenttemplateservice.getdocumenttemplate().subscribe(result => {
        this.resultDocumenttemplate = result
       
      })
    })
  }

}

