import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-filmes',
  templateUrl: './filmes.component.html',
  styleUrls: ['./filmes.component.scss']
})
export class FilmesComponent {

  public filmes: any = [];
  public filmesFiltrados : any = [];

  widthImg: number = 50;
  marginImg: number = 2;
  exibirImagem = true;
  private _filtroLista: string = ''

  public get filtroLista() : string{
    return this._filtroLista
  }

  public set filtroLista(value : string){
    this._filtroLista = value;
    this.filmesFiltrados = this.filtroLista ? this.filtrarFilmes(this.filtroLista) : this.filmes;
  }

  filtrarFilmes(filtrarPor : string): any {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.filmes.filter(
      (filme : {titulo : string;descricao : string;} ) => filme.titulo.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
      filme.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  constructor(private http: HttpClient) { }

  ngOnInit(): void{
    this.getFilmes();
  }

  alterarImagem() {
    this.exibirImagem = !this.exibirImagem;
  }

  public getFilmes(): void{

    this.http.get('https://localhost:5001/api/filme').subscribe(
      response => {
        this.filmes = response,
        this.filmesFiltrados = this.filmes
      },
      error => console.log(error)
    );
    console.log(this.filmes)
  }
}
