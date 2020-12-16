import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BeforeLoginComponent } from './before-login.component';
import { RouterModule, Routes } from '@angular/router';
import { SharingModule } from '../sharing/sharing.module';

const routes: Routes = [
  { path: '', redirectTo: '/', pathMatch: 'full' },
  { path: '', component: BeforeLoginComponent }
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes),
    
  ],
  exports: [RouterModule]
})
export class BeforeLoginRoutingModule { }
