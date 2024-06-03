
#include "gtest/gtest.h"

#include "Evaluator.h"

using namespace EvaluatorCppCli;



TEST(Evaluator, calculate)
{
	auto result = Evaluator::calcuate("sin(x) * 2", 3.1415 / 2);
	if (result.has_value())
	{
		std::cout << result.value() << std::endl;
	}
	
}



