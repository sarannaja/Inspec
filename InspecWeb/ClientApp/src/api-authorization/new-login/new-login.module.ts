import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NewLoginRoutingModule } from './new-login-routing.module';
import { NewLoginComponent } from './new-login.component';
import { FormsModule } from '@angular/forms';
import { SharingModule } from 'src/app/sharing/sharing.module';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    NewLoginRoutingModule,
    // FormsModule,
    SharingModule
  ],
  exports: [NewLoginRoutingModule]
})
export class NewLoginModule { }
