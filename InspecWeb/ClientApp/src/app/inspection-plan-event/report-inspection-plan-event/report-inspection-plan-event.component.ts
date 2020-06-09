import { Component, OnInit, TemplateRef } from '@angular/core';
import { NgxSpinnerService } from "ngx-spinner";
import { InspectionplaneventService } from 'src/app/services/inspectionplanevent.service';
import { UserService } from 'src/app/services/user.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-report-inspection-plan-event',
  templateUrl: './report-inspection-plan-event.component.html',
  styleUrls: ['./report-inspection-plan-event.component.css']
})
export class ReportInspectionPlanEventComponent implements OnInit {
  loading = false;
  dtOptions: DataTables.Settings = {};
  resultschedule: any[] = []
  role_id
  userid
  modalRef: BsModalRef;
  selectregion: any = [];
  resultregion: any = [];
  constructor(
    private spinner: NgxSpinnerService,
    private inspectionplanservice: InspectionplaneventService,
    private userService: UserService,
    private authorize: AuthorizeService,
    private modalService: BsModalService,
  ) { }

  ngOnInit() {
    this.spinner.show();

    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        this.userService.getuserfirstdata(this.userid)
          .subscribe(result => {
            this.role_id = result[0].role_id
          })
      })

    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [2],
          orderable: false
        }
      ]

    };

    this.inspectionplanservice.getscheduledata(this.userid)
      .subscribe(result => {
        // alert(JSON.stringify(result))
        this.spinner.hide();
        this.resultschedule = result
        this.loading = true
      })
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  getregion() {
    this.inspectionplanservice.getregion(this.userid).subscribe(response => {
      this.resultregion = response
      this.selectregion = this.resultregion.map((item, index) => {
        return { value: item.id, label: item.name }
      })
    })
  }

  ExportRegion(value) {

  }
}
