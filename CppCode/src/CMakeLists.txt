cmake_minimum_required(VERSION 3.28)


set( CMAKE_CXX_STANDARD 20 )

configure_file("Tools/Version.rc.in" "${CMAKE_CURRENT_BINARY_DIR}/Version.rc")
message("${PROJECT_NAME}: ${VER_MAJOR}.${VER_MINOR}.${VER_PATCH}.${VER_TWEAK}")

# file( GLOB_RECURSE _headers RELATIVE ${CMAKE_CURRENT_LIST_DIR} "*.hpp" )
file( GLOB_RECURSE _headers RELATIVE ${CMAKE_CURRENT_LIST_DIR} "*.h" )
file( GLOB_RECURSE _md_files RELATIVE ${CMAKE_CURRENT_LIST_DIR} "*.md" )

set( ALL_SOURCE 
    main.cpp
    ${CMAKE_CURRENT_BINARY_DIR}/Version.rc
)

add_library( ${PROJECT_NAME} 
    ${ALL_SOURCE}
    ${_headers}
    ${_md_files}
    )

source_group(TREE "${PROJECT_SOURCE_DIR}" PREFIX "Header Files" FILES ${_headers} ${_md_files})
source_group(TREE "${PROJECT_SOURCE_DIR}" PREFIX "Source Files" FILES ${ALL_SOURCE})


target_include_directories(${PROJECT_NAME} 
    PUBLIC ${CMAKE_CURRENT_SOURCE_DIR}/src
    PUBLIC "$<BUILD_INTERFACE:${PROJECT_SOURCE_DIR}/src>"
)




find_package(Microsoft.GSL REQUIRED)
target_include_directories(${PROJECT_NAME} PUBLIC ${Microsoft.GSL_INCLUDE_DIRS})

