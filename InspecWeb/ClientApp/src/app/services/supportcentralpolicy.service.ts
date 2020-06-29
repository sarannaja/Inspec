import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SupportcentralpolicyService {

  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/centralpolicy/';
    
  }
  getcentralpolicydata(): Observable<any[]> {
    return this.http.get<any[]>(this.url)
  }
  getdetailcentralpolicydata(id): Observable<any> {
    //console.log("ID: ", id);

    return this.http.get<any>(this.url + id)
  }


}
