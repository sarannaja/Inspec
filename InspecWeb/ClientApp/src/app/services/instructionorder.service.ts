import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class InstructionorderService {

  url = "";
  count = 0
  constructor(private http:HttpClient, @Inject('BASE_URL') baseUrl: string) { 
    this.url = baseUrl + 'api/instructionorder/';
  }
  getinstructionorder(){
    return this.http.get(this.url)
  }
  addInstructionorder(instructionorderData, file: FileList){
    // alert(JSON.stringify(instructionorderData))
    const formData = new FormData();
    formData.append('name',instructionorderData.name);
    formData.append('year',instructionorderData.year);
    formData.append('order',instructionorderData.order);
    formData.append('createBy',instructionorderData.createBy);
    formData.append('detail',instructionorderData.detail);

    if (file != null) {
      for (var iii = 0; iii < file.length; iii++) {
        formData.append("files", file[iii]);
      }
    }

    return this.http.post(this.url, formData);
  }

  deleteInstructionorder(id) {
    return this.http.delete(this.url + id);
  }

  editInstructionorder(instructionorderData,file: FileList,id,filename) {
    const formData = new FormData();
   // alert(JSON.stringify(instructionorderData));
    formData.append('Name',instructionorderData.name);
    formData.append('Year',instructionorderData.year);
    formData.append('Order',instructionorderData.order);
    formData.append('Detail',instructionorderData.detail);
    formData.append('CreateBy',instructionorderData.createBy);
    formData.append('Filename',filename);
    if (file != null) {
      for (var iii = 0; iii < file.length; iii++) {
        formData.append("files", file[iii]);
      }
    }

    return this.http.put(this.url+id, formData);
  }
}


