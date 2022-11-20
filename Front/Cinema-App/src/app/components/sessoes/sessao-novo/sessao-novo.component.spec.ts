import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SessaoNovoComponent } from './sessao-novo.component';

describe('SessaoNovoComponent', () => {
  let component: SessaoNovoComponent;
  let fixture: ComponentFixture<SessaoNovoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SessaoNovoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SessaoNovoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
