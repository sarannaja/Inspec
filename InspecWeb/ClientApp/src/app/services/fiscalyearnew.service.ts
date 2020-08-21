import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class FiscalyearnewService {
  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) 
  {
    this.url = baseUrl + 'api/fiscalyearnew/';
  }
  getdata():Observable<any[]> {
    return this.http.get<any[]>(this.url)
  }
  store(data) {
    // alert(2 + '' + data.Year);
    const formData = new FormData();
    formData.append('Year', data.Year);

    if(data.Startdate != null){
      formData.append('Startdate', data.Startdate.date.year + '-' + data.Startdate.date.month + '-' + data.Startdate.date.day);
    }else{
      formData.append('Startdate',null)
    }

    if(data.Enddate != null){
      formData.append('Enddate', data.Enddate.date.year + '-' + data.Enddate.date.month + '-' + data.Enddate.date.day);
      }else{
        formData.append('Enddate',null)
      }
    return this.http.post(this.url, formData);
  }
  delete(id) {
    return this.http.delete(this.url + id);
  }
  update(data, id) {
    const formData = new FormData();
    formData.append('Year', data.Year);

    if(data.Startdate != null){
      formData.append('Startdate', data.Startdate.date.year + '-' + data.Startdate.date.month + '-' + data.Startdate.date.day);
    }else{
      formData.append('Startdate',null)
    }

    if(data.Enddate != null){
      formData.append('Enddate', data.Enddate.date.year + '-' + data.Enddate.date.month + '-' + data.Enddate.date.day);
      }else{
        formData.append('Enddate',null)
      }
  
    return this.http.put(this.url + id, formData);
  }

}
