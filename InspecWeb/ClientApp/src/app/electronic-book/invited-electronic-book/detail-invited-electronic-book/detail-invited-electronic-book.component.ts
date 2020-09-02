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
  selector: 'app-detail-invited-electronic-book',
  templateUrl: './detail-invited-electronic-book.component.html',
  styleUrls: ['./detail-invited-electronic-book.component.css']
})
export class DetailInvitedElectronicBookComponent implements OnInit {

  electId: any;
  Form: FormGroup;
  userid: any;
  role_id: any;
  electronicBookData: any = [];
  modalRef: BsModalRef;
  suggestionForm: FormGroup;
  inspectionPlanEventId: any = [];
  invitedPeopleData: any = [];
  approveForm: any = [];
  ebookInviteId: any;
  opinionData: any = [];
  downloadUrl: any;
  signature: any;

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
    this.electId = activatedRoute.snapshot.paramMap.get('id');
    this.ebookInviteId = activatedRoute.snapshot.paramMap.get('ebookInviteId');
    // this.centralPolicyUserId = activatedRoute.snapshot.paramMap.get('centralPolicyUserId')
    this.downloadUrl = baseUrl + '/Uploads';
  }

  ngOnInit() {
    console.log("ELECTID: ", this.electId);
    // alert(this.ebookInviteId)
    this.spinner.show();
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub

        this.signature = result;
        console.log("userData: ", this.signature);
        // alert(this.userid)
        this.userservice.getuserfirstdata(this.userid)
          .subscribe(result => {
            this.role_id = result[0].role_id
            // alert(this.role_id)
          })
      })
    this.Form = this.fb.group({
      detail: new FormControl(null, [Validators.required]),
      problem: new FormControl(null, [Validators.required]),
      suggestion: new FormControl(null, [Validators.required]),
      Status: new FormControl("ร่างกำหนดการ", [Validators.required]),
      user: new FormControl("ร่างกำหนดการ", [Validators.required]),
    })

    this.approveForm = this.fb.group({
      opinion: new FormControl(null, [Validators.required]),
      accept: new FormControl("เห็นด้วย", [Validators.required]),
    })

    this.suggestionForm = this.fb.group({
      suggestion: new FormControl(null, [Validators.required]),
    })

    this.getElectronicBookDetail();
    this.getElectronicBookInviteOpinion();
    setTimeout(() => {
      this.spinner.hide();
    }, 300);
  }

  getElectronicBookDetail() {
    this.electronicBookService.getElectronicBookDetail(this.electId).subscribe(result => {
      console.log("RES: ", result);
      this.inspectionPlanEventId = result.electronicBookGroup.map((item, index) => {
        return {
          inspectionPlanEventId: item.centralPolicyEvent.inspectionPlanEventId
        }
      })

      this.getInvitedPeople(this.inspectionPlanEventId);

      this.electronicBookData = result;
      this.Form.patchValue({
        detail: result.electronicBook.detail,
        problem: result.electronicBook.problem,
        suggestion: result.electronicBook.suggestion
      })
    })
  }

  getInvitedPeople(inspectionPlanEventId) {
    this.electronicBookService.getInvitedPeople(inspectionPlanEventId).subscribe(res => {

      console.log("res people: ", res);
      var acceptPeople: any = [];
      var acceptPeople = res.filter(function (data) {
        return data.status == "ตอบรับ" || data.status == "มอบหมาย";
      });
      console.log('acceptPeople: ', acceptPeople);

      this.invitedPeopleData = acceptPeople.filter(
        (thing, i, arr) => arr.findIndex(t => t.userId === thing.userId) === i
      );
      console.log("invitedPeople: ", this.invitedPeopleData);

      var userInvite = this.invitedPeopleData.map((item, index) => {
        return item.userId;
      })

      console.log("userrrrr: ", userInvite);
      this.Form.patchValue({
        user: userInvite
      })
    })
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  openAlertModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  addOpinion(value) {
    this.electronicBookService.addOpinion(value, this.ebookInviteId).subscribe(res => {
      console.log('Opinion:', res);
      this.approveForm.reset();
      this.getElectronicBookDetail();
      this.getElectronicBookInviteOpinion();
      this.modalRef.hide();
      this._NotofyService.onSuccess("ลงนามสมุดตรวจอิเล็กทรอนิกส์",)
    })
  }

  closeModal() {
    this.approveForm.reset();
    this.getElectronicBookDetail();
    this.getElectronicBookInviteOpinion();
    this.modalRef.hide();
  }

  goBack() {
    window.history.back();
  }

  getElectronicBookInviteOpinion() {
    this.electronicBookService.getElectronicBookInviteOpinion(this.electId, this.userid).subscribe(res => {
      console.log("Opinion: ", res);
      this.opinionData = res;

      this.approveForm.patchValue({
        opinion: res.description
      })
    })
  }

}
