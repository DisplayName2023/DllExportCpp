


```

# TARGET_RUNTIME_DLLS generator expression available since CMake 3.21
set( dll_libraries "$<TARGET_RUNTIME_DLLS:${PROJECT_NAME}>" )
# Copy the required dlls
add_custom_command(TARGET ${PROJECT_NAME} POST_BUILD COMMAND
     ${CMAKE_COMMAND} -E copy_if_different ${dll_libraries} $<TARGET_FILE_DIR:${PROJECT_NAME}>
    COMMAND_EXPAND_LISTS
)


```