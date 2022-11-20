import { Funcao } from "src/app/Enums/Funcao.enum"

export interface UserUpdate {
  id : number;
  userName : string;
  email : string;
  nomeCompleto : string;
  password : string;
  token : string;
  funcao : Funcao;
}
