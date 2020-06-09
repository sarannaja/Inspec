import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { SubdistrictService } from '../services/subdistrict.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-subdistrict',
  templateUrl: './subdistrict.component.html',
  styleUrls: ['./subdistrict.component.css']
})
export class SubdistrictComponent implements OnInit {

  resultsubdistrict: any = []
  id
  titleprovince: []
  titledistrict: []

  constructor(
    private modalService: BsModalService,
    private fb: FormBuilder,
    private subdistrictservice: SubdistrictService,
    private activatedRoute : ActivatedRoute,
    private router:Router,
    public share: SubdistrictService) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
  }

  ngOnInit() {

    this.subdistrictservice.getsubdistrictdata(this.id).subscribe(result => {
      this.resultsubdistrict = result
      this.titleprovince = result[0].district.province.name
      this.titledistrict = result[0].district.name
      console.log(this.resultsubdistrict);
    })
  }
  Subvillage(id){
    this.router.navigate(['/info-village',id])
  }
}
