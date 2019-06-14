import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertifyService } from '../_Services/alertify.service';
import { ControllerService } from '../_Services/controller.service';
import { saveAs } from 'file-saver';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-pesquisaResultado',
  templateUrl: './pesquisaResultado.component.html',
  styleUrls: ['./pesquisaResultado.component.css']
})
export class PesquisaResultadoComponent implements OnInit {
  geraCSV: FormGroup;

  constructor(private fb: FormBuilder, private router: Router, private alertify: AlertifyService,
    private controller: ControllerService) { }

  ngOnInit() {
    this.createForm();
  }

  createForm() {
    this.geraCSV = this.fb.group({
      id: ['', Validators.required]
    });
  }

  recupera() {
    if (this.geraCSV.valid) {
      this.controller.geraCSV(this.geraCSV.get('id').value).subscribe(data => {
        saveAs(data, 'PesquisaTurma.csv');
      }, error => {
          this.alertify.error('Ocorreu um erro lamento');
      });
    }
  }
}
