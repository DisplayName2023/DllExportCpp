#include "gtest/gtest.h"



#include <iostream>


int main(int32_t argc, char* argv[])
{
    std::cout << "Unit Test" << std::endl;


	testing::InitGoogleTest(&argc, argv);

	int returnValue = RUN_ALL_TESTS();
	std::cout << "Tests Finished with return value " << returnValue << std::endl;

    return 0;
}



