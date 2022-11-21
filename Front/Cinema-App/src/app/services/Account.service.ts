import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environments/environment';

@Injectable()
export class AccountService {

baseURL = environment.apiURL+'api/filme';

constructor(private http: HttpClient) { }

}
