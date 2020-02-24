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

  resultregion: any[] = []
  delid: any
  name: any
  modalRef: BsModalRef;
  Form: FormGroup
  EditForm: FormGroup;
  loading = false;
  dtOptions: DataTables.Settings = {};
  forbiddenUsernames = ['admin', 'test', 'xxxx'];
  constructor(private modalService: BsModalService, private fb: FormBuilder, private regionservice: RegionService,
    public share: RegionService) { }

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

    this.Form = this.fb.group({
      "regionname": new FormControl(null, [Validators.required]),
      // "test" : new FormControl(null,[Validators.required,this.forbiddenNames.bind(this)])
    })

    //แก้ไข
    this.Form.patchValue({
      // test: "testest"
    })

    this.regionservice.getregiondata().subscribe(result => {
      this.resultregion = result
      this.loading = true;
      console.log(this.resultregion);
    })
  }
  openModal(template: TemplateRef<any>, id, name) {
    this.delid = id;
    this.name = name;
    console.log(this.delid);
    console.log(this.name);

    this.modalRef = this.modalService.show(template);
  }
  forbiddenNames(control: FormControl): { [s: string]: boolean } {
    if (this.forbiddenUsernames.indexOf(control.value) !== -1) {
      return { 'forbiddenNames': true };
    }
    return null;
  }

  storeRegion(value) {
    this.regionservice.addRegion(value).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.regionservice.getregiondata().subscribe(result => {
        this.resultregion = result
        this.loading = true;
        console.log(this.resultregion);
      })
    })
  }
  deleteRegion(value) {
    this.regionservice.deleteRegion(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.loading = false;
      this.regionservice.getregiondata().subscribe(result => {
        this.resultregion = result
        this.loading = true;
        console.log(this.resultregion);
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
      "regionname": new FormControl(null, [Validators.required]),
      // "test" : new FormControl(null,[Validators.required,this.forbiddenNames.bind(this)])
    })
    this.EditForm.patchValue({
      "regionname": name
    })
  }
  editRegion(value,delid) {
    console.clear();
    console.log(value);
    this.regionservice.editRegion(value,delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.regionservice.getregiondata().subscribe(result => {
        this.resultregion = result
        this.loading = true;
      })
    })
  }
}
