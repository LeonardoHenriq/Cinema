import { Router } from '@angular/router';
import { SalaService } from './../../../services/Sala.service';
import { Sessao } from 'src/app/models/Sessao';
import { Sala } from './../../../models/Sala';
import { SessaoService } from './../../../services/Sessao.service';
import { FilmeService } from 'src/app/services/Filme.service';
import { Component, OnInit } from '@angular/core';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Filme } from 'src/app/models/Filme';
import { FormBuilder, FormControl, FormGroup, FormGroupName} from '@angular/forms';


@Component({
  selector: 'app-sessao-novo',
  templateUrl: './sessao-novo.component.html',
  styleUrls: ['./sessao-novo.component.scss']
})
export class SessaoNovoComponent implements OnInit  {

  public filmes: Filme[] = [];
  public salas: Sala[] = [];
  public form: FormGroup;

  public datas = {inicial : new Date() , final : new Date()};

  model = {} as Sessao ;

  public horarios: FormGroupName;

  get bsConfig(): any{
    return {
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY',
      containerClass: 'theme-default',
      showWeekNumbers: false
    }
  }


  constructor(private fb: FormBuilder,
              private localService:BsLocaleService,
              private filmeService : FilmeService,
              private spinner : NgxSpinnerService,
              private toastr: ToastrService,
              private sessaoService: SessaoService,
              private salaService: SalaService,
              private router : Router) {
    this.localService.use('pt-br')
  }

  ngOnInit(): void {
    this.getFilmes();
    this.getSalas();
  }
  public getFilmes(): void{

    this.filmeService.getFilmes().subscribe(
      (_filmes: Filme[]) => {
        this.filmes = _filmes
      },
      (error : any) => {
        console.log(error);
        if(error.status === 0){
          this.toastr.info('Usuário sem permissão','Sem Permissão!');
        }else
        this.toastr.error('Erro ao Carregar os Filmes','Erro!');
      }
    ).add(()=> this.spinner.hide());
  }


  public carregaSalas() : void{

    this.spinner.show();

    if(this.model.filmeId !== 0 && this.model.dataSessao && this.model.horarioInicial){
      this.sessaoService.getHorarios(this.model.filmeId,this.model.dataSessao,this.model.horarioInicial.toString()).subscribe(
        (retorno: any) => {
          this.datas = retorno;
          this.model.horarioFinal = this.datas.final;
        },
        (error: any) => {
          console.log(error);
          if(error.status === 0){
            this.toastr.info('Usuário sem permissão','Sem Permissão!');
          }else
          this.toastr.error('Erro ao tentar buscar a duração do filme','Erro!')
        },
      ).add(()=> this.spinner.hide());

      this.getSalas();
    }

  }

  public salvarSessao(): void{
    this.spinner.show();
    this.sessaoService.postSessao(this.model).subscribe(
      () => {this.router.navigateByUrl('/sessoes');},
      (error: any) => {
        console.log(error);
        if(error.status === 0){
          this.toastr.info('Usuário sem permissão','Sem Permissão!');
        }else
        this.toastr.error(error.error,'Erro!')
      }
    ).add(() => this.spinner.hide())

  }

  public getSalas(): void{

    this.salaService.getSalas().subscribe({
      next : (_Salas: Sala[]) => {
        this.salas = _Salas
      },
      error : (error : any) => {
        console.log(error);
        this.spinner.hide();
        if(error.status === 0){
          this.toastr.info('Usuário sem permissão','Sem Permissão!');
        }else
        this.toastr.error('Erro ao Carregar os Salas','Erro!');
      },
      complete: () => this.spinner.hide()
    });
  }

  public cssValidator(campoForm: FormControl): any {

    return {'is-invalid': campoForm?.errors && campoForm?.touched};
  }

}
