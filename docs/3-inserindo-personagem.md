## 3 - Inserindo personagem

Nesta etapa, iremos introduzir o personagem controlável pelo usuário em nosso projeto, e configuraremos os inputs para que o jogador comande o objeto.

### Configurando o sprite sheet do personagem

Na pasta ```Assets/Resources``` de nosso projeto, iremos colocar o sprite sheet ```conker-sprite.png``` que pode ser encontrado [aqui](https://drive.google.com/drive/folders/1JvF5ncDJGAbjktF3B4yVo5NbdJx1Rgel?usp=sharing).
![Localização do sprite no projeto](images/3/1.png?raw=true "Localização do sprite no projeto")

A seguir, na aba ```Inspector``` do sprite, configuramos da mesma forma que fizemos com o Tileset:
![Localização do sprite no projeto](images/3/2.png?raw=true "Localização do sprite no projeto")

Clicamos em ```Sprite Editor```, no menu ```Slice``` e deixamos a configuração da seguinte forma:
![Localização do sprite no projeto](images/3/3.png?raw=true "Localização do sprite no projeto")

Ao fim, podemos clicar em ```Apply``` no canto direito desta janela para realizar as modificações. Pronto, já importamos o sprite sheet desejado no nosso projeto! Mas nós ainda não temos nenhum personagem em nossa cena. Portanto, devemos criar um!

### Criando a animação do personagem

Em nossa cena principal, clicamos com o botão direito em algum espaço vazio da aba e selecionamos ```Create empty```. Podemos nomear este objeto como "Char":
![Localização do sprite no projeto](images/3/4.png?raw=true "Localização do sprite no projeto")

Selecionando o objeto criado, vamos até seu ```Inspector``` e adicionamos o componente ```Sprite Renderer``` no mesmo. Neste componente, selecionamos o campo ```Sprite``` e escolhemos como sua imagem o primeiro frame do sprite que importamos no passo anterior.
![Escolha do primeiro frame do personagem (standing frame)](images/3/5.png?raw=true "Escolha do primeiro frame do personagem (standing frame)")

Selecione novamente o objeto "Char" e abra a aba ```Animation``` no seu Unity. Você verá uma tela como a seguir:
![Solicitação de criação de animação para o objeto selecionado](images/3/6.png?raw=true "Solicitação de criação de animação para o objeto selecionado")

Clique no botão ```Create```. Um menu irá aparecer na sua tela orientando-o a escolher o nome da animação que será criada. Dê o nome de "Walk" à animação e salve-a na pasta ```Assets/Animations```. Se esta pasta não existir, a crie.

Agora, o seu menu de animação será similar ao seguinte:
![Menu de animações disponível](images/3/7.png?raw=true "Menu de animações disponível")

Podemos agora fazer as transições de frames para nosso personagem!
Clique no botão circular vermelho do lado superior esquerdo do menu de animações. Você verá o seguinte:
![Modo de criação de animações ativado](images/3/8.png?raw=true "Modo de criação de animações ativado")

Neste momento, você está no modo de criação de animações. Clicando numa marcação de tempo qualquer da lista, você poderá configurar o frame/sprite para este momento específico da animação. Clique no tempo ```0:10``` por agora. 
![Menu de animação na marcação 0:10](images/3/9.png?raw=true "Menu de animação na marcação 0:10")

Vá até ```Inspector``` do objeto e selecione o sprite que você quer que seja exibido neste momento de animação.

Feito isso, você pode clicar em uma nova marcação de tempo e configurar outro frame/sprite, e assim por diante. Finalizada sua criação, clique novamente no botão vermelho para salvar a animação.
![Menu de animação com animação criada](images/3/10.png?raw=true "Menu de animação com animação criada")

Pronto, animamos nosso personagem! Você pode reposicioná-lo na cena para o lugar que achar mais adequado. Clicando em ```Play``` no centro superior do Unity, veremos nosso personagem movimentando-se!
![Animação do personagem exibida no Unity](images/3/conker.gif?raw=true "Animação do personagem exibida no Unity")

### Controlando o personagem por meio do teclado

Para comandar nosso personagem, deveremos realizar alguns novos passos. Mas, antes disso, precisaremos criar uma pasta ```Assets/Scripts``` para manter nosso projeto organizado.
![Alt](images/3/11.png?raw=true "Alt")

Para controlar um objeto de cena, ou mesmo configurar qualquer tipo de comportamento no mesmo, devemos associar o objeto de interesse a um script. O script do objeto é estruturado em C#, e por meio do uso de estruturas disponibilizadas pelo Unity, podemos modelar o funcionamento de um elemento de cena. Logo, iremos criar um script para controlar nosso personagem!

Dentro da pasta criada anteriormente, clique com o botão direito em algum espaço vazio e selecione ```Create > C# Script```. Dê o nome que desejar ao arquivo, como por exemplo ```Character.cs```.

Agora, temos de associar ao script criado o nosso personagem. Clique no objeto do personagem no menu lateral esquerdo do Unity. Se certifique de adicionar um novo componente ao nosso objeto, o componente "Script". Arraste para dentro do campo de mesmo nome deste componente o arquivo ```Character.cs``` que criamos acima. Ao executar esta etapa, o componente Script do personagem deve estar da seguinte forma:
![Alt](images/3/13.png?raw=true "Alt")

O Unity disponibiliza um componente chamado [Input](https://docs.unity3d.com/ScriptReference/Input.html) que pode ser usado para capturar a entrada do usuário por teclado. Por exemplo, por meio desse componente podemos realizar a movimentação básica da direita para a esquerda tão típica em jogos de plataforma. Entretanto, antes de usarmos essa classe, devemos afirmar em nosso script que desejamos manipular informações sobre o ```Rigidbody2D``` do nosso personagem. É no ```Rigidbody2D``` que se encontram informações como posição do objeto na tela ou velocidade de deslocamento, variáveis que são de nosso interesse nesta etapa. Criemos a variável que irá armazenar o corpo rígido em nosso script:

```csharp
private Rigidbody2D rb2d;
```

Após isso, podemos criar nossa função. Ela terá a seguinte cara:

```csharp
void Awake()
{
    rb2d = GetComponent<Rigidbody2D>();
}
```

A função ```Awake``` será chamada assim que nosso script começar a ser executado pelo Unity, uma única vez durante a execução desta cena. A função deve ter esse exato nome, pois é uma função padrão do Unity Engine que podemos sobrescrever da forma que melhor se encaixar no nosso projeto. Esta função associa o ```Rigidbody2D``` do personagem ao script corrente, possibilitando a alteração de suas variáveis internas.

A seguir, desejamos realizar o mais básico dos movimentos em jogos: movimentar-se para a direita e esquerda. Para tal, devemos primeiramente pensar em algumas variáveis. No início de nossa classe ```Character```, podemos declarar variáveis a seguir:

```csharp
public float moveForce = 365f;
public float maxSpeed = 5f;
```

Essas variáveis respectivamente armazenam os valores-padrão para força de movimento e velocidade máxima de nosso personagem. Como o modificador dessas variáveis é ```public```, você poderá perceber que ao selecionar o objeto do personagem na cena podemos alterar os valores desses campos por meio do menu Inspector -- essas alterações podem ser feitas por meio do componente Script presente nesta tela.

A função ```FixedUpdate``` a seguir descreve o controle da movimentação horizontal do personagem. Essa função, assim como a anterior, também é uma função padrão do Unity e é executada 50 vezes a cada segundo, uma taxa maior que a função similar ```Update``` -- também nativa da engine.


```csharp
void FixedUpdate()
{
    float h = Input.GetAxis("Horizontal");

    if (h * rb2d.velocity.x < maxSpeed)
        rb2d.AddForce(Vector2.right * h * moveForce);

    if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
        rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
}
```

A primeira linha da função anterior computa o valor do eixo horizontal representado pela entrada de input do usuário. Este valor varia de -1 a 1. Por exemplo, se apertamos a seta esquerda no nosso teclado, esse valor será < 0, e caso contrário, caso apertamos a tecla direita esse valor será > 0. Esse valor é usado como base para o cálculo da nova posição do personagem dada a psoição atual, representado no primeiro if. Já o segundo if corresponde a uma atualização da velocidade do dado objeto.

Neste momento, já temos um personagem que consegue movimentar-se da esquerda para a direita. Note que a colisão adicionada nos tiles criados na etapa anterior é suficiente para impedir que o personagem atravesse o "chão" no momento em que o jogo é iniciado.

Apesar de que o personagem já se movimenta, a orientação de seu sprite está incorreta dependendo da direção que teclamos em nosso input. De forma a trocar a orientação do sprite dependendo da direção, devemos em primeiro lugar definir a seguinte variável de classe:

```csharp
private bool lookingToTheRight = true;
```

Essa variável indica a orientação do objeto em qualquer dado momento da execução da cena. Para manipular tal variável, uma função ```FlipSprite``` deve ser cirada:

A única coisa que está função faz é inverter a orientação no eixo x por meio da manipulação da escala do objeto, fazendo o personagem "olhar" para a esquerda e para a direita conforme comandado pelo usuário. Para fazer uso dessa função em nosso script, podemos inserir mais um if dentro de ```FixedUpdate``` da seguinte maneira:

```csharp
void FixedUpdate()
{
    float h = Input.GetAxis("Horizontal");

    if ((lookingToTheRight && h < 0) || (!lookingToTheRight && h > 0))
        FlipSprite();

    if (h * rb2d.velocity.x < maxSpeed)
        rb2d.AddForce(Vector2.right * h * moveForce);

    if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
        rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
}
```

Esta cláusula if testa se a direção que o objeto está orientado atualmente vai de encontro com a orientação que o usuário ordenou por meio do teclado. Caso as orientações sejam distintas, o sprite é invertido horizontalmente.

Pronto! Agora temos um personagem que "olha" para as direções esperadas dados nossos comandos. Contudo, uma habilidade muito importante ainda não foi dada a nosso personagem: o poder de pular.

Inicialmente adicionamos uma nova variável para controlar quando o personagem está em contato com o chão, uma variável para a força do pulo e outra descrevendo a velocidade máxima do personagem:

```csharp
private bool grounded = true;
public float jumpForce = 2f;
public float maxVerticalSpeed = 3f;
```

Já a função ```OnCollisionEnter2D``` avalia se está ocorrendo agora uma colisão com algum objeto com a tag "TileMap". Se sim, é porquê estamos no chão do nível. De froma análoga, a função ```OnCollisionExit2D```prevê o momento em que o nosso personagem perde o contato com o chão -- ou seja, quando pula. A função ```Jump``` adiciona força e velocidade de forma a realuzar o pulo desejado. Adicione as funções a seguir em seu script:

```csharp
private void OnCollisionEnter2D(Collision2D other)
{
    if (other.gameObject.CompareTag("TileMap"))
        grounded = true;
}

private void OnCollisionExit2D(Collision2D other) {
    if (other.gameObject.CompareTag("TileMap"))
        grounded = false;
}

void Jump()
{
    rb2d.AddForce(new Vector2(0, jumpForce));
    rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Sign(rb2d.velocity.y) * maxVerticalSpeed);
}
```

Já ```FixedUpdate``` agora precisa chamar a função ```Jump```:

```csharp
void FixedUpdate()
{
    float h = Input.GetAxis("Horizontal");

    if (Input.GetKeyDown(KeyCode.Space) && grounded)
        Jump();

    if ((lookingToTheRight && h < 0) || (!lookingToTheRight && h > 0))
        FlipSprite();
        
    if (h * rb2d.velocity.x < maxSpeed)
        rb2d.AddForce(Vector2.right * h * moveForce);

    if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
        rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
}
```

* [Anterior](2-criação-do-tilemap.md)
* [Próxima](4-camera-e-background.md)
