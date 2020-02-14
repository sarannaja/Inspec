import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { DistrictService } from '../services/district.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-district',
  templateUrl: './district.component.html',
  styleUrls: ['./district.component.css']
})
export class DistrictComponent implements OnInit {

  resultdistrict: any = []
  id
  // router: any

  constructor(
    private modalService: BsModalService,
    private fb: FormBuilder,
    private districtservice: DistrictService,
    private activatedRoute : ActivatedRoute,
    private router:Router,
    public share: DistrictService) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
  }

  ngOnInit() {

    this.districtservice.getdistrictdata(this.id).subscribe(result => {
      this.resultdistrict = result
      console.log(this.resultdistrict);
    })
  }
  Subdistrict(id){
    this.router.navigate(['/subdistrict',id])
  }
}
