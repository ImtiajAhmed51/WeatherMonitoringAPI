using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public sealed class WeatherContextSingleton
    {
        private static WeatherContext instance = null;
        private static readonly object locked = new object();

        private WeatherContextSingleton() { }


        public static WeatherContext GetInstance()
        {

            if (instance == null)
            {
                lock (locked)
                {
                    if (instance == null)
                    {
                        instance = WeatherContext.Create();

                    }
                }
            }
            return instance;

        }
    }
}
