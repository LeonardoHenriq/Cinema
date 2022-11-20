import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { FilmesComponent } from './components/filmes/filmes.component';
import { SalasComponent } from './components/salas/salas.component';
import { SessoesComponent } from './components/sessoes/sessoes.component';
import { PerfilComponent } from './components/perfil/perfil.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';

const routes: Routes = [
  {path:'filmes',component : FilmesComponent},
  {path:'salas',component : SalasComponent},
  {path:'sessoes',component : SessoesComponent},
  {path:'perfil',component : PerfilComponent},
  {path:'dashboard',component : DashboardComponent},
  {path:'',redirectTo : 'dashboard', pathMatch: 'full'},
  {path:'**',redirectTo : 'dashboard', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
