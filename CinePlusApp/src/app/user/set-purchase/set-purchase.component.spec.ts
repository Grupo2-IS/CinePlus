import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SetPurchaseComponent } from './set-purchase.component';

describe('SetPurchaseComponent', () => {
  let component: SetPurchaseComponent;
  let fixture: ComponentFixture<SetPurchaseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SetPurchaseComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SetPurchaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
