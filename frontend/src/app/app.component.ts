import { Component } from '@angular/core';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { CommonModule, NgFor, NgIf } from '@angular/common';
import { AuthService } from './services/auth.service';
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-root',
  standalone: true,
  providers: [
    provideHttpClient(withFetch())  // Configurarea pentru fetch
  ],
  imports: [CommonModule, RouterLink, RouterOutlet, NgIf, NgFor, MatIcon, MatButton],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private authService: AuthService, private router: Router) {}

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  title = 'frontend';
}
