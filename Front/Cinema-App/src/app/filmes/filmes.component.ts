import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-filmes',
  templateUrl: './filmes.component.html',
  styleUrls: ['./filmes.component.scss']
})
export class FilmesComponent {

  public filmes: any;

  constructor(private http: HttpClient) { }

  ngOnInit(): void{
    this.getEventos();
  }

  public getEventos(): void{

    this.http.get('https://localhost:5001/api/filme').subscribe(
      response => this.filmes = response,
      error => console.log(error)
    );
    console.log(this.filmes)
  }
}
