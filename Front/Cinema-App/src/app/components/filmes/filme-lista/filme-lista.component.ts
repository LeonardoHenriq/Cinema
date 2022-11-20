import { Component, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Filme } from 'src/app/models/Filme';
import { FilmeService } from 'src/app/services/Filme.service';

@Component({
  selector: 'app-filme-lista',
  templateUrl: './filme-lista.component.html',
  styleUrls: ['./filme-lista.component.scss']
})
export class FilmeListaComponent {

  modalRef?: BsModalRef;
  public filmes: Filme[] = [];
  public filmesFiltrados : Filme[] = [];

  public widthImg: number = 50;
  public marginImg: number = 2;
  public exibirImagem = false;
  private _filtroLista: string = ''

  public get filtroLista() : string{
    return this._filtroLista
  }

  public set filtroLista(value : string){
    this._filtroLista = value;
    this.filmesFiltrados = this.filtroLista ? this.filtrarFilmes(this.filtroLista) : this.filmes;
  }

  public filtrarFilmes(filtrarPor : string): Filme[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.filmes.filter(
      (filme : {titulo : string;descricao : string;} ) => filme.titulo.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
      filme.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  constructor(private filmeService: FilmeService,
              private modalService: BsModalService,
              private toastr: ToastrService,
              private spinner: NgxSpinnerService,
              private router: Router) { }

  public ngOnInit(): void{
    this.spinner.show();
    this.getFilmes();
  }

  public alterarImagem(): void {
    this.exibirImagem = !this.exibirImagem;
  }

  public getFilmes(): void{

    this.filmeService.getFilmes().subscribe({
      next : (_filmes: Filme[]) => {
        this.filmes = _filmes,
        this.filmesFiltrados = this.filmes;
      },
      error : (error : any) => {
        console.log(error);
        this.spinner.hide();
        this.toastr.error('Erro ao Carregar os Filmes','Erro!');
      },
      complete: () => this.spinner.hide()
    });
  }

  openModal(template: TemplateRef<any>): void {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.toastr.success('O Filme foi excluido com sucesso!','Excluido!');
  }

  decline(): void {
    this.modalRef?.hide();
  }

  detalheFilme(id: number): void {
    this.router.navigate([`filmes/detalhe/${id}`]);
  }

}
