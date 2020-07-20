import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProvincialDepartmentService {

  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/provincialdepartment/';
  }

  getprovincialdepartmentdata(id): Observable<any[]> {
    return this.http.get<any[]>(this.url + id)
  }

  getdetaildata(id): Observable<any[]> {
    return this.http.get<any[]>(this.url +'Getdetail/' + id)
  }
 
  addProvincialDepartment(departmentdata,DepartmentId) {
    //alert(2 +' : '+ DepartmentId + departmentdata.Name);
    const formData = new FormData();
    formData.append('DepartmentId', DepartmentId);
    formData.append('Name', departmentdata.Name);


    for (var i = 0; i < departmentdata.Province.length; i++) {
      formData.append('Province', departmentdata.Province[i]); 
    }
   

    return this.http.post(this.url, formData);

  }

  deleteProvincialDepartment(id) {
    return this.http.delete(this.url + id);
  }

  updateProvincialDepartment(departmentdata, id) {
   // alert(2 + ':' + id);
    const formData = new FormData();
    formData.append('Name', departmentdata.Name);
    for (var i = 0; i < departmentdata.Province.length; i++) {
      formData.append('Province', departmentdata.Province[i]); 
    }
    return this.http.put(this.url + id, formData);
  }
  
}
