using JetBrains.Annotations;
using Microsoft.Practices.Unity;

namespace Infrastructure.ObjectExtension.DependencyResolver
{
    [UsedImplicitly]
    public static class Bootstrapper
    {
        [UsedImplicitly]
        private static UnityContainer _unityContainerInstance;

        /// <summary>
        /// Загрузчик зависимостей в проекте
        /// </summary>
        /// <returns>Instance Unity Container</returns>
        public static UnityContainer Container()
        {
            var unityContainer = new UnityContainer();
            RegisterService(unityContainer);

            return unityContainer;
        }

        private static void RegisterService(UnityContainer unityContainer)
        {
            #region <<<Register Services Dependencies IOC Container  Microsoft Unity>>>

           // unityContainer
             

            #endregion
        }
    }
}