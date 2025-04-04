import { AbstractControl, ValidationErrors } from '@angular/forms';

export class EmailValidators {
  static emailFormat(control: AbstractControl): ValidationErrors | null {
    const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

    // Vérifie si le champ est vide, on ne veut pas afficher d'erreur si l'utilisateur n'a rien entré
    if (!control.value) {
      return null;
    }

    return emailRegex.test(control.value) ? null : { emailFormat: true };
  }
}