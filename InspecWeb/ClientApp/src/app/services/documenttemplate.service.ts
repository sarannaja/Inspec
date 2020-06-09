import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DocumenttemplateService {
  url = "https://localhost:5001/api/documenttemplate/";

  constructor(private http:HttpClient) { }
  getdocumenttemplate(){
    return this.http.get(this.url)
  }
  addDocumenttemplate(documenttemplateData, file: FileList){
     alert(JSON.stringify(documenttemplateData))
    const formData = new FormData();
    formData.append('title',documenttemplateData.title);
    formData.append('year',documenttemplateData.year);
    for (var iii = 0; iii < file.length; iii++) {
      formData.append("files", file[iii]);
    }
    return this.http.post(this.url, formData);
  }

  deleteDocumenttemplate(id) {
    return this.http.delete(this.url + id);
  }

  editDocumenttemplate(documenttemplateData,id) {
    console.log(documenttemplateData);

    const formData = new FormData();
    // alert(JSON.stringify(governmentinspectionplanData))
    formData.append('title',documenttemplateData.title);
    formData.append('year',documenttemplateData.year);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url+id, formData);
  }
}


