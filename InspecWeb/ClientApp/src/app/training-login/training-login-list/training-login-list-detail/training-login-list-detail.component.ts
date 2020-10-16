import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { InspectionplanService } from 'src/app/services/inspectionplan.service';
import { NotofyService } from 'src/app/services/notofy.service';
import { TrainingLoginService } from 'src/app/services/training-login.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-training-login-list-detail',
  templateUrl: './training-login-list-detail.component.html',
  styleUrls: ['./training-login-list-detail.component.css']
})
export class TrainingLoginListDetailComponent implements OnInit {
  id: any;
  programType: any;
  userid: string;
  role_id: any;
  dtOptions: DataTables.Settings = {};
  trainingProgramData: any = [];
  trainingId: any;
  loading = false;
  userLogInData: any = [];
  Form: any;
  modalRef: BsModalRef;
  people: any = [];

  constructor(
    private router: Router,
    private trainingService: TrainingLoginService,
    private authorize: AuthorizeService,
    private modalService: BsModalService,
    private inspectionplanservice: InspectionplanService,
    private spinner: NgxSpinnerService,
    private userService: UserService,
    private _NotofyService: NotofyService,
    private activatedRoute: ActivatedRoute,
  ) {
    this.id = activatedRoute.snapshot.paramMap.get('programid');
    this.programType = activatedRoute.snapshot.paramMap.get('programType')
  }

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
    this.getUserLogin();
  }

  getUserLogin() {
    this.trainingService.getUserLogIn(this.id, this.programType).subscribe(res => {
      console.log("RES => ", res);
      this.userLogInData = res;
      this.loading = true;
    })
  }

  openModal(template: TemplateRef<any>, id) {

    this.modalRef = this.modalService.show(template);
  }

  addsPeople2(value) {
    console.log("userData => ", this.userLogInData);
    
    console.log('item.id', value);
    // var subject = value.vaule
    this.people = this.addPeople(this.people, value)
    console.log("people => ", this.people);
  }

  addPeople(array: any[], value) {
    var distinctThings: any[] = array.filter(
      (thing, i, arr) => arr.findIndex(t => t === value) === i
    );
    // //console.log('distinctThings', distinctThings);
    if (distinctThings.length != 0) {
      var s = new Set(array);
      s.delete(value);
      return Array.from(s);
    } else {
      var s = new Set(array);
      s.add(value);
      return Array.from(s);
    }
  }

  userRegister() {
    this.people.forEach(element => {
      this.trainingService.register(element, this.id, this.programType).subscribe(res => {
        console.log(res);
        
      })
    });
   
  }

}
