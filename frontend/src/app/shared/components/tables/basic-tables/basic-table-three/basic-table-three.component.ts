import { CommonModule } from '@angular/common';
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { ButtonComponent } from '../../../ui/button/button.component';
import { TableDropdownComponent } from '../../../common/table-dropdown/table-dropdown.component';
import { BadgeComponent } from '../../../ui/badge/badge.component';

export interface TableColumn {
  key: string;
  label: string;
}

@Component({
  selector: 'app-basic-table-three',
  standalone: true,
  imports: [
    CommonModule,
    ButtonComponent,
    TableDropdownComponent,
    BadgeComponent
  ],
  templateUrl: './basic-table-three.component.html',
  styles: ``
})
export class BasicTableThreeComponent {

  @Input() name: string = '';

  @Input() data: any[] = [];

  @Input() columns: TableColumn[] = [];

  @Input() itemsPerPage: number = 5;

  @Output() view = new EventEmitter<any>();

  @Output() delete = new EventEmitter<any>();
  @Input () title: string = '';
  @Input() canAdd: boolean = true;
@Input() canEdit: boolean = true;
@Input() canDelete: boolean = true;

@Output() add = new EventEmitter<void>();
@Output() edit = new EventEmitter<any>();




  currentPage = 1;

  searchTerm = '';

  get filteredData(): any[] {

    if (!this.searchTerm.trim()) {
      return this.data;
    }

    const search = this.searchTerm.toLowerCase();

    return this.data.filter(item =>
      this.columns.some(column =>
        String(item[column.key] ?? '')
          .toLowerCase()
          .includes(search)
      )
    );
  }

  get totalPages(): number {

    return Math.max(
      1,
      Math.ceil(
        this.filteredData.length / this.itemsPerPage
      )
    );
  }

  get currentItems(): any[] {

    const start =
      (this.currentPage - 1) * this.itemsPerPage;

    return this.filteredData.slice(
      start,
      start + this.itemsPerPage
    );
  }


  onSearch(event: Event): void {

    const input = event.target as HTMLInputElement;

    this.searchTerm = input.value;

    this.currentPage = 1;
  }


  goToPage(page: number): void {

    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
    }
  }


handleAdd(): void {
  this.add.emit();
}

handleEdit(item: any): void {
  this.edit.emit(item);
}

  handleDelete(item: any): void {

    this.delete.emit(item);
  }


  getBadgeColor(
    status: string
  ): 'success' | 'warning' | 'error' {

    switch (status?.toLowerCase()) {

      case 'success':
      case 'active':
      case 'approved':
      case 'completed':
        return 'success';

      case 'pending':
      case 'processing':
      case 'in progress':
        return 'warning';

      case 'failed':
      case 'inactive':
      case 'rejected':
      case 'cancelled':
        return 'error';

      default:
        return 'warning';
    }
  }


  isStatusColumn(column: TableColumn): boolean {

    return column.key.toLowerCase() === 'status';
  }
}