import { Component, OnInit } from '@angular/core';
import { HomeService } from '../services/home.service';
import { Candidate } from '../models/candidate.model';
import { FormBuilder, FormGroup, FormsModule } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-candidate',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './add-candidate.component.html',
  styleUrl: './add-candidate.component.css'
})
export class AddCandidateComponent{

  name!: string;
  addCandidateClose = this.homeServices.addCandidateOpen;

  constructor(private homeServices: HomeService, private modalService: NgbModal) { }

  onFormSubmit(){
    const data: Candidate = {
      id: 0,
      name: this.name,
      votesCount: 0
    }

    if(data.name != null){
      this.homeServices.addCandidates(data).subscribe({
        next: (response) =>{
          this.onClose();
          window.location.reload();
        }
      })
    }
    
  }

  onClose() {
    this.modalService.dismissAll();
  }
}
