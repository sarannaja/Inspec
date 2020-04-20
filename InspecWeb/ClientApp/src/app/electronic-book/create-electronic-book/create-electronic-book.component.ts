import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { IMyOptions } from 'mydatepicker-th';
import { FormGroup, FormBuilder, FormArray, FormControl } from '@angular/forms';
import { IOption } from 'ng-select';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { Router } from '@angular/router';
import { InspectionplaneventService } from 'src/app/services/inspectionplanevent.service';
import { UserService } from 'src/app/services/user.service';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { SubjectService } from 'src/app/services/subject.service';
import { ProvinceService } from 'src/app/services/province.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

interface addInput {
  id: number;
  name: string;
}

@Component({
  selector: 'app-create-electronic-book',
  templateUrl: './create-electronic-book.component.html',
  styleUrls: ['./create-electronic-book.component.css']
})
export class CreateElectronicBookComponent implements OnInit {

  url = "";
  private myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd/mm/yyyy',
  };
  userid: string
  files: string[] = []
  resultinspectionplan: any = []
  resultprovince: any = []
  resultcentralpolicy: any = []
  resultdetailcentralpolicy: any = []
  province: any = []
  delid: any
  title: any
  start_date: any
  end_date: any
  Form: FormGroup;
  selectdataprovince: Array<IOption>
  selectdatacentralpolicy: Array<IOption>
  id: any = 1
  input: any = [{ start_date_plan: '', end_date_plan: '', province: '' }]
  resultdetailcentralpolicyprovince: any = [];
  loading = false;
  resultdetailcentralpolicyData: any = [];
  resultdetailcentralpolicyprovinceData: any = [];
  EditForm: FormGroup;
  EditForm2: FormGroup;
  modalRef: BsModalRef;
  editid: any;
  subquestionclosename: any
  subquestionclosechoicename: any

  get f() { return this.Form.controls }
  get t() { return this.f.input as FormArray }

  constructor(
    private fb: FormBuilder, private authorize: AuthorizeService,
    private router: Router, private inspectionplaneventservice: InspectionplaneventService,
    private userservice: UserService,
    private centralpolicyservice: CentralpolicyService,
    private subjectservice: SubjectService,
    private provinceservice: ProvinceService,
    private spinner: NgxSpinnerService,
    private modalService: BsModalService,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.url = baseUrl;
  }

  ngOnInit() {

    this.EditForm = this.fb.group({
      subquestionclose: new FormControl(),
    })

    this.EditForm2 = this.fb.group({
      subquestionclosechoice: new FormControl(),
    })

    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
        // alert(this.userid)
      })

    this.Form = this.fb.group({
      //title: new FormControl(null, [Validators.required]),
      input: new FormArray([])
      // start_date: new FormControl(null, [Validators.required]),
      // end_date: new FormControl(null, [Validators.required]),
    });
    this.t.push(this.fb.group({
      // start_date_plan: '',
      // end_date_plan: '',
      provinces: [],
      centralpolicies: [],
      resultcentralpolicy: [],
      resultdetailcentralpolicy: ''
    }))

    this.userservice.getprovincedata(this.userid).subscribe(result => {
      this.resultprovince = result

      this.selectdataprovince = this.resultprovince.map((item, index) => {
        return { value: item.province.id, label: item.province.name }
      })
      console.log(this.resultprovince);
    })
  }

  DetailCentralPolicy(id: any, i) {
    this.subjectservice.storesubjectprovince(id, this.province[i].value)
      .subscribe(result => {
        console.log("storesubjectprovince : " + result);
        // window.open(this.url + 'centralpolicy/detailcentralpolicyprovince/' + result);
        this.getDetailCentralPolicyProvince(result);
      })
  }

  storeInspectionPlanEvent(value) {
    console.log("Store : ", value);
    this.inspectionplaneventservice.addInspectionplanevent(value).subscribe(response => {
      console.log(value);
      this.Form.reset()
      // this.router.navigate(['inspectionplanevent'])
      console.log("get");
    })
  }

  append() {
    this.t.push(this.fb.group({
      start_date_plan: '',
      end_date_plan: '',
      provinces: [],
      centralpolicies: [],
      resultcentralpolicy: [],
      resultdetailcentralpolicy: '',
    }));
  }

  selectedprovince(event, i, item) {
    this.province[i] = event;
    // alert(JSON.stringify(event));
    console.log("item", item);
    this.t.at(i).get('resultcentralpolicy').patchValue([1, 2])

    this.centralpolicyservice.getcentralpolicyfromprovince(event.value)
      .subscribe(result => {
        this.resultcentralpolicy = result
        // alert(JSON.stringify(this.resultcentralpolicy))
        this.selectdatacentralpolicy = this.resultcentralpolicy.map((item, index) => {
          return { value: item.centralPolicy.id, label: item.centralPolicy.title }
        })
        this.t.at(i).get('resultcentralpolicy').patchValue(this.selectdatacentralpolicy)
        console.log("t", this.t.value);
      })
  }

  selectedcentralpolicy(event, i) {
    this.centralpolicyservice.getdetailcentralpolicydata(event.value)
      .subscribe(result => {
        this.resultdetailcentralpolicy = result
        console.log(this.resultdetailcentralpolicy.title);
        this.t.at(i).get('resultdetailcentralpolicy').patchValue({ id: this.resultdetailcentralpolicy.id, title: this.resultdetailcentralpolicy.title })
        console.log("t", this.t.value);
      })
  }

  remove(index: number) {
    this.t.removeAt(index);
  }
  back() {
    window.history.back();
  }

  getDetailCentralPolicyProvince(centralPolicyProvinceId) {
    console.log("TESTID: ", this.id);
    this.spinner.show();
    this.centralpolicyservice.getdetailcentralpolicyprovincedata(centralPolicyProvinceId)
      .subscribe(result => {
        console.log("getDetail" , result);
        // alert(JSON.stringify(result))
        this.resultdetailcentralpolicyData = result.centralpolicydata
        this.resultdetailcentralpolicyprovinceData = result.subjectcentralpolicyprovincedata
        setTimeout(() => {
          this.spinner.hide();
          this.loading = true;
        }, 300);

      })
  }

  editModal(template: TemplateRef<any>, id, name) {
    this.editid = id;
    this.subquestionclosename = name;

    this.modalRef = this.modalService.show(template);
    this.EditForm = this.fb.group({
      subquestionclose: new FormControl(),

    })
    this.EditForm.patchValue({
      subquestionclose: name,
    })
  }

  editModal2(template: TemplateRef<any>, id, name) {
    this.editid = id;
    this.subquestionclosechoicename = name;

    this.modalRef = this.modalService.show(template);
    this.EditForm2 = this.fb.group({
      subquestionclosechoice: new FormControl(),

    })
    this.EditForm2.patchValue({
      subquestionclosechoice: name,
    })
  }

}
