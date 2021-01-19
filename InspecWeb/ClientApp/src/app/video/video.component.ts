import { Component, OnInit, ViewChild } from '@angular/core';
import { FirebaseApp } from '@angular/fire';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { map } from 'rxjs/operators';
import Video, { VideoService } from '../services/video.service';
import { OpenVideoComponent } from './modals/open-video/open-video.component';

@Component({
  selector: 'app-video',
  templateUrl: './video.component.html',
  styleUrls: ['./video.component.css']
})
export class VideoComponent implements OnInit {

  dtOptions: any = {};
  videos: Video[] = [];
  currentvideo = null;
  currentIndex = -1;
  checkrole: any[] = [
    {
      id: 0,
      name: "ทั้งหมด"
    },
    {
      id: 1,
      name: "การใช้งานระบบสำหรับผู้ดูแลระบบ"
    },
    {
      id: 2,
      name: "การใช้งานระบบสำหรับบุคลากรในระบบตรวจราชการ"
    },
    {
      id: 3,
      name: "การใช้งานระบบสำหรับหน่วยรับตรวจ"
    },
  ]
  title = '';
  video: Video = new Video();
  submitted = false;
  constructor(public app: FirebaseApp,
    private videoService: VideoService,
    private modalService: BsModalService

  ) { }

  ngOnInit() {
    this.dtOptions = {
      pagingType: 'full_numbers',
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
    this.retrievevideos({ id: 0 });
    // this.saveVideo()
  }
  checkRoleShow($event) {
    console.log($event);

  }

  saveVideo() {
    let video: Video = {
      title: 'test',
      description: "test des",
      url: "https://google.com",
      published: true,
      type: 'video/mp4'
    }
    this.videoService.create(video)
      .then(() => {
        console.log('Created new item successfully!');
        this.submitted = true;
      });
  }

  newVideo() {
    this.submitted = false;
    this.video = new Video();
  }
  refreshList() {
    this.currentvideo = null;
    this.currentIndex = -1;
    this.retrievevideos({ id: 0 });
  }

  retrievevideos($event) {
    this.videos = []
    this.videoService.getAll().snapshotChanges().pipe(
      map(changes =>
        changes.map(c =>
          ({ key: c.payload.key, ...c.payload.val() })
        )
      )
    ).subscribe(data => {

      this.videos = $event.id == 0 ? data : data.filter(res => res.role_id == $event.id);
      console.log(this.videos);
    });
  }
  modalRef: BsModalRef;

  openModal(video: any) {
    this.modalRef = this.modalService.show(OpenVideoComponent, {
      initialState: {
        // title: `${video.name} ( ${video.fiscalYears[0].name} ${video.cabinet.name} )`,
        title: `${video.title}`,
        video: video,
        data: {},

      },
      class: 'modal-dialog-centered modal-lg'
    });
  }
}
