import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { getUsernameFromToken } from '../Helpers/jwt-helper'
import { jwtDecode } from 'jwt-decode';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:7149/api/Auth';

  private authStatus = new BehaviorSubject<boolean>(this.isLoggedIn());
  authStatus$ = this.authStatus.asObservable();

  private usernameSubject = new BehaviorSubject<string | null>(this.getUsername());
  username$ = this.usernameSubject.asObservable();

  private expirationTimeout: any;

  constructor(private http: HttpClient, private router: Router) {}

  register(user: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, user);
  }

  login(credentials: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, credentials);
  }

  // Check if the user is logged in (token exists in localStorage)
  isLoggedIn(): boolean {
    return !!localStorage.getItem('token'); // Returns true if token exists
  }

  loginEmit(token: string): void {
    localStorage.setItem('token', token);

    this.startTokenExpirationTimer(token);  // Start the expiration timer

    const username = getUsernameFromToken(token);
    if (username) {
      this.usernameSubject.next(username); // Emit the username
    }
    this.authStatus.next(true); // Emit login event
  }

  logout(): void {
    localStorage.removeItem('token');

    clearTimeout(this.expirationTimeout);  // Clear any existing timeout

    this.usernameSubject.next(null); // Clear the username
    this.authStatus.next(false); // Emit logout event

    this.router.navigate(['/login']);    
  }

  // Helper method to get the username from localStorage
  getUsername(): string | null {
    const token = localStorage.getItem('token');
    if (token) {
      return getUsernameFromToken(token);
    }
    return null;
  }

  private getTokenExpirationDate(token: string): Date | null {
    const decoded: any = jwtDecode(token);
    if (decoded.exp === undefined) {
      return null;
    }
    const date = new Date(0);
    date.setUTCSeconds(decoded.exp);
    return date;
  }

  private startTokenExpirationTimer(token: string): void {
    const expirationDate = this.getTokenExpirationDate(token);
    if (expirationDate) {
      const expiresIn = expirationDate.getTime() - Date.now();  // Calculate milliseconds until expiration
      this.expirationTimeout = setTimeout(() => {
        this.logout();  // Auto logout when the token expires
        alert('Session expired. Please login again.');
      }, expiresIn);
    }
  }
}
