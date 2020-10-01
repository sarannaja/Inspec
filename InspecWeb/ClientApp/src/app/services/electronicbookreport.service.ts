import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ElectronicbookreportService {

  url = "";
  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.url = baseUrl + 'api/electronicbookreport/';
  }

  printReport(electID, electronicBookData) {
    console.log("EDATA: ", electronicBookData);
    console.log("electID: ", electID);
    var exportData: any = [];
    var exportData2: any = [];

    exportData = electronicBookData.ebookInvite.map((item, index) => {
      if (item.user.role_id == 6) {
        return {
          inspectorName: item.user.ministries.name + "\n" + item.user.prefix + item.user.name,
          inspectorSign: item.user.signature,
          inspectorDescription: item.description,
          approve: item.approve,
        }
      } else if (item.user.role_id == 10) {
        return {
          inspectorName: item.user.departments.name + "\n" + item.user.prefix + item.user.name,
          inspectorSign: item.user.signature,
          inspectorDescription: item.description,
          approve: item.approve,
        }
      }
      else if (item.user.role_id == 7) {
        return {
          inspectorName: "ที่ปรึกษาผู้ตรวจภาคประชาชน" + "\n" + "ด้าน" + item.user.sides.name + "\n" + item.user.prefix + item.user.name,
          inspectorSign: null,
          inspectorDescription: "-",
          approve: "-",
        }
      }
    })

    console.log("CHECK: ", exportData);
    var subjectData: any = [];
    subjectData = electronicBookData.electronicBookGroup.map((item, index) => {
      return item.centralPolicyTitle + " " + "(ตรวจ ณ สถานที่ จังหวัด: " + item.inspectionPlanProvinceName + ")"
    })
    console.log("CHECK2: ", subjectData);


    exportData2 = electronicBookData.electronicBookDepartment.map((item, index) => {
      return {
        department: item.provincialDepartments.name,
        departmentSign: item.userProvincialDepartment.signature,
        departmentDescription: item.description,
        departmentName: item.userProvincialDepartment.prefix + item.userProvincialDepartment.name
      }
    })

    console.log("CHECK22: ", exportData2);
    const formData = {
      printReport: exportData,
      printReport2: exportData2,
      electronicBookId: electID,
      subjectData: subjectData
    }
    return this.http.post<any>(this.url + 'printReport', formData)
  }
}
