import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DistrictService {

  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { 
    this.url = baseUrl + 'api/district/';
  }
  getdistrictdata(id) {
    console.log(id);
    return this.http.get(this.url+id)
  }

  adddistrict(districtData,provinceId) {
     const formData = new FormData();
     formData.append('ProvincesId', provinceId);
     formData.append('Name', districtData.Name);
     return this.http.post(this.url, formData);
   }
   deletedistrict(id) {
     return this.http.delete(this.url + id);
   }
   editdistrict(districtData,id) {
     const formData = new FormData();
     formData.append('Name', districtData.Name);
    
     return this.http.put(this.url+id, formData);
   }

   worddistrict(id):Observable<any>{
    return this.http.get<any>(this.url + "worddistrict/" + id)
  }

}
