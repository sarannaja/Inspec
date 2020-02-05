import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-region',
  templateUrl: './region.component.html',
  styleUrls: ['./region.component.css']
})
export class RegionComponent implements OnInit {

  modalRef: BsModalRef;
  form: FormGroup;
  forbiddenUsernames = ['admin', 'test', 'xxxx'];
  constructor(private modalService: BsModalService, private fb: FormBuilder) { }

  ngOnInit() {
    this.form = this.fb.group({
      "regionname": new FormControl(null, [Validators.required]),
      "test": new FormControl(null, [Validators.required, this.forbiddenNames.bind(this)])
    })
    this.form.patchValue({
      test: "test2"
    })
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  forbiddenNames(control: FormControl): { [s: string]: boolean } {
    if (this.forbiddenUsernames.indexOf(control.value) !== -1) {
      return { 'forbiddenNames': true };
    }
    return null;
  }

  storeRegion(value){
    alert(JSON.stringify(value));
    this.form.reset();
    this.modalRef.hide();
  }

}
