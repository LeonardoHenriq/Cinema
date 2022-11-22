import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'TipoAudioPipe'
})
export class TipoAudioPipe implements PipeTransform {

  transform(value: number): string {
    return value === 0 ? 'Original':'Dublado';
  }

}
