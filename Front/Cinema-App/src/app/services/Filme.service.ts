import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take } from 'rxjs';
import { Filme } from '../models/Filme';

@Injectable()
export class FilmeService {

baseURL = 'https://localhost:5001/api/filme';
//baseURL = 'https://localhost:44334/api/filme';

constructor(private http: HttpClient) { }

public getFilmes(): Observable<Filme[]>{
  return this.http.get<Filme[]>(this.baseURL).pipe(take(1));
}

public getFilmeById(idFilme : number): Observable<Filme>{
  return this.http.get<Filme>(`${this.baseURL}/${idFilme}`).pipe(take(1));
}

public post(filme : Filme): Observable<Filme>{
  return this.http.post<Filme>(this.baseURL, filme).pipe(take(1));
}

public put(filme : Filme): Observable<Filme>{
  return this.http.put<Filme>(`${this.baseURL}/${filme.id}`, filme).pipe(take(1));
}

public deleteFilme(idFilme : number): Observable<any>{
  return this.http.delete(`${this.baseURL}/${idFilme}`).pipe(take(1));
}

}
