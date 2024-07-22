namespace BookStore;
public sealed class Copies
{
    public bool IsValid => Value > 0;
    public int Value { get; }

    private Copies(int value)
    {
        Value = value;
    }

    public Copies AddCopies(int additionalCopies)
    {
        return additionalCopies > 0 ? new Copies(Value + additionalCopies) : this;
    }

    public static Copies Create(int value)
    {
        return value < 0 ? CreateEmptyCopies() : new Copies(value);
    }

    public static Copies CreateEmptyCopies()
    {
        return new Copies(0);
    }

    public Copies RemoveCopies(int soldCopies)
    {
        return soldCopies > 0 && Value >= soldCopies ? new Copies(Value - soldCopies) : this;
    }
}