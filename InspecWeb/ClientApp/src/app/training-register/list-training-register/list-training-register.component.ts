import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TrainingService } from '../../services/training.service';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { delay } from 'lodash';


@Component({
  selector: 'app-list-training-register',
  templateUrl: './list-training-register.component.html',
  styleUrls: ['./list-training-register.component.css']
})
export class ListTrainingRegisterComponent implements OnInit {
  peopledetail: any
  birthdate: any
  trainingid: string
  resulttraining: any[] = []
  modalRef: BsModalRef;
  delid: any
  loading = false;
  dtOptions: DataTables.Settings = {};
  Form: any;
  resulttrainingCondition: any[] = []
  people: any[] = []
  condition: any[] = []
  constructor(private modalService: BsModalService,
    private fb: FormBuilder,
    private trainingservice: TrainingService,
    public share: TrainingService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string) {
    this.trainingid = activatedRoute.snapshot.paramMap.get('id')
  }



  ngOnInit() {

    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [3, 4],
          orderable: false
        }
      ]

    };

    this.Form = this.fb.group({
      "approve": new FormControl(null, [Validators.required]),

    })



    this.getData()


    // this.trainingservice.gettrainingdata2(id)
    // .subscribe(result => {
    //   this.resulttraining = result
    //   this.loading = true;
    //   //console.log(this.resulttraining);
    // })

    //this.gettrainingdata2()
  }

  getData() {
    this.loading = false;
    this.trainingservice.getregistertrainingdata(this.trainingid)
      .subscribe(result => {
        this.resulttraining = result
        this.loading = true

        // this.birthdate = this.resulttraining.birthDate

        //console.log(this.resulttraining);

        this.trainingservice.getTrainingCondition(this.trainingid)
          .subscribe(result => {
            this.resulttrainingCondition = result.map(result => {
              return { ...result, status: false }
            })
            // this.condition = result.map((result => {
            //   return { id: result.id, status: false }
            // }))

            const doAsync = () => {
              return new Promise((resolve, reject) => {
                setTimeout(() => {
                  var check: any[] = []
                  for (var i1 = 0; i1 < this.resulttrainingCondition.length; i1++) {
                    for (var i2 = 0; i2 < this.resulttrainingCondition[i1].trainingRegisterConditions.length; i2++) {
                      //console.log('result3.trainingRegisterConditions', this.resulttrainingCondition[i1].trainingRegisterConditions[i2]);
                      this.resulttrainingCondition[i1].trainingRegisterConditions[i2]
                      check.push(
                        this.resulttrainingCondition[i1].trainingRegisterConditions[i2]
                        // .map(result => {
                        //   //console.log('result'+i1,result);

                        //   return {...result}
                        // })
                      )
                    }

                  }


                  resolve(check)
                  // return
                }, 100)
              })
            }
            // this.resulttrainingCondition.forEach(result3 => {

            // })

            doAsync().then(result2 => {
              var result3: any = result2
              this.resulttraining = this.resulttraining.map(result => {
                //console.log(result3);
                var check = result3.filter(result4 => {

                  return result.id == result4.registerId
                })

                if (check.length == 0) {
                  return { ...result, register_status: false }
                } else {
                  return { ...result, register_status: true }
                }
                // console.log('doAsync', result.id,result2);

                // result.filter(result=>{

                // })
                // return {...result,status:false}
              })


            })
            this.loading = true
            // console.log('result.id == result.registerId', this.resulttraining);

            // setTimeout(() => {
            //   var arrresulttraining = this.resulttraining.map((result2, index) => {


            //     // setTimeout(() => {

            //     // }, 200)
            //     // return arr



            //   })
            //   //console.log("resultresultresult", arrresulttraining);
            // }, 200)
            // console.log("this.resulttrainingCondition", this.resulttrainingCondition);

          })
      })
  }

  // gettrainingdata2() {
  //   this.trainingservice.gettrainingdata2(1)
  //   .subscribe(result => {
  //     this.resulttraining = result
  //     this.loading = true
  //     //console.log(this.resulttraining);
  //   })
  // }

  openModal(template: TemplateRef<any>, id, item = null, index = null) {

    this.trainingservice.getregistertrainingpeopledata(id)
      .subscribe(result => {
        // alert(JSON.stringify(result))
        this.peopledetail = result
        this.loading = true
      })

    // alert(JSON.stringify(this.peopledetail))

    this.resulttrainingCondition = this.resulttrainingCondition.map(result => {
      return { ...result, status: false }
    })
    this.delid = id;
    //console.log(this.delid);
    this.checkcondition(item, index)
    this.modalRef = this.modalService.show(template);

  }

  editRegisterList(value, delid) {
    // alert(JSON.stringify(value));
    // console.clear();
    // //console.log("kkkk" + JSON.stringify(value));
    this.trainingservice.editRegisterList(value, delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.trainingservice.getregistertrainingdata(this.trainingid).subscribe(result => {
        this.resulttraining = result
        this.loading = true
        this.modalRef.hide()
      })
    })
  }

  editRegisterList2(value) {
    // alert(JSON.stringify(value));
    // console.clear();
    // //console.log("kkkk" + JSON.stringify(value));
    this.trainingservice.editRegisterList2(value, this.people).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.trainingservice.getregistertrainingdata(this.trainingid).subscribe(result => {
        this.resulttraining = result
        this.loading = true
        this.modalRef.hide()
      })
      this.people = []
    })
  }

  addsPeople2(value) {
    // //console.log('item.id');
    // var subject = value.vaule
    this.people = this.addPeople(this.people, value)
    //console.log(this.people);
  }

  addPeople(array: any[], value) {
    var distinctThings: any[] = array.filter(
      (thing, i, arr) => arr.findIndex(t => t === value) === i
    );
    // //console.log('distinctThings', distinctThings);
    if (distinctThings.length != 0) {
      var s = new Set(array);
      s.delete(value);
      return Array.from(s);
    } else {
      var s = new Set(array);
      s.add(value);
      return Array.from(s);
    }
  }
  // test() {
  //   alert("12345")
  // }

  addscondition2(value) {
    // //console.log('item.id');
    // var subject = value.vaule
    this.resulttrainingCondition = this.addcondition(this.resulttrainingCondition, value)
    //console.log(this.resulttrainingCondition);
  }

  addcondition(array: any[], value) {
    var distinctThings: any[] = array.filter(
      (thing, i, arr) => arr.findIndex(t => t.id === value.id) === i
    );
    // //console.log('distinctThings', distinctThings);
    var s = new Set(array);
    s.delete(value);
    s.add({ ...value, status: !value.status });
    return Array.from(s);
    if (distinctThings.length != 0) {
      var s = new Set(array);
      s.add(value);
      return Array.from(s);
    } else {
      var s = new Set(array);
      s.add(value);
      return Array.from(s);
    }
  }

  editRegisterConditionList(delid) {
    //console.log("this.resulttrainingCondition", this.resulttrainingCondition);

    this.trainingservice.editRegisterConditionList(this.resulttrainingCondition, delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.trainingservice.getregistertrainingdata(this.trainingid).subscribe(result => {
        this.resulttraining = result
        this.loading = true
        this.modalRef.hide()
        this.getData();
      })
      // this.resulttrainingCondition = []
    })

  }
  showUpdatedItem(item, index) {
    this.resulttrainingCondition[index] = { ...item, status: !item.status };
    // //console.log(this.resulttrainingCondition);

  }

  showUpdatedItem2(item, index, status) {
    console.log('testetsttest', item, index, status);
    this.resulttrainingCondition[index] = { ...this.resulttrainingCondition[index], status: status };
    // if (status == 1) {

    // } else {

    //   this.resulttrainingCondition[index] = { ...this.resulttrainingCondition[index], status: false };
    // }
    // //console.log(this.resulttrainingCondition);

  }

  checkcondition(item, index) {
    //console.log('checkcondition', item, this.resulttrainingCondition);

    const doAsync = () => {
      return new Promise((resolve, reject) => {
        setTimeout(() => {
          var check: any[] = []
          for (var i1 = 0; i1 < this.resulttrainingCondition.length; i1++) {
            for (var i2 = 0; i2 < this.resulttrainingCondition[i1].trainingRegisterConditions.length; i2++) {
              //console.log('result3.trainingRegisterConditions', this.resulttrainingCondition[i1].trainingRegisterConditions[i2]);
              this.resulttrainingCondition[i1].trainingRegisterConditions[i2]
              check.push(
                this.resulttrainingCondition[i1].trainingRegisterConditions[i2]
              )
            }

          }


          resolve(check)
          // return
        }, 100)
      })
    }
    // this.resulttrainingCondition.forEach(result3 => {

    // })

    doAsync().then(result2 => {
      var result3: any = result2
      var data1 = result3
        // .filter(result => {
        //   return result.trainingRegister.training.id == item.id
        // })
        .filter(result => {
          return result.registerId == item.id
        })
      var arr: any[] = []
      for (var i1 = 0; i1 < data1.length; i1++) {
        data1[i1]
        // console.log('data' + i1, data1[i1]);
        this.showUpdatedItem2(data1[i1], i1, data1[i1].status)
      }
    })

  }




}
