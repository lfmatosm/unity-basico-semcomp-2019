## 2 - Inserindo TileMap

Nesta etapa, iremos explicar a função de tilemaps da unity, para assim criarmos o mapa onde o personagem vai se movimentar.

### Configurando o sprite sheet do tilemap

Na pasta ```Assets/Resources``` de nosso projeto, iremos colocar o sprite sheet do tilemap ```TileSet.png``` que pode ser encontrado [aqui](https://drive.google.com/drive/folders/1JvF5ncDJGAbjktF3B4yVo5NbdJx1Rgel?usp=sharing).

![Localização do sprite no projeto](images/2/1.png?raw=true "Localização do tileset no projeto")

As configurações que devem ser alteradas nessa imagem, para ela funcionar como tilemap, são as seguintes:
### Texture type -> sprite(2D and UI).
### Sprite mode -> Multiple, pois temos vários sprites.
### Pixels per unity -> 16, que é o tamanho de cada tile.
### Filter Mode -> Point, para não criar efeito de blur, já que é uma pixel art.
### Compression -> None

A seguir, na aba ```Inspector``` do sprite, configuramos da mesma forma que fizemos com o Tileset:
![Localização do sprite no projeto](images/2/2.png?raw=true "Localização do sprite no projeto")

Clicamos em ```Sprite Editor```, no menu ```Slice``` e deixamos a configuração da seguinte forma:
![Localização do sprite no projeto](images/2/3.png?raw=true "Localização do sprite no projeto")

Ao fim, podemos clicar em ```Apply``` no canto direito desta janela para realizar as modificações. Pronto, já importamos o sprite sheet desejado no nosso projeto! Mas nós ainda não temos nenhum personagem em nossa cena. Portanto, devemos criar um!

### Criando a animação do personagem

Em nossa cena principal, clicamos com o botão direito em algum espaço vazio da aba e selecionamos ```Create empty```. Podemos nomear este objeto como "Char":
![Localização do sprite no projeto](images/2/4.png?raw=true "Localização do sprite no projeto")

Selecionando o objeto criado, vamos até seu ```Inspector``` e adicionamos o componente ```Sprite Renderer``` no mesmo. Neste componente, selecionamos o campo ```Sprite``` e escolhemos como sua imagem o primeiro frame do sprite que importamos no passo anterior.
![Escolha do primeiro frame do personagem (standing frame)](images/2/5.png?raw=true "Escolha do primeiro frame do personagem (standing frame)")

Selecione novamente o objeto "Char" e abra a aba ```Animation``` no seu Unity. Você verá uma tela como a seguir:
![Solicitação de criação de animação para o objeto selecionado](images/2/6.png?raw=true "Solicitação de criação de animação para o objeto selecionado")

Clique no botão ```Create```. Um menu irá aparecer na sua tela orientando-o a escolher o nome da animação que será criada. Dê o nome de "Walk" à animação e salve-a na pasta ```Assets/Animations```. Se esta pasta não existir, a crie.

Agora, o seu menu de animação será similar ao seguinte:
![Menu de animações disponível](images/2/7.png?raw=true "Menu de animações disponível")

Podemos agora fazer as transições de frames para nosso personagem!
Clique no botão circular vermelho do lado superior esquerdo do menu de animações. Você verá o seguinte:
![Modo de criação de animações ativado](images/2/8.png?raw=true "Modo de criação de animações ativado")

Neste momento, você está no modo de criação de animações. Clicando numa marcação de tempo qualquer da lista, você poderá configurar o frame/sprite para este momento específico da animação. Clique no tempo ```0:10``` por agora. 
![Menu de animação na marcação 0:10](images/2/9.png?raw=true "Menu de animação na marcação 0:10")

Vá até ```Inspector``` do objeto e selecione o sprite que você quer que seja exibido neste momento de animação.

Feito isso, você pode clicar em uma nova marcação de tempo e configurar outro frame/sprite, e assim por diante. Finalizada sua criação, clique novamente no botão vermelho para salvar a animação.
![Menu de animação com animação criada](images/2/10.png?raw=true "Menu de animação com animação criada")

Pronto, animamos nosso personagem! Você pode reposicioná-lo na cena para o lugar que achar mais adequado. Clicando em ```Play``` no centro superior do Unity, veremos nosso personagem movimentando-se!
![Animação do personagem exibida no Unity](images/2/conker.gif?raw=true "Animação do personagem exibida no Unity")

### Controlando o personagem por meio do teclado

Para comandar nosso personagem, deveremos realizar alguns novos passos. Mas, antes disso, precisaremos criar uma pasta ```Assets/Scripts``` para manter nosso projeto organizado.
![Alt](images/2/11.png?raw=true "Alt")

Para controlar um objeto de cena, ou mesmo configurar qualquer tipo de comportamento no mesmo, devemos associar o objeto de interesse a um script. O script do objeto é estruturado em C#, e por meio do uso de estruturas disponibilizadas pelo Unity, podemos modelar o comportamento deste elemento de cena. Logo, iremos criar um script para controlar nosso personagem!

Dentro da pasta criada anteriormente, clique com o botão direito em algum espaço vazio e selecione ```Create > C# Script```. Dê o nome que desejar ao arquivo, como por exemplo ```Character.cs```

A continuar.

* [Anterior](1-inicio.md)
* [Próxima](2-inserindo-personagem.md)
