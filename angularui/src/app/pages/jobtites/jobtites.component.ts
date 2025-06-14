import { Component } from '@angular/core';

@Component({
  selector: 'app-jobtites',
  imports: [],
  templateUrl: './jobtites.component.html',
  styleUrl: './jobtites.component.css'
})
export class JobtitesComponent implements OnInit{

  constructor(){
    this.jobtitleService = this.genericCrudService.create<JobTitle>('JobTitle');
  }
ngOnit(){
 this.LoadJobTitles(); 
}

  jobtitleService: (GenericCrudService<JobTitle>)
  jobTitles: [] =[];
  jobtitle: JobTitleDto =  new JobTitleDto();
  genericCrudService = inject(GenericCrudFactoryService);

  LoadJobTitles():void{
    this.jobtitleService.getAll().subscribe(
      (data)=>{
        this.jobTitles = data;
      },(error)=>{
        console.log("Error Loading Data");
      })
  }
}