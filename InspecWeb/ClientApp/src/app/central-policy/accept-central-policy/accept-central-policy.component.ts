import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, Validators, FormGroup, FormBuilder } from '@angular/forms';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { UserService } from 'src/app/services/user.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { ElectronicbookService } from 'src/app/services/electronicbook.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-accept-central-policy',
  templateUrl: './accept-central-policy.component.html',
  styleUrls: ['./accept-central-policy.component.css']
})
export class AcceptCentralPolicyComponent implements OnInit {
  resultuser: any = []
  id
  resultdetailcentralpolicy: any = []
  resultcentralpolicyuser: any = []
  resultdetailcentralpolicyprovince: any = []
  Form: FormGroup;
  assignForm: FormGroup;
  userid: string
  answer
  resultelectronicbookdetail: any = []
  resultelectronicbookproblem: any = []
  resultelectronicbooksuggestion: any = []
  downloadUrl: any;
  modalRef: BsModalRef;
  assignDetail: any;
  centralpolicyproviceid
  electronicbookid
  provincename
  provinceid
  resultdate: any = []
  carlendarFile: any = [];
  cenid
  SuggestionForm: FormGroup;
  role_id: any

  constructor(private fb: FormBuilder,
    private modalService: BsModalService,
    private centralpolicyservice: CentralpolicyService,
    private userservice: UserService,
    private router: Router,
    private authorize: AuthorizeService,
    private activatedRoute: ActivatedRoute,
    private electronicBookService: ElectronicbookService,
    private notificationService: NotificationService,
    private userService: UserService,
    @Inject('BASE_URL') baseUrl: string) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.centralpolicyproviceid = activatedRoute.snapshot.paramMap.get('centralpolicyproviceid')
    this.cenid = activatedRoute.snapshot.paramMap.get('cenid')
    this.downloadUrl = baseUrl + '/Uploads';
  }

  ngOnInit() {
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log("UserData: ", result);
        // alert(this.userid)
      })

      this.userService.getuserfirstdata(this.userid)
      .subscribe(result => {
        this.resultuser = result;
        //console.log("test" , this.resultuser);
        this.role_id = result[0].role_id
        console.log("User Role: ", this.role_id);
      });

    this.Form = this.fb.group({
      answer: new FormControl(null, [Validators.required]),
    })
    this.assignForm = this.fb.group({
      assign: new FormControl(null, [Validators.required]),
    })

    this.SuggestionForm = this.fb.group({
      checkDetail: new FormControl(null, [Validators.required]),
      Problem: new FormControl(null, [Validators.required]),
      Suggestion: new FormControl(null, [Validators.required]),
    })

    this.getDetailCentralPolicy()
    this.getCentralPolicyUser()
    this.getDetailCentralPolicyProvince()
    this.getAssign();
  }
  getDetailCentralPolicy() {
    this.centralpolicyservice.getdetailacceptcentralpolicydata(this.id)
      .subscribe(result => {
        console.log("elec Detail: ", result);

        this.resultdetailcentralpolicy = result.centralpolicydata
        this.resultuser = result.userdata
        this.resultelectronicbookdetail = result.centralpolicydata.centralPolicyUser[0].electronicBook.electronicBookSuggestGroups[0].detail
        // alert(JSON.stringify(this.resultelectronicbookdetail))

        this.resultelectronicbookproblem = result.centralpolicydata.centralPolicyUser[0].electronicBook.electronicBookSuggestGroups[0].problem
        this.resultelectronicbooksuggestion = result.centralpolicydata.centralPolicyUser[0].electronicBook.electronicBookSuggestGroups[0].suggestion

        this.SuggestionForm.patchValue({
          checkDetail: result.centralpolicydata.centralPolicyUser[0].electronicBook.electronicBookSuggestGroups[0].detail,
          Problem: result.centralpolicydata.centralPolicyUser[0].electronicBook.electronicBookSuggestGroups[0].problem,
          Suggestion: result.centralpolicydata.centralPolicyUser[0].electronicBook.electronicBookSuggestGroups[0].suggestion
        })
      })
  }
  getCentralPolicyUser() {
    this.centralpolicyservice.getdetailuseracceptcentralpolicydata(this.id)
      .subscribe(result => {
        this.resultcentralpolicyuser = result
        console.log("result", result);
      })
  }

  // getSubjectCentralPolicyProvince() {
  //   this.centralpolicyservice.getSubjectCentralPolicyProvince(this.id)
  //     .subscribe(result => {
  //       this.resultdetailcentralpolicyprovince = result
  //       console.log("resultdetailcentralpolicyprovince : ", result);
  //     })
  // }

  getDetailCentralPolicyProvince() {
    this.centralpolicyservice.getdetailcentralpolicyprovincedata(this.centralpolicyproviceid)
      .subscribe(result => {
        console.log("123", result);
        // alert(JSON.stringify(result))
        this.resultdetailcentralpolicy = result.centralpolicydata
        this.resultdetailcentralpolicyprovince = result.subjectcentralpolicyprovincedata
        this.resultuser = result.userdata
        this.electronicbookid = result.centralPolicyEventdata.electronicBookId

        this.resultdate = result.centralPolicyEventdata.inspectionPlanEvent
        this.provincename = result.provincedata.name
        this.provinceid = result.provincedata.id

        // this.getCalendarFile();
      })
  }


  // storeAccept(value, answer) {
  //   this.centralpolicyservice.acceptCentralpolicy(value, answer, this.id)
  //     .subscribe(response => {
  //       console.log(response);
  //       this.Form.reset()

  //       this.notificationService.addNotification(this.resultdetailcentralpolicy.id, this.provinceid, this.userid, 2, 1)
  //         .subscribe(response => {
  //           console.log(response);
  //         })

  //       this.router.navigate(['calendaruser'])
  //     })
  // }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  sendAssign(value) {
    this.centralpolicyservice.sendAssign(value, this.id).subscribe(response => {
      console.log(response);
      this.modalRef.hide();
      this.router.navigate(['calendaruser'])
    })
  }

  getAssign() {
    this.centralpolicyservice.getAssign(this.id).subscribe(res => {
      this.assignDetail = res.forward;
      console.log("ASSIGN: ", this.assignDetail);
      this.assignForm.patchValue({
        assign: this.assignDetail
      })
    })
  }
  // getCalendarFile() {
  //   this.electronicBookService.getCalendarFile(this.electronicbookid).subscribe(res => {
  //     this.carlendarFile = res;
  //     console.log("calendarFile: ", res);

  //   })
  // }

}
