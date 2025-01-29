import { Component } from '@angular/core';
import { DataTableModule } from '@bhplugin/ng-datatable';
import { faComment, faHistory , faImage} from '@fortawesome/free-solid-svg-icons';
import { User } from 'src/app/Interfaces/User';
import { AuthService } from 'src/app/service/auth.service';
import { TicketService } from 'src/app/service/ticket.service';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent {

  constructor(private ticketService: TicketService, private auth: AuthService) {}
  userRole: string = '';
  search1 = '';
  search2 = '';
  faComment = faComment;
  faClockRotateLeft = faHistory;
  faImage = faImage;
  tickets: any[] = [];
  categoryFilter: number = 0 
  statusFilter: number = 0
  priorityFilter: number  = 0
  filteredTickets = [...this.tickets];
  categories = [
    { id: 1, name: 'Complaition' },
    { id: 2, name: 'Suggestion' },
  ];
  
  statuses = [
    { id: 1, name: 'Open' },
    { id: 2, name: 'In Progress' },
    { id: 3, name: 'Resolved' },
    {id: 4, name: 'Closed'}
  ];
  
  priorities = [
    { id: 1, name: 'Low' },
    { id: 2, name: 'Medium' },
    { id: 3, name: 'High' }
  ];
  selectedTicket: any = null; // Data to display in the modal
  showModal = false;

  messages: any[] = [];
  openModal(ticketId: number): void {
    // Find the selected ticket from the list (or fetch from API if needed)
    this.getMessages(ticketId);
    this.selectedTicket = this.tickets.find(ticket => ticket.id === ticketId);
    this.showModal = true; // Open the modal
    // this.getTickets();
  }

  closeModal() {
    this.showModal = false;
  }
  ngOnInit() {
    this.getTickets();
    this.userRole = this.auth.getUserDetail()!.roles[1];

  }
 
  getTickets(): void {
    this.ticketService.getTickets(this.categoryFilter,this.statusFilter, this.priorityFilter).subscribe({
      next: (data) => {
        this.tickets = data;

        // Fetch user details for Customer and AssignedToEmployee
        this.tickets.forEach((ticket) => {
          // Fetch Customer
          this.ticketService.getUserById(ticket.customerId).subscribe(
            (customerData) => {
              ticket.customerDetails = customerData.name;
              console.log('Customer:', ticket.customerDetails); // Store customer details on the ticket

            },
            (err) => {
              console.error('Error fetching customer details:', err);
            }
          );

          // Fetch AssignedToEmployee
          if (ticket.assignedToEmployee) {
            this.ticketService.getUserById(ticket.assignedToEmployee).subscribe(
              (assignedToData) => {
                ticket.assignedToDetails = assignedToData.name; 
                console.log('AssignedToEmployee:', ticket.assignedToDetails); // Store assignedTo employee details on the ticket
              },
              (err) => {
                console.error('Error fetching assignedTo employee details:', err);
              }
            );
          }
          if (ticket.messages && ticket.messages.length > 0) {
            ticket.messages.forEach((message: any) => {
              if (message.createdBy) {
                this.ticketService.getUserById(message.createdBy).subscribe(
                  (createdByData) => {
                    message.createdByDetails = createdByData.name; // Store `createdBy` details on the message
                    console.log(`Message ID: ${message.id}, CreatedBy: ${message.createdByDetails}`);
                  },
                  (err) => {
                    console.error(`Error fetching createdBy details for message ID: ${message.id}`, err);
                  }
                );
              }
            });
          }

       
        });

        console.log('Tickets with user details:', this.tickets);
      },
      error: (err) => {
        console.error('Error fetching tickets:', err);
      },
    });
  }

  applyFilters() {
    this.filteredTickets = this.tickets.filter(ticket =>
      (this.categoryFilter === null || ticket.categoryId === this.categoryFilter) &&
      (this.statusFilter === null || ticket.statusId === this.statusFilter) &&
      (this.priorityFilter === null || ticket.priorityId === this.priorityFilter)
      
    );
    this.getTickets();
  }

  user: User | null = null;
  userId: number = 1; 
  getUserDetails(): void {
    this.ticketService.getUserById(this.userId).subscribe(
      (data: User) => {
        this.user = data;
        console.log(this.user);  // You can display this data in your template
      },
      (error) => {
        console.error('Error fetching user:', error);
      }
    );
  }

  getMessages(ticketId: number): void {
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

  statusOptions = [
    { id: 1, name: 'Open' },
    { id: 2, name: 'In Progress' },
    { id: 3, name: 'Resolved' },
    { id: 4, name: 'Closed' },
  ];  dropdownVisible: { [key: number]: boolean } = {}; // Track dropdown visibility per ticket

  toggleDropdown(ticketId: number): void {
    this.dropdownVisible = {
      ...this.dropdownVisible,
      [ticketId]: !this.dropdownVisible[ticketId]
    };
  }
  updateStatus(ticketId: number, statusId: number): void {
    if(this.userRole == 'SupportAgent') {
    this.ticketService.updateTicketStatus(ticketId, statusId).subscribe({
      next: (response) => {
        console.log('Status updated successfully:', response);
        this.dropdownVisible[ticketId] = false; // Hide the dropdown
        const ticket = this.tickets.find((t) => t.id === ticketId);
        if (ticket) {
          ticket.statusId = statusId;
          ticket.status = this.getStatusName(statusId); // Update the status text
        }
        this.getTickets();

      },
      error: (err) => {
        console.error('Error updating status:', err);
        console.log(err);
        this.getTickets();

      }
    });}
   else{
    console.error('Only SupportAgent can update status');
    alert('Only SupportAgent can update status');
   }
  }

  // Helper method to get status name from statusId
  getStatusName(statusId: number): string {
    const statusMap: { [key: number]: string } = {
      1: 'Open',
      2: 'In Progress',
      3: 'Running',
      4: 'Closed',
    };
    return statusMap[statusId] || 'Unknown';
  }

  getUserRoleFromToken(): string {
    // Replace this with your actual logic to decode the JWT token
    const token = localStorage.getItem('token');
    if (token) {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload.role || '';
    }
    return '';
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

  showModal2: boolean = false;
  openModal2(ticketId: number): void {
    this.selectedTicket = this.tickets.find(ticket => ticket.id === ticketId);

    this.showModal2 = true;
  }

  closeModal2(): void {
    this.showModal2 = false;
  }
}
