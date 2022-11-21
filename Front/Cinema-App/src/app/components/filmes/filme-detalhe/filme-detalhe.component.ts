import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-filme-detalhe',
  templateUrl: './filme-detalhe.component.html',
  styleUrls: ['./filme-detalhe.component.scss']
})
export class FilmeDetalheComponent implements OnInit {

  public form!: FormGroup;
  constructor(private fb: FormBuilder) {}

  get f(): any{
    return this.form.controls;
  }

  ngOnInit(): void {
    this.validation();
  }

  public validation(): void {
    this.form = this.fb.group({
      titulo : ['', Validators.required],
      descricao : ['', Validators.required],
      duracao : ['', Validators.required],
      imagemURL : ['', Validators.required],
    });
  }

  resetForm(): void {
    this.form.reset();
  }
}
