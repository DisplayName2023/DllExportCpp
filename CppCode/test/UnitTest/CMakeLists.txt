cmake_minimum_required( VERSION  3.28 )


project( UnitTest )


add_executable( ${PROJECT_NAME}
    main.cpp
    EvaluatorTests.cpp
)


target_include_directories(${PROJECT_NAME} 
    PUBLIC ${CMAKE_SOURCE_DIR}/src
)


find_package(gtest REQUIRED)
target_include_directories(${PROJECT_NAME} PUBLIC ${GTest_INCLUDE_DIRS})
target_link_libraries(${PROJECT_NAME}  ${GTest_LIBRARIES})

find_package(evaluatorcppcli REQUIRED)
target_include_directories(${PROJECT_NAME} PUBLIC ${evaluatorcppcli_INCLUDE_DIRS})
target_link_libraries(${PROJECT_NAME}  ${evaluatorcppcli_LIBRARIES})
# add_test(${PROJECT_NAME} ${PROJECT_NAME})


get_target_property(target_type EvaluatorCpp TYPE)
message("EvaluatorCpp is of type: ${target_type}")

if( target_type STREQUAL "SHARED_LIBRARY" )
    install(TARGETS EvaluatorCpp RUNTIME DESTINATION ${CMAKE_CURRENT_BINARY_DIR})
endif()




if( true )

add_custom_command(TARGET ${PROJECT_NAME} POST_BUILD
  COMMAND ${CMAKE_COMMAND} -E copy_directory_if_different $<TARGET_FILE_DIR:EvaluatorCpp> $<TARGET_FILE_DIR:${PROJECT_NAME}> 
  COMMAND_EXPAND_LISTS
)
else()

add_custom_command(TARGET ${PROJECT_NAME} POST_BUILD
  COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:EvaluatorCpp> $<TARGET_FILE_DIR:${PROJECT_NAME}> 
  COMMAND_EXPAND_LISTS
)
endif()
