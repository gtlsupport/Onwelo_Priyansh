import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Candidate } from '../models/candidate.model';
import { environment } from '../../../environments/environment.development';
import { Observable } from 'rxjs';
import { Voter } from '../models/voter.model';
import { Vote } from '../models/vote.model';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  addCandidateOpen: boolean = false;

  constructor(private http: HttpClient) { }

  getCandidates(pageNumber?: number, pageSize?: number) : Observable<Candidate[]>{

    let params = new HttpParams();
    if(pageNumber){
      params = params.set('pageNumber', pageNumber);
    }

    if(pageSize){
      params = params.set('pageSize', pageSize);
    }
    return this.http.get<Candidate[]>(`${environment.apiBaseUrl}/api/Candidate` ,{params: params});
  }

  addCandidates(data: Candidate): Observable<Candidate>{
    return this.http.post<Candidate>(`${environment.apiBaseUrl}/api/Candidate`, data);
  }

  getCandidateTotal() : Observable<number>{
    return this.http.get<number>(`${environment.apiBaseUrl}/api/Candidate/Count`)
  }

  getVoters(pageNumber?: number, pageSize?: number): Observable<Voter[]>{
    let params = new HttpParams();
    if(pageNumber){
      params = params.set('pageNumber', pageNumber);
    }

    if(pageSize){
      params = params.set('pageSize', pageSize);
    }
    return this.http.get<Voter[]>(`${environment.apiBaseUrl}/api/Voter`, {params: params})
  }

  getNonVotedVoter(): Observable<Voter[]>{
    return this.http.get<Voter[]>(`${environment.apiBaseUrl}/api/Voter/nonvoted`)
  }

  getVoterTotal(): Observable<number>{
    return this.http.get<number>(`${environment.apiBaseUrl}/api/Voter/Count`)
  }
  addVoter(data: Voter): Observable<Voter>{
    return this.http.post<Voter>(`${environment.apiBaseUrl}/api/voter`, data)
  }

  addVote(data: Vote): Observable<Vote>{
    return this.http.post<Vote>(`${environment.apiBaseUrl}/api/vote`, data)
  }
    
  }
