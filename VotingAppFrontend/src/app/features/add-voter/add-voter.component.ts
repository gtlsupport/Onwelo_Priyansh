import { Component } from '@angular/core';
import { HomeService } from '../services/home.service';
import { Voter } from '../models/voter.model';
import { FormsModule } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-voter',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './add-voter.component.html',
  styleUrl: './add-voter.component.css'
})
export class AddVoterComponent {

  name!: string;
  constructor(private homeService: HomeService, private modalService: NgbModal) { }

  onFormSubmit(){
    const data: Voter = {
      id: 0,
      name: this.name,
      hasVoted: false
    }

    if(data.name != null){
      this.homeService.addVoter(data).subscribe({
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
