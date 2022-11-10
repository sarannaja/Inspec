import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TrainingRegisterlist } from './toeymodel/trainingregisterlist';
import { Ngong } from '../training/plan-training/plan-training.component';
import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';

@Injectable({
  providedIn: 'root'
})
export class TrainingService {

  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/training/';
  }
  gettrainingdata(): Observable<any[]> {
    return this.http.get<any[]>(this.url)
  }

  gettrainingdataShowPage(): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'ShowPage')
  }

  gettrainingregistercountdata(): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'trainingregistercount')
  }

  getregistertrainingdata(id): Observable<any[]> {
    return this.http.get<any[]>(this.url + id)
  }

  getregistertrainingdataApprove(id): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'approve/get/' + id)
  }

  getregistertrainingpeopledata(id): Observable<any> {
    return this.http.get<any>(this.url + 'peopledetail/' + id)
  }

  getlisttrainingsurveydata(id): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'listsurvey/' + id)
  }

  gettrainingsurveycountdata(): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'trainingsurveycount')
  }

  getdetailtraining(id): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'detail/' + id)
  }

  addTraining(trainingData) {

    // alert(JSON.stringify(trainingData))
    const formData = new FormData();

    formData.append('name', trainingData.name);
    formData.append('detail', trainingData.detail);
    formData.append('start_date', trainingData.start_date.date.year + '-' + trainingData.start_date.date.month + '-' + trainingData.start_date.date.day);
    formData.append('end_date', trainingData.end_date.date.year + '-' + trainingData.end_date.date.month + '-' + trainingData.end_date.date.day);
    formData.append('lecturer_name', trainingData.lecturer_name)
    formData.append('regis_start_date', trainingData.start_date.date.year + '-' + trainingData.start_date.date.month + '-' + trainingData.start_date.date.day);
    formData.append('regis_end_date', trainingData.start_date.date.year + '-' + trainingData.start_date.date.month + '-' + trainingData.start_date.date.day);
    formData.append('image', "https://cdn.pixabay.com/photo/2019/06/28/03/07/camping-4303359_1280.jpg")
    // formData.append('subjects', centralpolicyData.subjects);
    // formData.append('files', "filetest.pdf");

    console.log('FORMDATA: ' + formData);
    return this.http.post(this.url, formData);
  }

  addTraining2(trainingData, file: FileList) {

    // alert(JSON.stringify(trainingData))
    const formData = new FormData();

    formData.append('Name', trainingData.name);
    formData.append('Detail', trainingData.detail);
    formData.append('Generation', trainingData.generation);
    formData.append('Year', trainingData.year);
    formData.append('CourseCode', trainingData.coursecode);
    formData.append('StartDate', trainingData.start_date.date.year + '-' + trainingData.start_date.date.month + '-' + trainingData.start_date.date.day);
    formData.append('EndDate', trainingData.end_date.date.year + '-' + trainingData.end_date.date.month + '-' + trainingData.end_date.date.day);
    formData.append('RegisStartDate', trainingData.regis_start_date.date.year + '-' + trainingData.regis_start_date.date.month + '-' + trainingData.regis_start_date.date.day);
    formData.append('RegisEndDate', trainingData.regis_end_date.date.year + '-' + trainingData.regis_end_date.date.month + '-' + trainingData.regis_end_date.date.day);
    for (var i = 0; i < file.length; i++) {
      formData.append('files', file[i]);
    }

    console.log('FORMDATA: ' + formData);
    return this.http.post<any>(this.url, formData);
  }

  editTraining(trainingData, id, files: FileList) {
    console.log(trainingData);

    const formData = new FormData();
    formData.append('Name', trainingData.name);
    formData.append('Detail', trainingData.detail);
    formData.append('Generation', trainingData.generation);
    formData.append('Year', trainingData.year);
    formData.append('CourseCode', trainingData.coursecode);
    formData.append('StartDate', trainingData.start_date.year + '-' + trainingData.start_date.month + '-' + trainingData.start_date.day);
    formData.append('EndDate', trainingData.end_date.year + '-' + trainingData.end_date.month + '-' + trainingData.end_date.day);
    formData.append('RegisStartDate', trainingData.regis_start_date.year + '-' + trainingData.regis_start_date.month + '-' + trainingData.regis_start_date.day);
    formData.append('RegisEndDate', trainingData.regis_end_date.year + '-' + trainingData.regis_end_date.month + '-' + trainingData.regis_end_date.day);

    if (files != null) {
      for (var index = 0; index < files.length; index++) {
        formData.append("files", files[index]);
      }
    }

    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put<any>(this.url + 'maintraining/edit/' + id, formData);
  }

  deleteTraining(id) {
    return this.http.delete(this.url + id);
  }

  //---------zone training register--------
  editRegisterList(trainingregisterlistData, id, trainingid) {
    console.log("editRegisterList =>", trainingregisterlistData);

    // const formData = new FormData();
    // formData.append('status', trainingregisterlistData.approve);
    // console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.get(this.url + 'registerlist/' + id + '/' + trainingid + '/' + trainingregisterlistData.approve);
  }

  editRegisterList2(trainingregisterlistData, id, trainingid) {
    console.log(trainingregisterlistData);

    const formData = {
      status: trainingregisterlistData.approve,
      traningregisterid: id
    }
    // formData.append('status', trainingregisterlistData.approve);


    // for (var iii = 0; iii < id.length; iii++) {
    //   formData.append("traningregisterid", id[iii]);
    // }

    // const formData = {
    //   status: parseInt(trainingregisterlistData.approve),
    //   traningregisterid: id,
    // }
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.post(this.url + 'registerlist2/' + trainingid, formData);
  }

  editRegisterConditionList(trainingregisterlistData, id) {

    // const formData = new FormData();
    const formData = {
      traningregistercondition: trainingregisterlistData,
      traningregisterid: id
    }

    // formData.append('traningregisterid', id);

    // for (var iii = 0; iii < trainingregisterlistData.length; iii++) {
    //   formData.append("traningregistercondition", trainingregisterlistData[iii]);
    // }

    // console.log('FORMDATA: ', (formData.get("traningregistercondition").toString()));
    return this.http.put(this.url + 'editRegisterConditionList', formData);
  }

  //Update Group
  editRegisterGroup(trainingregisterlistData, id) {
    // console.log("trainingregisterlistData",trainingregisterlistData)
    // alert(trainingregisterlistData.approve1)
    // console.log(trainingregisterlistData);

    const formData = new FormData();

    if (trainingregisterlistData.approve1 == null) {
      trainingregisterlistData.approve1 = 0;
    }
    if (trainingregisterlistData.approve2 == null) {
      trainingregisterlistData.approve2 = 0;
    }
    if (trainingregisterlistData.approve3 == null) {
      trainingregisterlistData.approve3 = 0;
    }
    if (trainingregisterlistData.approve4 == null) {
      trainingregisterlistData.approve4 = 0;
    }
    if (trainingregisterlistData.approve5 == null) {
      trainingregisterlistData.approve5 = 0;
    }
    if (trainingregisterlistData.approve6 == null) {
      trainingregisterlistData.approve6 = 0;
    }
    if (trainingregisterlistData.approve7 == null) {
      trainingregisterlistData.approve7 = 0;
    }
    if (trainingregisterlistData.approve8 == null) {
      trainingregisterlistData.approve8 = 0;
    }
    if (trainingregisterlistData.approve9 == null) {
      trainingregisterlistData.approve9 = 0;
    }
    if (trainingregisterlistData.approve10 == null) {
      trainingregisterlistData.approve10 = 0;
    }

    for (var index = 0; index < trainingregisterlistData.length; index++) {
      // if (trainingregisterlistData[index].phaseNo == 1) {
      //   trainingregisterlistData.approve1 = trainingregisterlistData[index].value;
      // }
      trainingregisterlistData['approve' + trainingregisterlistData[index].phaseNo] = trainingregisterlistData[index].value;
    }
    // alert(trainingregisterlistData.approve1)
    formData.append('approve1', trainingregisterlistData.approve1);
    formData.append('approve2', trainingregisterlistData.approve2);
    formData.append('approve3', trainingregisterlistData.approve3);
    formData.append('approve4', trainingregisterlistData.approve4);
    formData.append('approve5', trainingregisterlistData.approve5);
    formData.append('approve6', trainingregisterlistData.approve6);
    formData.append('approve7', trainingregisterlistData.approve7);
    formData.append('approve8', trainingregisterlistData.approve8);
    formData.append('approve9', trainingregisterlistData.approve9);
    formData.append('approve10', trainingregisterlistData.approve10);

    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url + 'register/group/' + id, formData);
  }

  //insert training register font-end
  addTrainingRegister(trainingData, trainingid, files: FileList, CertificationFiles: FileList, idcardFiles: FileList, GovernmentpassportFiles: FileList, userid, username, departmentid) {
    //alert('Service:' + JSON.stringify(trainingData))
    //alert(trainingid)
    const formData = new FormData();
    formData.append('departmentid', departmentid);
    formData.append('username', username);
    formData.append('userid', userid);
    formData.append('trainingid', trainingid);
    formData.append('name', trainingData.name);
    formData.append('cardid', trainingData.cardid);
    formData.append('position', trainingData.position);

    formData.append('positionbefore', trainingData.positionbefore);
    formData.append('passportstatus', trainingData.passportstatus);
    if (trainingData.passportexpire != null) {
      formData.append('passportexpire', trainingData.passportexpire.date.year + '-' + trainingData.passportexpire.date.month + '-' + trainingData.passportexpire.date.day);
    }
    else{
      formData.append('passportexpire', null);
    }
    
    formData.append('address', trainingData.address);
    formData.append('officephone', trainingData.officephone);
    formData.append('religion', trainingData.religion);
    formData.append('Food', trainingData.Food);
    formData.append('foodallergy', trainingData.foodallergy);
    formData.append('blood', trainingData.blood);
    formData.append('congenitaldisease', trainingData.congenitaldisease);
    formData.append('collaboratorposition', trainingData.collaboratorposition);

    formData.append('department', trainingData.department);
    formData.append('phone', trainingData.phone);
    formData.append('email', trainingData.email);

    formData.append('type', trainingData.type);
    formData.append('nickname', trainingData.nickname);

    if(trainingData.retireddate != null){
      formData.append('retireddate', trainingData.retireddate.date.year + '-' + trainingData.retireddate.date.month + '-' + trainingData.retireddate.date.day);
    }
    else{
      formData.append('retireddate', null)
    }
    
    formData.append('birthdate', trainingData.birthdate.date.year + '-' + trainingData.birthdate.date.month + '-' + trainingData.birthdate.date.day);

    formData.append('officeaddress', trainingData.officeaddress);
    formData.append('fax', trainingData.fax);
    formData.append('collaboratorname', trainingData.collaboratorname);
    formData.append('collaboratorphone', trainingData.collaboratorphone);
    formData.append('collaboratorphoneoffice', trainingData.collaboratorphoneoffice);
    formData.append('collaboratoremail', trainingData.collaboratoremail);

    if (files != null) {
      for (var iii = 0; iii < files.length; iii++) {
        formData.append("files", files[iii]);
      }
    }
    if (CertificationFiles != null) {
      for (var index = 0; index < CertificationFiles.length; index++) {
        formData.append("CertificationFiles", CertificationFiles[index]);
      }
    }
    if (idcardFiles != null) {
      for (var index = 0; index < idcardFiles.length; index++) {
        formData.append("idcardFiles", idcardFiles[index]);
      }
    }
    if (GovernmentpassportFiles != null) {
      for (var index = 0; index < GovernmentpassportFiles.length; index++) {
        formData.append("GovernmentpassportFiles", GovernmentpassportFiles[index]);
      }
    }

    console.log('FORMDATA: ' + formData);
    return this.http.post(this.url + 'trainingregister/' + trainingid, formData);
  }

  //-------------------------------------




  //---------zone training survey--------


  //insert training survey topic
  addTrainingsurveytopic(trainingData) {
    //alert(JSON.stringify(trainingData))
    //alert(trainingid)
    const formData = new FormData();
    formData.append('name', trainingData.name);

    console.log('FORMDATA: ' + formData);
    return this.http.post<any>(this.url + 'trainingsurveytopic/add', formData);
  }

  editTrainingsurveytopic(trainingData, id) {
    console.log(trainingData);

    const formData = new FormData();
    formData.append('name', trainingData.name);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put<any>(this.url + 'trainingsurveytopic/edit/' + id, formData);
  }

  //insert training survey list
  addTrainingsurvey(trainingData, trainingid) {
    //alert(JSON.stringify(trainingData))
    //alert(trainingid)
    const formData = new FormData();
    formData.append('surveytype', trainingData.surveytype);
    formData.append('name', trainingData.name);

    console.log('FORMDATA: ' + formData);
    return this.http.post<any>(this.url + 'trainingsurvey/' + trainingid, formData);
  }

  editTrainingSurvey(trainingregisterlistData, id) {
    console.log(trainingregisterlistData);

    const formData = new FormData();
    formData.append('surveytype', trainingregisterlistData.surveytype);
    formData.append('name', trainingregisterlistData.name);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put<any>(this.url + 'survey/edit/' + id, formData);
  }

  deleteTrainingSurvey(trainingid) {
    return this.http.delete(this.url + 'trainingsurvey/' + trainingid);
  }
  //-------------------------------------




  //--------zone training document-------
  gettrainingviewdocumentdata(): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'trainingdocumentcount')
  }

  getlisttrainingdocumentdata(trainingid): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'listdocument/' + trainingid)
  }

  sendmaildocument(trainingid): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'maildocument/' + trainingid)
  }

  deleteTrainingDocument(trainingid) {
    return this.http.delete(this.url + 'deletedocument/' + trainingid);
  }

  addTrainingDocument(trainingData, file: FileList, trainingId) {

    //alert('service:' + JSON.stringify(trainingData))
    const formData = new FormData();
    formData.append('detail', trainingData.detail);

    for (var i = 0; i < file.length; i++) {
      formData.append('files', file[i]);
    }

    console.log('FORMDATA: ' + formData);
    console.log("test", formData.getAll('detail'));
    console.log("yyy", formData.getAll('files'));
    return this.http.post<any>(this.url + 'insertdocument/' + trainingId, formData);
  }

  addTrainingsurveyanswer(body) {
    return this.http.post(this.url + 'trainingsurveyanswer', body);
  }
  //-------------------------------------


  gethistorytraining(name): Observable<any[]> {
    console.log('FORMDATA: ' + name);
    return this.http.get<any[]>(this.url + 'historyreport/' + name)
  }




  //--------zone training program-------
  getprogramtraining(trainingid): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'program/' + trainingid)
  }
  getprogramtrainingdetail(programid): Observable<any> {
    return this.http.get<any>(this.url + "programdetail/" + programid)
  }
  addTrainingProgram(trainingData, file: FileList) {
    console.log(trainingData);
    console.log(file);

    const formData = new FormData();
    formData.append('TrainingPhaseId', trainingData.TrainingPhaseId);
    formData.append('ProgramDate', trainingData.programdate.date.year + '-' + trainingData.programdate.date.month + '-' + trainingData.programdate.date.day);
    formData.append('MinuteStartDate', trainingData.mStart);
    formData.append('MinuteEndDate', trainingData.mEnd);
    formData.append('ProgramType', trainingData.programtype);
    formData.append('ProgramTopic', trainingData.programtopic);
    formData.append('ProgramDetail', trainingData.programdetail);
    formData.append('ProgramLocation', trainingData.programlocation);
    formData.append('ProgramToDress', trainingData.programtodress);
    if(trainingData.lecturername != null){
      for (var i = 0; i < trainingData.lecturername.length; i++) {
        formData.append('TrainingLecturerId', trainingData.lecturername[i]);
      }
    }

    if (file != null) {
      for (var ii = 0; ii < file.length; ii++) {
        formData.append("files", file[ii]);
      }
    }
    console.log('FORMDATA: ' + formData);
    return this.http.post<any>(this.url + 'program/add', formData);
  }

  deleteTrainingProgram(trainingid) {
    return this.http.delete(this.url + 'program/delete/' + trainingid);
  }
  editTrainingProgram(trainingData, programid, removelecturer, addlecturer, file: FileList) {
    console.log(removelecturer);

    const formData = new FormData();
    formData.append('ProgramDate', trainingData.programdate.year + '-' + trainingData.programdate.month + '-' + trainingData.programdate.day);
    formData.append('MinuteStartDate', trainingData.mStart);
    formData.append('MinuteEndDate', trainingData.mEnd);
    formData.append('ProgramType', trainingData.programtype);
    formData.append('ProgramTopic', trainingData.programtopic);
    formData.append('ProgramDetail', trainingData.programdetail);
    formData.append('ProgramLocation', trainingData.programlocation);
    formData.append('ProgramToDress', trainingData.programtodress);
    if (removelecturer.length > 0) {
      for (var i = 0; i < removelecturer.length; i++) {
        formData.append('RemoveLecturer', removelecturer[i]);
      }
    }
    if (addlecturer.length > 0) {
      for (var i = 0; i < addlecturer.length; i++) {
        formData.append('AddLecturer', addlecturer[i]);
      }
    }
    if (file != null) {
      for (var ii = 0; ii < file.length; ii++) {
        formData.append("files", file[ii]);
      }
    }
    console.log("RemoveLecturer", formData.getAll('RemoveLecturer'));
    return this.http.put<any>(this.url + 'program/edit/' + programid, formData);
  }
  deleteTrainingProgramFiles(trainingprogramfilesid) {
    return this.http.delete(this.url + 'program/deletefiles/' + trainingprogramfilesid);
  }
  //-------------------------------------
  //--------zone training lecturer-------
  gettraininglecturer(): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'lecturer')
  }

  getUsetraininglecturer(lecturerid): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'lecturer/use/get/' + lecturerid)
  }

  gettraininglecturerById(id): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'lecturer/' + id)
  }

  gettraininglecturerlist(trainingid): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'lecturerlist2/' + trainingid)
  }

  addTraininglecturerjoinsurvey(trainingData, lecturerId, trainingId) {
    //alert(JSON.stringify(trainingData.name))
    const formData = new FormData();
    formData.append('trainingsurveytopicId', trainingData.name);
    formData.append('lecturerId', lecturerId);
    formData.append('trainingId', trainingId);

    console.log('FORMDATA: ' + formData);
    return this.http.post(this.url + 'lecturerjoinsurvey/save', formData);
  }

  editTraininglecturerjoinsurvey(trainingData, id) {
    //console.log(trainingData);
    const formData = new FormData();
    formData.append('trainingsurveytopicId', trainingData.name);

    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url + 'lecturerjoinsurvey/edit/' + id, formData);
  }

  addTraininglecturer(trainingData, picFiles: FileList, profileFiles: FileList) {
    console.log('profileFiles: ' + profileFiles);
    //alert(JSON.stringify(trainingData))
    const formData = new FormData();
    formData.append('LecturerType', trainingData.LecturerType);
    formData.append('LecturerName', trainingData.lecturername);
    formData.append('Phone', trainingData.lecturerphone);
    formData.append('Email', trainingData.lectureremail);
    formData.append('Education', trainingData.education);
    formData.append('WorkHistory', trainingData.workhistory);
    formData.append('Experience', trainingData.experience);
    formData.append('DetailPlus', trainingData.detailplus);
    formData.append('Address', trainingData.address);
    if (picFiles != null) {
      for (var index = 0; index < picFiles.length; index++) {
        formData.append("ImageProfile", picFiles[index]);
      }
    }
    else{
      formData.append("ImageProfile", null);
    }

    if (profileFiles != null) {
      for (var index = 0; index < profileFiles.length; index++) {
        formData.append("ProfileUploads", profileFiles[index]);
      }
    }
    else{
      formData.append("ProfileUploads", null);
    }

    console.log('FORMDATA: ' + formData);
    return this.http.post<any>(this.url + 'lecturer/save', formData);
  }


  editTraininglecturer(trainingData, id, picFiles: FileList, profileFiles: FileList) {
    console.log(trainingData);

    const formData = new FormData();
    formData.append('LecturerType', trainingData.LecturerType);
    formData.append('LecturerName', trainingData.lecturername);
    formData.append('Phone', trainingData.lecturerphone);
    formData.append('Email', trainingData.lectureremail);
    formData.append('Education', trainingData.education);
    formData.append('WorkHistory', trainingData.workhistory);
    formData.append('Experience', trainingData.experience);
    formData.append('DetailPlus', trainingData.detailplus);
    if (picFiles != null) {
      for (var index = 0; index < picFiles.length; index++) {
        formData.append("ImageProfile", picFiles[index]);
      }
    }
    else{
      formData.append("ImageProfile", null);
    }


    if (profileFiles != null) {
      for (var index = 0; index < profileFiles.length; index++) {
        formData.append("ProfileUploads", profileFiles[index]);
      }
    }
    else{
      formData.append("ProfileUploads", "");
    }


    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put<any>(this.url + 'lecturer/edit/' + id, formData);
  }

  deleteTrainingLecturer(id) {
    return this.http.delete(this.url + 'lecturer/delete/' + id);
  }
  //-------------------------------------

  //---------zone training phase---------
  getTrainingPhase(trainingid): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'phase/' + trainingid)
  }

  getTrainingPhaseDetail(id): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'phase/detail/' + id)
  }

  getTrainingPhaseCount(trainingid): Observable<string> {
    return this.http.get<string>(this.url + 'phase/count/' + trainingid)
  }
  addTrainingPhase(trainingphasedata, trainingid) {
    console.log(trainingphasedata);
    console.log(trainingid);
    var group: any = parseInt(trainingphasedata.group)
    console.log(group);
    const formData = new FormData();
    formData.append('TrainingId', trainingid);
    formData.append('PhaseNo', trainingphasedata.phaseno);
    formData.append('Title', trainingphasedata.title);
    formData.append('Detail', trainingphasedata.detail);
    formData.append('StartDate', trainingphasedata.startdate.date.year + '-' + trainingphasedata.startdate.date.month + '-' + trainingphasedata.startdate.date.day);
    formData.append('EndDate', trainingphasedata.enddate.date.year + '-' + trainingphasedata.enddate.date.month + '-' + trainingphasedata.enddate.date.day);
    formData.append('Location', trainingphasedata.location);
    formData.append('Group', group);
    // console.log('FORMDATA: ' + formData.get("StartDate"));
    return this.http.post<any>(this.url + "phase/add", formData);
  }

  editTrainingPhase(trainingData, id) {
    console.log(trainingData);
    var group: any = parseInt(trainingData.group)
    const formData = new FormData();
    formData.append('PhaseNo', trainingData.phaseno);
    formData.append('Title', trainingData.title);
    formData.append('Detail', trainingData.detail);
    formData.append('StartDate', trainingData.startdate.year + '-' + trainingData.startdate.month + '-' + trainingData.startdate.day);
    formData.append('EndDate', trainingData.enddate.year + '-' + trainingData.enddate.month + '-' + trainingData.enddate.day);
    formData.append('Location', trainingData.location);
    formData.append('Group', group);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put<any>(this.url + 'phase/edit/' + id, formData);
  }

  deleteTrainingPhase(trainingid) {
    return this.http.delete(this.url + 'phase/delete/' + trainingid);
  }

  //-------------------------------------

  //---------zone training condition---------
  //insert training condition list
  addTrainingCondition(trainingData, trainingid) {
    //alert(JSON.stringify(trainingData))
    //alert(trainingid)
    const formData = new FormData();
    formData.append('name', trainingData.name);
    formData.append('startyear', trainingData.startyear);
    formData.append('endyear', trainingData.endyear);
    formData.append('conditiontype', trainingData.conditiontype);

    console.log('FORMDATA: ' + formData);
    return this.http.post<any>(this.url + 'condition/add/' + trainingid, formData);
  }

  editTrainingCondition(trainingData, id) {
    console.log(trainingData);

    const formData = new FormData();
    formData.append('name', trainingData.name);
    formData.append('startyear', trainingData.startyear);
    formData.append('endyear', trainingData.endyear);
    formData.append('conditiontype', trainingData.conditiontype);

    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put<any>(this.url + 'condition/edit/' + id, formData);
  }

  getTrainingCondition(trainingid): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'condition/' + trainingid)
  }
  deleteTrainingCondition(trainingid) {
    return this.http.delete(this.url + 'condition/delete/' + trainingid);
  }

  //-------------------------------------

  printNamePlate(registerId) {
    console.log("RegisID: ", registerId);

    const formData = new FormData();
    for (var i = 0; i < registerId.length; i++) {
      formData.append('RegisterId', registerId[i]);
    }

    console.log('FORMDATA: ' + formData);
    return this.http.post<any[]>(this.url + "printNamePlate", formData);
  }
  getTrainingPlan(trainingphaseid): Observable<Ngong[]> {
    return this.http.get<Ngong[]>(this.url + 'plan/' + trainingphaseid)
  }
  getTrainingPlantable(trainingphaseid): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'plantable/' + trainingphaseid)
  }
  getchecktrainingregister(trainingid, userid): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'checktrainingregister/' + trainingid + '/' + userid)
  }


  //Update Group
  editRegisterGroup2(trainingregisterlistData, id) {
    console.log("trainingregisterlistData", trainingregisterlistData)
    // alert(trainingregisterlistData.approve1)
    // console.log(trainingregisterlistData);

    const formData = new FormData();

    if (trainingregisterlistData.approve1 == null) {
      trainingregisterlistData.approve1 = 0;
    }
    if (trainingregisterlistData.approve2 == null) {
      trainingregisterlistData.approve2 = 0;
    }
    if (trainingregisterlistData.approve3 == null) {
      trainingregisterlistData.approve3 = 0;
    }
    if (trainingregisterlistData.approve4 == null) {
      trainingregisterlistData.approve4 = 0;
    }
    if (trainingregisterlistData.approve5 == null) {
      trainingregisterlistData.approve5 = 0;
    }
    if (trainingregisterlistData.approve6 == null) {
      trainingregisterlistData.approve6 = 0;
    }
    if (trainingregisterlistData.approve7 == null) {
      trainingregisterlistData.approve7 = 0;
    }
    if (trainingregisterlistData.approve8 == null) {
      trainingregisterlistData.approve8 = 0;
    }
    if (trainingregisterlistData.approve9 == null) {
      trainingregisterlistData.approve9 = 0;
    }
    if (trainingregisterlistData.approve10 == null) {
      trainingregisterlistData.approve10 = 0;
    }

    for (var index = 0; index < trainingregisterlistData.length; index++) {
      trainingregisterlistData['approve' + trainingregisterlistData[index].phaseNo] = trainingregisterlistData[index].value;
    }
    // alert(trainingregisterlistData.approve1)
    formData.append('approve1', trainingregisterlistData.approve1);
    formData.append('approve2', trainingregisterlistData.approve2);
    formData.append('approve3', trainingregisterlistData.approve3);
    formData.append('approve4', trainingregisterlistData.approve4);
    formData.append('approve5', trainingregisterlistData.approve5);
    formData.append('approve6', trainingregisterlistData.approve6);
    formData.append('approve7', trainingregisterlistData.approve7);
    formData.append('approve8', trainingregisterlistData.approve8);
    formData.append('approve9', trainingregisterlistData.approve9);
    formData.append('approve10', trainingregisterlistData.approve10);

    for (var iii = 0; iii < id.length; iii++) {
      formData.append("traningregisterid", id[iii]);
    }

    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url + 'register2/group/', formData);
  }

  getTrainingregisterlist(id) {
    return this.http.get<TrainingRegisterlist[]>(this.url + 'trainingregisterlist/get/' + id)
  }

  Updateidcode(trainingRegisterlist) {
    console.log("trainingRegisterlist", trainingRegisterlist);
    var data: any = [];

    data = trainingRegisterlist.map((item, index) => {
      return {
        id: item.id,
        code: item.code
      }
    })
    console.log("data", data);

    // const formData = new FormData();
    // for (var iii = 0; iii < data.length; iii++) {
    //   formData.append("TrainingCode", data[iii]);
    // }

    const formData = {
      TrainingCode: data
    }

    // console.log("test: ", formData.getAll("TrainingCode"));

    console.log('FORMDATA: ', formData);
    return this.http.put(this.url + 'Updateidcode', formData);
  }

  //---------zone training ProgramaLogin---------
  //insert training condition list
  addTrainingProgramLogin(trainingData, trainingid, programdate) {
    //alert(JSON.stringify(trainingData))
    //alert(trainingid)
    const formData = new FormData();
    formData.append('programlogintype', trainingData.programtype);

    console.log('FORMDATA: ' + formData);
    return this.http.post(this.url + 'programlogin/add/' + trainingid + '/' + programdate, formData);
  }

  getTrainingProgramLogin(trainingid): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'TrainingProgramDate/get/' + trainingid)
  }

  getTrainingProgramLoginQR(programdate): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'TrainingProgramLoginQRDate/get/' + programdate)
  }

  updateTrainingProgramLogin(trainingData, programloginQRCodeid) {
    const formData = new FormData();
    formData.append('programlogintype', trainingData.programtype);
    console.log('programloginQRCodeid: ' + programloginQRCodeid);
    console.log('FORMDATA: ' + formData);
    return this.http.put(this.url + 'programlogin/update/' + programloginQRCodeid, formData);
  }
  //-------end zone training ProgramaLogin-------

  getTrainingLoginRate(trainingid): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'loginrate/get/' + trainingid)
  }

  getTrainingSurveyLecturer(username): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'surveylecturer/get/' + username)
  }

  getTraininghistoryreport(username): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'historyreport/get/' + username)
  }

  getprocessingOpentrainingsurvey(trainingLecturerJoinSurveysId): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'answeropen/get/' + trainingLecturerJoinSurveysId)
  }

  getprocessingYestrainingsurvey(trainingLecturerJoinSurveysId): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'answeryesno/get/' + trainingLecturerJoinSurveysId)
  }

  getprocessingLiketrainingsurvey(trainingLecturerJoinSurveysId): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'answerlike/get/' + trainingLecturerJoinSurveysId)
  }
  printNamePlatebyPalm(registerId) {

    return this.http.get<any>(`${this.url}printNamePlatebyPalm/${registerId}`);
  }
  //ดึงข้อมูลประเภทกิจกรรม
  getTrainingProgramType(): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'programtype/get')
  }

  //เพิ่มข้อมูลประเภทกิจกรรม
  addTrainingProgramType(trainingData) {
    //alert(JSON.stringify(trainingData))
    //alert(trainingid)
    const formData = new FormData();
    formData.append('name', trainingData.name);

    console.log('FORMDATA: ' + formData);
    return this.http.post(this.url + 'programtype/add', formData);
  }

  //แก้ไขประเภทกิจกรรม
  editTrainingProgramType(trainingData, id) {

    const formData = new FormData();
    formData.append('name', trainingData.name);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url + 'programtype/edit/' + id, formData);
  }


  //ลบประเภทกิจกรรม
  deleteTrainingProgramType(id) {
    return this.http.delete(this.url + 'programtype/delete/' + id);
  }





  //ดึงข้อมูลประเภทวิทยากร
  getTrainingLecturerType(): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'lecturertype/get')
  }

  //ดึงข้อมูลประเภทวิทยากร
  getTrainingLecturerTypeById(Id): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'lecturertypeById/get/' + Id)
  }

  //เพิ่มข้อมูลประเภทวิทยากร
  addTrainingLecturerType(trainingData) {
    //alert(JSON.stringify(trainingData))
    //alert(trainingid)
    const formData = new FormData();
    formData.append('name', trainingData.name);

    console.log('FORMDATA: ' + formData);
    return this.http.post<any>(this.url + 'lecturertype/add', formData);
  }

  //แก้ไขประเภทวิทยากร
  editTrainingLecturerType(trainingData, id) {

    const formData = new FormData();
    formData.append('name', trainingData.name);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put<any>(this.url + 'lecturertype/edit/' + id, formData);
  }


  //ลบประเภทวิทยากร
  deleteTrainingLecturerType(id) {
    return this.http.delete(this.url + 'lecturertype/delete/' + id);
  }

  //รายงานข้อมูลบุคคลของวิทยากร
  reportTrainingLecturer(trainingLecturerId, name, year) {
    const formData = new FormData();
    formData.append('trainingLecturerId', trainingLecturerId);
    formData.append('trainingname', name);
    formData.append('year', year);
    return this.http.post<any>(this.url + 'reportlecturer', formData);
  }
  //ตั้งค่าการประกาศหลักสูตร
  SettingTraining(trainingData, id) {
    console.log(trainingData);

    const formData = new FormData();
    formData.append('status', trainingData.status);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url + 'trainingsetting/edit/' + id, formData);
  }

  getCheckTrainingProgramLogin(trainingid): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'check_TrainingProgramLoginQRDate/get/' + trainingid);
  }


  //เก็บข้อมูลรายงานสรุปผลการฝึกอบรม(กลุ่ม)
  addTrainingSummaryReportGroup(trainingData, file: FileList, phaseId, group) {
    //alert('service:' + JSON.stringify(trainingData))
    const formData = new FormData();
    formData.append('Detail', trainingData.detail);

    for (var i = 0; i < file.length; i++) {
      formData.append('files', file[i]);
    }

    console.log('FORMDATA: ' + formData);
    //console.log("test", formData.getAll('detail'));
    //console.log("yyy", formData.getAll('files'));
    return this.http.post<any>(this.url + 'summaryreport/group/add/' + phaseId + '/' + group, formData);
  }

  //ดึงข้อมูลรายงานสรุปผลการฝึกอบรมช่วงเป็นกลุ่ม
  getTrainingSummaryReportGroup(phaseid, group): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'summaryreport/group/get/' + phaseid + '/' + group)
  }

  //ลบข้อมูลรายงานสรุปผลการฝึกอบรมช่วงเป็นกลุ่ม
  deleteTrainingSummaryReportGroup(id) {
    return this.http.delete(this.url + 'summaryreport/group/delete/' + id);
  }


  //เก็บข้อมูลรายงานสรุปผลการฝึกอบรม(ทั้งหลักสูตร)
  addTrainingSummaryReportProject(trainingData, file: FileList, trainingid) {
    //alert('service:' + JSON.stringify(trainingData))
    const formData = new FormData();
    formData.append('Detail', trainingData.detail);

    for (var i = 0; i < file.length; i++) {
      formData.append('files', file[i]);
    }

    console.log('FORMDATA: ' + formData);
    //console.log("test", formData.getAll('detail'));
    //console.log("yyy", formData.getAll('files'));
    return this.http.post<any>(this.url + 'summaryreport/project/add/' + trainingid , formData);
  }

  //ดึงข้อมูลรายงานสรุปผลการฝึกอบรม(ทั้งหลักสูตร)
  getTrainingSummaryReportProject(trainingid): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'summaryreport/project/get/' + trainingid)
  }


  //ลบข้อมูลรายงานสรุปผลการฝึกอบรม(ทั้งหลักสูตร)
  deleteTrainingSummaryReportProject(id) {
    return this.http.delete(this.url + 'summaryreport/project/delete/' + id);
  }

}


