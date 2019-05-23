import { Docente } from './docente';

export class CadastroPesquisa {

    id: number;

    tipoCurso: string;

    curso: string;

    turma: string;

    quantidadeAluno: number;

    coordenador: string;

    docentes: Docente[];

}
