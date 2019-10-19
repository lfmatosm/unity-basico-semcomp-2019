## 3 - Inserindo personagem

Nesta etapa, iremos introduzir o personagem controlável pelo usuário em nosso projeto, e configuraremos os inputs para que o jogador comande o objeto.

### Configurando o sprite sheet do personagem

Na pasta ```Assets/Resources``` de nosso projeto, iremos colocar o sprite sheet ```conker-sprite.png``` que pode ser encontrado [aqui](https://drive.google.com/drive/folders/1JvF5ncDJGAbjktF3B4yVo5NbdJx1Rgel?usp=sharing).
![Localização do sprite no projeto](images/3/1.png?raw=true "Localização do sprite no projeto")

A seguir, na aba ```Inspector``` do sprite, configuramos da mesma forma que fizemos com o Tileset:
![Localização do sprite no projeto](images/3/2.png?raw=true "Localização do sprite no projeto")

Clicamos em ```Sprite Editor```, no menu ```Slice``` e deixamos a configuração da seguinte forma:
![Localização do sprite no projeto](images/3/18.png?raw=true "Localização do sprite no projeto")

Outras configurações podem ser realizadas para importar um sprite sheet, mas iremos utilizar a ilustrada acima em nosso caso.

Clique em "Slice". Você perceberá que o sprite sheet foi sobreposto por uma quantidade de figuras retangulares, que provavelmente não estão limitando corretamente cada frame de animação do sprite sheet.

Felizmente, o Unity permite que cliquemos em cada um desses retângulos e ajustemos sua posição de dimensões manualmente, de forma que consigamos delimitar de forma mais precisa os sprites de que precisamos. Reorganize os frames da forma que achar melhor e, por fim, clique no botão ```Apply``` no canto superior direito da janela para salvar a configuração.
![Localização do sprite no projeto](images/3/19.png?raw=true "Localização do sprite no projeto")

A configuração que criamos nesta etapa nos auxiliará no momento de criação das animações para nosso personagem.

### Controlando o personagem por meio do teclado

Em nossa cena principal, clicamos com o botão direito em algum espaço vazio da aba e selecionamos ```Create empty```. Podemos nomear este objeto como "Char":
![Localização do sprite no projeto](images/3/4.png?raw=true "Localização do sprite no projeto")

Selecionando o objeto criado, vamos até seu ```Inspector``` e adicionamos o componente ```Sprite Renderer``` no mesmo. Neste componente, selecionamos o campo ```Sprite``` e escolhemos como sua imagem o primeiro frame do sprite que importamos no passo de importação do sprite.
![Escolha do primeiro frame do personagem (standing frame)](images/3/5.png?raw=true "Escolha do primeiro frame do personagem (standing frame)")

No ```Sprite Renderer``` também devemos configurar o parâmetro ```Order in Layer``` de forma que o personagem seja visível na tela e não fique atrás de algum elemento de background. Configure o valor desta propriedade para 1.

Ainda, adicione o componente ```Polygon Collider 2D``` no objeto. Este componente será útil quando desejarmos realizar colisão com outros elementos de cena.

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
public float jumpForce = 5f;
public float maxVerticalSpeed = 8f;
```

Já a função ```OnCollisionEnter2D``` avalia se está ocorrendo agora uma colisão com algum objeto com a tag "TileMap". Se sim, é porquê estamos no chão do nível. De froma análoga, a função ```OnCollisionExit2D```prevê o momento em que o nosso personagem perde o contato com o chão -- ou seja, quando pula. A função ```Jump``` adiciona força e velocidade de forma a realizar o pulo desejado. Adicione as funções a seguir em seu script:

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

Com as configurações acima, você poderá perceber que o pulo do personagem está alcançando uma altura muito alta. Uma forma de evitar isso é selecionar o objeto do personagem na cena, ir até seu ```Inspector``` e configurar na seção ```Rigidbody2D``` a gravidade do corpo para um valor adequado >= 1.
![Alteração da gravidade para o corpo do personagem](images/3/14.png?raw=true "Alteração da gravidade para o corpo do personagem")

Com este valor, os pulos serão mais condizentes com o comportamento esperado.

Entretanto, até este momento nosso personagem não se movimenta de forma natural. Para isso, devemos criar animações.

### Criando a animação do personagem

Selecione novamente o objeto "Char" e abra a aba ```Animation``` no seu Unity. Você verá uma tela como a seguir:
![Solicitação de criação de animação para o objeto selecionado](images/3/6.png?raw=true "Solicitação de criação de animação para o objeto selecionado")

Clique no botão ```Create```. Um menu irá aparecer na sua tela orientando-o a escolher o nome da animação que será criada. Dê o nome de "Stand" à animação e salve-a na pasta ```Assets/Animations```. Se esta pasta não existir, a crie. Esta será a animação que o personagem terá quando estiver parado. No nosso caso, esta "animação" terá apenas um frame. Posteriormente, a animação de caminhada ("Walk") terá mais frames.

Agora, o seu menu de animação será similar ao seguinte:
![Menu de animações disponível](images/3/15.png?raw=true "Menu de animações disponível")

Podemos agora fazer as transições de frames para nosso personagem!
Clique no botão circular vermelho do lado superior esquerdo do menu de animações. Você verá o seguinte:
![Modo de criação de animações ativado](images/3/16.png?raw=true "Modo de criação de animações ativado")

Neste momento, você está no modo de criação de animações. Clicando numa marcação de tempo qualquer da lista, você poderá configurar o frame/sprite para este momento específico da animação. Clique no tempo ```0:00``` por agora. 
![Menu de animação na marcação 0:00](images/3/16.png?raw=true "Menu de animação na marcação 0:00")

Vá até ```Inspector``` do objeto e selecione o sprite que você quer que seja exibido neste momento de animação. Para isso, vá até o componente ```Sprite Renderer``` no canto direito de seu Unity e configure o frame/sprite desejado:
![Menu de animação na marcação 0:00](images/3/17.png?raw=true "Menu de animação na marcação 0:00")

Feito isso, como queremos inicialmente criar o "Stand" com apenas um frame, nosso trabalho está finalizado. Clicamos então no botão vermelho para salvar esta animação.

O passo seguinte efetivamente trabalhará o uso de animação. Podemos agora criar a animação de caminhada, para dar maior sensação de "vida" a nosso jogo. Para tal, clique no canto inferior esquerdo no botão com o nome da animação Stand criada acima. Um menu de opções surgirá. Clique em ```Create New Clip...```. Você verá novamente a tela inicial de animação:
![Menu de animações disponível](images/3/7.png?raw=true "Menu de animações disponível")

Assim como na etapa anterior, você deve agora clicar no botão vermelho desta seção para iniciar o modo de criação. Feito isso, você verá uma tela como a seguinte:
![Menu de animações disponível](images/3/8.png?raw=true "Menu de animações disponível")

A sequência de marcações de tempo exibida nesta tela representa os momentos de animação de nosso personagem. Clicando em alguma das marcações, poderemos escolher o frame estático que deve ser exibido neste momento de animação escolhido. Sendo assim, clique na marcação ```0:10```. Vá até o ```Inspector``` no canto direito do seu Unity e selecione o frame desejado, da mesma forma que fizemos no passo anterior.

Agora, podemos configurar o frame a ser exibido em alguma outra marcação de tempo. Você pode selecionar ```0:20``` e escolher uma imagem distinta da anterior. Esta configuração já é suficiente para perceber a movimentação do sprite. Caso queira, continue configurando mais frames em marcações de tempo. Quando tiver finalizado, clique novamente no botão vermelho para desativar o modo de criação de animações. Na mesma tela, você poderá visualizar um preview da animação criada. Clique no botão ```Play``` na mesma linha do botão vermelho de criação de animação e verifique se o resultado é o desejado.

Repare que, a qualquer momento, pode-se abrir novamente a aba "Animation" de seu Unity e alterar quaisquer animações criadas para seus personagens.

Pronto! Conseguimos criar nossa animação básica. Mas e agora, como poderemos vê-la enquanto jogamos? E melhor, como as animações são trocadas dependendo do estado de nosso objeto? É o que veremos a seguir!

## Controlando o estado das animações

Abra a aba ```Project```. Verifique que dentro de sua pasta ```Assets/Animations``` existe um objeto com o nome de seu personagem.
![Menu de animações disponível](images/3/20.png?raw=true "Menu de animações disponível")

Esse é o nosso [Animator](https://learn.unity.com/tutorial/controlling-animation). Por meio do ```Animator``` poderemos configurar as transições de animação do nosso personagem. Dê um duplo clique neste componente. Você verá uma tela como a seguir:
![Menu de animações disponível](images/3/21.png?raw=true "Menu de animações disponível")

Os quadrados na imagem representam os estados de animação, e a transição representa uma mudança de animação. O estado "Entry" não representa animação alguma, e é apenas o estado inicial do objeto. "Any State" é um estado em que quaisquer transições saindo dele representam mudanças de animação que podem ocorrer independentemente do estado atual de animação. A sentra "Entry -> Stand" em laranja representa que "Stand" é a animação padrão do nosso objeto. Para saber mais sobre o Animator, leia a documentação do Unity. Como nosso exemplo é bem simples, usaremos o Animator apenas para alternar entre o personagem estático e caminhando.

Para tal, precisaremos manipular algumas variáveis em nosso script criado anteriormente para o personagem. Na aba ```Parameters```, clique no botão ```+```. Crie uma variável Float com nome "Speed" e valor 0:
![Menu de animações disponível](images/3/22.png?raw=true "Menu de animações disponível")

Agora, clique com o botão direito no estado "Stand" e crie uma transição para "Walk" com ```Make Transition```. Clique na transição criada e a configure da seguinte forma:
![Menu de animações disponível](images/3/23.png?raw=true "Menu de animações disponível")

Crie agora uma transição de "Walk -> Stand" e a configure assim:
![Menu de animações disponível](images/3/24.png?raw=true "Menu de animações disponível")

Nossas transições já estão configuradas no Animator. Só precisamos agora manipular a variável "Speed" dentro do script do personagem de forma que as transições sejam acionadas e as animações trocadas quando preciso.

Abra o ```Character.cs```. Certifique-se de deixar o método ```FixedUpdate``` da seguinte maneira - na prática, apenas uma linha é adicionada:

```csharp
void FixedUpdate()
{
    float h = Input.GetAxis("Horizontal");

    animator.SetFloat("Speed", Mathf.Abs(h));

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

Essa linha certifica que a variável "Speed" do Animator será configurada para um valor maior que 0.1 quando movimentarmos o personagem para qualquer direção (esquerda ou direita). Caso contrário, se o personagem estiver parado, "Speed" será um valor menor que 0.1.

Entre no ```Play Mode``` do Unity e teste o resultado.


* [Anterior](2-criação-do-tilemap.md)
* [Próxima](4-camera-e-background.md)
