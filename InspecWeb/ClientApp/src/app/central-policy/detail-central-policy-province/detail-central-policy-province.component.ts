import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { FormControl, Validators, FormGroup, FormBuilder } from '@angular/forms';
import { IOption } from 'ng-select';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { SubjectService } from 'src/app/services/subject.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { async } from '@angular/core/testing';

@Component({
  selector: 'app-detail-central-policy-province',
  templateUrl: './detail-central-policy-province.component.html',
  styleUrls: ['./detail-central-policy-province.component.css']
})
export class DetailCentralPolicyProvinceComponent implements OnInit {

  resultuser: any = []
  resultpeople: any = []
  resultministrypeople: any = []
  resultdetailcentralpolicy: any = []
  resultcentralpolicyuser: any = []
  allMinistryPeople: any = [];
  allUserPeople: any = [];
  resultdetailcentralpolicyprovince: any = []
  UserPeopleId: any;
  // UserMinistryId: any;
  id
  Form: FormGroup;
  EditForm: FormGroup;
  EditForm2: FormGroup;
  selectpeople: Array<IOption>
  selectministrypeople: Array<IOption>
  modalRef: BsModalRef;
  editid: any
  subquestionclosename: any
  subquestionclosechoicename: any
  downloadUrl: any
  loading = false;
  electronicbookid: any
  selectdataministrypeople: Array<IOption>
  ministryPeople: any = [];
  selectdatapeople: Array<IOption>
  userPeople: any = [];

  constructor(private fb: FormBuilder,
    private modalService: BsModalService,
    private centralpolicyservice: CentralpolicyService,
    private userservice: UserService,
    private subjectservice: SubjectService,
    private activatedRoute: ActivatedRoute,
    private spinner: NgxSpinnerService,
    @Inject('BASE_URL') baseUrl: string) {
    this.id = activatedRoute.snapshot.paramMap.get('result')
    this.downloadUrl = baseUrl + '/Uploads';
  }

