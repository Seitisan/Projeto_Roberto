import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { NavComponent } from './nav/nav.component';
import { PesquisaComponent } from './pesquisa/pesquisa.component';
import { ControllerService } from './_Services/controller.service';
import { HttpClientModule } from '@angular/common/http';
import { CadastroPesquisaSucessoComponent } from './cadastros/cadastro-pesquisa-sucesso/cadastro-pesquisa-sucesso.component';
import { CadastroPesquisaComponent } from './cadastros/cadastro-pesquisa/cadastro-pesquisa.component';
import { AlertifyService } from './_Services/alertify.service';
import { PesquisaResultadoComponent } from './pesquisaResultado/pesquisaResultado.component';

@NgModule({
   declarations: [
      AppComponent,
      HomeComponent,
      NavComponent,
      PesquisaComponent,
      CadastroPesquisaComponent,
      CadastroPesquisaSucessoComponent,
      PesquisaResultadoComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      FormsModule,
      ReactiveFormsModule,
      HttpClientModule
   ],
   providers: [
      ControllerService,
      AlertifyService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
