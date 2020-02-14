import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class SubdistrictService {

  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { 
    this.url = baseUrl + 'api/subdistrict/';
  }
  getsubdistrictdata(id) {
    console.log(id);
    
    return this.http.get(this.url+id)
  }
}
