import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ThaiDatePipe2 } from '../services/Pipe/thaidate2.service';
import { DataTablesModule } from 'angular-datatables';
import { FormsModule } from '@angular/forms';
import { AngularFireModule } from '@angular/fire';
import { AngularFireDatabaseModule } from '@angular/fire/database';
import { VideoService } from '../services/video.service';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TitleModalComponent } from '../external-organization/otps/modals/title-modal/title-modal.component';
import { MatVideoModule } from 'mat-video';

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
  declarations: [ThaiDatePipe2, TitleModalComponent],
  imports: [
    CommonModule,
    DataTablesModule,
    FormsModule,
    AngularFireModule.initializeApp(config),
    AngularFireDatabaseModule,
    ModalModule.forRoot(),
    MatVideoModule

  ],
  exports: [
    CommonModule,
    ThaiDatePipe2,
    DataTablesModule,
    AngularFireModule,
    AngularFireDatabaseModule,
    FormsModule,
    TitleModalComponent,
    ModalModule,
    MatVideoModule
  ],
  providers: [VideoService]
})
export class SharingModule { }
