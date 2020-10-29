import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { every, result } from 'lodash';
import { TrainingService } from '../services/training.service';
import { PeopleModel } from './peoplemodel';
import * as _ from 'lodash';

@Component({
  selector: 'app-name-plate',
  templateUrl: './name-plate.component.html',
  styleUrls: ['./name-plate.component.css']
})
export class NamePlateComponent implements OnInit {

  dtOptions: DataTables.Settings = {};
  people: any[] = [];
  loading = false;
  trainingid: any;
  trainingRegisterData: PeopleModel[] = [];
  printData: PeopleModel[] = [];
  printDataGroup: any[] = [];
  hide = false;
  url: any;
  onPrint = false
  ctx = { estimate: this.printData };
  constructor(
    private activatedRoute: ActivatedRoute,
    private trainingservice: TrainingService,
    private router: Router,
    @Inject('BASE_URL') baseUrl: string,
  ) {
    this.url = baseUrl,
      this.trainingid = activatedRoute.snapshot.paramMap.get('id');
  }

  ngOnInit() {
    this.dtOptions = {
      pagingType: 'full_numbers',

      ordering: false,
      "language": {
        "lengthMenu": "แสดง  _MENU_  รายการ",
        "search": "ค้นหา:",
        "info": "แสดง _START_ ถึง _END_ จาก _TOTAL_ แถว",
        "infoEmpty": "แสดง 0 ของ 0 รายการ",
        "zeroRecords": "ไม่พบข้อมูล",
        "paginate": {
          "first": "หน้าแรก",
          "last": "หน้าสุดท้าย",
          "next": "ต่อไป",
          "previous": "ย้อนกลับ"
        },
      }
    };
    this.getPeopleRegistered();
  }

  getPeopleRegistered() {
    this.trainingservice.getregistertrainingdata(this.trainingid).subscribe(res => {
      console.log("traningRegister: ", res);
      this.trainingRegisterData = res.filter(result => {
        return result.status == 1
      })
      // for (let i = 0; i < 100; i++) {
      //   this.trainingRegisterData.push(res[0])
      // }
      // this.trainingRegisterData.map((item, i) => ({ ...item, id: i + 1 }))
      this.loading = true

    })
  }
  public checkPeople(id) {
    return this.people.find(res => res == id)
  }
  addsPeoples(value) {
    // //console.log('item.id');
    // var subject = value.vaule
    this.people = this.addPeople(this.people, value)
    console.log("test => ", this.people);


  }

  addPeople(array: any[], value) {
    var distinctThings: any[] = array.filter(
      (thing, i, arr) => arr.findIndex(t => t === value) === i
    );
    // //console.log('distinctThings', distinctThings);
    if (distinctThings.length != 0) {
      var s = new Set(array);
      s.delete(value);
      this.printData = this.printData.filter(res => res.id != value)
      this.Peoplegroup(this.printData)
      // delete_people.delete()
      return Array.from(s);
    } else {
      var s = new Set(array);
      s.add(value);
      this.printToCart(value)
      return Array.from(s);
    }
  }

  Peoplegroup(printData: PeopleModel[] = []) {
    let perpage = 6
    const grouped = (printData) => {
      return new Promise((resolve, reject) => {
        const group = printData
          .map((result, index) => {
            let logic = (index + 1) % perpage
            let palmgroup =
              ((index + 1) <= perpage ? 1 :
                logic != 0 ? ((index + 1) / perpage) :
                  ((index + 1) / perpage)).toFixed()
            console.log('logic', palmgroup);
            return { ...result, palmgroup }
          })
        let data = Object.values(_.groupBy(group, "palmgroup"))
        resolve(data)
      })
    }
    grouped(printData).then((result: any) => {
      console.log(result);

      this.printDataGroup = result
    })

  }
  gotoPreview() {
    this.router.navigate(['/nameplatepreview', { selectedPeople: this.people }]);
  }

  gotoLabelPreview() {
    // this.router.navigate(['/namelabelpreview', { selectedPeople: this.people }]);
    let printContents, popupWin;
    printContents = document.getElementById('pdfDownload').innerHTML;
    popupWin = window.open('', '_blank', 'top=0,left=0,height=100%,width=auto');
    popupWin.document.open();
    popupWin.document.write(`
      <html>
        <head>
          <title>Print tab</title>
          <style>
          //........Customized style.......
          </style>
        </head>
        <body onload="window.print();window.close()">${printContents}</body>
      </html>`
    );
    popupWin.document.close();
  }

  gotoBack() {
    window.history.back();
  }
  printToCart(id) {
    this.trainingservice.printNamePlatebyPalm(id).subscribe(async res => {
      console.log("PrintData: ", res);
      try {
        await this.printData.push(res)
        this.Peoplegroup(this.printData)
      }
      catch (err) {
        console.log(err);

      }
      // this.printData = res;

      // this.print(res)
    });

  }
  addAllPeoples() {
    if (this.people.length == 0) {
      for (let i = 0; i < this.trainingRegisterData.length; i++) {
        this.addsPeoples(this.trainingRegisterData[i].id)
      }
    } else if (this.trainingRegisterData.length == this.people.length) {
      for (let i = 0; i < this.trainingRegisterData.length; i++) {
        this.addsPeoples(this.trainingRegisterData[i].id)
      }
    } else {
      let noadd: any[] =
        this.trainingRegisterData.filter(result => this.people.every(id => result.id != id))
      console.log(noadd);

      for (let i = 0; i < noadd.length; i++) {
        this.addsPeoples(noadd[i].id)
      }
    }

  }

  print() {
    window.print()
  }

}
