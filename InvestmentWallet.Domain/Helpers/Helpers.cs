using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentWallet.Domain.Helpers
{
    public class Helpers<TEntity>
        where TEntity : class
    {
        public static bool isSameObject(TEntity oldInstance, TEntity newInstance)
        {
            var oldProperties = oldInstance.GetType().GetProperties();
            var newProperties = newInstance.GetType().GetProperties();
            foreach (PropertyInfo info in oldProperties)
            {
                if (info.GetValue(oldInstance, null) != info.GetValue(newInstance, null))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
