import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { UserService } from 'src/app/services/user.service';
import { SubjectService } from 'src/app/services/subject.service';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ElectronicbookService } from 'src/app/services/electronicbook.service';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DepartmentService } from 'src/app/services/department.service';
import { NotofyService } from 'src/app/services/notofy.service';

@Component({
  selector: 'app-electronic-book-province-detail',
  templateUrl: './electronic-book-province-detail.component.html',
  styleUrls: ['./electronic-book-province-detail.component.css']
})
export class ElectronicBookProvinceDetailComponent implements OnInit {
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
  signature: any;
  provincialDepartmentData: any = [];
  otherDepartmentForm: FormGroup;
  electAcceptId: any;
  allDepartmentInvite: any = [];
  acceptDepartmentInvite: any = [];
  userProvinceId: any;
  checkProvince = false;
  submitted = false;
  signUrl: any;

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
    this.downloadUrl = baseUrl + 'Uploads';
    this.signUrl = baseUrl + 'Signature'
  }

  ngOnInit() {
    // console.log("ELECTID: ", this.electId);
    this.spinner.show();
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        // console.log("user naja: ", result);
        // alert(this.userid)
        // this.signature = result;
        // console.log("signatureProvince: ", this.signature);

        this.userservice.getuserfirstdata(this.userid)
          .subscribe(result => {
            console.log("user naja: ", result);
            this.role_id = result[0].role_id
            this.signature = result[0].signature
            this.userProvinceId = result[0].userProvince[0].provinceId
            // alert(this.role_id)
          })
      })
    this.Form = this.fb.group({
      detail: new FormControl(null, [Validators.required]),
      problem: new FormControl(null, [Validators.required]),
      suggestion: new FormControl(null, [Validators.required]),
    });

    this.submitForm = this.fb.group({
      description: new FormControl(null, [Validators.required]),
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
      this.electAcceptId = result.electronicBookAccept.id;
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
      console.log("electronicBookData => ", this.electronicBookData);
      this.allDepartmentInvite = result.electronicBookDepartment;

      this.electronicBookData.electronicBookAccept.forEach(element => {
        if (element.provinceId == this.userProvinceId && element.electronicBookId == this.electId) {
          if (element.status == "ลงนามเอกสารแล้ว") {
            this.checkProvince = true;
            console.log("checkProvince: ", this.checkProvince);

          }
        }
      });

      this.acceptDepartmentInvite = this.allDepartmentInvite.filter(function (data) {
        return data.status == "ดำเนินการแล้ว";
      });
      console.log("allDepartment: ", this.allDepartmentInvite.length);

      console.log('acceptDepartment: ', this.acceptDepartmentInvite.length);



      // console.log("provinceDataXXXXX: ", this.electronicBookData);

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
      var acceptPeople = res.filter((data) => data.status == "ตอบรับ" || (data.status == "มอบหมาย" && data.forwardExternal == 1));
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
      this.electronicBookService.provinceAddSignature(value, this.form.value.files, this.electId, this.userid, this.userProvinceId).subscribe(res => {
        // console.log("signatureRES: ", res);
        if (this.electronicBookData.electronicBookGroup.length == 0) {
          this.notificationService.addNotification(1, 1, this.userid, 8, this.electId, null,this.userid)
            .subscribe(response => {
              console.log("Noti res: ", response);
            })
          this.getElectronicBookDetail();
          this.Form.reset();
          this.modalRef.hide();
          this._NotofyService.onSuccess("รับทราบรายการสมุดตรวจ",)
        } else {
          this.notificationService.addNotification(this.electronicBookData.electronicBookGroup[0].centralPolicyId, 1, this.userid, 8, this.electId, null,this.userid)
            .subscribe(response => {
              console.log("Noti res: ", response);
            })
          this.getElectronicBookDetail();
          this.Form.reset();
          this.modalRef.hide();
          this._NotofyService.onSuccess("รับทราบรายการสมุดตรวจ",)
        }
      })
    }
  }

  // sendToOtherProvince(value) {
  //   console.log("Value: ", value);

  //   this.electronicBookService.sendToOtherProvince(value, this.userid, this.electAcceptId).subscribe(res => {
  //     console.log("sended: ", res);
  //     this.getElectronicBookDetail();
  //     this.modalRef.hide();
  //   })
  // }

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

}
