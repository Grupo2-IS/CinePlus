import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowingFormComponent } from './showing-form.component';

describe('ShowingFormComponent', () => {
  let component: ShowingFormComponent;
  let fixture: ComponentFixture<ShowingFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowingFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowingFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
