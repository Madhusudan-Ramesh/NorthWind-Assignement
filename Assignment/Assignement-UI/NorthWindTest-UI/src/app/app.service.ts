import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

export interface TokenKey{
    token : string
}

@Injectable( {
    providedIn: 'root'
})


export class AppService {

  

    constructor(private httpClient: HttpClient) { }

    public getEmployeesSales(): Observable<any[]>{
        debugger;
        let token = sessionStorage.getItem('token');
        return this.httpClient.get<any[]>("https://localhost:7085/api/EmolyeesSalesData",{
            headers: new HttpHeaders({
                Authorization: `Bearer ${token}`
            })
        })
    }

    public getToken(loginData: any): Observable<TokenKey>{
        debugger;
        let url = "https://localhost:7085/api/authenticate";
        return this.httpClient.post<TokenKey>(url, loginData);
      }

}
