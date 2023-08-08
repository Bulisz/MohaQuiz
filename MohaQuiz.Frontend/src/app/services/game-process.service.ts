import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HubConnection } from '@microsoft/signalr';
import { BehaviorSubject, firstValueFrom } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GameProcessStateModel } from '../models/game-process-state-model';

@Injectable({
  providedIn: 'root'
})
export class GameProcessService {

  BASE_URL = environment.apiUrl + 'gameprocess/'
  gameProcessState = new BehaviorSubject<GameProcessStateModel>({ gameName: '', roundNumber: 0, questionNumber: 0, isGameStarted: false, isScoring: false, isGameFinished: false })

  constructor(private http: HttpClient, public hc: HubConnection, private router: Router) {
    this.hc.start()
  }

  async startGame(gameName: string): Promise<any> {
    return await firstValueFrom(this.http.get(`${this.BASE_URL}startgame/${gameName}`))
  }

  async nextQuestion() {
    await firstValueFrom(this.http.get(`${this.BASE_URL}nextquestion`))
  }

  async startScoring() {
    await firstValueFrom(this.http.get(`${this.BASE_URL}startscoring`))
  }

  async stopScoring() {
    await firstValueFrom(this.http.get(`${this.BASE_URL}stopscoring`))
  }

  async nextRound() {
    await firstValueFrom(this.http.get(`${this.BASE_URL}nextround`))
  }

  async getActualGameProcess() {
    await firstValueFrom(this.http.get<GameProcessStateModel>(`${this.BASE_URL}getactualgameprocess`))
      .then(gp => this.gameProcessState.next(gp))
  }

  async resetGame() {
    await firstValueFrom(this.http.get(`${this.BASE_URL}resetgame`))
  }

  listenForStartGameMessage() {
    this.hc.on('StartGame', mes => {
      if (mes === 'StartGame') {
        this.router.navigate(['game'])
      }
    })
  }

  stopListenForStartGameMessage() {
    this.hc.off('StartGame')
  }

  listenForGameProcess() {
    this.hc.on('GetGameProcessState', gp => {
      this.gameProcessState.next(gp)
    })
  }

  stopListenForGameProcess() {
    this.hc.off('GetGameProcessState')
  }
}
