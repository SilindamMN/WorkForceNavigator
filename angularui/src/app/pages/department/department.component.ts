import { Department } from './../../models/Department';
import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { GenericCrudService } from '../../services/genericcrud.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { GenericCrudFactoryService } from '../../services/generic-crud-factory.service';
import { DatatableComponent } from '../generic/datatable/datatable.component';
import { DetaisModalComponent } from '../generic/detailsmodal/detailsmodal.component';
import { AddModalComponent } from '../generic/addmodal/addmodal.component';
import { EditModalComponent } from '../generic/editmodal/editmodal.component';
import { ModalField } from '../generic/basemodal/basemodal.component';

@Component({
  selector: 'app-department',
  imports: [FormsModule, CommonModule, DatatableComponent,AddModalComponent,EditModalComponent,DetaisModalComponent ],
  standalone: true,
  templateUrl: './department.component.html',
  styleUrl: './department.component.css'
})
export class DepartmentComponent implements OnInit {

@ViewChild('addModal') addModal!: AddModalComponent;
@ViewChild('editModal') editModal!: EditModalComponent;
@ViewChild('viewModal') viewModal!: DetaisModalComponent

  departments: Department[] = [];
  departmentService: (GenericCrudService<Department>);
  genericCrudService = inject(GenericCrudFactoryService);
  department: Department = new Department();
columns = [
  { field: 'departmentName', label: 'Department Name' },
  { field: 'description', label: 'Description' },
];
departmentFields: ModalField []= [
  { name: 'departmentName', label: 'Department Name', type: 'text', required: true },
  { name: 'description', label: 'Description', type: 'text', required: true }
];



  constructor() {
    this.departmentService = this.genericCrudService.create<Department>('Department');
  }
  ngOnInit(): void {
    this.loadDepartments();
  }

  loadDepartments(): void {
    this.departmentService.getAll().subscribe(
      (data) => {
        this.departments = data;
      }, (error) => {
        console.log('Error Loading Users', error);
      })
  }

  addDepartment(newDepartment:Department): void {
    this.departmentService.create(this.department).subscribe(
      (data) => {
        this.loadDepartments();
      },
      (error) => {
        console.log("Error Creating User", error);
      })
  }

  editDepartment(department: Department): void {
    this.departmentService.update(department).subscribe(
      (data) => {
        this.loadDepartments();
      },
      (error) => {
        console.log("Error Editing", error);
      })
  }
  
  selectedUser: any;

  viewDepartment(department: Department): void {
    // Show a modal or log to console
    console.log('Viewing department:', department);
  }

  deleteDepartment(id: number): void {
    if (confirm("Are you sure you want to delete")) {
      this.departmentService.delete(id).subscribe(
        () => {
          console.log("User Deleted");
          this.loadDepartments();
        },
        (error) => {
          console.log("Error Deleting User", error);
        })
    }
  }
}