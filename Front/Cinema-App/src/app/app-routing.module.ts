import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { FilmesComponent } from './components/filmes/filmes.component';
import { FilmeDetalheComponent } from './components/filmes/filme-detalhe/filme-detalhe.component';
import { FilmeListaComponent } from './components/filmes/filme-lista/filme-lista.component';

import { SalasComponent } from './components/salas/salas.component';

import { SessoesComponent } from './components/sessoes/sessoes.component';
import { SessaoListaComponent } from './components/sessoes/sessao-lista/sessao-lista.component';
import { SessaoNovoComponent } from './components/sessoes/sessao-novo/sessao-novo.component';

import { DashboardComponent } from './components/dashboard/dashboard.component';

import { PerfilComponent } from './components/user/perfil/perfil.component';
import { UserComponent } from './components/user/user.component';
import { LoginComponent } from './components/user/login/login.component';
import { RegistrationComponent } from './components/user/registration/registration.component';

const routes: Routes = [
  {
    path: 'user', component: UserComponent,
    children: [
      {path: 'login', component: LoginComponent},
      {path: 'registration', component: RegistrationComponent},
    ]
  },
  {path:'user/perfil',component : PerfilComponent},
  {path: 'filmes',redirectTo : 'filmes/lista'},
  {path: 'sessoes',redirectTo : 'sessoes/lista'},
  {
    path:'filmes',component : FilmesComponent,
    children: [
      {path:'detalhe/:id',component : FilmeDetalheComponent},
      {path:'detalhe',component : FilmeDetalheComponent},
      {path:'lista',component : FilmeListaComponent}
    ]
  },
  {path:'salas',component : SalasComponent},
  {
    path:'sessoes',component : SessoesComponent,
    children: [
      {path:'novo',component : SessaoNovoComponent},
      {path:'lista',component : SessaoListaComponent}
    ]
  },
  {path:'dashboard',component : DashboardComponent},
  {path:'',redirectTo : 'dashboard', pathMatch: 'full'},
  {path:'**',redirectTo : 'dashboard', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
