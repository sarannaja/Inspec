import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { CentralpolicyService } from '../../services/centralpolicy.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from "ngx-spinner";
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { InspectionplanService } from 'src/app/services/inspectionplan.service';
import { NotificationService } from 'src/app/services/notification.service';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-user-central-policy',
  templateUrl: './user-central-policy.component.html',
  styleUrls: ['./user-central-policy.component.css']
})
export class UserCentralPolicyComponent implements OnInit {

  resultcentralpolicy: any = []
  ScheduleData: any = [];
  delid: any
  modalRef: BsModalRef;
  dtOptions: DataTables.Settings = {};
  loading = false;
  userid: string
  centralpolicyprovinceid: any
  id
  provinceid
  timelineData: any = [];
  Form: FormGroup;
  assignForm: FormGroup;
  answer
  status
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private centralpolicyservice: CentralpolicyService,
    private modalService: BsModalService,
    private authorize: AuthorizeService,
    private activatedRoute: ActivatedRoute,
    private inspectionplanservice: InspectionplanService,
    private notificationService: NotificationService,
    private spinner: NgxSpinnerService) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.provinceid = activatedRoute.snapshot.paramMap.get('provinceid')
  }

  ngOnInit() {
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
        // alert(this.userid)
      })
    this.spinner.show();

    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [3],
          orderable: false
        }
      ]

    };

    this.Form = this.fb.group({
      answer: new FormControl(null, [Validators.required]),
    })
    this.assignForm = this.fb.group({
      assign: new FormControl(null, [Validators.required]),
    })

    this.centralpolicyservice.getcentralpolicyuserinviteddata(this.userid, this.id)
      .subscribe(result => {
        this.resultcentralpolicy = result
        this.status = result[0].status

        this.loading = true;
        this.spinner.hide();
        console.log("resultcentralpolicyDATA: ", this.resultcentralpolicy);
      })

    this.getTimeline();
    this.getScheduleData();
  }

  getTimeline() {
    this.inspectionplanservice.getTimeline(this.id).subscribe(res => {
      console.log("Timeline: ", res);
      this.timelineData = res.timelineData;
    })
  }

  openModal(template: TemplateRef<any>, id) {
    this.delid = id;
    this.modalRef = this.modalService.show(template);
  }

  openModal2(template: TemplateRef<any>, answer) {
    this.answer = answer
    this.modalRef = this.modalService.show(template);
  }

  Subject(id) {
    this.router.navigate(['/subject', id])
  }

  deleteCentralPolicy(value) {
    this.centralpolicyservice.deleteCentralPolicy(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.centralpolicyservice.getcentralpolicydata().subscribe(result => {
        this.resultcentralpolicy = result
        console.log(this.resultcentralpolicy);
      })
    })
  }

  CreateCentralPolicy() {
    this.router.navigate(['/centralpolicy/createcentralpolicy'])
  }
  DetailCentralPolicy(id: any) {
    this.router.navigate(['/centralpolicy/detailcentralpolicy', id])
  }

  AcceptCentralPolicy(id: any, cenid, proid) {
    this.inspectionplanservice.getcentralpolicyprovinceid(cenid, proid).subscribe(result => {
      console.log("result123", result);
      this.centralpolicyprovinceid = result
      this.router.navigate(['/acceptcentralpolicy', id, { centralpolicyproviceid: result, cenid: cenid }])
    })
  }

  gotoReport(id: any, cenid, proid) {
    this.inspectionplanservice.getcentralpolicyprovinceid(cenid, proid).subscribe(result => {
      console.log("result123", result);
      this.centralpolicyprovinceid = result

      this.router.navigate(['/reportcentralpolicy', id, { centralpolicyproviceid: result }])

    })
  }

  storeAccept(answer) {
    this.centralpolicyservice.acceptCentralpolicy(answer, this.id, this.userid)
      .subscribe(response => {
        console.log(response);
        this.Form.reset();

        location.reload();
        // this.notificationService.addNotification(this.resultdetailcentralpolicy.id, this.provinceid, this.userid, 2, 1)
        //   .subscribe(response => {
        //     console.log(response);
        //   })

        // this.router.navigate(['calendaruser'])
      })
  }
  getScheduleData() {
    this.inspectionplanservice.getScheduleData(this.id, this.provinceid).subscribe(res => {
      console.log("ScheduleData: ", res);
      this.ScheduleData = res;
    })
  }
}
