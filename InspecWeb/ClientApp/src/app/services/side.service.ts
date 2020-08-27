import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class SideService {
  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) 
  {
    this.url = baseUrl + 'api/side/';
  }
  getdata():Observable<any[]> {
    return this.http.get<any[]>(this.url)
  }
  store(data) {
    const formData = new FormData();
    formData.append('Name', data.Name);
    formData.append('NameEN', data.NameEN);
    formData.append('ShortnameEN', data.ShortnameEN);
    formData.append('ShortnameTH', data.ShortnameTH);
    return this.http.post(this.url, formData);
  }
  delete(id) {
    return this.http.delete(this.url + id);
  }
  update(data, id) {
    const formData = new FormData();
    formData.append('Name', data.Name);
    formData.append('NameEN', data.NameEN);
    formData.append('ShortnameEN', data.ShortnameEN);
    formData.append('ShortnameTH', data.ShortnameTH);
    return this.http.put(this.url + id, formData);
  }
}
