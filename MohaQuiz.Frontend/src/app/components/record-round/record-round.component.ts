import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { RecordRoundModel } from 'src/app/models/record-round-model';
import { QuizService } from 'src/app/services/quiz.service';

@Component({
  selector: 'app-record-round',
  templateUrl: './record-round.component.html',
  styleUrls: ['./record-round.component.scss']
})
export class RecordRoundComponent implements OnInit {

  roundForm: FormGroup;
  roundTypes: Array<string> = []
  selectedType!: string
  trueAnswer = new Array<boolean>(50)

  constructor(private qs: QuizService) {
    this.roundForm = new FormBuilder().group({
      roundName: new FormControl('', Validators.required),

      questionText0: new FormControl(''),
      fullScore0: new FormControl(1, [Validators.min(1), Validators.max(5)]),
      correctAnswerText0: new FormControl(''),

      questionText1: new FormControl('', Validators.required),
      fullScore1: new FormControl(1, [Validators.min(1), Validators.max(5)]),
      correctAnswerText1: new FormControl('', Validators.required),
      correctAnswerText11: new FormControl(''),
      correctAnswerText12: new FormControl(''),
      correctAnswerText13: new FormControl(''),

      questionText2: new FormControl('', Validators.required),
      fullScore2: new FormControl(1, [Validators.min(1), Validators.max(5)]),
      correctAnswerText2: new FormControl('', Validators.required),
      correctAnswerText21: new FormControl(''),
      correctAnswerText22: new FormControl(''),
      correctAnswerText23: new FormControl(''),

      questionText3: new FormControl('', Validators.required),
      fullScore3: new FormControl(1, [Validators.min(1), Validators.max(5)]),
      correctAnswerText3: new FormControl('', Validators.required),
      correctAnswerText31: new FormControl(''),
      correctAnswerText32: new FormControl(''),
      correctAnswerText33: new FormControl(''),

      questionText4: new FormControl('', Validators.required),
      fullScore4: new FormControl(1, [Validators.min(1), Validators.max(5)]),
      correctAnswerText4: new FormControl('', Validators.required),
      correctAnswerText41: new FormControl(''),
      correctAnswerText42: new FormControl(''),
      correctAnswerText43: new FormControl(''),

      questionText5: new FormControl('', Validators.required),
      fullScore5: new FormControl(1, [Validators.min(1), Validators.max(5)]),
      correctAnswerText5: new FormControl('', Validators.required),
      correctAnswerText51: new FormControl(''),
      correctAnswerText52: new FormControl(''),
      correctAnswerText53: new FormControl(''),
    })
  }

  async ngOnInit() {
    await this.qs.getRoundTypes()
      .then(res => this.roundTypes = res)
  }

