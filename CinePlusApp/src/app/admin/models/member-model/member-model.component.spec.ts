import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MemberModelComponent } from './member-model.component';

describe('MemberModelComponent', () => {
  let component: MemberModelComponent;
  let fixture: ComponentFixture<MemberModelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MemberModelComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MemberModelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
