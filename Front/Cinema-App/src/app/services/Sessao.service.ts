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

public getHorarios(idfilme : number, datasessao: Date, horarioInicial: string){

  let data = new HttpParams()
  .set('dataSessao',datasessao.toDateString())
  .set('horarioInicial',horarioInicial)
  .set('idfilme',idfilme.toString());

  return this.http.get<any>(`${this.baseURL}/gethorarios`,{params: data}).pipe(take(1))

}

public postSessao(sessao : Sessao): Observable<Sessao>{
  return this.http.post<Sessao>(this.baseURL, sessao).pipe(take(1));
}

public deleteSessao(idSessao : number): Observable<string>{
  return this.http.delete<string>(`${this.baseURL}/${idSessao}`).pipe(take(1));
}

}
