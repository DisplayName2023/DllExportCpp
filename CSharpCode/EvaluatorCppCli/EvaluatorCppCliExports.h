#pragma once


#pragma once

/* clang-format off */
#if defined(EvaluatorCppCli_STATIC)

#if defined(EvaluatorCppCli_EXPORTS)
#    define EVALUATORCPP_API          __declspec(dllexport) 
#else
#  define EVALUATORCPP_API
#endif

#elif defined(_WIN32)

#  if defined(EvaluatorCppCli_EXPORTS)
#    define EVALUATORCPP_API          __declspec(dllexport)   // should not be exported in Macro because the project has export all functions
#  else
#    define EVALUATORCPP_API          __declspec(dllimport)   // should not be used for class with inline implmentations

#  endif
#endif
/* clang-format on */

