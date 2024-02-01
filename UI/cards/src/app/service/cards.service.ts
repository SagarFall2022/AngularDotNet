import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Card } from '../models/card.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CardsService {
  
  baseUrl='https://localhost:7274/api/cards';
  constructor(private http:HttpClient) { }

  //Get all cards
  getAllCards(): Observable<Card []>{
    return this.http.get<Card []>(this.baseUrl);
  }
 //Add a Card
  addCard(card:Card):Observable<Card>{
    card.id='00000000-0000-0000-0000-000000000000';
    return this.http.post<Card>(this.baseUrl,card);
  }

  //Delete a Card
  deleteCard(id:string):Observable<Card> {
    return this.http.delete<Card>(this.baseUrl+'/'+id);
  }
  //Update a Card
  updateCard(card:Card):Observable<Card>{
    return this.http.put<Card>(this.baseUrl+'/'+card.id,card);
  }
}
