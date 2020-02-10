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
                },
                new Province
                {
                    Id = 2,
                    Name = "กระบี่",
                },
                 new Province
                 {
                     Id = 3,
                     Name = "กาญจนบุรี",
                 },
                  new Province
                  {
                      Id = 4,
                      Name = "กาฬสินธุ์",
                  },
                  new Province
                  {
                      Id = 5,
                      Name = "กำแพงเพชร",
                  },
                  new Province
                  {
                      Id = 6,
                      Name = "ขอนแก่น",
                  },
                   new Province
                   {
                       Id = 7,
                       Name = "จันทบุรี",
                   },
                   new Province
                   {
                       Id = 8,
                       Name = "ฉะเชิงเทรา",
                   },
                   new Province
                   {
                       Id = 9,
                       Name = "ชลบุรี",
                   },
                   new Province
                   {
                       Id = 10,
                       Name = "ชัยนาท",
                   },
                   new Province
                   {
                       Id = 11,
                       Name = "ชัยภูมิ",
                   },
                   new Province
                   {
                       Id = 12,
                       Name = "ชุมพร",
                   },
                   new Province
                   {
                       Id = 13,
                       Name = "เชียงราย",
                   },
                   new Province
                   {
                       Id = 14,
                       Name = "เชียงใหม่",
                   },
                   new Province
                   {
                       Id = 15,
                       Name = "ตรัง",
                   },
                   new Province
                   {
                       Id = 16,
                       Name = "ตราด",
                   },
                   new Province
                   {
                       Id = 17,
                       Name = "ตาก",
                   },
                   new Province
                   {
                       Id = 18,
                       Name = "นครนายก",
                   },
                   new Province
                   {
                       Id = 19,
                       Name = "นครปฐม",
                   },
                   new Province
                   {
                       Id = 20,
                       Name = "นครพนม",
                   },
                   new Province
                   {
                       Id = 21,
                       Name = "นครราชสีมา",
                   },
                   new Province
                   {
                       Id = 22,
                       Name = "นครศรีธรรมราช",
                   },
                   new Province
                   {
                       Id = 23,
                       Name = "นครสวรรค์",
                   },
                   new Province
                   {
                       Id = 24,
                       Name = "นนทบุรี",
                   },
                   new Province
                   {
                       Id = 25,
                       Name = "นราธิวาส",
                   },
                   new Province
                   {
                       Id = 26,
                       Name = "น่าน",
                   },
                   new Province
                   {
                       Id = 27,
                       Name = "บึงกาฬ",
                   },
                   new Province
                   {
                       Id = 28,
                       Name = "บุรีรัมย์",
                   },
                   new Province
                   {
                       Id = 29,
                       Name = "ปทุมธานี",
                   },
                   new Province
                   {
                       Id = 30,
                       Name = "ประจวบคีรีขันธ์",
                   },
                   new Province
                   {
                       Id = 31,
                       Name = "ปราจีนบุรี",
                   },
                   new Province
                   {
                       Id = 32,
                       Name = "ปัตตานี",
                   },
                   new Province
                   {
                       Id = 33,
                       Name = "พระนครศรีอยุธยา",
                   },
                   new Province
                   {
                       Id = 34,
                       Name = "พะเยา",
                   },
                   new Province
                   {
                       Id = 35,
                       Name = "พังงา",
                   },
                   new Province
                   {
                       Id = 36,
                       Name = "พัทลุง",
                   },
                   new Province
                   {
                       Id = 37,
                       Name = "พิจิตร",
                   },
                   new Province
                   {
                       Id = 38,
                       Name = "พิษณุโลก",
                   },
                   new Province
                   {
                       Id = 39,
                       Name = "เพชรบุรี",
                   },
                   new Province
                   {
                       Id = 40,
                       Name = "เพชรบูรณ์",
                   },
                   new Province
                   {
                       Id = 41,
                       Name = "แพร่",
                   },
                   new Province
                   {
                       Id = 42,
                       Name = "ภูเก็ต",
                   },
                   new Province
                   {
                       Id = 43,
                       Name = "มหาสารคาม",
                   },
                   new Province
                   {
                       Id = 44,
                       Name = "มุกดาหาร",
                   },
                   new Province
                   {
                       Id = 45,
                       Name = "แม่ฮ่องสอน",
                   },
                   new Province
                   {
                       Id = 46,
                       Name = "ยโสธร",
                   },
                   new Province
                   {
                       Id = 47,
                       Name = "ยะลา",
                   },
                   new Province
                   {
                       Id = 48,
                       Name = "ร้อยเอ็ด",
                   },
                   new Province
                   {
                       Id = 49,
                       Name = "ระนอง",
                   },
                   new Province
                   {
                       Id = 50,
                       Name = "ระยอง",
                   },
                   new Province
                   {
                       Id = 51,
                       Name = "ราชบุรี",
                   },
                   new Province
                   {
                       Id = 52,
                       Name = "ลพบุรี",
                   },
                   new Province
                   {
                       Id = 53,
                       Name = "ลำปาง",
                   },
                   new Province
                   {
                       Id = 54,
                       Name = "ลำพูน",
                   },
                   new Province
                   {
                       Id = 55,
                       Name = "เลย",
                   },
                   new Province
                   {
                       Id = 56,
                       Name = "ศรีสะเกษ",
                   },
                   new Province
                   {
                       Id = 57,
                       Name = "สกลนคร",
                   },
                   new Province
                   {
                       Id = 58,
                       Name = "สงขลา",
                   },
                   new Province
                   {
                       Id = 59,
                       Name = "สตูล",
                   },
                   new Province
                   {
                       Id = 60,
                       Name = "สมุทรปราการ",
                   },
                   new Province
                   {
                       Id = 61,
                       Name = "สมุทรสงคราม",
                   },
                   new Province
                   {
                       Id = 62,
                       Name = "สมุทรสาคร",
                   },
                   new Province
                   {
                       Id = 63,
                       Name = "สระแก้ว",
                   },
                   new Province
                   {
                       Id = 64,
                       Name = "สระบุรี",
                   },
                   new Province
                   {
                       Id = 65,
                       Name = "สิงห์บุรี",
                   },
                   new Province
                   {
                       Id = 66,
                       Name = "สุโขทัย",
                   },
                   new Province
                   {
                       Id = 67,
                       Name = "สุพรรณบุรี",
                   },
                   new Province
                   {
                       Id = 68,
                       Name = "สุราษฎร์ธานี",
                   },
                   new Province
                   {
                       Id = 69,
                       Name = "สุรินทร์",
                   },
                   new Province
                   {
                       Id = 70,
                       Name = "หนองคาย",
                   },
                   new Province
                   {
                       Id = 71,
                       Name = "หนองบัวลำภู",
                   },
                   new Province
                   {
                       Id = 72,
                       Name = "อ่างทอง",
                   },
                   new Province
                   {
                       Id = 73,
                       Name = "อำนาจเจริญ",
                   },
                   new Province
                   {
                       Id = 74,
                       Name = "อุดรธานี",
                   },
                   new Province
                   {
                       Id = 75,
                       Name = "อุตรดิตถ์",
                   },
                   new Province
                   {
                       Id = 76,
                       Name = "อุทัยธานี",
                   },
                   new Province
                   {
                       Id = 77,
                       Name = "อุบลราชธานี",
                   }
            );
        }
    }
}