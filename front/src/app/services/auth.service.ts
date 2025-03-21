import { Injectable } from '@angular/core';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  // Constructeur
  constructor(private api: ApiService) { }

  // Se connecter : Appel à l'ApiService
  login(email: string, password: string): Promise<boolean> {
    return new Promise((resolve, reject) => {
      this.api.login({ mail: email, password: password }).subscribe({
        next: (data: any) => {
          if (data["success"] == true) {
            localStorage.setItem('isLoggedIn', 'true');
            localStorage.setItem('tokenApi', data.data[1]);
            resolve(true);
          } else {
            reject(false);
          }
        },
        error: (error: any) => {
          console.error('Erreur lors de la connexion', error);
          reject(false);
        },
        complete: () => {}
      });
    });
  }

  // Se créer un compte : Appel à l'ApiService
  signup(pseudo: string, email: string, password: string): Promise<boolean> {
    return new Promise((resolve, reject) => {
      this.api.signup({ pseudo: pseudo, mail: email, password: password, contactPhone: null, isSSO: false, roleId: '08dd559d-716c-417b-8e4b-51b69469733c' }).subscribe({
        next: (data: any) => {
          if (data["success"] == true) {
            resolve(true);
          } else {
            reject(false);
          }
        },
        error: (error: any) => {
          console.error('Erreur lors de la connexion', error);
          reject(false);
        },
        complete: () => {}
      });
    });
  }

  // Se déconnecter : Appel à l'ApiService
  logout(): Promise<boolean> {
    return new Promise((resolve, reject) => {
      this.api.logout().subscribe({
        next: (data: any) => {
          if (data["success"] == true) {
            localStorage.removeItem('isLoggedIn');
            localStorage.removeItem('tokenApi');
            resolve(true);
          } else {
            reject(false);
          }
        },
        error: (error: any) => {
          console.error('Erreur lors de la déconnexion', error);
          reject(false);
        },
        complete: () => {}
      });
    });
  }

  // Vérification si l'utilisateur courant est connecté ou non
  isAuthenticated(): boolean {
    return localStorage.getItem('isLoggedIn') === 'true';
  }
}