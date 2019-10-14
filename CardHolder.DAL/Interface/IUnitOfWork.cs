using System;

namespace CardHolder.DAL.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}