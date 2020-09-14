import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { IMyOptions } from 'mydatepicker-th';

@Component({
  selector: 'app-date-length',
  templateUrl: './date-length.component.html',
  styleUrls: ['./date-length.component.css']
})
export class DateLengthComponent implements OnInit {

  @Output() childButtonEvent = new EventEmitter();
  @Output() value = new EventEmitter();

  myDatePickerOptions: IMyOptions = {
    // other options...
    // dateFormat: 'dd/mm/yyyy',

  };
  myDatePickerOptionsEnddate: IMyOptions = {
    // other options...
    // dateFormat: 'dd/mm/yyyy',

  };
  constructor() { }
  start_date
  end_date

  setStartDate
  setEndDate
  ngOnInit() {
  }
  startdate(event) {
    this.end_date ? this.checkStarttoResetEndDate() : null
    this.start_date = event
    this.disableDate(event)

    //console.log(this.start_date_plan_i[i]);

    // alert(JSON.stringify(event))
  }
  starttime(event) {
    this.start_date = event

  }

  enddate(event) {

    this.end_date = event


  }

  async endtime(event: any, i) {
    //console.log('this.end_date_plan_i[i]', this.end_date_plan_i[i]);
  }

  dateChecked(startDate: Date, endDate: Date) {
    //console.log(startDate.toString(), endDate.toString());

    return Date.parse(startDate.toString()) <= Date.parse(endDate.toString())
    // ? true : false
  }

  checkStarttoResetEndDate() {
    // this.end_date = null
    this.end_date = void 0;
  }

  setDateTime(date: Date) {
    var time = new Date(date)
    time.setHours(time.getHours() + 7)
    return time
  }

  disableDate(date: any) {
    //console.log(date.getFullYear(), date.getMonth() + 1, date.getDate());
    this.myDatePickerOptionsEnddate = {
      disableDateRanges: [{
        end: { year: date.getFullYear(), month: date.getMonth() + 1, day: date.getDate() - 1 },
        begin: { year: date.getFullYear() - 10, month: date.getMonth() + 1, day: date.getDate() - 1 }
      }]
    }
  }
}
