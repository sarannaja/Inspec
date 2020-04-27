import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { CabineService } from '../services/cabine.service';

@Component({
  selector: 'app-cabinet',
  templateUrl: './cabinet.component.html',
  styleUrls: ['./cabinet.component.css']
})
export class CabinetComponent implements OnInit {
  files: string[] = []
  resultcabine: any = []
  delid:any
  name:any
  position:any
  image:string[] = []
  prefix:any
  detail:any
  modalRef:BsModalRef;
  Form : FormGroup;
  loading = false;
  dtOptions: DataTables.Settings = {};

  constructor(
    private modalService: BsModalService, 
    private fb: FormBuilder, 
    private cabineservice: CabineService,
    public share: CabineService,
    ) { }

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
    this.cabineservice.getcabine().subscribe(result=>{
    this.resultcabine = result
    this.loading = true
    })
    
    this.Form = this.fb.group({
      name : new FormControl(null, [Validators.required]),
      position: new FormControl(null, [Validators.required]),
      prefix : new FormControl(null, [Validators.required]),
      detail : new FormControl(null, [Validators.required]),
      files : new FormControl(null, [Validators.required])
    })
  }
  openModal(template: TemplateRef<any>, id, name, position, prefix, detail) {
    this.delid = id;
    this.name = name;
    this.position = position;
    this.prefix = prefix;
    this.detail = detail;
    
    console.log(this.delid);
    console.log(this.name);
    console.log(this.position);
    console.log(this.prefix);
    console.log(this.detail);
    
    
    this.modalRef = this.modalService.show(template);
  }
  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.Form.patchValue({
      files: file
    });
    this.Form.get('files').updateValueAndValidity()

  }
  storeCabine(value) {
     //alert("9999");
    this.cabineservice.addCabine(value, this.Form.value.files).
    subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.cabineservice.getcabine().subscribe(result => {
        this.resultcabine = result
        //  alert("kklkmdasda");
        console.log(this.resultcabine);
      })
    })
  }
  deleteCabine(value) {
    this.cabineservice.deleteCabine(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.cabineservice.getcabine().subscribe(result => {
        this.resultcabine = result
        console.log(this.resultcabine);
      })
    })
  }
  editCabine(value,delid) {
    console.clear();
    console.log(value);
    this.cabineservice.editCabine(value,delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.cabineservice.getcabine().subscribe(result => {
        this.resultcabine = result
       
      })
    })
  }

}
