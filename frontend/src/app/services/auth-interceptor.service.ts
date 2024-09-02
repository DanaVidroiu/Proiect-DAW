import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.authService.getToken();
    // console.log('AuthInterceptor - Token:', token);

    if (token) {
      const myRequest =  req.clone({
        setHeaders: {
          'Authorization': `Bearer ${token}`
        }
      });
      // console.log('AuthInterceptor - Sending Request with Headers:', cloned.headers.get('Authorization'));
      return next.handle(myRequest);
    } else {
      // console.log('AuthInterceptor - No Token');
      return next.handle(req);
    }
  }
}