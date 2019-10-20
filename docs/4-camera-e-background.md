
## 4 - Movimentação da Câmera e Background

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

### Background

Na pasta ```Assets/Resources/Background``` de nosso projeto, iremos colocar o sprite sheet ```background.png``` que pode ser encontrado [aqui](https://drive.google.com/drive/folders/1JvF5ncDJGAbjktF3B4yVo5NbdJx1Rgel?usp=sharing).

Lembrando de colocar Pixels per Unit = 16 para manter a proporção com o resto dos sprites

Como Mario não conta com o efeito de parallax, vamos apenas adicionar o background no inicio da cena e cloná-lo até preencher todo cenário. Ctrl + C e Ctrl + V para copiar, arrastamos para o lado segurando Ctrl para eles se fixarem corretamente.

![Localização do sprite no projeto](images/4/7.png?raw=true "Cenário final")

##### Continuando...

* [Anterior](3-inserindo-personagem.md)
* [Próxima](5-inserindo-inimigo.md)
