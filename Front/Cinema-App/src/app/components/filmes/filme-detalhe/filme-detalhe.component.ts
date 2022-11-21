import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { FilmeService } from './../../../services/Filme.service';
import { Filme } from 'src/app/models/Filme';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { environment } from '@environments/environment';

@Component({
  selector: 'app-filme-detalhe',
  templateUrl: './filme-detalhe.component.html',
  styleUrls: ['./filme-detalhe.component.scss']
})
export class FilmeDetalheComponent implements OnInit {

  public filmeId : number;
  public filme = {} as Filme;
  public form!: FormGroup;
  public estadoSalvar = 'post';

  public imagemURL = 'assets/img/upload.png';
  public file: File;


  get modoEditar(): boolean {
    return this.estadoSalvar === 'put';
  }

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
     this.filmeId = +this.activatedrouter.snapshot.paramMap.get('id');


     if(this.filmeId !== null && this.filmeId !== 0){
      this.estadoSalvar = 'put';
      this.spinner.show();
       this.form.get('titulo')?.disable();
       this.filmeService.getFilmeById(this.filmeId).subscribe({
        next: (filme: Filme) =>{
          this.filme = {...filme};
          this.form.patchValue(this.filme);
          if(this.filme.imagemURL !== ''){
            this.imagemURL = environment.apiURL + 'resources/images/' + this.filme.imagemURL;
          }
        },
        error: (error: any) =>{
          this.toastr.error('Erro ao tentar carregar o filme','Erro!');
          console.log(error);
        },
       }).add(()=> this.spinner.hide());
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
      imagemURL : [''],
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
          this.router.navigate([`filmes/detalhe/${filmeRetorno.id}`]);
        },
        error:(error: any)=>{
          console.log(error);
          this.toastr.error('Erro ao tentar salvar o filme','Erro');
        }
      }).add(()=> this.spinner.hide());
    }
  }

  onFileChange(ev: any): void {
    const reader = new FileReader();

    reader.onload = (event: any) => this.imagemURL = event.target.result;

    this.file = ev.target.files;
    reader.readAsDataURL(this.file[0]);

    this.uploadImagem();
  }

  uploadImagem(): void {
    this.spinner.show();
    this.filmeService.postUpload(this.filmeId, this.file).subscribe({
      next: () =>{
        this.carregarFilme();
        this.toastr.success('Imagem Atualizada com sucesso!','Sucesso!');
      },
      error: (error : any) =>{
        this.toastr.error('Erro ao fazer upload de imagem','Sucesso!');
        console.log(error);
      },
    }).add(()=> this.spinner.hide());
  }
}
