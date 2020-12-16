import { Injectable } from '@angular/core';
import PNotify from 'pnotify/dist/es/PNotify';
import PNotifyButtons from 'pnotify/dist/es/PNotifyButtons';
 
@Injectable({
    providedIn: 'root'
})
export class PNotifyService {
  
  getPNotify() {
    PNotifyButtons; // Initiate the module. Important!
    return PNotify;
  }
}