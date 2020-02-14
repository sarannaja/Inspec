import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { SubdistrictService } from '../services/subdistrict.service';

@Component({
  selector: 'app-subdistrict',
  templateUrl: './subdistrict.component.html',
  styleUrls: ['./subdistrict.component.css']
})
export class SubdistrictComponent implements OnInit {

  resultsubdistrict: any = []
  id

  constructor(
    private modalService: BsModalService,
    private fb: FormBuilder,
    private subdistrictservice: SubdistrictService,
    private activatedRoute : ActivatedRoute,
    public share: SubdistrictService) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    }

  ngOnInit() {

    this.subdistrictservice.getsubdistrictdata(this.id).subscribe(result => {
      this.resultsubdistrict = result
      console.log(this.resultsubdistrict);
    })
  }

}
