using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrigorificosBle.Almacen.Core.Util
{
    public class NotProductsInStockException : Exception
    {
        public NotProductsInStockException()
        {

        }

        public NotProductsInStockException(string message)
            : base(message)
        {
        }

        public NotProductsInStockException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}
