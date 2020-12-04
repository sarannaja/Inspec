import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TrainingService } from '../services/training.service';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { TrainingRegisterlist } from '../services/toeymodel/trainingregisterlist';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { NotofyService } from '../services/notofy.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { LogService } from '../services/log.service';

@Component({
  selector: 'app-training-idcode',
  templateUrl: './training-idcode.component.html',
  styleUrls: ['./training-idcode.component.css']
})
export class TrainingIDCodeComponent implements OnInit {

  dtOptions: any = {};
  people: any = [];
  loading = false;
  trainingid: any;
  trainingRegisterData: any = [];
  printData: any = [];
  hide = false;
  url: any;
  trainingRegisterlist: TrainingRegisterlist[] = []

  movies = [
    'Episode I - The Phantom Menace',
    'Episode II - Attack of the Clones',
    'Episode III - Revenge of the Sith',
    'Episode IV - A New Hope',
    'Episode V - The Empire Strikes Back',
    'Episode VI - Return of the Jedi',
    'Episode VII - The Force Awakens',
    'Episode VIII - The Last Jedi',
    'Episode IX – The Rise of Skywalker'
  ];
  modalRef: any;
  userid: string;

  constructor(
    private modalService: BsModalService,
    private authorize: AuthorizeService,
    private _NotofyService: NotofyService,
    private spinner: NgxSpinnerService,
    private logService: LogService,
    
    private activatedRoute: ActivatedRoute,
    private _trainingservice: TrainingService,
    private router: Router,
  ) {
      this.trainingid = activatedRoute.snapshot.paramMap.get('id');
  }

  ngOnInit() {
    this.getuserinfo();

    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [3, 4],
          orderable: false
        }
      ],
      "language": {
        "lengthMenu": "แสดง  _MENU_  รายการ",
        "search": "ค้นหา:",
        "info": "แสดง _START_ ถึง _END_ จาก _TOTAL_ แถว",
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
    // this.getPeopleRegistered();
    this.getData()
  }

  //start getuser
  getuserinfo() {
    this.spinner.show();
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
      })
  }
  
  getData() {
    this._trainingservice.getTrainingregisterlist(this.trainingid)
      .subscribe(result => {
        this.trainingRegisterlist = result.map((result, index) => { return { ...result, code: this.genCode(index, result.training.year, result.training.courseCode) } })
        console.log(this.trainingRegisterlist);

      })
      this.spinner.hide();
  }
  genCode(index, year, courseCode) {
    return year.toString() + courseCode.toString() + ("00" + (index + 1)).slice(-3)
  }
  drop(event: CdkDragDrop<string[]>) {

    moveItemInArray(this.movies, event.previousIndex, event.currentIndex);
  }

  dropTraining(event: CdkDragDrop<string[]>) {

    moveItemInArray(this.trainingRegisterlist, event.previousIndex, event.currentIndex);
    // let trainingRegisterlist: TrainingRegisterlist = this.trainingRegisterlist.find((res, index) => index == event.currentIndex)
    this.trainingRegisterlist = this.trainingRegisterlist
      .map((result, index) => {
        return {
          ...result, code: + this.genCode(index, result.training.year, result.training.courseCode)
        }
      })
    console.log(this.trainingRegisterlist);
    // console.log(trainingRegisterlist.provincialDepartments.name + ' - ' + trainingRegisterlist.name + ' : ' + ("00" + (event.currentIndex + 1)).slice(-3));



  }
  UpdateIdCode(){

    // alert(JSON.stringify(this.trainingRegisterlist))
    // console.log(this.trainingRegisterlist);
    
    this._trainingservice.Updateidcode(this.trainingRegisterlist).subscribe(response => {
      // this.Form.reset()
      // this.modalRef.hide()
      // this.loading = false
      console.log("response =>", response);
      
      this.modalRef.hide()
      this.loading = false;
      //this.logService.addLog(this.userid,'TrainingRegisters','เพิ่ม',response.name,response.id).subscribe();
      this.getData()
      this._NotofyService.onSuccess("เพิ่มข้อมูล")
      
    })
  }

  gotoBack() {
    window.history.back();
  }

  gotoMain(){
    this.router.navigate(['/main'])
  }

  gotoMainTraining(){
    this.router.navigate(['/training'])
  }

  gotoTrainingManage(){
    this.router.navigate(['/training/manage/', this.trainingid])
  }

  openModal(template: TemplateRef<any>) {

   this.modalRef = this.modalService.show(template);
 }

}
