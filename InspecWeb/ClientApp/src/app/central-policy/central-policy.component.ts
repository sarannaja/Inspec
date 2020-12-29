import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { CentralpolicyService } from '../services/centralpolicy.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from "ngx-spinner";
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { UserService } from '../services/user.service';
import { FiscalyearService } from '../services/fiscalyear.service';
import { NotofyService } from '../services/notofy.service';
import { FiscalyearnewService } from '../services/fiscalyearnew.service';

@Component({
  selector: 'app-central-policy',
  templateUrl: './central-policy.component.html',
  styleUrls: ['./central-policy.component.css']
})
export class CentralPolicyComponent implements OnInit {

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
    private authorize: AuthorizeService,
    private userService: UserService,
    private spinner: NgxSpinnerService,
    private _NotofyService: NotofyService) { }

  ngOnInit() {
    this.spinner.show();

    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
        // alert(this.userid)

      })
    setTimeout(() => {
      this.userService.getuserfirstdata(this.userid)
        .subscribe(result => {
          // this.resultuser = result;
          //console.log("test" , this.resultuser);
          this.role_id = result[0].role_id
          this.userministryId = result[0].ministryId
          if (result[0].role_id == 1 || result[0].role_id == 2) {
            console.log("in1", this.role_id);
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
          } else {
            console.log("in2", this.role_id);

            this.dtOptions = {
              pagingType: 'full_numbers',
              columnDefs: [
                {
                  targets: [7],
                  orderable: false
                }
              ],
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
          }

          // alert(this.role_id)
        })
      this.getFiscalyear()
    }, 200)




    // this.getCurrentYear()
  }

  openModal(template: TemplateRef<any>, id) {
    this.delid = id;
    this.modalRef = this.modalService.show(template);
  }
  getFiscalyear() {
    this.fiscalyearnewservice.getdata().subscribe(result => {
      this.resultfiscalyear = result
      var current_year = new Date().getFullYear() + 543;
      var current_date = new Date();
      let d3: any
      // this.currentyear = result.filter(result => {
      //   let start_date = new Date(result.startDate).toISOString()
      //   console.log(current_date, start_date);
      //   d3 = (current_date > start_date ? current_year + 1 : current_year)
      //   console.log('d3', d3);

      //   return result.year == d3
      // })[0]

      let fiscalyear = result.filter(result => {
        let start_date = new Date(result.startDate)
        let end_date = new Date(result.endDate)
        // console.log('start_date > current_date', start_date.toISOString(), current_date.toISOString());
        console.log(start_date.toISOString(), current_date.toISOString(), (current_date.toISOString() > start_date.toISOString()) && (current_date.toISOString() < end_date.toISOString()));


        // return start_date.toISOString() < current_date.toISOString()
        return (current_date.toISOString() > start_date.toISOString()) && (current_date.toISOString() < end_date.toISOString())
      })[0]

      this.currentyear = fiscalyear ? fiscalyear : "allfiscalyear"
      // this.currentyear = this.currentyear[this.currentyear.length - 1]
      console.log('this.currentyear', this.currentyear);

      if (this.currentyear == "allfiscalyear") {
        this.getCentralPolicy()
      } else {
        this.getCurrentCentralPolicy(this.currentyear)
      }

      // this.getCentralPolicy()
    })
  }
  // getCurrentYear() {
  //   this.fiscalyearservice.getcurrentyeardata().subscribe(result => {
  //     this.currentyear = result
  //   })
  // }
  getCentralPolicy() {
    this.loading = false
    this.resultcentralpolicy = []
    this.centralpolicyservice.getcentralpolicydata()
      .subscribe(async result => {
        // this.resultcentralpolicy = result.map(result2=>{
        //   return
        // })
        const doAsync = () => {
          return new Promise((resolve, reject) => {
            setTimeout(() => {
              let array: any[] = []
              for (let i1 = 0; i1 < result.length; i1++) {
                //console.log('this.id[i1]', this.id[i1]);
                console.log("result", result[0], i1);

                this.centralpolicyservice.getcentralpolicysubjectcount(result[i1].id).subscribe(resultCount => {
                  console.log('result[i1]', result[i1], i1);

                  if (this.role_id != 1 && this.role_id != 2 && result[i1].status == "ใช้งานจริง") {
                    array.push({ ...result[i1], count: resultCount });
                  } else if (this.role_id == 1 || this.role_id == 2) {
                    array.push({ ...result[i1], count: resultCount });
                  }
                })
                // }, 100 * i1 + 1)
              }
              resolve(array)
              // return
            }, 300)
          })
        }
        doAsync().then(res => {
          this.resultcentralpolicy = res
          setTimeout(() => {
            this.loading = true;

          }, 500)
        })
        // if (this.role_id != 1 && this.role_id == 2) {
        //   // this.resultcentralpolicy = []
        //   // result.forEach(element => {
        //   //   // if (element.status == "ใช้งานจริง") {
        //   //   //   this.resultcentralpolicy.push(element);
        //   //   // }
        //   //   this.resultcentralpolicy.push({ ...element, count: 0 });
        //   // });

        //   doAsync().then(res => {
        //     this.resultcentralpolicy = res
        //     setTimeout(() => {
        //       this.loading = true;

        //     }, 100)
        //   })
        //   console.log("data", this.resultcentralpolicy);
        // }


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
        // console.log("eiei",result);
        const doAsync = () => {
          return new Promise((resolve, reject) => {
            // setTimeout(() => {
            let array: any[] = []
            for (let i1 = 0; i1 < result.length; i1++) {
              //console.log('this.id[i1]', this.id[i1]);
              // console.log("result", result[0], i1);

              this.centralpolicyservice.getcentralpolicysubjectcount(result[i1].id)
                .subscribe((resultCount) => {
                  // console.log('result[i1]', result[i1], i1);
                  // if (result[i1].status == "ใช้งานจริง") {
                  if (this.role_id != 1 && this.role_id != 2 && result[i1].status == "ใช้งานจริง") {
                    console.log('role_id != 1,2', { ...result[i1], count: resultCount });

                    array.push({ ...result[i1], count: resultCount });
                  } else if (this.role_id == 1) {
                    console.log('role_id == 1', { ...result[i1], count: resultCount });
                    array.push({ ...result[i1], count: resultCount });
                  } else if (this.role_id == 2 && (this.userministryId == result[i1].user.ministryId) ||
                    (result[i1].status == "ใช้งานจริง" && this.userministryId != result[i1].user.ministryId)) {
                    console.log('role_id == 2', { ...result[i1], count: resultCount });
                    array.push({ ...result[i1], count: resultCount });
                  }
                  // }
                })


              // }, 100 * i1 + 1)
            }

            resolve(array)
            // return
            // }, 300)
          })
        }
        doAsync().then(res => {
          this.resultcentralpolicy = res
          setTimeout(() => {
            this.loading = true;

          }, 500)
        })
        // if (this.role_id != 1 && this.role_id != 2) {

        //   doAsync().then(res => {
        //     this.resultcentralpolicy = res
        //     setTimeout(() => {
        //       this.loading = true;

        //     }, 1000)
        //   })
        // }

        // this.loading = true;
        this.spinner.hide();
      })
  }
  getSelectfiscalyear() {
    this.resultcentralpolicy = []
    this.centralpolicyservice.getcentralpolicyfiscalyeardata(this.selectfiscalyearid)
      .subscribe(result => {
        // this.resultcentralpolicy = result
        const doAsync = () => {
          return new Promise((resolve, reject) => {
            setTimeout(() => {
              let array: any[] = []
              for (let i1 = 0; i1 < result.length; i1++) {
                //console.log('this.id[i1]', this.id[i1]);
                console.log("result", result[0], i1);

                this.centralpolicyservice.getcentralpolicysubjectcount(result[i1].id).subscribe(resultCount => {
                  console.log('result[i1]', result[i1], i1);

                  if (this.role_id != 1 && this.role_id != 2 && result[i1].status == "ใช้งานจริง") {
                    array.push({ ...result[i1], count: resultCount });
                  } else if (this.role_id == 1 || this.role_id == 2) {
                    array.push({ ...result[i1], count: resultCount });
                  }
                })


                // }, 100 * i1 + 1)
              }

              resolve(array)
              // return
            }, 300)
          })
        }
        doAsync().then(res => {
          this.resultcentralpolicy = res
          setTimeout(() => {
            this.loading = true;

          }, 500)
        })
        // if (this.role_id != 1 && this.role_id != 2) {
        //   // this.resultcentralpolicy = []
        //   // result.forEach(element => {
        //   //   // if (element.status == "ใช้งานจริง") {
        //   //   //   this.resultcentralpolicy.push(element);
        //   //   // }
        //   //   this.resultcentralpolicy.push(element);
        //   // });
        //   // console.log("data", this.resultcentralpolicy);
        //   doAsync().then(res => {
        //     this.resultcentralpolicy = res
        //     setTimeout(() => {
        //       this.loading = true;

        //     }, 100)
        //   })
        // }

        // this.loading = true;
        this.spinner.hide();
      })
  }
  deleteCentralPolicy(value) {
    this.loading = false;
    this.centralpolicyservice.deleteCentralPolicy(value, this.userid).subscribe(response => {
      console.log(value);
      console.log(this.selectfiscalyearid);
      this._NotofyService.onSuccess("ลบข้อมูล")
      this.modalRef.hide()
      if (this.selectfiscalyearid == "currentfiscalyear") {
        console.log("1");
        this.getCurrentCentralPolicy(this.currentyear);
        this.spinner.show();
      } else if (this.selectfiscalyearid == "allfiscalyear") {
        console.log("2");
        this.getCentralPolicy();
        this.spinner.show();
      } else if (this.selectfiscalyearid == null) {
        console.log("3");
        if (this.currentyear == "allfiscalyear") {
          this.getCentralPolicy()
          this.spinner.show();
        } else {
          this.getCurrentCentralPolicy(this.currentyear)
          this.spinner.show();
        }
        // this.getCurrentCentralPolicy(this.currentyear);
      } else {
        console.log("4");
        this.getSelectfiscalyear();
        this.spinner.show();
      }
      // this.centralpolicyservice.getcentralpolicydata().subscribe(result => {
      //   this.resultcentralpolicy = result
      //   console.log(this.resultcentralpolicy);
      // })
    })
  }
  Subject(id) {
    this.router.navigate(['/subject', id])
  }
  DetailrowCentralPolicy(id: any) {
    this.router.navigate(['/centralpolicy/detailrowcentralpolicy', id])
  }
  CreateCentralPolicy() {
    this.router.navigate(['/centralpolicy/createcentralpolicy'])
  }
  DetailCentralPolicy(id: any) {
    this.router.navigate(['/centralpolicy/detailcentralpolicy', id])
  }
  EditCentralPolicy(id: any) {
    this.router.navigate(['/centralpolicy/editcentralpolicy', id])
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
