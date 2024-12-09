# Projeto_IA
## Introdução
Este projeto foi desenvolvido no âmbito da UC de Inteligência Artifical Aplicada a Jogos.

Neste jogo, o jogador começa a zona inicial e terá de chegar à zona final o mais rápido possivel e para isso terá que entrar num labirinto.
## Controlos
- W/A/S/D - Controlos de movimento do jogador.
- Generate Maze - Botão para gerar o labirinto.
- Change Maze - Botão para mudar o labirinto.

![{8B3BD82E-64B5-4172-9831-8BED40FBB6D1}](https://github.com/user-attachments/assets/151ef20c-7786-424d-ac80-1602ec8f0553)

## AI
Neste jogo utilizamos as seguintes técnicas de Inteligência Artificial:
- DFS para Procedural Generation;
- PathFinding (NavMesh/A*);
- Machine Learning.


## DFS para Procedural Generation (Rafael)
Para a base deste jogo utilizamos um Procedural Maze. É um labirinto gerado automaticamente utilizando algoritmos computacionais. Em vez de ser criado manualmente, os labirintos são criados com base em regras predefinidas e podem ser diferentes a cada execução do programa.
Neste caso, o labirinto é gerado em tempo real, com o layout do labirinto determinado de forma aleatória.
 
Scripts:
-Maze Cell
-Maze Generator

Maze Cell- Elementos e Paredes
.Cada célula possui quatro paredes: esquerda, direita, frontal, e traseira, bem como um bloco para indicar que a célula ainda não foi visitada.
.As paredes podem ser removidas para criar caminhos.
![image](https://github.com/user-attachments/assets/8f0d5aa2-6d34-40b4-9087-0d43bb5abb25)
![image](https://github.com/user-attachments/assets/545bd1e8-6f20-4e10-a92f-86cb34c14607)

Maze Generator- Gera o labirinto; Define aleatoriamente a entrada e saida do labirinto; Função de Reset do Labirinto
![image](https://github.com/user-attachments/assets/2514d510-37bb-4f51-bf5b-cbec2c9e1dcd)
![image](https://github.com/user-attachments/assets/c79a2279-6574-4c25-98d2-881f10de81a7)
![image](https://github.com/user-attachments/assets/c573e7e4-e56d-4161-b699-548c141702d1)
![image](https://github.com/user-attachments/assets/6c7dec3e-979d-4979-b40c-801237b78cf0)
Este algoritmo começa numa célula inicial, marca-a como visitada, e remove paredes aleatoriamente para conectar-se a células vizinhas ainda não visitadas.
Quando não há vizinhos disponíveis, ele "volta" para células anteriores até encontrar novas possibilidades.





## PathFinding (Gabriel)
Para este jogo utilizamos PathFinding para mostrar ao jogador o caminho mais rápido no labirinto.

Para isso utilizamos o Unity NavMesh que é um sistema que permite a criação de mapas de navegação( Mesh ) que definem as zonas onde os personagens ou outros tipos de GameObjects se poderão mover.
#### Unity NavMesh

Elementos do NavMesh:
- NavMesh Navigation Mesh:
  - Malha simplificada que representa as áreas navegáveis pelo NavMesh Agent.
- NavMesh Agent:
  - É um componente adicionada a um GameObject que o transforma num "Agente" capaz de navegar pela Navigation Mesh.
- NavMesh Obstacle:
  - É o compenente que determina que GameObject o NavMesh Agent tem que evitar.
- NavMesh Bake:
  - É o processo de criar a Navigation Mesh, onde o Unity analisa a geometria do nível e cria uma malha navegável sobre os objetos que podem ser navegados. 

![{2985E6D7-5C9C-4F86-8290-7C6C5FF8D9B4}](https://github.com/user-attachments/assets/5edcf1a5-5366-4ef0-b3d1-9cafcf79ecb2)

#### A* no Unity NavMesh
O A* é um algoritmo de pesquisa heuristica amplamente utilizado para encontrar o caminho mais curto em grafos ou redes. Combina a eficiência do algoritmo de Dijkstra com a função Heurística para acelarar a pesquisa pelo caminho ideal.

No contexto de um NavMesh, o A* é aplicado para encontrar o caminho mais curto entre 2 pontos na Navigation Mesh.

O NavMesh para o A* é dividido em:
- Nós:
  - Representam áreas navegáveis com polígonos e triangulos.
- Arestas:
  - Conexões entre os polígonos adjacentes.
Um grafo é criado, onde cada nó representa um polígono do NavMesh, e as arestas representam caminhos viáveis.

Processo de execução do Pathfinding:
- Identificar os nós inicial e final:
  - Localizar os polígons do NavMesh que contêm o ponto inicial e final.
- Executar o A*:
  - Usar o A* para calcular o caminho do ponto inicial ao final através do grafo de polígonos.
- Interpolação dentro dos polígonos:
  - O caminho que foi calculado é uma sequência de polígonos;
  - Uma interpolação linear pode ser aplicada para gerar o caminho detalhado dentro dos polígonos.
- Refinar o caminho:
  - Pode ser aplicada uma suavização do caminho de modo a eliminar movimentos desnecessários ou pouco naturais.
    
![image](https://github.com/user-attachments/assets/02a41611-d57f-4ab9-a442-077af4fff0e1)
![image](https://github.com/user-attachments/assets/f3000156-d7b1-4e2f-a13a-262ea34b8fbb)
![image](https://github.com/user-attachments/assets/3c3cae36-d5fd-41b8-b7d6-4b362979240b)



## Machine Learning (Nuno)

Para este jogo utilizamos também o Machine Learning com o objetivo do bot conseguir completar o caminho certo do labirinto.

Para isto utilizamos o ML-Agents
Ações Realizadas:
-Instalação do pacote ML-Agents diretamente no Unity via Package Manager.
-Criação de um ambiente Unity com agentes que representam "solucionadores de labirintos".

-Configuração do componente Behavior Parameters no agente
![image](https://github.com/user-attachments/assets/61049e90-153c-4764-8465-a0bb2bc34aec)
Nome do Behavior: MazeSolverAgent.

-Adicionamos um Script com o nome de "Maze Agent"

![image](https://github.com/user-attachments/assets/f34f57a9-fb0a-4c4f-8518-ae1eeab50e68)

Nele definimos colisões, movimento, regras do movimento e o que acontece se as regras forem quebradas
![image](https://github.com/user-attachments/assets/34199d5c-30fc-4d35-9896-139b34355608)
![image](https://github.com/user-attachments/assets/bb8864d3-1e0a-4642-b969-fba345d95b75)

-Adicionamos uma Decision Request e deixamos os valores Default
![image](https://github.com/user-attachments/assets/5a2141f2-b77e-44d8-85df-7a4e8ee63f99)

Com isto passamos para o treinamento do Bot
![image](https://github.com/user-attachments/assets/e4f0d8b6-cdb0-4f8d-9fe2-2418c365352d)
utilizamos o comando "mlagents-learn --run-id=TesteApli" para começar com os testes para o bot aprender o caminho.

Objetivo: Validar o comportamento do bot treinado.

Exportar o modelo treinado (.onnx) com eficiência e importar no unity.
![image](https://github.com/user-attachments/assets/c163eea1-0638-4c19-b6ab-b6c47880500e)

Testar em cenários diferentes para avaliar se o agente encontra a solução do labirinto com eficiência





