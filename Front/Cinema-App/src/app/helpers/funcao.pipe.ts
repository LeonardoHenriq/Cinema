import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'funcaoPipe'
})
export class FuncaoPipe implements PipeTransform {

  transform(value: number): any {
    return value === 0 ? 'Não Informado' : 'Gerente';
  }

}
