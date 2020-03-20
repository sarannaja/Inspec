using InspecWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InspecWeb.Data.Seeders
{   
    public class UserSeeder : IEntityTypeConfiguration<ApplicationUser>
    {    
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var users = new object[][] {
                new object[] 
                { 
                    1L, "นาง", "ชลาลัย  สุขสถิตย์", "ปริญญาโท บริหารธุรกิจมหาบัณฑิตมหาวิทยาลัยรามคำแหง",
                    DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), 
                    null, "0817538056", null,"chalalia@hotmail.com","819", "ถนนสามเสน",null, "10300","สังคม"
                    ,1L,1L,2L,17L,null,DateTime.Now 
                },
                 new object[]
                {
                    2L, "นาง", "ฐานิตา  แพร่วานิชย์", "ปริญญาโท ศิลปศาสตร์มหาบัณฑิตมหาวิทยาลัยรามคำแหง",
                    DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),
                    null, "0898135138", null,"-","249/22 หมู่บ้านสัมมากรโครงการ 2", "ถนนรามคำแหง","รามคำแหง 41", "10300","สังคม"
                    ,1L,1L,2L,17L,null,DateTime.Now
                },
            };
            var userPlaces = new List<ApplicationUser>();
            foreach(var user in users)
            {           
                    var userPlace = new ApplicationUser()
                {
                   // Id = (long)user[0],
                    UserName = (string)user[1],
                    Email = (string)user[2],
                    Prefix = (string)user[3],
                    Name = (string)user[4],
                    Role_id = (long)user[5],
                    Educational = (string)user[6],
                    Birthday = (DateTime)user[7] ,
                    Officephonenumber = (string)user[8],
                    PhoneNumber = (string)user[9],
                    Telegraphnumber = (string)user[10],                   
                    Housenumber = (string)user[11],
                    Rold = (string)user[12],
                    Alley = (string)user[13],
                    Postalcode = (string)user[14],
                    Side = (string)user[15],                   
                    ProvinceId = (long)user[16],
                    DistrictId = (long)user[17],
                    SubdistrictId = (long)user[18],
                    Img = (string)user[19],
                    CreatedAt = (DateTime)user[20],
                };

                userPlaces.Add(userPlace);
            }

            builder.HasData(userPlaces.ToArray()
            //new Userpalace
            //{
            //    Id = 1,
            //    Prefix = "นาง",
            //    Name = "กรุงเทพมหานคร",
            //    Educational = "",
            //    Birthday = DateTime.ParseExact("2015/06/15 13:45:00", "yyyy/MM/dd HH:mm:ss", null),
            //    Officephonenumber ="2",
            //    Phonenumber ="2",
            //    Telegraphnumber ="",
            //    Address = "",
            //    Postalcode = "",
            //    RegionId = 1,
            //    ProvinceId = 1,
            //    DistrictId = 1,
            //    SubdistrictId = 1,
            //    Img = "",
            //    CreatedAt = DateTime.Now,
            //}
            );
        }
    }
}