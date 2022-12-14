import { AccountService } from '@app/services/Account.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit  {

  isCollapsed = true;
  constructor(private router:Router,
              public accountService : AccountService) { }

  ngOnInit(): void{

  }

  logout(): void {
    this.accountService.logout();
    this.router.navigateByUrl('/user/login');
    window.location.reload();

  }

  showMenu(): boolean{
    return this.router.url !== '/user/login';
  }
}
