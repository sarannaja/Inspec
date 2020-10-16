import { Component, Inject, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { ElectronicbookService } from '../services/electronicbook.service';
import { InspectionplanService } from '../services/inspectionplan.service';
import { NotofyService } from '../services/notofy.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-electronic-book-all',
  templateUrl: './electronic-book-all.component.html',
  styleUrls: ['./electronic-book-all.component.css']
})
export class ElectronicBookAllComponent implements OnInit {
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
    private _NotofyService: NotofyService,
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
          targets: [6],
          orderable: false
        }
      ],
      "language": {
        "lengthMenu": "แสดง  _MENU_  รายการ",
        "search": "ค้นหา:",
        // "info": "แสดง _PAGE_ ของ _PAGES_ รายการ",
        // "info": "แสดง _PAGE_ ของ _PAGES_ รายการ จาก _TOTAL_ แถว",
        "info": "แสดง _START_ ถึง _END_ จาก _TOTAL_ แถว",
        // "info": "แสดง _START_ ถึง _END_ จาก _TOTAL_ แถว",
        "infoEmpty": "แสดง 0 ของ 0 รายการ",
        "zeroRecords": "ไม่พบข้อมูล",
        "paginate": {
          "first": "หน้าแรก",
          "last": "หน้าสุดท้าย",
          "next": "ต่อไป",
          "previous": "ย้อนกลับ"
        },
      }
    };
    this.getElectronicBook();
  }

  openModal(template: TemplateRef<any>, id) {
    this.delid = id;
    this.modalRef = this.modalService.show(template);
  }

  getElectronicBook() {
    this.electronicBookService.getElectronicBookAll().subscribe(results => {
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
      this.loading = false;
      this.modalRef.hide();
      this._NotofyService.onSuccess("ลบข้อมูล",)
      this.getElectronicBook();
    });
  }

  gotoEdit(id, elecId, centralPolicyUserID) {
    console.log("ID: ", id);
    console.log("ELECID: ", elecId);
    console.log("centralPolicyUserID", centralPolicyUserID);
    this.router.navigate(['/electronicbook/edit/' + id, { electronicBookId: elecId, centralPolicyUserId: centralPolicyUserID }])
  }

  gotoDetail(id) {
    this.router.navigate(['/electronicbookall/detail/' + id])
  }

  gotoTheme(id) {
    // alert(elec)
    this.router.navigate(['/electronicbook/theme/' + id])
  }

  gotoEdit2(id, elecId) {
    // alert(id)
    // alert(elecId)
    // this.inspectionplanservice.getcentralpolicyprovinceid(cenid, proid).subscribe(result => {
    //   // this.centralpolicyprovinceid = result
    //   this.router.navigate(['/electronicbook/edit/' + result, { electronicBookId: elecId, centralPolicyUserId: cenid }])
    // })
    // alert(this.centralpolicyprovinceid)

    this.router.navigate(['/electronicbook/edit/' + id, { electronicBookId: elecId }])
  }

}
