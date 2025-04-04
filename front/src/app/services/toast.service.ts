import { Injectable } from '@angular/core';
import { Toast } from '../interfaces/toast.interface';

@Injectable({ 
    providedIn: 'root' 
})
export class ToastService {

  // Liste des notifications
  toasts: Toast[] = [];

  // Afficher la notification
  show(message: string, type: 'success' | 'warning' | 'info' | 'danger' = 'success') {

    // Ajouter la notification à la liste des notifications
    this.toasts.push({ message, type });

    // Afficher la notification sur le DOM après 10ms d'attente
    setTimeout(() => {
      const toastElement = document.querySelector(`.toast[data-message="${message}"]`);
      if (toastElement) {
        toastElement.classList.add('show');
      }
    }, 10);

    // Supprimer automatiquement la notification après 5s d'attente
    setTimeout(() => this.remove(message), 3000);
  }

  // Supprimer la notification
  remove(message: string) {

    // Supprimer la notification sur le DOM
    const toastElement = document.querySelector(`.toast[data-message="${message}"]`);
    if (toastElement) {
      toastElement.classList.add('hide');
    }

    // Mis à jour de la liste des notifications (on ne garde que celles dont le message est différent de celle supprimée) après 400ms d'attente
    setTimeout(() => {
      this.toasts = this.toasts.filter(toast => toast.message !== message);
    }, 400);
  }
}