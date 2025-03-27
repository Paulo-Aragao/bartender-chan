# Bar Tycoon Game

Este projeto é um protótipo funcional de um jogo do tipo tycoon, no qual o jogador gerencia um bar. O objetivo é contratar e aprimorar bartenders, preparar drinks com diferentes características (preço, custo de produção, dificuldade e impacto na reputação) e manter o bar com uma reputação alta enquanto se gerencia os recursos financeiros.

## Visão Geral

No jogo, você gerencia um bar com até 5 bartenders. Cada bartender prepara drinks de acordo com sua taxa de produção e skill, afetando diretamente o ganho financeiro do bar. Conforme o bar evolui, novos bartenders são desbloqueados.

### Principais Mecânicas
- **Preparação de Drinks:**  
  Cada bartender tenta preparar drinks; a chance de sucesso é calculada combinando a dificuldade do drink com a skill do bartender. Em caso de sucesso, o bar ganha dinheiro e reputação; em caso de falha, há penalidades.
  
- **Gerenciamento de Dinheiro e Reputação:**  
  O jogador deve tentar maximar os ganhos para contratar novas bartenders e assim aumentar sua taxa de ganhos.

- **Ferramentas de Debug:**  
  Ferramentas auxiliares desenvolvidas com Odin Inspector, incluindo uma janela de debug que exibe métricas em tempo real (dinheiro atual, dinheiro por minuto e FPS) e mede o desempenho das operações de save/load.

## Estrutura do Projeto

_Project/
├── Animation/             # Animações do jogo
├── Data/                  # Dados e configurações gerais do jogo
│   ├── Game Elements/     # Dados dos elementos do jogo (drinks e bartenders)
│   │   ├── Bartenders/    # Configurações e dados dos bartenders
│   │   └── Drinks/        # Configurações e dados dos drinks
│   └── Services/          # Configurações e dados dos serviços (ex.: SaveService)
├── Editor/                # Scripts e customizações do Editor do Unity
├── Fonts/                 # Fontes utilizadas no jogo
├── Prefabs/               # Prefabs para instanciar objetos nas cenas
│   ├── Core/              # Prefabs centrais
│   └── UI/                # Prefabs da interface do usuário
├── Scenes/                # Cenas do jogo
├── Scripts/               # Código-fonte do jogo
│   ├── Core/              # Scripts centrais e gerenciadores
│   ├── Data/              # Scripts relacionados a dados e configurações
│   ├── Gameplay/          # Scripts das mecânicas de jogo
│   │   ├── Bartender/     # Scripts específicos para bartenders
│   │   │   └── State Pattern/  # Implementação do State Pattern para bartenders
│   │   └── Drinks/        # Scripts para criação e gerenciamento de drinks
│   ├── Services/          # Scripts dos serviços do jogo
│   │   ├── Providers/     # Provedores de serviços (ex.: SoundService)
│   │   └── SaveSystem/    # Sistema de salvamento
│   │       └── Data/      # Dados utilizados pelo sistema de salvamento
│   ├── UI/                # Scripts da interface do usuário
│   └── Utils/             # Scripts utilitários e métodos de extensão
├── Sounds/                # Arquivos de áudio do jogo
│   ├── Music/             # Músicas e trilha sonora
│   └── SFX/               # Efeitos sonoros (SFX)
└── Sprites/               # Imagens e sprites do jogo
    ├── Characters/        # Sprites dos personagens
    │   ├── Bartender/     # Sprites dos bartenders
    │   ├── Client/        # Sprites dos clientes
    ├── Enviroment/        # Sprites dos elementos do ambiente
    ├── GUI/               # Sprites da interface gráfica (GUI)
    └── Items/             # Sprites dos itens
        ├── HighBall/      # Sprites para copos HighBall
        ├── LittleBottle/  # Sprites para garrafas pequenas
        ├── MartiniGlass/  # Sprites para copos Martini
        └── Poco Grande Glass/  # Sprites para copos Poco Grande

## Ferramentas e Tecnologias

- **Unity 2022** – Motor de jogo utilizado para o desenvolvimento.
- **C#** – Linguagem de programação dos scripts.
- **DOTween** – Biblioteca de tweening para animações e efeitos visuais suaves.
- **Odin Inspector** – Ferramenta que aprimora o Inspector do Unity e permite criar janelas customizadas para debug.
- **Service Locator Pattern** – Padrão de projeto para gerenciamento centralizado de serviços (por exemplo, MoneyService, SaveService, AudioService).
- **State Pattern** – Padrão de projeto para gerenciamento de estados da bartender (fazendo drinks, em espera, comemorando, falhando).

## Ferramentas de Debug e Performance

Uma janela de editor personalizada chamada **PerformanceDebugTool** foi criada utilizando Odin Inspector. Essa ferramenta permite monitorar em tempo real:
- **Current Money:** Dinheiro atual do bar.
- **Money Per Minute:** Ganho de dinheiro por minuto, calculado com base na variação do valor.
- **FPS:** Frames por segundo.

Para acessar basta ir em Window/Performance Debug Tool
Além disso, a ferramenta inclui botões para medir o tempo de execução das operações de save e load, permitindo identificar se as escritas em arquivos estão afetando o desempenho.

