import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { FilmeService } from './../../../services/Filme.service';
import { Filme } from 'src/app/models/Filme';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-filme-detalhe',
  templateUrl: './filme-detalhe.component.html',
  styleUrls: ['./filme-detalhe.component.scss']
})
export class FilmeDetalheComponent implements OnInit {

  public filme = {} as Filme;
  public form!: FormGroup;
  constructor(private fb: FormBuilder,
              private router: ActivatedRoute,
              private filmeService: FilmeService,
              private spinner : NgxSpinnerService,
              private toastr: ToastrService) {}

  get f(): any{
    return this.form.controls;
  }

  public carregarFilme(): void{
     const filmeIdParam = this.router.snapshot.paramMap.get('id');

     if(filmeIdParam !== null){
      this.spinner.show();
       this.form.get('titulo')?.disable();
       this.filmeService.getFilmeById(+filmeIdParam).subscribe({
        next: (filme: Filme) =>{
          this.filme = {...filme};
          console.log(filme)
          this.form.patchValue(this.filme);
        },
        error: (error: any) =>{
          this.spinner.hide()
          this.toastr.error('Erro ao tentar carregar o filme','Erro!');
          console.log(error);
        },
        complete: () =>{
          this.spinner.hide()
        },
       })
     }else{
      // this.form.get('titulo')?.enable()
     }
  }

  ngOnInit(): void {
    this.validation();
    this.carregarFilme();
  }

  public validation(): void {
    this.form = this.fb.group({
      titulo : ['', Validators.required],
      descricao : ['', Validators.required],
      duracao : ['', Validators.required],
      imagemURL : ['', Validators.required],
    });
  }

  resetForm(event : any): void {
    event.preventDefault();
    this.form.reset();
  }

  public cssValidator(campoForm: FormControl): any {
    return {'is-invalid': campoForm?.errors && campoForm?.touched};
  }
}
