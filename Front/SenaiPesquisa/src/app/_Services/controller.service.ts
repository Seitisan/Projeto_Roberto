import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ControllerService {
  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

  registerCadastrarPesquisa(model: any) {
    return this.http.post(this.baseUrl + '/cadastrar', model);
  }

  registerPesquisa(model: any) {
    return this.http.post(this.baseUrl, model);
  }

  getCadastroPesquisa(id: number, turma: string) {
    return this.http.get(this.baseUrl + '/cadastro?id=' + id + '&turma=' + turma);
  }
}
