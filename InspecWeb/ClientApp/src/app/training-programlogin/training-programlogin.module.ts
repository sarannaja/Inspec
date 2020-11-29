import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TrainingProgramLoginComponent } from './training-programlogin.component';
import { RouterModule, Routes } from '@angular/router';
import { QRCodeModule } from 'angularx-qrcode';
import { SharingModule } from '../sharing/sharing.module';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: '/'
  },
  {
    path: '',
    component: TrainingProgramLoginComponent
  }
]

@NgModule({
  declarations: [TrainingProgramLoginComponent],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    QRCodeModule,
    SharingModule
  ],
  exports: [
    RouterModule
  ]
})
export class TrainingProgramloginModule { }
