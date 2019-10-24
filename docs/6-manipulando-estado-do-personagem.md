## 6 - Manipulando estado do personagem

Nesta etapa, iremos introduzir funções de morte e spawn para o personagem.

### Criando variáveis para controlar o comportamento do personagem.

```csharp
  private bool isDead = false; #controle para o estado atual.
  public BoxCollider2D body; #referência aos colisores do personagem.
  public CircleCollider2D feet;
  Transform target; #posição alvo do personagem ao morrer.
  public Sprite deathSprite; #sprite de morte do personagem.
  SpriteRenderer spriteRenderer; #referência ao spriterenderer do personagem
```

O sprite pode ser encontrado [aqui](https://drive.google.com/drive/folders/1JvF5ncDJGAbjktF3B4yVo5NbdJx1Rgel?usp=sharing).
Para atribuir os valores dessas variáveis vamos associar seus respectivos objetos na interface da unity(por isso variáveis públicas) e adicionaremos ao método Awake() a seguinte linha:

```csharp
spriteRenderer = GetComponent<SpriteRenderer>();
```

No método ```FixedUpdate()``` adicionaremos uma estrutura de if/else para controlar quando o personagem estiver morto ou não.

```csharp
    void FixedUpdate()
    {
        if (isDead)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 0.1f * Time.deltaTime);
        }
        else
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
    }
```
Por fim, teremos uma função de morte:

```csharp
    public void Death()
    {
        isDead = true;
        target = transform;
        target.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        animator.enabled = false;
        rb2d.velocity = new Vector3(0, rb2d.velocity.y);
        spriteRenderer.sprite = deathSprite;
        body.isTrigger = true;
        feet.isTrigger = true;
    }
```

Nessa função, pegamos a posição que será usada para animar a morte, desativaremos animações e triggers, travaremos o eixo X e trocaremos o sprite.


Agora precisamos que a morte seja acionada:

Adicionaremos colisores em todos os buracos da fase.
![Colisores do vazio](images/6/1.jpg?raw=true "Void")

Todos eles com a tag ```Void```, para podermos adicionar esse if no OnCollisionEnter2D do personagem.

```csharp
if (other.gameObject.CompareTag("Void"))
{
    Death();
}
```

E para o inimigo adicionaremos a seguinte função:
```csharp
private void OnCollisionEnter2D(Collision2D other)
{
    if (other.gameObject.CompareTag("Mario")){
        other.gameObject.GetComponent<Character>().Death();
    }
}
```
### Respawnando o personagem.

Para respawnar primeiro criaremos uma função de ```Respawn()```, revertando a função ```Death()```.

```csharp
void Spawn()
{
    isDead = false;
    animator.enabled = true;
    body.isTrigger = false;
    feet.isTrigger = false;
    transform.position = spawnPosition;
}
``` 

Adicionaremos esse if no ```FixedUpdate()``` para que o personagem respawne ao apertar Enter.

```csharp
if (Input.GetKeyDown("return"))
    Spawn();
```

O último detalhe será adicionar uma variável com a posição inicial da fase.
```csharp
Vector3 spawnPosition = new Vector3(-1.7f, -1.8f, 0);
```

Com essas alterações o personagem poderá morrer e renascer!

##### Fim!

* [Anterior](5-inserindo-inimigo.md)
