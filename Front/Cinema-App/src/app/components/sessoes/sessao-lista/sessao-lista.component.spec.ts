import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SessaoListaComponent } from './sessao-lista.component';

describe('SessaoListaComponent', () => {
  let component: SessaoListaComponent;
  let fixture: ComponentFixture<SessaoListaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SessaoListaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SessaoListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
