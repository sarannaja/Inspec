import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { DistrictService } from '../services/district.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-info-district',
  templateUrl: './info-district.component.html',
  styleUrls: ['./info-district.component.css']
})
export class InfoDistrictComponent implements OnInit {
  resultdistrict: any = []
  id
  name: any
  titleprovince:[]
  Form: FormGroup;
  EditForm: FormGroup;
  loading = false;
  dtOptions: DataTables.Settings = {};
  // router: any

  constructor(
    private modalService: BsModalService,
    private fb: FormBuilder,
    private districtservice: DistrictService,
    private activatedRoute : ActivatedRoute,
    private router:Router,
    public share: DistrictService) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.name = activatedRoute.snapshot.paramMap.get('name')
  }

  ngOnInit() {

    // alert(this.name)

    this.districtservice.getdistrictdata(this.id).subscribe(result => {
      this.resultdistrict = result
      this.titleprovince = result[0].province.name
      this.loading = true
      console.log(this.resultdistrict);
    })
  }
  Subdistrict(id){
    this.router.navigate(['/infosubdistrict',id])
  }
}