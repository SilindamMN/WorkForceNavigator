import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TimesheetSummary, Timesheet } from '../../models/timesheet';
import { ProjectsService } from '../../shared/services/projects.service';
import { TimesheetService } from '../../shared/services/timesheet.service';

export interface TimesheetEntry {
  id: number;
  date: string;
  description: string;
  hours: number;
  projectId: number;
  projectName: string;
}

export interface WeekDay {
  dayName: string;
  dayNumber: number;
  monthName: string;
  dateString: string;
}

@Component({
  selector: 'app-timesheet',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './timesheet.component.html'
})
export class TimesheetComponent implements OnInit {

  private timesheetService = inject(TimesheetService);
  private projectsService = inject(ProjectsService);

  currentWeekStart!: Date;
  weekDays: WeekDay[] = [];

  entries: TimesheetEntry[] = [];
  timesheets: TimesheetSummary[] = [];

  isOpen = false;
  selectedEntry: TimesheetEntry | null = null;
  weekOffSet = 0;
  entryDate = '';
  entryDescription = '';
  entryHours = 1;
  selectedProjectId = 0;

  projects: any[] = [];

  username = '';

  ngOnInit(): void {
    this.username = this.getLoggedInUsername();
    this.loadTimesheets();
    this.goToCurrentWeek();
    this.loadProjects();
  }

  getLoggedInUsername(): string {
    const possibleKeys = [
      'username',
      'userName',
      'currentUser',
      'user',
      'authUser',
      'current_user',
      'auth_user'
    ];

    for (const key of possibleKeys) {
      const value = localStorage.getItem(key);

      if (!value) {
        continue;
      }

      try {
        const parsed = JSON.parse(value);

        if (typeof parsed === 'string' && parsed.trim()) {
          return parsed.trim();
        }

        if (parsed?.username) {
          return String(parsed.username);
        }

        if (parsed?.userName) {
          return String(parsed.userName);
        }

        if (parsed?.email) {
          return String(parsed.email);
        }
      } catch {
        if (value.trim()) {
          return value.trim();
        }
      }
    }

    const tokenKeys = [
      'token',
      'accessToken',
      'jwt',
      'authToken'
    ];

    for (const key of tokenKeys) {
      const token = localStorage.getItem(key);

      if (!token) {
        continue;
      }

      const username = this.getUsernameFromToken(token);

      if (username) {
        return username;
      }
    }

    return '';
  }

loadTimesheets(): void {
  this.timesheetService
    .getTimesheetSummary(this.weekOffSet)
    .subscribe(data => {
      this.timesheets = data;
    });
}
  getUsernameFromToken(token: string): string {
    try {
      const parts = token.split('.');

      if (parts.length !== 3) {
        return '';
      }

      const payload = parts[1]
        .replace(/-/g, '+')
        .replace(/_/g, '/');

      const decoded = decodeURIComponent(
        atob(payload)
          .split('')
          .map(char =>
            `%${('00' + char.charCodeAt(0).toString(16)).slice(-2)}`
          )
          .join('')
      );

      const claims = JSON.parse(decoded);

      return String(
        claims.username ??
        claims.userName ??
        claims.unique_name ??
        claims['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'] ??
        ''
      );
    } catch {
      return '';
    }
  }

  goToCurrentWeek(): void {
    this.currentWeekStart = this.getMonday(new Date());
    this.generateWeekDays();
    this.loadWeekEntries();
  }

  previousWeek(): void {
    const date = new Date(this.currentWeekStart);

    date.setDate(date.getDate() - 7);

    this.currentWeekStart = date;

    this.generateWeekDays();
    this.loadWeekEntries();
  }

  nextWeek(): void {
    const date = new Date(this.currentWeekStart);

    date.setDate(date.getDate() + 7);

    this.currentWeekStart = date;

    this.generateWeekDays();
    this.loadWeekEntries();
  }

  getMonday(date: Date): Date {
    const result = new Date(date);

    result.setHours(0, 0, 0, 0);

    const day = result.getDay();
    const difference = day === 0 ? -6 : 1 - day;

    result.setDate(result.getDate() + difference);

    return result;
  }

