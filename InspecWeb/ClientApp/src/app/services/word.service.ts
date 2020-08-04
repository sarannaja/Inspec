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
  exportWord(id ,   elecId) {
    // alert(elecId)

    const formData = {
      id: id,
      // ProvinId: 1,
      elecId : elecId,
     // EndDate : EndWork ,
      // Target : value.Target
    }

  //  alert(JSON.stringify(formData))
    return this.http.post<any>(this.url , formData)
  }

  exportExcel() {
    return this.http.get(this.url + "/Excel")
  }
}
