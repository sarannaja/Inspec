import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import Video from 'src/app/services/video.service';

@Component({
  selector: 'app-open-video',
  templateUrl: './open-video.component.html',
  styleUrls: ['./open-video.component.css']
})
export class OpenVideoComponent implements OnInit {
  // @ViewChild(PlyrComponent, { static: true })

  // plyr: PlyrComponent;
  // player: Plyr;
  @ViewChild("videoPlayer", { static: false }) videoplayer: ElementRef;
  isPlay: boolean = false;
  toggleVideo(event: any) {
    this.videoplayer.nativeElement.play();
  }
  playPause() {
    var myVideo: any = document.getElementById("my_video_1");
    if (myVideo.paused) myVideo.play();
    else myVideo.pause();
  }

  makeBig() {
    var myVideo: any = document.getElementById("my_video_1");
    myVideo.width = 560;
  }

  makeSmall() {
    var myVideo: any = document.getElementById("my_video_1");
    myVideo.width = 320;
  }

  makeNormal() {
    var myVideo: any = document.getElementById("my_video_1");
    myVideo.width = 420;
  }

  skip(value) {
    let video: any = document.getElementById("my_video_1");
    video.currentTime += value;
  }

  restart() {
    let video: any = document.getElementById("my_video_1");
    video.currentTime = 0;
  }


  title;
  video: Video = null
  videoSources: 'https://drive.google.com/uc?export=download&id=0B1RdHU7FLbIBclhVUk9yMFZvLUE'


  constructor() { }

  ngOnInit() {
    console.log(this.video);

    // this.videoSources = [
    //   {
    //     src: this.video.url,
    //     // src: 'https://drive.google.com/uc?export=download&id=0B1RdHU7FLbIBclhVUk9yMFZvLUE',
    //     type: 'video/mp4',
    //     size: 720,
    //   },
    //   // {
    //   //   src: 'https://cdn.plyr.io/static/demo/View_From_A_Blue_Moon_Trailer-720p.mp4',
    //   //   type: 'video/mp4',
    //   //   size: 720,
    //   // },
    //   // {
    //   //   src: 'https://cdn.plyr.io/static/demo/View_From_A_Blue_Moon_Trailer-1080p.mp4',
    //   //   type: 'video/mp4',
    //   //   size: 1080,
    //   // },
    //   // {
    //   //   src: 'https://cdn.plyr.io/static/demo/View_From_A_Blue_Moon_Trailer-1440p.mp4',
    //   //   type: 'video/mp4',
    //   //   size: 1440,
    //   // },
    // ];
    // console.log(this.video);

  }

}
