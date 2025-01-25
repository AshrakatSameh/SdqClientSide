import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../Interfaces/User';
import { Message } from '../Interfaces/Message';
import { decode } from 'punycode';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class TicketService {

  //apiUrl = 'http://localhost:5025/api/Ticket';
 apiUrl= 'http://localhost:5025/api/Ticket/getall details';

  constructor(private http: HttpClient) { }

  getTickets(category?: string,status?: string, priority?: string): Observable<any[]> {
    let params = new HttpParams();

    if (category) {
      params = params.set('category', category);
    }
    if (status) {
      params = params.set('status', status);
    }
    if (priority) {
      params = params.set('priority', priority);
    }

    return this.http.get<any[]>(this.apiUrl, { params });
  }

  api = 'http://localhost:5025/api/'
  getUserById(userId: number): Observable<User> {
    return this.http.get<User>(`${this.api}Account/${userId}`);
  }

  getMessages(ticketId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.api}TicketMessage/ticketMessages/${ticketId}`);
  }

  updateTicketStatus(ticketId: number, statusId: number): Observable<any> {
    const url = `${this.api}Ticket/${ticketId}`;
    const payload = {
      ticketId: ticketId,
      statusId: statusId,
    };
  
    // Get the authentication token from local storage or your authentication service
    const token = localStorage.getItem('token');  // Or use your auth service to fetch the token
  
    // Add the token to the headers
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  
    return this.http.put(url, payload, { headers });
  }

  postMessage(ticketId: number, messageContent: string): Observable<any> {
    const token = localStorage.getItem('token');
    const decodedToken: any = jwtDecode(token!);
    
    const createdBy = decodedToken.nameid;
    const createdAt = new Date().toISOString(); // Get current timestamp
    
    const messageData: Message = {
      ticketId: ticketId,
      createdBy: createdBy,
      createdAt: createdAt,
      message: messageContent,
      isSeen: false, // Initially set to false
    };

    return this.http.post(this.api+'TicketMessage', messageData);
  }
 
}
