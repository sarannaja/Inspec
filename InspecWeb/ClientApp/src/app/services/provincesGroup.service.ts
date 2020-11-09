import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class ProvincesGroupService {
  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) 
  {
    this.url = baseUrl + 'api/provincesgroup/';
  }

  getdata():Observable<any[]> {

    return this.http.get<any[]>(this.url)

  }
  
  store(data) {
    const formData = new FormData();
    formData.append('Name', data.Name);
   
    return this.http.post<any>(this.url, formData);
  }

  delete(id) {
    return this.http.delete(this.url + id);
  }
  update(data, id) {
    const formData = new FormData();
    formData.append('Name', data.Name);
    return this.http.put<any>(this.url + id, formData);
  }
}
