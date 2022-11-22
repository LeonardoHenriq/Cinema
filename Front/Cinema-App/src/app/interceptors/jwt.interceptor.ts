import { AccountService } from '@app/services/Account.service';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { catchError, Observable, take } from 'rxjs';
import { User } from '@app/models/Identity/User';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private accountService: AccountService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let currentUser = {} as User;

    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      currentUser = user

      if (currentUser) {
        request = request.clone({
          setHeaders: {
            Authorization: `Bearer ${currentUser.token}`
          }
        }
        );
      }
    });

    return next.handle(request)

  }
}
