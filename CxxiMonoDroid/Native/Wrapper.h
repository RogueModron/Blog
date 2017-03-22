#if defined(_MSC_VER)
    #define EXPORT __declspec(dllexport)
#else
    #define EXPORT
#endif

namespace ClippingLibrary
{


class EXPORT Wrapper
{
public:
    Wrapper();
    ~Wrapper();
    
    void AddInnerRing();
    void AddOuterRing();
    void AddPoint(double x, double y);

    void ExecuteDifference();
    void ExecuteIntersection();
    void ExecuteUnion();
    void ExecuteXor();
    
    bool GetInnerRing();
    bool GetOuterRing();
    bool GetPoint(double& x, double& y);

    void InitializeClip();
    void InitializeSubject();
    
private:
    class Implementation;
    Implementation* _pImpl;
};


}