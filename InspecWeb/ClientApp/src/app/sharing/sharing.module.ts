import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ThaiDatePipe2 } from '../services/Pipe/thaidate2.service';
import { DataTablesModule } from 'angular-datatables';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AngularFireModule } from '@angular/fire';
import { AngularFireDatabaseModule } from '@angular/fire/database';
import { VideoService } from '../services/video.service';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TitleModalComponent } from '../external-organization/otps/modals/title-modal/title-modal.component';
import { MatVideoModule } from 'mat-video';
import { DateLengthComponent } from '../services/components/date-length/date-length.component';
import { MyDatePickerTHModule } from 'mydatepicker-th';
import { TimepickerModule } from 'ngx-bootstrap/timepicker';

const config = {
  apiKey: "AIzaSyAQuvxbuqchibI6XJRVXRdwXvi9sDQAj-g",
  authDomain: "palmfirebase.firebaseapp.com",
  databaseURL: "https://palmfirebase.firebaseio.com",
  projectId: "palmfirebase",
  storageBucket: "palmfirebase.appspot.com",
  messagingSenderId: "336991440347",
  appId: "1:336991440347:web:61f568bcdb790b92046bfe"
};
@NgModule({
  declarations: [ThaiDatePipe2,DateLengthComponent,TitleModalComponent],
  imports: [
    CommonModule,
    DataTablesModule,
    FormsModule,
    ReactiveFormsModule,
    AngularFireModule.initializeApp(config),
    AngularFireDatabaseModule,
    ModalModule.forRoot(),
    MatVideoModule,
    TimepickerModule.forRoot(),

    MyDatePickerTHModule
  ],
  exports: [
    CommonModule,
    ThaiDatePipe2,
    DataTablesModule,
    AngularFireModule,
    AngularFireDatabaseModule,
    FormsModule,
    TimepickerModule,
    TitleModalComponent,
    MyDatePickerTHModule,
    ModalModule,
    ReactiveFormsModule,
    DateLengthComponent,
    MatVideoModule
  ],
  providers: [VideoService]
})
export class SharingModule { }
