public interface ILevelParser<OutputData>
{
    OutputData ParseLevelFromString(string level);
}
