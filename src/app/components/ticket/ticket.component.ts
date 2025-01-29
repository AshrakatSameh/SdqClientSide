import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { AuthService } from 'src/app/service/auth.service';
import { TicketService } from 'src/app/service/ticket.service';

@Component({
  selector: 'app-ticket',
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.css']
})
export class TicketComponent implements OnInit{
  ticket: any;
  messages: any[] = [];
  userId: string | null = null; // To store the userId


  constructor(private route: ActivatedRoute, private ticketService: TicketService, private auth:AuthService) { }
  ngOnInit(): void {
  const ticketId = this.route.snapshot.paramMap.get('id');
  if (ticketId) {
    this.ticketService.getTicketById(ticketId).subscribe(data => {
      this.ticket = data;
      this.ticket.messages.forEach((message: any)=>{
        this.ticketService.getUserById(message.createdBy).subscribe(data => {
          message.userDetails = data.name;
        }, error => {
          console.error('Error fetching user details:', error);
        });
      });
      this.ticketService.getUserById(this.ticket.customerId).subscribe(data => {
        this.ticket.customerDetails = data.name;
      });
      this.ticketService.getUserById(this.ticket.assignedToEmployee).subscribe(data => {
        this.ticket.assignedToDetails = data.name;
      });
      if(this.ticket.assignmentHistories){
        this.ticket.assignmentHistories.forEach((history: any) => {
          this.ticketService.getUserById(history.employeeId).subscribe(data => {
            history.assignedToDetails = data.name;
          });
          this.ticketService.getUserById(history.assignedBy).subscribe(data => {
            history.assignedByDetails = data.name;
          });
        });
      }
      console.log(this.ticket);
    });
    this.getMessages(ticketId);
   this.userId= this.auth.getUserDetail()?.id;
  }
  }

  getUser(id: number): void {
    this.ticketService.getUserById(this.ticket.customerId).subscribe(data => {
      this.ticket.customerDetails = data;
    });
  }

  isImage(attachment: any): boolean {
    const imageExtensions = ['.jpg', '.jpeg', '.png', '.gif', '.bmp', '.svg'];
    return imageExtensions.some((ext) => attachment.fileTitle.toLowerCase().endsWith(ext));
  }

  isPdf(attachment: any): boolean {
    return attachment.fileTitle.toLowerCase().endsWith('.pdf');
  }

  getUserIdFromToken(): string | null {
    const token = localStorage.getItem('token');  // Assuming token is stored in localStorage
    if (token) {
      const decoded: any = jwtDecode(token);
      return decoded.userId;  // Adjust based on the token structure
    }
    return null;
  }
  getTicketById(ticketId: string): void {
    this.ticketService.getTicketById(ticketId).subscribe(data => {
      this.ticket = data;
    });
  }

  assignTicket(ticketId: number) {
    if (!this.userId) {
      alert('User not logged in.');
      return;
    }

    this.ticketService.updateTicketStatus(ticketId, 2).subscribe({
      next: () => {
        alert('Ticket assigned to you successfully!');
        this.ticket.assignedToEmployee = this.userId;
        this.ticket.statusId = 2; // Update UI instantly
        // this.getTicketById(ticketId.toString());
      },
      error: err => console.error('Error updating ticket:', err)
    });
  }

  closeTicket(ticketId: number) {
    this.ticketService.updateTicketStatus(ticketId, 4).subscribe({
      next: () => {
        alert('Ticket closed successfully!');
        this.ticket.statusId = 4; // Update UI instantly
        // this.getTicketById(ticketId.toString());
      },
      error: err => console.error('Error updating ticket:', err)
    });
  }


  getMessages(ticketId: any): void {
    this.ticketService.getMessages(ticketId).subscribe({
      next: (data) => {
        this.messages = data;
        this.messages.forEach((message) => {
          if (message.createdBy) {
            this.ticketService.getUserById(message.createdBy).subscribe(
              (createdByData) => {
                message.createdByName = createdByData.name; // Store `createdBy` details on the message
                console.log(`Message ID: ${message.id}, CreatedBy: ${message.createdByName}`);
              },
              (err) => {
                console.error(`Error fetching createdBy details for message ID: ${message.id}`, err);
              }
            );
          }
        });
       
      },
      error: (err) => {
        console.error('Error fetching messages:', err);
      },
    });
  }
  messageContent = '';
  sendMessage(ticketId: number): void {
    if (this.messageContent.trim()) {
      this.ticketService.postMessage(ticketId, this.messageContent).subscribe({
        next: (response) => {
          console.log('Message sent successfully', response);
          this.messageContent = '';
          this.getMessages(ticketId);
        },
        error: (err) => {
          console.error('Error sending message', err);
        }
      });
    } else {
      console.log('Message content cannot be empty.');
    }
  }
  }
