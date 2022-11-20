/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { FilmeService } from './Filme.service';

describe('Service: Filme', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FilmeService]
    });
  });

  it('should ...', inject([FilmeService], (service: FilmeService) => {
    expect(service).toBeTruthy();
  }));
});
