import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { FormControl, Validators, FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { IOption } from 'ng-select';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { SubjectService } from 'src/app/services/subject.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { async } from '@angular/core/testing';
import { ElectronicbookService } from 'src/app/services/electronicbook.service';
import { DepartmentService } from 'src/app/services/department.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { NotificationService } from 'src/app/services/notification.service';

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
  Form2: FormGroup;
  Form3: FormGroup;
  Form: FormGroup;
  EditForm: FormGroup;
  EditForm2: FormGroup;
  EditForm3: FormGroup;
  EditForm4: FormGroup;
  AddForm: FormGroup;
  selectpeople: Array<IOption>
  selectministrypeople: Array<IOption>
  modalRef: BsModalRef;
  editid: any
  subquestionclosename: any
  subquestionclosechoicename: any
  subject: any;
  subjectquestionopen: any;
  downloadUrl: any
  urllink
  loading = false;
  electronicbookid: any
  selectdataministrypeople: Array<IOption>
  ministryPeople: any = [];
  selectdatapeople: Array<IOption>
  userPeople: any = [];
  fileStatus = false;
  form: FormGroup;
  carlendarFile: any = [];
  provincename
  provinceid
  resultdate: any = []
  department: any = []
  peopleanswer: any = []
  subjectid
  delid
  userid
  role_id
  temp = []
  resultdsubjectid: any = []
  editAnswerForm: FormGroup;
  answer: any;

  constructor(private fb: FormBuilder,
    private modalService: BsModalService,
    private centralpolicyservice: CentralpolicyService,
    private userservice: UserService,
    private subjectservice: SubjectService,
    private activatedRoute: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private electronicBookService: ElectronicbookService,
    private departmentService: DepartmentService,
    private notificationService: NotificationService,
    private authorize: AuthorizeService,
    private userService: UserService,
    @Inject('BASE_URL') baseUrl: string) {
    this.id = activatedRoute.snapshot.paramMap.get('result')
    this.downloadUrl = baseUrl + '/Uploads';
    this.urllink = baseUrl + 'answersubject/outsider/';
  }

  async ngOnInit() {

    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        // this.role_id = result.role_id
        // console.log(result);
        // alert(this.role_id)

        this.userService.getuserfirstdata(this.userid)
          .subscribe(result => {
            // this.resultuser = result;
            //console.log("test" , this.resultuser);
            this.role_id = result[0].role_id
            // alert(this.role_id)
          })
      })

    console.log("ID: ", this.id);

    this.spinner.show();
    this.Form = this.fb.group({
      UserPeopleId: new FormControl(null, [Validators.required]),
    })

    this.Form2 = this.fb.group({
      DepartmentId: new FormControl(null, [Validators.required]),
      // UserMinistryId: new FormControl(null, [Validators.required]),
    })
    this.Form3 = this.fb.group({
      peopleanswer: new FormControl(null, [Validators.required]),
      // UserMinistryId: new FormControl(null, [Validators.required]),
    })
    this.form = this.fb.group({
      files: [null],
      step: new FormControl(null, [Validators.required]),
      status: new FormControl(null, [Validators.required]),
      questionPeople: new FormControl(null, [Validators.required]),
    })

    this.EditForm = this.fb.group({
      subquestionclose: new FormControl(),
    })

    this.EditForm2 = this.fb.group({
      subquestionclosechoice: new FormControl(),
    })

    this.EditForm3 = this.fb.group({
      subject: new FormControl(),
    })

    this.EditForm4 = this.fb.group({
      subjectquestionopen: new FormControl(),
    })

    this.AddForm = this.fb.group({
      name: new FormControl(null, [Validators.required]),
      // centralpolicydateid: new FormControl(null, [Validators.required]),
      status: new FormControl("ใช้งานจริง่", [Validators.required]),
      inputsubjectdepartment: this.fb.array([
        this.initdepartment()
      ]),
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
    // await this.getDepartment()

    setTimeout(() => {
      this.spinner.hide();
    }, 800);
  }
  initdepartment() {
    return this.fb.group({
      departmentId: [null, [Validators.required, Validators.pattern('[0-9]{3}')]],
      inputquestionopen: this.fb.array([
        this.initquestionopen()
      ]),
      inputquestionclose: this.fb.array([
        this.initquestionclose()
      ])
    })
  }
  initquestionopen() {
    return this.fb.group({
      questionopen: [null, [Validators.required, Validators.pattern('[0-9]{3}')]]

    })
  }
  initquestionclose() {
    return this.fb.group({
      questionclose: [null, [Validators.required, Validators.pattern('[0-9]{3}')]],
      inputanswerclose: this.fb.array([
        this.initanswerclose()
      ])
    });
  }
  initanswerclose() {
    return this.fb.group({
      answerclose: [null, [Validators.required, Validators.pattern('[0-9]{3}')]],
    })
  }
  async openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
    await this.getMinistryPeople();
    await this.getUserPeople();
    this.getDepartmentdata();
  }
  openModal2(template: TemplateRef<any>, subjectid) {
    this.subjectid = subjectid
    this.departmentService.getalldepartdata().subscribe(res => {
      this.department = res.map((item, index) => {
        return {
          value: item.id,
          label: item.name
        }
      })
      this.modalRef = this.modalService.show(template);
    })
  }

  openModal3(template: TemplateRef<any>, subjectid) {
    this.subjectid = subjectid

    this.userservice.getuserdata(7).subscribe(result => {
      // alert(JSON.stringify(result))
      this.peopleanswer = result.map((item, index) => {
        return {
          value: item.id,
          label: item.name
        }
      })
      this.modalRef = this.modalService.show(template);
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

  editModal3(template: TemplateRef<any>, id, name) {
    this.editid = id;
    this.subject = name;

    this.modalRef = this.modalService.show(template);
    this.EditForm3 = this.fb.group({
      subject: new FormControl(),

    })
    this.EditForm3.patchValue({
      subject: name,
    })
  }

  editModal4(template: TemplateRef<any>, id, name) {
    this.editid = id;
    this.subjectquestionopen = name;

    this.modalRef = this.modalService.show(template);
    this.EditForm4 = this.fb.group({
      subjectquestionopen: new FormControl(),

    })
    this.EditForm4.patchValue({
      subjectquestionopen: name,
    })
  }

  editModal5(template: TemplateRef<any>, id, name) {
    this.editid = id;
    this.answer = name;

    this.modalRef = this.modalService.show(template);
    this.editAnswerForm = this.fb.group({
      answer: new FormControl(),
      answerId: new FormControl(),

    })
    this.editAnswerForm.patchValue({
      answer: name,
      answerId: id
    })
  }

  DelModal(template: TemplateRef<any>, id) {
    this.delid = id;
    this.modalRef = this.modalService.show(template);
  }

  DelModal2(template: TemplateRef<any>, id) {
    this.delid = id;
    this.modalRef = this.modalService.show(template);
  }

  delsubjectModal(template: TemplateRef<any>, id) {
    this.delid = id;
    this.modalRef = this.modalService.show(template);
  }

  delquestionModal(template: TemplateRef<any>, id) {
    this.delid = id;
    this.modalRef = this.modalService.show(template);
  }
  deloptionModal(template: TemplateRef<any>, id) {
    this.delid = id;
    this.modalRef = this.modalService.show(template);
  }
  // getDetailCentralPolicy() {
  //   this.centralpolicyservice.getdetailcentralpolicydata(this.id)
  //     .subscribe(result => {
  //       // this.resultdetailcentralpolicy = result
  //     })
  // }

  getDetailCentralPolicyProvince() {
    this.centralpolicyservice.getdetailcentralpolicyprovincedata(this.id)
      .subscribe(result => {
        console.log("123", result);
        // alert(JSON.stringify(result))
        this.resultdetailcentralpolicy = result.centralpolicydata
        this.resultdetailcentralpolicyprovince = result.subjectcentralpolicyprovincedata

        if (this.role_id == 3) {
          if (this.resultdetailcentralpolicyprovince[0].centralPolicyProvince.step == 'มอบหมายเขต') {
            this.resultdetailcentralpolicyprovince[0].centralPolicyProvince.step = "มอบหมายจังหวัด"
          }
          this.form.patchValue({
            step: this.resultdetailcentralpolicyprovince[0].centralPolicyProvince.step
          })
        } else if (this.role_id == 5) {
          if (this.resultdetailcentralpolicyprovince[0].centralPolicyProvince.step == 'มอบหมายเขต') {
            this.resultdetailcentralpolicyprovince[0].centralPolicyProvince.step = "มอบหมายเขต"
          }
          this.form.patchValue({
            step: this.resultdetailcentralpolicyprovince[0].centralPolicyProvince.step
          })
        }

        this.form.patchValue({
          questionPeople: this.resultdetailcentralpolicyprovince[0].centralPolicyProvince.questionPeople,
          status: this.resultdetailcentralpolicyprovince[0].centralPolicyProvince.status
        })

        this.resultuser = result.userdata
        this.electronicbookid = result.centralPolicyEventdata.electronicBookId

        this.resultdate = result.centralPolicyEventdata.inspectionPlanEvent
        this.provincename = result.provincedata.name
        this.provinceid = result.provincedata.id

        this.getCalendarFile();
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
    // alert(JSON.stringify(value))
    this.electronicBookService.addElectronicBookFileFromCalendar(value, this.form.value.files, this.electronicbookid, this.id).subscribe(response => {
      console.log(value);
      this.Form.reset()
      // this.router.navigate(['inspectionplanevent'])
      // console.log("get");
      this.notificationService.addNotification(this.resultdetailcentralpolicy.id, this.provinceid, this.userid, 4, 1)
        .subscribe(response => {
          console.log(response);
        })

      window.history.back();
    })
  }

  storePeople(value: any) {
    let UserPeopleId: any[] = value.UserPeopleId
    // alert(JSON.stringify(value))
    this.centralpolicyservice.addCentralpolicyUser(value, this.id, this.electronicbookid).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()

      for (let i = 0; i < UserPeopleId.length; i++) {
        this.notificationService.addNotification(this.resultdetailcentralpolicy.id, this.provinceid, UserPeopleId[i], 1, 1)
          .subscribe(response => {
            console.log(response);

          })
      }

      this.getCentralPolicyProvinceUser();
    })
  }


  storeDepartment(value) {
    // alert(this.subjectid)
    this.centralpolicyservice.addDepartment(value, this.subjectid).subscribe(response => {
      console.log(value);
      this.Form2.reset()
      this.modalRef.hide()
      this.getDetailCentralPolicyProvince();
    })
  }

  storepeopleanswer(value) {
    let UserPeopleanswerId: any[] = value.peopleanswer
    // alert(this.subjectid)
    this.centralpolicyservice.addPeopleAnswer(value, this.subjectid).subscribe(response => {
      console.log(value);
      this.Form3.reset()
      this.modalRef.hide()


      for (let i = 0; i < UserPeopleanswerId.length; i++) {
        this.notificationService.addNotification(this.resultdetailcentralpolicy.id, this.provinceid, UserPeopleanswerId[i], 5, 1)
          .subscribe(response => {
            console.log(response);

          })
      }

      this.getDetailCentralPolicyProvince();
    })
  }

  storeMinistryPeople(value: any) {
    let UserPeopleId: any[] = value.UserPeopleId
    this.centralpolicyservice.addCentralpolicyUser(value, this.id, this.electronicbookid).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      for (let i = 0; i < UserPeopleId.length; i++) {
        this.notificationService.addNotification(this.resultdetailcentralpolicy.id, this.provinceid, UserPeopleId[i], 1, 1)
          .subscribe(response => {
            console.log(response);
          })
      }
      this.getCentralPolicyProvinceUser();
    })
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

  editsubject(value, id) {
    this.subjectservice.editsubjectchoiceprovince(value, id).subscribe(response => {
      console.log(value);
      this.EditForm3.reset()
      this.modalRef.hide()
      this.getDetailCentralPolicyProvince();
    })
  }

  editsubjectquestionopen(value, id) {
    this.subjectservice.editsubjectquestionopenchoiceprovince(value, id).subscribe(response => {
      console.log(value);
      this.EditForm4.reset()
      this.modalRef.hide()
      this.getDetailCentralPolicyProvince();
    })
  }

  editAnswer(value, id) {
    this.subjectservice.editAnswer(value, id).subscribe(response => {
      console.log(value);
      this.editAnswerForm.reset()
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
      // alert(JSON.stringify(this.resultpeople))
      console.log("tttt:", this.resultpeople);

    })
    await this.centralpolicyservice.getcentralpolicyprovinceuserdata(this.id).subscribe(async result => {
      await result.forEach(async element => {
        if (element.user.role_id == 7) {
          this.allUserPeople.push(element.user)
        }
      }); // Selected
      console.log("selectedUser: ", this.allUserPeople);
      this.getRecycledUserPeople();
    })
  }

  async getRecycledUserPeople() {
    var test: any = [];
    test = this.resultpeople;
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

  uploadFile(event) {
    this.fileStatus = true;
    const file = (event.target as HTMLInputElement).files;

    this.form.patchValue({
      files: file
    });
    console.log("fff:", this.form.value.files)
    this.form.get('files').updateValueAndValidity()
  }

  getCalendarFile() {
    this.electronicBookService.getCalendarFile(this.electronicbookid).subscribe(res => {
      this.carlendarFile = res;
      console.log("calendarFile: ", res);

    })
  }

  deleteProvinceial(value) {
    this.subjectservice.deleteProvincial(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.loading = false

      this.getDetailCentralPolicyProvince();

    })
  }

  deletepeopleanswer(value) {
    this.subjectservice.deletePeopleanswer(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.loading = false

      this.getDetailCentralPolicyProvince();

    })
  }
  addV() {
    const control = <FormArray>this.AddForm.controls['inputsubjectdepartment'];
    control.push(this.initdepartment());
  }
  addW(iv) {
    const control = (<FormArray>this.AddForm.controls['inputsubjectdepartment']).at(iv).get('inputquestionopen') as FormArray;
    control.push(this.initquestionopen());
  }
  addX(iv) {
    const control = (<FormArray>this.AddForm.controls['inputsubjectdepartment']).at(iv).get('inputquestionclose') as FormArray;
    control.push(this.initquestionclose());
  }
  addY(iv, ix) {
    const control = ((<FormArray>this.AddForm.controls['inputsubjectdepartment']).at(iv).get('inputquestionclose') as FormArray).at(ix).get('inputanswerclose') as FormArray;
    control.push(this.initanswerclose());
  }
  // remove(index: number) {
  //   this.d.removeAt(index);
  // }
  removeV(index: number) {
    const control = <FormArray>this.AddForm.controls['inputsubjectdepartment'];
    control.removeAt(index);
  }
  removeW(iv: number, iw: number) {
    const control = (<FormArray>this.AddForm.controls['inputsubjectdepartment']).at(iv).get('inputquestionopen') as FormArray;
    control.removeAt(iw);
  }
  removeX(iv: number, ix: number) {
    const control = (<FormArray>this.AddForm.controls['inputsubjectdepartment']).at(iv).get('inputquestionclose') as FormArray;
    control.removeAt(ix);
  }
  removeY(iv: number, ix: number, iy: number) {
    const control = ((<FormArray>this.AddForm.controls['inputsubjectdepartment']).at(iv).get('inputquestionclose') as FormArray).at(ix).get('inputanswerclose') as FormArray;
    control.removeAt(iy);
  }
  back() {
    window.history.back();
  }

  deletesubject(value) {
    this.subjectservice.deletesubjectrole3(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.loading = false
      this.getDetailCentralPolicyProvince();
    })
  }
  deletequestion(value) {
    this.subjectservice.deletequestionrole3(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.loading = false
      this.getDetailCentralPolicyProvince();
    })
  }
  deleteoption(value) {
    this.subjectservice.deleteoptionrole3(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.loading = false
      this.getDetailCentralPolicyProvince();
    })
  }

  getDepartmentdata() {
    // this.resultprovince.forEach((element, index) => {
    //   console.log('element', element);

    this.departmentService.getalldepartdata()
      .subscribe(result => {
        this.temp = result.map((item, index) => {
          return {
            value: item.id,
            label: item.name,
          }
        })
        console.log(result);
      })
    // });
  }

  storeSubject(value) {
    this.spinner.show();
    console.log(value);
    this.subjectservice.addSubjectRole3(value, this.id).subscribe(response => {
      console.log("Response : ", response);
      this.resultdsubjectid.push(response.getSubjectID)
      response.termsList.forEach(element => {
        this.resultdsubjectid.push(element)
      });
      console.log("Response2 : ", this.resultdsubjectid);
      // this.storefiles();
      this.AddForm.reset();
      this.modalRef.hide();
      this.spinner.hide();
      this.getDetailCentralPolicyProvince()
    })
  }

}
