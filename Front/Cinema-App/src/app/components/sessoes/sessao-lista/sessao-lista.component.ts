import { Sala } from './../../../models/Sala';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Filme } from 'src/app/models/Filme';
import { Sessao } from 'src/app/models/Sessao';
import { SessaoService } from 'src/app/services/Sessao.service';

@Component({
  selector: 'app-sessao-lista',
  templateUrl: './sessao-lista.component.html',
  styleUrls: ['./sessao-lista.component.scss']
})
export class SessaoListaComponent implements OnInit  {
  modalRef?: BsModalRef;
  public sessoes: Sessao[] = [];

  public sessoesFiltrados : Sessao[] = [];

  private _filtroLista: string = ''

  public get filtroLista() : string{
    return this._filtroLista
  }

  public set filtroLista(value : string){
    this._filtroLista = value;
    this.sessoesFiltrados = this.filtroLista ? this.filtrarSessoes(this.filtroLista) : this.sessoes;
  }

  public filtrarSessoes(filtrarPor : string): Sessao[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.sessoes.filter(
      (sessao : {filme : Filme; sala: Sala} ) =>
      sessao.filme.titulo.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
      sessao.filme.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
      sessao.sala.nome.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  constructor(private sessaoService: SessaoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) { }

  public ngOnInit(): void{
    this.spinner.show();
    this.getSessoes();
  }

  public getSessoes(): void{

    this.sessaoService.getSessoes().subscribe({
      next : (_sessoes: Sessao[]) => {
        this.sessoes = _sessoes
        this.sessoesFiltrados = this.sessoes;
      },
      error : (error : any) => {
        console.log(error);
        this.spinner.hide();
        this.toastr.error('Erro ao Carregar Sessões','Erro!');
      },
      complete: () => this.spinner.hide()
    });
  }

  public openModal(template: TemplateRef<any>): void {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  public confirm(): void {
    this.modalRef?.hide();
    this.toastr.success('A Sessão foi excluida com sucesso!','Excluido!');
  }

  public decline(): void {
    this.modalRef?.hide();
  }
}
