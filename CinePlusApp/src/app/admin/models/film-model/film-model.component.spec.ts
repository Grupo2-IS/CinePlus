import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FilmModelComponent } from './film-model.component';

describe('FilmModelComponent', () => {
  let component: FilmModelComponent;
  let fixture: ComponentFixture<FilmModelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FilmModelComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FilmModelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
