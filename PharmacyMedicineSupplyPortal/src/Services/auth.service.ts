import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { getUsernameFromToken } from '../Helpers/jwt-helper'

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:7149/api/Auth';

  private authStatus = new BehaviorSubject<boolean>(this.isLoggedIn());
  authStatus$ = this.authStatus.asObservable();

  private usernameSubject = new BehaviorSubject<string | null>(this.getUsername());
  username$ = this.usernameSubject.asObservable();

  constructor(private http: HttpClient) {}

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
    const username = getUsernameFromToken(token);
    if (username) {
      this.usernameSubject.next(username); // Emit the username
    }
    this.authStatus.next(true); // Emit login event
  }

  logout(): void {
    localStorage.removeItem('token');
    this.usernameSubject.next(null); // Clear the username
    this.authStatus.next(false); // Emit logout event
  }

  // Helper method to get the username from localStorage
  getUsername(): string | null {
    const token = localStorage.getItem('token');
    if (token) {
      return getUsernameFromToken(token);
    }
    return null;
  }
}
