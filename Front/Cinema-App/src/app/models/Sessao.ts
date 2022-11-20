import { TipoAnimacao } from "../Enums/TipoAnimacao.enum";
import { TipoAudio } from "../Enums/TipoAudio.enum";
import { Filme } from "./Filme";
import { Sala } from "./Sala";

export interface Sessao {
  id :  number;
  dataSessao :  Date;
  horarioInicial :  Date;
  horarioFinal :  Date;
  valorIngresso :  number;
  tipoAnimacao :  TipoAnimacao;
  tipoAudio :  TipoAudio;
  filmeId :  number;
  filme :  Filme;
  salaId :  number;
  sala :  Sala;
}
