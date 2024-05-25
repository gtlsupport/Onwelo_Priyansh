import { Component, OnInit, ViewChild } from '@angular/core';
import { HomeService } from '../services/home.service';
import { Observable } from 'rxjs';
import { Candidate } from '../models/candidate.model';
import { CommonModule } from '@angular/common';
import { Router, RouterModule, RouterOutlet } from '@angular/router';
import { AddCandidateComponent } from '../add-candidate/add-candidate.component';
import { Voter } from '../models/voter.model';
import { FormsModule } from '@angular/forms';
import { Vote } from '../models/vote.model';
import { AddVoterComponent } from '../add-voter/add-voter.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    RouterOutlet,
    AddCandidateComponent,
    FormsModule,
    AddVoterComponent,
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent implements OnInit {
  candidates?: Candidate[] = [];
  candidates$?: Observable<Candidate[]>;
  voters?: Voter[] = [];
  voters$?: Observable<Voter[]>;
  nonVotedVoters?: Voter[] = [];
  addCandidate: boolean = false;
  addVoter: boolean = false;
  candidateId!: number;
  voterId!: number;

  listCandidate: number[] = [];
  totalCountCandidate?: number;
  pageNumberCandidate = 1;
  pageSizeCandidate = 10;

  listVoter: number[] = [];
  totalCountVoter?: number;
  pageNumberVoter = 1;
  pageSizeVoter = 10;

  constructor(
    private homeServices: HomeService,
    private router: Router,
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {
    this.homeServices.getCandidateTotal().subscribe({
      next: (value) => {
        this.totalCountCandidate = value;
        this.listCandidate = new Array(
          Math.ceil(value / this.pageSizeCandidate)
        );
        this.candidates$ = this.homeServices.getCandidates(
          this.pageNumberCandidate,
          this.pageSizeCandidate
        );
      },
    });

    this.homeServices.getCandidates().subscribe({
      next: (response: any) => {
        this.candidates = response;
      },
    });

    this.homeServices.getVoterTotal().subscribe({
      next: (value) => {
        this.totalCountVoter = value;
        this.listVoter = new Array(Math.ceil(value / this.pageSizeVoter));
        this.voters$ = this.homeServices.getVoters(
          this.pageNumberVoter,
          this.pageSizeVoter
        );
      },
    });

    this.homeServices.getVoters().subscribe({
      next: (response: any) => {
        this.voters = response;
      },
    });

    this.homeServices.getNonVotedVoter().subscribe({
      next: (response: any) => {
        this.nonVotedVoters = response;
      },
    });
  }

  transformValue(value: boolean): string {
    return value ? 'V' : 'X';
  }

  openAddCandidate() {
    this.modalService.open(AddCandidateComponent);
  }

  openAddVoter() {
    this.modalService.open(AddVoterComponent);
  }

  onVoteSubmit() {
    const data: Vote = {
      id: 0,
      candidateId: this.candidateId,
      voterId: this.voterId,
    };

    if(data.candidateId && data.voterId){
      this.homeServices.addVote(data).subscribe({
        next: (response: any) => {
          console.log(response);
          window.location.reload();
        },
      });
    }
  }

  getPageVoter(pageNumber: number) {
    this.pageNumberVoter = pageNumber;
    this.voters$ = this.homeServices.getVoters(pageNumber, this.pageSizeVoter);
  }

  getPreviousPageVoter() {
    if (this.pageNumberVoter - 1 < 1) {
      return;
    }
    this.pageNumberVoter -= 1;
    this.voters$ = this.homeServices.getVoters(
      this.pageNumberVoter,
      this.pageSizeVoter
    );
  }

  getNextPageVoter() {
    if (this.pageNumberVoter + 1 > this.listVoter.length) {
      return;
    }
    this.pageNumberVoter += 1;
    this.voters$ = this.homeServices.getVoters(
      this.pageNumberVoter,
      this.pageSizeVoter
    );
  }

  getPageCandidate(pageNumber: number) {
    this.pageNumberCandidate = pageNumber;
    this.candidates$ = this.homeServices.getCandidates(
      pageNumber,
      this.pageSizeCandidate
    );
  }

  getPreviousPageCandidate() {
    if (this.pageNumberCandidate - 1 < 1) {
      return;
    }
    this.pageNumberCandidate -= 1;
    this.candidates$ = this.homeServices.getCandidates(
      this.pageNumberCandidate,
      this.pageSizeCandidate
    );
  }

  getNextPageCandidate() {
    if (this.pageNumberCandidate + 1 > this.listCandidate.length) {
      return;
    }
    this.pageNumberCandidate += 1;
    this.candidates$ = this.homeServices.getCandidates(
      this.pageNumberCandidate,
      this.pageSizeCandidate
    );
  }
}
