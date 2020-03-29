import { Component, OnInit , Inject} from '@angular/core';
import {Router} from "@angular/router";
import {Contact} from "../../model/contact.model";
import {ApiService} from "../../service/api.service";

@Component({
  selector: 'app-list-contact',
  templateUrl: './list-contact.component.html',
  styleUrls: ['./list-contact.component.css']
})
export class ListContactComponent implements OnInit {

  contacts: Contact[];

  constructor(private router: Router, private apiService: ApiService) { }

  ngOnInit() {
    
    this.apiService.getContacts()
      .subscribe( data => {        
        this.contacts = data;
      });
  }

  deleteContact(contact: Contact): void {
    this.apiService.deleteContact(contact.id)
      .subscribe( data => {
        this.contacts = this.contacts.filter(u => u !== contact);
      })
  };

  editContact(contact: Contact): void {
    window.localStorage.removeItem("editContactId");
    window.localStorage.setItem("editContactId", contact.id.toString());
    this.router.navigate(['contact']);
  };

  addContact(): void {
    window.localStorage.removeItem("editContactId");
    window.localStorage.setItem("editContactId", '0');
    this.router.navigate(['contact']);
  };
}
