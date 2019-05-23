import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../_Services/alertify.service';
import { CadastroPesquisa } from '../_models/cadastroPesquisa';
import { ControllerService } from '../_Services/controller.service';
import { Pesquisa } from '../_models/pesquisa';
import { Resposta } from '../_models/resposta';

@Component({
  selector: 'app-pesquisa',
  templateUrl: './pesquisa.component.html',
  styleUrls: ['./pesquisa.component.css']
})
export class PesquisaComponent implements OnInit {
  pesquisaForm: FormGroup;
  pagina: number;
  firstPage: boolean;
  secondPage: boolean;
  thirdPage: boolean;
  fourthPage: boolean;
  fifthPage: boolean;
  lastPage: boolean;
  cadastroPesquisa: CadastroPesquisa = new CadastroPesquisa();
  pesquisa: Pesquisa = new Pesquisa();
  resposta: Resposta = new Resposta();

  constructor(private controller: ControllerService,
      private fb: FormBuilder, private router: Router, private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.carregaDados();
    console.log(this.cadastroPesquisa);
    this.pagina = 1;
    this.loadContent(this.pagina);
    this.createPesquisaForm();
  }

  createPesquisaForm() {
    this.pesquisaForm = this.fb.group({
      importancia1: ['', Validators.required],
      importancia2: ['', Validators.required],
      importancia3: ['', Validators.required],
      importancia4: ['', Validators.required],
      importancia5: ['', Validators.required],
      importancia6: ['', Validators.required],
      importancia7: ['', Validators.required],
      importancia8: ['', Validators.required],
      importancia9: ['', Validators.required],
      resposta1: ['', Validators.required],
      resposta2: ['', Validators.required],
      resposta3: ['', Validators.required],
      resposta4: ['', Validators.required],
      resposta5: ['', Validators.required],
      resposta6: ['', Validators.required],
      resposta7: ['', Validators.required],
      resposta8: ['', Validators.required],
      resposta9: ['', Validators.required],
      resposta1docente1: ['', Validators.required],
      resposta1docente2: ['', Validators.required],
      resposta1docente3: ['', Validators.required],
      resposta1docente4: ['', Validators.required],
      resposta1docente5: ['', Validators.required],
      resposta1docente6: ['', Validators.required],
      resposta1docente7: ['', Validators.required],
      resposta2docente1: ['', Validators.required],
      resposta2docente2: ['', Validators.required],
      resposta2docente3: ['', Validators.required],
      resposta2docente4: ['', Validators.required],
      resposta2docente5: ['', Validators.required],
      resposta2docente6: ['', Validators.required],
      resposta2docente7: ['', Validators.required],
      resposta3docente1: ['', Validators.required],
      resposta3docente2: ['', Validators.required],
      resposta3docente3: ['', Validators.required],
      resposta3docente4: ['', Validators.required],
      resposta3docente5: ['', Validators.required],
      resposta3docente6: ['', Validators.required],
      resposta3docente7: ['', Validators.required],
      resposta4docente1: ['', Validators.required],
      resposta4docente2: ['', Validators.required],
      resposta4docente3: ['', Validators.required],
      resposta4docente4: ['', Validators.required],
      resposta4docente5: ['', Validators.required],
      resposta4docente6: ['', Validators.required],
      resposta4docente7: ['', Validators.required],
      resposta1coordenador: ['', Validators.required],
      resposta2coordenador: ['', Validators.required],
      resposta3coordenador: ['', Validators.required],
      resposta4coordenador: ['', Validators.required],
      comentario: [''],
      aluno: ['']
    });
  }

