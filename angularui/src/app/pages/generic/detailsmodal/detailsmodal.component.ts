import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-details-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './detailsmodal.component.html',
  styleUrls: ['./detailsmodal.component.css']
})
export class DetaisModalComponent {
  @Input() title: string = '';
  @Input() fields: any[] = [];
  @Input() entity: any;

  showModal: boolean = false;

  open() {
    this.showModal = true;
  }

  close() {
    this.showModal = false;
  }
}
