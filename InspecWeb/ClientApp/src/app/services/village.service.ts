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
    console.log(id);
    // alert("fdfdf");
    return this.http.get<any[]>(this.url+id)
  }
}
