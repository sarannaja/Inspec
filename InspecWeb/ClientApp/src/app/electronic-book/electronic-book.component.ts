import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { ElectronicbookService } from '../services/electronicbook.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { InspectionplanService } from '../services/inspectionplan.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-electronic-book',
  templateUrl: './electronic-book.component.html',
  styleUrls: ['./electronic-book.component.css']
})
export class ElectronicBookComponent implements OnInit {

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
    this.electronicBookService.getElectronicBook(this.userid).subscribe(results => {
      console.log("res: ", results);
      this.electronicBookData = results;
      console.log("ELECTDATA: ", this.electronicBookData);

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

  gotoEdit(id, elecId, centralPolicyUserID) {
    console.log("ID: ", id);
    console.log("ELECID: ", elecId);
    console.log("centralPolicyUserID", centralPolicyUserID);
    this.router.navigate(['/electronicbook/edit/' + id, { electronicBookId: elecId, centralPolicyUserId: centralPolicyUserID }])
  }

  gotoDetail(id, elecId) {
    this.router.navigate(['/electronicbook/detail/' + id ,{electronicBookId: elecId}])
  }

  gotoEdit2(id, elecId) {
    // alert(id)
    // alert(elecId)
    // this.inspectionplanservice.getcentralpolicyprovinceid(cenid, proid).subscribe(result => {
    //   // this.centralpolicyprovinceid = result
    //   this.router.navigate(['/electronicbook/edit/' + result, { electronicBookId: elecId, centralPolicyUserId: cenid }])
    // })
    // alert(this.centralpolicyprovinceid)

    this.router.navigate(['/electronicbook/edit/' + id ,{electronicBookId: elecId}])
  }
}
