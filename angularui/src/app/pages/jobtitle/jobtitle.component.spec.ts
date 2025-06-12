import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobtitleComponent } from './jobtitle.component';

describe('JobtitleComponent', () => {
  let component: JobtitleComponent;
  let fixture: ComponentFixture<JobtitleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [JobtitleComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JobtitleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
