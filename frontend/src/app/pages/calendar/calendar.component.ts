import { CommonModule } from '@angular/common';
import {
  Component,
  inject,
  NgZone,
  OnInit,
  ViewChild
} from '@angular/core';
import { FormsModule } from '@angular/forms';

import {
  FullCalendarComponent,
  FullCalendarModule
} from '@fullcalendar/angular';

import {
  EventInput,
  CalendarOptions,
  DateSelectArg,
  EventClickArg
} from '@fullcalendar/core';

import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import interactionPlugin from '@fullcalendar/interaction';

import { ModalComponent } from '../../shared/components/ui/modal/modal.component';

import {
  LeaveRequest,
  CreateLeaveRequestDto,
  UpdateLeaveRequestDto
} from '../../models/leaverequest';

import { LeaveAllocationDto } from '../../models/leaveallocation';

import { LeaverequestService } from '../../shared/services/leaverequest.service';
import { LeaveAllocationsService } from '../../shared/services/leaveallocations.service';

interface CalendarEvent extends EventInput {
  extendedProps: {
    calendar: string;
    leaveRequestId?: number;
    leaveType?: string;
    leaveTypeId?: number;
    status?: string;
    firstName?: string;
    lastName?: string;
    numberOfDays?: number;
  };
}

@Component({
  selector: 'app-calendar',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    FullCalendarModule,
    ModalComponent
  ],
  templateUrl: './calendar.component.html',
  styles: ``
})
export class CalendarComponent implements OnInit {

  @ViewChild('calendar')
  calendarComponent!: FullCalendarComponent;

  private zone = inject(NgZone);
  leaveRequestsService = inject(LeaverequestService);
  leaveAllocationsService = inject(LeaveAllocationsService);

  leaveRequests: LeaveRequest[] = [];
  leaveTypes: LeaveAllocationDto[] = [];
  events: CalendarEvent[] = [];
  selectedEvent: CalendarEvent | null = null;
  isOpen = false;

  // Form fields (Formatted strictly as YYYY-MM-DD for native <input type="date">)
  eventTitle = '';
  eventStartDate = '';
  eventEndDate = '';
  eventLevel = '';
  selectedLeaveTypeId = 0;

  calendarsEvents: Record<string, string> = {
    Danger: 'danger',
    Success: 'success',
    Primary: 'primary',
    Warning: 'warning'
  };

  calendarOptions!: CalendarOptions;

  ngOnInit(): void {
    this.initializeCalendar();
    this.loadLeaveRequests();
    this.loadLeaveTypes();
  }

  // ============================================================
  // LOAD LEAVE REQUESTS
  // ============================================================

  loadLeaveRequests(): void {
    this.leaveRequestsService
      .getAll('/upcoming')
      .subscribe({
        next: (data: LeaveRequest[]) => {
          this.leaveRequests = data;
          this.events = this.mapLeaveRequestsToEvents(data);
          this.calendarOptions.events = [...this.events];
        },
        error: (error) => {
          console.error('Failed to load leave requests', error);
        }
      });
  }

  // ============================================================
  // LOAD LEAVE TYPES
  // ============================================================

  loadLeaveTypes(): void {
    const userInfoJson = localStorage.getItem('userInfo');

    if (!userInfoJson) {
      return;
    }

    const userInfo = JSON.parse(userInfoJson);

    this.leaveAllocationsService
      .getLeaveAllocationsByUsername(userInfo.username)
      .subscribe({
        next: (data: LeaveAllocationDto[]) => {
          this.leaveTypes = data;
        },
        error: (error) => {
          console.error('Failed to load leave types', error);
        }
      });
  }

  // ============================================================
  // MAP LEAVE REQUESTS TO FULLCALENDAR EVENTS
  // ============================================================

  mapLeaveRequestsToEvents(requests: LeaveRequest[]): CalendarEvent[] {
    return requests.map((request: any) => {
      return {
        id: request.id?.toString(),
        title: `${request.leaveName} - ${request.firstName} ${request.lastName}`,
        start: request.startDate,
        end: this.getCalendarEndDate(request.endDate),
        allDay: true,
        extendedProps: {
          calendar: this.getStatusColor(request.status),
          leaveRequestId: request.id,
          leaveType: request.leaveName,
          leaveTypeId: request.leaveTypeId,
          status: request.status,
          firstName: request.firstName,
          lastName: request.lastName,
          numberOfDays: request.numberOfDays
        }
      };
    });
  }

