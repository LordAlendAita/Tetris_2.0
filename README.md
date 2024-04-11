``` mermaid
classDiagram
    class Spieler {
        +Account: string
        +Passwort: string
        +TAG: string
        +Von_Administrator_Löschbar: bool
    }

    class LogInScreen {
        +Username: string
        +Passwort: string
        +Error_Meldung: string
    }

    class Tetris {
        +game
        +score
    }

    class Leaderboard {
        +Top_5_highscore: int[]
        +player_Tag: string[]
        +player_Score: int[]
        +repeat_boolean: bool
    }

    Spieler "1" --> "1..1" LogInScreen : verwendet
    Tetris "1" --> "1" Spieler : hat
    Leaderboard "1" --> "0..1" Tetris : verwendet
```
