import {Routes} from "@angular/router";
import {LoginComponent} from './components/login/login.component';
import {CourseListComponent} from './components/course-list/course-list.component';
import {UserListComponent} from './components/user-list/user-list.component';

export const routes: Routes = [
    { path: 'courses', component: CourseListComponent },
    { path: 'login', component: LoginComponent },
    { path: `users`, component: UserListComponent },
    { path: '', redirectTo: '/courses', pathMatch: 'full' }
  ];