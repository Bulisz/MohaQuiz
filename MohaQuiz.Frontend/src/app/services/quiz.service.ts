import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { TeamNameModel } from '../models/team-name-model';
import { BehaviorSubject, firstValueFrom } from 'rxjs';
import { GameProcessService } from './game-process.service';
import { RoundDetailsModel } from '../models/round-details-model';
import { TeamAnswerModel } from '../models/team-answer-model';
import { RoundAndTeamModel } from '../models/round-and-team-model';
import { RoundAnswersOfTeamModel } from '../models/round-answers-of-team-model';
import { ScoringModel } from '../models/scoring-model';
import { TeamScoreSummaryModel } from '../models/team-score-summary-model';
import { GameSummaryModel } from '../models/game-summary-model';
import { RecordRoundModel } from '../models/record-round-model';
import { RoundOfGameModel } from '../models/round-of-game-model';
import { TeamAndGameModel } from '../models/team-and-game-model';
import { GameNameModel } from '../models/game-name-model';

@Injectable({
  providedIn: 'root'
})
export class QuizService {

  BASE_URL = environment.apiUrl +'quiz/'
  teamName = new BehaviorSubject<string | null>(null)

  constructor(private http: HttpClient, private gps: GameProcessService) {
  }

  async createTeam(teamName: TeamNameModel) {
    await firstValueFrom(this.http.post<TeamNameModel>(`${this.BASE_URL}createteam`,teamName))
      .then(res => {
        localStorage.setItem('teamName',res.teamName)
        this.teamName.next(res.teamName)
      })
      this.gps.listenForStartGameMessage()
  }

  async isTeamCreated(teamName: string) {
    await firstValueFrom(this.http.get<TeamNameModel>(`${this.BASE_URL}isteamcreated/${teamName}`))
      .then(res => {
        if(res){
          localStorage.setItem('teamName',res.teamName)
          this.teamName.next(res.teamName)
        } else {
          localStorage.removeItem('teamName')
        }
      })
      this.gps.listenForStartGameMessage()
  }

  async getAllTeamNames(): Promise<Array<string>> {
    return await firstValueFrom(this.http.get<Array<string>>(`${this.BASE_URL}getallteamnames`))
  }

  async getRoundDetails(model: RoundOfGameModel): Promise<RoundDetailsModel> {
    return await firstValueFrom(this.http.post<RoundDetailsModel>(`${this.BASE_URL}getrounddetails`,model))
  }

  async sendAnswer(teamAnswer: TeamAnswerModel) {
    await firstValueFrom(this.http.post((`${this.BASE_URL}sendanswer`),teamAnswer))
  }

  async getRoundAnswersOfTeam(roundAndTeam: RoundAndTeamModel): Promise<RoundAnswersOfTeamModel> {
    return await firstValueFrom(this.http.post<RoundAnswersOfTeamModel>(`${this.BASE_URL}getroundanswersofteam`,roundAndTeam))
  }

  async scoringOfAQuestion(score: ScoringModel) {
    await firstValueFrom(this.http.post((`${this.BASE_URL}scoringofaquestion`),score))
  }

  async randomizeTeamNames() {
    await firstValueFrom(this.http.get(`${this.BASE_URL}randomizeteamnames`))
  }

  async getSummaryOfTeam(model: TeamAndGameModel): Promise<TeamScoreSummaryModel> {
    return await firstValueFrom(this.http.post<TeamScoreSummaryModel>(`${this.BASE_URL}getsummaryofteam`,model))
  }

  async getSummaryOfGame(model: GameNameModel): Promise<Array<GameSummaryModel>> {
    return await firstValueFrom(this.http.post<Array<GameSummaryModel>>(`${this.BASE_URL}getsummaryofgame`,model))
  }

  async resetGame() {
    await firstValueFrom(this.http.get(`${this.BASE_URL}resetgame`))
  }

  async recordRound(round: RecordRoundModel) {
    await firstValueFrom(this.http.post((`${this.BASE_URL}recordround`),round))
  }

  async getRoundTypes(): Promise<Array<string>> {
    return await firstValueFrom(this.http.get<Array<string>>((`${this.BASE_URL}getroundtypes`)))
  }

  async createGame(model: GameNameModel) {
    await firstValueFrom(this.http.post(`${this.BASE_URL}creategame`,model))
  }

  async getAllGameNames(): Promise<Array<string>> {
    return await firstValueFrom(this.http.get<Array<string>>((`${this.BASE_URL}getallgamenames`)))
  }
}
