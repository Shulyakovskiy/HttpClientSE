using System;
using JetBrains.Annotations;

namespace Infrastructure.ObjectExtension.ObjectInstance
{
    [UsedImplicitly]
    public static class ClassInstance 
    {
        private static readonly object Lock = new object();

        /// <summary>
        /// Singleton Safety Thread
        /// Создает экземпляр класса(потоко безопастный)
        /// </summary> 
        [NotNull]
        public static T CreateInstance<T>(T instance) where T : new()
        {
            try
            {
                if (instance == null)
                {
                    lock (Lock)
                    {
                        if (instance == null)
                            instance = new T();
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                throw new Exception(string.Format("{0}", ex.Message));
            }
            return instance;
        }
    }
}