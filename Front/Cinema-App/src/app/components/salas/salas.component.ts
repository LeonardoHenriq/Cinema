import { SalaService } from '../../services/Sala.service';
import { Component } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Sala } from '../../models/Sala';

@Component({
  selector: 'app-salas',
  templateUrl: './salas.component.html',
  styleUrls: ['./salas.component.scss']
})
export class SalasComponent {
  public salas: Sala[] =[];

  constructor(private salaService: SalaService,
              private toastr: ToastrService,
              private spinner: NgxSpinnerService) { }

  public ngOnInit(): void{
    this.spinner.show();
    this.getSalas();
  }

  public getSalas(): void{

    this.salaService.getSalas().subscribe({
      next : (_Salas: Sala[]) => {
        this.salas = _Salas
      },
      error : (error : any) => {
        console.log(error);
        this.spinner.hide();
        this.toastr.error('Erro ao Carregar os Salas','Erro!');
      },
      complete: () => this.spinner.hide()
    });
  }
}
