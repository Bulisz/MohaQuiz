import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class GameProcessService {

  BASE_URL = environment.apiUrl +'gameprocess/'

  constructor(private http: HttpClient) {
  }
}
