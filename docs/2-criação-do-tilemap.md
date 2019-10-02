## 2 - Criando TileMap

Nesta etapa, iremos explicar a função de tilemaps da unity, para assim criarmos o mapa onde o personagem vai se movimentar.

### Configurando o sprite sheet do tilemap

Na pasta ```Assets/Resources``` de nosso projeto, iremos colocar o sprite sheet do tilemap ```TileSet.png``` que pode ser encontrado [aqui](https://drive.google.com/drive/folders/1JvF5ncDJGAbjktF3B4yVo5NbdJx1Rgel?usp=sharing).

![Localização do sprite no projeto](images/2/1.png?raw=true "Localização do tileset no projeto")

As configurações que devem ser alteradas nessa imagem, para ela funcionar como tilemap, são as seguintes:<br/>
Texture type -> sprite(2D and UI).<br/>
Sprite mode -> Multiple, pois temos vários sprites.<br/>
Pixels per unity -> 16, que é o tamanho de cada tile.<br/>
Filter Mode -> Point, para não criar efeito de blur, já que é uma pixel art.<br/>
Compression -> None<br/>

Clicamos em ```Sprite Editor```, no menu ```Slice``` e deixamos a configuração da seguinte forma:
![Localização do sprite no projeto](images/2/2.png?raw=true "Slice do tileset")

Faremos as seguintes opções:
Slice -> Type -> Grid by cell size<br/>
Pixel Size -> 16x16<br/>
Padding -> 1x1<br/>

Ao fim, podemos clicar em ```Apply``` no canto direito desta janela para realizar as modificações. Pronto, já importamos o sprite sheet desejado no nosso projeto! Mas nós ainda não temos nossa palette do tilemap pronta. Portanto, vamos criar uma!

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
