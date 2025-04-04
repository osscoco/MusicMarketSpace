import { AfterViewInit, Component, ElementRef, HostListener, OnInit, ViewChild } from '@angular/core';
import { Modal } from 'bootstrap';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { NgClass, NgIf, NgStyle } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { EmailValidators } from '../../validators/email-validators';

@Component({
  selector: 'app-navbar-anonymous',
  standalone: true,
  imports: [ReactiveFormsModule, NgIf, NgClass, NgStyle, MatButtonModule],
  templateUrl: './navbar-anonymous.component.html',
  styleUrl: './navbar-anonymous.component.scss'
})
export class NavbarAnonymousComponent implements OnInit, AfterViewInit {

  // Permet au Modal Login d'être fermé si l'utilisateur clic dans une zone extérieur au modal
  @ViewChild('modalElementLogin') modalElementLogin!: ElementRef;
  @HostListener('document:click', ['$event'])
  onClickOutsideLogin(event: Event) {
    if (this.modalElementLogin && !this.modalElementLogin.nativeElement.contains(event.target)) {
      this.formGroupLogin.markAsUntouched();
    }
  }

  // Permet au Modal Signup d'être fermé si l'utilisateur clic dans une zone extérieur au modal
  @ViewChild('modalElementSignup') modalElementSignup!: ElementRef;
  @HostListener('document:click', ['$event'])
  onClickOutsideSignup(event: Event) {
    if (this.modalElementSignup && !this.modalElementSignup.nativeElement.contains(event.target)) {
      this.formGroupSignup.markAsUntouched();
    }
  }

  // Instance du modal Login
  private modalInstanceLogin!: Modal;  

  // Instance du modal Signup
  private modalInstanceSignup!: Modal;  

  // Responsive pour les Tooltips
  isMobileOrTablet = false;

  // Constructeur
  constructor(private authService: AuthService) {}

  // Formulaire Login
  formGroupLogin = new FormGroup({
    email: new FormControl("", [Validators.required, EmailValidators.emailFormat]),
    password: new FormControl("", [Validators.required, Validators.minLength(5)]) 
  });

  // Formulaire Signup
  formGroupSignup = new FormGroup({
    pseudo: new FormControl("", [Validators.required]),
    email: new FormControl("", [Validators.required, EmailValidators.emailFormat]),
    password: new FormControl("", [Validators.required, Validators.minLength(5)]) 
  });

  // Si les champs du formulaire sont invalides et (touchés ou modifiés)
  isInvalidAndTouchedOrDirty(formControl: FormControl) {
    return formControl.invalid && (formControl.touched ||formControl.dirty);
  }

  /// ---
  /// --- Cette partie est diédiée au bon affichage des éléments du menu navbar en fonction du support (PC, Tablette, Mobile)
  /// ---
  ngOnInit(): void {
    this.checkViewport();
  }

  @HostListener('window:resize')
  onResize() {
    this.checkViewport();
  }

  checkViewport() {
    const width = window.innerWidth;
    this.isMobileOrTablet = width >= 320 && width <= 480;
  }
  /// ---
  /// --- Fin de la partie dédiée
  /// ---

  // Après avoir chargé la vue du composant, on initialise une nouvelle instance du modal pour le faire apparaitre
  ngAfterViewInit() {
    const modalElementLogin = document.getElementById('LoginModal');
    const modalElementSignup = document.getElementById('SignupModal');
    if (modalElementLogin) {
      this.modalInstanceLogin = new Modal(modalElementLogin);
    }
    if (modalElementSignup) {
      this.modalInstanceSignup = new Modal(modalElementSignup);
    }
  }

  // Une fois le formulaire Login soumis
  async OnSubmitLogin() {
    this.formGroupLogin.markAllAsTouched();
    this.modalInstanceLogin.hide();
    this.removeBackDropModalFromDOM();
    if (this.formGroupLogin.invalid) {
      return;
    } else {
      if (this.formGroupLogin.value.email! && this.formGroupLogin.value.password!) {
        await this.authService.login(this.formGroupLogin.value.email, this.formGroupLogin.value.password);
      }
    }
  }

  // Fermeture du modal Login (côté template html du composant)
  closeModalLogin() {
    this.formGroupLogin.markAsUntouched();
    this.modalInstanceLogin.hide();
  }

  // Une fois le formulaire Signup soumis
  async OnSubmitSignup() {
    this.formGroupSignup.markAllAsTouched();
    this.modalInstanceLogin.hide();
    this.removeBackDropModalFromDOM();
    if (this.formGroupSignup.invalid) {
      return;
    } else {
      try {
        if (this.formGroupSignup.value.pseudo! &&this.formGroupSignup.value.email! && this.formGroupSignup.value.password!) {
          await this.authService.signup(this.formGroupSignup.value.pseudo, this.formGroupSignup.value.email, this.formGroupSignup.value.password);
        }
      } catch (error) {
        alert('Erreur de création de compte');
      }
    }
  }

  // Fermeture du Modal Signup (côté template html du composant)
  closeModalSignup() {
    this.formGroupSignup.markAsUntouched();
    this.modalInstanceSignup.hide();
  }

  removeBackDropModalFromDOM() {
    document.querySelectorAll('.modal-backdrop').forEach(el => el.remove());
    document.body.classList.remove('modal-open');
  }
}
