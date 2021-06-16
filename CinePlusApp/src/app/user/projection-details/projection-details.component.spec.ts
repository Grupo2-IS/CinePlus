import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectionDetailsComponent } from './projection-details.component';

describe('ProjectionDetailsComponent', () => {
  let component: ProjectionDetailsComponent;
  let fixture: ComponentFixture<ProjectionDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProjectionDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectionDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
