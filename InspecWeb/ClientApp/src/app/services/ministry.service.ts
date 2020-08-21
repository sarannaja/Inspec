import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class MinistryService {
  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) 
  {
    this.url = baseUrl + 'api/ministry/';
  }
  getministry():Observable<any[]> {
    return this.http.get<any[]>(this.url)
  }
  addMinistry(ministryData) {
    const formData = new FormData();
    formData.append('Name', ministryData.Name);
    formData.append('NameEN', ministryData.NameEN);
    formData.append('ShortnameEN', ministryData.ShortnameEN);
    formData.append('ShortnameTH', ministryData.ShortnameTH);
    return this.http.post(this.url, formData);
  }
  deleteMinistry(id) {
    return this.http.delete(this.url + id);
  }
  editMinistry(ministryData, id) {
    //console.log(ministryData);

    const formData = new FormData();
    formData.append('Name', ministryData.Name);
    formData.append('NameEN', ministryData.NameEN);
    formData.append('ShortnameEN', ministryData.ShortnameEN);
    formData.append('ShortnameTH', ministryData.ShortnameTH);
    return this.http.put(this.url + id, formData);
  }

   //20200720
  getministryfirst(id: any):Observable<any> {
    return this.http.get<any>(this.url + id)
  }
  //20200720
  getministryfirst2(id: any):Observable<any> {
    return this.http.get<any>(this.url+'ministryfirst2/'+ id)
  }

  getexcelministry():Observable<any>{
    alert(2);
    return this.http.get<any>(this.url+'excelministry')
  }
}
