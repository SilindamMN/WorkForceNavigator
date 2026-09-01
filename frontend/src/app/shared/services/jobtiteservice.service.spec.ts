import { TestBed } from '@angular/core/testing';

import { JobtiteserviceService } from './jobtiteservice.service';

describe('JobtiteserviceService', () => {
  let service: JobtiteserviceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(JobtiteserviceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
