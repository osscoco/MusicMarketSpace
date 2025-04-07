import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {

  // Constructeur
  constructor(private authService: AuthService) {}

  // Vérification si l'utilisateur est connecté pour avoir le droit d'accéder aux composants protégés par ce guard (et admin)
  canActivate(): boolean {
    if (this.authService.isAuthenticated() && this.authService.isAdmin()) {
      return true;
    } else {
      return false;
    }
  }
}