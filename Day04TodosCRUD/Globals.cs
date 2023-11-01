using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04TodosCRUD
{
    public class Globals
    {
        public static TodoDbContext dbContext;

        //Singleton Pattern
        private static TodoDbContext _dbContextInternal;
        public static TodoDbContext dbContextAuto  // Exceptions
        {
            get
            {
                if(_dbContextInternal == null)
                { 
                    _dbContextInternal = new TodoDbContext(); 
                }
                return _dbContextInternal;


            }
        }
    }
}
