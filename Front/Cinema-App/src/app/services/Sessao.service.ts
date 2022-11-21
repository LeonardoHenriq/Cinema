import { Sala } from './../models/Sala';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Sessao } from '../models/Sessao';

@Injectable()
export class SessaoService {

  baseURL = 'https://localhost:5001/api/sessao';

constructor(private http: HttpClient) { }

public getSessoes(): Observable<Sessao[]>{
  return this.http.get<Sessao[]>(this.baseURL);
}

public getSessaoById(idSessao : number): Observable<Sessao>{
  return this.http.get<Sessao>(`${this.baseURL}/${idSessao}`);
}
public getDuracaoFilme(idFilme: number): Observable<string>{
  return this.http.get<string>(`${this.baseURL}/duracao-filme/${idFilme}`);
}

public salaAvailable(inicial : Date,final : Date): Observable<Sala[]>{

  let params = new HttpParams();

  params.set('inicial',inicial.toString());
  params.set('final',final.toString());

  return this.http.get<Sala[]>(`${this.baseURL}/salas-available`,{params});
}

public postSessao(sessao : Sessao): Observable<Sessao>{
  return this.http.post<Sessao>(this.baseURL, sessao);
}

public deleteSessao(idSessao : number): Observable<string>{
  return this.http.delete<string>(`${this.baseURL}/${idSessao}`);
}

}
