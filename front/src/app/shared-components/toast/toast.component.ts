import { ChangeDetectorRef, Component } from '@angular/core';
import { ToastService } from '../../services/toast.service';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-toast',
  standalone: true,
  imports: [NgFor],
  templateUrl: './toast.component.html',
  styleUrl: './toast.component.scss'
})
export class ToastComponent {
  
  // Constructeur
  constructor(public toastService: ToastService, private cdr: ChangeDetectorRef) {}

  // Fermeture de la notification
  closeToast(toast: any) {
    this.toastService.remove(toast.message);
    this.cdr.detectChanges(); // Forcer la mise à jour
  }
}
