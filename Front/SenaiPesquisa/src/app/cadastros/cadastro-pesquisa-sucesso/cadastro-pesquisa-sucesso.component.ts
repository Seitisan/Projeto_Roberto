import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-cadastro-pesquisa-sucesso',
  templateUrl: './cadastro-pesquisa-sucesso.component.html',
  styleUrls: ['./cadastro-pesquisa-sucesso.component.css']
})
export class CadastroPesquisaSucessoComponent implements OnInit {
  id: string;

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.id = this.route.snapshot.queryParamMap.get('codigo');
  }

}
