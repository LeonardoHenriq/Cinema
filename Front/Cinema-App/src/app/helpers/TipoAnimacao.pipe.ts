import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'TipoAnimacaoPipe'
})
export class TipoAnimacaoPipe implements PipeTransform {

  transform(value: number): any {
    return value === 0? '2D' : '3D';
  }

}
