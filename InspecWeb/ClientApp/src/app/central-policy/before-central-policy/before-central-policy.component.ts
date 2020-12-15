import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { FiscalyearService } from 'src/app/services/fiscalyear.service';
import { FiscalyearnewService } from 'src/app/services/fiscalyearnew.service';

@Component({
  selector: 'app-before-central-policy',
  templateUrl: './before-central-policy.component.html',
  styleUrls: ['./before-central-policy.component.css']
})
export class BeforeCentralPolicyComponent implements OnInit {

  resultcentralpolicy: any = []
  resultcentralpolicyrold3: any = []
  resultfiscalyear: any = []
  delid: any
  modalRef: BsModalRef;
  dtOptions: any = {};
  loading = false;
  userid
  userministryId
  role_id
  currentyear
  selectfiscalyearid

  constructor(
    private router: Router,
    private centralpolicyservice: CentralpolicyService,
    private fiscalyearservice: FiscalyearService,
    private fiscalyearnewservice: FiscalyearnewService,
    private modalService: BsModalService,
    private spinner: NgxSpinnerService,
  ) { }

  ngOnInit() {
    this.spinner.show();

    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [8],
          orderable: false
        }
      ],
      "language": {
        "lengthMenu": "แสดง  _MENU_  รายการ",
        "search": "ค้นหา:",
        "infoFiltered": "ไม่พบข้อมูล",
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

    this.getFiscalyear()
  }

  getFiscalyear() {
    this.fiscalyearnewservice.getdata().subscribe(result => {
      this.resultfiscalyear = result
      var current_year = new Date().getFullYear() + 543;
      var current_date = new Date();
      let d3: any

      let fiscalyear = result.filter(result => {
        let start_date = new Date(result.startDate)
        let end_date = new Date(result.endDate)
        console.log(start_date.toISOString(), current_date.toISOString(), (current_date.toISOString() > start_date.toISOString()) && (current_date.toISOString() < end_date.toISOString()));

        return (current_date.toISOString() > start_date.toISOString()) && (current_date.toISOString() < end_date.toISOString())
      })[0]

      this.currentyear = fiscalyear ? fiscalyear : "allfiscalyear"
      console.log('this.currentyear', this.currentyear);

      if (this.currentyear == "allfiscalyear") {
        this.getCentralPolicy()
      } else {
        this.getCurrentCentralPolicy(this.currentyear)
      }

    })
  }
  getCentralPolicy() {
    this.loading = false
    this.resultcentralpolicy = []
    this.centralpolicyservice.getcentralpolicydata()
      .subscribe(async result => {
        const doAsync = () => {
          return new Promise((resolve, reject) => {
            setTimeout(() => {
              let array: any[] = []
              for (let i1 = 0; i1 < result.length; i1++) {
                console.log("result", result[0], i1);

                this.centralpolicyservice.getcentralpolicysubjectcount(result[i1].id).subscribe(resultCount => {
                  console.log('result[i1]', result[i1], i1);

                  if (result[i1].status == "ใช้งานจริง") {
                    console.log('ใช้งานจริง', { ...result[i1], count: resultCount });
                    array.push({ ...result[i1], count: resultCount });
                  }
                  else {
                    array.push({ ...result[i1], count: resultCount });
                  }
                })
              }
              resolve(array)
            }, 300)
          })
        }
        doAsync().then(res => {
          this.resultcentralpolicy = res
          setTimeout(() => {
            this.loading = true;

          }, 500)
        })
        this.spinner.hide();
      })
  }
  get result17() {
    console.log(this.resultcentralpolicy);

    return this.resultcentralpolicy.filter(result => result.id == 17)
  }
  getCurrentCentralPolicy(currentyear) {
    this.resultcentralpolicy = []
    this.centralpolicyservice.getcentralpolicyfiscalyeardata(currentyear.id)
      .subscribe(result => {
        const doAsync = () => {
          return new Promise((resolve, reject) => {
            let array: any[] = []
            for (let i1 = 0; i1 < result.length; i1++) {
              this.centralpolicyservice.getcentralpolicysubjectcount(result[i1].id)
                .subscribe((resultCount) => {
                  if (result[i1].status == "ใช้งานจริง") {
                    console.log('ใช้งานจริง', { ...result[i1], count: resultCount });
                    array.push({ ...result[i1], count: resultCount });
                  }
                  else {
                    console.log('ไม่ใช้งานจริง', { ...result[i1], count: resultCount });
                    array.push({ ...result[i1], count: resultCount });
                  }
                })
            }
            resolve(array)
          })
        }
        doAsync().then(res => {
          this.resultcentralpolicy = res
          setTimeout(() => {
            this.loading = true;

          }, 500)
        })
        console.log("23123123", this.resultcentralpolicy);

        this.spinner.hide();
      })
  }
  getSelectfiscalyear() {
    this.resultcentralpolicy = []
    this.centralpolicyservice.getcentralpolicyfiscalyeardata(this.selectfiscalyearid)
      .subscribe(result => {
        const doAsync = () => {
          return new Promise((resolve, reject) => {
            setTimeout(() => {
              let array: any[] = []
              for (let i1 = 0; i1 < result.length; i1++) {
                console.log("result", result[0], i1);

                this.centralpolicyservice.getcentralpolicysubjectcount(result[i1].id).subscribe(resultCount => {
                  console.log('result[i1]', result[i1], i1);

                  if (result[i1].status == "ใช้งานจริง") {
                    array.push({ ...result[i1], count: resultCount });
                  }
                  else {
                    array.push({ ...result[i1], count: resultCount });
                  }
                })
              }

              resolve(array)
            }, 300)
          })
        }
        doAsync().then(res => {
          this.resultcentralpolicy = res
          setTimeout(() => {
            this.loading = true;

          }, 500)
        })
        this.spinner.hide();
      })
  }
  Subject(id) {
    this.router.navigate(['/mainsubject', id])
  }
  DetailrowCentralPolicy(id: any) {
    this.router.navigate(['/maincentralpolicy/detailrowcentralpolicy', id])
  }

  selectfiscalyear(value) {
    console.log(value);
    if (value == "currentfiscalyear") {
      this.selectfiscalyearid = value
      this.loading = false;
      // this.getCurrentYear()
      this.spinner.show();
    }
    else if (value == "allfiscalyear") {
      this.selectfiscalyearid = value
      this.loading = false;
      this.getCentralPolicy()
      this.spinner.show();
    }
    else {
      this.selectfiscalyearid = value
      this.loading = false;
      this.getSelectfiscalyear()
      this.spinner.show();
      // this.router.navigate(['/centralpolicyfiscalyear/' + id])
    }
  }
}
