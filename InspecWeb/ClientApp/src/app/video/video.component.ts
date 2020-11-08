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
 
  dtOptions: DataTables.Settings = {};
  videos: Video[] = [];
  currentvideo = null;
  currentIndex = -1;
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
    this.retrievevideos();
    // this.saveVideo()
  }


  saveVideo() {
    let video: Video = {
      title: 'test',
      description: "test des",
      url: "https://google.com",
      published: true,
      type:'video/mp4'
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
    this.retrievevideos();
  }

  retrievevideos() {
    this.videoService.getAll().snapshotChanges().pipe(
      map(changes =>
        changes.map(c =>
          ({ key: c.payload.key, ...c.payload.val() })
        )
      )
    ).subscribe(data => {
      this.videos = data;
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
