using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegest.Tests
{
    public class ExpressionSolverTest
    {
        public static void SimpleExpressions()
        {
            string expr = "(12-2)*-2/0";
            decimal value  = TaxService.Manager.Helpers.ExpressionSolver.Evaluate(expr);
        }
    }
}
