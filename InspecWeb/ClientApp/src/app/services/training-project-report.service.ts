import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TrainingProjectReportService {
  url = "";

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.url = baseUrl + 'api/TrainingProjectReport/'
  }

  getTrainingProjectReports() {
    return this.http.get<any>(this.url + "getTrainingProjectReports");
  }

  getTrainingProjectReportById(id) {
    return this.http.get<any>(this.url + "getTrainingProjectReportById/" + id);
  }

  addTrainingProjectReport(value, userId, trainingProjectReportfile: FileList,
    projectDocumentFile: FileList, trainingDetailFile: FileList, modelDirectoryFile: FileList, practiceGuideFile: FileList) {
    console.log('valueS => ', value);
    console.log('userIdS => ', userId);
    console.log('trainingProjectReportfile => ', trainingProjectReportfile);
    console.log('projectDocumentFile => ', projectDocumentFile);
    console.log('trainingDetailFile => ', trainingDetailFile);
    console.log('modelDirectoryFile => ', modelDirectoryFile);
    console.log('practiceGuideFile => ', practiceGuideFile);

    const formData = new FormData();
    formData.append('Year', value.year.toString());
    formData.append('CourseType', value.courseType);
    formData.append('userId', userId);

    if (trainingProjectReportfile != null) {
      for (var i = 0; i < trainingProjectReportfile.length; i++) {
        formData.append("reportFiles", trainingProjectReportfile[i]);
      }
    }
    if (projectDocumentFile != null) {
      for (var i = 0; i < projectDocumentFile.length; i++) {
        formData.append("projectDocumentFiles", projectDocumentFile[i]);
      }
    }
    if (trainingDetailFile != null) {
      for (var i = 0; i < trainingDetailFile.length; i++) {
        formData.append("trainingDetailFiles", trainingDetailFile[i]);
      }
    }
    if (modelDirectoryFile != null) {
      for (var i = 0; i < modelDirectoryFile.length; i++) {
        formData.append("modelDirectoryFiles", modelDirectoryFile[i]);
      }
    }
    if (practiceGuideFile != null) {
      for (var i = 0; i < practiceGuideFile.length; i++) {
        formData.append("practiceGuideFiles", practiceGuideFile[i]);
      }
    }
    console.log('name', formData.getAll("Name"));
    return this.http.post<any>(this.url + "addTrainingProjectReport", formData);
  }


  editTrainingProjectReport(value, userId, trainingProjectReportfile: FileList,
    projectDocumentFile: FileList, trainingDetailFile: FileList, modelDirectoryFile: FileList, practiceGuideFile: FileList, id) {
    console.log('valueS => ', value);
    console.log('userIdS => ', userId);
    console.log('trainingProjectReportfile => ', trainingProjectReportfile);
    console.log('projectDocumentFile => ', projectDocumentFile);
    console.log('trainingDetailFile => ', trainingDetailFile);
    console.log('modelDirectoryFile => ', modelDirectoryFile);
    console.log('practiceGuideFile => ', practiceGuideFile);

    const formData = new FormData();
    formData.append('Year', value.year.toString());
    formData.append('CourseType', value.courseType);
    formData.append('userId', userId);

    if (trainingProjectReportfile != null) {
      for (var i = 0; i < trainingProjectReportfile.length; i++) {
        formData.append("reportFiles", trainingProjectReportfile[i]);
      }
    }
    if (projectDocumentFile != null) {
      for (var i = 0; i < projectDocumentFile.length; i++) {
        formData.append("projectDocumentFiles", projectDocumentFile[i]);
      }
    }
    if (trainingDetailFile != null) {
      for (var i = 0; i < trainingDetailFile.length; i++) {
        formData.append("trainingDetailFiles", trainingDetailFile[i]);
      }
    }
    if (modelDirectoryFile != null) {
      for (var i = 0; i < modelDirectoryFile.length; i++) {
        formData.append("modelDirectoryFiles", modelDirectoryFile[i]);
      }
    }
    if (practiceGuideFile != null) {
      for (var i = 0; i < practiceGuideFile.length; i++) {
        formData.append("practiceGuideFiles", practiceGuideFile[i]);
      }
    }
    console.log('name', formData.getAll("Name"));
    return this.http.put<any>(this.url + "editTrainingProjectReport/" + id, formData);
  }

  deleteTrainingProjectReportFile(id, fileType) {
    return this.http.delete<any>(this.url + "deleteTrainingProjectReportFile/" + id + "/" + fileType);
  }

  deleteTrainingProjectReport(id) {
    return this.http.delete<any>(this.url + "deleteTrainingProjectReport/" + id);
  }
}
