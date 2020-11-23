import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
import { ElectronicbookService } from 'src/app/services/electronicbook.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { InspectionplanService } from 'src/app/services/inspectionplan.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-electronic-book-other',
  templateUrl: './electronic-book-other.component.html',
  styleUrls: ['./electronic-book-other.component.css']
})
export class ElectronicBookOtherComponent implements OnInit {
  electronicBookData: any = [];
  loading = false;
  dtOptions: any = {};
  userid: string;
  delid: any;
  modalRef: BsModalRef;
  centralpolicyprovinceid: any;
  role_id;
  provincialDepartmentId: any;
  userProvince: any = [];

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
    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [3],
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
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
        // alert(this.userid)
        this.userService.getuserfirstdata(this.userid)
          .subscribe(result => {
            // this.resultuser = result;
            //console.log("test" , this.resultuser);
            console.log("provinceId: ", result);

            this.role_id = result[0].role_id
            this.provincialDepartmentId = result[0].provincialDepartmentId
            this.userProvince = result[0].userProvince
            this.getElectronicBook();
          })
      })
  }

  openModal(template: TemplateRef<any>, id) {
    this.delid = id;
    this.modalRef = this.modalService.show(template);
  }

  getElectronicBook() {
    this.electronicBookService.getSendedElectronicBookOtherDepartment(this.provincialDepartmentId).subscribe(results => {
      // console.log("res: ", results);
      console.log("userProvince: ", this.userProvince);
      var test: any = [];
      this.userProvince.forEach(element => {
        test.push(element.provinceId)
      });
      console.log("TEST: ", test);

      this.electronicBookData = [];
      console.log("ELECTDATA: ", this.electronicBookData);
        results.forEach(element2 => {
          if (test.includes(element2.provinceId) == true) {
            this.electronicBookData.push(element2)
          }
        })
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
      this.getElectronicBook();
    });
  }

  gotoEdit(id, elecId, centralPolicyUserID) {
    console.log("ID: ", id);
    console.log("ELECID: ", elecId);
    console.log("centralPolicyUserID", centralPolicyUserID);
    this.router.navigate(['/electronicbook/edit/' + id, { electronicBookId: elecId, centralPolicyUserId: centralPolicyUserID }])
  }

  gotoDetail(id, otherId) {
    // console.log("OtherID: ", otherId);
    this.router.navigate(['/electronicbook/otherdetail/' + id, { acceptOtherId: otherId }])
  }

  gotoTheme(id, elecId) {
    this.router.navigate(['/electronicbook/theme/' + id, { electronicBookId: elecId }])
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
