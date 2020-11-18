import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SubdistrictService } from '../services/subdistrict.service';

@Component({
  selector: 'app-info-subdistrict',
  templateUrl: './info-subdistrict.component.html',
  styleUrls: ['./info-subdistrict.component.css']
})
export class InfoSubdistrictComponent implements OnInit {

  resultsubdistrict: any = []
  id
  titleprovince: []
  titledistrict: []
  Form: FormGroup;
  EditForm: FormGroup;
  loading = false;
  dtOptions: any = {};
  modalRef: BsModalRef;

  constructor(
    private router:Router,
    private subdistrictservice: SubdistrictService,
    private activatedRoute: ActivatedRoute,
    public share: SubdistrictService) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
  }
    
  ngOnInit() {
    this.subdistrictservice.getsubdistrictdata(this.id).subscribe(result => {
      this.resultsubdistrict = result
      this.titleprovince = result[0].district.province.name
      this.titledistrict = result[0].district.name
      this.loading = true
      console.log(this.resultsubdistrict);
    })
  }
  Village(id) {
    this.router.navigate(['/infovillage',id])
  }

}
