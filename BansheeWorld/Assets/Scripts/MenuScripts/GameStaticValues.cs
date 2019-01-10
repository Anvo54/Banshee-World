
public static class GameStaticValues
{
    public static bool multiplayer = true;

    public static Character player1 = Character.Character1;
    public static Character player2 = Character.Character1;
    public static Character bot = Character.Character1;

    public static Level level = Level.Level1;
}

public enum Character
{
    Character1, Character2, Character3, Character4
}

public enum Level
{
    Level1, Level2, Level3
}

