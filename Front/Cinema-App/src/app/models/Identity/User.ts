import { Funcao } from "src/app/Enums/Funcao.enum";

export interface User {
  username : string;
  email : string;
  password : string;
  funcao : Funcao;
  nomeCompleto : string;
}
