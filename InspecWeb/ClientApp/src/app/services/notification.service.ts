import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  count = 0
  url = "";
  testLoop(func) {
    console.log(`loop to ${func}`);
  }

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/notification/';
  }

  getnotificationsdata(id: any): Observable<any[]> {
    this.testLoop('getnotificationsdata')
    return this.http.get<any[]>(this.url + 'getnotifications/' + id)
  }
  getnotificationscountdata(id: any): Observable<any> {
    this.testLoop('getnotificationscountdata')
    return this.http.get<any>(this.url + 'getnotificationscount/' + id)
  }

  getnotificationsdatastatus10(id: any): Observable<any> {
    return this.http.get<any>(this.url + 'getnotificationsdatastatus10/' + id)
  }

  getnotificationsforexecutiveorderdata(id: any): Observable<any> {
    this.testLoop('getnotificationsforexecutiveorderdata')
    return this.http.get<any>(this.url + 'getnotificationsforexecutiveorder/' + id)
  }

  addNotification(CentralPolicyId, ProvinceId, UserId, status, xe,title) {
    // alert(status)
    this.testLoop('addNotification')

    var formData = new FormData();

    formData.append('CentralPolicyId', CentralPolicyId);
    formData.append('ProvinceId', ProvinceId);
    formData.append('UserId', UserId);
    formData.append('Status', status);
    formData.append('xe', xe);
    formData.append('title', title);

    return this.http.post(this.url, formData);
  }

  updateNotification(id) {
    this.testLoop('updateNotification')
    const formData = new FormData();
    formData.append('update', 'update');
    return this.http.put(this.url + id, formData);
  }

  getinspactionsplaneven(id: any): Observable<any> {
    //this.testLoop('getnotificationsdata')
    return this.http.get<any>(this.url + 'getinspactionsplaneven/' + id)
  }

  getElectronicBookUserInvite(userId: any, electId: any): Observable<any> {
    return this.http.get<any>(this.url + 'getElectronicBookUserInvite/' + electId + "/" + userId)
  }

  getElectronicBookProvincialDepartment(provincialId: any, electId: any): Observable<any> {
    return this.http.get<any>(this.url + 'getElectronicBookProvincialDepartment/' + electId + "/" + provincialId)
  }
  getCentralPolicyProvince(cenId: any, provinceId: any): Observable<any> {
    return this.http.get<any>(this.url + 'getCentralPolicyProvince/' + cenId + "/" + provinceId)
  }

}
