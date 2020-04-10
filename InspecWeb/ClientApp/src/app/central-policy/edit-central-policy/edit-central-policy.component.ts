import { Component, OnInit } from '@angular/core';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { FormControl, Validators, FormGroup, FormBuilder } from '@angular/forms';
import { IOption } from 'ng-select';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FiscalyearService } from 'src/app/services/fiscalyear.service';
import { ProvinceService } from 'src/app/services/province.service';

@Component({
  selector: 'app-edit-central-policy',
  templateUrl: './edit-central-policy.component.html',
  styleUrls: ['./edit-central-policy.component.css']
})
export class EditCentralPolicyComponent implements OnInit {

  id: any;
  fiscalYearId: any;
  year: any;
  resultdetailcentralpolicy: any = []
  resultfiscalyear: any = []
  resultfiscalyearId: any = []
  fiscalYearIdString: any = [];
  resultprovince: any = []
  EditForm: FormGroup;
  selectdataprovince: Array<IOption>

  constructor(
    private fb: FormBuilder,
    private modalService: BsModalService,
    private centralpolicyservice: CentralpolicyService,
    private userservice: UserService,
    private activatedRoute: ActivatedRoute,
    private fiscalyearservice: FiscalyearService,
    private provinceservice: ProvinceService
  ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
  }

  ngOnInit() {
    this.getDetailCentralpolicy()
    this.getFiscalyear()
    this.getProvince()
  }
  getDetailCentralpolicy() {
    this.centralpolicyservice.getdetailcentralpolicydata(this.id)
      .subscribe(result => {
        this.resultdetailcentralpolicy = result
        this.fiscalYearId = this.resultdetailcentralpolicy.fiscalYearId
      })
  }
  getFiscalyear() {
    this.fiscalyearservice.getfiscalyeardata().subscribe(result => {
      this.resultfiscalyear = result

      this.fiscalYearIdString = this.resultfiscalyear.map((item, index) => {
        return {
          id: item.id.toString(),
          year: item.year.toString()
        }
      })
      console.log("TEST: ", this.fiscalYearIdString);

    })
  }
  getProvince() {
    this.provinceservice.getprovincedata().subscribe(result => {
      this.resultprovince = result
      this.selectdataprovince = this.resultprovince.map((item, index) => {
        return { value: item.id, label: item.name }
      })
    })
  }


  EditCentralpolicy(value) {
    console.log(value);
  }
}