  generateWeekDays(): void {
    this.weekDays = [];

    const dayNames = [
      'Monday',
      'Tuesday',
      'Wednesday',
      'Thursday',
      'Friday'
    ];

    for (let i = 0; i < 5; i++) {
      const date = new Date(this.currentWeekStart);

      date.setDate(date.getDate() + i);

      this.weekDays.push({
        dayName: dayNames[i],
        dayNumber: date.getDate(),
        monthName: date.toLocaleDateString('en-ZA', {
          month: 'short',
          year: 'numeric'
        }),
        dateString: this.formatDate(date)
      });
    }
  }

  formatDate(date: Date): string {
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');

    return `${year}-${month}-${day}`;
  }

  formatWeekRange(): string {
    if (!this.currentWeekStart) {
      return '';
    }

    const monday = new Date(this.currentWeekStart);
    const friday = new Date(this.currentWeekStart);

    friday.setDate(friday.getDate() + 4);

    const mondayText = monday.toLocaleDateString('en-ZA', {
      day: '2-digit',
      month: 'short',
      year: 'numeric'
    });

    const fridayText = friday.toLocaleDateString('en-ZA', {
      day: '2-digit',
      month: 'short',
      year: 'numeric'
    });

    return `${mondayText} - ${fridayText}`;
  }

  getEntriesForDay(dateString: string): TimesheetEntry[] {
    return this.entries.filter(entry => entry.date === dateString);
  }

  getDayTotal(dateString: string): number {
    return this.getEntriesForDay(dateString)
      .reduce((total, entry) => total + entry.hours, 0);
  }

  getWeekTotal(): number {
    return this.weekDays
      .reduce((total, day) => total + this.getDayTotal(day.dateString), 0);
  }

  getShortDescription(description: string): string {
    const maxLength = 100;

    if (description.length <= maxLength) {
      return description;
    }

    return description.substring(0, maxLength) + '...';
  }

  loadProjects(): void {
    if (!this.username) {
      console.error('No logged-in username was found.');
      this.projects = [];
      return;
    }

    this.projectsService
      .getUserProjectByUserName(this.username)
      .subscribe({
        next: (response: any) => {
          if (Array.isArray(response)) {
            this.projects = response;
          } else if (Array.isArray(response?.data)) {
            this.projects = response.data;
          } else if (Array.isArray(response?.projects)) {
            this.projects = response.projects;
          } else if (Array.isArray(response?.result)) {
            this.projects = response.result;
          } else if (Array.isArray(response?.items)) {
            this.projects = response.items;
          } else if (response?.projectName) {
            this.projects = [response];
          } else {
            this.projects = [];
          }

          console.log('Logged in username:', this.username);
          console.log('Projects:', this.projects);
        },
        error: error => {
          console.error('Error loading projects:', error);
          this.projects = [];
        }
      });
  }

  getProjectId(project: any): number {
    return Number(
      project?.projectId ??
      project?.id ??
      project?.ProjectId ??
      project?.Id ??
      0
    );
  }

  getProjectName(projectId: number): string {
    const project = this.projects.find(
      item => this.getProjectId(item) === Number(projectId)
    );

    return String(
      project?.projectName ??
      project?.ProjectName ??
      ''
    );
  }

  loadWeekEntries(): void {
    this.entries = [];

    for (const day of this.weekDays) {
      this.timesheetService
        .getTimesheetDetails(day.dateString)
        .subscribe({
          next: (data: Timesheet[]) => {
            const dayEntries: TimesheetEntry[] = data.map(
              (timesheet: Timesheet) => ({
                id: timesheet.id,
                date: this.formatApiDate(timesheet.timesheetDate),
                description: timesheet.description,
                hours: Number(timesheet.timeSpent),
                projectId: Number(timesheet.projectId),
                projectName:
                  this.getProjectName(Number(timesheet.projectId)) ||
                  timesheet.projectNames
              })
            );

            this.entries = [
              ...this.entries.filter(
                entry => entry.date !== day.dateString
              ),
              ...dayEntries
            ];
          },
          error: error => {
            console.error(
              `Error loading timesheets for ${day.dateString}:`,
              error
            );
          }
        });
    }
  }

