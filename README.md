# Critters_Combat
Integrantes de este trabajo

Julian Jaramillo Vargas

Santiago Cortes Monsalve

Mateo Orrego 

Cosideraciones a la hora de realizar el trabajo:

Clases [Affiniy]

[Skill] abstract

[AttackSkill]

[SupportSkill]

[PlayerBase]

[Player]

[Referee]

[UIManager]

[AIEnemy]

Se tomo la decisión de utilizar un diseño de clases adicionales, con respecto a las otorgadas por el profesor. 
Se decidió el uso de arreglos para almacenar estas afinities entre todas las demás estructuras de datos, 
debido a que en el diseño que se nos entregó, ya conocíamos un tamaño especifico de affinities, 
y no se mencionó que este podía ser expandible, por ende, el uso de fue una solución rápida y fácil de implementar

La clase que agregamos fue [Affinity]; esta clase se encarga de almacenar en un arreglo las afinidades y el multiplicador de affinity en los combates.

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
