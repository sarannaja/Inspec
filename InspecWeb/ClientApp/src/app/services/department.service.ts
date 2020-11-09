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

  //20200720
  getdepartmentsfirst(id: any):Observable<any> {
    return this.http.get<any>(this.url +'departmentsfirst/'+ id)
  }

  getdepartmentsforsupportdata(id): Observable<any[]> {
    return this.http.get<any[]>(this.url+'departmentsforsupport/'+ id)
  }
  getdepartmentsforuserdata(id): Observable<any[]> {
    return this.http.get<any[]>(this.url+'departmentsforuser/'+ id)
  }

  getdepartmentsdata(id): Observable<any[]> {
    return this.http.get<any[]>(this.url+'departmentsdata/'+ id)
  }

  addDepartment(departmentdata,ministryid) {
   // alert(2 +' : '+ ministryid + departmentdata.Name);
    const formData = new FormData();
    formData.append('MinistryId', ministryid);
    formData.append('Name', departmentdata.Name);
    formData.append('NameEN', departmentdata.NameEN);
    formData.append('ShortnameEN', departmentdata.ShortnameEN);
    formData.append('ShortnameTH', departmentdata.ShortnameTH);
    return this.http.post<any>(this.url, formData);

  }

  deleteDepartment(id) {
    return this.http.delete(this.url + id);
  }

  editDepartment(departmentdata, id) {
    const formData = new FormData();
    formData.append('Name', departmentdata.Name);
    formData.append('NameEN', departmentdata.NameEN);
    formData.append('ShortnameEN', departmentdata.ShortnameEN);
    formData.append('ShortnameTH', departmentdata.ShortnameTH);
    return this.http.put<any>(this.url + id, formData);
  }
  
}
