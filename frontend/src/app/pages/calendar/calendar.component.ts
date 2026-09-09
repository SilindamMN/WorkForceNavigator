import { CommonModule } from '@angular/common';
import {
  Component,
  inject,
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
import { LeaveAllocationsService } from '../../shared/services/leaveallocations.service'; // adjust path if different

interface CalendarEvent extends EventInput {
  extendedProps: {
    calendar: string;
    leaveRequestId?: number;
    leaveType?: string;
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

  leaveRequestsService = inject(LeaverequestService);
  leaveAllocationsService = inject(LeaveAllocationsService);

  leaveRequests: LeaveRequest[] = [];

  leaveTypes: LeaveAllocationDto[] = [];

  events: CalendarEvent[] = [];

  selectedEvent: CalendarEvent | null = null;

  isOpen = false;

  /*
   * Keep these as strings because:
   * - FullCalendar returns date strings
   * - <input type="date"> works with strings
   * - DTOs normally expect date strings
   */
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

          this.events =
            this.mapLeaveRequestsToEvents(data);

          this.calendarOptions.events =
            this.events;

        },

        error: (error) => {

          console.error(
            'Failed to load leave requests',
            error
          );

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
          console.error(
            'Failed to load leave types',
            error
          );
        }
      });
  }

  // ============================================================
  // MAP LEAVE REQUESTS TO FULLCALENDAR EVENTS
  // ============================================================

  mapLeaveRequestsToEvents(
    requests: LeaveRequest[]
  ): CalendarEvent[] {

    return requests.map((request: any) => {

      return {

        id: request.id?.toString(),

        title:
          `${request.leaveName} - ${request.firstName} ${request.lastName}`,

        start: request.startDate,

        /*
         * FullCalendar uses an exclusive end date.
         * Therefore we add one day to the actual leave end date.
         */
        end:
          this.getCalendarEndDate(
            request.endDate
          ),

        allDay: true,

        extendedProps: {

          calendar:
            this.getStatusColor(
              request.status
            ),

          leaveRequestId:
            request.id,

          leaveType:
            request.leaveName,

          status:
            request.status,

          firstName:
            request.firstName,

          lastName:
            request.lastName,

          numberOfDays:
            request.numberOfDays
        }
      };
    });
  }

  // ============================================================
  // STATUS COLOUR
  // ============================================================

  getStatusColor(
    status: string
  ): string {

    switch (
      status?.toLowerCase()
    ) {

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
  // FULLCALENDAR END DATE
  // ============================================================

  getCalendarEndDate(
    endDate: string
  ): string {

    if (!endDate) {
      return endDate;
    }

    const date =
      new Date(endDate);

    date.setDate(
      date.getDate() + 1
    );

    return date
      .toISOString()
      .split('T')[0];
  }

  // ============================================================
  // INITIALIZE CALENDAR
  // ============================================================

  initializeCalendar(): void {

    this.calendarOptions = {

      plugins: [
        dayGridPlugin,
        timeGridPlugin,
        interactionPlugin
      ],

      initialView:
        'dayGridMonth',

      headerToolbar: {

        left:
          'prev,next today',

        center:
          'title',

        right:
          'dayGridMonth,timeGridWeek,timeGridDay'
      },

      selectable: true,

      editable: false,

      events:
        this.events,

      select: (
        info: DateSelectArg
      ) => {

        this.handleDateSelect(
          info
        );

      },

      eventClick: (
        info: EventClickArg
      ) => {

        this.handleEventClick(
          info
        );

      },

      eventContent: (
        arg
      ) => {

        return this.renderEventContent(
          arg
        );

      }
    };
  }

  // ============================================================
  // DATE SELECT
  // ============================================================

  handleDateSelect(
    selectInfo: DateSelectArg
  ): void {

    this.resetModalFields();

    this.eventStartDate =
      selectInfo.startStr;

    /*
     * FullCalendar's endStr is exclusive.
     *
     * Example:
     * Selecting 10 September only
     * gives:
     *
     * startStr = 2026-09-10
     * endStr   = 2026-09-11
     *
     * Therefore subtract one day.
     */
    if (selectInfo.endStr) {

      this.eventEndDate =
        this.removeOneDay(
          selectInfo.endStr
        );

    } else {

      this.eventEndDate =
        selectInfo.startStr;

    }

    this.openModal();
  }

  // ============================================================
  // EVENT CLICK
  // ============================================================

  handleEventClick(
    clickInfo: EventClickArg
  ): void {

    const event =
      clickInfo.event;

    this.selectedEvent = {

      id:
        event.id,

      title:
        event.title,

      start:
        event.startStr,

      end:
        event.endStr,

      extendedProps: {

        calendar:
          event.extendedProps[
            'calendar'
          ],

        leaveRequestId:
          event.extendedProps[
            'leaveRequestId'
          ],

        leaveType:
          event.extendedProps[
            'leaveType'
          ],

        status:
          event.extendedProps[
            'status'
          ],

        firstName:
          event.extendedProps[
            'firstName'
          ],

        lastName:
          event.extendedProps[
            'lastName'
          ],

        numberOfDays:
          event.extendedProps[
            'numberOfDays'
          ]
      }
    };

    this.eventTitle =
      event.title;

    this.eventStartDate =
      event.startStr;

    /*
     * FullCalendar end date is exclusive,
     * so remove one day when displaying it.
     */
    this.eventEndDate =
      event.endStr
        ? this.removeOneDay(
            event.endStr
          )
        : event.startStr;

    this.eventLevel =
      event.extendedProps[
        'calendar'
      ];

    this.openModal();
  }

  // ============================================================
  // REMOVE ONE DAY
  // ============================================================

  removeOneDay(
    dateString: string
  ): string {

    const date =
      new Date(dateString);

    date.setDate(
      date.getDate() - 1
    );

    return date
      .toISOString()
      .split('T')[0];
  }

  // ============================================================
  // ADD OR UPDATE
  // ============================================================

  handleAddOrUpdateEvent(): void {

    if (!this.eventStartDate) {
      return;
    }

    if (!this.eventEndDate) {
      return;
    }

    if (
      this.selectedEvent
    ) {

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
          console.error(
            'Failed to create leave request',
            error
          );
        }
      });
  }

  toDate(eventStartDate: string): Date {
    throw new Error('Method not implemented.');
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
      .updateLeaveRequest(
        leaveRequestId,
        request
      )
      .subscribe({
        next: () => {
          this.loadLeaveRequests();
          this.closeModal();
        },
        error: (error) => {
          console.error(
            'Failed to update leave request',
            error
          );
        }
      });
  }

  // ============================================================
  // DELETE
  // ============================================================

  deleteEvent(): void {

    if (!this.selectedEvent) {
      return;
    }

    const leaveRequestId =
      Number(
        this.selectedEvent
          .extendedProps
          .leaveRequestId
        ??
        this.selectedEvent.id
      );

    console.log(
      'Delete leave request:',
      leaveRequestId
    );

    /*
     * Add your delete service method here.
     *
     * Example:
     *
     * this.leaveRequestsService
     *   .delete(leaveRequestId)
     *   .subscribe({
     *     next: () => {
     *       this.loadLeaveRequests();
     *       this.closeModal();
     *     }
     *   });
     */
  }

  // ============================================================
  // GET LEAVE TYPE ID
  // ============================================================

  getLeaveTypeId(): number {

    const request: any =
      this.leaveRequests.find(
        (x: any) =>
          x.id ===
          this.selectedEvent
            ?.extendedProps
            .leaveRequestId
      );

    return request?.leaveTypeId ?? 0;
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

  renderEventContent(
    eventInfo: any
  ): any {

    const colorClass =
      `fc-bg-${eventInfo.event.extendedProps.calendar?.toLowerCase()}`;

    return {

      html: `
        <div
          class="event-fc-color flex fc-event-main
                 ${colorClass} p-1 rounded-sm"
        >

          <div
            class="fc-daygrid-event-dot"
          ></div>

          <div
            class="fc-event-title"
          >
            ${eventInfo.event.title}
          </div>

        </div>
      `
    };
  }
}