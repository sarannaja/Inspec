import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TrainingIDCodeComponent } from './training-idcode.component';
import { Routes, RouterModule } from '@angular/router';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';

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
    HttpClientModule,
    // BrowserModule,
    // BrowserAnimationsModule,
    DragDropModule,
    RouterModule.forChild(routes)
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },

  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],

  exports: [RouterModule]
})
export class TrainingIdcodeModule { }
