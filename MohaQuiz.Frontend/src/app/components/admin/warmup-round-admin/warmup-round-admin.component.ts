import { Component, OnInit } from '@angular/core';
import { RoundDetailsModel } from 'src/app/models/round-details-model';
import { QuizService } from 'src/app/services/quiz.service';

@Component({
  selector: 'app-warmup-round-admin',
  templateUrl: './warmup-round-admin.component.html',
  styleUrls: ['./warmup-round-admin.component.scss']
})
export class WarmupRoundAdminComponent implements OnInit {

  roundDetails!: RoundDetailsModel
  actualQuestion = 0
  isScoring = false

  constructor(private qs: QuizService){
  }

  async ngOnInit() {
    this.roundDetails = await this.qs.GetRoundDetails(1)
  }
 
  nextQuestion(){
    this.actualQuestion++
  }

  scoring(){

  }

  nextRound(){

  }
}
