using System.Data.Objects;
using CardHolder.DAL;
using CardHolder.DAL.Base;
using CardHolder.DAL.Interface;
using StructureMap;

namespace CardHolder.BAL
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public static class GeneralManager
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <remarks></remarks>
        public static void Initialize()
        {
            // Hook up the interception
            ObjectFactory.Initialize(
                x =>
                {
                    x.For<IUnitOfWorkFactory>().Use<EFUnitOfWorkFactory>();
                    x.For(typeof(IRepository<>)).Use(typeof(EFRepository<>));
                }
            );

            // We tell the concrete factory what EF model we want to use
            EFUnitOfWorkFactory.SetObjectContext(() => new BOBCardEntities());
        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        /// <remarks></remarks>
        public static void Commit()
        {
            UnitOfWork.Commit();
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        /// <remarks></remarks>
        public static void Dispose()
        {
            UnitOfWork.Current.Dispose();
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static ObjectContext GetContext()
        {
            return ((EFUnitOfWork)UnitOfWork.Current).Context;
        }
    }
}