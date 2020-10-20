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
     //alert(JSON.stringify(inspectionorderData))
    const formData = new FormData();
    formData.append('name',inspectionorderData.name);
    formData.append('year',inspectionorderData.year);
    formData.append('order',inspectionorderData.order);
    formData.append('createBy',inspectionorderData.createBy);
    for (var iii = 0; iii < file.length; iii++) {
      formData.append("files", file[iii]);
    }
    return this.http.post(this.url, formData);
  }

  deleteInspectionorder(id) {
    return this.http.delete(this.url + id);
  }

  editInspectionorder(inspectionorderData,id) {
    console.log(inspectionorderData);

    const formData = new FormData();
    // alert(JSON.stringify(governmentinspectionplanData))
    formData.append('name',inspectionorderData.name);
    formData.append('year',inspectionorderData.year);
    formData.append('order',inspectionorderData.order);
    formData.append('createBy',inspectionorderData.createBy);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url+id, formData);
  }
}


