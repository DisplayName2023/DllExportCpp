#include "EvaluatorFunctions.h"

#include "EvaluatorManaged.h"

#include <string>
#include <msclr/marshal_cppstd.h>

#pragma comment(lib, "mscoree.lib")
int Evaluator_calcuate2(char* expression, size_t expressionLength, double x, double y, double* result)
{
    auto exprNative = std::string(expression, expression + expressionLength);
    auto exprClr = msclr::interop::marshal_as<System::String^>(exprNative);

    auto eval = gcnew EvaluatorManaged();
    auto nullOrResult = eval->Calculate(exprClr, x, y);
    if (nullOrResult.HasValue == false)
    {
        return -1;
    }

    *result = nullOrResult.Value;

    return 0;
}

int Evaluator_calcuate(char* expression, size_t expressionLength, double x, double* result)
{
    return Evaluator_calcuate2(expression, expressionLength, x, 0, result);
}
