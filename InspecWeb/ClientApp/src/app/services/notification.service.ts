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

  getnotificationsdata(id: any): Observable<any> {
    return this.http.get<any>(this.url +'getnotifications/'+ id)
  }
  getnotificationscountdata(id: any): Observable<any> {
    return this.http.get<any>(this.url +'getnotificationscount/'+ id)
  }

  getnotificationsforexecutiveorderdata(id: any): Observable<any> {
    return this.http.get<any>(this.url +'getnotificationsforexecutiveorder/'+ id)
  }

  addNotification(CentralPolicyId,ProvinceId,UserId,status, xe) {
    
    var formData = new FormData();
    
    formData.append('CentralPolicyId',CentralPolicyId);
    formData.append('ProvinceId',ProvinceId);
    formData.append('UserId',UserId);
    formData.append('Status',status);
    formData.append('xe',xe);

    return this.http.post(this.url, formData);
  }

  updateNotification(id) {
    const formData = new FormData();
    formData.append('update', 'update');
    return this.http.put(this.url+id, formData);
  }


}
