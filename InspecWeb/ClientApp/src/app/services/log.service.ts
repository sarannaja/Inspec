import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LogService {

  count = 0
  url = "";


  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string)
  {
    this.url = baseUrl + 'api/Log/';
  }

  addLog(UserId,DatabaseName,EventType,status) {
    
    var formData = new FormData();
    
    formData.append('UserId',UserId); //id user
    formData.append('DatabaseName',DatabaseName); //table name
    formData.append('EventType',EventType); // ลบ แก้ไข หรือ เพิ่ม
    formData.append('ObjectType',status); // text
   
    return this.http.post(this.url, formData);
  }




}