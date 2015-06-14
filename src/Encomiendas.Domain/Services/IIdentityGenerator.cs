namespace Encomiendas.Domain.Services
{
    public interface IIdentityGenerator<out T>
    {
        T Generate();
    }
}