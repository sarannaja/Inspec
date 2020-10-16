import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IframeComponent } from './iframe.component';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

const routes: Routes = [
  {
    path: '',
    component: IframeComponent
  }
]

@NgModule({
  declarations: [IframeComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(routes),

  ],
  exports: [RouterModule]
})
export class IframeModule { }
