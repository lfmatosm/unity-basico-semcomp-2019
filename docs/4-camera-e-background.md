
## 2 - Movimentação da Câmera e Background

Nesta etapa, iremos adicionar a movimentação da câmera e do background seguindo o personagem.

### Câmera - Criação do script da Main Camera

No objeto Main Camera iremos criar um novo componente, um script chamado CameraMovement, da seguinte maneira:

![Localização do sprite no projeto](images/4/1.png?raw=true "Criação do script")

Começaremos criando as seguintes variáveis de controle:

![Localização do sprite no projeto](images/4/2.png?raw=true "Variáveis")

Target será nosso player.
Os bounds serão os limitadores de movimento da câmera. Como também teremos variáveis para definir o tamanho da câmera e os limites do level.
SmoothDampTime é o tempo aproximado que vai demorar para ir da posição original até a nova posição.
SmoothDampVelocity seria a velocidade dessa variação de posição.

Agora vamos associar os valores corretos para cada uma dessas variáveis, ao iniciar a cena:
![Localização do sprite no projeto](images/4/3.png?raw=true "Start")
Aqui vamos inicializar os tamanhos da nossa câmera, o tamanho dos nossos limites(e com esses tamanhos, retirar suas posições).

Agora com as variáveis iniciadas, vamos criar nosso método update()
![Localização do sprite no projeto](images/4/4.png?raw=true "Update")
Vamos verificar se o player foi inicializado, caso afirmativo, iremos a cada frame encontrar a posição do nosso player, nesse momento usaremos a função Mathf.Max() para sabermos se a câmera deve parar ou não nas bordas do level.

Com isso em mãos, vamos usar a função Mathf.SmoothDamp, que já declaramos os valores que utilizaremos anteriormente, esse função faz com que a posição A vá até a posição B, utilizando os valores de tempo e velocidade que declaramos.

No final de tudo isso, vamos atualizar a posição da câmera com esse X e Y novos e mantendo o Z.

### Detalhes Finais

Para que isso funcione, vamos criar um objeto vazio chamado Bounds, e nele vamos criar 3 objetos, esses serão apenas colisores que limitarão a cena.

![Localização do sprite no projeto](images/4/5.png?raw=true "Bounds")

Por fim iremos associar cada um desses objetos ao script da câmera, arrastando da hierarquia até os slots do componente:

![Localização do sprite no projeto](images/4/6.png?raw=true "Associação script")

### Criando o TileMap

Em nossa janela de hierarquia, clicamos com o botão direito em algum espaço vazio e criamos um tilemap ```2D Object -> TileMap```:
![Localização do sprite no projeto](images/2/3.png?raw=true "Criação do tilemap")

Serão criados um Objeto Grid e um TileMap, nós usaremos todas as configurações no default da unity.

Agora vamos criar nossa Palette, vamos em ```Windows->2D->Tile Palette```:
![Localização do sprite no projeto](images/2/4.png?raw=true "Criação do tile palette")

Posicionaremos essa nova janela na interface da maneira mais confortável:
![Localização do sprite no projeto](images/2/5.png?raw=true "Tile palette")

Nessa janela vamos criar uma nova palette e uma pasta em Source para organizarmos as palettes.
Após isso, vamos arrastar nosso tileset para essa janela.
![Localização do sprite no projeto](images/2/6.png?raw=true "Setup com tile palette")

Agora com nossa cena e nossa tile palette abertas, podemos começar a desenhar o level com o brush(B)!
![Localização do sprite no projeto](images/2/7.png?raw=true "Level teste")

Ao criar um tilemap usando pixel perfect, podemos ter um problemas com linhas marcando da seguinte forma:
![Localização do sprite no projeto](images/2/8.png?raw=true "Linhas de blur")

Para corrigir esse problema vamos em ```Edit -> Project Settings -> Quality``` e desativamos o anti-aliasing.

Agora vamos adicionar uma plataforma na frente da outra tile, para trabalharmos com layers diferentes.
![Modo de criação de animações ativado](images/2/9.png?raw=true "Novo tilemap")

Vamos criar um novo tilemap dentr do grid, para esse ter um layer maior que o anterior, assim ele ficará na frente em cena.

![Modo de criação de animações ativado](images/2/10.png?raw=true "Tilemap em uso")

Devemos lembrar de escolher o qual o tilemap está ativo naquele momento na aba de tile palettes.

Terminaremos nossa cena dessa maneira:
![Modo de criação de animações ativado](images/2/11.png?raw=true "Cena final")

### Colisão do TileMap

Para finalizar, iremos adicionar colisão individual aos tiles, em ambos os tilemaps, iremos adicionar o seguinte componente:
![Modo de criação de animações ativado](images/2/12.png?raw=true "Colisão")

Assim, nossas plataformas irão funcionar como chão corretamente.

* [Anterior](1-inicio.md)
* [Próxima](3-inserindo-personagem.md)
