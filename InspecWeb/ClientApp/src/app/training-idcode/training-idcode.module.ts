import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TrainingIDCodeComponent } from './training-idcode.component';
import { Routes, RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DragDropModule } from '@angular/cdk/drag-drop';

const routes: Routes = [
  {
    path: '',
    component: TrainingIDCodeComponent
  }
]

@NgModule({
  declarations: [TrainingIDCodeComponent],
  imports: [
    CommonModule,
    // BrowserModule,
    // BrowserAnimationsModule,
    DragDropModule,
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class TrainingIdcodeModule { }
