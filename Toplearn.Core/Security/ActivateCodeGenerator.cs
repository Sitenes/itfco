using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toplearn.Core.Security
{
	public static class CodeGenerator
	{
		public static string ActivatePhone()
		{
            Random rnd = new Random();
            return rnd.Next(10000, 100000).ToString(); // Generate a number between 10000 and 99999 for Phone Activate Code.
        }
	}
}
