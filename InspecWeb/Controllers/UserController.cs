using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InspecWeb.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private static UserManager<ApplicationUser> _userManager;
        private static ApplicationDbContext _context;

        public UserController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        [Route("[controller]/[action]")]
        public async Task<string> Create()
        {
            string result = string.Empty;

            if (_context.Users.Count() != 2)
            {
                UserViewModel[] users = 
                    {
                          new UserViewModel { UserName ="admin@inspec.go.th", Email = "admin@inspec.go.th", Name ="Super Admin",MinistryId =1, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "Superadmin ",Prefix = "นาย",Role_id =1,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "",PhoneNumber = "",Telegraphnumber = "",Housenumber = "",Rold = "",Alley = "", Postalcode = "",Side = "", Img = "",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {1},UserProvince = new List<int>(){1} },
                          new UserViewModel { UserName ="userRole3n1@inspec.go.th", Email = "userRole3n1@inspec.go.th",Name ="ชัยวัฒน์ โฆสิตาภา",MinistryId =1, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "ผต.นร. ",Prefix = "พลเอก",Role_id =3,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "",PhoneNumber = "0899006901",Telegraphnumber = "",Housenumber = "",Rold = "",Alley = "", Postalcode = "",Side = "", Img = "",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {1},UserProvince = new List<int>(){1} },
                          new UserViewModel { UserName ="userRole3n2@inspec.go.th", Email = "userRole3n2@inspec.go.th",Name ="สุรุ่งลักษณ์ เมฆะอำนวยชัย",MinistryId =1, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "ผต.นร. ",Prefix = "นางสาว",Role_id =3,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "",PhoneNumber = "0816408221",Telegraphnumber = "",Housenumber = "",Rold = "",Alley = "", Postalcode = "",Side = "", Img = "",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {2,13},UserProvince = new List<int>(){1} },
                          new UserViewModel { UserName ="userRole3n3@inspec.go.th", Email = "userRole3n3@inspec.go.th",Name ="สุมิตรา อติศัพท์",MinistryId =1, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "ผต.นร. ",Prefix = "นาง",Role_id =3,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "",PhoneNumber = "0892057524 ",Telegraphnumber = "",Housenumber = "",Rold = "",Alley = "", Postalcode = "",Side = "", Img = "",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {3,15,18},UserProvince = new List<int>(){1} },
                          new UserViewModel { UserName ="userRole3n4@inspec.go.th", Email = "userRole3n4@inspec.go.th",Name ="ณรงค์ เชื้อบุญช่วย",MinistryId =1, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "ผต.นร. ",Prefix = "นาย",Role_id =3,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "",PhoneNumber = "0982807576",Telegraphnumber = "",Housenumber = "",Rold = "",Alley = "", Postalcode = "",Side = "", Img = "",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {4,10},UserProvince = new List<int>(){1} },
                          new UserViewModel { UserName ="userRole3n5@inspec.go.th", Email = "userRole3n5@inspec.go.th",Name ="พีระ ทองโพธิ์",MinistryId =1, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "ผต.นร. ",Prefix = "นาย",Role_id =3,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "",PhoneNumber = "0818380278",Telegraphnumber = "",Housenumber = "",Rold = "",Alley = "", Postalcode = "",Side = "", Img = "",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {5,19},UserProvince = new List<int>(){1} },
                          new UserViewModel { UserName ="userRole3n6@inspec.go.th", Email = "userRole3n6@inspec.go.th",Name ="ธสรณ์อัฑฒ์ ธนิทธิพันธ์",MinistryId =1, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "ผต.นร. ",Prefix = "นาย",Role_id =3,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "",PhoneNumber = "0854856845",Telegraphnumber = "",Housenumber = "",Rold = "",Alley = "", Postalcode = "",Side = "", Img = "",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {6,8,11},UserProvince = new List<int>(){1} },
                          new UserViewModel { UserName ="userRole3n7@inspec.go.th", Email = "userRole3n7@inspec.go.th",Name ="ปภัสมน อัมราลิขิต ",MinistryId =1, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "ผต.นร. ",Prefix = "นางสาว",Role_id =3,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "",PhoneNumber = "0818014534",Telegraphnumber = "",Housenumber = "",Rold = "",Alley = "", Postalcode = "",Side = "", Img = "",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {7,17},UserProvince = new List<int>(){1} },
                          new UserViewModel { UserName ="userRole3n8@inspec.go.th", Email = "userRole3n8@inspec.go.th",Name ="จิรายุ นันท์ธราธร",MinistryId =1, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "ผต.นร. ",Prefix = "นาง",Role_id =3,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "",PhoneNumber = "0899697416",Telegraphnumber = "",Housenumber = "",Rold = "",Alley = "", Postalcode = "",Side = "", Img = "",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {9,14},UserProvince = new List<int>(){1} },
                          new UserViewModel { UserName ="userRole3n9@inspec.go.th", Email = "userRole3n9@inspec.go.th",Name ="อรนุช ศรีนนท์",MinistryId =1, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "ผต.นร. ",Prefix = "นางสาว",Role_id =3,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "",PhoneNumber = "0901972107",Telegraphnumber = "",Housenumber = "",Rold = "",Alley = "", Postalcode = "",Side = "", Img = "",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {12,16},UserProvince = new List<int>(){1} },
                          new UserViewModel { UserName ="apai.s2509@gmail.com", Email = "apai.s2509@gmail.com",Name ="อภัย สุทธิสังข์",MinistryId =7, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "(หัวหน้าผู้ตรวจราชการ) กำกับดูแลภาพรวมทุกกลุ่มสินค้า ",Prefix = "นาย",Role_id =6,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "022818864",PhoneNumber = "0816415234",Telegraphnumber = "022829921",Housenumber = "",Rold = "",Alley = "", Postalcode = "",Side = "", Img = "",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19},UserProvince = new List<int>(){} },
                          new UserViewModel { UserName ="ploenjit7663@gmail.com", Email = "ploenjit7663@gmail.com",Name ="เพลินจิตต์ พรหมหิตาธร",MinistryId =7, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "เจ้าหน้าที่สนับสนุนการตรวจราชการ ",Prefix = "นาย",Role_id =6,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "022818864",PhoneNumber = "0982847669",Telegraphnumber = "022829921",Housenumber = "",Rold = "",Alley = "", Postalcode = "",Side = "", Img = "",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19},UserProvince = new List<int>(){} },
                          new UserViewModel { UserName ="pongocky@gmail.com", Email = "pongocky@gmail.com",Name ="พงศกร นุตยะสกุล",MinistryId =7, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "เจ้าหน้าที่สนับสนุนการตรวจราชการ ",Prefix = "นาย",Role_id =6,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "022818864",PhoneNumber = "0846448562",Telegraphnumber = "022829921",Housenumber = "",Rold = "",Alley = "", Postalcode = "",Side = "", Img = "",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19},UserProvince = new List<int>(){} },
                          new UserViewModel { UserName ="userRole6n4@inspec.go.th", Email = "userRole6n4@inspec.go.th",Name ="นลิน บุญสพ",MinistryId =7, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "เจ้าหน้าที่สนับสนุนการตรวจราชการ ",Prefix = "นางสาว",Role_id =6,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "022818864",PhoneNumber = "0853604040",Telegraphnumber = "022829921",Housenumber = "",Rold = "",Alley = "", Postalcode = "",Side = "", Img = "",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19},UserProvince = new List<int>(){} },
                          new UserViewModel { UserName ="ppongsapitch@gmail.com", Email = "ppongsapitch@gmail.com",Name ="พิศาล พงศาพิชณ์",MinistryId =7, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "กรมการข้าว / มกอช. ",Prefix = "นาย",Role_id =6,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "022826863",PhoneNumber = "0898868980",Telegraphnumber = "022823552",Housenumber = "",Rold = "",Alley = "", Postalcode = "",Side = "", Img = "",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {1},UserProvince = new List<int>(){} },
                          new UserViewModel { UserName ="mahathein.an@gmail.cpm", Email = "mahathein.an@gmail.cpm",Name ="อัญชลี มหาเทียน",MinistryId =7, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "เจ้าหน้าที่สนับสนุนการตรวจราชการ ",Prefix = "นางสาว",Role_id =6,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "022826863",PhoneNumber = "0818401292",Telegraphnumber = "022823552",Housenumber = "",Rold = "",Alley = "", Postalcode = "",Side = "", Img = "",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {1},UserProvince = new List<int>(){} },
                          new UserViewModel { UserName ="userRole6n7@inspec.go.th", Email = "userRole6n7@inspec.go.th",Name ="ต่อพร สัตบุษ",MinistryId =7, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "เจ้าหน้าที่สนับสนุนการตรวจราชการ ",Prefix = "นางสาว",Role_id =6,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "022826863",PhoneNumber = "0830197219",Telegraphnumber = "022823552",Housenumber = "",Rold = "",Alley = "", Postalcode = "",Side = "", Img = "",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {1},UserProvince = new List<int>(){} },
                          new UserViewModel { UserName ="wprompoj@moac.go.th", Email = "wprompoj@moac.go.th",Name ="วราภรณ์ พรหมพจน์",MinistryId =7, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "การส่งเสริมสหกรณ์ ",Prefix = "นาง",Role_id =6,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "026299058",PhoneNumber = "0850706477",Telegraphnumber = "",Housenumber = "",Rold = "",Alley = "", Postalcode = "",Side = "", Img = "",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {1,3},UserProvince = new List<int>(){} },
                          new UserViewModel { UserName ="jubjang645@gmail.com", Email = "jubjang645@gmail.com",Name ="สุพัตรา ผลรัตนไพบูลย์",MinistryId =7, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "เจ้าหน้าที่สนับสนุนการตรวจราชการ ",Prefix = "นาง",Role_id =6,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "026299058",PhoneNumber = "0847230033",Telegraphnumber = "",Housenumber = "",Rold = "",Alley = "", Postalcode = "",Side = "", Img = "",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {1,3},UserProvince = new List<int>(){} },
                          new UserViewModel { UserName ="userRole6n10@inspec.go.th", Email = "userRole6n10@inspec.go.th",Name ="ธัญลักษณ์ ดีปินตา",MinistryId =7, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "เจ้าหน้าที่สนับสนุนการตรวจราชการ ",Prefix = "นาง",Role_id =6,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "026299058",PhoneNumber = "0876576398",Telegraphnumber = "",Housenumber = "",Rold = "",Alley = "", Postalcode = "",Side = "", Img = "",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {1,3},UserProvince = new List<int>(){} },
                          new UserViewModel { UserName ="anut@moac.go.th", Email = "anut@moac.go.th",Name ="อานัติ วิเศษรจนา",MinistryId =7, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "สำนักการปฏิรูปที่ดินเพื่อเกษตรกรรม ",Prefix = "นาย",Role_id =6,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "022801745",PhoneNumber = "0844508512",Telegraphnumber = "022801745",Housenumber = "",Rold = "",Alley = "", Postalcode = "",Side = "", Img = "",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {4,5},UserProvince = new List<int>(){} },
                          new UserViewModel { UserName ="sipraya001@gmail.com", Email = "sipraya001@gmail.com",Name ="โสมสุทธา ภัสสรปกณกิจ",MinistryId =7, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "เจ้าหน้าที่สนับสนุนการตรวจราชการ ",Prefix = "นางสาว",Role_id =6,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "022801745",PhoneNumber = "0922598909",Telegraphnumber = "022801745",Housenumber = "",Rold = "",Alley = "", Postalcode = "",Side = "", Img = "",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {4,5},UserProvince = new List<int>(){} },
                          new UserViewModel { UserName ="pimolbutrm@yahoo.com", Email = "pimolbutrm@yahoo.com",Name ="อุมาพร พิมลบุตร",MinistryId =7, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "ผต.นร. ",Prefix = "นาง",Role_id =6,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "082823559",PhoneNumber = "0827006202",Telegraphnumber = "022811553",Housenumber = "",Rold = "",Alley = "", Postalcode = "",Side = "", Img = "",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {6,7},UserProvince = new List<int>(){} },
                          new UserViewModel { UserName ="fiem2548@gmail.com", Email = "fiem2548@gmail.com",Name ="อำพร ระดมบุญ",MinistryId =7, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "ผต.นร. ",Prefix = "นางสาว",Role_id =6,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "082823559",PhoneNumber = "0614173070",Telegraphnumber = "022811553",Housenumber = "",Rold = "",Alley = "", Postalcode = "",Side = "", Img = "",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {6,7},UserProvince = new List<int>(){} },
                          new UserViewModel { UserName ="userRole6n15@inspec.go.th", Email = "userRole6n15@inspec.go.th",Name ="สุวดี พิมพขันธ์",MinistryId =7, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "ผต.นร. ",Prefix = "นางสาว",Role_id =6,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "082823559",PhoneNumber = "0633932153",Telegraphnumber = "022811553",Housenumber = "",Rold = "",Alley = "", Postalcode = "",Side = "", Img = "",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {6,7},UserProvince = new List<int>(){} },

                    };

                
                foreach (var item in users) {
                    var user = new ApplicationUser { UserName = item.UserName, Email = item.Email };

                    var success = await _userManager.CreateAsync(user, "Admin@12345678").ConfigureAwait(false);

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
                    await _userManager.ConfirmEmailAsync(user, code).ConfigureAwait(false);

                    user.DistrictId = item.DistrictId;
                    user.ProvinceId = item.ProvinceId;
                    user.SubdistrictId = item.SubdistrictId;
                    user.MinistryId = item.MinistryId;
                    user.Position = item.Position;
                    user.Prefix = item.Prefix;
                    user.Name = item.Name;
                    user.Role_id = item.Role_id;
                    user.Educational = item.Educational;
                    user.Birthday = item.Birthday;
                    user.Officephonenumber = item.Officephonenumber;
                    user.PhoneNumber = item.PhoneNumber;
                    user.Telegraphnumber = item.Telegraphnumber;
                    user.Housenumber = item.Housenumber;
                    user.Rold = item.Rold;
                    user.Alley = item.Alley;
                    user.Postalcode = item.Postalcode;
                    user.Side = item.Side;
                    user.Img = item.Img;
                    user.CreatedAt = item.CreatedAt;
                    user.Startdate = item.Startdate;
                    user.Enddate = item.Enddate;
                    user.Active = item.Active;

                    _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();

                                
                        foreach (var item2 in item.UserRegion)
                        {
                            var userregiondata = new UserRegion
                            {
                                UserID = user.Id,
                                RegionId = item2
                            };
                            _context.UserRegions.Add(userregiondata);
                            _context.SaveChanges();
                        }

                    foreach (var item3 in item.UserProvince)
                    {
                        var userprovincedata = new UserProvince
                        {
                            UserID = user.Id,
                            ProvinceId = item3
                        };
                        _context.UserProvinces.Add(userprovincedata);
                        _context.SaveChanges();
                    }


                }
                result = "Success";             
            }
            else
            {
                result = "Fail";
            }

            return result;
        }

        [HttpGet("api/[controller]/[action]/{id}")]
        public IEnumerable<ApplicationUser> getuser(long id)
        {
            var users = _context.Users
                .Include(s => s.UserRegion)
                .ThenInclude(r => r.Region)
                .Where(m => m.Role_id == id);
                
            return users;
        }
    }
}