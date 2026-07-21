import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';

import { FormComponent } from '../../../../shared/ui/form/form.component';
import { SlugMappingsService } from '../../services/slug.services';
import { NotificationService } from '../../../../core/services/notification.services';
import { AddSlug, UrlSlug } from '../../types/ISlug';

@Component({
  standalone: true,
  selector: 'app-slug-form',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormComponent
  ],
  templateUrl: './slug-form.components.html'
})
export class SlugsMappingsFormComponent implements OnInit {

  form!: FormGroup;
  roleId: string | null = null;

  fields = [
    {
      label: 'Name',
      controlName: 'name',
      type: 'text' as const
    }
  ];

  constructor(
    private fb: FormBuilder,
    private service: SlugMappingsService,
    private router: Router,
    private route: ActivatedRoute,
    private notification: NotificationService
  ) {}

  ngOnInit(): void {

    this.form = this.fb.group({
        id: [''],
        name: ['', Validators.required]
    });

  }


  handleSubmit(): void {

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this.notification.showError(
        'Slug Name are required'
      );
      return;
    }

    const formValue = this.form.getRawValue();

    const payload = {
      name: formValue.name?.trim(),
    };

    console.log('Submitting:', payload);

    if (!formValue.id) {

      this.service.createSlugMapping(payload as AddSlug).subscribe({
        next: () => {

          this.notification.showSuccess(
            'Slugs created successfully'
          );

          this.router.navigate(['/url-shortner']);
        },
        error: (err) => {

          console.error('Create failed:', err);

          this.notification.showError(
            err?.error?.message ||
            'Failed to create Slugs'
          );
        }
      });

    } else {

      this.service.updateSlugMapping({
        id: formValue.id,
        ...payload
      }).subscribe({
        next: () => {

          this.notification.showSuccess(
            'urlMapping updated successfully'
          );

          this.router.navigate(['/slug']);
        },
        error: (err) => {

          console.error('Update failed:', err);

          this.notification.showError(
            err?.error?.message ||
            'Failed to update slug'
          );
        }
      });
    }
  }
}