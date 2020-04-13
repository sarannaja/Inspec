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
                    Link = "http://www.bangkok.go.th/"
                },
                new Province
                {
                    Id = 2,
                    Name = "กระบี่",
                    Link = "http://www.krabi.go.th/"
                },
                 new Province
                 {
                     Id = 3,
                     Name = "กาญจนบุรี",
                     Link = "https://ww2.kanchanaburi.go.th/"
                 },
                  new Province
                  {
                      Id = 4,
                      Name = "กาฬสินธุ์",
                      Link = "http://www.kalasin.go.th/"
                  },
                  new Province
                  {
                      Id = 5,
                      Name = "กำแพงเพชร",
                      Link = "http://www.kamphaengphet.go.th/"
                  },
                  new Province
                  {
                      Id = 6,
                      Name = "ขอนแก่น",
                      Link = "http://www.khonkaen.go.th/"
                  },
                   new Province
                   {
                       Id = 7,
                       Name = "จันทบุรี",
                       Link = "http://www.chanthaburi.go.th/"
                   },
                   new Province
                   {
                       Id = 8,
                       Name = "ฉะเชิงเทรา",
                       Link = "http://www.chachoengsao.go.th/"
                   },
                   new Province
                   {
                       Id = 9,
                       Name = "ชลบุรี",
                       Link = "http://www.chonburi.go.th/"
                   },
                   new Province
                   {
                       Id = 10,
                       Name = "ชัยนาท",
                       Link = "http://www.chainat.go.th/"
                   },
                   new Province
                   {
                       Id = 11,
                       Name = "ชัยภูมิ",
                       Link = "http://www.chaiyaphum.go.th/"
                   },
                   new Province
                   {
                       Id = 12,
                       Name = "ชุมพร",
                       Link = "http://www.chumphon.go.th/"
                   },
                   new Province
                   {
                       Id = 13,
                       Name = "เชียงราย",
                       Link = "http://www.chiangrai.net/"
                   },
                   new Province
                   {
                       Id = 14,
                       Name = "เชียงใหม่",
                       Link = "http://www.chiangmai.go.th/"
                   },
                   new Province
                   {
                       Id = 15,
                       Name = "ตรัง",
                       Link = "http://www.trang.go.th/"
                   },
                   new Province
                   {
                       Id = 16,
                       Name = "ตราด",
                       Link = "http://www.trat.go.th/"
                   },
                   new Province
                   {
                       Id = 17,
                       Name = "ตาก",
                       Link = "http://www.tak.go.th/"
                   },
                   new Province
                   {
                       Id = 18,
                       Name = "นครนายก",
                       Link = "https://ww2.nakhonnayok.go.th/"
                   },
                   new Province
                   {
                       Id = 19,
                       Name = "นครปฐม",
                       Link = "http://www.nakhonpathom.go.th/"
                   },
                   new Province
                   {
                       Id = 20,
                       Name = "นครพนม",
                       Link = "http://www2.nakhonphanom.go.th/"
                   },
                   new Province
                   {
                       Id = 21,
                       Name = "นครราชสีมา",
                       Link = "http://www.nakhonratchasima.go.th/"
                   },
                   new Province
                   {
                       Id = 22,
                       Name = "นครศรีธรรมราช",
                       Link = "http://www.nakhonsithammarat.go.th/"
                   },
                   new Province
                   {
                       Id = 23,
                       Name = "นครสวรรค์",
                       Link = "http://www.nakhonsawan.go.th/"
                   },
                   new Province
                   {
                       Id = 24,
                       Name = "นนทบุรี",
                       Link = "http://nonthaburi.go.th/"
                   },
                   new Province
                   {
                       Id = 25,
                       Name = "นราธิวาส",
                       Link = "http://www.narathiwat.go.th/"
                   },
                   new Province
                   {
                       Id = 26,
                       Name = "น่าน",
                       Link = "http://www.nan.go.th/"
                   },
                   new Province
                   {
                       Id = 27,
                       Name = "บึงกาฬ",
                       Link = "http://www.buengkan.go.th/"
                   },
                   new Province
                   {
                       Id = 28,
                       Name = "บุรีรัมย์",
                       Link = "http://www.buriram.go.th/"
                   },
                   new Province
                   {
                       Id = 29,
                       Name = "ปทุมธานี",
                       Link = "http://www2.pathumthani.go.th/"
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
                       Link = "http://www.prachuapkhirikhan.go.th/"
                   },
                   new Province
                   {
                       Id = 32,
                       Name = "ปัตตานี",
                       Link = "http://www.pattani.go.th/"
                   },
                   new Province
                   {
                       Id = 33,
                       Name = "พระนครศรีอยุธยา",
                       Link = "https://ww2.ayutthaya.go.th/"
                   },
                   new Province
                   {
                       Id = 34,
                       Name = "พะเยา",
                       Link = "http://phayao.go.th/"
                   },
                   new Province
                   {
                       Id = 35,
                       Name = "พังงา",
                       Link = "http://www.phangnga.go.th/"
                   },
                   new Province
                   {
                       Id = 36,
                       Name = "พัทลุง",
                       Link = "http://www.phatthalung.go.th/"
                   },
                   new Province
                   {
                       Id = 37,
                       Name = "พิจิตร",
                       Link = "http://www.phichit.go.th/"
                   },
                   new Province
                   {
                       Id = 38,
                       Name = "พิษณุโลก",
                       Link = "http://www.phitsanulok.go.th/"
                   },
                   new Province
                   {
                       Id = 39,
                       Name = "เพชรบุรี",
                       Link = "http://www.phetchaburi.go.th/"
                   },
                   new Province
                   {
                       Id = 40,
                       Name = "เพชรบูรณ์",
                       Link = "http://www.phetchabun.go.th/"
                   },
                   new Province
                   {
                       Id = 41,
                       Name = "แพร่",
                       Link = "http://www.phrae.go.th/"
                   },
                   new Province
                   {
                       Id = 42,
                       Name = "ภูเก็ต",
                       Link = "https://www.phuket.go.th/"
                   },
                   new Province
                   {
                       Id = 43,
                       Name = "มหาสารคาม",
                       Link = "http://www.mahasarakham.go.th/"
                   },
                   new Province
                   {
                       Id = 44,
                       Name = "มุกดาหาร",
                       Link = "http://www.mukdahan.go.th/"
                   },
                   new Province
                   {
                       Id = 45,
                       Name = "แม่ฮ่องสอน",
                       Link = "http://www.maehongson.go.th/"
                   },
                   new Province
                   {
                       Id = 46,
                       Name = "ยโสธร",
                       Link = "http://www.yasothon.go.th/"
                   },
                   new Province
                   {
                       Id = 47,
                       Name = "ยะลา",
                       Link = "http://www.yala.go.th/"
                   },
                   new Province
                   {
                       Id = 48,
                       Name = "ร้อยเอ็ด",
                       Link = "http://www.roiet.go.th/"
                   },
                   new Province
                   {
                       Id = 49,
                       Name = "ระนอง",
                       Link = "http://www.ranong.go.th/"
                   },
                   new Province
                   {
                       Id = 50,
                       Name = "ระยอง",
                       Link = "http://www.rayong.go.th/"
                   },
                   new Province
                   {
                       Id = 51,
                       Name = "ราชบุรี",
                       Link = "http://www.ratchaburi.go.th/"
                   },
                   new Province
                   {
                       Id = 52,
                       Name = "ลพบุรี",
                       Link = "http://www.lopburi.go.th/"
                   },
                   new Province
                   {
                       Id = 53,
                       Name = "ลำปาง",
                       Link = "http://www.lampang.go.th/"
                   },
                   new Province
                   {
                       Id = 54,
                       Name = "ลำพูน",
                       Link = "https://www.lamphun.go.th/"
                   },
                   new Province
                   {
                       Id = 55,
                       Name = "เลย",
                       Link = "http://www.loei.go.th/"
                   },
                   new Province
                   {
                       Id = 56,
                       Name = "ศรีสะเกษ",
                       Link = "http://www.sisaket.go.th/"
                   },
                   new Province
                   {
                       Id = 57,
                       Name = "สกลนคร",
                       Link = "http://sakonnakhon.go.th/"
                   },
                   new Province
                   {
                       Id = 58,
                       Name = "สงขลา",
                       Link = "https://www.songkhla.go.th/"
                   },
                   new Province
                   {
                       Id = 59,
                       Name = "สตูล",
                       Link = "http://www.satun.go.th/"
                   },
                   new Province
                   {
                       Id = 60,
                       Name = "สมุทรปราการ",
                       Link = "http://www.samutprakan.go.th/"
                   },
                   new Province
                   {
                       Id = 61,
                       Name = "สมุทรสงคราม",
                       Link = "http://www.samutsongkhram.go.th/"
                   },
                   new Province
                   {
                       Id = 62,
                       Name = "สมุทรสาคร",
                       Link = "http://www.samutsakhon.go.th/"
                   },
                   new Province
                   {
                       Id = 63,
                       Name = "สระแก้ว",
                       Link = "http://www.sakaeo.go.th/"
                   },
                   new Province
                   {
                       Id = 64,
                       Name = "สระบุรี",
                       Link = "http://www.saraburi.go.th/"
                   },
                   new Province
                   {
                       Id = 65,
                       Name = "สิงห์บุรี",
                       Link = "http://www.singburi.go.th/"
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
                       Link = "http://www.suphanburi.go.th/"
                   },
                   new Province
                   {
                       Id = 68,
                       Name = "สุราษฎร์ธานี",
                       Link = "http://www.suratthani.go.th/"
                   },
                   new Province
                   {
                       Id = 69,
                       Name = "สุรินทร์",
                       Link = "http://www.surin.go.th/"
                   },
                   new Province
                   {
                       Id = 70,
                       Name = "หนองคาย",
                       Link = "http://www.nongkhai.go.th/"
                   },
                   new Province
                   {
                       Id = 71,
                       Name = "หนองบัวลำภู",
                       Link = "http://www.nongbualamphu.go.th/"
                   },
                   new Province
                   {
                       Id = 72,
                       Name = "อ่างทอง",
                       Link = "http://www.angthong.go.th/"
                   },
                   new Province
                   {
                       Id = 73,
                       Name = "อำนาจเจริญ",
                       Link = "http://www.amnatcharoen.go.th/"
                   },
                   new Province
                   {
                       Id = 74,
                       Name = "อุดรธานี",
                       Link = "http://www.udonthani.go.th/"
                   },
                   new Province
                   {
                       Id = 75,
                       Name = "อุตรดิตถ์",
                       Link = "http://www.uttaradit.go.th/"
                   },
                   new Province
                   {
                       Id = 76,
                       Name = "อุทัยธานี",
                       Link = "http://www.uthaithani.go.th/"
                   },
                   new Province
                   {
                       Id = 77,
                       Name = "อุบลราชธานี",
                       Link = "http://www.ubonratchathani.go.th/"
                   }
            );
        }
    }
}