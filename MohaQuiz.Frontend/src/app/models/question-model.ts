import { CorrectAnswerModel } from "./correct-answer-model";

export interface QuestionModel {
    questionNumber: number,
    fullScore: number,
    questionText: string,
    correctAnswers: Array<CorrectAnswerModel>,
}
