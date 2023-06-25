import { QuestionModel } from "./question-model";
import { RoundTypeModel } from "./round-type-model";

export interface RoundDetailsModel {
    roundNumber: number,
    roundName: string,
    roundType: RoundTypeModel,
    questions: Array<QuestionModel>
}
