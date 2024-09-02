import { TestBed } from '@angular/core/testing';

import { CourseService } from './course-list.service';

describe('CourseListService', () => {
  let service: CourseService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CourseService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
