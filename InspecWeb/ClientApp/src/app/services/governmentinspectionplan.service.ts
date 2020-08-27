import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})

export class GorvermentinspectionplanService {
  // url = "https://localhost:5001/api/governmentinspectionplan/";
  url = "";

  constructor(private http:HttpClient,@Inject('BASE_URL')  baseUrl: string) { 
    this.url = baseUrl + 'api/governmentinspectionplan/';
  }


  getgovernmentinspectionplan(){
    return this.http.get(this.url)
  }
  addGovernmentinspectionplan(governmentinspectionplanData, file: FileList){
    // alert(JSON.stringify(governmentinspectionplanData))
    const formData = new FormData();
    formData.append('year',governmentinspectionplanData.year);
    formData.append('title',governmentinspectionplanData.title);
    if(file != null){
      for (var iii = 0; iii < file.length; iii++) {
        formData.append("files", file[iii]);
      }
    }
    return this.http.post(this.url, formData);
  }
  deleteGovernmentinspectionplan(id) {
    return this.http.delete(this.url + id);
  }
  editGovernmentinspectionplan(governmentinspectionplanData, file: FileList,id,filesname) {

    const formData = new FormData();
    formData.append('year', governmentinspectionplanData.year);
    formData.append('title',governmentinspectionplanData.title);
    formData.append('filesname',filesname);
    if(file != null){
      for (var iii = 0; iii < file.length; iii++) {
        formData.append("files", file[iii]);
        //alert(file[iii]);
      }
    }
    return this.http.put(this.url+id, formData);
  }
}
