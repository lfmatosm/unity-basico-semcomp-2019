## 5 - Inserindo inimigo

Nesta etapa, iremos introduzir um inimigo simples no mapa de jogo.

### Criando colisores nas plataformas

Nosso intuito é de que o inimigo movimente-se entre as pontas de uma plataforma ou região, e ao chegar em uma das pontas, troque de orientação e caminhe até a outra ponta. Para tal, o inimigo deve reconhecer quando chegar à ponta da plataforma ou região em que está movimentando-se. Isso pode ser feito por meio do uso de colisores. O Unity disponibiliza vários tipos de colisores, mas para esta etapa usaremos apenas o Circle Collider 2D.

Selecione o objeto ```TileMap_Base```. Consideremos uma plataforma qualquer em que desejamos inserir um inimigo. Vá até o ```Inspector``` e adicione dois componentes ```Circle Collider``` em seu tilemap base. Não se esqueça de marcar nos colisores a opção "IsTrigger". Ela será útil mais à frente.
![Localização do sprite no projeto](images/5/1.png?raw=true "Localização do sprite no projeto")

Para cada um desses dois componentes, altere suas proporções e posições por meio do botão![Localização do sprite no projeto](images/5/2.png?raw=true "Localização do sprite no projeto") localizado no ```Inspector```. Deixe cada colisor em cada lado da plataforma de interesse, de forma similar à figura seguinte:
![Localização do sprite no projeto](images/5/3.png?raw=true "Localização do sprite no projeto")

Você pode criar colisores em tantas plataformas quanto desejar.

Agora já podemos criar o objeto do inimigo. Mas ao invés de simplesmente instanciarmos um objeto da forma que fizemos com nosso protagonista, iremos criar um Prefab.

### Inserindo o inimigo

Um [Prefab](https://docs.unity3d.com/Manual/Prefabs.html) é um asset padrão que pode ser criado para nosso projeto todas as vezes que quisermos criar componentes reusáveis em nossas cenas. O Prefab garante que todas as suas instâncias terão o mesmo comportamento definido na configuração do Prefab, garantindo comportamento uniforme para esses objetos ao longo de uma cena.

Crie um objeto vazio na hierarquia de sua cena. Importe o arquivo ```goomba_dft.png``` encontrado [aqui]() dentro de seu projeto como foi feito para o sprite do protagonista, e realize o slice desse sprite sheet da mesma forma. Caso tenha alguma dúvida, [volte](3-inserindo-personagem.md) a este trecho do tutorial.

Feito isso, iremos criar as animações para o inimigo. Criaremos as animações "StandEnemy", "WalkEnemy" e "DeathEnemy", da mesma forma descrita anteriormente. A única diferença é que agora teremos também uma animação para representar a morte do inimigo.

Abra o Animator do objeto e o estruture da seguinte forma:
![Localização do sprite no projeto](images/5/4.png?raw=true "Localização do sprite no projeto") As transições "StandEnemy -> WalkEnemy" e "WalkEnemy -> StandEnemy" são criadas com configuração análoga às do protagonista. Até mesmo a variável "Speed" é usada da mesma forma, com as mesmas condições correspondentes. A única diferença é a trans. "Any State -> DeathEnemy", disparada por uma condição de "IsAlive=false" para a variável booleana "IsAlive".

Agora, criaremos dois colisores para o inimigo. Um ```Box Collider 2D``` e um ```Circle Collider 2D```. O primeiro será utilizado para detectar colisões que danificam o jogador, enquanto o segundo será utilizado para colisões em que o jogador elimina este inimigo. Coloque o ```Circle Collider 2D``` na parte superior do inimigo (em sua "cabeça") e o ```Box Collider 2D``` na parte inferior ("corpo"), da seguinte forma:
![Localização do sprite no projeto](images/5/6.png?raw=true "Localização do sprite no projeto")
![Localização do sprite no projeto](images/5/7.png?raw=true "Localização do sprite no projeto")

Crie um arquivo chamado ```SimpleEnemy.cs``` na sua pasta ```Assets/Scripts```. Associe seu objeto a este script. Crie uma pasta ```Assets/Prefabs``` em sua aba Project. Arraste o objeto SimpleEnemy de sua cena para dentro desta pasta. Pronto, o Prefab está criado!

### Alterando o comportamento do inimigo

Podemos modificar agora o script para controlar nosso inimigo. O intuito é de que nosso inimigo movimente-se entre as pontas de uma plataforma ou região, e ao chegar em uma das pontas, troque de orientação e caminhe até a outra ponta.

No arquivo ```SimpleEnemy.cs``` insira inicialmente o seguinte código:

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    public Animator animator;
    public float speed = 3f;
    private Rigidbody2D rb2d;
    private bool lookingToTheRight = true;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
}
```

Como pode-se perceber, esta é a apenas a definição de algumas variáveis do objeto associado. ```orientation``` será usada para direcionar o inimigo para a esquerda ou direita conforme for preciso, e ```lookingToTheRight``` será usada para trocar a orientação do sprite da mesma forma que fizemos no protagonista. Certifique-se de adicionar a instânica do Animator no componente do Prefab, na aba ```Inspector```:
![Localização do sprite no projeto](images/5/5.png?raw=true "Localização do sprite no projeto")

Para fazer o inimigo movimentar-se, inserimos o seguinte código:

```csharp
private void FixedUpdate() {
    animator.SetFloat("Speed", Mathf.Abs(speed));
    transform.Translate(Vector2.right * speed * Time.deltaTime);
}
```

Já podemos ao menos ver nosso inimigo movendo-se na tela! Entre no ```Play Mode``` e verifique que o inimigo irá andar pela plataforma mas não irá respeitar os colisores que criamos anteriormente. Isso se deve pois precisamos criar uma função adicional em nosso script:

```csharp
private void OnTriggerEnter2D(Collider2D other)
{
    if (other.gameObject.CompareTag("TileMap"))
        speed = -speed;
        if ((lookingToTheRight && speed < 0) || (!lookingToTheRight && speed > 0))
            FlipSprite();
}

