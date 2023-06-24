import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { QuizService } from 'src/app/services/quiz.service';

@Component({
  selector: 'app-team',
  templateUrl: './team.component.html',
  styleUrls: ['./team.component.scss']
})
export class TeamComponent {

  teamNameForm: FormGroup;

  constructor(private qs: QuizService, private router: Router) {
    this.teamNameForm = new FormBuilder().group({
      teamName: new FormControl('', Validators.required)
    })
  }

  async onSubmit() {
    await this.qs.createTeam(this.teamNameForm.value)
      .then(() => {
        this.router.navigate([''])
      })
      .catch(err => {
        this.teamNameForm.get('teamName')?.setErrors({ServerError: err.error})
      })
  }
}
