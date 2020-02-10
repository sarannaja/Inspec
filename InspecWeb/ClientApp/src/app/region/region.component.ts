import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { RegionService } from '../services/region.service';

@Component({
  selector: 'app-region',
  templateUrl: './region.component.html',
  styleUrls: ['./region.component.css']
})
export class RegionComponent implements OnInit {

  resultregion: any = []
  delid: any
  name: any
  modalRef: BsModalRef;
  Form: FormGroup
  forbiddenUsernames = ['admin', 'test', 'xxxx'];
  constructor(private modalService: BsModalService, private fb: FormBuilder, private regionservice: RegionService,
    public share: RegionService) { }

  ngOnInit() {
    this.Form = this.fb.group({
      "regionname": new FormControl(null, [Validators.required]),
     // "test": new FormControl(null, [Validators.required, this.forbiddenNames.bind(this)])
    })
    this.Form.patchValue({
     // test: "test2"
    })

    
    this.regionservice.getregiondata().subscribe(result => {
      this.resultregion = result
      console.log(this.resultregion);
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
    this.Form.reset();
    this.modalRef.hide();
  }

}
