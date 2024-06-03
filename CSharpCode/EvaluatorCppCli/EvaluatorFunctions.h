#pragma once
#include "EvaluatorCppCliExports.h"


#if defined __cplusplus
extern "C" {
#endif

    EVALUATORCPP_API int Evaluator_calcuate2(char* expression, size_t expressionLength, double x, double y, double* result);
    EVALUATORCPP_API int Evaluator_calcuate(char* expression, size_t expressionLength, double x, double* result);



#if defined __cplusplus
}
#endif