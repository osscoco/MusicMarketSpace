import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    
    if (req.url.endsWith('logout') || req.url.endsWith('getAuthMe')) {
      
      // Récupération du tocken dans le localstorage
      const token = localStorage.getItem('tokenApi');
  
      // Si le token existe
      if (token) {
  
        // On ajoute le token dans les Authorization des headers de la requête http interceptée
        const clonedRequest = req.clone({
          setHeaders: {
            Authorization: `Bearer ${token}`
          }
        });
        return next.handle(clonedRequest);
      }
    }

    return next.handle(req);
  }
}