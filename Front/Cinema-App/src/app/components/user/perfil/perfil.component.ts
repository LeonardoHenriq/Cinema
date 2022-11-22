import { UserUpdate } from './../../../models/Identity/UserUpdate';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '@app/services/Account.service';
import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidadorField } from 'src/app/helpers/ValidadorField';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {

  userUpdate = {} as UserUpdate;

  form!: FormGroup;

  constructor(public fb: FormBuilder,
              public accountService: AccountService,
              private router : Router,
              private toastr : ToastrService,
              private spinner: NgxSpinnerService
              ) {}

  get f(): any{
    return this.form.controls;
  }

  ngOnInit(): void {
    this.validation();
    this.carregarUsuario();
  }

  private carregarUsuario(): void {
    this.spinner.show();
    this.accountService.getUser().subscribe(
      (user : UserUpdate) => {
        console.log(user)
        this.userUpdate = user;
        this.form.patchValue(this.userUpdate);
        this.toastr.success('Usuario carregado com sucesso','Sucesso')
      },
      (error : any) => {
        console.log(error);
        this.toastr.error('Usuario nao carregado','Erro');
        this.router.navigate(['/dashboard'])
      }
    ).add(()=> this.spinner.hide());
  }

  private validation(): void{

    const formOptions: AbstractControlOptions = {
      validators: ValidadorField.MustMatch('password','passwordConfirm')
    }

    this.form = this.fb.group({
      userName :[''],
      nomeCompleto : ['',Validators.required],
      funcao : ['NaoInformado'],
      email : ['',[Validators.required , Validators.email]],
      password : ['',Validators.required],
      passwordConfirm : ['',Validators.required],
    }, formOptions);
  }

  public resetForm(event : any) : void{
    event.preventDefault();
    this.form.reset();
  }
  public onSubmit(): void{
    this.atualizarUsuario()

  }

  atualizarUsuario(): void {
    this.userUpdate ={...this.form.value}
    this.spinner.show();

    this.accountService.updateUser(this.userUpdate).subscribe(
      () => this.toastr.success('usuario foi atualizado','Sucesso'),
      (error: any) => {
        console.log(error);
        this.toastr.error('Erro ao tentar atualizar o usuario','Erro');
      }
    ).add(()=> this.spinner.hide())
  }
}
