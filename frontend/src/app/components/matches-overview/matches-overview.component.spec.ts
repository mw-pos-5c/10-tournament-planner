import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MatchesOverviewComponent } from './matches-overview.component';

describe('MatchesOverviewComponent', () => {
  let component: MatchesOverviewComponent;
  let fixture: ComponentFixture<MatchesOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MatchesOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MatchesOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
