namespace CustomStringInterpolation
{
    public interface IGetCompositeFormatStringDescriptionAction
    {
        ICompositeFormatStringDescription Execute(string interpolatedFormatValue);
    }
}
