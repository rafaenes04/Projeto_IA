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


## Machine Learning (Nuno)





