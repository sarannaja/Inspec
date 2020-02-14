import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GorvermentinspectionplanService {
  url = "https://localhost:5001/api/governmentinspectionplan/";

  constructor(private http:HttpClient) { }
  getgovernmentinspectionplan(){
    return this.http.get(this.url)
  }
  addGovernmentinspectionplan(governmentinspectionplanData){
     alert(JSON.stringify(governmentinspectionplanData))
    const formData = new FormData();
    formData.append('year',governmentinspectionplanData.year);
    formData.append('title',governmentinspectionplanData.title);
    return this.http.post(this.url, formData);
  }
  deleteGovernmentinspectionplan(id) {
    return this.http.delete(this.url + id);
  }
  editGovernmentinspectionplan(governmentinspectionplanData,id) {
    console.log(governmentinspectionplanData);

    const formData = new FormData();
    // alert(JSON.stringify(governmentinspectionplanData))
    formData.append('year', governmentinspectionplanData.year);
    formData.append('title',governmentinspectionplanData.title);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url+id, formData);
  }
}
