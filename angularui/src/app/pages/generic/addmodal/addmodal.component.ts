// add-modal.component.ts
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BaseModalComponent, ModalField } from '../basemodal/basemodal.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-add-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './addmodal.component.html',
  styleUrls: ['./addmodal.component.css']
})
export class AddModalComponent extends BaseModalComponent {

  @Input() override model: any = {};
  @Input() override fields: ModalField[] = [];
  @Input() override title: string = 'Add New Item';
  @Input() submitText: string = 'Save';

  @Output() submitted = new EventEmitter<any>();  

  constructor() {
    super();
  }

  override submit(): void {
    this.submitted.emit(this.model);  
    this.close();          
  }
}