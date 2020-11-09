import { Component, OnInit, Output, EventEmitter, AfterContentInit, AfterContentChecked } from '@angular/core';
import { IMyOptions } from 'mydatepicker-th';
import { DateLengthService } from './date-lengthservice.service';

@Component({
  selector: 'app-date-length',
  templateUrl: './date-length.component.html',
  styleUrls: ['./date-length.component.css']
})
export class DateLengthComponent implements OnInit, AfterContentInit, AfterContentChecked {

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
  constructor(private _DateLengthService: DateLengthService) { }
  ngAfterContentInit(): void {
  }
  ngAfterContentChecked(): void {
  }
  start_date
  end_date

  start_time
  end_time


  ngOnInit() {
    this.value.emit({ start_date: this.setDateTime(this.start_date), end_date: this.setDateTime(this.end_date) })
  }

  startdate(event=null, i=null) {
    this.end_date ? this.checkStarttoResetEndDate(i) : null
    this.start_date = event
    this.disableDate_i(event, i)
    this.value.emit({ start_date: this.setDateTime(this.start_date), end_date: this.setDateTime(this.end_date) })

    //console.log(this.start_date);

    // alert(JSON.stringify(event))
  }
  starttime(event, i) {
    this.start_date = event
    this.value.emit({ start_date: this.setDateTime(this.start_date), end_date: this.setDateTime(this.end_date) })

  }

  enddate(event, i) {

    this.end_date = event
    this.value.emit({ start_date: this.setDateTime(this.start_date), end_date: this.setDateTime(this.end_date) })

  }

  async endtime(event: any, i) {
    //console.log('this.end_date', this.end_date);
    this.value.emit({ start_date: this.setDateTime(this.start_date), end_date: this.setDateTime(this.end_date) })
  }

  dateChecked(startDate: Date, endDate: Date) {

    return Date.parse(startDate.toString()) <= Date.parse(endDate.toString())
  }

  checkStarttoResetEndDate(index) {
    this.end_date = void 0;
    this.value.emit({ start_date: this.setDateTime(this.start_date), end_date: this.setDateTime(this.end_date) })
  }


  setDateTime(date: Date) {
    var time = new Date(date)
    time.setHours(time.getHours() + 7)
    return time
  }

  disableDate_i(date: any, i) {
    this.myDatePickerOptionsEnddate = {
      disableDateRanges: [{
        end: { year: date.getFullYear(), month: date.getMonth() + 1, day: date.getDate() - 1 },
        begin: { year: date.getFullYear() - 10, month: date.getMonth() + 1, day: date.getDate() - 1 }
      }]
    }
  }
}
