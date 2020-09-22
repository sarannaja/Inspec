import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TrainingService } from '../services/training.service';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { TrainingRegisterlist } from '../services/toeymodel/trainingregisterlist';

@Component({
  selector: 'app-training-idcode',
  templateUrl: './training-idcode.component.html',
  styleUrls: ['./training-idcode.component.css']
})
export class TrainingIDCodeComponent implements OnInit {

  dtOptions: DataTables.Settings = {};
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



  constructor(
    private activatedRoute: ActivatedRoute,
    private _trainingservice: TrainingService,
    private router: Router,
    @Inject('BASE_URL') baseUrl: string,
  ) {
    this.url = baseUrl,
      this.trainingid = activatedRoute.snapshot.paramMap.get('id');
  }

  ngOnInit() {
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
  getData() {
    this._trainingservice.getTrainingregisterlist(this.trainingid)
      .subscribe(result => {
        this.trainingRegisterlist = result.map((result, index) => { return { ...result, code: this.genCode(index, result.training.year, result.training.courseCode) } })
        console.log(this.trainingRegisterlist);

      })
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

      this.getData()
    })
  }

}
