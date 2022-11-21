import { Component, OnInit } from '@angular/core';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';

@Component({
  selector: 'app-sessao-novo',
  templateUrl: './sessao-novo.component.html',
  styleUrls: ['./sessao-novo.component.scss']
})
export class SessaoNovoComponent implements OnInit  {

  get bsConfig(): any{
    return {
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY',
      containerClass: 'theme-default',
      showWeekNumbers: false
    }
  }

  constructor(private localService:BsLocaleService) {
    this.localService.use('pt-br')
  }


  ngOnInit(): void {
  }

}
