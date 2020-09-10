import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { UserService } from 'src/app/services/user.service';
import { SubjectService } from 'src/app/services/subject.service';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ElectronicbookService } from 'src/app/services/electronicbook.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DepartmentService } from 'src/app/services/department.service';
import { NotofyService } from 'src/app/services/notofy.service';

@Component({
  selector: 'app-electronic-book-department-detail',
  templateUrl: './electronic-book-department-detail.component.html',
  styleUrls: ['./electronic-book-department-detail.component.css']
})
export class ElectronicBookDepartmentDetailComponent implements OnInit {
  electId: any;
  Form: FormGroup;
  userid: any;
  role_id: any;
  electronicBookData: any = [];
  modalRef: BsModalRef;
  centralPolicyEventId: any;
  suggestionForm: FormGroup;
  inspectionPlanEventId: any = [];
  invitedPeopleData: any = [];
  allMinistry: any = [];
  approveMinistry: any = [];
  provinceId: any = [];
  form: FormGroup;
  submitForm: FormGroup;
  downloadUrl: any;
  signature: any = [];
  provincialDepartmentData: any = [];
  otherDepartmentForm: FormGroup;
  electAcceptId: any;
  ebookProvincialDepartment: any = [];
  provincialID: any;
  electronicBook
  electProvincialID: any;
  userProvincialDepartmentId: any;
  checkProvincialDepartment = false;
  submitted = false;
  submitted2 = false;

  constructor(
    private fb: FormBuilder,
    private modalService: BsModalService,
    private centralpolicyservice: CentralpolicyService,
    private userservice: UserService,
    private subjectservice: SubjectService,
    private activatedRoute: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private electronicBookService: ElectronicbookService,
    private authorize: AuthorizeService,
    private notificationService: NotificationService,
    private departmentService: DepartmentService,
    private _NotofyService: NotofyService,
    @Inject('BASE_URL') baseUrl: string) {
    this.electId = activatedRoute.snapshot.paramMap.get('id')
    // this.centralPolicyUserId = activatedRoute.snapshot.paramMap.get('centralPolicyUserId')
    this.downloadUrl = baseUrl + '/Uploads';
  }

