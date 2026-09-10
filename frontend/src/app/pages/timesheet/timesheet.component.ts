import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { ModalComponent } from '../../shared/components/ui/modal/modal.component';


// ============================================================
// TIMESHEET ENTRY
// ============================================================

export interface TimesheetEntry {
  id: number;
  date: string;
  description: string;
  hours: number;
}


// ============================================================
// WEEK DAY
// ============================================================

export interface WeekDay {
  dayName: string;
  dayNumber: number;
  monthName: string;
  dateString: string;
}


// ============================================================
// COMPONENT
// ============================================================

@Component({
  selector: 'app-timesheet',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ModalComponent
  ],
  templateUrl: './timesheet.component.html',
  styles: ``
})
export class TimesheetComponent implements OnInit {

  // ============================================================
  // WEEK
  // ============================================================

  currentWeekStart!: Date;

  weekDays: WeekDay[] = [];


  // ============================================================
  // TIMESHEET ENTRIES
  // ============================================================

  entries: TimesheetEntry[] = [];

  private nextId = 1;


  // ============================================================
  // MODAL
  // ============================================================

  isOpen = false;

  selectedEntry: TimesheetEntry | null = null;


  // ============================================================
  // FORM
  // ============================================================

  entryDate = '';

  entryDescription = '';

  entryHours = 1;


  // ============================================================
  // INIT
  // ============================================================

  ngOnInit(): void {

    this.goToCurrentWeek();

    this.loadSampleData();

  }


  // ============================================================
  // CURRENT WEEK
  // ============================================================

  goToCurrentWeek(): void {

    const today = new Date();

    this.currentWeekStart = this.getMonday(today);

    this.generateWeekDays();

  }


  // ============================================================
  // PREVIOUS WEEK
  // ============================================================

  previousWeek(): void {

    const date = new Date(
      this.currentWeekStart
    );

    date.setDate(
      date.getDate() - 7
    );

    this.currentWeekStart = date;

    this.generateWeekDays();

  }


  // ============================================================
  // NEXT WEEK
  // ============================================================

  nextWeek(): void {

    const date = new Date(
      this.currentWeekStart
    );

    date.setDate(
      date.getDate() + 7
    );

    this.currentWeekStart = date;

    this.generateWeekDays();

  }


  // ============================================================
  // GET MONDAY
  // ============================================================

  getMonday(date: Date): Date {

    const result = new Date(date);

    result.setHours(0, 0, 0, 0);

    const day = result.getDay();

    const difference =
      day === 0
        ? -6
        : 1 - day;

    result.setDate(
      result.getDate() + difference
    );

    return result;

  }


  // ============================================================
  // GENERATE MONDAY - FRIDAY
  // ============================================================

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

      const date = new Date(
        this.currentWeekStart
      );

      date.setDate(
        date.getDate() + i
      );

