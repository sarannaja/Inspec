import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MinistryService {
  url = "https://localhost:5001/api/InstructionOrder/";

  constructor(private http:HttpClient) { }
  getInstructionOrder(){
    return this.http.get(this.url)
  }
  addInstructionOrder(InstructionOrderData){
    const formData = new FormData();
    formData.append('name',InstructionOrderData.InstructionOrdername);
    return this.http.post(this.url, formData);
  }
  deleteInstructionOrder(id) {
    return this.http.delete(this.url + id);
  }
  editInstructionOrder(InstructionOrderData,id) {
    console.log(InstructionOrderData);

    const formData = new FormData();
    formData.append('name', InstructionOrderData.InstructionOrdername);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url+id, formData);
  }
}
