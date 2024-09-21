import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/Services/auth.service';

@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.css']
})
export class UserDashboardComponent implements OnInit {

  username: string | null = '';
  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    // Subscribe to the username observable to display it on the dashboard
    this.authService.username$.subscribe(
      (name: string | null) => {
        this.username = name;
      }
    );
  }

}
