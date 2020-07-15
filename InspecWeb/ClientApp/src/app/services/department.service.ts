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
  getalldepartdata(): Observable<any[]> {
    return this.http.get<any[]>(this.url)
  }
  
  getdepartmentdata(id): Observable<any[]> {
    return this.http.get<any[]>(this.url + id)

  }
  getdepartmentsforsupportdata(id): Observable<any[]> {
    return this.http.get<any[]>(this.url+'departmentsforsupport/'+ id)
  }
  getdepartmentsforuserdata(id): Observable<any[]> {
    return this.http.get<any[]>(this.url+'departmentsforuser/'+ id)
  }
  
}
