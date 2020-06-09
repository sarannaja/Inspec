import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { VillageService } from '../services/village.service';

@Component({
  selector: 'app-info-village',
  templateUrl: './info-village.component.html',
  styleUrls: ['./info-village.component.css']
})
export class InfoVillageComponent implements OnInit {

  resultvillage: any = []
  id
  titleprovince: []
  titledistrict: []
  titlesubdistrict: []
  Form: FormGroup;
  EditForm: FormGroup;
  loading = false;
  dtOptions: DataTables.Settings = {};
  modalRef: BsModalRef;

  constructor(
    private villageservice: VillageService,
    private activatedRoute : ActivatedRoute,
    public share: VillageService) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
   }

  ngOnInit() {
    this.villageservice.getvillagedata(this.id).subscribe(result => {
      console.log('res',result)
      this.resultvillage = result
      this.titlesubdistrict = result[0].subdistrict.name
      this.titledistrict = result[0].subdistrict.district.name
      this.titleprovince = result[0].subdistrict.district.province.name
      this.loading = true
      console.log(this.resultvillage);
    })
  }

}
