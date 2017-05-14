using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StdBlog.Helper;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> lis = new List<int>() { 0, 1, 12, 456561615, 4444 };
            var str=UserFollows.Encode(lis);
            int a = 0;
            var t = UserFollows.Decode(str);
            a = 0;
        }
    }
}
