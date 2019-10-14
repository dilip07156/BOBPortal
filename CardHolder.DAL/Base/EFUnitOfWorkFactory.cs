using System;
using System.Data.Objects;
using CardHolder.DAL.Interface;

namespace CardHolder.DAL.Base
{
    public class EFUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private static Func<ObjectContext> _objectContextDelegate;
        private static readonly Object _lockObject = new object();

        public static void SetObjectContext(Func<ObjectContext> objectContextDelegate)
        {
            _objectContextDelegate = objectContextDelegate;
        }

        public IUnitOfWork Create()
        {
            ObjectContext context;

            lock (_lockObject)
            {
                context = _objectContextDelegate();
            }

            return new EFUnitOfWork(context);
        }
    }
}