  // ============================================================
  // STATUS COLOUR
  // ============================================================

  getStatusColor(status: string): string {
    switch (status?.toLowerCase()) {
      case 'approved':
        return 'Success';
      case 'declined':
        return 'Danger';
      case 'pending':
        return 'Warning';
      default:
        return 'Primary';
    }
  }

  // ============================================================
  // FORMAT DATE TO YYYY-MM-DD
  // ============================================================

  formatToInputDate(dateInput: string | Date): string {
    if (!dateInput) return '';
    const date = new Date(dateInput);
    if (isNaN(date.getTime())) return '';
    
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  }

  // ============================================================
  // FULLCALENDAR END DATE (Adds 1 day because FullCalendar uses exclusive end date)
  // ============================================================

  getCalendarEndDate(endDate: string | Date): string {
    if (!endDate) {
      return '';
    }

    const date = new Date(endDate);
    date.setDate(date.getDate() + 1);
    return this.formatToInputDate(date);
  }

  // ============================================================
  // REMOVE ONE DAY (For displaying dates from FullCalendar)
  // ============================================================

  removeOneDay(dateString: string): string {
    if (!dateString) return '';
    const date = new Date(dateString);
    date.setDate(date.getDate() - 1);
    return this.formatToInputDate(date);
  }

  // ============================================================
  // TO DATE (Convert string YYYY-MM-DD to Date object)
  // ============================================================

  toDate(dateString: string): Date {
    const parts = dateString.split('-');
    return new Date(
      parseInt(parts[0], 10),
      parseInt(parts[1], 10) - 1,
      parseInt(parts[2], 10)
    );
  }

  // ============================================================
  // INITIALIZE CALENDAR
  // ============================================================

  initializeCalendar(): void {
    this.calendarOptions = {
      plugins: [dayGridPlugin, timeGridPlugin, interactionPlugin],
      initialView: 'dayGridMonth',
      headerToolbar: {
        left: 'prev,next today',
        center: 'title',
        right: 'dayGridMonth,timeGridWeek,timeGridDay'
      },
      selectable: true,
      editable: false,
      events: this.events,
      select: (info: DateSelectArg) => {
        // Run inside Angular zone to update bindings immediately
        this.zone.run(() => {
          this.handleDateSelect(info);
        });
      },
      eventClick: (info: EventClickArg) => {
        // Run inside Angular zone to update bindings immediately
        this.zone.run(() => {
          this.handleEventClick(info);
        });
      },
      eventContent: (arg) => {
        return this.renderEventContent(arg);
      }
    };
  }

  // ============================================================
  // DATE SELECT
  // ============================================================

  handleDateSelect(selectInfo: DateSelectArg): void {
    this.resetModalFields();
    this.eventStartDate = this.formatToInputDate(selectInfo.startStr);

    if (selectInfo.endStr) {
      this.eventEndDate = this.removeOneDay(selectInfo.endStr);
    } else {
      this.eventEndDate = this.eventStartDate;
    }

    this.openModal();
  }

  // ============================================================
  // EVENT CLICK
  // ============================================================

  handleEventClick(clickInfo: EventClickArg): void {
    const event = clickInfo.event;

    this.selectedEvent = {
      id: event.id,
      title: event.title,
      start: event.startStr,
      end: event.endStr,
      extendedProps: {
        calendar: event.extendedProps['calendar'],
        leaveRequestId: event.extendedProps['leaveRequestId'],
        leaveType: event.extendedProps['leaveType'],
        leaveTypeId: event.extendedProps['leaveTypeId'],
        status: event.extendedProps['status'],
        firstName: event.extendedProps['firstName'],
        lastName: event.extendedProps['lastName'],
        numberOfDays: event.extendedProps['numberOfDays']
      }
    };

    // Populate form fields
    this.selectedLeaveTypeId = event.extendedProps['leaveTypeId'] || 0;
    this.eventTitle = event.title;
    this.eventStartDate = this.formatToInputDate(event.startStr);
    this.eventEndDate = event.endStr ? this.removeOneDay(event.endStr) : this.eventStartDate;
    this.eventLevel = event.extendedProps['calendar'];

    this.openModal();
  }

