import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertifyService } from 'src/app/_Services/alertify.service';
import { Router } from '@angular/router';
import { CadastroPesquisa } from 'src/app/_models/cadastroPesquisa';
import { Docente } from 'src/app/_models/docente';
import { ControllerService } from 'src/app/_Services/controller.service';

@Component({
  selector: 'app-cadastro-pesquisa',
  templateUrl: './cadastro-pesquisa.component.html',
  styleUrls: ['./cadastro-pesquisa.component.css']
})
export class CadastroPesquisaComponent implements OnInit {
  cadastroTurmaMateriaDocente: FormGroup;
  cadastroPesquisa: CadastroPesquisa = new CadastroPesquisa();
  docente: Docente = new Docente();

  constructor(private controller: ControllerService,
    private fb: FormBuilder, private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
    this.createForm();
  }

  createForm() {
    this.cadastroTurmaMateriaDocente = this.fb.group({
      tipoCurso: ['', Validators.required],
      curso: ['', Validators.required],
      turma: ['', Validators.required],
      coordenador: ['', Validators.required],
      qtdAluno: ['', Validators.required],
      materia1: ['', Validators.required],
      docente1: ['', Validators.required],
      materia2: ['', Validators.required],
      docente2: ['', Validators.required],
      materia3: ['', Validators.required],
      docente3: ['', Validators.required],
      materia4: ['', Validators.required],
      docente4: ['', Validators.required],
      materia5: ['', Validators.required],
      docente5: ['', Validators.required],
      materia6: ['', Validators.required],
      docente6: ['', Validators.required],
      materia7: ['', Validators.required],
      docente7: ['', Validators.required]
    });
  }

  register() {
    if (this.cadastroTurmaMateriaDocente.valid) {
      this.mapper();
      this.controller.registerCadastrarPesquisa(this.cadastroPesquisa).subscribe((res: number) => {
        this.alertify.success('Cadastro de dados para pesquisa realizado com sucesso');
        this.router.navigate(['/cadastro/pesquisa/sucesso'], { queryParams: { codigo: res } });
      }, error => {
        this.alertify.error('Falha ao Cadastrar dados Pesquisa');
      });
    }
  }

  mapper() {
    this.cadastroPesquisa.tipoCurso = this.cadastroTurmaMateriaDocente.get('tipoCurso').value;
    this.cadastroPesquisa.curso = this.cadastroTurmaMateriaDocente.get('curso').value;
    this.cadastroPesquisa.quantidadeAluno = this.cadastroTurmaMateriaDocente.get('qtdAluno').value;
    this.cadastroPesquisa.coordenador = this.cadastroTurmaMateriaDocente.get('coordenador').value;
    this.cadastroPesquisa.turma = this.cadastroTurmaMateriaDocente.get('turma').value;
    this.cadastroPesquisa.docentes = [];
    for (let i = 1; i < 8; i++) {
      this.docente.nomeDocente = this.cadastroTurmaMateriaDocente.get('docente' + i).value;
      this.docente.materia = this.cadastroTurmaMateriaDocente.get('materia' + i).value;
      this.cadastroPesquisa.docentes.push(this.docente);
      this.docente = null;
      this.docente = new Docente();
    }
  }
}
