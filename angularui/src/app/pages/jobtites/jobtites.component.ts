import { JobTitleDto } from './../../models/dtos/JobtitleDto';
import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { JobTitle } from '../../models/entities/JobTitle';
import { GenericCrudFactoryService } from '../../services/generic-crud-factory.service';
import { GenericCrudService } from '../../services/genericcrud.service';
import { AddModalComponent } from '../generic/addmodal/addmodal.component';
import { DetaisModalComponent } from '../generic/detailsmodal/detailsmodal.component';
import { EditModalComponent } from '../generic/editmodal/editmodal.component';
import { ModalField } from '../generic/basemodal/basemodal.component';
import { Seniority } from '../../models/Enums/Seniority';
import { DatatableComponent } from '../generic/datatable/datatable.component';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Department } from '../../models/Department';

@Component({
  selector: 'app-jobtites',
  imports: [AddModalComponent, EditModalComponent, DetaisModalComponent, DatatableComponent, FormsModule, CommonModule],
  standalone: true,
  templateUrl: './jobtites.component.html',
  styleUrl: './jobtites.component.css'
})
export class JobtitesComponent implements OnInit {

  @ViewChild('addModal') addModal!: AddModalComponent;
  @ViewChild('editModal') editModal!: EditModalComponent;
  @ViewChild('viewModal') viewModal!: DetaisModalComponent


  jobTitles: JobTitleDto[] = [];
  jobtitleDto: JobTitleDto = new JobTitleDto();
  jobtitle: JobTitle = new JobTitle();
  departments: Department[] = [];
  departmentService: (GenericCrudService<Department>);
  genericCrudService = inject(GenericCrudFactoryService);
  jobtitleService: (GenericCrudService<JobTitleDto>);


  columns = [
    { field: 'title', label: 'Title' },
    { field: 'description', label: 'Description' },
    { field: 'seniority', label: 'Seniority' },
    { field: 'departmentName', label: 'Department Name' },
  ]

get jobTitlesFields(): ModalField[] {
  return [
    { name: 'title', label: 'Title', type: 'text', required: true },
    { name: 'description', label: 'Description', type: 'text', required: true },
    {
      name: 'seniority',
      label: 'Seniority',
      type: 'select',
      required: true,
      options: Object.values(Seniority).map(value => ({
        value,
        label: value
      }))
    },
    {
      name: 'departmentId', 
      label: 'Department',
      type: 'select',
      required: true,
      options: this.departments.map(department => ({
        value: department.id,
        label: department.departmentName
      }))
    }
  ];
}


  constructor() {
    this.jobtitleService = this.genericCrudService.create<JobTitleDto>('JobTitle');
    this.departmentService = this.genericCrudService.create<Department>('Department');
  }

  ngOnInit(): void {
    this.LoadJobTitles();
    this.loadDepartments();
  }

  LoadJobTitles(): void {
    this.jobtitleService.getAll().subscribe(
      (data) => {
        this.jobTitles = data;
      }, (error) => {
        console.log("Error Loading Data");
      })
  }

 loadDepartments(): void {
    this.departmentService.getAll().subscribe(
      (data) => {
        this.departments = data;
      }, (error) => {
        console.log('Error Loading Users', error);
      })
  }

  addJobTitles(newJobTitle: JobTitle): void {
    this.jobtitleService.create(newJobTitle.toDto(), '/CreateJobTitle').subscribe(
      (data) => {
        this.LoadJobTitles();
        console.log("User Created", data);
      },
      (error) => {
        console.log("Error Creating User", error);
      })
  }

  editJobTitles(jobTitle: JobTitle): void {
    this.jobtitleService.update(jobTitle.toDto(), '/UpdateJobTitle').subscribe(
      (data) => {
        this.LoadJobTitles();
      },
      (error) => {
        console.log("Error Editing", error);
      })
  }

  deleteJobTitle(id: number): void {
    if (confirm("Are you sure you want to delete")) {
      this.jobtitleService.delete(id, `/DeleteDepartment?id=${id}`).subscribe(
        () => {
          console.log("User Deleted");
          this.LoadJobTitles();
        },
        (error) => {
          console.log("Error Deleting User", error);
        })
    }
  }
openModal(modal: any, entity?: JobTitleDto): void {
  this.jobtitleDto = entity ?? new JobTitleDto();
  modal.open();
}
}