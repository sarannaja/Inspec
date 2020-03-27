import { Injectable , Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CabineService {
  
  count = 0
  url= "";


  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/cabine/';
   }
   getcabine():Observable<any[]> {
    return this.http.get<any[]>(this.url)
   }
   addCabine(cabineData){
    alert(JSON.stringify(cabineData))
   const formData = new FormData();
   formData.append('name',cabineData.cabinename);
   formData.append('position',cabineData.cabineposition);
   return this.http.post(this.url, formData);
 }
 deleteCabine(id) {
  return this.http.delete(this.url + id);
}
editCabine(cabineData,id) {
  console.log(cabineData);

  const formData = new FormData();
  // alert(JSON.stringify(governmentinspectionplanData))
  formData.append('name', cabineData.cabinename);
  formData.append('position',cabineData.cabineposition);
  console.log('FORMDATA: ' + JSON.stringify(formData));
  return this.http.put(this.url+id, formData);
}
}
