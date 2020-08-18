import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CircularLetterService {
  url = '';
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/circularletter/';
  }

  getdata(): Observable<any[]> {
    return this.http.get<any[]>(this.url)
  }

  //สำหรับเพิ่มข้อมูล
  store(data, file: FileList) {
    const formData = new FormData();
    formData.append('Title', data.Title);

    if(file != null){
      for (var iii = 0; iii < file.length; iii++) {
        formData.append("files", file[iii]);
      }     
    }
     return this.http.post<any>(this.url, formData);
  }
   //สำหรับแก้ไขข้อ
   update(data, file: FileList, id) {
    // alert(2)
     const formData = new FormData();
     formData.append('Id', id);
     formData.append('Title', data.Title);
    
     if(file != null){
       for (var iii = 0; iii < file.length; iii++) {
         formData.append("files", file[iii]);
       }
     }
     return this.http.put<any>(this.url, formData);
   }

  //ลบข้อมูล
  destroy(id) {
    return this.http.delete(this.url + id);
  }
}
