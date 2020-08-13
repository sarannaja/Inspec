import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { UserService } from 'src/app/services/user.service';
import { SubjectService } from 'src/app/services/subject.service';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from "ngx-spinner";
import { ElectronicbookService } from 'src/app/services/electronicbook.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DepartmentService } from 'src/app/services/department.service';
import * as _ from 'lodash';
import { ElectronicbookreportService } from 'src/app/services/electronicbookreport.service';

@Component({
  selector: 'app-detail-electronic-book',
  templateUrl: './detail-electronic-book.component.html',
  styleUrls: ['./detail-electronic-book.component.css']
})
export class DetailElectronicBookComponent implements OnInit {

  electId: any;
  Form: FormGroup;
  Form2: FormGroup;
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
  provincialDepartmentId: any = [];
  resultministrypeople: any = [];
  selectdataministrypeople: any = [];
  resultpeople: any = [];
  selectdatapeople: any = [];
  resultdepartmentpeople: any = [];
  selectdatadepartmentpeople: any = [];
  peopleuserdata: any = [];
  PeopleSelect: any = [];
  MinistrySelect: any = [];
  DepartmentSelect: any = [];
  inviteId: any;
  url: any;


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
    private electronicBookReportService: ElectronicbookreportService,
    @Inject('BASE_URL') baseUrl: string) {
    this.electId = activatedRoute.snapshot.paramMap.get('id')
    // this.centralPolicyUserId = activatedRoute.snapshot.paramMap.get('centralPolicyUserId')
    this.downloadUrl = baseUrl + '/Uploads';
    // this.getElectronicBookDetail();
    this.url = baseUrl;
  }

  async ngOnInit() {
    this.spinner.show();
    // console.log("ELECTID: ", this.electId);
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
      provincialDepartment: new FormControl(null, [Validators.required]),
    })

    this.Form2 = this.fb.group({
      UserPeopleId: new FormControl(null, [Validators.required]),
      UserMinistryId: new FormControl(null, [Validators.required]),
      UserDepartmentId: new FormControl(null, [Validators.required]),
    })

    // this.provinceForm = this.fb.group({
    //   provinceDetail: new FormControl(null, [Validators.required]),
    // })

    this.suggestionForm = this.fb.group({
      suggestion: new FormControl(null, [Validators.required]),
    })

    await this.getElectronicBookDetail();
  }

  async getElectronicBookDetail() {
    await this.electronicBookService.getElectronicBookDetail(this.electId).subscribe(async result => {
      console.log("RES: ", result);
      this.electronicBookData = result;
      this.peopleuserdata = result.ebookInvite;

      var provincialDepartments: any = [];
      await result.electronicBookGroup.forEach(async element => {
        await element.centralPolicyEvent.centralPolicy.centralPolicyProvinces.forEach(async element2 => {
          console.log("LOOP: ", element2.id);
          await element2.subjectCentralPolicyProvinces.forEach(async element3 => {
            await element3.subquestionCentralPolicyProvinces.forEach(async element4 => {
              await element4.subjectCentralPolicyProvinceGroups.forEach(async element5 => {
                await provincialDepartments.push(element5.provincialDepartmentId)
              });
            });
          });
        });
      });

      console.log("provincialDepartment: ", provincialDepartments);
      this.provincialDepartmentId = provincialDepartments.filter(
        (thing, i, arr) => arr.findIndex(t => t === thing) === i
      );
      console.log("uniqueProvincialDepartment: ", this.provincialDepartmentId);

      var provinces: any = [];
      await result.electronicBookGroup.forEach(element => {
        provinces.push(element.centralPolicyEvent.inspectionPlanEvent.provinceId)
      });
      // console.log("allProvices: ", provinces);

      this.provinceId = provinces.filter(
        (thing, i, arr) => arr.findIndex(t => t === thing) === i
      );
      // console.log("uniqueProvince: ", this.provinceId);

      this.inspectionPlanEventId = result.electronicBookGroup.map((item, index) => {
        return {
          inspectionPlanEventId: item.centralPolicyEvent.inspectionPlanEventId
        }
      })

      await this.getInvitedPeople(this.inspectionPlanEventId);
      // console.log("eBookDetail: ", this.electronicBookData);

      // this.provinceForm.patchValue({
      //   provinceDetail: this.electronicBookData.electronicBookAccept.description
      // })

      await this.electronicBookData.ebookInvite.forEach(async element => {
        if (element.user.role_id == 6 || element.user.role_id == 10) {
          await this.allMinistry.push(element)
        }
      });
      console.log("All: ", this.allMinistry.length);

      await this.electronicBookData.ebookInvite.forEach(async element => {
        if (element.user.role_id == 6 || element.user.role_id == 10) {
          if (element.status == "ลงความเห็นแล้ว") {
            await this.approveMinistry.push(element)
          }
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

  async getInvitedPeople(inspectionPlanEventId) {
    await this.electronicBookService.getInvitedPeople(inspectionPlanEventId).subscribe(async res => {

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

      await this.getDepartmentPeople();
      await this.getMinistryPeople();
      await this.getUserPeople();
    })
    this.spinner.hide();
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
    // if (this.electronicBookData.ebookInvite.length == 0 && value.status == 'ใช้งานจริง') {
    //   alert("ไม่สามารถใช้งานจริงได้เนื่องจาก ไม่มีข้อมูลผู้ตรวจราชการ")
    // } else {
    this.electronicBookService.editSuggestion(value, this.electId).subscribe(res => {
      console.log("Edit Suggestion: ", res);
      this.getElectronicBookDetail();
      this.modalRef.hide();
    })
    // }
  }

  closeModal() {
    this.suggestionForm.reset();
    this.modalRef.hide();
  }

  goBack() {
    window.history.back();
  }

  sendToProvince(electId) {
    this.electronicBookService.sendToProvince(electId, this.userid, this.provinceId, this.provincialDepartmentId).subscribe(res => {
      console.log("sended: ", res);
      this.getElectronicBookDetail();
      this.modalRef.hide();
    })
  }

  async getMinistryPeople() {
    this.selectdataministrypeople = [];
    await this.userservice.getuserdata(6).subscribe(async result => {
      this.resultministrypeople = result // All
      console.log("Ministry: ", this.resultministrypeople);
      for (var i = 0; i < this.resultministrypeople.length; i++) {
        await this.selectdataministrypeople.push({ value: this.resultministrypeople[i].id, label: this.resultministrypeople[i].ministries.name + " - " + this.resultministrypeople[i].name })
      }

      var data: any[] = this.invitedPeopleData.map(result => {
        return result.user.id
      })
      var data2: any[] = this.peopleuserdata.map(result => {
        return result.user.id
      })
      this.MinistrySelect = _.filter(this.selectdataministrypeople, (v) => !_.includes(
        data, v.value
      ))
      this.MinistrySelect = _.filter(this.selectdataministrypeople, (v) => !_.includes(
        data2, v.value
      ))
    })
  }
  async getDepartmentPeople() {
    this.selectdatadepartmentpeople = [];
    await this.userservice.getuserdata(10).subscribe(async result => {
      this.resultdepartmentpeople = result // All
      for (var i = 0; i < this.resultdepartmentpeople.length; i++) {
        await this.selectdatadepartmentpeople.push({ value: this.resultdepartmentpeople[i].id, label: this.resultdepartmentpeople[i].ministries.name + " - " + this.resultdepartmentpeople[i].name })
      }

      var data: any[] = this.invitedPeopleData.map(result => {
        return result.user.id
      })
      var data2: any[] = this.peopleuserdata.map(result => {
        return result.user.id
      })
      this.DepartmentSelect = _.filter(this.selectdatadepartmentpeople, (v) => !_.includes(
        data, v.value
      ))
      this.DepartmentSelect = _.filter(this.selectdatadepartmentpeople, (v) => !_.includes(
        data2, v.value
      ))
    })
  }
  async getUserPeople() {
    this.selectdatapeople = []
    await this.userservice.getuserdata(7).subscribe(async result => {
      this.resultpeople = result
      console.log("tttt:", this.peopleuserdata);
      for (var i = 0; i < this.resultpeople.length; i++) {
        await this.selectdatapeople.push({ value: this.resultpeople[i].id, label: "ด้าน" + this.resultpeople[i].side + " - " + this.resultpeople[i].name })
      }

      var data: any[] = this.invitedPeopleData.map(result => {
        return result.user.id
      })
      var data2: any[] = this.peopleuserdata.map(result => {
        return result.user.id
      })
      this.PeopleSelect = _.filter(this.selectdatapeople, (v) => !_.includes(
        data, v.value
      ))
      this.PeopleSelect = _.filter(this.selectdatapeople, (v) => !_.includes(
        data2, v.value
      ))

    })
  }

  async openInviteModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  storeMorePeople(value) {
    console.log("MOREEEE: ", value);
    console.log("Elect JA: ", this.electId);

    this.electronicBookService.inviteMorePeople(value, this.electId).subscribe(res => {
      this.getElectronicBookDetail();
      this.Form2.reset();
      this.modalRef.hide();
    })
  }

  openDeleteModal(template: TemplateRef<any>, inviteId) {
    console.log("INVITED ID: ", inviteId);
    this.modalRef = this.modalService.show(template);
    this.inviteId = inviteId;
  }

  deleteMoreInvited() {
    this.electronicBookService.deleteMoreInvitedPeople(this.inviteId).subscribe(res => {
      this.getElectronicBookDetail();
      this.modalRef.hide();
    })
  }

  printReport() {
    this.electronicBookReportService.printReport(this.electId, this.electronicBookData).subscribe(res => {
      window.open(this.url + "Uploads/" + res.data);
      console.log(res);
    })
  }
}
