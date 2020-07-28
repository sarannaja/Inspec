import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  count = 0
  url = "";
  base = "";
  urlfirst = "";
  urllist = "";
  //files: FileList

  private subject = new Subject<any>();
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/user/getuser/';
    this.urllist = baseUrl + 'api/user/getuserlist/';
    this.base = baseUrl + 'api/user/';
  }

  getuserdata(id: any): Observable<any[]> { //role
    return this.http.get<any[]>(this.url + id)
  }
  getprovincedata(id): Observable<any[]> {
    return this.http.get<any[]>(this.base + 'province/' + id)
  }
  getuserlistdata(id: any): Observable<any[]> {
    return this.http.get<any[]>(this.urllist + id)
  }

  getuserfirstdata(id: any): Observable<any[]> {
    return this.http.get<any[]>(this.base +'getuserfirst/'+ id)
  }

  getplancount(id: any): Observable<any[]> {
    return this.http.get<any[]>(this.base +'getplancount/'+ id)
  }

  getprovincialdepartment(id: any): Observable<any> {
    return this.http.get<any>(this.base +'getprovincialdepartment/'+ id)
  }

 sendNav(roleId: string) {
    this.subject.next({ roleId: roleId });

  }

  clearUserNav() {
    this.subject.next();
  }
  getUserNav(): Observable<any> {
    return this.subject.asObservable();
  }

  addUser(userData,file: FileList ,roleId) {
     //alert(roleId);
      //console.log("servicelog: ", userData);
      const formData = new FormData();
      formData.append('Email', userData.Email); //email   
      formData.append('Role_id', roleId); //role
      formData.append('Prefix', userData.Prefix); //คำนำหน้า
      formData.append('Name', userData.Name); //ชื่อ
      formData.append('Position', userData.Position);//ตำแหน่ง
      formData.append('Educational','');
      formData.append('Commandnumber',userData.Commandnumber); //เลขที่คำสั่ง
      formData.append('Officephonenumber','');
      formData.append('PhoneNumber',userData.PhoneNumber);
      formData.append('Telegraphnumber','');
      formData.append('Housenumber','');
      formData.append('Rold','');
      formData.append('Alley','');
      formData.append('Postalcode','');

      //<!-- ด้าน -->
      if(userData.Side != null){
      formData.append('Side',userData.Side); 
      }else{
        formData.append('Side',null);
      }
      //<!-- END ด้าน -->
      formData.append('Img','');
      formData.append('startdate', userData.Startdate.date.year + '-' + userData.Startdate.date.month + '-' + userData.Startdate.date.day);
      
      if(userData.Enddate != null){
      formData.append('enddate', userData.Enddate.date.year + '-' + userData.Enddate.date.month + '-' + userData.Enddate.date.day);
      }else{
        formData.append('enddate',null)
      }

      if(userData.Commandnumberdate != null){
      formData.append('Commandnumberdate', userData.Commandnumberdate.date.year + '-' + userData.Commandnumberdate.date.month + '-' + userData.Commandnumberdate.date.day);
      }else{
        formData.append('Commandnumberdate',null)
      }

      if(userData.UserRegion != null){
        for (var i = 0; i < userData.UserRegion.length; i++) {
          formData.append('UserRegion', userData.UserRegion[i]); //เขตที่รับผิดชอบมีได้หลายอัน
        }
      }else{
        formData.append('UserRegionId', '1');
      }

      if (userData.UserProvince != null) {
        // alert(userData.UserProvince);
          formData.append('UserProvinceId', userData.UserProvince); //จังหวัดที่รับผิดชอบมีได้หลายอัน
      } else {
       // alert(1);
        formData.append('UserProvinceId', '1');
      }

      if (userData.ProvinceId == null) {
        formData.append('ProvinceId', '1');
      } else {
        formData.append('ProvinceId', userData.ProvinceId); //จังหวัดมีได้อันเดียว
      }

      if (userData.DistrictId == null) {
        formData.append('DistrictId', '1');
      } else {
        formData.append('DistrictId', userData.DistrictId); //อำเภอมีได้อันเดียว
      }

      if (userData.SubdistrictId == null) {
        formData.append('SubdistrictId', '1');
      } else {
        formData.append('SubdistrictId', userData.SubdistrictId); //ตำบลมีได้อันเดียว
      }

      if (userData.MinistryId == null) { 
        formData.append('MinistryId', '1');
      } else {
        formData.append('MinistryId', userData.MinistryId); //กระทรวงมีได้อันเดียว
      }

      if (userData.DepartmentId == null) {
        formData.append('DepartmentId', '1');
      } else {
        formData.append('DepartmentId', userData.DepartmentId);//กรมมีได้อันเดียว
      }

      if (userData.FiscalYear == null) {
        formData.append('FiscalYearId', '1');
      } else {
        //alert(userData.FiscalYear);
        formData.append('FiscalYearId', userData.FiscalYear);//ปีงบ
      }

      if (userData.ProvincialDepartmentId == null) {
        formData.append('ProvincialDepartmentId', '1');
      } else {
        formData.append('ProvincialDepartmentId', userData.ProvincialDepartmentId);//หน่วยงานภูมิภาคมีได้อันเดียว
      }

      for (var iii = 0; iii < file.length; iii++) {

        formData.append("files", file[iii]);
      }

    return this.http.post(this.base, formData);
  }

  editprofile(userData, file: FileList, userId) {
   // alert('2 :' + userId)
    const formData = new FormData();
    formData.append('Role_id', userData.Role_id); //role
    formData.append('Prefix', userData.Prefix);
    formData.append('Name', userData.Name);
    formData.append('Position', userData.Position);
    formData.append('PhoneNumber', userData.PhoneNumber);
    formData.append('Formprofile', userData.Formprofile);// สำหรับเช็ค user หรือแอดมิน เป็นคนเพิ่ม

    formData.append('Email', userData.Email); //email  
    
    formData.append('Educational','');
    formData.append('Commandnumber',userData.Commandnumber); //เลขที่คำสั่ง
    formData.append('Officephonenumber','');
    formData.append('Telegraphnumber','');
    formData.append('Housenumber','');
    formData.append('Rold','');
    formData.append('Alley','');
    formData.append('Postalcode','');
    formData.append('Img',userData.Img);

    //<!-- ด้าน -->
    if(userData.Side != null){
    formData.append('Side',userData.Side); 
    }else{
      formData.append('Side',null);
    }
    //<!-- END ด้าน -->
    formData.append('startdate', userData.Startdate.date.year + '-' + userData.Startdate.date.month + '-' + userData.Startdate.date.day);
    
    if(userData.Enddate != null){
    formData.append('enddate', userData.Enddate.date.year + '-' + userData.Enddate.date.month + '-' + userData.Enddate.date.day);
    }else{
      formData.append('enddate',null)
    }

    if(userData.Commandnumberdate != null){
    formData.append('Commandnumberdate', userData.Commandnumberdate.date.year + '-' + userData.Commandnumberdate.date.month + '-' + userData.Commandnumberdate.date.day);
    }else{
      formData.append('Commandnumberdate',null)
    }

    if(userData.UserRegion != null){
      for (var i = 0; i < userData.UserRegion.length; i++) {
        formData.append('UserRegion', userData.UserRegion[i]); //เขตที่รับผิดชอบมีได้หลายอัน
      }
    }else{
      formData.append('UserRegionId', '1');
    }

    if (userData.UserProvince != null) {
        formData.append('UserProvinceId', userData.UserProvince); //จังหวัดที่รับผิดชอบมีได้หลายอัน
    } else {
      formData.append('UserProvinceId', '1');
    }

    if (userData.ProvinceId == null) {
      formData.append('ProvinceId', '1');
    } else {
      formData.append('ProvinceId', userData.ProvinceId); //จังหวัดมีได้อันเดียว
    }

    if (userData.DistrictId == null) {
      formData.append('DistrictId', '1');
    } else {
      formData.append('DistrictId', userData.DistrictId); //อำเภอมีได้อันเดียว
    }

    if (userData.SubdistrictId == null) {
      formData.append('SubdistrictId', '1');
    } else {
      formData.append('SubdistrictId', userData.SubdistrictId); //ตำบลมีได้อันเดียว
    }

    if (userData.MinistryId == null) { 
      formData.append('MinistryId', '1');
    } else {
      formData.append('MinistryId', userData.MinistryId); //กระทรวงมีได้อันเดียว
    }

    if (userData.DepartmentId == null) {
      formData.append('DepartmentId', '1');
    } else {
      formData.append('DepartmentId', userData.DepartmentId);//กรมมีได้อันเดียว
    }

    if (userData.FiscalYear == null) {
      formData.append('FiscalYearId', '1');
    } else {
      formData.append('FiscalYearId', userData.FiscalYear);//ปีงบ
    }

    if (userData.ProvincialDepartmentId == null) {
      formData.append('ProvincialDepartmentId', '1');
    } else {
      formData.append('ProvincialDepartmentId', userData.ProvincialDepartmentId);//หน่วยงานภูมิภาคมีได้อันเดียว
    }

    for (var iii = 0; iii < file.length; iii++) {

      formData.append("files", file[iii]);
    }
   
    let path = this.base + userId;
    return this.http.put(path, formData)
  }

  deleteUser(id) {
    return this.http.delete(this.base + id);
  }

}
