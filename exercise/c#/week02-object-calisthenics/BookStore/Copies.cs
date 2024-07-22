namespace BookStore;
public sealed class Copies
{
    public bool IsValid => Value > 0;
    public readonly int Value;

    private Copies(int value) => Value = value;

    public Copies AddCopies(int additionalCopies) => additionalCopies > 0 ? new Copies(Value + additionalCopies) : this;

    public static Copies Create(int value) => value <= 0 ? CreateEmptyCopies() : new Copies(value);

    public static Copies CreateEmptyCopies() => new(0);

    public Copies RemoveCopies(int soldCopies) => soldCopies > 0 && Value >= soldCopies ? new Copies(Value - soldCopies) : this;
}