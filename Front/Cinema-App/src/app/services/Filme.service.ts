import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environments/environment';
import { Observable, take } from 'rxjs';
import { Filme } from '../models/Filme';

@Injectable()
export class FilmeService {

baseURL = environment.apiURL+'api/filme';

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

public postUpload(filmeId: number, file: File): Observable<Filme>{

  const fileToUpload = file[0] as File;
  const formData = new FormData();
  formData.append('file', fileToUpload);

  return this.http.post<Filme>(`${this.baseURL}/upload-image/${filmeId}`, formData).pipe(take(1));
}

}
