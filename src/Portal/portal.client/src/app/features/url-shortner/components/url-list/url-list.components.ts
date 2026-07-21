import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { UrlMappingsService } from '../../services/url.services';
import { NotificationService } from '../../../../core/services/notification.services';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { BehaviorSubject, switchMap, map } from 'rxjs';
import { UrlMapping } from '../../types/IUrl';
import { ButtonComponent } from '../../../../shared/ui/button/button.component';

@Component({
  standalone: true,
  selector: 'app-url-list',
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
  templateUrl: './url-list.components.html'
})
export class UrlMappingListComponent implements OnInit {

  displayedColumns: string[] = [ 'destinationUrl', 'actions'];
  dataSource = new MatTableDataSource<UrlMapping>([]);

  private search$ = new BehaviorSubject<string>('');
  private refresh$ = new BehaviorSubject<void>(undefined as any);

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private UrlMappingsService: UrlMappingsService,
    private notification: NotificationService
  ) {}

  ngOnInit(): void {
    this.dataSource.filterPredicate = (data: UrlMapping, filter: string) => {
      const term = filter.trim().toLowerCase();
      return (data.url ?? '').toLowerCase().includes(term);
    };

    this.refresh$.pipe(
      switchMap(() => this.UrlMappingsService.getUrlMappings()),
      map(res => res?.data ?? [])
    ).subscribe({
      next: (data) => {
        this.dataSource.data = data;
        setTimeout(() => {
          if (this.paginator) this.dataSource.paginator = this.paginator;
        });
      },
      error: (err) => console.error('Failed to load urls', err)
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
    this.UrlMappingsService.deleteUrlMapping(id).subscribe({
      next: () => {
        this.notification.showSuccess('UrlMapping deleted successfully');
        this.refresh$.next();
      },
      error: () => {
        this.notification.showError('Failed to delete role');
      }
    });
  }

}
