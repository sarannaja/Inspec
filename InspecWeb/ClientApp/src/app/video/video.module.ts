import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VideoComponent } from './video.component';
import { RouterModule, Routes } from '@angular/router';
import { SharingModule } from '../sharing/sharing.module';
import { OpenVideoComponent } from './modals/open-video/open-video.component';

const routes: Routes = [
  // {
  //   path: '',
  //   pathMatch: 'full',
  //   redirectTo: ''
  // },
  {
    path: '',
    component: VideoComponent
  }
]

@NgModule({
  declarations: [VideoComponent, OpenVideoComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharingModule
  ],
  exports: [
    RouterModule
  ],
  entryComponents: [OpenVideoComponent],
  providers: [

  ]
})
export class VideoModule { }
