using InspecWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InspecWeb.Data.Seeders
{
    public class ProvinceSeeder : IEntityTypeConfiguration<Province>
    {

        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.HasData(
                new Province
                {
                    Id = 1,
                    Name = "กรุงเทพมหานคร",
                    Link = "https://www.google.co.th/"
                },
                new Province
                {
                    Id = 2,
                    Name = "กระบี่",
                    Link = "https://www.google.co.th/"
                },
                 new Province
                 {
                     Id = 3,
                     Name = "กาญจนบุรี",
                     Link = "https://www.google.co.th/"
                 },
                  new Province
                  {
                      Id = 4,
                      Name = "กาฬสินธุ์",
                      Link = "https://www.google.co.th/"
                  },
                  new Province
                  {
                      Id = 5,
                      Name = "กำแพงเพชร",
                      Link = "https://www.google.co.th/"
                  },
                  new Province
                  {
                      Id = 6,
                      Name = "ขอนแก่น",
                      Link = "https://www.google.co.th/"
                  },
                   new Province
                   {
                       Id = 7,
                       Name = "จันทบุรี",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 8,
                       Name = "ฉะเชิงเทรา",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 9,
                       Name = "ชลบุรี",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 10,
                       Name = "ชัยนาท",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 11,
                       Name = "ชัยภูมิ",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 12,
                       Name = "ชุมพร",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 13,
                       Name = "เชียงราย",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 14,
                       Name = "เชียงใหม่",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 15,
                       Name = "ตรัง",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 16,
                       Name = "ตราด",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 17,
                       Name = "ตาก",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 18,
                       Name = "นครนายก",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 19,
                       Name = "นครปฐม",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 20,
                       Name = "นครพนม",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 21,
                       Name = "นครราชสีมา",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 22,
                       Name = "นครศรีธรรมราช",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 23,
                       Name = "นครสวรรค์",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 24,
                       Name = "นนทบุรี",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 25,
                       Name = "นราธิวาส",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 26,
                       Name = "น่าน",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 27,
                       Name = "บึงกาฬ",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 28,
                       Name = "บุรีรัมย์",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 29,
                       Name = "ปทุมธานี",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 30,
                       Name = "ประจวบคีรีขันธ์",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 31,
                       Name = "ปราจีนบุรี",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 32,
                       Name = "ปัตตานี",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 33,
                       Name = "พระนครศรีอยุธยา",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 34,
                       Name = "พะเยา",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 35,
                       Name = "พังงา",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 36,
                       Name = "พัทลุง",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 37,
                       Name = "พิจิตร",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 38,
                       Name = "พิษณุโลก",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 39,
                       Name = "เพชรบุรี",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 40,
                       Name = "เพชรบูรณ์",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 41,
                       Name = "แพร่",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 42,
                       Name = "ภูเก็ต",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 43,
                       Name = "มหาสารคาม",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 44,
                       Name = "มุกดาหาร",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 45,
                       Name = "แม่ฮ่องสอน",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 46,
                       Name = "ยโสธร",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 47,
                       Name = "ยะลา",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 48,
                       Name = "ร้อยเอ็ด",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 49,
                       Name = "ระนอง",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 50,
                       Name = "ระยอง",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 51,
                       Name = "ราชบุรี",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 52,
                       Name = "ลพบุรี",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 53,
                       Name = "ลำปาง",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 54,
                       Name = "ลำพูน",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 55,
                       Name = "เลย",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 56,
                       Name = "ศรีสะเกษ",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 57,
                       Name = "สกลนคร",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 58,
                       Name = "สงขลา",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 59,
                       Name = "สตูล",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 60,
                       Name = "สมุทรปราการ",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 61,
                       Name = "สมุทรสงคราม",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 62,
                       Name = "สมุทรสาคร",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 63,
                       Name = "สระแก้ว",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 64,
                       Name = "สระบุรี",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 65,
                       Name = "สิงห์บุรี",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 66,
                       Name = "สุโขทัย",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 67,
                       Name = "สุพรรณบุรี",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 68,
                       Name = "สุราษฎร์ธานี",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 69,
                       Name = "สุรินทร์",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 70,
                       Name = "หนองคาย",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 71,
                       Name = "หนองบัวลำภู",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 72,
                       Name = "อ่างทอง",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 73,
                       Name = "อำนาจเจริญ",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 74,
                       Name = "อุดรธานี",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 75,
                       Name = "อุตรดิตถ์",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 76,
                       Name = "อุทัยธานี",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 77,
                       Name = "อุบลราชธานี",
                       Link = "https://www.google.co.th/"
                   }
            );
        }
    }
}