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
                new Province{ Id = 1,
                    SectorId = 1,
                    ProvincesGroupId = 1,
                    Name = "กรุงเทพมหานคร",
                    Link = "http://www.bangkok.go.th/"
                },
                new Province
                {
                    Id = 2,
                    SectorId = 2,
                    ProvincesGroupId = 7,
                    Name = "กระบี่",
                    Link = "http://www.krabi.go.th/"
                },
                 new Province
                 {
                     Id = 3,
                     SectorId = 1,
                     ProvincesGroupId = 4,
                     Name = "กาญจนบุรี",
                     Link = "https://ww2.kanchanaburi.go.th/"
                 },
                  new Province
                  {
                      Id = 4,
                      SectorId = 4,
                      ProvincesGroupId = 13,
                      Name = "กาฬสินธุ์",
                      Link = "http://www.kalasin.go.th/"
                  },
                  new Province
                  {
                      Id = 5,
                      SectorId = 5,
                      ProvincesGroupId = 19,
                      Name = "กำแพงเพชร",
                      Link = "http://www.kamphaengphet.go.th/"
                  },
                  new Province
                  {
                      Id = 6,
                      SectorId = 4,
                      ProvincesGroupId = 13,
                      Name = "ขอนแก่น",
                      Link = "http://www.khonkaen.go.th/"
                  },
                   new Province
                   {
                       Id = 7,
                       SectorId = 3,
                       ProvincesGroupId = 10,
                       Name = "จันทบุรี",
                       Link = "http://www.chanthaburi.go.th/"
                   },
                   new Province
                   {
                       Id = 8,
                       SectorId = 3,
                       ProvincesGroupId = 9,
                       Name = "ฉะเชิงเทรา",
                       Link = "http://www.chachoengsao.go.th/"
                   },
                   new Province
                   {
                       Id = 9,
                       SectorId = 3,
                       ProvincesGroupId = 9,
                       Name = "ชลบุรี",
                       Link = "http://www.chonburi.go.th/"
                   },
                   new Province
                   {
                       Id = 10,
                       SectorId = 1,
                       ProvincesGroupId = 2,
                       Name = "ชัยนาท",
                       Link = "http://www.chainat.go.th/"
                   },
                   new Province
                   {
                       Id = 11,
                       SectorId = 1,
                       ProvincesGroupId = 14,
                       Name = "ชัยภูมิ",
                       Link = "http://www.chaiyaphum.go.th/"
                   },
                   new Province
                   {
                       Id = 12,
                       SectorId = 2,
                       ProvincesGroupId = 6,
                       Name = "ชุมพร",
                       Link = "http://www.chumphon.go.th/"
                   },
                   new Province
                   {
                       Id = 13,
                       SectorId = 5,
                       ProvincesGroupId = 17,
                       Name = "เชียงราย",
                       Link = "http://www.chiangrai.net/"
                   },
                   new Province
                   {
                       Id = 14,
                       SectorId = 5,
                       ProvincesGroupId = 16,
                       Name = "เชียงใหม่",
                       Link = "http://www.chiangmai.go.th/"
                   },
                   new Province
                   {
                       Id = 15,
                       SectorId = 2,
                       ProvincesGroupId = 7,
                       Name = "ตรัง",
                       Link = "http://www.trang.go.th/"
                   },
                   new Province
                   {
                       Id = 16,
                       SectorId = 3,
                       ProvincesGroupId = 10,
                       Name = "ตราด",
                       Link = "http://www.trat.go.th/"
                   },
                   new Province
                   {
                       Id = 17,
                       SectorId = 5,
                       ProvincesGroupId = 18,
                       Name = "ตาก",
                       Link = "http://www.tak.go.th/"
                   },
                   new Province
                   {
                       Id = 18,
                       SectorId = 3,
                       ProvincesGroupId = 10,
                       Name = "นครนายก",
                       Link = "https://ww2.nakhonnayok.go.th/"
                   },
                   new Province
                   {
                       Id = 19,
                       SectorId = 1,
                       ProvincesGroupId = 3,
                       Name = "นครปฐม",
                       Link = "http://www.nakhonpathom.go.th/"
                   },
                   new Province
                   {
                       Id = 20,
                       SectorId = 4,
                       ProvincesGroupId = 12,
                       Name = "นครพนม",
                       Link = "http://www2.nakhonphanom.go.th/"
                   },
                   new Province
                   {
                       Id = 21,
                       SectorId = 4,
                       ProvincesGroupId = 14,
                       Name = "นครราชสีมา",
                       Link = "http://www.nakhonratchasima.go.th/"
                   },
                   new Province
                   {
                       Id = 22,
                       SectorId = 2,
                       ProvincesGroupId = 6,
                       Name = "นครศรีธรรมราช",
                       Link = "http://www.nakhonsithammarat.go.th/"
                   },
                   new Province
                   {
                       Id = 23,
                       SectorId = 5,
                       ProvincesGroupId = 19,
                       Name = "นครสวรรค์",
                       Link = "http://www.nakhonsawan.go.th/"
                   },
                   new Province
                   {
                       Id = 24,
                       SectorId = 1,
                       ProvincesGroupId = 3,
                       Name = "นนทบุรี",
                       Link = "http://nonthaburi.go.th/"
                   },
                   new Province
                   {
                       Id = 25,
                       SectorId = 2,
                       ProvincesGroupId = 8,
                       Name = "นราธิวาส",
                       Link = "http://www.narathiwat.go.th/"
                   },
                   new Province
                   {
                       Id = 26,
                       SectorId = 5,
                       ProvincesGroupId = 17,
                       Name = "น่าน",
                       Link = "http://www.nan.go.th/"
                   },
                   new Province
                   {
                       Id = 27,
                       SectorId = 3,
                       ProvincesGroupId = 11,
                       Name = "บึงกาฬ",
                       Link = "http://www.buengkan.go.th/"
                   },
                   new Province
                   {
                       Id = 28,
                       SectorId = 3,
                       ProvincesGroupId = 14,
                       Name = "บุรีรัมย์",
                       Link = "http://www.buriram.go.th/"
                   },
                   new Province
                   {
                       Id = 29,
                       SectorId = 1,
                       ProvincesGroupId = 3,
                       Name = "ปทุมธานี",
                       Link = "http://www2.pathumthani.go.th/"
                   },
                   new Province
                   {
                       Id = 30,
                       SectorId = 2,
                       ProvincesGroupId = 5,
                       Name = "ประจวบคีรีขันธ์",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 31,
                       SectorId = 3,
                       ProvincesGroupId = 10,
                       Name = "ปราจีนบุรี",
                       Link = "http://www.prachuapkhirikhan.go.th/"
                   },
                   new Province
                   {
                       Id = 32,
                       SectorId = 2,
                       ProvincesGroupId = 8,
                       Name = "ปัตตานี",
                       Link = "http://www.pattani.go.th/"
                   },
                   new Province
                   {
                       Id = 33,
                       SectorId = 1,
                       ProvincesGroupId = 2,
                       Name = "พระนครศรีอยุธยา",
                       Link = "https://ww2.ayutthaya.go.th/"
                   },
                   new Province
                   {
                       Id = 34,
                       SectorId = 5,
                       ProvincesGroupId = 17,
                       Name = "พะเยา",
                       Link = "http://phayao.go.th/"
                   },
                   new Province
                   {
                       Id = 35,
                       SectorId = 2,
                       ProvincesGroupId = 7,
                       Name = "พังงา",
                       Link = "http://www.phangnga.go.th/"
                   },
                   new Province
                   {
                       Id = 36,
                       SectorId = 2,
                       ProvincesGroupId = 6,
                       Name = "พัทลุง",
                       Link = "http://www.phatthalung.go.th/"
                   },
                   new Province
                   {
                       Id = 37,
                       SectorId = 5,
                       ProvincesGroupId = 19,
                       Name = "พิจิตร",
                       Link = "http://www.phichit.go.th/"
                   },
                   new Province
                   {
                       Id = 38,
                       SectorId = 5,
                       ProvincesGroupId = 18,
                       Name = "พิษณุโลก",
                       Link = "http://www.phitsanulok.go.th/"
                   },
                   new Province
                   {
                       Id = 39,
                       SectorId = 1,
                       ProvincesGroupId = 5,
                       Name = "เพชรบุรี",
                       Link = "http://www.phetchaburi.go.th/"
                   },
                   new Province
                   {
                       Id = 40,
                       SectorId = 5,
                       ProvincesGroupId = 19,
                       Name = "เพชรบูรณ์",
                       Link = "http://www.phetchabun.go.th/"
                   },
                   new Province
                   {
                       Id = 41,
                       SectorId = 5,
                       ProvincesGroupId = 17,
                       Name = "แพร่",
                       Link = "http://www.phrae.go.th/"
                   },
                   new Province
                   {
                       Id = 42,
                       SectorId = 2,
                       ProvincesGroupId = 7,
                       Name = "ภูเก็ต",
                       Link = "https://www.phuket.go.th/"
                   },
                   new Province
                   {
                       Id = 43,
                       SectorId = 4,
                       ProvincesGroupId = 13,
                       Name = "มหาสารคาม",
                       Link = "http://www.mahasarakham.go.th/"
                   },
                   new Province
                   {
                       Id = 44,
                       SectorId = 4,
                       ProvincesGroupId = 12,
                       Name = "มุกดาหาร",
                       Link = "http://www.mukdahan.go.th/"
                   },
                   new Province
                   {
                       Id = 45,
                       SectorId = 5,
                       ProvincesGroupId = 16,
                       Name = "แม่ฮ่องสอน",
                       Link = "http://www.maehongson.go.th/"
                   },
                   new Province
                   {
                       Id = 46,
                       SectorId = 4,
                       ProvincesGroupId = 15,
                       Name = "ยโสธร",
                       Link = "http://www.yasothon.go.th/"
                   },
                   new Province
                   {
                       Id = 47,
                       SectorId = 2,
                       ProvincesGroupId = 8,
                       Name = "ยะลา",
                       Link = "http://www.yala.go.th/"
                   },
                   new Province
                   {
                       Id = 48,
                       SectorId = 4,
                       ProvincesGroupId = 13,
                       Name = "ร้อยเอ็ด",
                       Link = "http://www.roiet.go.th/"
                   },
                   new Province
                   {
                       Id = 49,
                       SectorId = 2,
                       ProvincesGroupId = 7,
                       Name = "ระนอง",
                       Link = "http://www.ranong.go.th/"
                   },
                   new Province
                   {
                       Id = 50,
                       SectorId = 3,
                       ProvincesGroupId = 9,
                       Name = "ระยอง",
                       Link = "http://www.rayong.go.th/"
                   },
                   new Province
                   {
                       Id = 51,
                       SectorId = 1,
                       ProvincesGroupId = 4,
                       Name = "ราชบุรี",
                       Link = "http://www.ratchaburi.go.th/"
                   },
                   new Province
                   {
                       Id = 52,
                       SectorId = 1,
                       ProvincesGroupId = 2,
                       Name = "ลพบุรี",
                       Link = "http://www.lopburi.go.th/"
                   },
                   new Province
                   {
                       Id = 53,
                       SectorId = 5,
                       ProvincesGroupId = 16,
                       Name = "ลำปาง",
                       Link = "http://www.lampang.go.th/"
                   },
                   new Province
                   {
                       Id = 54,
                       SectorId = 5,
                       ProvincesGroupId = 16,
                       Name = "ลำพูน",
                       Link = "https://www.lamphun.go.th/"
                   },
                   new Province
                   {
                       Id = 55,
                       SectorId = 4,
                       ProvincesGroupId = 11,
                       Name = "เลย",
                       Link = "http://www.loei.go.th/"
                   },
                   new Province
                   {
                       Id = 56,
                       SectorId = 4,
                       ProvincesGroupId = 15,
                       Name = "ศรีสะเกษ",
                       Link = "http://www.sisaket.go.th/"
                   },
                   new Province
                   {
                       Id = 57,
                       SectorId = 4,
                       ProvincesGroupId = 12,
                       Name = "สกลนคร",
                       Link = "http://sakonnakhon.go.th/"
                   },
                   new Province
                   {
                       Id = 58,
                       SectorId = 2,
                       ProvincesGroupId = 6,
                       Name = "สงขลา",
                       Link = "https://www.songkhla.go.th/"
                   },
                   new Province
                   {
                       Id = 59,
                       SectorId = 2,
                       ProvincesGroupId = 7,
                       Name = "สตูล",
                       Link = "http://www.satun.go.th/"
                   },
                   new Province
                   {
                       Id = 60,
                       SectorId = 1,
                       ProvincesGroupId = 3,
                       Name = "สมุทรปราการ",
                       Link = "http://www.samutprakan.go.th/"
                   },
                   new Province
                   {
                       Id = 61,
                       SectorId = 1,
                       ProvincesGroupId = 5,
                       Name = "สมุทรสงคราม",
                       Link = "http://www.samutsongkhram.go.th/"
                   },
                   new Province
                   {
                       Id = 62,
                       SectorId = 1,
                       ProvincesGroupId = 5,
                       Name = "สมุทรสาคร",
                       Link = "http://www.samutsakhon.go.th/"
                   },
                   new Province
                   {
                       Id = 63,
                       SectorId = 3,
                       ProvincesGroupId = 10,
                       Name = "สระแก้ว",
                       Link = "http://www.sakaeo.go.th/"
                   },
                   new Province
                   {
                       Id = 64,
                       SectorId = 1,
                       ProvincesGroupId = 2,
                       Name = "สระบุรี",
                       Link = "http://www.saraburi.go.th/"
                   },
                   new Province
                   {
                       Id = 65,
                       SectorId = 1,
                       ProvincesGroupId = 2,
                       Name = "สิงห์บุรี",
                       Link = "http://www.singburi.go.th/"
                   },
                   new Province
                   {
                       Id = 66,
                       SectorId = 5,
                       ProvincesGroupId = 18,
                       Name = "สุโขทัย",
                       Link = "https://www.google.co.th/"
                   },
                   new Province
                   {
                       Id = 67,
                       SectorId = 1,
                       ProvincesGroupId = 5,
                       Name = "สุพรรณบุรี",
                       Link = "http://www.suphanburi.go.th/"
                   },
                   new Province
                   {
                       Id = 68,
                       SectorId = 2,
                       ProvincesGroupId = 7,
                       Name = "สุราษฎร์ธานี",
                       Link = "http://www.suratthani.go.th/"
                   },
                   new Province
                   {
                       Id = 69,
                       SectorId = 4,
                       ProvincesGroupId = 15,
                       Name = "สุรินทร์",
                       Link = "http://www.surin.go.th/"
                   },
                   new Province
                   {
                       Id = 70,
                       SectorId = 4,
                       ProvincesGroupId = 11,
                       Name = "หนองคาย",
                       Link = "http://www.nongkhai.go.th/"
                   },
                   new Province
                   {
                       Id = 71,
                       SectorId = 4,
                       ProvincesGroupId = 11,
                       Name = "หนองบัวลำภู",
                       Link = "http://www.nongbualamphu.go.th/"
                   },
                   new Province
                   {
                       Id = 72,
                       SectorId = 1,
                       ProvincesGroupId = 3,
                       Name = "อ่างทอง",
                       Link = "http://www.angthong.go.th/"
                   },
                   new Province
                   {
                       Id = 73,
                       SectorId = 4,
                       ProvincesGroupId = 14,
                       Name = "อำนาจเจริญ",
                       Link = "http://www.amnatcharoen.go.th/"
                   },
                   new Province
                   {
                       Id = 74,
                       SectorId = 4,
                       ProvincesGroupId = 11,
                       Name = "อุดรธานี",
                       Link = "http://www.udonthani.go.th/"
                   },
                   new Province
                   {
                       Id = 75,
                       SectorId = 5,
                       ProvincesGroupId = 18,
                       Name = "อุตรดิตถ์",
                       Link = "http://www.uttaradit.go.th/"
                   },
                   new Province
                   {
                       Id = 76,
                       SectorId = 5,
                       ProvincesGroupId = 19,
                       Name = "อุทัยธานี",
                       Link = "http://www.uthaithani.go.th/"
                   },
                   new Province
                   {
                       Id = 77,
                       SectorId = 4,
                       ProvincesGroupId = 15,
                       Name = "อุบลราชธานี",
                       Link = "http://www.ubonratchathani.go.th/"
                   }
            );
        }
    }
}