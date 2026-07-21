import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-gate',
  imports: [CommonModule, RouterModule],
  templateUrl: './gate.component.html',
  styleUrls: ['./gate.component.scss']
})
export class GateComponent {
}
