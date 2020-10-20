import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MenuService {

  count = 0
  url = "";
  testLoop(func) {
    console.log(`loop to ${func}`);
  }

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/menu/';
  }

  getmenulistdata(): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'getmenulistdata/')
  }
  getmenudata(id: any) {
    return this.http.get<any[]>(this.url + 'getmenu/' + id)
  }
  update(data: any[], id) {


    const formData = new FormData();
    for (let i = 0; i < data.length; i++) {
      console.log(data[i].menuname, data[i].status);
      
      formData.append(data[i].menuname, data[i].status)
    }
    // console.log(data);
    //return 
    // formData.append('m1', ); 
     return this.http.put(this.url+id, formData)
  }
}
