import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';

import { FormComponent } from '../../../../shared/ui/form/form.component';
import { UrlMappingsService } from '../../services/url.services';
import { NotificationService } from '../../../../core/services/notification.services';
import { UrlMapping } from '../../types/IUrl';

@Component({
  standalone: true,
  selector: 'app-url-form',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormComponent
  ],
  templateUrl: './url-form.components.html'
})
export class UrlMappingsFormComponent implements OnInit {

  form!: FormGroup;
  roleId: string | null = null;

  fields = [
    {
      label: 'Urls',
      controlName: 'url',
      type: 'text' as const
    }
  ];

  constructor(
    private fb: FormBuilder,
    private service: UrlMappingsService,
    private router: Router,
    private route: ActivatedRoute,
    private notification: NotificationService
  ) {}

  ngOnInit(): void {

    this.form = this.fb.group({
        id: [''],
        url: ['', Validators.required]
    });

  }


  handleSubmit(): void {

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this.notification.showError(
        'Slug Name and Destination Url are required'
      );
      return;
    }

    const formValue = this.form.getRawValue();

    const payload = {
      url: formValue.url?.trim(),
    };

    console.log('Submitting:', payload);

    if (!formValue.id) {

      this.service.createUrlMapping(payload as UrlMapping).subscribe({
        next: () => {

          this.notification.showSuccess(
            'UrlMapping created successfully'
          );

          this.router.navigate(['/url-shortner']);
        },
        error: (err) => {

          console.error('Create failed:', err);

          this.notification.showError(
            err?.error?.message ||
            'Failed to create urlMapping'
          );
        }
      });

    } else {

      this.service.updateUrlMapping({
        id: formValue.id,
        ...payload
      }).subscribe({
        next: () => {

          this.notification.showSuccess(
            'urlMapping updated successfully'
          );

          this.router.navigate(['/urlMapping']);
        },
        error: (err) => {

          console.error('Update failed:', err);

          this.notification.showError(
            err?.error?.message ||
            'Failed to update urlMapping'
          );
        }
      });
    }
  }
}