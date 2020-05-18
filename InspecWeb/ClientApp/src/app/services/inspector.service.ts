import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class InspectorService {
  url = "";
  files: FileList

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/inspector/';
  }
  
  getinspectordata(): Observable<any[]> {
    return this.http.get<any[]>(this.url)
  }
  addInspector(inspectorData) {
    alert(JSON.stringify(inspectorData))
    const formData = new FormData();
    formData.append('name',inspectorData.name);
    formData.append('phonenumber',inspectorData.phonenumber);
    formData.append('regionId',inspectorData.regionId);
    return this.http.post(this.url, formData);
  }
  deleteInspector(id) {
    return this.http.delete(this.url + id);
  }
  editInspector(inspectorData,id) {
    console.log(inspectorData);

    const formData = new FormData();
    formData.append('name',inspectorData.name);
    formData.append('phonenumber',inspectorData.phonenumber);
    formData.append('regionId',inspectorData.regionId);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url+id, formData);
  }
}
