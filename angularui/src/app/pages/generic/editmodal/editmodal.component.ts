// edit-modal.component.ts
import { Component, Input } from '@angular/core';
import { BaseModalComponent } from '../basemodal/basemodal.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-edit-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './editmodal.component.html',
  styleUrls: ['./editmodal.component.css']
})
export class EditModalComponent extends BaseModalComponent {
  @Input() set entity(value: any) {
    if (value) {
      this.model = { ...value };
    }
  }

  constructor() {
    super();
    this.title = 'Edit Item';
  }

  protected override initializeModel() {
    // Don't reinitialize for edit modals
    if (!this.model || Object.keys(this.model).length === 0) {
      super.initializeModel();
    }
  }
}