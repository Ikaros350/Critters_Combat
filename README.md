# Critters_Combat
Integrantes de este trabajo

Julian Jaramillo Vargas

Santiago Cortes Monsalve

Mateo Orrego 

Cosideraciones a la hora de realizar el trabajo:

Clases sin cambios
[Affiniy]

[Skill] abstract

[AttackSkill]

[SupportSkill]

MonoBehaviour
[Observer] abstract
MonoBehaviour
[PlayerBase]
MonoBehaviour
[Player]
MonoBehaviour
[Referee]
MonoBehaviour
[UIManager]
MonoBehaviour
[EnemyLogic]
MonoBehaviour
[PoolObject]

Interfaz

[IPool]

---------------------------------------------------------Antiguo proyecto----------------------------------------------------------------------------------------------

Se tomo la decisión de utilizar un diseño de clases adicionales, con respecto a las otorgadas por el profesor. 
Se decidió el uso de arreglos para almacenar estas afinities entre todas las demás estructuras de datos, 
debido a que en el diseño que se nos entregó, ya conocíamos un tamaño especifico de affinities, 
y no se mencionó que este podía ser expandible, por ende, el uso de fue una solución rápida y fácil de implementar

Se decidió crear una clase abstracta llamada [Skill] la cual tenía como atributos, 
el nombre y el poder de la habilidad, no se incluyó la afinidad en esta super-clase debido a que [SupportSkill] 
no le afectaban en nada que tuviera o no el atributo affinity, por ende, decidimos no sobre cargar esa clase con un atributo que no se usaría. 
A partir de esta se generaron dos sub-clases [AttackSkill] y [SupportSkill], las cuales heredaban los atributos de la super clase [Skill], 
pero los implementaban específicamente en el constructor de [AttackSkill] y [SupportSkill]. 
Además, la clase [AttackSkill] contenía un atributo adicional llamado affinity, debido a que el diseño que se nos fue entregado que necesitaba la affinity de dicha clase.

En la clase [Critter] se tomó la decisión de generar 3 atributos adicionales llamados (currentSpd, currentAtq, currentDef); 
los cuales actúan como los stats alterables en combate, sin contar el Hp de la clase [Critter}, tomamos esta decisión, 
debido a que el diseño necesitaba conservar los valores base de cada estadística del [Critter], 
y se hizo una analogía con el juego de Pokémon en el que usan atributos temporales en cada combate

--------------------------------------------------------------------------------------------------------------------------------------------------------------------
La clases que agregamos fueron [Referee], [UIManager], [EnemyLogic], [PoolObject], una super clase llamada [PlayerBase] y una interfaz llamada [IPool]

[EnemyLogic] la cual es la logica que lleva al enemigo atomar decisiones en cada combate.

[PlayerBase] esta superclase se vio ultilizada ya que el necesitar un IA [EnemyLogic] con la cual el jugador deberia enfrentarse, esta debia tener algunos elementos que alteriormenete tenia la clase [Player], por ende se decidio que lo mas optimo seria que ambos hereden dichas caracteristicas

[Referee] del manejo de turnos entre el jugador[Player] y el enemigo [EnemyLogic], y tambien registra como se realizaron los ataques, ademas de ordenar que los jugadores que deben cambiar de critter cuando este ya no pueda pelear, llevar un registro de los ataques en cada turno, y quien fue el ganador, como en pokemon.

[UIManager] es la clase encargada del manejo de la UI con la cual el jugador interactua y la actualizacion en tiempo real de cosas como la vida, afinidad, los ataques de los critters y sus habilidades.

[Critters] ahora los critters se inicialian sus habilidades al ser creados, esto por facilidad para utilizar un "Pooling".

[PoolObject] y [IPool] se utilizo este patron debido a que se vio la necesidad de tener un mejor manejo en la inicializion de los Critters, ademas esta decision se tomo como la idea de utilizar la mecanica de "Pokémon Stadium", en el cual los "pokemones" son predefinidos y los jugadores acceden a ellos, y una ultima razon es que por la facilidad que haria esto, si se quisiera alargar el juego, ya que no habria que crear mas Critters en el tiempo real de juego, eso si actualmente no regresan debido a la partida acaba caundo un jugador se queda sin critters que utilizar, ademas de la actual restriccion de que el jugador se queda con los critters que derroto.

[Observer] este patron fue ultilizado para llevar acabo un manejo adecuado del contador de Critters derrotados, ya que si los combates en algun momento fueran modificados, aun permaneceria la condicion de victoria.


