// add-modal.component.ts
import { Component } from '@angular/core';
import { BaseModalComponent } from '../basemodal/basemodal.component';
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
  constructor() {
    super();
    this.title = 'Add New Item';
  }
}