import { Component, OnDestroy, OnInit } from '@angular/core';
import { GameProcessStateModel } from 'src/app/models/game-process-state-model';
import { GameProcessService } from 'src/app/services/game-process.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.scss']
})
export class GameComponent implements OnInit,OnDestroy {

  gameProcessState: GameProcessStateModel = {roundNumber: 0, questionNumber: 0, isGameStarted: false}

  constructor(private gps: GameProcessService){}

  async ngOnInit() {
    this.gps.gameProcessState.subscribe({
      next: gp => this.gameProcessState = gp
    })

    await this.gps.getActualGameProcess()

    this.gps.listenForGameProcess()
  }

  ngOnDestroy(): void {
    this.gps.hc.off('GetGameProcessState')
  }
}
