import { TestBed } from '@angular/core/testing';

import { GenericCrudFactoryService } from './generic-crud-factory.service';

describe('GenericCrudFactoryService', () => {
  let service: GenericCrudFactoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GenericCrudFactoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
