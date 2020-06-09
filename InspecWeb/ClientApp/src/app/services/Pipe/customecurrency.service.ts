import { Pipe, PipeTransform } from '@angular/core';
import { CurrencyPipe } from '@angular/common';

@Pipe({ name: 'CustomeCurrency' })
export class CustomCurrencyPipe implements PipeTransform {

    constructor(private currencyPipe: CurrencyPipe) { }
    transform(value: any, currencyCode?: string, display?: string | boolean, digitsInfo?: string, locale?: string): string {
        if (value != null)
            return this.currencyPipe.transform(value, currencyCode, '', digitsInfo, locale);
        return this.currencyPipe.transform(0, currencyCode, '', locale).split('0.00')[0];
    }
}