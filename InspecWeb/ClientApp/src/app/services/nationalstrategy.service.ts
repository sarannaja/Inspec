import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class NationalstrategyService {
  url = "https://localhost:5001/api/nationalstrategy/";

  constructor(private http:HttpClient) { }
  getnationalstrategy(){
    return this.http.get(this.url)
  }
  addNationalstrategy(nationalstrategyData, file: FileList){
     alert(JSON.stringify(nationalstrategyData))
    const formData = new FormData();
    formData.append('title',nationalstrategyData.title);
    formData.append('link',nationalstrategyData.link);
    for (var iii = 0; iii < file.length; iii++) {
      formData.append("files", file[iii]);
    }
    return this.http.post(this.url, formData);
  }

  deleteNationalstrategy(id) {
    return this.http.delete(this.url + id);
  }

  editNationalstrategy(nationalstrategyData,id) {
    console.log(nationalstrategyData);

    const formData = new FormData();
    // alert(JSON.stringify(governmentinspectionplanData))
    formData.append('title',nationalstrategyData.title);
    formData.append('link',nationalstrategyData.link);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url+id, formData);
  }
}


