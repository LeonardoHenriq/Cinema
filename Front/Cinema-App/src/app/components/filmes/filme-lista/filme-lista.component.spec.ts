import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FilmeListaComponent } from './filme-lista.component';

describe('FilmeListaComponent', () => {
  let component: FilmeListaComponent;
  let fixture: ComponentFixture<FilmeListaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FilmeListaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FilmeListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
