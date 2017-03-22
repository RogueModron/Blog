#include <iostream>
#include <sstream>

#include "Wrapper.h"


using namespace ClippingLibrary;
using namespace std;


string Test()
{
    Wrapper w;
    w.AddOuterRing();
    w.AddPoint(0, 0);
    w.AddPoint(0, 1000);
    w.AddPoint(1000, 1000);
    w.AddPoint(1000, 0);
    w.AddPoint(0, 0);
    w.AddInnerRing();
    w.AddPoint(100, 100);
    w.AddPoint(900, 100);
    w.AddPoint(900, 200);
    w.AddPoint(100, 200);
    w.AddPoint(100, 100);
    w.AddInnerRing();
    w.AddPoint(100, 700);
    w.AddPoint(900, 700);
    w.AddPoint(900, 900);
    w.AddPoint(100, 900);
    w.AddPoint(100, 700);
    w.InitializeClip();
    w.AddOuterRing();
    w.AddPoint(-20, 400);
    w.AddPoint(-20, 600);
    w.AddPoint(1020, 600);
    w.AddPoint(1020, 400);
    w.AddPoint(-20, 400);
    w.AddInnerRing();
    w.AddPoint(100, 450);
    w.AddPoint(900, 450);
    w.AddPoint(900, 550);
    w.AddPoint(100, 550);
    w.AddPoint(100, 450);
    w.ExecuteDifference();

    stringstream s;
    while (w.GetOuterRing())
    {
        s << "(";

        s << "(";

        double x;
        double y;
        while (w.GetPoint(x, y))
        {
            s << "(" << x << ";" << y << ")";
        }

        s << ")";

        while (w.GetInnerRing())
        {
            s << "(";

            while (w.GetPoint(x, y))
            {
                s << "(" << x << ";" << y << ")";
            }

            s << ")";
        }

        s << ") ";
    }
    return s.str();
}

int main()
{
    cout << Test();
    return 0;
}
