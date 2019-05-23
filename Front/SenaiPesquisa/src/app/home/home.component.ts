import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertifyService } from '../_Services/alertify.service';
import { ControllerService } from '../_Services/controller.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  pesquisaCadastroForm: FormGroup;

  constructor(private fb: FormBuilder, private router: Router, private alertify: AlertifyService,
    private controller: ControllerService) { }

  ngOnInit() {
    this.createForm();
  }

  createForm() {
    this.pesquisaCadastroForm = this.fb.group({
      id: ['', Validators.required],
      turma: ['', Validators.required]
    });
  }

  pesquisa() {
    if (this.pesquisaCadastroForm.valid) {
      this.controller.getCadastroPesquisa(this.pesquisaCadastroForm.get('id').value,
        this.pesquisaCadastroForm.get('turma').value).subscribe(() => {
          this.router.navigate(['/pesquisa'], {
            queryParams: {
              codigo: this.pesquisaCadastroForm.get('id').value,
              turma: this.pesquisaCadastroForm.get('turma').value
            }
          });
        }, error => {
            this.alertify.error('codigo ou turma incorretos');
      });
    }
  }
}