  ngOnInit() {
    // console.log("ELECTID: ", this.electId);
    this.spinner.show();
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log("UserData: ", result);
        // alert(this.userid)
        // this.signature = result;
        // this.provincialID = result;
        // console.log("signatureProvince: ", this.signature);
      })

    this.userservice.getuserfirstdata(this.userid)
      .subscribe(result2 => {
        console.log("UserData2: ", result2);
        this.role_id = result2[0].role_id;
        this.signature = result2[0].signature;
        this.provincialID = result2[0].provincialDepartmentId;
        // alert(this.role_id)
        // alert(this.role_id)
        // alert(this.provincialID)
        // alert(this.signature)
        this.getElectronicBookProvincialDepartment();
      })
    this.Form = this.fb.group({
      detail: new FormControl(null, [Validators.required]),
      problem: new FormControl(null, [Validators.required]),
      suggestion: new FormControl(null, [Validators.required]),
    });

    this.submitForm = this.fb.group({
      description: new FormControl("", [Validators.required]),
    });

    this.form = this.fb.group({
      files: [null]
    })

    this.otherDepartmentForm = this.fb.group({
      description: new FormControl(null, [Validators.required]),
      provincialDepartment: new FormControl(null, [Validators.required]),
    })

    this.getElectronicBookDetail();
    this.getProvincialDepartment();

    setTimeout(() => {
      this.spinner.hide();
    }, 300);
  }

  getElectronicBookDetail() {
    this.electronicBookService.getElectronicBookDetail(this.electId).subscribe(result => {
      console.log("RESProvince: ", result);

      // console.log("Filter Provincial: ", result.electronicBook.electronicBookProvincialDepartments.filter(x => x.provincialDepartmentId == this.provincialID));
      // var acceptId = result.electronicBook.electronicBookProvincialDepartments.filter(x => x.provincialDepartmentId == this.provincialID);
      // this.electAcceptId = acceptId;

      var provinces: any = [];

      result.electronicBookGroup.forEach(element => {
        provinces.push(element.provinceId)
      });
      // console.log("allProvices: ", provinces);

      this.provinceId = provinces.filter(
        (thing, i, arr) => arr.findIndex(t => t === thing) === i
      );
      // console.log("uniqueProvince: ", this.provinceId);

      this.inspectionPlanEventId = result.electronicBookGroup.map((item, index) => {
        return {
          inspectionPlanEventId: item.inspectionPlanEventId
        }
      })

      this.getInvitedPeople(this.inspectionPlanEventId);

      this.electronicBookData = result;
      // console.log("provinceDataXXXXX: ", this.electronicBookData);

      this.electronicBookData.electronicBookDepartment.forEach(element => {
        if (element.provincialDepartmentId == this.provincialID && element.electronicBookId == this.electId) {
          if (element.status == "ดำเนินการแล้ว") {
            this.checkProvincialDepartment = true;
            console.log("checkProvince: ", this.checkProvincialDepartment);

          }
        }
      });

      this.electronicBookData.ebookInvite.forEach(element => {
        if (element.user.role_id == 6) {
          this.allMinistry.push(element)
        }
      });
      // console.log("All: ", this.allMinistry.length);

      this.electronicBookData.ebookInvite.forEach(element => {
        if (element.user.role_id == 6 && element.status == "ลงความเห็นแล้ว") {
          this.approveMinistry.push(element)
        }
      });

      // console.log("Approved: ", this.approveMinistry.length);
      this.Form.patchValue({
        detail: result.electronicBook.detail,
        problem: result.electronicBook.problem,
        suggestion: result.electronicBook.suggestion
      })
    })
  }

  getInvitedPeople(inspectionPlanEventId) {
    this.electronicBookService.getInvitedPeople(inspectionPlanEventId).subscribe(res => {

      // console.log("res people: ", res);
      var acceptPeople: any = [];
      var acceptPeople = res.filter(function (data) {
        return data.status == "ตอบรับ" || data.status == "มอบหมาย";
      });
      // console.log('acceptPeople: ', acceptPeople);

      this.invitedPeopleData = acceptPeople.filter(
        (thing, i, arr) => arr.findIndex(t => t.userId === thing.userId) === i
      );
      // console.log("invitedPeople: ", this.invitedPeopleData);

      var userInvite = this.invitedPeopleData.map((item, index) => {
        return item.userId;
      })
      // console.log("userrrrr: ", userInvite);

      this.Form.patchValue({
        user: userInvite
      })
    })
  }

  openModal(template: TemplateRef<any>, centralPolicyEventId) {
    this.modalRef = this.modalService.show(template);
    this.centralPolicyEventId = centralPolicyEventId;
  }

  openAlertModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  closeModal() {
    this.suggestionForm.reset();
    this.modalRef.hide();
  }

  goBack() {
    window.history.back();
  }

  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.form.patchValue({
      files: file
    });
    // console.log("fff:", this.form.value.files);
    this.form.get('files').updateValueAndValidity()
  }

  postSignature(value) {
    this.submitted = true;
    if (this.submitForm.invalid) {
      console.log("in1");
      return;
    } else {
      this.electronicBookService.departmentAddSignature(value, this.form.value.files, this.electId, this.userid, this.electProvincialID).subscribe(res => {
        // console.log("signatureRES: ", res);
        this.getElectronicBookDetail();
        this.getElectronicBookProvincialDepartment();
        this.Form.reset();
        this.submitted = false;
        this.modalRef.hide();
        this._NotofyService.onSuccess("รับทราบรายการสมุดตรวจ",)
      })
    }
  }

  sendToOtherProvince(value) {
    console.log("Value: ", value);
    this.submitted2 = true;
    if (this.otherDepartmentForm.invalid) {
      console.log("in1");
      return;
    } else {
      this.electronicBookService.sendToOtherProvince(value, this.userid, this.electAcceptId).subscribe(res => {
        console.log("sended: ", res);
        this.getElectronicBookDetail();
        this.modalRef.hide();
        this._NotofyService.onSuccess("ส่งต่อสมุดตรวจ",)
      })
    }
  }

  getProvincialDepartment() {
    this.electronicBookService.getProvincialDepartment().subscribe(res => {
      // console.log("ProvincialDepartment: ", res);
      this.provincialDepartmentData = res.provincialDepartmentData.map((item, index) => {
        return {
          value: item.id,
          label: item.name
        }
      })
    })
  }

  getElectronicBookProvincialDepartment() {
    this.electronicBookService.getElectronicBookDepartmentById(this.electId, this.provincialID).subscribe(res => {
      this.ebookProvincialDepartment = res;
      console.log("KKKKKKKKKKK: ", res);
      this.electProvincialID = res.id;
      // alert(this.electProvincialID);
      // this.ebookProvincialDepartment.patchValue({
      //   opinion: res.description
      // })
    })
  }

}
