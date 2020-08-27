import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class NationalstrategyService {
  url = "";

  constructor(private http:HttpClient,@Inject('BASE_URL')  baseUrl: string) { 
    this.url = baseUrl + 'api/nationalstrategy/';
  }
  getnationalstrategy(){
    return this.http.get(this.url)
  }
  addNationalstrategy(nationalstrategyData, file: FileList){
     //alert(JSON.stringify(nationalstrategyData))
    const formData = new FormData();
    formData.append('Title',nationalstrategyData.title);
    for (var iii = 0; iii < file.length; iii++) {
      formData.append("files", file[iii]);
    }
    return this.http.post(this.url, formData);
  }

  deleteNationalstrategy(id) {
    return this.http.delete(this.url + id);
  }

  editNationalstrategy(nationalstrategyData,file: FileList,id,namefile) {
    const formData = new FormData();
    formData.append('Title',nationalstrategyData.title);
    formData.append('namefile',namefile);
    //alert(namefile);
    if(file != null){
      for (var iii = 0; iii < file.length; iii++) {
        formData.append("files", file[iii]);
       // alert(file[iii]); 
      }
    }
    return this.http.put(this.url+id, formData);
  }
  delete(id){
    return this.http.delete(this.url+id);
  }
}


