import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-button',
  imports: [CommonModule, RouterModule],
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss']
})
export class ButtonComponent {

  // ✅ Button style type
  @Input() type: 'primary' | 'danger' | 'success' = 'primary';

  // ✅ Button text
  @Input() label: string = '';

  // ✅ Optional routing
  @Input() routerLink: any;

  // ✅ Click event (for actions like delete)
  @Output() clicked = new EventEmitter<void>();

  onClick() {
    this.clicked.emit();
  }
}
``