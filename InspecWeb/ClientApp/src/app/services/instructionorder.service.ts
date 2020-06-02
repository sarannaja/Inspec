import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class InstructionorderService {
  url = "https://localhost:5001/api/instructionorder/";

  constructor(private http:HttpClient) { }
  getinstructionorder(){
    return this.http.get(this.url)
  }
  addInstructionorder(instructionorderData, file: FileList){
     alert(JSON.stringify(instructionorderData))
    const formData = new FormData();
    formData.append('name',instructionorderData.name);
    formData.append('year',instructionorderData.year);
    formData.append('order',instructionorderData.order);
    formData.append('createBy',instructionorderData.createBy);
    formData.append('detail',instructionorderData.detail);
    for (var iii = 0; iii < file.length; iii++) {
      formData.append("files", file[iii]);
    }
    return this.http.post(this.url, formData);
  }

  deleteInstructionorder(id) {
    return this.http.delete(this.url + id);
  }

  editInstructionorder(instructionorderData,id) {
    console.log(instructionorderData);

    const formData = new FormData();
    // alert(JSON.stringify(governmentinspectionplanData))
    formData.append('name',instructionorderData.name);
    formData.append('year',instructionorderData.year);
    formData.append('order',instructionorderData.order);
    formData.append('createBy',instructionorderData.createBy);
    formData.append('detail',instructionorderData.detail);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url+id, formData);
  }
}


