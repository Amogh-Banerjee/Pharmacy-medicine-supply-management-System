import {jwtDecode} from 'jwt-decode';

export function getUsernameFromToken(token: string): string | null {
  try {
    const decodedToken: any = jwtDecode(token);
    return decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"] || null;
  } catch (error) {
    return null;
  }
}
