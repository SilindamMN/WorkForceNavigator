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

  @Output() save = new EventEmitter<{
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


  // =========================================
  // FILTER
  // =========================================

  get filteredData(): any[] {

    if (!this.searchTerm.trim()) {
      return this.data;
    }

    const search =
      this.searchTerm.toLowerCase();

    return this.data.filter(item =>
      this.columns.some(column =>
        String(item[column.key] ?? '')
          .toLowerCase()
          .includes(search)
      )
    );
  }


  // =========================================
  // TOTAL PAGES
  // =========================================

  get totalPages(): number {

    return Math.max(
      1,
      Math.ceil(
        this.filteredData.length /
        this.itemsPerPage
      )
    );
  }


  // =========================================
  // CURRENT ITEMS
  // =========================================

  get currentItems(): any[] {

    const start =
      (this.currentPage - 1) *
      this.itemsPerPage;

    return this.filteredData.slice(
      start,
      start + this.itemsPerPage
    );
  }


  // =========================================
  // SEARCH
  // =========================================

  onSearch(event: Event): void {

    const input =
      event.target as HTMLInputElement;

    this.searchTerm = input.value;

    this.currentPage = 1;
  }


  // =========================================
  // PAGINATION
  // =========================================

  goToPage(page: number): void {

    if (
      page >= 1 &&
      page <= this.totalPages
    ) {
      this.currentPage = page;
    }
  }


  // =========================================
  // ADD
  // =========================================

  handleAdd(): void {

    console.log('ADD BUTTON CLICKED');

    this.modalMode = 'add';

    this.formData = {};

    this.modalOpen = true;

    this.add.emit();
  }


  // =========================================
  // EDIT
  // =========================================

  handleEdit(item: any): void {

    console.log(
      'EDIT BUTTON CLICKED:',
      item
    );

    this.modalMode = 'edit';

    /*
     * Copy the complete object.
     *
     * This is important because it keeps
     * the user's ID for update.
     */
    this.formData = {
      ...item
    };


    /*
     * Convert select values to strings.
     *
     * Example:
     *
     * jobTitleId: 154
     *
     * becomes:
     *
     * jobTitleId: "154"
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
          value !== undefined
        ) {

          this.formData[
            column.valueKey
          ] = value.toString();
        }
      }
    });


    console.log(
      'FORM DATA FOR EDIT:',
      this.formData
    );


    this.modalOpen = true;

    this.edit.emit(item);
  }


  // =========================================
  // DELETE
  // =========================================

  handleDelete(item: any): void {

    console.log(
      'DELETE BUTTON CLICKED:',
      item
    );

    this.delete.emit(item);
  }


  // =========================================
  // CLOSE MODAL
  // =========================================

  closeModal(): void {

    console.log('MODAL CLOSED');

    this.modalOpen = false;
  }


  // =========================================
  // SAVE
  // =========================================

  handleSave(): void {

    console.log(
      '================================'
    );

    console.log(
      'SAVE BUTTON CLICKED'
    );

    console.log(
      'MODE:',
      this.modalMode
    );

    console.log(
      'FORM DATA:',
      this.formData
    );

    console.log(
      '================================'
    );


    /*
     * Make sure we actually have data.
     */
    if (!this.formData) {

      console.error(
        'FORM DATA IS EMPTY'
      );

      return;
    }


    /*
     * Send data to UsersComponent.
     */
    this.save.emit({
      mode: this.modalMode,
      data: {
        ...this.formData
      }
    });


    /*
     * Close modal.
     */
    this.modalOpen = false;
  }


  // =========================================
  // INPUT CHANGE
  // =========================================

  onFieldChange(
    key: string,
    event: Event
  ): void {

    const input =
      event.target as HTMLInputElement;

    this.formData[key] =
      input.value;

    console.log(
      'FIELD CHANGED:',
      key,
      input.value
    );
  }


  // =========================================
  // SELECT CHANGE
  // =========================================

  onSelectFieldChange(
    column: TableColumn,
    value: string
  ): void {

    console.log(
      'SELECT CHANGED:',
      column.key,
      value
    );


    if (column.valueKey) {

      /*
       * Example:
       *
       * jobTitleName -> displayed
       * jobTitleId   -> saved
       */
      this.formData[
        column.valueKey
      ] = value;

    } else {

      /*
       * Example:
       *
       * gender -> saved
       */
      this.formData[
        column.key
      ] = value;
    }


    console.log(
      'FORM DATA AFTER SELECT:',
      this.formData
    );
  }


  // =========================================
  // SELECT CHECK
  // =========================================

  isSelectColumn(
    column: TableColumn
  ): boolean {

    return column.type === 'select';
  }


  // =========================================
  // SELECT VALUE
  // =========================================

  getSelectValue(
    column: TableColumn
  ): string {

    const key =
      column.valueKey ??
      column.key;

    const value =
      this.formData[key];

    if (
      value === null ||
      value === undefined
    ) {
      return '';
    }

    return value.toString();
  }


  // =========================================
  // BADGE COLOR
  // =========================================

  getBadgeColor(
    status: string
  ): 'success' | 'warning' | 'error' {

    switch (
      status?.toLowerCase()
    ) {

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


  // =========================================
  // STATUS COLUMN
  // =========================================

  isStatusColumn(
    column: TableColumn
  ): boolean {

    return (
      column.key.toLowerCase() ===
      'status'
    );
  }

}