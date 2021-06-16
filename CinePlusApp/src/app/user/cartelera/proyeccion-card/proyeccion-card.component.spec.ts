import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProyeccionCardComponent } from './proyeccion-card.component';

describe('ProyeccionCardComponent', () => {
  let component: ProyeccionCardComponent;
  let fixture: ComponentFixture<ProyeccionCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProyeccionCardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProyeccionCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
