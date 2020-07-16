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

@Component({
  selector: 'app-detail-electronic-book',
  templateUrl: './detail-electronic-book.component.html',
  styleUrls: ['./detail-electronic-book.component.css']
})
export class DetailElectronicBookComponent implements OnInit {

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
  downloadUrl: any;

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
    @Inject('BASE_URL') baseUrl: string) {
    this.electId = activatedRoute.snapshot.paramMap.get('id')
    // this.centralPolicyUserId = activatedRoute.snapshot.paramMap.get('centralPolicyUserId')
    this.downloadUrl = baseUrl + '/Uploads';
  }

  ngOnInit() {
    console.log("ELECTID: ", this.electId);
    this.spinner.show();
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
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
      provinceDetail: new FormControl(null, [Validators.required]),
    })

    // this.provinceForm = this.fb.group({
    //   provinceDetail: new FormControl(null, [Validators.required]),
    // })

    this.suggestionForm = this.fb.group({
      suggestion: new FormControl(null, [Validators.required]),
    })

    this.getElectronicBookDetail();
    setTimeout(() => {
      this.spinner.hide();
    }, 300);
  }

  getElectronicBookDetail() {
    this.electronicBookService.getElectronicBookDetail(this.electId).subscribe(result => {
      console.log("RES: ", result);

      var provinces: any = [];

      result.electronicBookGroup.forEach(element => {
        provinces.push(element.centralPolicyEvent.inspectionPlanEvent.provinceId)
      });
      console.log("allProvices: ", provinces);

      this.provinceId = provinces.filter(
        (thing, i, arr) => arr.findIndex(t => t === thing) === i
      );
      console.log("uniqueProvince: ", this.provinceId);

      this.inspectionPlanEventId = result.electronicBookGroup.map((item, index) => {
        return {
          inspectionPlanEventId: item.centralPolicyEvent.inspectionPlanEventId
        }
      })

      this.getInvitedPeople(this.inspectionPlanEventId);

      this.electronicBookData = result;
      console.log("eBookDetail: ", this.electronicBookData);

      // this.provinceForm.patchValue({
      //   provinceDetail: this.electronicBookData.electronicBookAccept.description
      // })

      this.electronicBookData.ebookInvite.forEach(element => {
        if (element.user.role_id == 6 || element.user.role_id == 10) {
          this.allMinistry.push(element)
        }
      });
      console.log("All: ", this.allMinistry.length);

      this.electronicBookData.ebookInvite.forEach(element => {
        if (element.user.role_id == 6 || element.user.role_id == 10 && element.status == "ลงความเห็นแล้ว") {
          this.approveMinistry.push(element)
        }
      });

      console.log("Approved: ", this.approveMinistry.length);


      this.Form.patchValue({
        detail: result.electronicBook.detail,
        problem: result.electronicBook.problem,
        suggestion: result.electronicBook.suggestion,
      })

      if (this.electronicBookData.electronicBookAccept != null) {
        this.Form.patchValue({
          provinceDetail: this.electronicBookData.electronicBookAccept.description
        })
      }
     
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

  openModal(template: TemplateRef<any>, centralPolicyEventId) {
    this.modalRef = this.modalService.show(template);
    this.centralPolicyEventId = centralPolicyEventId;
  }

  addSuggestion(value) {
    this.electronicBookService.addSuggestion(value, this.electId, this.centralPolicyEventId).subscribe(res => {
      console.log("Suggestion: ", res);
      this.getElectronicBookDetail();
      this.modalRef.hide();
    })
  }

  editSuggestion(value) {
    if (this.electronicBookData.ebookInvite.length == 0 && value.status == 'ใช้งานจริง') {
      alert("ไม่สามารถใช้งานจริงได้เนื่องจาก ไม่มีข้อมูลผู้ตรวจราชการ")
    } else {
      this.electronicBookService.editSuggestion(value, this.electId).subscribe(res => {
        console.log("Edit Suggestion: ", res);
        this.getElectronicBookDetail();
        this.modalRef.hide();
      })
    }
  }

  closeModal() {
    this.suggestionForm.reset();
    this.modalRef.hide();
  }

  goBack() {
    window.history.back();
  }

  sendToProvince(electId) {
    this.electronicBookService.sendToProvince(electId, this.userid, this.provinceId).subscribe(res => {
      console.log("sended: ", res);
      this.getElectronicBookDetail();
      this.modalRef.hide();
    })
  }
}
