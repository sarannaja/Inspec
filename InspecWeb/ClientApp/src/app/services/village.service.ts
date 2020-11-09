import { Injectable,Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VillageService {
  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { 
    this.url = baseUrl + 'api/village/';
  }
  getvillagedata(id):Observable<any[]> {
    return this.http.get<any[]>(this.url+id)
  }
  add(Data,SubdistrictId) {
    const formData = new FormData();
    formData.append('SubdistrictId', SubdistrictId);
    formData.append('Name', Data.Name);
    return this.http.post<any>(this.url, formData);
  }
  delete(id) {
    return this.http.delete(this.url + id);
  }
  edit(Data,id) {
    const formData = new FormData();
    formData.append('Name', Data.Name);
   
    return this.http.put<any>(this.url+id, formData);
  }
}
