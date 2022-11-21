import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environments/environment';
import { Observable, take } from 'rxjs';
import { Sala } from '../models/Sala';

@Injectable()
export class SalaService {

  baseURL = environment.apiURL+'api/sala';

constructor(private http: HttpClient) { }

public getSalas(): Observable<Sala[]>{
  return this.http.get<Sala[]>(this.baseURL).pipe(take(1));
}

public getSalaById(idSala : number): Observable<Sala>{
  return this.http.get<Sala>(`${this.baseURL}/${idSala}`).pipe(take(1));
}


}
