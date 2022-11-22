import { Router } from '@angular/router';
import { User } from './../../../models/Identity/User';
import { ValidadorField } from './../../../helpers/ValidadorField';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { AccountService } from '@app/services/Account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  user = {} as User;
  form!: FormGroup;

  constructor(private fb: FormBuilder,
              private accountService: AccountService,
              private router: Router,
              private toastr: ToastrService) {}

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
      funcao : ['']
    }, formOptions);
  }

  register(): void {
    this.user = {...this.form.value};
    this.accountService.register(this.user).subscribe(
      () => this.router.navigateByUrl('/dashboard'),
      (error : any) => this.toastr.error(error.error)
    )
  }

}