      this.weekDays.push({

        dayName: dayNames[i],

        dayNumber: date.getDate(),

        monthName:
          date.toLocaleDateString(
            'en-ZA',
            {
              month: 'short',
              year: 'numeric'
            }
          ),

        dateString:
          this.formatDate(date)

      });

    }

  }


  // ============================================================
  // FORMAT DATE
  // ============================================================

  formatDate(date: Date): string {

    const year =
      date.getFullYear();

    const month =
      String(
        date.getMonth() + 1
      ).padStart(2, '0');

    const day =
      String(
        date.getDate()
      ).padStart(2, '0');

    return `${year}-${month}-${day}`;

  }


  // ============================================================
  // WEEK RANGE
  // ============================================================

  formatWeekRange(): string {

    if (!this.currentWeekStart) {
      return '';
    }

    const monday =
      new Date(
        this.currentWeekStart
      );

    const friday =
      new Date(
        this.currentWeekStart
      );

    friday.setDate(
      friday.getDate() + 4
    );

    const mondayText =
      monday.toLocaleDateString(
        'en-ZA',
        {
          day: '2-digit',
          month: 'short',
          year: 'numeric'
        }
      );

    const fridayText =
      friday.toLocaleDateString(
        'en-ZA',
        {
          day: '2-digit',
          month: 'short',
          year: 'numeric'
        }
      );

    return `${mondayText} - ${fridayText}`;

  }


  // ============================================================
  // GET ENTRIES FOR DAY
  // ============================================================

  getEntriesForDay(
    dateString: string
  ): TimesheetEntry[] {

    return this.entries
      .filter(
        entry =>
          entry.date === dateString
      );

  }


  // ============================================================
  // DAY TOTAL
  // ============================================================

  getDayTotal(
    dateString: string
  ): number {

    return this.getEntriesForDay(
      dateString
    ).reduce(
      (total, entry) =>
        total + Number(entry.hours),
      0
    );

  }


  // ============================================================
  // WEEK TOTAL
  // ============================================================

  getWeekTotal(): number {

    return this.weekDays.reduce(
      (total, day) =>
        total +
        this.getDayTotal(
          day.dateString
        ),
      0
    );

  }


  // ============================================================
  // SHORT DESCRIPTION
  // ============================================================

  getShortDescription(
    description: string
  ): string {

    const maxLength = 100;

    if (
      description.length <= maxLength
    ) {
      return description;
    }

    return (
      description.substring(
        0,
        maxLength
      ) + '...'
    );

  }


  // ============================================================
  // ADD ENTRY
  // ============================================================

  openAddModal(
    date: string
  ): void {

    this.resetModalFields();

    this.entryDate = date;

    this.entryHours = 1;

    this.isOpen = true;

  }


  // ============================================================
  // EDIT ENTRY
  // ============================================================

  openEditModal(
    entry: TimesheetEntry
  ): void {

    this.selectedEntry = entry;

    this.entryDate =
      entry.date;

    this.entryDescription =
      entry.description;

    this.entryHours =
      entry.hours;

    this.isOpen = true;

  }


  // ============================================================
  // ADD / UPDATE
  // ============================================================

  handleAddOrUpdateEntry(): void {

    if (!this.entryDate) {

      console.error(
        'Please select a date'
      );

      return;

    }


    if (
      !this.entryDescription.trim()
    ) {

      console.error(
        'Please enter a description'
      );

      return;

    }


    if (
      !this.entryHours ||
      this.entryHours <= 0
    ) {

      console.error(
        'Please enter valid hours'
      );

      return;

    }


    if (this.selectedEntry) {

      this.updateEntry();

    } else {

      this.createEntry();

    }

  }


  // ============================================================
  // CREATE
  // ============================================================

  createEntry(): void {

    const newEntry: TimesheetEntry = {

      id: this.nextId++,

      date: this.entryDate,

      description:
        this.entryDescription.trim(),

      hours:
        Number(this.entryHours)

    };


    this.entries.push(
      newEntry
    );

    this.closeModal();

  }


  // ============================================================
  // UPDATE
  // ============================================================

  updateEntry(): void {

    if (!this.selectedEntry) {
      return;
    }


    this.selectedEntry.date =
      this.entryDate;

    this.selectedEntry.description =
      this.entryDescription.trim();

    this.selectedEntry.hours =
      Number(this.entryHours);


    this.closeModal();

  }


  // ============================================================
  // DELETE
  // ============================================================

  deleteEntry(): void {

    if (!this.selectedEntry) {
      return;
    }


    this.entries =
      this.entries.filter(
        entry =>
          entry.id !==
          this.selectedEntry!.id
      );


    this.closeModal();

  }


  // ============================================================
  // DATE PICKER
  // ============================================================

  openDatePicker(
    event: MouseEvent
  ): void {

    const inputEl =
      event.target as HTMLInputElement;

    if (
      inputEl &&
      typeof inputEl.showPicker === 'function'
    ) {

      inputEl.showPicker();

    }

  }


  // ============================================================
  // RESET MODAL
  // ============================================================

  resetModalFields(): void {

    this.selectedEntry = null;

    this.entryDate = '';

    this.entryDescription = '';

    this.entryHours = 1;

  }


  // ============================================================
  // CLOSE MODAL
  // ============================================================

  closeModal(): void {

    this.isOpen = false;

    this.resetModalFields();

  }


  // ============================================================
  // SAMPLE DATA
  // REMOVE WHEN API IS CONNECTED
  // ============================================================

  loadSampleData(): void {

    this.entries = [

      // MONDAY
      {
        id: this.nextId++,
        date: this.getWeekDate(0),
        description:
          'Worked on the user management API and fixed authentication validation issues.',
        hours: 3
      },

      {
        id: this.nextId++,
        date: this.getWeekDate(0),
        description:
          'Updated Angular forms and fixed date binding issues.',
        hours: 2
      },

      {
        id: this.nextId++,
        date: this.getWeekDate(0),
        description:
          'Tested the leave request functionality and fixed issues found during testing.',
        hours: 1
      },

      {
        id: this.nextId++,
        date: this.getWeekDate(0),
        description:
          'Reviewed code and prepared changes for deployment.',
        hours: 2
      },


      // TUESDAY
      {
        id: this.nextId++,
        date: this.getWeekDate(1),
        description:
          'Implemented timesheet functionality and worked on the weekly UI.',
        hours: 3
      },

      {
        id: this.nextId++,
        date: this.getWeekDate(1),
        description:
          'Fixed responsive layout issues and tested the application.',
        hours: 2
      },


      // WEDNESDAY
      {
        id: this.nextId++,
        date: this.getWeekDate(2),
        description:
          'Worked on API integration and database queries.',
        hours: 3
      },

      {
        id: this.nextId++,
        date: this.getWeekDate(2),
        description:
          'Performed manual testing and fixed bugs.',
        hours: 2
      },


      // THURSDAY
      {
        id: this.nextId++,
        date: this.getWeekDate(3),
        description:
          'Worked with the team to review requirements and implement requested changes.',
        hours: 4
      },


      // FRIDAY
      {
        id: this.nextId++,
        date: this.getWeekDate(4),
        description:
          'Completed testing and prepared the completed functionality for review.',
        hours: 3
      }

    ];

  }


  // ============================================================
  // GET DATE FROM CURRENT WEEK
  // ============================================================

  getWeekDate(
    dayOffset: number
  ): string {

    const date =
      new Date(
        this.currentWeekStart
      );

    date.setDate(
      date.getDate() +
      dayOffset
    );

    return this.formatDate(date);

  }

}