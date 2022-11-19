import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-salas',
  templateUrl: './salas.component.html',
  styleUrls: ['./salas.component.scss']
})
export class SalasComponent {
  public salas: any;

  constructor(private http: HttpClient) { }

  ngOnInit(): void{
    this.getEventos();
  }

  public getEventos(): void{

    this.http.get('https://localhost:5001/api/sala').subscribe(
      response => this.salas = response,
      error => console.log(error)
    );
    console.log(this.salas)
  }
}
