cmake_minimum_required(VERSION 3.28)

#==============================================================================
# set(CMAKE_OSX_DEPLOYMENT_TARGET "10.15" CACHE STRING "Minimum OS X deployment version")
set(CMAKE_XCODE_GENERATE_SCHEME TRUE) 

project( EvaluatorCpp LANGUAGES CXX)

set( CMAKE_CXX_STANDARD 20 )



set( INTERPROCEDURAL_OPTIMIZATION OFF )
set( CMAKE_WINDOWS_EXPORT_ALL_SYMBOLS OFF )

set(CMAKE_MAP_IMPORTED_CONFIG_DEBUG RELEASE)
set(CMAKE_MAP_IMPORTED_CONFIG_RELWITHDEBINFO RELEASE)

# find_package(xxxx REQUIRED)
# message(STATUS "xxxx_INCLUDE_DIRS: " ${xxxx_INCLUDE_DIRS})
# message(STATUS "xxxx_LIBRARIES: " ${xxxx_LIBRARIES})

add_subdirectory(src)


if( IS_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}/test )
    add_subdirectory(test)
endif()
