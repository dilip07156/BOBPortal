namespace CardHolder.DAL.Interface
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}