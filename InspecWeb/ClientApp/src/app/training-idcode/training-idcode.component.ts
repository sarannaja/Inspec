import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TrainingService } from '../services/training.service';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';

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
    private trainingservice: TrainingService,
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
    this.getPeopleRegistered();
  }

  getPeopleRegistered() {
    this.trainingservice.getregistertrainingdata(this.trainingid).subscribe(res => {
      console.log("traningRegister: ", res);
      this.trainingRegisterData = res.filter(result => {
        return result.status == 1
      })
      this.loading = true;
    })
  }
  drop(event: CdkDragDrop<string[]>) {
    console.log(event);

    moveItemInArray(this.movies, event.previousIndex, event.currentIndex);
  }


}
