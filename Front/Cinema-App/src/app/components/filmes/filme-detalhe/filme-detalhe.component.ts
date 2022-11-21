import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

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
  public estadoSalvar = 'post';

  constructor(private fb: FormBuilder,
              private activatedrouter: ActivatedRoute,
              private filmeService: FilmeService,
              private spinner : NgxSpinnerService,
              private toastr: ToastrService,
              private router: Router) {}

  get f(): any{
    return this.form.controls;
  }

  public carregarFilme(): void{
     const filmeIdParam = this.activatedrouter.snapshot.paramMap.get('id');


     if(filmeIdParam !== null){
      this.estadoSalvar = 'put';
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

  public salvarAlteracao(): void{
    this.spinner.show();
    if(this.form.valid){

      this.filme = this.estadoSalvar === 'post' ? {... this.form.value} :
        this.filme = {id: this.filme.id,... this.form.value};

      this.filmeService[this.estadoSalvar](this.filme).subscribe({
        next:(filmeRetorno : Filme)=>{
          this.toastr.success('Filme salvo com sucesso','Sucesso!');
          this.router.navigate([`filme/detalhe/${filmeRetorno.id}`]);
        },
        error:(error: any)=>{
          console.log(error);
          this.spinner.hide();
          this.toastr.error('Erro ao tentar salvar o filme','Erro');
        },
        complete:()=> this.spinner.hide()
      });
    }
  }
}
