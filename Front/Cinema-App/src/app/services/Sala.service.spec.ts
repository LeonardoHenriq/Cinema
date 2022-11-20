/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { SalaService } from './Sala.service';

describe('Service: Sala', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SalaService]
    });
  });

  it('should ...', inject([SalaService], (service: SalaService) => {
    expect(service).toBeTruthy();
  }));
});
