import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidadorField } from 'src/app/helpers/ValidadorField';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {
  form!: FormGroup;

  constructor(public fb: FormBuilder) {}

  get f(): any{
    return this.form.controls;
  }

  ngOnInit(): void {
    this.validation();
  }

  private validation(): void{

    const formOptions: AbstractControlOptions = {
      validators: ValidadorField.MustMatch('password','passwordConfirm')
    }

    this.form = this.fb.group({
      nomeCompleto : ['',Validators.required],
      email : ['',[Validators.required , Validators.email]],
      password : ['',Validators.required],
      passwordConfirm : ['',Validators.required],
    }, formOptions);
  }
}
