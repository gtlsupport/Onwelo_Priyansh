import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddVoterComponent } from './add-voter.component';

describe('AddVoterComponent', () => {
  let component: AddVoterComponent;
  let fixture: ComponentFixture<AddVoterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddVoterComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddVoterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