  // ============================================================
  // OPEN NATIVE PICKER ON CLICK
  // ============================================================

  openDatePicker(event: MouseEvent): void {
    const inputEl = event.target as HTMLInputElement;
    if (inputEl && typeof inputEl.showPicker === 'function') {
      inputEl.showPicker();
    }
  }

  // ============================================================
  // ADD OR UPDATE
  // ============================================================

  handleAddOrUpdateEvent(): void {
    if (!this.eventStartDate || !this.eventEndDate) {
      console.error('Please select start and end dates');
      return;
    }

    if (this.selectedLeaveTypeId === 0) {
      console.error('Please select a leave type');
      return;
    }

    if (this.selectedEvent) {
      this.updateEvent();
    } else {
      this.createEvent();
    }
  }

  // ============================================================
  // CREATE LEAVE REQUEST
  // ============================================================

  createEvent(): void {
    const request: CreateLeaveRequestDto = {
      leaveTypeId: this.selectedLeaveTypeId,
      startDate: this.toDate(this.eventStartDate),
      endDate: this.toDate(this.eventEndDate)
    };

    console.log('Creating leave request:', request);

    this.leaveRequestsService
      .createLeaveRequest(request)
      .subscribe({
        next: () => {
          this.loadLeaveRequests();
          this.closeModal();
        },
        error: (error) => {
          console.error('Failed to create leave request', error);
        }
      });
  }

  // ============================================================
  // UPDATE LEAVE REQUEST
  // ============================================================

  updateEvent(): void {
    if (!this.selectedEvent) {
      return;
    }

    const leaveRequestId = Number(
      this.selectedEvent.extendedProps.leaveRequestId ??
      this.selectedEvent.id
    );

    const request: UpdateLeaveRequestDto = {
      startDate: this.toDate(this.eventStartDate),
      endDate: this.toDate(this.eventEndDate),
      comment: ''
    };

    console.log('Updating leave request:', request);

    this.leaveRequestsService
      .updateLeaveRequest(leaveRequestId, request)
      .subscribe({
        next: () => {
          this.loadLeaveRequests();
          this.closeModal();
        },
        error: (error) => {
          console.error('Failed to update leave request', error);
        }
      });
  }

  // ============================================================
  // DELETE LEAVE REQUEST
  // ============================================================

  deleteEvent(): void {
    if (!this.selectedEvent) {
      return;
    }

    const leaveRequestId = Number(
      this.selectedEvent.extendedProps.leaveRequestId ??
      this.selectedEvent.id
    );

    this.leaveRequestsService
      .delete(leaveRequestId)
      .subscribe({
        next: () => {
          this.loadLeaveRequests();
          this.closeModal();
        },
        error: (error) => {
          console.error('Failed to delete leave request', error);
        }
      });
  }

  // ============================================================
  // RESET MODAL
  // ============================================================

  resetModalFields(): void {
    this.eventTitle = '';
    this.eventStartDate = '';
    this.eventEndDate = '';
    this.eventLevel = '';
    this.selectedLeaveTypeId = 0;
    this.selectedEvent = null;
  }

  // ============================================================
  // OPEN MODAL
  // ============================================================

  openModal(): void {
    this.isOpen = true;
  }

  // ============================================================
  // CLOSE MODAL
  // ============================================================

  closeModal(): void {
    this.isOpen = false;
    this.resetModalFields();
  }

  // ============================================================
  // RENDER EVENT
  // ============================================================

  renderEventContent(eventInfo: any): any {
    const colorClass = `fc-bg-${eventInfo.event.extendedProps.calendar?.toLowerCase()}`;

    return {
      html: `
        <div class="event-fc-color flex fc-event-main ${colorClass} p-1 rounded-sm">
          <div class="fc-daygrid-event-dot"></div>
          <div class="fc-event-title">${eventInfo.event.title}</div>
        </div>
      `
    };
  }
}