import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class WordService {

  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/word';
  }
  exportWord(id , provinId ,  elecId) {
    // alert(id)

    const formData = {
      id: id,
      ProvinId: provinId,
      elecId : elecId,
     // EndDate : EndWork ,
      // Target : value.Target
    }

    console.log("formData" , formData);
    return this.http.post<any>(this.url , formData)
  }

  exportExcel() {
    return this.http.get(this.url + "/Excel")
  }
}
