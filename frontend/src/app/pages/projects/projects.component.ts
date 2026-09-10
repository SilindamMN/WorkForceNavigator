import { Component, inject, OnInit } from '@angular/core';

import {
  TableColumn,
  BasicTableThreeComponent
} from '../../shared/components/tables/basic-tables/basic-table-three/basic-table-three.component';

import { ProjectsService } from '../../shared/services/projects.service';

import {
  Project,
  ProjectDto
} from '../../models/project';

@Component({
  selector: 'app-projects',
  standalone: true,
  imports: [
    BasicTableThreeComponent
  ],
  templateUrl: './projects.component.html'
})
export class ProjectsComponent implements OnInit {

  projectsService = inject(ProjectsService);

  projects: Project[] = [];

  columns: TableColumn[] = [
    {
      key: 'projectName',
      label: 'Project Name'
    },
    {
      key: 'clientName',
      label: 'Client Name'
    },
    {
      key: 'teamName',
      label: 'Team'
    },
    {
      key: 'description',
      label: 'Description'
    },
    {
      key: 'startDate',
      label: 'Start Date'
    },
    {
      key: 'endDate',
      label: 'End Date'
    }
  ];

  ngOnInit(): void {
    this.loadProjects();
  }

  loadProjects(): void {

    this.projectsService
      .getAll()
      .subscribe({
        next: (data) => {

          this.projects = data;

          console.log(
            'PROJECTS:',
            this.projects
          );
        },

        error: (error) => {

          console.error(
            'ERROR LOADING PROJECTS:',
            error
          );
        }
      });
  }

  createProject(): void {

    console.log(
      'CREATE PROJECT CLICKED'
    );
  }

  editProject(
    project: Project
  ): void {

    console.log(
      'EDIT PROJECT:',
      project
    );
  }

  deleteProject(
    project: Project
  ): void {

    console.log(
      'DELETE PROJECT:',
      project
    );
  }

  handleSave(
    event: {
      mode: 'add' | 'edit';
      data: Project;
    }
  ): void {

    if (
      event.mode === 'add'
    ) {

      const projectDto: ProjectDto = {

        projectName:
          event.data.projectName,

        clientName:
          event.data.clientName,

        teamName:
          event.data.teamName,

        description:
          event.data.description,

        startDate:
          event.data.startDate,

        endDate:
          event.data.endDate
      };

      this.projectsService
        .create(projectDto as Project)
        .subscribe({
          next: () => {

            this.loadProjects();
          },

          error: (error) => {

            console.error(
              'CREATE PROJECT FAILED:',
              error
            );
          }
        });

      return;
    }

    this.projectsService
      .update(
        event.data,
        ''
      )
      .subscribe({
        next: () => {

          this.loadProjects();
        },

        error: (error) => {

          console.error(
            'UPDATE PROJECT FAILED:',
            error
          );
        }
      });
  }
}