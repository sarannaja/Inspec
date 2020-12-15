import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BeforeLoginComponent } from './before-login.component';
import { RouterModule, Routes } from '@angular/router';
import { SharingModule } from '../sharing/sharing.module';
import { BeforeLoginRoutingModule } from './before-login.module-routing';


@NgModule({
  declarations: [BeforeLoginComponent],
  imports: [
    CommonModule,
    SharingModule,
    BeforeLoginRoutingModule,
  ],
  exports: [BeforeLoginRoutingModule]
})
export class BeforeLoginModule { }