  loadContent(page) {
    switch (page) {
      case 0: {
        this.router.navigate(['']);
        break;
      }
      case 1: {
        this.firstPage = true;
        this.secondPage = false;
        this.thirdPage = false;
        this.fourthPage = false;
        this.fifthPage = false;
        this.lastPage = false;
        break;
      }
      case 2: {
        this.firstPage = false;
        this.secondPage = true;
        this.thirdPage = false;
        this.fourthPage = false;
        this.fifthPage = false;
        this.lastPage = false;
        break;
      }
      case 3: {
        this.firstPage = false;
        this.secondPage = false;
        this.thirdPage = true;
        this.fourthPage = false;
        this.fifthPage = false;
        this.lastPage = false;
        break;
      }
      case 4: {
        this.firstPage = false;
        this.secondPage = false;
        this.thirdPage = false;
        this.fourthPage = true;
        this.fifthPage = false;
        this.lastPage = false;
        break;
      }
      case 5: {
        this.firstPage = false;
        this.secondPage = false;
        this.thirdPage = false;
        this.fourthPage = false;
        this.fifthPage = true;
        this.lastPage = false;
        break;
      }
      case 6: {
        this.firstPage = false;
        this.secondPage = false;
        this.thirdPage = false;
        this.fourthPage = false;
        this.fifthPage = false;
        this.lastPage = true;
        break;
      }
      default: {
        this.firstPage = true;
        this.secondPage = false;
        this.thirdPage = false;
        this.fourthPage = false;
        this.fifthPage = false;
        this.lastPage = false;
        break;
      }
    }
  }

  proximaPagina() {
    this.pagina++;
    this.loadContent(this.pagina);
  }

  anteriorPagina() {
    this.pagina--;
    this.loadContent(this.pagina);
  }

  carregaDados() {
    this.controller.getCadastroPesquisa(parseInt(this.route.snapshot.queryParamMap.get('codigo'), 16),
      this.route.snapshot.queryParamMap.get('turma')).subscribe((res: CadastroPesquisa) => {
        this.cadastroPesquisa.id = res.id;
        this.cadastroPesquisa.curso = res.curso;
        this.cadastroPesquisa.turma = res.turma;
        this.cadastroPesquisa.coordenador = res.coordenador;
        this.cadastroPesquisa.docentes = res.docentes;
      }, error => {
          this.alertify.error('Código e/ou turma incorretos');
          this.router.navigate(['']);
        });
  }

  register() {
    if (this.pesquisaForm.valid) {
      this.popularPesquisa();
      this.controller.registerPesquisa(this.pesquisa).subscribe(() => {
        this.alertify.success('Pesquisa cadastrada com sucesso!');
        this.router.navigate(['/']);
      }, error => {
        this.alertify.error('Algo Deu errado, tente novamente!');
      });
    }
  }

  popularPesquisa() {
    this.pesquisa.comentario = this.pesquisaForm.get('comentario').value;
    this.pesquisa.idCadastroPesquisa = this.cadastroPesquisa.id;
    this.pesquisa.nomeAluno = this.pesquisaForm.get('aluno').value;
    this.pesquisa.respostas = [];
    this.populaRespostaGeral();
  }

  populaRespostaGeral() {
    // Resposta sobre o ambiente em geral
    for (let i = 0; i < 9; i++) {
      this.resposta.idPergunta = i;
      this.resposta.valorImportancia = parseInt(this.pesquisaForm.get('importancia' + (i + 1)).value, 16);
      this.resposta.valorResposta = parseInt(this.pesquisaForm.get('resposta' + (i + 1)).value, 16);
      this.pesquisa.respostas.push(this.resposta);
      this.resposta = null;
      this.resposta = new Resposta();
    }
    // Resposta sobre os docentes da turma
    let contador = 1;
    for (let i = 9; i < 13; i++) {
      for (let j = 0; j < 7; j++) {
        this.resposta = null;
        this.resposta = new Resposta();
        this.resposta.idPergunta = i;
        this.resposta.idDocente = this.cadastroPesquisa.docentes[j].id;
        this.resposta.docenteMateria = this.cadastroPesquisa.docentes[j].materia;
        this.resposta.valorResposta = this.resposta.valorResposta =
          parseInt(this.pesquisaForm.get('resposta' + contador + 'docente' + (j + 1)).value, 16);
        this.pesquisa.respostas.push(this.resposta);
      }
      contador++;
    }
    // Resposta sobre o coordenador da turma
    contador = 1;
    for (let i = 13; i < 17; i++) {
      this.resposta = null;
      this.resposta = new Resposta();
      this.resposta.coordenador = this.cadastroPesquisa.coordenador;
      this.resposta.idPergunta = i;
      this.resposta.valorResposta = this.resposta.valorResposta =
        parseInt(this.pesquisaForm.get('resposta' + contador + 'coordenador').value, 16);
      this.pesquisa.respostas.push(this.resposta);
      contador++;
    }
  }
}
