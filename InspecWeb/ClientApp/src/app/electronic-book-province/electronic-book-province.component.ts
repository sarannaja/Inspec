import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { ElectronicbookService } from '../services/electronicbook.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { InspectionplanService } from '../services/inspectionplan.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-electronic-book-province',
  templateUrl: './electronic-book-province.component.html',
  styleUrls: ['./electronic-book-province.component.css']
})
export class ElectronicBookProvinceComponent implements OnInit {
  electronicBookData: Array<any> = [];
  loading = false;
  dtOptions: DataTables.Settings = {};
  userid: string;
  delid: any;
  modalRef: BsModalRef;
  centralpolicyprovinceid: any;
  role_id
  countaccept: Array<any> = [];
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
        this.userService.getuserfirstdata(this.userid)
          .subscribe(result => {
            this.role_id = result[0].role_id
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
    this.electronicBookService.getElectronicBookProvince(this.userid).subscribe(results => {
      console.log("res: ", results);
      this.electronicBookData = results;

      if (this.role_id == 9) {
        this.electronicBookData.forEach(element => {
          console.log('electronicBookAccepts', element);
          element.electronicBookAccepts.forEach(element2 => {
            // alert("123")
            if (element2.userId == this.userid) {
              this.countaccept[element.id] = 1;
            }
          });
        });
      }

      console.log("ELECTDATA: ", this.electronicBookData);

      this.loading = true;
    })
  }

  gotoDetail(id, elecId) {
    this.router.navigate(['/electronicbook/detail/' + id, { electronicBookId: elecId }])
  }

  Accept(value) {
    this.electronicBookService.acceptelectronicbook(value, this.userid).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.loading = false
      this.getElectronicBook();
    })
  }
}

