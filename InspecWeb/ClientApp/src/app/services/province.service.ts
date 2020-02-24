import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProvinceService {

  count = 0
  url = "";


  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string)
  {
    this.url = baseUrl + 'api/province/';
  }
  getprovincedata():Observable<any[]> {
    return this.http.get<any[]>(this.url)
  }
  addProvince(provinceData) {
    const formData = new FormData();
    formData.append('name', provinceData.provincename);
    formData.append('link', provinceData.provincelink)
    console.log('FORMDATA: ' + formData.get("name"));
    return this.http.post(this.url, formData);
  }
  deleteProvince(id) {
    return this.http.delete(this.url + id);
  }
  editProvince(provinceData,id) {
    console.log(provinceData);

    const formData = new FormData();
    formData.append('name', provinceData.provincename);
    formData.append('link', provinceData.provincelink)
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url+id, formData);
  }
}
