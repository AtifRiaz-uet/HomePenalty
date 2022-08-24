import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { catchError, map, tap } from 'rxjs/operators';
import { Observable, Subject } from 'rxjs';
import { dateModel } from '../Model/dates';

@Injectable({
  providedIn: 'root'
})
export class ConnectionService {

    private _url: string = "https://localhost:44380/api/PenaltyCalculator/GetCountriesData";
    private _urlPost: string = "https://localhost:44380/api/PenaltyCalculator/Post";
    newDates:dateModel;


    constructor(private http: HttpClient) { }

    getCountriesName(): Observable<String[]> {

        return this.http.get<String[]>(this._url)

    }

    
    
    postDates(checkIn:any,returnDate:any):Observable<number>

    {
      this.newDates={
        checkIn:checkIn,
        checkOut:returnDate
      }
      return this.http.post<number>(this._urlPost,this.newDates);

    }
}
