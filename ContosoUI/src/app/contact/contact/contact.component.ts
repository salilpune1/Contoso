import { Component, OnInit , Inject} from '@angular/core';
import {Router} from "@angular/router";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {first} from "rxjs/operators";
import {Contact} from "../../model/contact.model";
import {ApiService} from "../../service/api.service";
import { ReturnStatement } from '@angular/compiler';

@Component({
  selector: 'app-edit-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {

  contact: Contact;
  editForm: FormGroup;
  contactId: string;

  //TODO this can be filled using ngservice. Enum values from APIs can be filled. 
  //Any change in API side will be automatically reflected here
  statuslist = [
    'Active',  'Inactive'   
  ];

  constructor(private formBuilder: FormBuilder,private router: Router, private apiService: ApiService) { }

  ngOnInit() {
    this.contactId  = window.localStorage.getItem("editContactId");
    if(!this.contactId) {
      alert("Invalid action.")
      this.router.navigate(['list-contact']);
      return;
    }
    this.editForm = this.formBuilder.group({
      id: ['0'],      
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]  ,
      phoneNumber: [''],
      statusDisplay: [this.statuslist[0]],
      rowVersion: [''],
      status: ['0']
    });

    if(+this.contactId != 0) {
      this.apiService.getContactById(+this.contactId)
      .subscribe( data => {
        this.editForm.patchValue(data);
      });
    }
    
  }
  get status() {
    return this.editForm.get('statusDisplay');
  }

  get f() { return this.editForm.controls; }

  changeStatus(e) {
   
    this.status.setValue(e.target.value, {
      onlySelf: true
      
    })
    this.editForm.value.status = e.target.selectedIndex
  }

  onSubmit() {
    if (this.editForm.invalid)
    {
      return;
    }
    if(+this.contactId != 0) {
      this.apiService.updateContact(this.editForm.value)
      .pipe(first())
      .subscribe(
        data => {
          if(data) {
            alert('Contact updated successfully.');
            this.router.navigate(['list-contact']);
          }else {
            alert(data.message);
          }
        },
        error => {
          alert(error);
        });
  }
 
else {
        this.apiService.createContact(this.editForm.value)
        .subscribe( data => {
          this.router.navigate(['list-contact']);
        });
    }
  } 

}
