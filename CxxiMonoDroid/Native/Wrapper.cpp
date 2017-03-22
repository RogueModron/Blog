#include <boost/geometry/geometry.hpp>
#include <boost/geometry/geometries/point_xy.hpp>
#include <boost/geometry/geometries/polygon.hpp>
#include <boost/geometry/multi/geometries/multi_polygon.hpp>

#include "Wrapper.h"


namespace ClippingLibrary
{


namespace ggl = boost::geometry;
using namespace ggl;
using namespace std;


class Wrapper::Implementation
{
public:
    Implementation():
        _pInputMultiPolygon(&_subject),
        _pInputRing(NULL)
    {
        //
    }

    ~Implementation()
    {
        //
    }

    void AddInnerRing()
    {
        vector<Ring>& inners = _pInputMultiPolygon->back().inners();
        inners.resize(inners.size() + 1);

        _pInputRing = &(inners.back());
    }

    void AddOuterRing()
    {
        _pInputMultiPolygon->resize(_pInputMultiPolygon->size() + 1);

        _pInputRing = &(_pInputMultiPolygon->back().outer());
    }

    void AddPoint(double x, double y)
    {
        _pInputRing->push_back(make<Point>(x, y));
    }

    void ExecuteDifference()
    {
        PrepareForOutput();
        difference(_subject, _clip, _result);
    }

    void ExecuteIntersection()
    {
        PrepareForOutput();
        intersection(_subject, _clip, _result);
    }

    void ExecuteUnion()
    {
        PrepareForOutput();
        union_(_subject, _clip, _result);
    }

    void ExecuteXor()
    {
        PrepareForOutput();
        sym_difference(_subject, _clip, _result);
    }

    bool GetInnerRing()
    {
        if (_pOutputInners->size() <= _outputInnersIndex)
            return false;
    
        _pOutputRing = &((*_pOutputInners)[_outputInnersIndex]);

        _outputInnersIndex++;
    
        _outputPointIndex = 0;
    
        return true;
    }
    
    bool GetOuterRing()
    {
        if (_result.size() <= _outputPolygonIndex)
            return false;
        
        Polygon& polygon = _result[_outputPolygonIndex];
        _pOutputRing = &(polygon.outer());
        _pOutputInners = &(polygon.inners());

        _outputPolygonIndex++;
    
        _outputInnersIndex = 0;
        _outputPointIndex = 0;
    
        return true;
    }
    
    bool GetPoint(double& x, double& y)
    {
        if (_pOutputRing->size() <= _outputPointIndex)
            return false;
        
        Point& point = (*_pOutputRing)[_outputPointIndex];
        x = point.x();
        y = point.y();

        _outputPointIndex++;
    
        return true;
    }

    void InitializeClip()
    {
        PrepareForInput(_clip);
    }

    void InitializeSubject()
    {
        PrepareForInput(_subject);
    }


private:
    typedef model::d2::point_xy<double>   Point;
    typedef model::ring<Point>            Ring;
    typedef model::polygon<Point>         Polygon;
    typedef model::multi_polygon<Polygon> MultiPolygon;


    MultiPolygon  _clip;
    MultiPolygon  _result;
    MultiPolygon  _subject;
 
    MultiPolygon* _pInputMultiPolygon;
    Ring*         _pInputRing;

    unsigned int  _outputInnersIndex;
    unsigned int  _outputPointIndex;
    unsigned int  _outputPolygonIndex;

    vector<Ring>* _pOutputInners;
    Ring*         _pOutputRing;


    void PrepareForInput(MultiPolygon& operand)
    {
        operand.clear();
        _pInputMultiPolygon = &operand;
        _pInputRing = NULL;
    }

    void PrepareForOutput()
    {
        _result.clear();
        _outputInnersIndex = 0;
        _outputPointIndex = 0;
        _outputPolygonIndex = 0;
        _pOutputInners = NULL;
        _pOutputRing = NULL;
    }
};


Wrapper::Wrapper():
    _pImpl(new Implementation)
{
    //
}

Wrapper::~Wrapper()
{
    delete _pImpl;
}

void Wrapper::AddInnerRing()
{
    _pImpl->AddInnerRing();
}

void Wrapper::AddOuterRing()
{
    _pImpl->AddOuterRing();
}

void Wrapper::AddPoint(double x, double y)
{
    _pImpl->AddPoint(x, y);
}

void Wrapper::ExecuteDifference()
{
    _pImpl->ExecuteDifference();
}

void Wrapper::ExecuteIntersection()
{
    _pImpl->ExecuteIntersection();
}

void Wrapper::ExecuteUnion()
{
    _pImpl->ExecuteUnion();
}

void Wrapper::ExecuteXor()
{
    _pImpl->ExecuteXor();
}
    
bool Wrapper::GetInnerRing()
{
    return _pImpl->GetInnerRing();
}

bool Wrapper::GetOuterRing()
{
    return _pImpl->GetOuterRing();
}

bool Wrapper::GetPoint(double& x, double& y)
{
    return _pImpl->GetPoint(x, y);
}

void Wrapper::InitializeClip()
{
    _pImpl->InitializeClip();
}

void Wrapper::InitializeSubject()
{
    _pImpl->InitializeSubject();
}


}