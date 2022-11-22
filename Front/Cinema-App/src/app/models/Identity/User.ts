import { Funcao } from "src/app/Enums/Funcao.enum";

export interface User {
  userName : string;
  email : string;
  password : string;
  funcao : Funcao;
  nomeCompleto : string;
  token: string;
}
