import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { DistrictService } from '../services/district.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';

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
  dtOptions: any = {};
  // router: any
  userid: any;

  constructor(
    private modalService: BsModalService,
    private fb: FormBuilder,
    private districtservice: DistrictService,
    private activatedRoute : ActivatedRoute,
    private router:Router,
    public share: DistrictService,
    private authorize: AuthorizeService,
    ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.name = activatedRoute.snapshot.paramMap.get('name')
  }

  ngOnInit() {
    this.getdata();
    this.authorize.getUser()
    .subscribe(result => {
      this.userid = result.sub
  })
  }

  getdata(){
    this.districtservice.getdistrictdata(this.id).subscribe(result => {
      this.resultdistrict = result
      this.titleprovince = result[0].province.name
      this.loading = true
     // console.log(this.resultdistrict);
    })
  }
  Subdistrict(id){
    if (this.userid == null) {
      this.router.navigate(['/infosubdistrictmain',id])
    } else {
      this.router.navigate(['/infosubdistrict',id])
    }

  }
   //<!-- excel -->
   excel(){
    window.location.href = '/api/district/exceldistrict/'+ this.id;
  }
  //<!-- END excel -->

  //<!-- Word -->
  word() {
    this.districtservice.worddistrict(this.id)
      .subscribe(result => {
       // alert(result.data);
        //console.log('result', result);
        window.open("reportdistrict/" + result.data);
        this.getdata();
      })
  }
  //<!-- END Word -->
}
