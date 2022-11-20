import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Filme } from '../models/Filme';

@Injectable()
export class FilmeService {

baseURL = 'https://localhost:5001/api/filme';

constructor(private http: HttpClient) { }

public getFilmes(): Observable<Filme[]>{
  return this.http.get<Filme[]>(this.baseURL)
}

public getFilmeById(idEvento : number): Observable<Filme>{
  return this.http.get<Filme>(`${this.baseURL}/${idEvento}`)
}

}
