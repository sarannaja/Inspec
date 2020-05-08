import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { IMyOptions } from 'mydatepicker-th';
import { FormGroup, FormBuilder, FormArray, FormControl, Validators } from '@angular/forms';
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
import { ElectronicbookService } from 'src/app/services/electronicbook.service';
import { DepartmentService } from 'src/app/services/department.service';

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
  resultuser: any;
  provinceId: any;
  resultministrypeople: any = [];
  selectministrypeople: any = [];
  resultpeople: any = [];
  selectpeople: any = [];
  inspectionplan: any = [];
  provinceID: any;
  fileStatus = false;
  form: FormGroup;
  provincename: string;
  temp = []
  subjectdepartmentId: Array<any> = []
  provincetodepartmentId: any;
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
    private electronicBookService: ElectronicbookService,
    private departmentservice: DepartmentService,
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
      input: new FormArray([]),
      checkDetail: new FormControl(null, [Validators.required]),
      UserMinistryId: new FormControl(null, [Validators.required]),
      UserPeopleId: new FormControl(null, [Validators.required]),
      Status: new FormControl("ร่างกำหนดการ", [Validators.required]),
      Problem: new FormControl(null, [Validators.required]),
      Suggestion: new FormControl(null, [Validators.required]),
    });
    this.t.push(this.fb.group({
      // start_date_plan: '',
      // end_date_plan: '',
      provinces: [],
      centralpolicies: [],
      resultcentralpolicy: [],
      resultdetailcentralpolicy: ''
    }))

    this.form = this.fb.group({
      files: [null]
    })

    this.userservice.getprovincedata(this.userid).subscribe(result => {
      this.resultprovince = result

      this.selectdataprovince = this.resultprovince.map((item, index) => {
        return { value: item.province.id, label: item.province.name }
      })
      console.log(this.resultprovince);
    })

    this.userservice.getuserdata(6).subscribe(result => {
      // alert(JSON.stringify(result))
      this.resultministrypeople = result
      console.log(this.resultministrypeople);
      this.selectministrypeople = this.resultministrypeople.map((item, index) => {
        return { value: item.id, label: item.name }
      })
    })

    this.userservice.getuserdata(7).subscribe(result => {
      // alert(JSON.stringify(result))
      this.resultpeople = result
      console.log(this.resultpeople);
      this.selectpeople = this.resultpeople.map((item, index) => {
        return { value: item.id, label: item.name }
      })
    })

  }

  DetailCentralPolicy(id: any, i) {
    this.getDepartmentdata();
    this.subjectservice.storesubjectprovince(id, this.province[i].value)
      .subscribe(result => {
        console.log("storesubjectprovince : " + result);
        // window.open(this.url + 'centralpolicy/detailcentralpolicyprovince/' + result);
        this.provinceId = result;
        this.getDetailCentralPolicyProvince(result);
      })
  }

  storeInspectionPlanEvent(value) {
    console.log("Store : ", value);
    // alert("DATA: " + JSON.stringify(value));
    this.electronicBookService.addElectronicBook(value, this.userid, this.form.value.files, this.subjectdepartmentId).subscribe(response => {
      console.log(value);
      this.Form.reset()
      // this.router.navigate(['inspectionplanevent'])
      console.log("get");
      window.history.back();
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
    this.provincename = event.label;
    this.provincetodepartmentId = event.value;
    this.province[i] = event;
    // alert(JSON.stringify(event));
    console.log("item", item);

    this.provinceID = item.provinces;
    this.getinspectionplanservice(item.value.provinces);

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

    // alert(JSON.stringify(event))
    this.centralpolicyservice.getdetailcentralpolicydata(event.value)
      .subscribe(result => {
        this.resultdetailcentralpolicy = result
        console.log("jjjj", this.resultdetailcentralpolicy.title);
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
        console.log("getDetail", result);

        this.resultdetailcentralpolicyData = result.centralpolicydata
        console.log("DATA: ", this.resultdetailcentralpolicyData);

        this.resultuser = result.userdata;
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

  editsubquestionclose(value, id) {
    this.subjectservice.editsubquestionprovince(value, id).subscribe(response => {
      console.log(value);
      this.EditForm.reset()
      this.modalRef.hide()
      this.getDetailCentralPolicyProvince(this.provinceId);
    })
  }

  editsubquestionclosechoice(value, id) {
    this.subjectservice.editsubquestionchoiceprovince(value, id).subscribe(response => {
      console.log(value);
      this.EditForm2.reset()
      this.modalRef.hide()
      this.getDetailCentralPolicyProvince(this.provinceId);
    })
  }

  goBack() {
    window.history.back();
  }

  getinspectionplanservice(pID) {
    // alert("DD" + pID)
    this.electronicBookService.getNotSelectedInspectionPlan(pID).subscribe(result => {
      this.resultinspectionplan = result //Chose

      console.log("selected: ", this.resultinspectionplan);
      this.centralpolicyservice.getcentralpolicyfromprovince(pID)
        .subscribe(result => {
          this.resultcentralpolicy = result //All
          console.log("all: ", this.resultcentralpolicy);
          this.getRecycled();
          // alert(JSON.stringify(this.resultcentralpolicy))
        })
    })
  }

  getRecycled() {
    this.selectdatacentralpolicy = []
    this.inspectionplan = this.resultinspectionplan

    if (this.inspectionplan.length == 0) {
      for (var i = 0; i < this.resultcentralpolicy.length; i++) {
        this.selectdatacentralpolicy.push({ value: this.resultcentralpolicy[i].centralPolicyId, label: this.resultcentralpolicy[i].centralPolicy.title })
      }
    }
    else {
      for (var i = 0; i < this.resultcentralpolicy.length; i++) {
        var n = 0;
        for (var ii = 0; ii < this.inspectionplan.length; ii++) {
          if (this.resultcentralpolicy[i].centralPolicyId == this.inspectionplan[ii].centralPolicyId) {
            n++;
          }
        }
        if (n == 0) {
          this.selectdatacentralpolicy.push({ value: this.resultcentralpolicy[i].centralPolicyId, label: this.resultcentralpolicy[i].centralPolicy.title })
        }
      }
    }
  }

  uploadFile(event) {
    this.fileStatus = true;
    const file = (event.target as HTMLInputElement).files;

    this.form.patchValue({
      files: file
    });
    console.log("fff:", this.form.value.files)
    this.form.get('files').updateValueAndValidity()
  }

  getDepartmentdata() {
    // this.resultprovince.forEach((element, index) => {
    //   console.log('element', element);
    // alert(this.provincetodepartmentId)

    this.departmentservice.getdepartmentdata(this.provincetodepartmentId)
      .subscribe(result => {
        console.log("Result : ", result);
        this.temp = result.map((item, index) => {
          return {
            value: item.id,
            label: item.provincialDepartment.name,
            provincialDepartmentID: item.provincialDepartmentID,
            provinceId: item.provinceId
          }
        })
        console.log("nik : ", this.temp);
      })
    // });
  }

  addsubjectdepartment2(value, input) {
    // var subject = value.vaule

    if (input == 'add') {
      this.subjectdepartmentId = this.addSubjectdepartments(this.subjectdepartmentId, value)
      console.log(this.subjectdepartmentId);
    } else {
      this.subjectdepartmentId = this.removeSubjectdepartments(this.subjectdepartmentId, value)
      console.log(this.subjectdepartmentId);

    }
  }

  removeSubjectdepartments(array: any, value) {
    var s = new Set(array);
    s.delete(value);
    return Array.from(s);
  }

  addSubjectdepartments(array: any, value) {
    var s = new Set(array);
    s.add(value);
    return Array.from(s);
  }
}
