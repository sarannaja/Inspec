import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PremierorderService {
  url = "";
  
  constructor(private http:HttpClient, @Inject('BASE_URL') baseUrl: string) { 
    this.url = baseUrl + 'api/premierorder/';
  }
  getpremierorder(){
    return this.http.get(this.url)
  }
  addPremierorder(premierorderData, file: FileList){
     //alert(JSON.stringify(premierorderData))
    const formData = new FormData();
    formData.append('title',premierorderData.title);
    formData.append('year',premierorderData.year);
    for (var iii = 0; iii < file.length; iii++) {
      formData.append("files", file[iii]);
    }
    return this.http.post(this.url, formData);
  }

  deletePremierorder(id) {
    return this.http.delete(this.url + id);
  }

  editPremierorder(premierorderData,id) {
   // console.log(premierorderData);

    const formData = new FormData();
    // alert(JSON.stringify(governmentinspectionplanData))
    formData.append('title',premierorderData.title);
    formData.append('year',premierorderData.year);
    //console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url+id, formData);
  }
}


