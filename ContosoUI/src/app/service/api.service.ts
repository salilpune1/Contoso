import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Contact} from "../model/contact.model";
import {Observable} from "rxjs/index";
import {ApiResponse} from "../model/api.response";

@Injectable()
export class ApiService {

  constructor(private http: HttpClient) { }
  baseUrl: string = 'http://localhost:44341/api/contact/';

  login(loginPayload) : Observable<ApiResponse> {
    return this.http.post<ApiResponse>('http://localhost:44341/' + 'token/generate-token', loginPayload);
  }

  getContacts() : Observable<Contact[]> {
    return this.http.get<Contact[]>(this.baseUrl);
  }

  getContactById(id: number): Observable<Contact> {
    return this.http.get<Contact>(this.baseUrl + id);
  }

  createContact(contact: Contact): Observable<ApiResponse> {
    return this.http.post<ApiResponse>(this.baseUrl, contact);
  }

  updateContact(contact: Contact): Observable<ApiResponse> {
    return this.http.put<ApiResponse>(this.baseUrl + contact.id, contact);
  }

  deleteContact(id: number): Observable<ApiResponse> {
    return this.http.delete<ApiResponse>(this.baseUrl + id);
  }
}
