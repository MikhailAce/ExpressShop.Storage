using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressShop.Storage
{
    /// <summary>
    /// Содержит локальные настройки системы. Синглтон.
    /// </summary>
    internal class LocalConfiguration
    {
        private static LocalConfiguration _instance;

        /// <summary>
        /// Используемый экземпляр настроек системы.
        /// </summary>
        public static LocalConfiguration Instance
        {
            get
            {
                return _instance != null 
                    ? _instance 
                    : _instance = new LocalConfiguration();
            }
        }

        /// <summary>
        /// Приватный конструктор для гарантии Singletone.
        /// </summary>
        private LocalConfiguration()
        {
        }

        /// <summary>
        /// Строка подключения к базе данных.
        /// </summary>
        public string ConnectionString 
            => ConfigurationManager.ConnectionStrings["ExpressShop.Storage.ConnectionString"].ConnectionString;

    }
}
