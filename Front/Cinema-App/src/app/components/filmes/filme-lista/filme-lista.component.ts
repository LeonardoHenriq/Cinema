import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '@environments/environment';
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
export class FilmeListaComponent implements OnInit {

  modalRef?: BsModalRef;
  public filmes: Filme[] = [];
  public filmesFiltrados : Filme[] = [];
  public filmeTitulo!: string;
  public filmeId!:number;

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

  public retornaImagem(imagemURL: string): string{

    return(imagemURL !== '') ? `${environment.apiURL}resources/images/${imagemURL}`: '/assets/img/semImagem.png'

  }

  public getFilmes(): void{

    this.filmeService.getFilmes().subscribe(
      (_filmes: Filme[]) => {
        this.filmes = _filmes,
        this.filmesFiltrados = this.filmes;
      },
      (error : any) => {
        console.log(error);
        this.spinner.hide();
        if(error.status === 0){
          this.toastr.info('Usuário sem permissão','Sem Permissão!');
        }else
        this.toastr.error('Erro ao Carregar os Filmes','Erro!');
      }
    ).add(() => this.spinner.hide());
  }

  openModal(template: TemplateRef<any>, filmeTitulo: string, filmeId: number): void {
    this.filmeTitulo = filmeTitulo;
    this.filmeId = filmeId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();

    this.filmeService.deleteFilme(this.filmeId).subscribe(
       (result : any) => {

        result.message ==='Excluido' ?
          this.toastr.success(result.message,'Excluido!'):
          this.toastr.error(result.message,'Erro');

          this.getFilmes();
      },
     (error: any) => {
        console.log(error);
        if(error.status === 0){
          this.toastr.info('Usuário sem permissão','Sem Permissão!');
        }else
        this.toastr.error(error.error, 'Erro');
      }
  ).add(()=> this.spinner.hide());
  }

  decline(): void {
    this.modalRef?.hide();
  }

  detalheFilme(id: number): void {
    this.router.navigate([`filmes/detalhe/${id}`]);
  }

}
