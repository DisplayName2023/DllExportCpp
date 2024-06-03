#include "EvaluatorManaged.h"



System::Nullable<double> EvaluatorManaged::Calculate(System::String^ expression, double x, double y)
{
    EvaluatorCs::Calculator^ c = gcnew EvaluatorCs::Calculator();
    return c->Calculate(expression, x, y);


}
