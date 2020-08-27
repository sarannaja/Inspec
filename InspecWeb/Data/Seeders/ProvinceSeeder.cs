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
                    NameEN = "bangkok",
                    ShortnameEN = "bbk",
                    Link = "http://www.bangkok.go.th/"
                },
                new Province
                {
                    Id = 2,
                    SectorId = 2,
                    ProvincesGroupId = 7,
                    Name = "กระบี่",
                    NameEN = "krabi",
                    ShortnameEN = "kbi",
                    Link = "http://www.krabi.go.th/"
                },
                 new Province
                 {
                     Id = 3,
                     SectorId = 1,
                     ProvincesGroupId = 4,
                     Name = "กาญจนบุรี",
                     NameEN = "kanchanaburi",
                     ShortnameEN = "kri",
                     Link = "https://ww2.kanchanaburi.go.th/"
                 },
                  new Province
                  {
                      Id = 4,
                      SectorId = 4,
                      ProvincesGroupId = 13,
                      Name = "กาฬสินธุ์",
                      NameEN = "kalasin",
                      ShortnameEN = "ksn",
                      Link = "http://www.kalasin.go.th/"
                  },
                  new Province
                  {
                      Id = 5,
                      SectorId = 5,
                      ProvincesGroupId = 19,
                      Name = "กำแพงเพชร",
                      NameEN = "kamphaengphet",
                      ShortnameEN = "kpt",
                      Link = "http://www.kamphaengphet.go.th/"
                  },
                  new Province
                  {
                      Id = 6,
                      SectorId = 4,
                      ProvincesGroupId = 13,
                      Name = "ขอนแก่น",
                      NameEN = "khonkaen",
                      ShortnameEN = "kkn",
                      Link = "http://www.khonkaen.go.th/"
                  },
                   new Province
                   {
                       Id = 7,
                       SectorId = 3,
                       ProvincesGroupId = 10,
                       Name = "จันทบุรี",
                       NameEN = "chanthaburi",
                       ShortnameEN = "cti",
                       Link = "http://www.chanthaburi.go.th/"
                   },
                   new Province
                   {
                       Id = 8,
                       SectorId = 3,
                       ProvincesGroupId = 9,
                       Name = "ฉะเชิงเทรา",
                       NameEN = "chachoengsao",
                       ShortnameEN = "cco",
                       Link = "http://www.chachoengsao.go.th/"
                   },
                   new Province
                   {
                       Id = 9,
                       SectorId = 3,
                       ProvincesGroupId = 9,
                       Name = "ชลบุรี",
                       NameEN = "chonburi",
                       ShortnameEN = "cbi",
                       Link = "http://www.chonburi.go.th/"
                   },
                   new Province
                   {
                       Id = 10,
                       SectorId = 1,
                       ProvincesGroupId = 2,
                       Name = "ชัยนาท",
                       NameEN = "chainat",
                       ShortnameEN = "cnt",
                       Link = "http://www.chainat.go.th/"
                   },
                   new Province
                   {
                       Id = 11,
                       SectorId = 1,
                       ProvincesGroupId = 14,
                       Name = "ชัยภูมิ",
                       NameEN = "chaiyaphum",
                       ShortnameEN = "cpm",
                       Link = "http://www.chaiyaphum.go.th/"
                   },
                   new Province
                   {
                       Id = 12,
                       SectorId = 2,
                       ProvincesGroupId = 6,
                       Name = "ชุมพร",
                       NameEN = "chumphon",
                       ShortnameEN = "cpn",
                       Link = "http://www.chumphon.go.th/"
                   },
                   new Province
                   {
                       Id = 13,
                       SectorId = 5,
                       ProvincesGroupId = 17,
                       Name = "เชียงราย",
                       NameEN = "chiangrai",
                       ShortnameEN = "cri",
                       Link = "http://www.chiangrai.net/"
                   },
                   new Province
                   {
                       Id = 14,
                       SectorId = 5,
                       ProvincesGroupId = 16,
                       Name = "เชียงใหม่",
                       NameEN = "chiangmai",
                       ShortnameEN = "cmi",
                       Link = "http://www.chiangmai.go.th/"
                   },
                   new Province
                   {
                       Id = 15,
                       SectorId = 2,
                       ProvincesGroupId = 7,
                       Name = "ตรัง",
                       NameEN = "trang",
                       ShortnameEN = "",
                       Link = "http://www.trang.go.th/"
                   },
                   new Province
                   {
                       Id = 16,
                       SectorId = 3,
                       ProvincesGroupId = 10,
                       Name = "ตราด",
                       NameEN = "trat",
                       ShortnameEN = "trg",
                       Link = "http://www.trat.go.th/"
                   },
                   new Province
                   {
                       Id = 17,
                       SectorId = 5,
                       ProvincesGroupId = 18,
                       Name = "ตาก",
                       NameEN = "tak",
                       ShortnameEN = "tak",
                       Link = "http://www.tak.go.th/"
                   },
                   new Province
                   {
                       Id = 18,
                       SectorId = 3,
                       ProvincesGroupId = 10,
                       Name = "นครนายก",
                       NameEN = "nakhonnayok",
                       ShortnameEN = "nyk",
                       Link = "https://ww2.nakhonnayok.go.th/"
                   },
                   new Province
                   {
                       Id = 19,
                       SectorId = 1,
                       ProvincesGroupId = 3,
                       Name = "นครปฐม",
                       NameEN = "nakhonpathom",
                       ShortnameEN = "npt",
                       Link = "http://www.nakhonpathom.go.th/"
                   },
                   new Province
                   {
                       Id = 20,
                       SectorId = 4,
                       ProvincesGroupId = 12,
                       Name = "นครพนม",
                       NameEN = "nakhonphanom",
                       ShortnameEN = "npm",
                       Link = "http://www2.nakhonphanom.go.th/"
                   },
                   new Province
                   {
                       Id = 21,
                       SectorId = 4,
                       ProvincesGroupId = 14,
                       Name = "นครราชสีมา",
                       NameEN = "nakhonratchasima",
                       ShortnameEN = "nma",
                       Link = "http://www.nakhonratchasima.go.th/"
                   },
                   new Province
                   {
                       Id = 22,
                       SectorId = 2,
                       ProvincesGroupId = 6,
                       Name = "นครศรีธรรมราช",
                       NameEN = "nakhonsithammarat",
                       ShortnameEN = "nrt",
                       Link = "http://www.nakhonsithammarat.go.th/"
                   },
                   new Province
                   {
                       Id = 23,
                       SectorId = 5,
                       ProvincesGroupId = 19,
                       Name = "นครสวรรค์",
                       NameEN = "nakhonsawan",
                       ShortnameEN = "nsn",
                       Link = "http://www.nakhonsawan.go.th/"
                   },
                   new Province
                   {
                       Id = 24,
                       SectorId = 1,
                       ProvincesGroupId = 3,
                       Name = "นนทบุรี",
                       NameEN = "nonthaburi",
                       ShortnameEN = "nbi",
                       Link = "http://nonthaburi.go.th/"
                   },
                   new Province
                   {
                       Id = 25,
                       SectorId = 2,
                       ProvincesGroupId = 8,
                       Name = "นราธิวาส",
                       NameEN = "narathiwat",
                       ShortnameEN = "nwt",
                       Link = "http://www.narathiwat.go.th/"
                   },
                   new Province
                   {
                       Id = 26,
                       SectorId = 5,
                       ProvincesGroupId = 17,
                       Name = "น่าน",
                       NameEN = "nan",
                       ShortnameEN = "nan",
                       Link = "http://www.nan.go.th/"
                   },
                   new Province
                   {
                       Id = 27,
                       SectorId = 3,
                       ProvincesGroupId = 11,
                       Name = "บึงกาฬ",
                       NameEN = "buengkan",
                       ShortnameEN = "bkn",
                       Link = "http://www.buengkan.go.th/"
                   },
                   new Province
                   {
                       Id = 28,
                       SectorId = 3,
                       ProvincesGroupId = 14,
                       Name = "บุรีรัมย์",
                       NameEN = "buriram",
                       ShortnameEN = "brm",
                       Link = "http://www.buriram.go.th/"
                   },
                   new Province
                   {
                       Id = 29,
                       SectorId = 1,
                       ProvincesGroupId = 3,
                       Name = "ปทุมธานี",
                       NameEN = "pathumthani",
                       ShortnameEN = "pte",
                       Link = "http://www2.pathumthani.go.th/"
                   },
                   new Province
                   {
                       Id = 30,
                       SectorId = 2,
                       ProvincesGroupId = 5,
                       Name = "ประจวบคีรีขันธ์",
                       NameEN = "prachuapkhirikhan",
                       ShortnameEN = "pkn",
                       Link = "http://www.prachuapkhirikhan.go.th/"
                   },
                   new Province
                   {
                       Id = 31,
                       SectorId = 3,
                       ProvincesGroupId = 10,
                       Name = "ปราจีนบุรี",
                       NameEN = "prachuapkhirikhan",
                       ShortnameEN = "pri",
                       Link = "http://www.prachuapkhirikhan.go.th/"
                   },
                   new Province
                   {
                       Id = 32,
                       SectorId = 2,
                       ProvincesGroupId = 8,
                       Name = "ปัตตานี",
                       NameEN = "pattani",
                       ShortnameEN = "ptn",
                       Link = "http://www.pattani.go.th/"
                   },
                   new Province
                   {
                       Id = 33,
                       SectorId = 1,
                       ProvincesGroupId = 2,
                       Name = "พระนครศรีอยุธยา",
                       NameEN = "ayutthaya",
                       ShortnameEN = "AYA",
                       Link = "https://ww2.ayutthaya.go.th/"
                   },
                   new Province
                   {
                       Id = 34,
                       SectorId = 5,
                       ProvincesGroupId = 17,
                       Name = "พะเยา",
                       NameEN = "phayao",
                       ShortnameEN = "pyo",
                       Link = "http://phayao.go.th/"
                   },
                   new Province
                   {
                       Id = 35,
                       SectorId = 2,
                       ProvincesGroupId = 7,
                       Name = "พังงา",
                       NameEN = "phangnga",
                       ShortnameEN = "",
                       Link = "http://www.phangnga.go.th/"
                   },
                   new Province
                   {
                       Id = 36,
                       SectorId = 2,
                       ProvincesGroupId = 6,
                       Name = "พัทลุง",
                       NameEN = "phatthalung",
                       ShortnameEN = "pna",
                       Link = "http://www.phatthalung.go.th/"
                   },
                   new Province
                   {
                       Id = 37,
                       SectorId = 5,
                       ProvincesGroupId = 19,
                       Name = "พิจิตร",
                       NameEN = "phichit",
                       ShortnameEN = "pct",
                       Link = "http://www.phichit.go.th/"
                   },
                   new Province
                   {
                       Id = 38,
                       SectorId = 5,
                       ProvincesGroupId = 18,
                       Name = "พิษณุโลก",
                       NameEN = "phitsanulok",
                       ShortnameEN = "plk",
                       Link = "http://www.phitsanulok.go.th/"
                   },
                   new Province
                   {
                       Id = 39,
                       SectorId = 1,
                       ProvincesGroupId = 5,
                       Name = "เพชรบุรี",
                       NameEN = "phetchaburi",
                       ShortnameEN = "pbi",
                       Link = "http://www.phetchaburi.go.th/"
                   },
                   new Province
                   {
                       Id = 40,
                       SectorId = 5,
                       ProvincesGroupId = 19,
                       Name = "เพชรบูรณ์",
                       NameEN = "phetchabun",
                       ShortnameEN = "pnb",
                       Link = "http://www.phetchabun.go.th/"
                   },
                   new Province
                   {
                       Id = 41,
                       SectorId = 5,
                       ProvincesGroupId = 17,
                       Name = "แพร่",
                       NameEN = "phrae",
                       ShortnameEN = "pre",
                       Link = "http://www.phrae.go.th/"
                   },
                   new Province
                   {
                       Id = 42,
                       SectorId = 2,
                       ProvincesGroupId = 7,
                       Name = "ภูเก็ต",
                       NameEN = "phuket",
                       ShortnameEN = "pkt",
                       Link = "https://www.phuket.go.th/"
                   },
                   new Province
                   {
                       Id = 43,
                       SectorId = 4,
                       ProvincesGroupId = 13,
                       Name = "มหาสารคาม",
                       NameEN = "mahasarakham",
                       ShortnameEN = "mkm",
                       Link = "http://www.mahasarakham.go.th/"
                   },
                   new Province
                   {
                       Id = 44,
                       SectorId = 4,
                       ProvincesGroupId = 12,
                       Name = "มุกดาหาร",
                       NameEN = "mukdahan",
                       ShortnameEN = "mdh",
                       Link = "http://www.mukdahan.go.th/"
                   },
                   new Province
                   {
                       Id = 45,
                       SectorId = 5,
                       ProvincesGroupId = 16,
                       Name = "แม่ฮ่องสอน",
                       NameEN = "maehongson",
                       ShortnameEN = "msn",
                       Link = "http://www.maehongson.go.th/"
                   },
                   new Province
                   {
                       Id = 46,
                       SectorId = 4,
                       ProvincesGroupId = 15,
                       Name = "ยโสธร",
                       NameEN = "yasothon",
                       ShortnameEN = "yst",
                       Link = "http://www.yasothon.go.th/"
                   },
                   new Province
                   {
                       Id = 47,
                       SectorId = 2,
                       ProvincesGroupId = 8,
                       Name = "ยะลา",
                       NameEN = "yala",
                       ShortnameEN = "yla",
                       Link = "http://www.yala.go.th/"
                   },
                   new Province
                   {
                       Id = 48,
                       SectorId = 4,
                       ProvincesGroupId = 13,
                       Name = "ร้อยเอ็ด",
                       NameEN = "roiet",
                       ShortnameEN = "ret",
                       Link = "http://www.roiet.go.th/"
                   },
                   new Province
                   {
                       Id = 49,
                       SectorId = 2,
                       ProvincesGroupId = 7,
                       Name = "ระนอง",
                       NameEN = "ranong",
                       ShortnameEN = "rng",
                       Link = "http://www.ranong.go.th/"
                   },
                   new Province
                   {
                       Id = 50,
                       SectorId = 3,
                       ProvincesGroupId = 9,
                       Name = "ระยอง",
                       NameEN = "rayong",
                       ShortnameEN = "ryg",
                       Link = "http://www.rayong.go.th/"
                   },
                   new Province
                   {
                       Id = 51,
                       SectorId = 1,
                       ProvincesGroupId = 4,
                       Name = "ราชบุรี",
                       NameEN = "ratchaburi",
                       ShortnameEN = "rbr",
                       Link = "http://www.ratchaburi.go.th/"
                   },
                   new Province
                   {
                       Id = 52,
                       SectorId = 1,
                       ProvincesGroupId = 2,
                       Name = "ลพบุรี",
                       NameEN = "lopburi",
                       ShortnameEN = "lri",
                       Link = "http://www.lopburi.go.th/"
                   },
                   new Province
                   {
                       Id = 53,
                       SectorId = 5,
                       ProvincesGroupId = 16,
                       Name = "ลำปาง",
                       NameEN = "lampang",
                       ShortnameEN = "lpg",
                       Link = "http://www.lampang.go.th/"
                   },
                   new Province
                   {
                       Id = 54,
                       SectorId = 5,
                       ProvincesGroupId = 16,
                       Name = "ลำพูน",
                       NameEN = "lamphun",
                       ShortnameEN = "lpn",
                       Link = "https://www.lamphun.go.th/"
                   },
                   new Province
                   {
                       Id = 55,
                       SectorId = 4,
                       ProvincesGroupId = 11,
                       Name = "เลย",
                       NameEN = "loei",
                       ShortnameEN = "lei",
                       Link = "http://www.loei.go.th/"
                   },
                   new Province
                   {
                       Id = 56,
                       SectorId = 4,
                       ProvincesGroupId = 15,
                       Name = "ศรีสะเกษ",
                       NameEN = "sisaket",
                       ShortnameEN = "ssk",
                       Link = "http://www.sisaket.go.th/"
                   },
                   new Province
                   {
                       Id = 57,
                       SectorId = 4,
                       ProvincesGroupId = 12,
                       Name = "สกลนคร",
                       NameEN = "sakonnakhon",
                       ShortnameEN = "snk",
                       Link = "http://sakonnakhon.go.th/"
                   },
                   new Province
                   {
                       Id = 58,
                       SectorId = 2,
                       ProvincesGroupId = 6,
                       Name = "สงขลา",
                       NameEN = "songkhla",
                       ShortnameEN = "ska",
                       Link = "https://www.songkhla.go.th/"
                   },
                   new Province
                   {
                       Id = 59,
                       SectorId = 2,
                       ProvincesGroupId = 7,
                       Name = "สตูล",
                       NameEN = "satun",
                       ShortnameEN = "stn",
                       Link = "http://www.satun.go.th/"
                   },
                   new Province
                   {
                       Id = 60,
                       SectorId = 1,
                       ProvincesGroupId = 3,
                       Name = "สมุทรปราการ",
                       NameEN = "samutprakan",
                       ShortnameEN = "spk",
                       Link = "http://www.samutprakan.go.th/"
                   },
                   new Province
                   {
                       Id = 61,
                       SectorId = 1,
                       ProvincesGroupId = 5,
                       Name = "สมุทรสงคราม",
                       NameEN = "samutsongkhram",
                       ShortnameEN = "",
                       Link = "http://www.samutsongkhram.go.th/"
                   },
                   new Province
                   {
                       Id = 62,
                       SectorId = 1,
                       ProvincesGroupId = 5,
                       Name = "สมุทรสาคร",
                       NameEN = "samutsakhon",
                       ShortnameEN = "skm",
                       Link = "http://www.samutsakhon.go.th/"
                   },
                   new Province
                   {
                       Id = 63,
                       SectorId = 3,
                       ProvincesGroupId = 10,
                       Name = "สระแก้ว",
                       NameEN = "sakaeo",
                       ShortnameEN = "skw",
                       Link = "http://www.sakaeo.go.th/"
                   },
                   new Province
                   {
                       Id = 64,
                       SectorId = 1,
                       ProvincesGroupId = 2,
                       Name = "สระบุรี",
                       NameEN = "saraburi",
                       ShortnameEN = "sri",
                       Link = "http://www.saraburi.go.th/"
                   },
                   new Province
                   {
                       Id = 65,
                       SectorId = 1,
                       ProvincesGroupId = 2,
                       Name = "สิงห์บุรี",
                       NameEN = "singburi",
                       ShortnameEN = "sbr",
                       Link = "http://www.singburi.go.th/"
                   },
                   new Province
                   {
                       Id = 66,
                       SectorId = 5,
                       ProvincesGroupId = 18,
                       Name = "สุโขทัย",
                       NameEN = "sukhothai",
                       ShortnameEN = "sti",
                       Link = "http://www.sukhothai.go.th/"
                   },
                   new Province
                   {
                       Id = 67,
                       SectorId = 1,
                       ProvincesGroupId = 5,
                       Name = "สุพรรณบุรี",
                       NameEN = "suphanburi",
                       ShortnameEN = "spb",
                       Link = "http://www.suphanburi.go.th/"
                   },
                   new Province
                   {
                       Id = 68,
                       SectorId = 2,
                       ProvincesGroupId = 7,
                       Name = "สุราษฎร์ธานี",
                       NameEN = "suratthani",
                       ShortnameEN = "sni",
                       Link = "http://www.suratthani.go.th/"
                   },
                   new Province
                   {
                       Id = 69,
                       SectorId = 4,
                       ProvincesGroupId = 15,
                       Name = "สุรินทร์",
                       NameEN = "surin",
                       ShortnameEN = "srn",
                       Link = "http://www.surin.go.th/"
                   },
                   new Province
                   {
                       Id = 70,
                       SectorId = 4,
                       ProvincesGroupId = 11,
                       Name = "หนองคาย",
                       NameEN = "nongkhai",
                       ShortnameEN = "nki",
                       Link = "http://www.nongkhai.go.th/"
                   },
                   new Province
                   {
                       Id = 71,
                       SectorId = 4,
                       ProvincesGroupId = 11,
                       Name = "หนองบัวลำภู",
                       NameEN = "nongbualamphu",
                       ShortnameEN = "nbp",
                       Link = "http://www.nongbualamphu.go.th/"
                   },
                   new Province
                   {
                       Id = 72,
                       SectorId = 1,
                       ProvincesGroupId = 3,
                       Name = "อ่างทอง",
                       NameEN = "angthong",
                       ShortnameEN = "atg",
                       Link = "http://www.angthong.go.th/"
                   },
                   new Province
                   {
                       Id = 73,
                       SectorId = 4,
                       ProvincesGroupId = 14,
                       Name = "อำนาจเจริญ",
                       NameEN = "amnatcharoen",
                       ShortnameEN = "acr",
                       Link = "http://www.amnatcharoen.go.th/"
                   },
                   new Province
                   {
                       Id = 74,
                       SectorId = 4,
                       ProvincesGroupId = 11,
                       Name = "อุดรธานี",
                       NameEN = "udonthani",
                       ShortnameEN = "udn",
                       Link = "http://www.udonthani.go.th/"
                   },
                   new Province
                   {
                       Id = 75,
                       SectorId = 5,
                       ProvincesGroupId = 18,
                       Name = "อุตรดิตถ์",
                       NameEN = "uttaradit",
                       ShortnameEN = "utd",
                       Link = "http://www.uttaradit.go.th/"
                   },
                   new Province
                   {
                       Id = 76,
                       SectorId = 5,
                       ProvincesGroupId = 19,
                       Name = "อุทัยธานี",
                       NameEN = "uthaithani",
                       ShortnameEN = "uti",
                       Link = "http://www.uthaithani.go.th/"
                   },
                   new Province
                   {
                       Id = 77,
                       SectorId = 4,
                       ProvincesGroupId = 15,
                       Name = "อุบลราชธานี",
                       NameEN = "ubonratchathani",
                       ShortnameEN = "ubn",
                       Link = "http://www.ubonratchathani.go.th/"
                   }
            );
        }
    }
}