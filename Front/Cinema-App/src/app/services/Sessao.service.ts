import { Sala } from './../models/Sala';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take } from 'rxjs';
import { Sessao } from '../models/Sessao';
import { environment } from '@environments/environment';

@Injectable()
export class SessaoService {

  baseURL = environment.apiURL+'api/sessao';

constructor(private http: HttpClient) { }

public getSessoes(): Observable<Sessao[]>{
  return this.http.get<Sessao[]>(this.baseURL).pipe(take(1));
}

public getSessaoById(idSessao : number): Observable<Sessao>{
  return this.http.get<Sessao>(`${this.baseURL}/${idSessao}`).pipe(take(1));
}
public getDuracaoFilme(idFilme: number): Observable<string>{
  return this.http.get<string>(`${this.baseURL}/duracao-filme/${idFilme}`).pipe(take(1));
}

public salaAvailable(inicial : Date,final : Date): Observable<Sala[]>{

  let params = new HttpParams();

  params.set('inicial',inicial.toString());
  params.set('final',final.toString());

  return this.http.get<Sala[]>(`${this.baseURL}/salas-available`,{params}).pipe(take(1));
}

public postSessao(sessao : Sessao): Observable<Sessao>{
  return this.http.post<Sessao>(this.baseURL, sessao).pipe(take(1));
}

public deleteSessao(idSessao : number): Observable<string>{
  return this.http.delete<string>(`${this.baseURL}/${idSessao}`).pipe(take(1));
}

}
