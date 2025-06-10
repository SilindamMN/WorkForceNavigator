import { Component, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { AuthserviceService } from './services/authservice.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']  
})
export class AppComponent  implements OnInit{
  constructor(private router:Router,private auth:AuthserviceService){}

ngOnInit():void{
  const token = this.auth.getToken();
  if(token && this.auth.isLoggedIn()){
    const user = this.auth.getUserFromStorage();
    if(user){
      this.auth['userSubject'].next(user);
    }
    }
    else{
      this.auth.logout();
      this.router.navigate(['/login']);
    }
}
}
