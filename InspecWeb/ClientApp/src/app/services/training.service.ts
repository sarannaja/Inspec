import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TrainingService {

  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/training/';
  }
  gettrainingdata():Observable<any[]> {
    return this.http.get<any[]>(this.url)
  }
  addTraining(trainingData) {

    // alert(JSON.stringify(trainingData))
    const formData = new FormData();

    formData.append('name', trainingData.name);
    formData.append('detail', trainingData.detail);
    formData.append('start_date', trainingData.start_date.date.year + '-' + trainingData.start_date.date.month + '-' + trainingData.start_date.date.day);
    formData.append('end_date', trainingData.end_date.date.year + '-' + trainingData.end_date.date.month + '-' + trainingData.end_date.date.day);
    formData.append('lecturer_name',trainingData.lecturer_name)
    formData.append('regis_start_date', trainingData.start_date.date.year + '-' + trainingData.start_date.date.month + '-' + trainingData.start_date.date.day);
    formData.append('regis_end_date', trainingData.start_date.date.year + '-' + trainingData.start_date.date.month + '-' + trainingData.start_date.date.day);
    formData.append('image',"https://cdn.pixabay.com/photo/2019/06/28/03/07/camping-4303359_1280.jpg")
    // formData.append('subjects', centralpolicyData.subjects);
    // formData.append('files', "filetest.pdf");

    console.log('FORMDATA: ' + formData);
    return this.http.post(this.url, formData);
  }
  deleteTraining(id) {
    return this.http.delete(this.url + id);
  }
}
