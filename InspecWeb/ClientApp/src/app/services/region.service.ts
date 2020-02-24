import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RegionService {

  count = 0
  url = "";


  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) 
  { 
    this.url = baseUrl + 'api/region/';
  }
  getregiondata():Observable<any[]> {
    return this.http.get<any[]>(this.url)
  }
  addRegion(regionData) {
    const formData = new FormData();
    formData.append('name', regionData.regionname);
    console.log('FORMDATA: ' + formData);
    return this.http.post(this.url, formData);
  }
  deleteRegion(id) {
    return this.http.delete(this.url + id);
  }
  editRegion(regionData,id) {
    console.log(regionData);

    const formData = new FormData();
    formData.append('name', regionData.regionname);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url+id, formData);
  }
}
