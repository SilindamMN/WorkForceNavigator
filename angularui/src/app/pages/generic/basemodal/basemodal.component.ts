// base-modal.component.ts
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

export interface ModalField {
  name: string;
  label: string;
  type: 'text' | 'number' | 'date' | 'select' | 'email' | 'password' | 'textarea' | 'checkbox';
  required?: boolean;
  options?: { value: any; label: string }[];
  defaultValue?: any;
}

@Component({
  selector: 'app-base-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: ''
})
export class BaseModalComponent {
  @Input() title: string = '';
  @Input() fields: ModalField[] = [];
  @Output() onSubmit = new EventEmitter<any>();
  @Output() onClose = new EventEmitter<void>();

  showModal: boolean = false;
  model: any = {};

  open() {
    this.initializeModel();
    this.showModal = true;
  }

  close() {
    this.showModal = false;
    this.onClose.emit();
  }

  protected initializeModel() {
    this.model = {};
    this.fields.forEach(field => {
      this.model[field.name] = field.defaultValue || '';
    });
  }

  submit() {
    this.onSubmit.emit(this.model);
    this.close();
  }
}