import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  count = 0
  url = "";


  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string)
  {
    this.url = baseUrl + 'api/notification/';
  }

  addNotification(CentralPolicyId,ProvinceId,UserId,status) {

    const formData = new FormData();
    
    formData.append('CentralPolicyId',CentralPolicyId);
    formData.append('ProvinceId',ProvinceId);
    formData.append('UserId',UserId);
    formData.append('Status',status);

    return this.http.post(this.url, formData);
  }


}
