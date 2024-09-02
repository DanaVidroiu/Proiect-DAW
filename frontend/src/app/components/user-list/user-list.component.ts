import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user-list.service';
import { User } from '../../types/user/user.component';
import { NgForOf, NgIf } from "@angular/common";
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [
    NgIf,
    NgForOf,
    RouterLink
  ],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css'
})
export class UserListComponent implements OnInit {
  users: any[] = [];

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.userService.getAllUsers().subscribe(
      (data: User[]) => {
        this.users = data;
      },
      (error: any) => {
        console.error('Error fetching users:', error);
      }
    );
  }
}