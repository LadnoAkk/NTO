using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Cultura.data;

namespace Cultura.Helper
{
    public static class Connect
    {
        public static CulturnaBasaContext context = new CulturnaBasaContext();
        public static Window MainMainWindow = null;
    }
}
