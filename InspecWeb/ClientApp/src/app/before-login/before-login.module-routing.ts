import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BeforeLoginComponent } from './before-login.component';
import { RouterModule, Routes } from '@angular/router';
import { SharingModule } from '../sharing/sharing.module';

const routes: Routes = [
  // { path: '', redirectTo: '/', pathMatch: 'full' },

  {
    path: '',
    children: [
      { path: '', component: BeforeLoginComponent },
      { path: 'videoex', loadChildren: () => import('../video/video.module').then(m => m.VideoModule) },
    ]
  }

  // {
  //   children: [


  //   ]
  // }
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes),

  ],
  exports: [RouterModule]
})
export class BeforeLoginRoutingModule { }
