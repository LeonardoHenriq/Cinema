import { Sala } from './../../../models/Sala';
import { SessaoService } from './../../../services/Sessao.service';
import { FilmeService } from 'src/app/services/Filme.service';
import { Component, OnInit } from '@angular/core';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Filme } from 'src/app/models/Filme';
import { FormBuilder, FormControl, FormControlName, FormGroup, FormGroupName, Validators } from '@angular/forms';


@Component({
  selector: 'app-sessao-novo',
  templateUrl: './sessao-novo.component.html',
  styleUrls: ['./sessao-novo.component.scss']
})
export class SessaoNovoComponent implements OnInit  {

  public filmes: Filme[] = [];
  public salas: Sala[] = [];
  public form: FormGroup;

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
              private sessaoService: SessaoService) {
    this.localService.use('pt-br')
  }

  get f(): any{
    return this.form.controls;
  }


  ngOnInit(): void {
    this.getFilmes();
    this.validation();
  }

  public validation(): void {
    this.form = this.fb.group({
      dataSessao :  ['', Validators.required],
      horarioInicial : ['', Validators.required],
      filmeId : ['', Validators.required],
      valorIngresso : ['', Validators.required],
      tipoAnimacao :  ['', Validators.required],
      tipoAudio :  ['', Validators.required],
      salaId : ['', Validators.required],
    })
  }

  public getFilmes(): void{

    this.filmeService.getFilmes().subscribe(
      (_filmes: Filme[]) => {
        this.filmes = _filmes
        console.log(this.filmes)
      },
      (error : any) => {
        console.log(error);
        this.toastr.error('Erro ao Carregar os Filmes','Erro!');
      }
    ).add(()=> this.spinner.hide());
  }

  public cssValidator(campoForm: FormControl): any {
    console.log(campoForm);
    return {'is-invalid': campoForm?.errors && campoForm?.touched};
  }

}
