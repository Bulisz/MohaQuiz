export interface RecordRoundModel {
    roundTypeName: string,
    roundName: string,
    questions: Array<{
        questionNumber: number,
        questionText: string,
        fullScore: number,
        correctAnswers: Array<{
            correctAnswerText: string,
            isCorrect: boolean
            }>
        }>
}
