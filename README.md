|Behavior|Input|Output|
|----|------|-----|
|App accepts values "Rock","Paper", "Scissors" or first letters thereof |"r"| Iterpretes player uses Rock|
|Values not equivalent to expected result in error message  and ask player to input again | "Sock" | "Please try again"|
|App declares both players must input|P1:"",P2:"scissors"|Waits for P1 input|
|App declares draw when both players select the  same value|P1: S, P2: S |"Draw"|
|App determines winning player using the following value precedence: Rock > Scissors > Paper > Rock |P1: Rock; P2: Scissors | P1 value wins |
|App enables multi-round play with same group of players | How many rounds? 3 | Initializes game of 3 rounds |
|App enables multiplayer play e.g. 2+ players | How many players? 3 |Initializes game for 3 players|
|App enables single play | How many players? 1 |Initializes game for 1 player|
|Console hides user input| Input: Scissors| Console: ...|