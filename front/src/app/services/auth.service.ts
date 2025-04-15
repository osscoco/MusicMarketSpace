import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { ToastService } from './toast.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  // Constructeur
  constructor(private api: ApiService, private toastService: ToastService) { }
  
  // Vérifier si l'utilisateur est connecté
  checkAuthentication(): Promise<boolean> {
    return new Promise((resolve, reject) => {
      if (localStorage.getItem("tokenApi") && localStorage.getItem("isLoggedIn")) {
        this.api.authMe().subscribe({
          next: async (data: any) => {
            if (data["success"] == true) {
              resolve(true);
            } else {
              localStorage.removeItem('tokenApi');
              resolve(true);
            }
          },
          error: (error: any) => {
            console.error('Erreur lors de la vérification du token:', error);
            localStorage.removeItem('tokenApi');
            reject(false);
          },
          complete: () => {}
        });
      }
    });
  }

  // Se connecter : Appel à l'ApiService
  login(email: string, password: string): Promise<boolean> {
    return new Promise((resolve, reject) => {
      this.api.login({ mail: email, password: password }).subscribe({
        next: async (data: any) => {
          if (data["success"] == true) {
            localStorage.setItem('isLoggedIn', 'true');
            localStorage.setItem('tokenApi', data.data);
            this.toastService.show(data["message"], 'success');
            resolve(true);
          } else if (data["success"] == false) {
            this.toastService.show(data["message"], 'danger');
            resolve(true);
          } else {
            reject(false);
          }
        },
        error: (error: any) => {
          console.error('Erreur lors de la connexion', error);
          reject(false);
        },
        complete: () => {
          setTimeout(() => {
            window.location.reload();
          }, 2000);
        }
      });
    });
  }

  // Se créer un compte : Appel à l'ApiService
  signup(pseudo: string, email: string, password: string): Promise<boolean> {
    return new Promise((resolve, reject) => {
      this.api.signup({ pseudo: pseudo, mail: email, password: password }).subscribe({
        next: async (data: any) => {
          if (data["success"] == true) {
            this.toastService.show(data["message"], 'success');
            resolve(true);
          } else if (data["success"] == false) {
            this.toastService.show(data["message"], 'danger');
            resolve(true);
          } else {
            reject(false);
          }
        },
        error: (error: any) => {
          console.error('Erreur lors de la connexion', error);
          reject(false);
        },
        complete: () => {
          setTimeout(() => {
            window.location.reload();
          }, 2000);
        }
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
            resolve(true);
          } else if (data["success"] == false) {
            this.toastService.show(data["message"], 'danger');
            resolve(true);
          } else {
            reject(false);
          }
        },
        error: (error: any) => {
          console.error('Erreur lors de la déconnexion', error);
          reject(false);
        },
        complete: () => {
          setTimeout(() => {
            window.location.reload();
          }, 2000);
        }
      });
    });
  }
}