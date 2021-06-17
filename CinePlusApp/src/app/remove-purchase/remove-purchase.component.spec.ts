import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RemovePurchaseComponent } from './remove-purchase.component';

describe('RemovePurchaseComponent', () => {
  let component: RemovePurchaseComponent;
  let fixture: ComponentFixture<RemovePurchaseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RemovePurchaseComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RemovePurchaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