  async onSubmit() {
    let form: RecordRoundModel
    if(this.selectedType === 'Connection'){
      form = {
        roundName: this.roundForm.get('roundName')?.value,
        roundTypeName: this.selectedType,
        questions: [{
          questionNumber: 0,
          questionText: this.roundForm.get('questionText0')?.value,
          fullScore: this.roundForm.get('fullScore0')?.value,
          correctAnswers: [{correctAnswerText: this.roundForm.get('correctAnswerText0')?.value, isCorrect: true}]
        },{
          questionNumber: 1,
          questionText: this.roundForm.get('questionText1')?.value,
          fullScore: this.roundForm.get('fullScore1')?.value,
          correctAnswers: [{correctAnswerText: this.roundForm.get('correctAnswerText1')?.value, isCorrect: true}]
        },{
          questionNumber: 2,
          questionText: this.roundForm.get('questionText2')?.value,
          fullScore: this.roundForm.get('fullScore2')?.value,
          correctAnswers: [{correctAnswerText: this.roundForm.get('correctAnswerText2')?.value, isCorrect: true}]
        },{
          questionNumber: 3,
          questionText: this.roundForm.get('questionText3')?.value,
          fullScore: this.roundForm.get('fullScore3')?.value,
          correctAnswers: [{correctAnswerText: this.roundForm.get('correctAnswerText3')?.value, isCorrect: true}]
        },{
          questionNumber: 4,
          questionText: this.roundForm.get('questionText4')?.value,
          fullScore: this.roundForm.get('fullScore4')?.value,
          correctAnswers: [{correctAnswerText: this.roundForm.get('correctAnswerText4')?.value, isCorrect: true}]
        },{
          questionNumber: 5,
          questionText: this.roundForm.get('questionText5')?.value,
          fullScore: this.roundForm.get('fullScore5')?.value,
          correctAnswers: [{correctAnswerText: this.roundForm.get('correctAnswerText5')?.value, isCorrect: true}]
        }]
      }
      await this.qs.recordRound(form)
        .then(() => this.roundForm.reset())
    } else if(this.selectedType === 'ABCD'){
      form = {
        roundName: this.roundForm.get('roundName')?.value,
        roundTypeName: this.selectedType,
        questions: [{
          questionNumber: 1,
          questionText: this.roundForm.get('questionText1')?.value,
          fullScore: this.roundForm.get('fullScore1')?.value,
          correctAnswers: [{correctAnswerText: this.roundForm.get('correctAnswerText1')?.value, isCorrect: this.trueAnswer[0]},
                          {correctAnswerText: this.roundForm.get('correctAnswerText11')?.value, isCorrect:  this.trueAnswer[1]},
                          {correctAnswerText: this.roundForm.get('correctAnswerText12')?.value, isCorrect:  this.trueAnswer[2]},
                          {correctAnswerText: this.roundForm.get('correctAnswerText13')?.value, isCorrect:  this.trueAnswer[3]}]
        },{
          questionNumber: 2,
          questionText: this.roundForm.get('questionText2')?.value,
          fullScore: this.roundForm.get('fullScore2')?.value,
          correctAnswers: [{correctAnswerText: this.roundForm.get('correctAnswerText2')?.value, isCorrect: this.trueAnswer[10]},
                          {correctAnswerText: this.roundForm.get('correctAnswerText21')?.value, isCorrect:  this.trueAnswer[11]},
                          {correctAnswerText: this.roundForm.get('correctAnswerText22')?.value, isCorrect:  this.trueAnswer[12]},
                          {correctAnswerText: this.roundForm.get('correctAnswerText23')?.value, isCorrect:  this.trueAnswer[13]}]
        },{
          questionNumber: 3,
          questionText: this.roundForm.get('questionText3')?.value,
          fullScore: this.roundForm.get('fullScore3')?.value,
          correctAnswers: [{correctAnswerText: this.roundForm.get('correctAnswerText3')?.value, isCorrect: this.trueAnswer[20]},
                          {correctAnswerText: this.roundForm.get('correctAnswerText31')?.value, isCorrect:  this.trueAnswer[21]},
                          {correctAnswerText: this.roundForm.get('correctAnswerText32')?.value, isCorrect:  this.trueAnswer[22]},
                          {correctAnswerText: this.roundForm.get('correctAnswerText33')?.value, isCorrect:  this.trueAnswer[23]}]
        },{
          questionNumber: 4,
          questionText: this.roundForm.get('questionText4')?.value,
          fullScore: this.roundForm.get('fullScore4')?.value,
          correctAnswers: [{correctAnswerText: this.roundForm.get('correctAnswerText4')?.value, isCorrect: this.trueAnswer[30]},
                          {correctAnswerText: this.roundForm.get('correctAnswerText41')?.value, isCorrect:  this.trueAnswer[31]},
                          {correctAnswerText: this.roundForm.get('correctAnswerText42')?.value, isCorrect:  this.trueAnswer[32]},
                          {correctAnswerText: this.roundForm.get('correctAnswerText43')?.value, isCorrect:  this.trueAnswer[33]}]
        },{
          questionNumber: 5,
          questionText: this.roundForm.get('questionText5')?.value,
          fullScore: this.roundForm.get('fullScore5')?.value,
          correctAnswers: [{correctAnswerText: this.roundForm.get('correctAnswerText5')?.value, isCorrect: this.trueAnswer[40]},
                          {correctAnswerText: this.roundForm.get('correctAnswerText51')?.value, isCorrect:  this.trueAnswer[41]},
                          {correctAnswerText: this.roundForm.get('correctAnswerText52')?.value, isCorrect:  this.trueAnswer[42]},
                          {correctAnswerText: this.roundForm.get('correctAnswerText53')?.value, isCorrect:  this.trueAnswer[43]}]
        }]
      }
      await this.qs.recordRound(form)
      .then(() => this.roundForm.reset())
    } else {
      form = {
        roundName: this.roundForm.get('roundName')?.value,
        roundTypeName: this.selectedType,
        questions: [{
          questionNumber: 1,
          questionText: this.roundForm.get('questionText1')?.value,
          fullScore: this.roundForm.get('fullScore1')?.value,
          correctAnswers: [{correctAnswerText: this.roundForm.get('correctAnswerText1')?.value, isCorrect: true}]
        },{
          questionNumber: 2,
          questionText: this.roundForm.get('questionText2')?.value,
          fullScore: this.roundForm.get('fullScore2')?.value,
          correctAnswers: [{correctAnswerText: this.roundForm.get('correctAnswerText2')?.value, isCorrect: true}]
        },{
          questionNumber: 3,
          questionText: this.roundForm.get('questionText3')?.value,
          fullScore: this.roundForm.get('fullScore3')?.value,
          correctAnswers: [{correctAnswerText: this.roundForm.get('correctAnswerText3')?.value, isCorrect: true}]
        },{
          questionNumber: 4,
          questionText: this.roundForm.get('questionText4')?.value,
          fullScore: this.roundForm.get('fullScore4')?.value,
          correctAnswers: [{correctAnswerText: this.roundForm.get('correctAnswerText4')?.value, isCorrect: true}]
        },{
          questionNumber: 5,
          questionText: this.roundForm.get('questionText5')?.value,
          fullScore: this.roundForm.get('fullScore5')?.value,
          correctAnswers: [{correctAnswerText: this.roundForm.get('correctAnswerText5')?.value, isCorrect: true}]
        }]
      }
      await this.qs.recordRound(form)
        .then(() => this.roundForm.reset())
    }

  }
}
