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
  resultcabine: any = []
  delid:any
  name:any
  position:any
  image:any
  modalRef:BsModalRef;
  Form : FormGroup

  constructor(private modalService: BsModalService, private fb: FormBuilder, private cabineservice: CabineService,
    public share: CabineService) { }

  ngOnInit() {
    this.cabineservice.getcabine().subscribe(result=>{
    this.resultcabine = result
    })
    this.Form = this.fb.group({
      "name" : new FormControl(null, [Validators.required]),
      "position": new FormControl(null, [Validators.required])
    })
  }
  openModal(template: TemplateRef<any>, id, name, position) {
    this.delid = id;
    this.name = name;
    this.position = position
    console.log(this.delid);
    console.log(this.name);
    console.log(this.position);
    
    this.modalRef = this.modalService.show(template);
  }
  storeCabine(value) {
    // alert(JSON.stringify(value));
    this.cabineservice.addCabine(value).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.cabineservice.getcabine().subscribe(result => {
        this.resultcabine = result
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
