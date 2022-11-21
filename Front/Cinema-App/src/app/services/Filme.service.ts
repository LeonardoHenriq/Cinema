import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Filme } from '../models/Filme';

@Injectable()
export class FilmeService {

baseURL = 'https://localhost:5001/api/filme';

constructor(private http: HttpClient) { }

public getFilmes(): Observable<Filme[]>{
  return this.http.get<Filme[]>(this.baseURL);
}

public getFilmeById(idFilme : number): Observable<Filme>{
  return this.http.get<Filme>(`${this.baseURL}/${idFilme}`);
}

public postFilme(filme : Filme): Observable<Filme>{
  return this.http.post<Filme>(this.baseURL, filme);
}

public putFilme(idFilme: number,filme : Filme): Observable<Filme>{
  return this.http.put<Filme>(`${this.baseURL}/${idFilme}`, filme);
}

public deleteFilme(idFilme : number): Observable<string>{
  return this.http.delete<string>(`${this.baseURL}/${idFilme}`);
}

}