## Service Locator

O padrão Service Locator é utilizado neste projeto para gerenciar de forma centralizada os principais serviços do jogo, como o gerenciamento de dinheiro, salvamento, áudio, entre outros. Em vez de criar instâncias e referenciar diretamente cada serviço em vários pontos do código, o Service Locator atua como um "repositório" de serviços, permitindo que qualquer parte do sistema os acesse de maneira desacoplada.

### Como Funciona

- **Registro de Serviços:**  
  Durante a fase de inicialização (por exemplo, em uma cena de boot ou através de métodos estáticos com `[RuntimeInitializeOnLoadMethod]`), cada serviço é registrado no Service Locator. Isso significa que os serviços, como `MoneyService`, `SaveService` e `AudioService`, são adicionados a um dicionário interno com uma chave que geralmente é o seu tipo. Dessa forma, o código garante que apenas uma instância de cada serviço seja usada em todo o projeto.

- **Acesso aos Serviços:**  
  Em qualquer lugar do código, quando um serviço é necessário, basta chamar o método `ServiceLocator.Get<T>()`, passando o tipo do serviço desejado. O Service Locator retorna a instância previamente registrada. Essa abordagem elimina a necessidade de referências diretas e permite que os serviços sejam facilmente substituídos ou atualizados sem alterar os scripts que os consomem.

- **Desacoplamento:**  
  O uso do Service Locator promove um alto nível de desacoplamento entre os componentes do jogo. Os scripts não precisam conhecer os detalhes de criação ou inicialização dos serviços; eles simplesmente os requisitam quando necessário. Isso facilita a manutenção e a escalabilidade do código, pois mudanças na implementação de um serviço não impactam diretamente os consumidores.


### Exemplo de Uso no Projeto

No projeto, alguns exemplos de serviços gerenciados pelo Service Locator são:

- **MoneyService:**  
  Responsável por gerenciar o dinheiro atual do bar, incluindo operações de adição, subtração e salvamento do estado financeiro do jogo.

- **SaveService:**  
  Gerencia o salvamento e carregamento dos dados do jogo utilizando o sistema de arquivos.  
  A performance dessas operações é monitorada por ferramentas de debug para garantir que não haja impactos significativos no desempenho.

- **SoundService:**  
  Cuida da reprodução de efeitos sonoros (SFX) e música, instanciando um `AudioSource` em um objeto persistente e disponibilizando métodos para tocar os clipes de áudio.

Para registrar e acessar esses serviços, o código do projeto utiliza algo similar ao seguinte:

```csharp
// Registro dos serviços durante a inicialização:
ServiceLocator.Register(new MoneyService());
ServiceLocator.Register(new SaveService());
ServiceLocator.Register(new AudioService());

// Acesso aos serviços em qualquer script:
MoneyService moneyService = ServiceLocator.Get<MoneyService>();
moneyService.Add(100);//adiciona 100 de dinheiro
```

## State Pattern

O padrão State (Estado) é utilizado neste projeto para gerenciar o comportamento dinâmico dos bartenders durante a preparação dos drinks. Com ele, cada bartender pode transitar entre diferentes estados (como Idle, Shaking, Success e Failed) de maneira organizada e modular, permitindo que a lógica de cada estado fique encapsulada em classes separadas.

### Como Funciona

- **Encapsulamento dos Estados:**  
  Cada estado do bartender é representado por uma classe distinta que implementa uma interface ou herda de uma classe base de estado. Isso permite que a lógica associada a cada estado seja isolada, facilitando a manutenção e a evolução do comportamento.

- **Transição de Estados:**  
  O bartender mantém uma referência para o seu estado atual. Quando ocorre um evento (por exemplo, após tentar preparar um drink), o bartender muda seu estado chamando um método como `ChangeState(new SuccessState())` ou `ChangeState(new FailedState())`. Essa transição permite que o comportamento do bartender seja alterado dinamicamente.

### Exemplo de Uso no Projeto

No nosso projeto, o State Pattern é aplicado na lógica dos bartenders, conforme ilustrado abaixo foi usado em uma outra ferramenta de debug:

```csharp
// Metodos de uma ferramenta de debug para visualização dos estados da bartender 
  [Button][FoldoutGroup("Debug")]
  public void Shake() => ChangeState(new ShakingState());
  [Button][FoldoutGroup("Debug")]
  public void Success() => ChangeState(new SuccessState());
  [Button][FoldoutGroup("Debug")]
  public void Failed() => ChangeState(new FailedState());
  [Button][FoldoutGroup("Debug")]
  public void Idle() => ChangeState(new IdleState());

```
![URL](https://i.postimg.cc/qvk88sBQ/Screenshot-2025-03-27-010939.png)

## Configuração e Execução

1. Clone o Repositório:**
   ```bash
   git clone https://github.com/Paulo-Aragao/bartender-chan.git
  ```

2. O projeto deve ser sempre executado pela a cena boot

  A cena boot é onde os serviços do Service Locator são registrados e inicializados, essa cena serve para garantir que ao iniciar o jogo tudo que é necessario já vai está pronto para o uso.
  A cena fica em Assets/_Project/Scenes/boot.scene:
