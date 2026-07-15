import { Component, Input, TemplateRef } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-table',
  imports: [CommonModule],
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss']
})
export class TableComponent {

  @Input() columns: string[] = [];
  @Input() data: any[] = [];

  @Input() actionTemplate!: TemplateRef<any>;   // ✅ MUST EXIST
}