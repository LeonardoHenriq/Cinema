import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { TabsModule } from 'ngx-bootstrap/tabs';

import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';


import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';



import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FilmesComponent } from './components/filmes/filmes.component';
import { SalasComponent } from './components/salas/salas.component';
import { SessoesComponent } from './components/sessoes/sessoes.component';
import { NavComponent } from './shared/nav/nav.component';
import { TituloComponent } from './shared/titulo/titulo.component';
import { PerfilComponent } from './components/perfil/perfil.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';

import { DateTimeFormatPipe } from './helpers/DateTimeFormat.pipe';

import { FilmeService } from './services/Filme.service';
import { SalaService } from './services/Sala.service';
import { SessaoService } from './services/Sessao.service';
import { AccountService } from './services/Account.service';
import { TimeFormatPipe } from './helpers/TimeFormat.pipe';

@NgModule({
  declarations: [
    AppComponent,
    FilmesComponent,
    SalasComponent,
    SessoesComponent,
    NavComponent,
    DateTimeFormatPipe,
    TimeFormatPipe,
    TituloComponent,
    DashboardComponent,
    PerfilComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CollapseModule.forRoot(),
    TooltipModule.forRoot(),
    BsDropdownModule.forRoot(),
    BsDatepickerModule.forRoot(),
    PaginationModule.forRoot(),
    TabsModule.forRoot(),
    ModalModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 4000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true,
    }),
    NgxSpinnerModule
  ],
  providers: [
    FilmeService,
    SalaService,
    SessaoService,
    AccountService
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
