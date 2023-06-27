import { TeamAnswerModel } from "./team-answer-model";

export interface RoundAnswersOfTeamModel {
    teamName: string,
    roundNumber: number,
    answers: Array<TeamAnswerModel>
}