  formatApiDate(value: Date | string): string {
    if (!value) {
      return '';
    }

    const valueString = String(value);

    if (/^\d{4}-\d{2}-\d{2}$/.test(valueString)) {
      return valueString;
    }

    const match = valueString.match(/^(\d{4})-(\d{2})-(\d{2})/);

    if (match) {
      return `${match[1]}-${match[2]}-${match[3]}`;
    }

    const date = new Date(valueString);

    return [
      date.getFullYear(),
      String(date.getMonth() + 1).padStart(2, '0'),
      String(date.getDate()).padStart(2, '0')
    ].join('-');
  }

  createLocalDate(dateString: string): Date {
    const [year, month, day] = dateString.split('-').map(Number);

    return new Date(
      year,
      month - 1,
      day,
      12,
      0,
      0,
      0
    );
  }

  openAddModal(date: string): void {
    this.selectedEntry = null;
    this.entryDate = date;
    this.entryDescription = '';
    this.entryHours = 1;
    this.selectedProjectId = 0;
    this.isOpen = true;
  }

  openEditModal(entry: TimesheetEntry): void {
    this.selectedEntry = entry;
    this.entryDate = entry.date;
    this.entryDescription = entry.description;
    this.entryHours = entry.hours;
    this.selectedProjectId = entry.projectId;
    this.isOpen = true;
  }

  handleAddOrUpdateEntry(): void {
    if (!this.entryDate) {
      alert('Please select a date.');
      return;
    }

    if (!this.entryDescription.trim()) {
      alert('Please enter a description.');
      return;
    }

    if (!this.entryHours || Number(this.entryHours) <= 0) {
      alert('Please enter valid hours.');
      return;
    }

    if (!this.selectedProjectId || Number(this.selectedProjectId) <= 0) {
      alert('Please select a project.');
      return;
    }

    if (!this.username) {
      alert('Unable to determine the logged-in user.');
      return;
    }

    if (this.selectedEntry) {
      this.updateEntry();
    } else {
      this.createEntry();
    }
  }

  createEntry(): void {
    const date = this.createLocalDate(this.entryDate);

    const timesheet: Timesheet = {
      id: 0,
      timesheetDate: date,
      dayName: date.toLocaleDateString('en-ZA', {
        weekday: 'long'
      }),
      username: this.username,
      description: this.entryDescription.trim(),
      timeSpent: Number(this.entryHours),
      projectId: Number(this.selectedProjectId),
      projectNames: this.getProjectName(
        Number(this.selectedProjectId)
      )
    };

    console.log('Creating timesheet:', timesheet);

    this.timesheetService
      .create(timesheet, 'create')
      .subscribe({
        next: () => {
          this.closeModal();
          this.loadWeekEntries();
        },
        error: error => {
          console.error('Create timesheet failed:', error);
          alert('Failed to create timesheet entry.');
        }
      });
  }

  updateEntry(): void {
    if (!this.selectedEntry) {
      return;
    }

    const date = this.createLocalDate(this.entryDate);

    const timesheet: Timesheet = {
      id: this.selectedEntry.id,
      timesheetDate: date,
      dayName: date.toLocaleDateString('en-ZA', {
        weekday: 'long'
      }),
      username: this.username,
      description: this.entryDescription.trim(),
      timeSpent: Number(this.entryHours),
      projectId: Number(this.selectedProjectId),
      projectNames: this.getProjectName(
        Number(this.selectedProjectId)
      )
    };

    console.log('Updating timesheet:', timesheet);

    this.timesheetService
      .update(timesheet)
      .subscribe({
        next: () => {
          this.closeModal();
          this.loadWeekEntries();
        },
        error: error => {
          console.error('Update timesheet failed:', error);
          alert('Failed to update timesheet entry.');
        }
      });
  }

  deleteEntry(): void {
    if (!this.selectedEntry) {
      return;
    }

    const id = this.selectedEntry.id;

    this.timesheetService
      .delete(id)
      .subscribe({
        next: () => {
          this.closeModal();
          this.loadWeekEntries();
        },
        error: error => {
          console.error('Delete timesheet failed:', error);
          alert('Failed to delete timesheet entry.');
        }
      });
  }

  closeModal(): void {
    this.isOpen = false;
    this.selectedEntry = null;
    this.entryDate = '';
    this.entryDescription = '';
    this.entryHours = 1;
    this.selectedProjectId = 0;
  }
}