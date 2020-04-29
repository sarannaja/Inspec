import { Injectable , Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CabineService {
    url = "https://localhost:5001/api/cabine/";
    files: FileList;
    constructor(private http:HttpClient) { }
   getcabine(){
    return this.http.get(this.url)
  }
   addCabine(cabineData, file: FileList){
     //alert(JSON.stringify(cabineData))
   const formData = new FormData();
   formData.append('Prefix',cabineData.prefix);
   formData.append('Name',cabineData.name);
   formData.append('Position',cabineData.position);
   formData.append('Detail',cabineData.detail);
   for (var iii = 0; iii < file.length; iii++) {
    formData.append("files", file[iii]);
  }
   return this.http.post(this.url, formData);
 }
 deleteCabine(id) {
  return this.http.delete(this.url + id);
}
editCabine(cabineData,id) {
  console.log(cabineData);
  const formData = new FormData();
  //alert(JSON.stringify(cabineData))
  formData.append('Prefix',cabineData.prefix);
  formData.append('Name',cabineData.name);
  formData.append('Position',cabineData.position);
  formData.append('Detail',cabineData.detail);
  for (var iii = 0; iii < File.length; iii++) {
    formData.append("files", File[iii]);
  }
  
  console.log('FORMDATA: ' + JSON.stringify(formData));
  return this.http.put(this.url+id, formData);
}
}
