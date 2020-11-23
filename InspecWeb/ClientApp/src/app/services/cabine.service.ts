import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CabineService {
  url = "";
  files: FileList;
  constructor(private http: HttpClient,  @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/cabine/';
   }

  getcabine() {
    return this.http.get(this.url)
  }
  getcabineministry(id): Observable<any[]> {
    return this.http.get<any[]>(this.url + id)
  }
  addCabine(cabineData, file: FileList) {
    const formData = new FormData();
    formData.append('Prefix', cabineData.prefix);
    formData.append('Name', cabineData.name);
    formData.append('Position', cabineData.position);
    formData.append('Detail', cabineData.detail);

    if(file != null){
      for (var iii = 0; iii < file.length; iii++) {
        formData.append("files", file[iii]);
      }
    }else {
      formData.append("files", null);
    }
    formData.append('Commandnumber', cabineData.Commandnumber);
    formData.append('cabinet', cabineData.cabinet);
    formData.append('tel', cabineData.tel);
    formData.append('MinistryId', cabineData.MinistryId);

    return this.http.post<any>(this.url, formData);
  }
  deleteCabine(id) {
    return this.http.delete(this.url + id);
  }
  editCabine(cabineData,file:FileList,filename, id) {
    const formData = new FormData();
    formData.append('Prefix', cabineData.prefix);
    formData.append('Name', cabineData.name);
    formData.append('Position', cabineData.position);
    formData.append('Detail', cabineData.detail);

    if(file != null){
      for (var iii = 0; iii < file.length; iii++) {
        formData.append("files", file[iii]);
      }
    }else {
      formData.append("files", null);
    }
    formData.append('Commandnumber', cabineData.Commandnumber);
    formData.append('cabinet', cabineData.cabinet);
    formData.append('tel', cabineData.tel);
    formData.append('MinistryId', cabineData.MinistryId);
    formData.append('Filename', filename);
    return this.http.put<any>(this.url + id, formData);
  }
}
