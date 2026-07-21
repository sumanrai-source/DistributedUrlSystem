import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SlugMappingsService } from '../../services/slug.services';
import { NotificationService } from '../../../../core/services/notification.services';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { BehaviorSubject, switchMap, map } from 'rxjs';
import { UrlSlug } from '../../types/ISlug';
import { ButtonComponent } from '../../../../shared/ui/button/button.component';

@Component({
  standalone: true,
  selector: 'app-slug-list',
  imports: [
    CommonModule,
    RouterModule,
    MatTableModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    ButtonComponent
  ],
  templateUrl: './slug-list.components.html'
})
export class SlugsMappingListComponent implements OnInit {

  displayedColumns: string[] = [ 'slug', 'status','actions'];
  dataSource = new MatTableDataSource<UrlSlug>([]);

  private search$ = new BehaviorSubject<string>('');
  private refresh$ = new BehaviorSubject<void>(undefined as any);

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private SlugMappingsService: SlugMappingsService,
    private notification: NotificationService
  ) {}

  ngOnInit(): void {
    this.dataSource.filterPredicate = (data: UrlSlug, filter: string) => {
      const term = filter.trim().toLowerCase();
      return (data.slug ?? '').toLowerCase().includes(term) || 
      (data.status ?? 0).toString().includes(term);
    };

    this.refresh$
  .pipe(
    switchMap(() => this.SlugMappingsService.getSlugMappings())
  )
  .subscribe({
    next: (data) => {
      this.dataSource.data = data;

      setTimeout(() => {
        if (this.paginator) {
          this.dataSource.paginator = this.paginator;
        }
      });
    },
    error: (err) => console.error('Failed to load slugs', err)
  });

    this.search$.subscribe(term => {
      this.dataSource.filter = term;
    });

    // initial load
    this.refresh$.next();
  }

  onSearch(term: string): void {
    this.search$.next(term.trim().toLowerCase());
  }

  delete(id?: string): void {
    if (!id) return;
    this.SlugMappingsService.deleteSlugMapping(id).subscribe({
      next: () => {
        this.notification.showSuccess('Slugs deleted successfully');
        this.refresh$.next();
      },
      error: () => {
        this.notification.showError('Failed to delete slugs');
      }
    });
  }

}
