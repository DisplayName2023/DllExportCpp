#pragma once

#include "EvaluatorFunctions.h"

#include <optional>
#include <string>

namespace EvaluatorCppCli
{

	class Evaluator
	{
	public :
		static std::optional<double> calcuate(const std::string& expression, double x, double y)
		{
			double result;
			if (0 == Evaluator_calcuate2((char*)expression.c_str(), expression.size(), x, y, &result))
			{
				return result;
			}
			return {};
		}
		static std::optional<double> calcuate(const std::string& expression, double x)
		{
			return calcuate(expression, x, 0);
		}
	};

}