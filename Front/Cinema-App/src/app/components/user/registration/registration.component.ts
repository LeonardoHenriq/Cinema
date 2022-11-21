import { ValidadorField } from './../../../helpers/ValidadorField';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

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
      userName : ['',Validators.required],
      email : ['',[Validators.required , Validators.email]],
      password : ['',Validators.required],
      passwordConfirm : ['',Validators.required],
      funcao : ['',Validators.required]
    }, formOptions);
  }

}
