import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import { Constants } from '../Util/constants';

@Pipe({
  name: 'TimeFormat'
})
export class TimeFormatPipe extends DatePipe implements PipeTransform {

  override transform(value: any, args?: any): any {
    return super.transform(value, Constants.TIME_FMT);
  }

}
