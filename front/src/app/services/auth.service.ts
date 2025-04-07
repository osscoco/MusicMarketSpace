import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { ToastService } from './toast.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  // Constructeur
  constructor(private api: ApiService, private toastService: ToastService, private router: Router) { }

  // Se connecter : Appel à l'ApiService
  login(email: string, password: string): Promise<boolean> {
    return new Promise((resolve, reject) => {
      this.api.login({ mail: email, password: password }).subscribe({
        next: async (data: any) => {
          if (data["success"] == true) {
            localStorage.setItem('isLoggedIn', 'true');
            localStorage.setItem('tokenApi', data.data[1]);
            this.toastService.show(data["message"], 'success');
            await this.router.navigateByUrl('/', { skipLocationChange: true });
            await this.router.navigateByUrl(this.router.url);
            resolve(true);
          } else if (data["success"] == false) {
            this.toastService.show(data["message"], 'danger');
            await this.router.navigateByUrl('/', { skipLocationChange: true });
            await this.router.navigateByUrl(this.router.url);
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
        next: async (data: any) => {
          if (data["success"] == true) {
            this.toastService.show(data["message"], 'success');
            await this.router.navigateByUrl('/', { skipLocationChange: true });
            await this.router.navigateByUrl(this.router.url);
            resolve(true);
          } else if (data["success"] == false) {
            this.toastService.show(data["message"], 'danger');
            await this.router.navigateByUrl('/', { skipLocationChange: true });
            await this.router.navigateByUrl(this.router.url);
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
        next: async (data: any) => {
          if (data["success"] == true) {
            localStorage.removeItem('isLoggedIn');
            localStorage.removeItem('tokenApi');
            this.toastService.show(data["message"], 'danger');
            await this.router.navigateByUrl('/', { skipLocationChange: true });
            await this.router.navigateByUrl(this.router.url);
            resolve(true);
          } else if (data["success"] == false) {
            this.toastService.show(data["message"], 'danger');
            await this.router.navigateByUrl('/', { skipLocationChange: true });
            await this.router.navigateByUrl(this.router.url);
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

  // Vérification si l'utilisateur courant est admin
  isAdmin(): boolean {
    return localStorage.getItem('isAdmin') === 'true';
  }
}