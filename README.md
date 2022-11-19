# Cinema

Projeto backend em .NET core
Projeto frontend em Angular
banco de dados utilizado foi Sqlite

## Considerações
1. sqlite não necessita de instalação, porém se quiser ver os registros das tabelas
1. opcional* [Sqlite Browser Donwload](https://sqlitebrowser.org/dl/)
2. opcional* abra o sqlite browser e arraste o arquivo cinema.db que se encontra dentro do caminho /Back/src/Cinema.API
3. opcional* utilizei o editor Visual Studio code para codar o projeto angular 


## Setup de instalações

### Instalações Angular
1. [Instalar o Node.js](https://nodejs.org/)
2. Instalar o angular cli 
 - abra o promp comandos e digite o comando ```npm install -g @angular/cli@latest```

### Instalações .NET
1. [Versão para Windows 64-bits](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-5.0.102-windows-x64-installer)
2. [Versão para Windows 32-bits](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-5.0.214-windows-x86-installer)

### Clone o Projeto
1. Fork o [Cinema](https://github.com/vsandrade/ProEventos/fork)
2. Clone o repo que voce deu forked.

### Restauração de Pacotes do projeto

#### Restaurar pacotes do projeto backend
1. Via CLI : abra o prompt comandos navegue até a pasta back/src/Cinema.API, apos isso digite o comando ```dotnet build```
2. Via Visual studio : no solution explorer faça um build da Solution 'Cinema'

#### Restaurar pacotes do projeto frontend
1. abra o prompt de comandos navegue até a pasta \Front\Cinema-App , apos isso digite o comando ```npm install --force```

## Rodando os projetos

### Projeto backend
1. Via CLI: abra o prompt comandos navegue até a pasta back/src/Cinaema.API, apos isso digite o comando ```dotnet watch run```
2. Via Visual Studio : Set Cinema.API como projeto principal e depois aperte 'F5' ou o atalho quer você utiliza para startar o projeto 

### Projeto frontend
1. abra o prompt de comandos navegue até a pasta \Front\Cinema-App e digite o comando ```npm start```