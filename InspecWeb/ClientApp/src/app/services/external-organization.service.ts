import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Regions, Ministers } from '../models/otps';
// [assembly: RegisterModule(typeof(SSLRequestModule))]                                                                                                                                                                                                                                                                                                                                             

@Injectable({
  providedIn: 'root'
})
export class ExternalOrganizationService {
  httpOptions = { 
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }
  //api ระบบพี่ตัง
  getRegions(): Observable<Regions[]> {
    return this.http.get<Regions[]>('http://203.113.14.20:3000/testservice/otps/regions',this.httpOptions,)
  }
  getMinisters(): Observable<Ministers[]> {
    return this.http.get<Ministers[]>(this.baseUrl + 'api/ExternalOrganization/otps/ministers', this.httpOptions)
  }
}
