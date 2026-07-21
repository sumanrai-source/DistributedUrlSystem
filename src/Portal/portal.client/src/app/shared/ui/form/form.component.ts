import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormGroup } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'app-form',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent {

  @Input() form!: FormGroup;

  // ✅ Dynamic field config with types
  @Input() fields: {
    label: string;
    controlName: string;
    type?: 'text' | 'email' | 'select' | 'checkbox';
    options?: { label: string; value: any }[];
  }[] = [];

  @Input() submitLabel: string = 'Submit';

  @Output() submitted = new EventEmitter<any>();

  onSubmit() {
    if (this.form.valid) {
      this.submitted.emit(this.form.value);
    }
  }
}