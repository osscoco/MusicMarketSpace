import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AnonymousGuard implements CanActivate {

  // Constructeur
  constructor(private authService: AuthService) {}

  // Vérification si l'utilisateur n'est pas connecté pour avoir le droit d'accéder aux composants protégés par ce guard
  canActivate(): boolean {
    if (!this.authService.isAuthenticated()) {
      return true;
    } else {
      return false;
    }
  }
}