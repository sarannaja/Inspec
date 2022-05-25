import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class InspectionorderService {
  url = "";
  count = 0
  constructor(private http:HttpClient, @Inject('BASE_URL') baseUrl: string) { 
    this.url = baseUrl + 'api/inspectionorder/';
  }
  getinspectionorder(){
    return this.http.get(this.url)
  }
  addInspectionorder(inspectionorderData, file: FileList){
    // alert("2");
    const formData = new FormData();
    formData.append('name',inspectionorderData.name);
    formData.append('year',inspectionorderData.year);
    formData.append('order',inspectionorderData.order);
    formData.append('createBy',inspectionorderData.createBy);

    if (file != null) {
      for (var iii = 0; iii < file.length; iii++) {
        formData.append("files", file[iii]);
      }
    }
    return this.http.post<any>(this.url, formData);
  }

  deleteInspectionorder(id) {
    return this.http.delete<any>(this.url + id);
  }

  editInspectionorder(inspectionorderData,file: FileList,id) {
    const formData = new FormData();
    formData.append('Name',inspectionorderData.name);
    formData.append('Year',inspectionorderData.year);
    formData.append('Order',inspectionorderData.order);
    formData.append('CreateBy',inspectionorderData.createBy);

    if (file != null) {
      for (var iii = 0; iii < file.length; iii++) {
        formData.append("files", file[iii]);
      }
    }
    return this.http.put<any>(this.url+id, formData);
  }
}


