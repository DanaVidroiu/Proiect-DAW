import { TestBed } from '@angular/core/testing';

import { UserService } from './user-list.service';

describe('UserListService', () => {
  let service: UserService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});