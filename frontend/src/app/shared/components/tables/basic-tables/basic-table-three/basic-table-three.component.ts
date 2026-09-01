import { CommonModule } from '@angular/common';
import {
  Component,
  Input,
  Output,
  EventEmitter,
  OnInit
} from '@angular/core';

import { ButtonComponent } from '../../../ui/button/button.component';
import { BadgeComponent } from '../../../ui/badge/badge.component';
import { LabelComponent } from '../../../form/label/label.component';
import { InputFieldComponent } from '../../../form/input/input-field.component';
import { SelectComponent } from '../../../form/select/select.component';
import { ModalComponent } from '../../../ui/modal/modal.component';

export interface TableColumnOption {
  value: string;
  label: string;
}

export interface TableColumn {

  key: string;

  label: string;

  type?: 'text' | 'select';

  options?: TableColumnOption[];

  /**
   * Used when a select column displays one property
   * but saves another property.
   *
   * Example:
   *
   * key: 'jobTitleName'
   * valueKey: 'jobTitleId'
   */
  valueKey?: string;
}

@Component({
  selector: 'app-basic-table-three',
  standalone: true,
  imports: [
    CommonModule,
    ButtonComponent,
    BadgeComponent,
    ModalComponent,
    LabelComponent,
    InputFieldComponent,
    SelectComponent
  ],
  templateUrl: './basic-table-three.component.html',
  styles: ``
})
export class BasicTableThreeComponent implements OnInit {

  @Input() name: string = '';

  @Input() title: string = '';

  @Input() data: any[] = [];

  @Input() columns: TableColumn[] = [];

  @Input() itemsPerPage: number = 5;

  @Input() canAdd: boolean = true;

  @Input() canEdit: boolean = true;

  @Input() canDelete: boolean = true;

  @Output() add = new EventEmitter<void>();

  @Output() edit = new EventEmitter<any>();

  @Output() delete = new EventEmitter<any>();

  @Output() save =
    new EventEmitter<{
      mode: 'add' | 'edit';
      data: any;
    }>();

  currentPage = 1;

  searchTerm = '';

  modalOpen = false;

  modalMode: 'add' | 'edit' = 'add';

  formData: any = {};

  ngOnInit(): void {
    this.modalOpen = false;
  }

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

    const input =
      event.target as HTMLInputElement;

    this.searchTerm = input.value;

    this.currentPage = 1;
  }

  goToPage(page: number): void {

    if (
      page >= 1 &&
      page <= this.totalPages
    ) {
      this.currentPage = page;
    }
  }

  handleAdd(): void {

    this.modalMode = 'add';

    this.formData = {};

    this.modalOpen = true;

    this.add.emit();
  }

  handleEdit(item: any): void {

    this.modalMode = 'edit';

    /**
     * Copy the complete user object.
     *
     * Example:
     *
     * {
     *   jobTitleId: 154,
     *   jobTitleName: "Procurement Assistant"
     * }
     */
    this.formData = {
      ...item
    };

    /**
     * For select fields we need to make sure
     * the value stored in formData matches
     * the value used by the dropdown.
     *
     * jobTitleName is displayed.
     * jobTitleId is selected.
     */
    this.columns.forEach(column => {

      if (
        column.type === 'select' &&
        column.valueKey
      ) {

        const value =
          this.formData[column.valueKey];

        if (value !== null && value !== undefined) {

          this.formData[column.valueKey] =
            value.toString();
        }
      }
    });

    console.log(
      'FORM DATA AFTER EDIT:',
      this.formData
    );

    this.modalOpen = true;

    this.edit.emit(item);
  }

  handleDelete(item: any): void {
    this.delete.emit(item);
  }

  closeModal(): void {
    this.modalOpen = false;
  }

  handleSave(): void {

    /**
     * Convert select values back to numbers
     * before sending them to the API.
     */
    this.columns.forEach(column => {

      if (
        column.type === 'select' &&
        column.valueKey
      ) {

        const value =
          this.formData[column.valueKey];

        if (
          value !== null &&
          value !== undefined &&
          value !== ''
        ) {

          this.formData[column.valueKey] =
            Number(value);
        }
      }
    });

    console.log(
      'FINAL SAVE DATA:',
      this.formData
    );

    this.save.emit({
      mode: this.modalMode,
      data: this.formData
    });

    this.modalOpen = false;
  }

  onFieldChange(
    key: string,
    event: Event
  ): void {

    const input =
      event.target as HTMLInputElement;

    this.formData[key] = input.value;
  }

  onSelectFieldChange(
    column: TableColumn,
    value: string
  ): void {

    if (column.valueKey) {

      this.formData[column.valueKey] = value;

    } else {

      this.formData[column.key] = value;
    }
  }

  isSelectColumn(
    column: TableColumn
  ): boolean {

    return column.type === 'select';
  }

  getSelectValue(
    column: TableColumn
  ): string {

    const key =
      column.valueKey ?? column.key;

    const value =
      this.formData[key];

    return value !== null &&
      value !== undefined
      ? value.toString()
      : '';
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

  isStatusColumn(
    column: TableColumn
  ): boolean {

    return column.key.toLowerCase() === 'status';
  }
}