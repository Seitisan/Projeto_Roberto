import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PesquisaComponent } from './pesquisa/pesquisa.component';
import { CadastroPesquisaComponent } from './cadastros/cadastro-pesquisa/cadastro-pesquisa.component';
import { CadastroPesquisaSucessoComponent } from './cadastros/cadastro-pesquisa-sucesso/cadastro-pesquisa-sucesso.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'pesquisa', component: PesquisaComponent },
  { path: 'cadastro/pesquisa', component: CadastroPesquisaComponent },
  { path: 'cadastro/pesquisa/sucesso', component: CadastroPesquisaSucessoComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
