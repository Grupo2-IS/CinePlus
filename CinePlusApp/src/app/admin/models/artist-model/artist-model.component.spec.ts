import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArtistModelComponent } from './artist-model.component';

describe('ArtistModelComponent', () => {
  let component: ArtistModelComponent;
  let fixture: ComponentFixture<ArtistModelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ArtistModelComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ArtistModelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
