import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-titulo',
  templateUrl: './titulo.component.html',
  styleUrls: ['./titulo.component.scss']
})
export class TituloComponent {

  @Input() titulo!: string;
  @Input() iconClass = 'fa fa-user';
  @Input() subtitulo = '';
  @Input() botaoListar = false;

  constructor(private router: Router) { }

public ngOnInit(): void{
}

listar(): void {
this.router.navigate([`/${this.removeAcento(this.titulo.toLocaleLowerCase())}/lista`])
}

public removeAcento (text:string): string
{
    text = text.toLowerCase();
    text = text.replace(new RegExp('[ÁÀÂÃ]','gi'), 'a');
    text = text.replace(new RegExp('[ÉÈÊ]','gi'), 'e');
    text = text.replace(new RegExp('[ÍÌÎ]','gi'), 'i');
    text = text.replace(new RegExp('[ÓÒÔÕ]','gi'), 'o');
    text = text.replace(new RegExp('[ÚÙÛ]','gi'), 'u');
    text = text.replace(new RegExp('[Ç]','gi'), 'c');
    return text;
}

}
