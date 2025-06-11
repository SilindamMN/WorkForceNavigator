import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BasemodalComponent } from './basemodal.component';

describe('BasemodalComponent', () => {
  let component: BasemodalComponent;
  let fixture: ComponentFixture<BasemodalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BasemodalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BasemodalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
