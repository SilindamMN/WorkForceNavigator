import { CommonModule } from '@angular/common';
import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';

export interface TableColumn {
  field: string;
  label: string;
}

@Component({
  selector: 'app-generic-table',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './datatable.component.html',
  styleUrl: './datatable.component.css',
})
export class DatatableComponent implements OnChanges {
  @Input() data: any[] = [];
  @Input() columns: TableColumn[] = [];

  @Input() showEdit: boolean = false;
  @Input() showView: boolean = false;
  @Input() showDelete: boolean = false;


  @Output() onEdit = new EventEmitter<any>();
  @Output() onView = new EventEmitter<any>();
  @Output() onDelete = new EventEmitter<any>();


  ngOnChanges(changes: SimpleChanges): void {
    if (changes['data']) {
      this.rebuildDataTable();
    }
  }

  rebuildDataTable() {
    setTimeout(() => {
      const table = $('#genericTable');
      if ($.fn.DataTable.isDataTable(table)) {
        table.DataTable().destroy();
      }
      table.DataTable();
    }, 100);
  }

  handleEdit(row: any) {
    this.onEdit.emit(row);
  }

  handleView(row: any) {
    this.onView.emit(row);
  }

  handleDelete(id:number){
    this.onDelete.emit(id);
  }
}
