import { SessaoService } from './../../../services/Sessao.service';
import { FilmeService } from 'src/app/services/Filme.service';
import { Component, OnInit } from '@angular/core';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Filme } from 'src/app/models/Filme';


@Component({
  selector: 'app-sessao-novo',
  templateUrl: './sessao-novo.component.html',
  styleUrls: ['./sessao-novo.component.scss']
})
export class SessaoNovoComponent implements OnInit  {

  public filmes: Filme[] = [];

  get bsConfig(): any{
    return {
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY',
      containerClass: 'theme-default',
      showWeekNumbers: false
    }
  }


  constructor(private localService:BsLocaleService,
              private filmeService : FilmeService,
              private spinner : NgxSpinnerService,
              private toastr: ToastrService,
              private sessaoService: SessaoService) {
    this.localService.use('pt-br')
  }


  ngOnInit(): void {
    this.getFilmes();
  }

  public getFilmes(): void{

    this.filmeService.getFilmes().subscribe(
      (_filmes: Filme[]) => {
        this.filmes = _filmes
      },
      (error : any) => {
        console.log(error);
        this.toastr.error('Erro ao Carregar os Filmes','Erro!');
      }
    ).add(()=> this.spinner.hide());
  }

}
