import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { MinistermonitoringService } from '../services/ministermonitoring.service';

@Component({
  selector: 'app-ministermonitoring',
  templateUrl: './ministermonitoring.component.html',
  styleUrls: ['./ministermonitoring.component.css']
})
export class MinistermonitoringComponent implements OnInit {

  resultMinistermonitoring: any = []
  delid: any
  name: any
  position: any
  image: any
  createAt: any
  modalRef: BsModalRef;
  Form: FormGroup
  loading = false

  constructor(private modalService: BsModalService, private fb: FormBuilder, private ministermonitoringservice: MinistermonitoringService,
    public share: MinistermonitoringService) { }

  ngOnInit() {
    console.log(this.modalRef);
    this.ministermonitoringservice.getministermonitoringdata().subscribe(result => {
      this.resultMinistermonitoring = result
      this.loading = true
    })

    this.Form = this.fb.group({
      "name": new FormControl(null, [Validators.required]),
      "position": new FormControl(null, [Validators.required]),
      "image": new FormControl(null, [Validators.required]),
      "createAt": new FormControl(null, [Validators.required]),
    })
  }
  openModal(template: TemplateRef<any>, modalType: string = 'edit') {
    modalType != 'edit' ? this.Form.reset() : null;
    this.modalRef = this.modalService.show(template);

  }

  onEdit(modaleditMinistermonitoring, item) {
    this.openModal(modaleditMinistermonitoring)
    this.delid = item.id;
    this.name = item.name
    console.log(this.delid);
    console.log(this.name);
    this.Form.patchValue(item)

  }
  storeMinistermonitoring(value) {
    // alert(JSON.stringify(value));
    this.ministermonitoringservice.addMinistermonitoring(value).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.ministermonitoringservice.getministermonitoringdata().subscribe(result => {
        this.resultMinistermonitoring = result
        console.log(this.resultMinistermonitoring);
      })
    })
  }
  deleteMinistermonitoring(value) {
    this.ministermonitoringservice.deleteMinistermonitoring(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.ministermonitoringservice.getministermonitoringdata().subscribe(result => {
        this.resultMinistermonitoring = result
        console.log(this.resultMinistermonitoring);
      })
    })
  }
  editMinistermonitoring(value, delid) {
    console.clear();
    console.log(value);
    this.ministermonitoringservice.editMinistermonitoring(value, delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.ministermonitoringservice.getministermonitoringdata().subscribe(result => {
        this.resultMinistermonitoring = result

      })
    })
  }

}
