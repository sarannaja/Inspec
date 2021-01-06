import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TrainingProgramLoginComponent } from './training-programlogin.component';
import { RouterModule, Routes } from '@angular/router';
import { QRCodeModule } from 'angularx-qrcode';
import { SharingModule } from '../sharing/sharing.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthorizeInterceptor } from 'src/api-authorization-new/authorize.interceptor';

const routes: Routes = [
  // {
  //   path: '',
  //   pathMatch: 'full',
  //   redirectTo: '/'
  // },
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
    SharingModule,

    HttpClientModule,
  ],
  // providers: [
  //   { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },

  // ],
  // schemas: [CUSTOM_ELEMENTS_SCHEMA],
  exports: [
    RouterModule
  ]
})
export class TrainingProgramloginModule { }
