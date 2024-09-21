import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/Services/auth.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  isLoggedIn = false;
  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    // Subscribe to the authStatus observable to track changes in login state
    this.authService.authStatus$.subscribe(
      (status: boolean) => {
        this.isLoggedIn = status;
      }
    );
  }

  logout() {
    this.authService.logout();    
  }

}
