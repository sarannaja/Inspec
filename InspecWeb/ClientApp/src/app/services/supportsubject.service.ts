import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SupportsubjectService {
  url = "";
  
  constructor(private http: HttpClient, @Inject('BASE_URL')baseUrl: string) { 
    this.url = baseUrl + 'api/subject/';
  }
  getsubjectdata(id) {
    return this.http.get(this.url + id)
  }

  getsubjectdetaildata(id) {
    return this.http.get(this.url + "subjectdetail/" + id)
    
    
  }
}
