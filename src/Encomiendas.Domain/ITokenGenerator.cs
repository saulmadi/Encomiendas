namespace Encomiendas.Domain
{
    public interface ITokenGenerator<out T>
    {
        T Generate();
    }
}