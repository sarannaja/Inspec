import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class InformationoperationService {
  url = "https://localhost:5001/api/informationoperation/";

  constructor(private http:HttpClient) { }
  getinformationoperation(){
    return this.http.get(this.url)
  }
  addInformationoperation(informationoperationData, file: FileList){
     alert(JSON.stringify(informationoperationData))
    const formData = new FormData();
    formData.append('location',informationoperationData.location);
    formData.append('name',informationoperationData.name);
    formData.append('detail',informationoperationData.detail);
    formData.append('tel',informationoperationData.tel);
    formData.append('province',informationoperationData.province);
    formData.append('district',informationoperationData.district);
    for (var iii = 0; iii < file.length; iii++) {
      formData.append("files", file[iii]);
    }
    return this.http.post(this.url, formData);
  }
  deleteInformationoperation(id) {
    return this.http.delete(this.url + id);
  }
  editInformationoperation(informationoperationData,id) {
    console.log(informationoperationData);

    const formData = new FormData();
    // alert(JSON.stringify(governmentinspectionplanData))
    formData.append('location',informationoperationData.location);
    formData.append('name',informationoperationData.name);
    formData.append('detail',informationoperationData.detail);
    formData.append('tel',informationoperationData.tel);
    formData.append('province',informationoperationData.province);
    formData.append('district',informationoperationData.district);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url+id, formData);
  }
}
