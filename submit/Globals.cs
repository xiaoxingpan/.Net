using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPizzaOrders
{
    internal class Globals
    {
        static internal PizzaOrderDbContext dbContext;

        private static PizzaOrderDbContext _dbContextInternal;
        public static PizzaOrderDbContext dbContextAuto  
        {
            get
            {
                if (_dbContextInternal == null)
                {
                    _dbContextInternal = new PizzaOrderDbContext();
                }
                return _dbContextInternal;


            }
        }
    }
}
