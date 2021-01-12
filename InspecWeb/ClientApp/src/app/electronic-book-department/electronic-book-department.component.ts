import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
import { ElectronicbookService } from '../services/electronicbook.service';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { InspectionplanService } from '../services/inspectionplan.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserService } from '../services/user.service';
import { forEach } from 'lodash';

@Component({
  selector: 'app-electronic-book-department',
  templateUrl: './electronic-book-department.component.html',
  styleUrls: ['./electronic-book-department.component.css']
})
export class ElectronicBookDepartmentComponent implements OnInit {
  electronicBookData: any = [];
  loading = false;
  dtOptions: any = {};
  userid: string;
  delid: any;
  modalRef: BsModalRef;
  centralpolicyprovinceid: any;
  role_id;
  provinceId;
  provincialDepartmentID: any;
  checkSort = 0;

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
            console.log("user res: ", result);

            this.role_id = result[0].role_id
            // this.provinceId = result[0].provinceId
            this.provinceId = result[0].userProvince.map((item, index) => {
              return item.provinceId
            })
            this.provincialDepartmentID = result[0].provincialDepartmentId
          })
      })
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
    setTimeout(() => {
      this.getElectronicBook();
    }, 500);
  }

  openModal(template: TemplateRef<any>, id) {
    this.delid = id;
    this.modalRef = this.modalService.show(template);
  }

  getElectronicBook() {
    this.electronicBookService.getSendedElectronicBookDepartment(this.provincialDepartmentID).subscribe(results => {
      console.log("res Department: ", results);
      console.log("provinceID: ", this.provinceId);

      // let filterData = results.provinceData.filter(x => x.provinceId == this.provinceId);
      this.electronicBookData = [];
      for (let index = 0; index < results.ebookProvince.length; index++) {
        this.provinceId.forEach(element => {
          console.log(results.provinceData[index].provinceId.includes(element));

          if (results.provinceData[index].provinceId.includes(element) == true) {
            this.electronicBookData.push(results.ebookProvince[index])
          }

          if (results.ebookProvince[index].electronicBook.centralPolicy != null) {
            if (results.provinceData2[index].provinceId.includes(element) == true) {
              this.electronicBookData.push(results.ebookProvince[index])
            }
          }

        });
      }

      // console.log("filtered: ", filterData);
      // this.electronicBookData = results.ebookProvince;
      // console.log("ELECTDATA: ", this.electronicBookData);

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

  gotoDetail(id, ebookProvincialDepartmentId) {
    this.router.navigate(['/electronicbook/departmentdetail/' + id, { electronicBookProvincialDepartmentId: ebookProvincialDepartmentId }])
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

  sortDate() {
    this.loading = false;
    if (this.checkSort == 0) {
      this.electronicBookService.sortDate(this.userid).subscribe(res => {
        this.electronicBookData = res;
        this.loading = true;
        this.checkSort = 1;
      })
    } else {
      this.electronicBookService.sortDateDESC(this.userid).subscribe(res => {
        this.electronicBookData = res;
        this.loading = true;
        this.checkSort = 0;
      })
    }
  }

}
