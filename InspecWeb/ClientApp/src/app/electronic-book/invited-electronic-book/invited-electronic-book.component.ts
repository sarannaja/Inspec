import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
import { ElectronicbookService } from 'src/app/services/electronicbook.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { InspectionplanService } from 'src/app/services/inspectionplan.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-invited-electronic-book',
  templateUrl: './invited-electronic-book.component.html',
  styleUrls: ['./invited-electronic-book.component.css']
})
export class InvitedElectronicBookComponent implements OnInit {

  electronicBookData: any = [];
  loading = false;
  dtOptions: DataTables.Settings = {};
  userid: string;
  delid: any;
  modalRef: BsModalRef;
  centralpolicyprovinceid: any;
  role_id

  constructor(
    private router: Router,
    private electronicBookService: ElectronicbookService,
    private authorize: AuthorizeService,
    private modalService: BsModalService,
    private inspectionplanservice: InspectionplanService,
    private spinner: NgxSpinnerService,
    private userService: UserService,
    @Inject('BASE_URL') baseUrl: string
  ) { }

  ngOnInit() {
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
        // alert(this.userid)
        this.userService.getuserfirstdata(this.userid)
          .subscribe(result => {
            // this.resultuser = result;
            //console.log("test" , this.resultuser);
            this.role_id = result[0].role_id
            // alert(this.role_id)
          })
      })
    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [3],
          orderable: false
        }
      ]
    };
    this.getElectronicBook();
  }

  openModal(template: TemplateRef<any>, id) {
    this.delid = id;
    this.modalRef = this.modalService.show(template);
  }

  getElectronicBook() {
    this.electronicBookService.getElectronicBookInvited(this.userid).subscribe(results => {
      console.log("res: ", results);
      this.electronicBookData = results;
      console.log("electInvite: ", this.electronicBookData);

      this.loading = true;
    })
  }

  createElectronicBook() {
    this.router.navigate(['/electronicbook/create'])
  }

  deleteElectronicBook() {
    this.electronicBookService.deleteElectronicBook(this.delid).subscribe(result => {
      console.log('Delete Res: ', result);
      this.modalRef.hide();
      this.getElectronicBook();
    });
  }

  gotoDetail(id, inviteid) {
    this.router.navigate(['/electronicbook/invitedetail/' + id, {ebookInviteId: inviteid}])
  }

  gotoTheme(cenid, proid, elecId) {
    // this.router.navigate(['/electronicbook/theme/' + id, { electronicBookId: elecId }])

    this.inspectionplanservice.getcentralpolicyprovinceid(cenid, proid).subscribe(result => {
      // this.centralpolicyprovinceid = result
      // this.router.navigate(['/electronicbook/edit/' + result, { electronicBookId: elecId }])
      var id = result
      this.router.navigate(['/electronicbook/theme/' + id, { electronicBookId: elecId }])
    })
  }
}

