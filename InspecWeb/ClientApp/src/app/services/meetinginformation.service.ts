import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MeetinginformationService {
  url = "https://localhost:5001/api/meetinginformation/";

  constructor(private http:HttpClient) { }
  getmeetinginformation(){
    return this.http.get(this.url)
  }
  addMeetinginformation(meetinginformationData, file: FileList){
     alert(JSON.stringify(meetinginformationData))
    const formData = new FormData();
    formData.append('title',meetinginformationData.title);
    formData.append('year',meetinginformationData.year);
    for (var iii = 0; iii < file.length; iii++) {
      formData.append("files", file[iii]);
    }
    return this.http.post(this.url, formData);
  }

  deleteMeetinginformation(id) {
    return this.http.delete(this.url + id);
  }

  editMeetinginformation(meetinginformationData,id) {
    console.log(meetinginformationData);

    const formData = new FormData();
    // alert(JSON.stringify(governmentinspectionplanData))
    formData.append('title',meetinginformationData.title);
    formData.append('year',meetinginformationData.year);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url+id, formData);
  }
}