void FlipSprite()
{
    lookingToTheRight = !lookingToTheRight;
    Vector3 scale = transform.localScale;
    scale.x *= -1;
    transform.localScale = scale;
}
```

Repare que ```FlipSprite``` é a mesma função existente em ```Character.cs```, apenas trouxemos para este script.

Agora o inimigo bate nos colisores e troca de orientação, sem cair das plataformas. Isso só é possível pois marcamos nos colisores "IsTrigger=true", o que possibilitou a definição da função ```OnTriggerEnter2D```.

### Colidindo com o inimigo

Anteriormente, definimos dois colisores dentro do nosso inimigo: ```Box Collider 2D``` e ```Circle Collider 2D```. Queremos que nosso protagonista possa matar os inimigos ao pular sobre eles. Ou seja, o colisor circular ficou na parte superior do inimigo. Já se o inimigo encostar lateralmente em nosso protagonista, queremos que ele perca uma vida. Sendo assim, o colisor retangular ficou na parte inferior do inimigo.

Portanto, precisamos, de alguma forma, distinguir com que tipo de colisor estamos colidindo assim que uma colisão ocorrer. Para isso podemos empregar o uso de variáveis públicas. Em seu ```SimpleEnemy.cs```, adicione as seguintes variáveis:

```csharp
public BoxCollider2D body;
public CircleCollider2D head;
private bool isAlive = true;
private float deathTime;
```

No ```Inspector``` do Prefab, certifique-se agora de que as variáveis públicas apontam para os colisores que criamos anteriormente:
![Localização do sprite no projeto](images/5/8.png?raw=true "Localização do sprite no projeto")

Inicialmente, iremos realizar a colisão em que o inimigo é destruído, ou seja, quando o protagonista pula na cabeça do inimigo. No script adicione a função:

```csharp
    public void Kill()
    {
        animator.SetBool("IsAlive", false);
        deathTime = animator.GetCurrentAnimatorStateInfo(0).length + Time.time;
        isAlive = false;
        body.isTrigger = true;
        rb2d.gravityScale = 0;
    }
```

Essa função será responsável por ativar a animação de morte que criamos anteriormente. Além disso, computa o momento em que o objeto do inimigo deverá ser destruído da tela (ou seja, após o fim da animação de morte). A variável ```isAlive``` será usada em nosso ```FixedUpdate``` da seguinte forma:

```csharp
private void FixedUpdate()
{
    if (!isAlive)
    {
        if (Time.time > deathTime) Destroy(gameObject);
    }
    else
    {
        animator.SetFloat("Speed", Mathf.Abs(speed));
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
```

Dessa maneira, o inimigo só irá se movimentar caso esteja vivo. Agora, iremos alterar o ``` SimpleEnemy.cs```. Adicione uma nova função neste script:

```csharp
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("TileMap"))
        {
            speed = -speed;
            if ((lookingToTheRight && speed < 0) || (!lookingToTheRight && speed > 0))
                FlipSprite();
        }
        if (other.GetType() == typeof(CircleCollider2D) && other.gameObject.CompareTag("Mario"))
        {
            this.Kill();
        }
    }
```

Essa função verifica se o protagonista atingiu a cabeça do inimigo. Caso positivo, o segundo é eliminado. ```GetComponent``` é um método que nos dá acesso ao script ```SimpleEnemy.cs``` associado ao inimigo. Essa função será alterada posteriormente para que possamos realizar a condição de "perder uma vida" para nosso personagem caso este toque no corpo do inimigo.

#### Mais informações

* [Detectando colisões no Unity](https://youtu.be/ZoZcBgRR9ns)
* [Collider2D, Unity Docs](https://docs.unity3d.com/ScriptReference/Collider2D.html)

##### Continuando...

* [Anterior](4-camera-e-background.md)
* [Próxima](6-manipulando-variaveis-do-personagem.md)
