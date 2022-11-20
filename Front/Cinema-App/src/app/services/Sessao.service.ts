import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Sessao } from '../models/Sessao';

@Injectable()
export class SessaoService {

  baseURL = 'https://localhost:5001/api/sessao';

constructor(private http: HttpClient) { }

public getSessoes(): Observable<Sessao[]>{
  return this.http.get<Sessao[]>(this.baseURL)
}

public getSessaoById(idSessao : number): Observable<Sessao>{
  return this.http.get<Sessao>(`${this.baseURL}/${idSessao}`)
}
public getDuracaoFilme(idFilme: number): Observable<string>{
  return this.http.get<string>(`${this.baseURL}/duracao-filme/${idFilme}`);
}

}
