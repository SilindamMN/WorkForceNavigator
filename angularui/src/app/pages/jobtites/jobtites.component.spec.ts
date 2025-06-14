import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobtitesComponent } from './jobtites.component';

describe('JobtitesComponent', () => {
  let component: JobtitesComponent;
  let fixture: ComponentFixture<JobtitesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [JobtitesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JobtitesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
