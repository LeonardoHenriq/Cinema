import { UserUpdate } from './../models/Identity/UserUpdate';
import { User } from './../models/Identity/User';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environments/environment';
import { map, Observable, ReplaySubject, take } from 'rxjs';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';

@Injectable()
export class AccountService {

  private currentUserSource = new ReplaySubject<User>(1);

  public currentUser$ = this.currentUserSource.asObservable();

  baseURL = environment.apiURL+'api/Account/';

constructor(private http: HttpClient) { }

public login(model : any): Observable<void>{
  return this.http.post<User>(this.baseURL+'login', model).pipe(
    take(1),
    map((response: User)=> {
      const user = response;
      if(user){
        this.setCurrentUser(user)
      }
    })
  );
}

logout(): void{
  localStorage.removeItem('user');
  this.currentUserSource.next(null);
  this.currentUserSource.complete();
}

public setCurrentUser(user:User): void{
  localStorage.setItem('user', JSON.stringify(user));
  this.currentUserSource.next(user);
}

public getUser(): Observable<UserUpdate>{
  return this.http.get<UserUpdate>(this.baseURL + 'getUser').pipe(take(1));
}

public updateUser(model: UserUpdate): Observable<void>{
  return this.http.put<UserUpdate>(this.baseURL +'updateUser',model).pipe(
    take(1),
    map((user : UserUpdate) =>{
      this.setCurrentUser(user);
    })
  )
}

public register(model : any): Observable<void>{
  return this.http.post<User>(this.baseURL+'register', model).pipe(
    take(1),
    map((response: User)=> {
      const user = response;
      if(user){
        this.setCurrentUser(user)
      }
    })
  );
}


}
