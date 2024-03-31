using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reassessASE
{
    public interface ICanvas
    {
        void Circle(int radius);
    }

    public class Canvas: ICanvas
    {
        //Standard size of canvas
        const int XSIZE = 640;
        const int YSIZE = 480;
    }
}
