import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { CentralpolicyService } from '../../services/centralpolicy.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from "ngx-spinner";
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { InspectionplanService } from 'src/app/services/inspectionplan.service';

@Component({
  selector: 'app-user-central-policy',
  templateUrl: './user-central-policy.component.html',
  styleUrls: ['./user-central-policy.component.css']
})
export class UserCentralPolicyComponent implements OnInit {

  resultcentralpolicy: any = []
  delid: any
  modalRef: BsModalRef;
  dtOptions: DataTables.Settings = {};
  loading = false;
  userid: string
  centralpolicyprovinceid: any
  id
  constructor(
    private router: Router,
    private centralpolicyservice: CentralpolicyService,
    private modalService: BsModalService,
    private authorize: AuthorizeService,
    private activatedRoute: ActivatedRoute,
    private inspectionplanservice: InspectionplanService,
    private spinner: NgxSpinnerService) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
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

    this.centralpolicyservice.getcentralpolicyuserinviteddata(this.userid, this.id)
      .subscribe(result => {
        this.resultcentralpolicy = result
        this.loading = true;
        this.spinner.hide();
        console.log("resultcentralpolicyDATA: ", this.resultcentralpolicy);
      })

  }

  openModal(template: TemplateRef<any>, id) {
    this.delid = id;
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

}
