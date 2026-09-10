import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GenericCrudService } from './generic.service';
import { Team, UserTeamListDto, TeamMemberDetailsDto, AddMemberDto } from '../../models/team';

@Injectable({ providedIn: 'root' })
export class TeamsService extends GenericCrudService<Team> {
  constructor(http: HttpClient) {
    super(http, 'teams');
  }
    getUserTeamByUserId(userId: number): Observable<UserTeamListDto[]> {
      return this.http.get<UserTeamListDto[]>(
        `${this.baseUrl}/?user=${userId}`
      );
    }

   getTeamMembersByTeamId(teamId: number): Observable<TeamMemberDetailsDto[]> {
  return this.http.get<TeamMemberDetailsDto[]>(
    `${this.baseUrl}/${teamId}/withdetails`
  );
}
    
    addMemberToTeam(request: AddMemberDto): Observable<any> {
  return this.http.post<any>(
    `${this.baseUrl}/add-member`,
    request
  );
}
}