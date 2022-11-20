import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Sala } from '../models/Sala';

@Injectable()
export class SalaService {

  baseURL = 'https://localhost:5001/api/sala';

constructor(private http: HttpClient) { }

public getSalas(): Observable<Sala[]>{
  return this.http.get<Sala[]>(this.baseURL)
}

public getSalaById(idSala : number): Observable<Sala>{
  return this.http.get<Sala>(`${this.baseURL}/${idSala}`)
}


}
