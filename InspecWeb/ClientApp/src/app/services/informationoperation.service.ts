import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class InformationoperationService {
  url = "";

  constructor(private http:HttpClient,@Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/informationoperation/';
   }
  getinformationoperation(){
    return this.http.get(this.url)
  }
  addInformationoperation(informationoperationData, file: FileList){
     //alert(JSON.stringify(informationoperationData))
    const formData = new FormData();
    formData.append('location',informationoperationData.location);
    formData.append('name',informationoperationData.name);
    formData.append('detail',informationoperationData.detail);
    formData.append('tel',informationoperationData.tel);
    formData.append('province',informationoperationData.province);
    formData.append('district',informationoperationData.district);
    if(file != null){
      for (var iii = 0; iii < file.length; iii++) {
        formData.append("files", file[iii]);
      }
    }

    return this.http.post<any>(this.url, formData);
  }
  deleteInformationoperation(id) {
    return this.http.delete(this.url + id);
  }
  editInformationoperation(informationoperationData,file: FileList,id) {
    //console.log(informationoperationData);

    const formData = new FormData();
    // alert(JSON.stringify(governmentinspectionplanData))
    formData.append('Id',id);
    formData.append('Location',informationoperationData.location);
    formData.append('Name',informationoperationData.name);
    formData.append('Detail',informationoperationData.detail);
    formData.append('Tel',informationoperationData.tel);
    formData.append('Province',informationoperationData.province);
    formData.append('District',informationoperationData.district);
    if(file != null){
      for (var iii = 0; iii < file.length; iii++) {
        formData.append("files", file[iii]);
      }
    }
    return this.http.put<any>(this.url, formData);
  }
}
