import { Component, OnInit } from '@angular/core';
import { CourseService } from '../../services/course-list.service';  
import { Course } from '../../types/course/course.component';
import { NgForOf, NgIf } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-course-list',
  standalone: true,
  imports: [
    NgIf,
    NgForOf,
    RouterLink
  ],
  templateUrl: './course-list.component.html',
  styleUrls: ['./course-list.component.css']
})
export class CourseListComponent implements OnInit {
  courses: any[] = [];

  constructor(private courseService: CourseService) {}

  ngOnInit(): void {
    this.courseService.getAllCourses().subscribe(
      (data: Course[]) => {
        this.courses = data;
      },
      (error: any) => {
        console.error('Error fetching users:', error);
      }
    );
  }
}
