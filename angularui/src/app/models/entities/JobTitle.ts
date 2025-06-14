import { JobTitleDto } from '../dtos/JobtitleDto';
import { Seniority } from '../Enums/Seniority';

export class JobTitle {
  id: number = 0;
  title: string = '';
  description: string = '';
  seniority: Seniority = Seniority.Junior;
  departmentId: number = 0;
  departmentName?: string;

  constructor(init?: Partial<JobTitle>) {
    Object.assign(this, init);
  }

  static fromDto(dto: JobTitleDto): JobTitle {
    return new JobTitle({
      id: dto.id,
      title: dto.title,
      description: dto.description,
      seniority: dto.seniority as Seniority, 
      departmentName: dto.departmentName
    });
  }

  toDto(): JobTitleDto {
    return {
      id: this.id,
      title: this.title,
      description: this.description,
      seniority: this.seniority,
      departmentName: this.departmentName ?? ''
    };
  }
}
