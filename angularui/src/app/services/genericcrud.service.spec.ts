import { TestBed } from '@angular/core/testing';

import { GenericcrudService } from './genericcrud.service';

describe('GenericcrudService', () => {
  let service: GenericcrudService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GenericcrudService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
