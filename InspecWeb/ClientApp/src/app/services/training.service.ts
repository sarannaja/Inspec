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

  gettrainingregistercountdata():Observable<any[]> {
    return this.http.get<any[]>(this.url + 'trainingregistercount')
  }

  getregistertrainingdata(id):Observable<any[]> {
    return this.http.get<any[]>(this.url + id)
  }


  getlisttrainingsurveydata(id):Observable<any[]> {
    return this.http.get<any[]>(this.url + 'listsurvey/' + id)
  }

  gettrainingsurveycountdata():Observable<any[]> {
    return this.http.get<any[]>(this.url + 'trainingsurveycount')
  }

  getdetailtraining(id):Observable<any[]> {
    return this.http.get<any[]>(this.url + 'detail/' + id)
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

  addTraining2(trainingData,file: FileList) {

    // alert(JSON.stringify(trainingData))
    const formData = new FormData();

    formData.append('Name', trainingData.name);
    formData.append('Detail', trainingData.detail);
    formData.append('StartDate', trainingData.start_date.date.year + '-' + trainingData.start_date.date.month + '-' + trainingData.start_date.date.day);
    formData.append('EndDate', trainingData.end_date.date.year + '-' + trainingData.end_date.date.month + '-' + trainingData.end_date.date.day);
    formData.append('LecturerName',trainingData.lecturer_name)
    formData.append('RegisStartDate', trainingData.start_date.date.year + '-' + trainingData.start_date.date.month + '-' + trainingData.start_date.date.day);
    formData.append('RegisEndDate', trainingData.start_date.date.year + '-' + trainingData.start_date.date.month + '-' + trainingData.start_date.date.day);
    // formData.append('image',"https://cdn.pixabay.com/photo/2019/06/28/03/07/camping-4303359_1280.jpg")
    // formData.append('subjects', centralpolicyData.subjects);
    // formData.append('files', "filetest.pdf");
    for(var i = 0; i < file.length; i++){
      formData.append('files', file[i]);
    }

    console.log('FORMDATA: ' + formData);
    return this.http.post(this.url, formData);
  }

  deleteTraining(id) {
    return this.http.delete(this.url + id);
  }

  //---------zone training register--------
  editRegisterList(trainingregisterlistData,id) {
    console.log(trainingregisterlistData);

    const formData = new FormData();
    formData.append('status', trainingregisterlistData.approve);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url + 'registerlist/' + id , formData);
  }

  //insert training register font-end
  addTrainingRegister(trainingData, trainingid) {
    alert('Service:' + JSON.stringify(trainingData))
    //alert(trainingid)
    const formData = new FormData();
    formData.append('name', trainingData.name);
    //formData.append('name', trainingData.cardid);
    formData.append('position', trainingData.position);
    formData.append('phone', trainingData.phone);
    //formData.append('name', trainingData.email);

    console.log('FORMDATA: ' + formData);
    return this.http.post(this.url + 'trainingregister/' + trainingid , formData);
  }

  //-------------------------------------
  //---------zone training survey--------
  //insert training survey list
  addTrainingsurvey(trainingData, trainingid) {
    //alert(JSON.stringify(trainingData))
    alert(trainingid)
    const formData = new FormData();
    formData.append('name', trainingData.name);

    console.log('FORMDATA: ' + formData);
    return this.http.post(this.url + 'trainingsurvey/' + trainingid , formData);
  }

  deleteTrainingSurvey(trainingid) {
    return this.http.delete(this.url + 'trainingsurvey/' + trainingid);
  }
  //-------------------------------------
  //--------zone training document-------
  gettrainingviewdocumentdata():Observable<any[]> {
    return this.http.get<any[]>(this.url + 'trainingdocumentcount')
  }

  getlisttrainingdocumentdata(trainingid):Observable<any[]> {
    return this.http.get<any[]>(this.url + 'listdocument/' + trainingid)
  }

  deleteTrainingDocument(trainingid) {
    return this.http.delete(this.url + 'deletedocument/' + trainingid);
  }

  addTrainingDocument(trainingData,file: FileList) {

    //alert('service:' + JSON.stringify(trainingData))
    const formData = new FormData();
    formData.append('detail', trainingData.detail);

    for(var i = 0; i < file.length; i++){
      formData.append('files', file[i]);
    }

    console.log('FORMDATA: ' + formData);
    return this.http.post(this.url + 'insertdocument/', formData);
  }
  //-------------------------------------
}
