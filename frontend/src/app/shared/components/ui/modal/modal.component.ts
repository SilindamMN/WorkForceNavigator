import { CommonModule } from '@angular/common';
import {
  Component,
  ElementRef,
  EventEmitter,
  HostListener,
  Input,
  OnChanges,
  OnDestroy,
  OnInit,
  Output
} from '@angular/core';

@Component({
  selector: 'app-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './modal.component.html'
})
export class ModalComponent implements OnInit, OnChanges, OnDestroy {

  @Input() isOpen = false;
  @Input() className = '';
  @Input() showCloseButton = true;
  @Input() isFullscreen = false;

  @Output() close = new EventEmitter<void>();

  constructor(private el: ElementRef) {}

  ngOnInit(): void {
    this.updateBodyScroll();
  }

  ngOnChanges(): void {
    this.updateBodyScroll();
  }

  ngOnDestroy(): void {
    document.body.style.overflow = 'unset';
  }

  private updateBodyScroll(): void {
    document.body.style.overflow = this.isOpen
      ? 'hidden'
      : 'unset';
  }

  onBackdropClick(event: MouseEvent): void {
    if (event.target === event.currentTarget && !this.isFullscreen) {
      this.close.emit();
    }
  }

  onContentClick(event: MouseEvent): void {
    event.stopPropagation();
  }

  @HostListener('document:keydown.escape')
  onEscape(): void {
    if (this.isOpen) {
      this.close.emit();
    }
  }
}