  async ngOnInit() {
    console.log("ID: ", this.id);

    this.spinner.show();
    this.Form = this.fb.group({
      UserPeopleId: new FormControl(null, [Validators.required]),
      // UserMinistryId: new FormControl(null, [Validators.required]),
    })

    this.EditForm = this.fb.group({
      subquestionclose: new FormControl(),
    })

    this.EditForm2 = this.fb.group({
      subquestionclosechoice: new FormControl(),
    })

    // this.userservice.getuserdata(7).subscribe(result => {
    //   // alert(JSON.stringify(result))
    //   this.resultpeople = result
    //   console.log(this.resultpeople);
    //   this.selectpeople = this.resultpeople.map((item, index) => {
    //     return { value: item.id, label: item.name }
    //   })
    // })
    // this.userservice.getuserdata(6).subscribe(result => {
    //   // alert(JSON.stringify(result))
    //   this.resultministrypeople = result
    //   console.log(this.resultministrypeople);
    //   this.selectministrypeople = this.resultministrypeople.map((item, index) => {
    //     return { value: item.id, label: item.name }
    //   })
    // })


    // this.getDetailCentralPolicy()
    await this.getCentralPolicyProvinceUser()
    await this.getDetailCentralPolicyProvince()

    await this.getMinistryPeople();
    await this.getUserPeople();

    setTimeout(() => {
      this.spinner.hide();
    }, 800);
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
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

  getDetailCentralPolicy() {
    this.centralpolicyservice.getdetailcentralpolicydata(this.id)
      .subscribe(result => {
        // this.resultdetailcentralpolicy = result
      })
  }

  getDetailCentralPolicyProvince() {
    this.centralpolicyservice.getdetailcentralpolicyprovincedata(this.id)
      .subscribe(result => {
        console.log("123", result);
        // alert(JSON.stringify(result))
        this.resultdetailcentralpolicy = result.centralpolicydata
        this.resultdetailcentralpolicyprovince = result.subjectcentralpolicyprovincedata
        this.resultuser = result.userdata
        this.electronicbookid = result.centralPolicyEventdata.electronicBookId
      })
  }

  getCentralPolicyProvinceUser() {
    this.centralpolicyservice.getcentralpolicyprovinceuserdata(this.id)
      .subscribe(result => {
        this.resultcentralpolicyuser = result
        // console.log("result" + result);
      })

  }


  storeFiles(value) {

  }

  storePeople(value) {
    // alert(JSON.stringify(value))
    this.centralpolicyservice.addCentralpolicyUser(value, this.id, this.electronicbookid).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.getCentralPolicyProvinceUser();
    })
  }

  storeMinistryPeople(value) {
    alert(JSON.stringify(value))
  }

  editsubquestionclose(value, id) {
    this.subjectservice.editsubquestionprovince(value, id).subscribe(response => {
      console.log(value);
      this.EditForm.reset()
      this.modalRef.hide()
      this.getDetailCentralPolicyProvince();
    })
  }

  editsubquestionclosechoice(value, id) {
    this.subjectservice.editsubquestionchoiceprovince(value, id).subscribe(response => {
      console.log(value);
      this.EditForm2.reset()
      this.modalRef.hide()
      this.getDetailCentralPolicyProvince();
    })
  }

  async getMinistryPeople() {
    await this.userservice.getuserdata(6).subscribe(result => {
      this.resultministrypeople = result // All
    })

    await this.centralpolicyservice.getcentralpolicyprovinceuserdata(this.id).subscribe(async result => {
      await result.forEach(async element => {
        if (element.user.role_id == 6) {
          await this.allMinistryPeople.push(element.user)
        }
      }); // Selected
      // console.log("selectedMinistry: ", this.allMinistryPeople);
      this.getRecycledMinistryPeople();
    })
  }

  async getRecycledMinistryPeople() {
    this.selectdataministrypeople = []
    this.ministryPeople = this.allMinistryPeople
    console.log("MINISTRY: ", this.ministryPeople);
    console.log("allMinistry: ", this.resultministrypeople);
    if (this.ministryPeople.length == 0) {
      for (var i = 0; i < this.resultministrypeople.length; i++) {
        await this.selectdataministrypeople.push({ value: this.resultministrypeople[i].id, label: this.resultministrypeople[i].name })
      }
    }
    else {
      for (var i = 0; i < this.resultministrypeople.length; i++) {
        var n = 0;
        for (var ii = 0; ii < this.ministryPeople.length; ii++) {
          if (this.resultministrypeople[i].id == this.ministryPeople[ii].id) {
            await n++;
          }
        }
        if (n == 0) {
          await this.selectdataministrypeople.push({ value: this.resultministrypeople[i].id, label: this.resultministrypeople[i].name })
        }
      }
    }
    // console.log("TEST: ", this.selectdataministrypeople);
  }

  async getUserPeople() {
    await this.userservice.getuserdata(7).subscribe(result => {
      // alert(JSON.stringify(result))
      this.resultpeople = result
    })
    await this.centralpolicyservice.getcentralpolicyprovinceuserdata(this.id).subscribe(async result => {
      await result.forEach(async element => {
        if (element.user.role_id == 7) {
          await this.allUserPeople.push(element.user)
        }
      }); // Selected
      console.log("selectedUser: ", this.allUserPeople);
      this.getRecycledUserPeople();
    })
  }

  async getRecycledUserPeople() {
    this.selectdatapeople = []
    this.userPeople = this.allUserPeople
    console.log("user: ", this.userPeople);
    console.log("allUser: ", this.resultpeople);

    if (this.userPeople.length == 0) {
      for (var i = 0; i < this.resultpeople.length; i++) {
        await this.selectdatapeople.push({ value: this.resultpeople[i].id, label: this.resultpeople[i].name })
      }
    }
    else {
      for (var i = 0; i < this.resultpeople.length; i++) {
        var n = 0;
        for (var ii = 0; ii < this.userPeople.length; ii++) {
          if (this.resultpeople[i].id == this.userPeople[ii].id) {
            await n++;
          }
        }
        if (n == 0) {
          await this.selectdatapeople.push({ value: this.resultpeople[i].id, label: this.resultpeople[i].name })
        }
      }
    }
    console.log("TEST: ", this.selectdatapeople);
  }
}
