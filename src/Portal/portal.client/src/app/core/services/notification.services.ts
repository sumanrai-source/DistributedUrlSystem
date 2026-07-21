import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {


showSuccess(message: string): void {
    alert(message);
  }

  showError(message: string): void {
    alert(message);
  }

}