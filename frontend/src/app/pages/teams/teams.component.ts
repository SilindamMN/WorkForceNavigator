import { Component, inject, OnInit } from '@angular/core';
import { BasicTableThreeComponent, TableColumn } from '../../shared/components/tables/basic-tables/basic-table-three/basic-table-three.component';
import { Department } from '../../models/department';
import { Team, TeamDto } from '../../models/team';
import { DepartmentsService } from '../../shared/services/departments.service';
import { TeamsService } from '../../shared/services/teams.service';

@Component({
  selector: 'app-teams',
  standalone: true,
  imports: [
    BasicTableThreeComponent
  ],
  templateUrl: './teams.component.html'
})
export class TeamsComponent implements OnInit {

  teamsService = inject(TeamsService);

  departmentsService = inject(DepartmentsService);

  teams: Team[] = [];

  departments: Department[] = [];

  columns: TableColumn[] = [
    {
      key: 'teamName',
      label: 'Team Name'
    },
    {
      key: 'description',
      label: 'Description'
    },
    {
      key: 'departmentName',
      label: 'Department',
      type: 'select',
      valueKey: 'departmentId',
      options: []
    }
  ];

  ngOnInit(): void {

    this.loadTeams();

    this.loadDepartments();
  }

  loadTeams(): void {

    this.teamsService
      .getAll()
      .subscribe({
        next: (data) => {

          this.teams = data;

          console.log(
            'TEAMS:',
            this.teams
          );
        },

        error: (error) => {

          console.error(
            'ERROR LOADING TEAMS:',
            error
          );
        }
      });
  }

  loadDepartments(): void {

    this.departmentsService
      .getAll()
      .subscribe({
        next: (data) => {

          this.departments = data;

          const field = this.columns.find(
            column =>
              column.key === 'departmentName'
          );

          if (!field) {
            return;
          }

          field.options = data.map(
            (department: Department) => ({
              value: String(
                department.id ?? 0
              ),
              label:
                department.departmentName
            })
          );
        },

        error: (error) => {

          console.error(
            'ERROR LOADING DEPARTMENTS:',
            error
          );
        }
      });
  }

  createTeam(): void {

    console.log(
      'CREATE TEAM CLICKED'
    );
  }

  editTeam(
    team: Team
  ): void {

    console.log(
      'EDIT TEAM:',
      team
    );

    console.log(
      'TEAM ID:',
      team.id
    );

    console.log(
      'DEPARTMENT ID:',
      team.departmentId
    );

    this.getTeamMembers(
      team.id
    );
  }

  getTeamMembers(
    teamId: number
  ): void {

    this.teamsService
      .getTeamMembersByTeamId(teamId)
      .subscribe({
        next: (data) => {

          console.log(
            'TEAM MEMBERS:',
            data
          );
        },

        error: (error) => {

          console.error(
            'ERROR GETTING TEAM MEMBERS:',
            error
          );
        }
      });
  }

  deleteTeam(
    team: Team
  ): void {

    console.log(
      'DELETE TEAM:',
      team
    );
  }

  handleSave(
    event: {
      mode: 'add' | 'edit';
      data: Team;
    }
  ): void {

    if (
      event.mode === 'add'
    ) {

      const teamDto: TeamDto = {

        id: 0,

        teamName:
          event.data.teamName,

        description:
          event.data.description,

        departmentName:
          event.data.departmentName ?? ''
      };

      console.log(
        'CREATE TEAM DTO:',
        teamDto
      );

      this.teamsService
        .create(event.data)
        .subscribe({
          next: () => {

            this.loadTeams();
          },

          error: (error) => {

            console.error(
              'CREATE TEAM FAILED:',
              error
            );
          }
        });

      return;
    }

    this.teamsService
      .update(
        event.data,
        ''
      )
      .subscribe({
        next: () => {

          this.loadTeams();
        },

        error: (error) => {

          console.error(
            'UPDATE TEAM FAILED:',
            error
          );
        }
      });
  }
}