import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowingModelComponent } from './showing-model.component';

describe('ShowingModelComponent', () => {
  let component: ShowingModelComponent;
  let fixture: ComponentFixture<ShowingModelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowingModelComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowingModelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
