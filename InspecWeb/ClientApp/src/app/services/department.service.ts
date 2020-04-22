import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {

  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { 
    this.url = baseUrl + 'api/department/';
  }
  
  // getdepartmentdata(id): Observable<any> {
  //   // alert(id)
  //   return this.http.get<any>(this.url + id)
  // }
  getdepartmentdata(id): Observable<any> {
    // alert(id)
    return this.http.get<any>(this.url+ "masteraof/" + id)
  }
}
